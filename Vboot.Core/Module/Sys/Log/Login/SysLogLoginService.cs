using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys.Job.Log
{
    public class SysLogLoginService : BaseService<SysLogLogin>, ITransient
    {
        public SysLogLoginService(ISqlSugarRepository<SysLogLogin> repo)
        {
            this.repo = repo;
        }
    }
}