using System;
using System.Collections.Generic;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Gen;

[ApiDescriptionSettings("Gen")]
public class GenOrgDeptApi : IDynamicApiController
{
    private readonly ISqlSugarRepository<SysOrgDept> repo;

    public GenOrgDeptApi(ISqlSugarRepository<SysOrgDept> Repo)
    {
        repo = Repo;
    }

    public List<Ztree> GetTree()
    {
        var trees = repo.Context.SqlQueryable<Ztree>("select id,pid,name from sys_org_dept")
            .ToTreeAsync(it => it.children, it => it.pid, null).Result;
        Console.WriteLine(trees);
        return trees;
    }
}