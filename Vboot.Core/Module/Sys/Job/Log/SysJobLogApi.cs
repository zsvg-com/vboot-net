using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Core.Module.Sys.Job.Log
{
    
    [ApiDescriptionSettings("Sys",Tag ="定时任务-日志" )]
    public class SysJobLogApi: IDynamicApiController
    {
        
        private readonly SysJobLogService _service;

        public SysJobLogApi(SysJobLogService sysJobLogService)
        {
            _service = sysJobLogService;
        }
        
        [QueryParameters]
        public async Task<dynamic> Get(string name)
        {
            var pp=XreqUtil.GetPp();
            var items = await _service.repo.Context.Queryable<SysJobLog>()
                .Select((t) => new {t.id, t.name, t.sttim,t.entim,t.msg,t.ret})
                .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
                .ToPageListAsync(pp.page, pp.pageSize, pp.total);
            return RestPageResult.Build(pp.total.Value, items);
        }
        
        public async Task<SysJobLog> GetOne(string id)
        {
            var cate = await _service.SingleAsync(id);
            return cate;
        }
        
        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(idArr);
        }
    }
}