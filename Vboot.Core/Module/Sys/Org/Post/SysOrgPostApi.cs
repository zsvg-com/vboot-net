using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

[ApiDescriptionSettings("Sys", Tag = "组织架构-岗位")]
public class SysOrgPostApi : IDynamicApiController
{
    private readonly SysOrgPostService _postService;
    private readonly SysOrgDeptService _deptService;

    public SysOrgPostApi(
        SysOrgPostService postService,
        SysOrgDeptService deptService)
    {
        _postService = postService;
        _deptService = deptService;
    }

    [QueryParameters]
    public async Task<dynamic> Get(string name, string deptid)
    {
        var pp = XreqUtil.GetPp();
        var expable = Expressionable.Create<SysOrgPost>();
        if (!string.IsNullOrWhiteSpace(name))
        {
            expable.And(t => t.name.Contains(name.Trim()));
        }
        else
        {
            if (deptid=="")
            {
                expable.And(t => t.deptid == null);
            }
            else if (!string.IsNullOrWhiteSpace(deptid))
            {
                expable.And(t => t.deptid == deptid);
            }
        }
        var items = await _postService.repo.Context.Queryable<SysOrgPost>()
            .Where(expable.ToExpression())
            .OrderBy(u => u.ornum)
            .Select((t) => new {t.id, t.name, t.notes, t.crtim, t.uptim})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<SysOrgPost> GetOne(string id)
    {
        var post = await _postService.repo.Context.Queryable<SysOrgPost>()
            .Mapper<SysOrgPost, SysOrg, SysOrgPostOrg>(it =>
                ManyToMany.Config(it.pid, it.oid))
            .Where(it => it.id == id).FirstAsync();
        if (post.deptid != null)
        {
            post.dept = await _deptService.SingleAsync(post.deptid);
        }

        return post;
    }

    public async Task Post(SysOrgPost post)
    {
        if (post.dept != null)
        {
            post.deptid = post.dept.id;
        }

        post.id = YitIdHelper.NextId() + "";
        var postUsers = new List<SysOrgPostOrg>();
        if (post.users != null)
        {
            foreach (var user in post.users)
            {
                postUsers.Add(new SysOrgPostOrg {pid = post.id, oid = user.id});
            }
        }

        await _postService.InsertAsync(post, postUsers);
    }

    public async Task Put(SysOrgPost post)
    {
        if (post.dept != null)
        {
            post.deptid = post.dept.id;
        }

        var postUsers = new List<SysOrgPostOrg>();
        if (post.users != null)
        {
            foreach (var user in post.users)
            {
                postUsers.Add(new SysOrgPostOrg {pid = post.id, oid = user.id});
            }
        }
        await _postService.UpdateAsync(post, postUsers);
    }

    public async Task Delete(string ids)
    {
        await _postService.DeleteAsync(ids);
    }
}