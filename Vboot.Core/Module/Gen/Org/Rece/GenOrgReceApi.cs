using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Gen;

[ApiDescriptionSettings("Gen")]
public class GenOrgReceApi : IDynamicApiController
{
    private readonly SysOrgReceService _service;

    public GenOrgReceApi(SysOrgReceService service)
    {
        _service = service;
    }

    public async Task Post(List<SysOrgRece> reces)
    {
        if (reces != null && reces.Count > 0)
        {
           await _service.update(reces);
        }
    }

    [QueryParameters]
    public async Task<dynamic> Get(int type)
    {
        string userId = XuserUtil.getUserId();
        List<dynamic> list = new List<dynamic>();
        if ((type & 8) != 0)//用户
        {
            var userList = await _service._repo.Context.Queryable<SysOrgRece, SysOrgUser, SysOrgDept>((t, u, d)
                    => new JoinQueryInfos(JoinType.Inner, u.id == t.orgid, JoinType.Inner, d.id == u.deptid))
                .OrderBy(t => t.uptim, OrderByType.Desc)
                .Where(t => t.userid == userId)
                .Select((t, u, d) => new {id = t.orgid, name = u.name, dept = d.name})
                .ToListAsync();
            list.AddRange(userList);
        }

        if ((type & 2) != 0||(type & 1) != 0)  //部门
        {
            var deptList = await _service._repo.Context.Queryable<SysOrgRece, SysOrgDept>((t, d)
                    => new JoinQueryInfos(JoinType.Inner, d.id == t.orgid))
                .OrderBy(t => t.uptim, OrderByType.Desc)
                .Where(t => t.userid == userId)
                .Select((t, d) => new {id = t.orgid, name=d.name})
                .ToListAsync();
            list.AddRange(deptList);
        }

        if ((type & 4) != 0) //岗位
        {
            var postList = await _service._repo.Context.Queryable<SysOrgRece, SysOrgPost, SysOrgDept>((t, u, d)
                    => new JoinQueryInfos(JoinType.Inner, u.id == t.orgid, JoinType.Inner, d.id == u.deptid))
                .OrderBy(t => t.uptim, OrderByType.Desc)
                .Where(t => t.userid == userId)
                .Select((t, u, d) => new {id = t.orgid, name = u.name, dept = d.name})
                .ToListAsync();
            list.AddRange(postList);
        }

        return list;
    }
}