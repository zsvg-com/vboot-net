using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    [ApiDescriptionSettings("Ext",Tag ="流程管理-流程实例" )]
    public class WfInsMainApi : IDynamicApiController
    {
        private readonly WfInsMainService _service;

        public WfInsMainApi(WfInsMainService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<WfInsMain>()
                .Select((t) => new {t.id, t.name, t.notes})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }

        public async Task<WfInsMain> GetOne(string id)
        {
            var main = await _service.repo.Context.Queryable<WfInsMain>()
                .Where(it => it.id == id).FirstAsync();
            
            
            return main;
        }

        public async Task Post(WfInsMain main)
        {
            await _service.InsertAsyncWithFlow(main);
        }

        public async Task Put(WfInsMain main)
        {
            await _service.UpdateAsync(main);
        }

        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(idArr);
        }
    }
}