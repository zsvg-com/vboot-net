using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Core.Module.Sys
{
    [ApiDescriptionSettings("Sys",Tag ="组织架构-部门" )]
    public class SysOrgDeptApi : IDynamicApiController
    {
        private readonly SysOrgDeptService _service;

        public SysOrgDeptApi(SysOrgDeptService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get()
        {
            // int i = 0;
            // int j = 0 / i;
            var pp=XreqUtil.GetPp();
            var items = await _service.repo.Context.Queryable<SysOrgDept>()
                .OrderBy(u => u.id,OrderByType.Desc)
                .Select((t) => new {t.id, t.name, t.notes, t.crtim, t.uptim})
                .ToPageListAsync(pp.page, pp.pageSize, pp.total);
            return RestPageResult.Build(pp.total.Value, items);
        }
        
        public async Task<SysOrgDept> GetOne(string id)
        {
            var sysOrgDept= await _service.SingleAsync(id);
            if (sysOrgDept.pid != null)
            {
                sysOrgDept.parent =await _service.repo.Context.Queryable<SysOrg>().InSingleAsync(sysOrgDept.pid);
            }
            return sysOrgDept;
        }

        public async Task Post(SysOrgDept dept)
        {
            await _service.InsertAsync(dept);
        }
        
        public async Task Put(SysOrgDept dept)
        {
            await _service.UpdateAsync(dept);
        }
        
        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(idArr);
        }

    }
}