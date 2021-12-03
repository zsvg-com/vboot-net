using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys
{
    public class SysOrgUserService : BaseMainService<SysOrgUser>, ITransient
    {
        public SysOrgUserService(ISqlSugarRepository<SysOrgUser> repo)
        {
            base.repo = repo;
        }

        public new async Task InsertAsync(SysOrgUser user)
        {
            user.id = YitIdHelper.NextId() + "";
            if (user.dept != null)
            {
                user.deptid = user.dept.id;
                var deptTier = await repo.Context.Queryable<SysOrgDept>()
                    .Where(it => it.id == user.deptid).Select(it => it.tier).SingleAsync();
                user.tier = deptTier + user.id + "x";
            }
            
            using var tran = repo.Context.UseTran();
            await base.InsertAsync(user);
            await repo.Context.Insertable(new SysOrg {id = user.id, name = user.name}).ExecuteCommandAsync();
            tran.CommitTran();
        }


        public new async Task UpdateAsync(SysOrgUser user)
        {
            if (user.dept != null)
            {
                user.deptid = user.dept.id;
                var deptTier = await repo.Context.Queryable<SysOrgDept>()
                    .Where(it => it.id == user.deptid).Select(it => it.tier).SingleAsync();
                user.tier = deptTier + user.id + "x";
            }

            using var tran = repo.Context.UseTran();
            await base.UpdateAsync(user);
            await repo.Context.Updateable(new SysOrg {id = user.id, name = user.name})
                .UpdateColumns(it => new {it.name}).ExecuteCommandAsync();
            tran.CommitTran();
        }
    }
}