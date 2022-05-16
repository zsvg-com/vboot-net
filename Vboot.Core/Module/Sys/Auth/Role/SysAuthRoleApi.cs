using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys
{
    [ApiDescriptionSettings("Sys", Tag = "权限管理-角色")]
    public class SysAuthRoleApi : IDynamicApiController
    {
        private readonly SysAuthRoleService _service;

        public SysAuthRoleApi(
            SysAuthRoleService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get()
        {
            var pp=XreqUtil.GetPp();
            var items = await _service.repo.Context.Queryable<SysAuthRole>()
                .OrderBy(u => u.ornum)
                .Select((t) => new {t.id, t.name, t.notes, t.crtim, t.uptim})
                .ToPageListAsync(pp.page, pp.pageSize, pp.total);
            return RestPageResult.Build(pp.total.Value, items);
        }

        public async Task<SysAuthRole> GetOne(string id)
        {
            var role = await _service.repo.Context.Queryable<SysAuthRole>()
                .Mapper<SysAuthRole, SysOrg, SysAuthRoleOrg>(it =>
                    ManyToMany.Config(it.rid, it.oid))
                .Mapper<SysAuthRole, SysAuthMenu, SysAuthRoleMenu>(it =>
                    ManyToMany.Config(it.rid, it.mid))
                .Where(it => it.id == id).FirstAsync();
            return role;
        }

        public async Task Post(SysAuthRole role)
        {
            role.id = YitIdHelper.NextId() + "";
            var romaps = new List<SysAuthRoleOrg>();
            foreach (var org in role.orgs)
            {
                romaps.Add(new SysAuthRoleOrg {rid = role.id, oid = org.id});
            }
            var rmmaps = new List<SysAuthRoleMenu>();
            foreach (var menu in role.menus)
            {
                rmmaps.Add(new SysAuthRoleMenu {rid = role.id, mid = menu.id});
            }
            await _service.InsertAsync(role, romaps,rmmaps);
        }
        
        public async Task PostReperm()
        {
           await _service.repo.Context.Updateable<SysOrgUser>().SetColumns(it => it.retag == false).Where(it => it.retag == true)
                .ExecuteCommandAsync();
        }

        public async Task Put(SysAuthRole role)
        {
            var romaps = new List<SysAuthRoleOrg>();
            foreach (var org in role.orgs)
            {
                romaps.Add(new SysAuthRoleOrg {rid = role.id, oid = org.id});
            }
            var rmmaps = new List<SysAuthRoleMenu>();
            foreach (var menu in role.menus)
            {
                rmmaps.Add(new SysAuthRoleMenu {rid = role.id, mid = menu.id});
            }
            await _service.UpdateAsync(role, romaps,rmmaps);
        }

        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(ids);
        }
    }
}