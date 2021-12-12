using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Furion;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.EventBus;
using Furion.FriendlyException;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAParser;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Pub
{
    // [ApiDescriptionSettings("Auth", Tag = "登录与注销")]
    //[ApiDescriptionSettings("Auth",Tag = "登录与注销")]
    public class PubAuthApi : IDynamicApiController, ITransient
    {
        private readonly LoginService _loginService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventPublisher _eventPublisher;
        private readonly IUserManager _userManager; // 用户管理

        public PubAuthApi(LoginService loginService,
            IHttpContextAccessor httpContextAccessor,
            IEventPublisher eventPublisher,
            IUserManager userManager)
        {
            _loginService = loginService;
            _httpContextAccessor = httpContextAccessor;
            _eventPublisher = eventPublisher;
            _userManager = userManager;
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<LoginOutput2> LoginAsync(LoginInput input)
        {
            
            var dbUser = await _loginService.getDbUser(input.username);
            var password = MD5Encryption.Encrypt("abc" + input.password + "xyz");
            if (password != dbUser.pacod)
            {
                throw Oops.Oh(ErrorCode.D1000);
            }
            
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
            {
                {ClaimConst.CLAINM_USERID, dbUser.id},
                {ClaimConst.TENANT_ID, "1"},
                {ClaimConst.CLAINM_ACCOUNT, input.username},
                {ClaimConst.CLAINM_NAME, dbUser.name},
                {ClaimConst.CLAINM_SUPERADMIN, true},
            });

            // var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            // {
            //     {"UserId", dbUser.id},
            //     {"Account", dbUser.name}
            // });
            _httpContextAccessor.HttpContext.SigninToSwagger(accessToken);
            
            var httpContext = App.HttpContext;
            await _eventPublisher.PublishAsync(new ChannelEventSource("Update:UserLoginInfo",
                new SysOrgUser {id = dbUser.id, laloi = httpContext.GetLocalIpAddressToIPv4(), lalot = DateTime.Now}));
            
            var output = new LoginOutput2
            {
                token = accessToken,
                zuser = new Zuser() {id = dbUser.id, name = dbUser.name, usnam = input.username}
            };
            return output;
        }
        
        [HttpPost("/logout")]
        public void LogoutAsync()
        {
            
        }


        // [HttpGet("/getUserInfo")]
        // [AllowAnonymous]
        // public async Task<string> getUserInfo()
        // {
        //     return "userinfo";
        // }

        [HttpGet("/getUserInfo")]
        [AllowAnonymous]
        public async Task<LoginOutput> getUserInfo()
        {
            
            var user = _userManager.User;

            var httpContext = App.GetService<IHttpContextAccessor>().HttpContext;
            var loginOutput = user.Adapt<LoginOutput>();

            var crtim=user.lalot = DateTime.Now;
            var ip = HttpNewUtil.Ip;
            ip = user.laloi = string.IsNullOrEmpty(user.laloi) ? httpContext.GetRemoteIpAddressToIPv4() : ip;

            var clent = Parser.GetDefault().Parse(httpContext.Request.Headers["User-Agent"]);
            var agbro= clent.UA.Family + clent.UA.Major;
            var ageos= clent.OS.Family + clent.OS.Major;

            // 员工信息
            // loginOutput.LoginEmpInfo = await _sysEmpService.GetEmpInfo(userId);

            // 角色信息
            // loginOutput.Roles = await _sysRoleService.GetUserRoleList(userId);

            // 权限信息
            // loginOutput.Permissions = await _sysMenuService.GetLoginPermissionList(userId);

            // 数据范围信息(机构Id集合)
            // loginOutput.DataScopes = await _sysUserService.GetUserDataScopeIdList(userId);

            // 具备应用信息（多系统，默认激活一个，可根据系统切换菜单）,返回的结果中第一个为激活的系统
            // loginOutput.Apps = await _sysAppService.GetLoginApps(userId);

            // // 菜单信息
            // if (loginOutput.Apps.Count > 0)
            // {
            //     var defaultActiveAppCode = loginOutput.Apps.FirstOrDefault().Code;
            //     loginOutput.Menus = await _sysMenuService.GetLoginMenusAntDesign(userId, "");
            //     loginOutput.Menus.ForEach(item => { item.Hidden = item.Application != defaultActiveAppCode; });
            // }
            
            
            
            // 增加登录日志
            await _eventPublisher.PublishAsync(new ChannelEventSource("Create:LoginLog",
                new SysLogLogin
                {
                    id = YitIdHelper.NextId()+"",
                    name = user.name,
                    ip = ip,
                    agbro = agbro,
                    ageos = ageos,
                    crtim = crtim,
                    usnam = user.usnam
                }));
            

            return loginOutput;
        }
    }
}