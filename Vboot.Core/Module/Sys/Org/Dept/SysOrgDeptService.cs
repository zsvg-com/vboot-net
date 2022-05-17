using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class SysOrgDeptService : BaseMainService<SysOrgDept>, ITransient
{
    public SysOrgDeptService(ISqlSugarRepository<SysOrgDept> repo)
    {
        base.repo = repo;
    }

    public new async Task InsertAsync(SysOrgDept dept)
    {
        //处理上级部门与部门层级信息
        dept.id = YitIdHelper.NextId() + "";
        if (dept.parent != null)
        {
            dept.pid = dept.parent.id;
            var parentTier = await repo.Context.Queryable<SysOrgDept>()
                .Where(it => it.id == dept.pid).Select(it => it.tier).SingleAsync();
            dept.tier = parentTier + dept.id + "x";
        }
        else
        {
            dept.tier = "x" + dept.id + "x";
        }

        //事务包裹处理数据库操作
        using var tran = repo.Context.UseTran();
        await base.InsertAsync(dept);
        //增加sys_org_dept时同时增加sys_org表
        await repo.Context.Insertable(new SysOrg {id = dept.id, name = dept.name}).ExecuteCommandAsync();
        tran.CommitTran();
    }


    public new async Task UpdateAsync(SysOrgDept dept)
    {
        //处理上级部门与部门层级信息
        if (dept.parent != null)
        {
            dept.pid = dept.parent.id;
            var parentTier = await repo.Context.Queryable<SysOrgDept>()
                .Where(it => it.id == dept.pid).Select(it => it.tier).SingleAsync();
            dept.tier = parentTier + dept.id + "x";
            var arr = parentTier.Split("x");
            if (arr.Any(str => dept.id == str))
            {
                throw new Exception("父部门不能为自己或者自己的子部门");
            }
        }
        else
        {
            dept.tier = "x" + dept.id + "x";
        }

        var olderTier = await repo.Context.Queryable<SysOrgDept>()
            .Where(it => it.id == dept.id).Select(it => it.tier).SingleAsync();
        //事务包裹处理数据库操作
        using var tran = repo.Context.UseTran();
        await base.UpdateAsync(dept);
        //修改sys_org_dept时同时修改sys_org表name字段
        await repo.Context.Updateable(new SysOrg {id = dept.id, name = dept.name})
            .UpdateColumns(it => new {it.name}).ExecuteCommandAsync();
        //修改sys_org_dept层级时需要同时修改子部门，部门下的员工，岗位的层级信息
        await DealDeptTier(olderTier, dept.tier, dept.id);
        // 事务测试
        // await repo.Context.Updateable<SysOrg>(new {id="228949221105733",name="张三张三张三张三张三张三张三张三张三张三张三张三张三"})
        //     .UpdateColumns(it => new { it.name}).ExecuteCommandAsync();
        await DealUserTier(olderTier, dept.tier);
        await DealPostTier(olderTier, dept.tier);
        tran.CommitTran();
    }

    private async Task DealDeptTier(string oldTier, string newTier, string id)
    {
        var idNameList = await
            repo.Ado.SqlQueryAsync<ZidName>(
                "select id,tier as name from sys_org_dept where tier like @oldTier and id<>@id",
                new {id, oldTier = oldTier + "%"});

        var dtList = new List<Dictionary<string, object>>();
        foreach (var idName in idNameList)
        {
            var dt = new Dictionary<string, object>
            {
                {"id", idName.id}, {"tier", idName.name.Replace(oldTier, newTier)}
            };
            dtList.Add(dt);
        }

        await repo.Context.Updateable(dtList).AS("sys_org_dept").WhereColumns("id").ExecuteCommandAsync();
    }

    private async Task DealUserTier(string oldTier, string newTier)
    {
        var idNameList = await
            repo.Ado.SqlQueryAsync<ZidName>(
                "select id,tier as name from sys_org_user where tier like @oldTier",
                new {oldTier = oldTier + "%"});

        var list = new List<Dictionary<string, object>>();
        foreach (var idName in idNameList)
        {
            var dt = new Dictionary<string, object>
            {
                {"id", idName.id}, {"tier", idName.name.Replace(oldTier, newTier)}
            };
            list.Add(dt);
        }

        await repo.Context.Updateable(list).AS("sys_org_user").WhereColumns("id").ExecuteCommandAsync();
    }

    private async Task DealPostTier(string oldTier, string newTier)
    {
        var idNameList = await
            repo.Ado.SqlQueryAsync<ZidName>(
                "select id,tier as name from sys_org_post where tier like @oldTier",
                new {oldTier = oldTier + "%"});

        var list = new List<Dictionary<string, object>>();
        foreach (var idName in idNameList)
        {
            var dt = new Dictionary<string, object>
            {
                {"id", idName.id}, {"tier", idName.name.Replace(oldTier, newTier)}
            };
            list.Add(dt);
        }

        await repo.Context.Updateable(list).AS("sys_org_post").WhereColumns("id").ExecuteCommandAsync();
    }


    public new async Task DeleteAsync(string[] ids)
    {
        //最好是通过外键来控制，部门还有其他地方会用到
        foreach (var id in ids)
        {
            bool flag = await CanDelete(id);
            if (!flag)
            {
                throw new Exception("部门下有子部门，员工或岗位不能删除");
            }
        }

        using var tran = repo.Context.UseTran();
        await repo.Context.Deleteable<SysOrg>().In(ids).ExecuteCommandAsync();
        await repo.Context.Deleteable<SysOrgDept>().In(ids).ExecuteCommandAsync();
        tran.CommitTran();
    }

    private async Task<bool> CanDelete(string id)
    {
        var tier = await repo.Context.Queryable<SysOrgDept>()
            .Where(it => it.id == id).Select(it => it.tier).SingleAsync();

        var deptCount = await
            repo.Context.Queryable<SysOrgDept>().Where(it => it.tier.StartsWith(tier)).CountAsync();
        if (deptCount > 1)
        {
            return false;
        }

        var userCount = await
            repo.Context.Queryable<SysOrgUser>().Where(it => it.tier.StartsWith(tier)).CountAsync();
        if (userCount > 0)
        {
            return false;
        }

        var postCount = await
            repo.Context.Queryable<SysOrgPost>().Where(it => it.tier.StartsWith(tier)).CountAsync();
        if (postCount > 0)
        {
            return false;
        }

        return true;
    }
}