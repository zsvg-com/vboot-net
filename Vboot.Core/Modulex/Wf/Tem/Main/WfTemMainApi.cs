using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Modulex.Wf
{
    [ApiDescriptionSettings("Ext",Tag ="流程管理-流程模板" )]
    public class WfTemMainApi : IDynamicApiController
    {
        private readonly WfTemMainService _service;

        public WfTemMainApi(WfTemMainService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<WfTemMain>()
                .OrderBy(u => u.ornum)
                .Select((t) => new {t.id, t.name, t.notes})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }
        
        [QueryParameters]
        public async Task<dynamic> GetList()
        {
            var list = await _service.repo.Context
                .SqlQueryable<WfTemMainVo>("select id,name from wf_tem_main")
                .ToListAsync();
            return list;
        }
        
        [QueryParameters]
        public async Task<dynamic> GetTree()
        {
            var treeList = await _service.repo.Context
                .SqlQueryable<WfTemMainVo>(
                    "select id,pid,name,'cate' type from wf_tem_cate union all select id,cateid pid,name,'main' type from wf_tem_main")
                .ToTreeAsync(it => it.children, it => it.pid, null);
            return treeList;
        }
        

        public async Task<WfTemMain> GetOne(string id)
        {
            var main = await _service.repo.Context.Queryable<WfTemMain>()
                .Where(it => it.id == id).FirstAsync();
            
            if (main.cateid != null)
            {
                main.catename = await _service.repo.Context.Queryable<WfTemCate>()
                    .Where(it => it.id == main.cateid).Select(it => it.name).SingleAsync();
            }
            
            return main;
        }

        public async Task Post(WfTemMain main)
        {
            await _service.InsertAsync(main);
        }

        public async Task Put(WfTemMain main)
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