﻿using System.Threading.Tasks;
using Furion.DynamicApiController;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys.Job.Log
{
    public class SysJobLogApi: IDynamicApiController
    {
        
        private readonly SysJobLogService _service;

        public SysJobLogApi(SysJobLogService sysJobLogService)
        {
            _service = sysJobLogService;
        }
        
        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize, string name)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<SysJobLog>()
                .Select((t) => new {t.id, t.name, t.sttim,t.entim,t.msg,t.ret})
                .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
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