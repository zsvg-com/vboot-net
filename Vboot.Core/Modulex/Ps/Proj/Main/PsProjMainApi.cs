using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Ass;
using Vboot.Core.Module.Pub;

namespace Vboot.Core.Modulex.Ps
{
    [ApiDescriptionSettings("Ext",Tag ="项目管理-主信息" )]
    public class PsProjMainApi : IDynamicApiController
    {
        private readonly PsProjMainService _service;

        public PsProjMainApi(PsProjMainService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<PsProjMain>()
                .OrderBy(u => u.ornum)
                .Select((t) => new {t.id, t.name, t.notes})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }

        public async Task<PsProjMain> GetOne(string id)
        {
            var main = await _service.repo.Context.Queryable<PsProjMain>()
                .Where(it => it.id == id).FirstAsync();
            
            if (main.cateid != null)
            {
                main.catename = await _service.repo.Context.Queryable<PsProjCate>()
                    .Where(it => it.id == main.cateid).Select(it => it.name).SingleAsync();
            }
            
            return main;
        }

        public async Task Post(PsProjMain main)
        {
            await _service.InsertAsync(main);
        }

        public async Task Put(PsProjMain main)
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