using System.Threading.Tasks;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Common
{
    public class BaseService<TEntity> where TEntity : BaseEntity, new()
    {
        public ISqlSugarRepository<TEntity> repo { get; set; }

        public async Task InsertAsync(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.id))
            {
                entity.id = YitIdHelper.NextId() + "";
            }
            await repo.InsertAsync(entity);
        }

        public async Task<TEntity> SingleAsync(string id)
        {
            return await repo.SingleAsync(t => t.id == id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string[] ids)
        {
            await repo.Context.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
        }
    }
}