using System;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Wf
{
    [ApiDescriptionSettings("Ext",Tag ="流程管理-流程分类" )]
    public class WfTemCateApi : IDynamicApiController
    {
        private readonly WfTemCateService _service;

        public WfTemCateApi(WfTemCateService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> GetTree()
        {
            var treeList = await _service.repo.Context
                .SqlQueryable<WfTemCate>(
                    "select id,pid,name,crtim,uptim,notes from wf_tem_cate order by ornum")
                .ToTreeAsync(it => it.children, it => it.pid, null);
            return treeList;
        }
        
        [QueryParameters]
        public async Task<dynamic> Get()
        {
            var treeList = await _service.repo.Context
                .SqlQueryable<WfTemCate>(
                    "select id,pid,name,crtim,uptim,notes from wf_tem_cate order by ornum")
                .ToTreeAsync(it => it.children, it => it.pid, null);
            return treeList;
        }

        public async Task<WfTemCate> GetOne(string id)
        {
            var menu = await _service.repo.Context.Queryable<WfTemCate>()
                .Where(it => it.id == id).FirstAsync();
            if (menu.pid != null)
            {
                menu.pname = await _service.repo.Context.Queryable<WfTemCate>()
                    .Where(it => it.id == menu.pid).Select(it => it.name).SingleAsync();
            }

            return menu;
        }

        public async Task Post(WfTemCate cate)
        {
            cate.id = YitIdHelper.NextId() + "";
            await _service.InsertAsync(cate);
        }

        public async Task Put(WfTemCate cate)
        {
            await _service.UpdateAsync(cate);
        }

        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            foreach (var id in idArr)
            {
                var count = await
                    _service.repo.Context.Queryable<WfTemCate>().Where(it => it.pid == id).CountAsync();
                if (count > 0)
                {
                    throw new Exception("有子分类无法删除");
                }
            }
            await _service.DeleteAsync(idArr);
        }
    }
}