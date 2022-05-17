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

namespace Vboot.Core.Module.Pub;

//[ApiDescriptionSettings("Auth",Tag = "登录与注销")]
/// <summary>
/// 登录注销
/// </summary>
public class AuthApi : IDynamicApiController, ITransient
{
    private readonly LoginService _loginService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEventPublisher _eventPublisher;
    private readonly IUserManager _userManager; // 用户管理
    private readonly SysCacheService _cache;

    public AuthApi(LoginService loginService,
        IHttpContextAccessor httpContextAccessor,
        IEventPublisher eventPublisher,
        IUserManager userManager,
        SysCacheService sysCacheService)
    {
        _loginService = loginService;
        _httpContextAccessor = httpContextAccessor;
        _eventPublisher = eventPublisher;
        _userManager = userManager;
        _cache = sysCacheService;
    }

    /// <summary>
    /// 登录
    /// </summary>
    [HttpPost("/login")]
    [AllowAnonymous]
    public async Task<Dictionary<string, object>> LoginAsync(LoginInput input)
    {
        var duser = await _loginService.getDbUser(input.username);
        var password = MD5Encryption.Encrypt("abc" + input.password + "xyz");
        if (password != duser.pacod)
        {
            throw Oops.Oh(ErrorCode.D1000);
        }

        Dictionary<string, object> backDict = new Dictionary<string, object>();

        //初始化用户
        Zuser zuser = new Zuser(duser.id, duser.name, input.username);
        _loginService.InitUser(zuser, duser, backDict);
        // _cache.Set(redisKey,zuser);

        string redisKey = "" + YitIdHelper.NextId();
        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>
        {
            {ClaimConst.CLAINM_USERID, duser.id},
            {ClaimConst.TENANT_ID, "1"},
            {ClaimConst.CLAINM_ACCOUNT, input.username},
            {ClaimConst.CLAINM_NAME, duser.name},
            {ClaimConst.CLAINM_SUPERADMIN, zuser.id == "sa"},
            {ClaimConst.CLAINM_PERMS, zuser.perms},
        });


        backDict.Add("token", accessToken);
        // backDict.Add("rtoken", accessToken);
        //设置swagger自动登录
        _httpContextAccessor.HttpContext.SigninToSwagger(accessToken);

        //记录登录日志
        var httpContext = App.HttpContext;
        await _eventPublisher.PublishAsync(new ChannelEventSource("Update:UserLoginInfo",
            new SysOrgUser {id = duser.id, laloi = httpContext.GetLocalIpAddressToIPv4(), lalot = DateTime.Now}));

        return backDict;
    }

    /// <summary>
    /// 注销
    /// </summary>
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

    /// <summary>
    /// 获取用户信息
    /// </summary>
    [HttpGet("/getUserInfo")]
    [AllowAnonymous]
    public async Task<LoginOutput> getUserInfo()
    {
        var user = _userManager.User;

        var httpContext = App.GetService<IHttpContextAccessor>().HttpContext;
        var loginOutput = user.Adapt<LoginOutput>();

        var crtim = user.lalot = DateTime.Now;
        var ip = HttpNewUtil.Ip;
        ip = user.laloi = string.IsNullOrEmpty(user.laloi) ? httpContext.GetRemoteIpAddressToIPv4() : ip;

        var clent = Parser.GetDefault().Parse(httpContext.Request.Headers["User-Agent"]);
        var agbro = clent.UA.Family + clent.UA.Major;
        var ageos = clent.OS.Family + clent.OS.Major;

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
                id = YitIdHelper.NextId() + "",
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