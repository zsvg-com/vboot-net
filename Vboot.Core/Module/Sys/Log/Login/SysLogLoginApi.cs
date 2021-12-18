using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys.Job.Log
{
    [ApiDescriptionSettings("Sys",Tag ="系统日志-登录日志" )]
    public class SysLogLoginApi: IDynamicApiController
    {
        
        private readonly SysLogLoginService _service;

        public SysLogLoginApi(SysLogLoginService sysLogLoginService)
        {
            _service = sysLogLoginService;
        }
        
        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize, string name)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<SysLogLogin>()
                .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }
        
        public async Task<SysLogLogin> GetOne(string id)
        {
            return await _service.SingleAsync(id);
        }
        
        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(idArr);
        }
        
        public async Task DeleteAll(string ids)
        {
            var idArr = ids.Split(",");
            await _service.repo.Context.Deleteable<SysLogLogin>().ExecuteCommandAsync();
        }
    }
}