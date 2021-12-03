
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Threading.Tasks;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Common
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager : IUserManager, IScoped
    {
        private readonly ISqlSugarRepository<SysOrgUser> _sysUserRep; // 用户表仓储   
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string UserId
        {
            get => _httpContextAccessor.HttpContext.User.FindFirst(ClaimConst.CLAINM_USERID)?.Value;
        }

        public string Account
        {
            get => _httpContextAccessor.HttpContext.User.FindFirst(ClaimConst.CLAINM_ACCOUNT)?.Value;
        }

        public string Name
        {
            get => _httpContextAccessor.HttpContext.User.FindFirst(ClaimConst.CLAINM_NAME)?.Value;
        }

        public bool SuperAdmin
        {
            get => _httpContextAccessor.HttpContext.User.FindFirst(ClaimConst.CLAINM_SUPERADMIN)?.Value == ((int)AdminType.SuperAdmin).ToString();
        }

        public SysOrgUser User
        {
            get => _sysUserRep.FirstOrDefault(u => u.id == UserId);
        }

        public UserManager(ISqlSugarRepository<SysOrgUser> sysUserRep,
                           IHttpContextAccessor httpContextAccessor)
        {
            _sysUserRep = sysUserRep;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public async Task<SysOrgUser> CheckUserAsync(string userId, bool tracking = true)
        {
            var user = await _sysUserRep.FirstOrDefaultAsync(u => u.id == userId);
            return user ?? throw Oops.Oh(ErrorCode.D1002);
        }

        /// <summary>
        /// 获取用户员工信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<SysOrgUser> GetUserEmpInfo(string userId)
        {
            var emp = await _sysUserRep.FirstOrDefaultAsync(u => u.id == userId);
            return emp ?? throw Oops.Oh(ErrorCode.D1002);
        }
    }
}