using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    [ApiDescriptionSettings("Ext",Tag ="流程管理-流程模板" )]
    public class WfTempMainApi : IDynamicApiController
    {
        private readonly WfTempMainService _service;

        public WfTempMainApi(WfTempMainService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<WfTempMain>()
                .OrderBy(u => u.ornum)
                .Select((t) => new {t.id, t.name, t.notes})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }

        public async Task<WfTempMain> GetOne(string id)
        {
            var main = await _service.repo.Context.Queryable<WfTempMain>()
                .Where(it => it.id == id).FirstAsync();
            
            if (main.cateid != null)
            {
                main.catename = await _service.repo.Context.Queryable<WfTempCate>()
                    .Where(it => it.id == main.cateid).Select(it => it.name).SingleAsync();
            }
            
            return main;
        }

        public async Task Post(WfTempMain main)
        {
            await _service.InsertAsync(main);
        }

        public async Task Put(WfTempMain main)
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