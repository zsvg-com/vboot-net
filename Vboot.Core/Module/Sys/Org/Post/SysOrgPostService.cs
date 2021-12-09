using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    public class SysOrgPostService : BaseMainService<SysOrgPost>, ITransient
    {
        public SysOrgPostService(ISqlSugarRepository<SysOrgPost> repo)
        {
            base.repo = repo;
        }

        
        
        public async Task InsertAsync(SysOrgPost post, List<SysOrgPostOrg> pumaps)
        {
            if (post.dept != null)
            {
                post.deptid = post.dept.id;
                var deptTier = await repo.Context.Queryable<SysOrgDept>()
                    .Where(it => it.id == post.deptid).Select(it => it.tier).SingleAsync();
                post.tier = deptTier + post.id + "x";
            }
            
            using var tran = repo.Context.UseTran();
            await base.InsertAsync(post);
            await repo.Context.Insertable(new SysOrg {id = post.id, name = post.name}).ExecuteCommandAsync();
            await repo.Context.Insertable(pumaps).ExecuteCommandAsync();
            tran.CommitTran();
        }


        public async Task UpdateAsync(SysOrgPost post, List<SysOrgPostOrg> pumaps)
        {
            if (post.dept != null)
            {
                post.deptid = post.dept.id;
                var deptTier = await repo.Context.Queryable<SysOrgDept>()
                    .Where(it => it.id == post.deptid).Select(it => it.tier).SingleAsync();
                post.tier = deptTier + post.id + "x";
            }
            using var tran = repo.Context.UseTran();
            await base.UpdateAsync(post);
            await repo.Context.Updateable(new SysOrg {id = post.id, name = post.name})
                .UpdateColumns(it => new {it.name}).ExecuteCommandAsync();
            await repo.Context.Deleteable<SysOrgPostOrg>().Where(it => it.pid == post.id).ExecuteCommandAsync();
            await repo.Context.Insertable(pumaps).ExecuteCommandAsync();
            tran.CommitTran();
        }
    }
}