using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;

namespace Vboot.Web.Core.Init;

public class ApiGatherService : IScoped
{
    private readonly ISqlSugarRepository<SysPermApi> repo;

    public ApiGatherService(ISqlSugarRepository<SysPermApi> repo)
    {
        this.repo = repo;
    }


    public async void Init()
    {
        List<Yurl> codeUrlList = GetScanList();

        List<string> dbUrlList = repo.Context.Queryable<SysPermApi>()
            // .Where(t => t.type == "get")
            .Select(t => t.id)
            .ToList();

        //收集最大权限位及最大权限位的最大权限码
        string sql = @"select max(u.pos) as pos,max(u.code) as code from sys_perm_api u 
where u.pos = (select max(uu.pos) from sys_perm_api uu)";
        List<dynamic> maxList = repo.Context.Ado.SqlQuery<dynamic>(sql);
        int pos = 0;
        long code = 0;
        if (maxList != null && maxList.Count > 0 && maxList[0].pos != null)
        {
            SysPermApiCache.AUTHPOS = maxList[0].pos;
            pos = maxList[0].pos;
            code = maxList[0].code;
        }

        //比较两个list得到insertList
        List<SysPermApi> insertList = new List<SysPermApi>();
        List<SysPermApi> updateList = new List<SysPermApi>();
        foreach (var codeUrl in codeUrlList)
        {
            bool flag = false;
            foreach (var dbUrl in dbUrlList)
            {
                if (codeUrl.id == dbUrl)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag) //插入
            {
                if (!codeUrl.id.StartsWith("~"))
                {
                    var perm = new SysPermApi();
                    perm.id = codeUrl.id;
                    perm.url = codeUrl.url;
                    perm.pos = 0;
                    perm.code = 0;
                    perm.avtag = true;
                    insertList.Add(perm);
                }
                else
                {
                    if (code >= (1L << 62))
                    {
                        pos += 1;
                        SysPermApiCache.AUTHPOS++;
                        code = 1;
                    }
                    else
                    {
                        if (code == 0)
                        {
                            code = 1;
                        }
                        else
                        {
                            code <<= 1;
                        }
                    }

                    var perm = new SysPermApi();
                    perm.id = codeUrl.id;
                    perm.url = codeUrl.url;
                    perm.pid = codeUrl.pid;
                    perm.type = codeUrl.type;
                    perm.pos = pos;
                    perm.code = code;
                    perm.avtag = true;
                    insertList.Add(perm);
                }
            }
            else //更新
            {
                var perm = new SysPermApi();
                perm.id = codeUrl.id;
                perm.url = codeUrl.url;
                perm.pid = codeUrl.pid;
                perm.type = codeUrl.type;
                perm.avtag = true;
                updateList.Add(perm);
            }
        }

        repo.Context.Updateable<SysPermApi>().SetColumns(it => it.avtag == false).Where(it => it.avtag == true)
            .ExecuteCommand();
        if (updateList.Count > 0)
        {
            await repo.UpdateAsync(updateList);
        }

        if (insertList.Count > 0)
        {
            await repo.InsertAsync(insertList);
        }

        string getsSql = "SELECT url,pos,code from sys_perm_api where avtag= 1 and code<>0 and type='get'";
        List<Yperm> cacheGets = repo.Ado.SqlQuery<Yperm>(getsSql);

        string postsSql = "SELECT url,pos,code from sys_perm_api where avtag= 1 and code<>0 and type='post'";
        List<Yperm> cachePosts = repo.Ado.SqlQuery<Yperm>(postsSql);

        string putsSql = "SELECT url,pos,code from sys_perm_api where avtag= 1 and code<>0 and type='put'";
        List<Yperm> cachePuts = repo.Ado.SqlQuery<Yperm>(putsSql);

        string deletesSql = "SELECT url,pos,code from sys_perm_api where avtag= 1 and code<>0 and type='delete'";
        List<Yperm> cacheDeletes = repo.Ado.SqlQuery<Yperm>(deletesSql);

        SysPermApiCache.GET_URLS = cacheGets.ToArray();
        SysPermApiCache.POST_URLS = cachePosts.ToArray();
        SysPermApiCache.PUT_URLS = cachePuts.ToArray();
        SysPermApiCache.DELETE_URLS = cacheDeletes.ToArray();

        repo.Context.Updateable<SysOrgUser>().SetColumns(it => it.retag == false).Where(it => it.retag == true)
            .ExecuteCommand();
    }


    //通过反射获取最新代码里所有需要认证的URL集合
    private List<Yurl> GetScanList()
    {
        var list = new List<Yurl>();
        var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
        var coreAssemblies = System.IO.Directory.GetFiles(path, "Vboot.Application.dll").Select(Assembly.LoadFrom)
            .ToArray();
        var coreModelTypes = coreAssemblies
            .SelectMany(a => a.DefinedTypes)
            .Select(type => type.AsType())
            .Where(x => x.IsClass && x.Name.EndsWith("Api")).ToList();
        coreModelTypes.ForEach(t =>
        {
            var preUrl = XstrUtil.RenameUrlCase(t.Name.Substring(0, t.Name.Length - 3));
            var methodInfo = t.GetMethods();
            var rootUrl = new Yurl
            {
                id = preUrl,
                url = preUrl,
                pid = null
            };
            list.Add(rootUrl);

            foreach (MethodInfo mInfo in methodInfo)
            {
                if ("GetType" != mInfo.Name && "ToString" != mInfo.Name && "Equals" != mInfo.Name &&
                    "GetHashCode" != mInfo.Name)
                {
                    if (mInfo.Name.StartsWith("Get"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(3));
                        var yurl = new Yurl
                        {
                            id = "~GET:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "get",
                            url = (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl)
                        };
                        list.Add(yurl);
                    }
                    else if (mInfo.Name.StartsWith("Post"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(4));
                        var yurl = new Yurl
                        {
                            id = "~POST:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "post",
                            url = nextUrl == "" ? preUrl : preUrl + "/" + nextUrl
                        };
                        list.Add(yurl);
                    }
                    else if (mInfo.Name.StartsWith("Put"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(3));
                        var yurl = new Yurl
                        {
                            id = "~PUT:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "put",
                            url = nextUrl == "" ? preUrl : preUrl + "/" + nextUrl
                        };
                        list.Add(yurl);
                    }
                    else if (mInfo.Name.StartsWith("Delete"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(6));
                        var yurl = new Yurl
                        {
                            id = "~DEL:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "delete",
                            url = nextUrl == "" ? preUrl : preUrl + "/" + nextUrl
                        };
                        list.Add(yurl);
                    }
                }
            }
        });
        return list;
    }
}