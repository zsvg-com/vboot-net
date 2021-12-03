using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Ass
{
    [ApiDescriptionSettings("Ass",Tag ="字典数据" )]
    public class AssDictDataApi : IDynamicApiController
    {
        private readonly AssDictDataService _service;

        public AssDictDataApi(AssDictDataService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize,string cateid,string name)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<AssDictData>()
                .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
                .Where((t) => t.cateid==cateid)
                .OrderBy(u => u.ornum)
                .Select((t) => new {t.id, t.name,t.code, t.notes})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }

        public async Task<AssDictData> GetOne(string id)
        {
            var data = await _service.repo.Context.Queryable<AssDictData>()
                .Where(it => it.id == id).FirstAsync();
            return data;
        }

        public async Task Post(AssDictData data)
        {
            await _service.InsertAsync(data);
        }

        public async Task Put(AssDictData data)
        {
            await _service.UpdateAsync(data);
        }

        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(idArr);
        }
    }
}