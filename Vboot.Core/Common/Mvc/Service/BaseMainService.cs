using System;
using System.Threading.Tasks;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Common
{
    public class BaseMainService<TEntity> where TEntity : BaseMainEntity, new()
    {
        public ISqlSugarRepository<TEntity> repo { get; set; }

        public async Task<TEntity> SingleAsync(string id)
        {
            return await repo.SingleAsync(t => t.id == id);
        }
        
        public async Task<string> InsertAsync(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.id))
            {
                // entity.id = Guid.NewGuid().ToString("N");//UUID
                entity.id = YitIdHelper.NextId() + ""; //雪花ID
            }
            entity.crtim = DateTime.Now;
            entity.avtag = true;
            
            await repo.InsertAsync(entity);
            return entity.id;
        }
        
        public async Task<string> UpdateAsync(TEntity entity)
        {
            entity.uptim = DateTime.Now;
            await repo.UpdateAsync(entity);
            return entity.id;
        }
        
        public async Task DeleteAsync(string[] ids)
        {
           await repo.Context.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
        }
        
    }
}