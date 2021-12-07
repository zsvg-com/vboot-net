using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;

namespace Vboot.Core.Module.Sys.Job.Log
{
    public class SysJobLogService : ITransient
    {
        public ISqlSugarRepository<SysJobLog> repo { get; }

        public SysJobLogService(ISqlSugarRepository<SysJobLog> repo)
        {
            this.repo = repo;
        }

        public async Task<SysJobLog> SingleAsync(string id)
        {
            return await repo.SingleAsync(t => t.id == id);
        }

        public async Task DeleteAsync(string[] ids)
        {
            await repo.Context.Deleteable<SysJobLog>().In(ids).ExecuteCommandAsync();
        }
    }
}