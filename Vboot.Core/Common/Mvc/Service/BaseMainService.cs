using System;
using System.Threading.Tasks;
using SqlSugar;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Common
{
    public class BaseMainService<TEntity> where TEntity : BaseMainEntity, new()
    {
        public ISqlSugarRepository<TEntity> repo { get; set; }

        public async Task<TEntity> SingleAsync(string id)
        {
            var main=await repo.SingleAsync(t => t.id == id);
            if (main.crmid != null)
            {
              main.crman= await repo.Context.Queryable<SysOrg>()
                  .Where(it => it.id == main.crmid).SingleAsync();
            }
            if (main.upmid != null)
            {
                main.upman= await repo.Context.Queryable<SysOrg>()
                    .Where(it => it.id == main.upmid).SingleAsync();
            }
            return main;
        }

        // public async Task<dynamic> GetPageList(dynamic sugarQueryable)
        // {
        //     var pp= XreqUtil.GetPp(); 
        //     var items =await sugarQueryable.ToPageListAsync(pp.page, pp.pageSize, pp.total);
        //     return RestPageResult.Build(pp.total.Value, items);
        // }

        public async Task<string> InsertAsync(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.id))
            {
                // entity.id = Guid.NewGuid().ToString("N");//UUID
                entity.id = YitIdHelper.NextId() + ""; //雪花ID
            }

            entity.crmid = XuserUtil.getUserId();
            entity.crtim = DateTime.Now;
            entity.avtag = true;
            
            await repo.InsertAsync(entity);
            return entity.id;
        }

        public async Task<string> UpdateAsync(TEntity entity)
        {
            entity.uptim = DateTime.Now;
            entity.upmid = XuserUtil.getUserId();
            await repo.UpdateAsync(entity);
            return entity.id;
        }

        public async Task DeleteAsync(string ids)
        {
            var idArr = ids.Split(",");
            await repo.Context.Deleteable<TEntity>().In(idArr).ExecuteCommandAsync();
        }
    }
}