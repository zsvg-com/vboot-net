using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Core.Module.Sys.Org.User
{
    [ApiDescriptionSettings("Sys",Tag ="组织架构-用户" )]
    public class SysOrgUserApi : IDynamicApiController
    {
        private readonly SysOrgUserService _userService;
        private readonly SysOrgDeptService _deptService;

        public SysOrgUserApi(
            SysOrgUserService userService,
            SysOrgDeptService deptService)
        {
            _userService = userService;
            _deptService = deptService;
        }

        [QueryParameters]
        public async Task<dynamic> Get()
        {
            var pp = XreqUtil.GetPp();
            var items = await _userService.repo.Context.Queryable<SysOrgUser>()
                .OrderBy(u => u.ornum)
                .Select((t) => new {t.id, t.name, t.notes, t.crtim, t.uptim})
                .ToPageListAsync(pp.page, pp.pageSize, pp.total);
            return RestPageResult.Build(pp.total.Value, items);
        }

        public async Task<SysOrgUser> GetOne(string id)
        {
            var user = await _userService.SingleAsync(id);
            if (user.deptid != null)
            {
                user.dept = await _deptService.SingleAsync(user.deptid);
            }

            return user;
        }

        public async Task Post(SysOrgUser user)
        {
            await _userService.InsertAsync(user);
        }

        public async Task Put(SysOrgUser user)
        {
            await _userService.UpdateAsync(user);
        }

        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _userService.DeleteAsync(ids);
        }
    }
}