using System.Threading.Tasks;
using Furion.DynamicApiController;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Application.Sd.Proj.Main
{
    /// <summary>
    /// 项目信息接口
    /// </summary>
    public class SdProjMainApi : IDynamicApiController
    {
        private readonly SdProjMainService _service;

        public SdProjMainApi(SdProjMainService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取项目信息的分页数据
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<SdProjMain>()
                .OrderBy(t=>t.crtim,OrderByType.Desc)
                .Select((t) => new {t.id, t.name, t.addre, t.crtim})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }

        /// <summary>
        /// 获取单个项目的详细信息
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <returns></returns>
        public async Task<SdProjMain> GetOne(string id)
        {
            var main = await _service.SingleAsync(id);
            return main;
        }

        /// <summary>
        /// 新增项目
        /// </summary>
        public async Task<string> Post(SdProjMain main)
        {
           return await _service.InsertAsync(main);
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        public async Task<string> Put(SdProjMain main)
        {
            return await _service.UpdateAsync(main);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            await _service.DeleteAsync(idArr);
        }
    }
}