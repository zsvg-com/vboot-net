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
    private readonly ISqlSugarRepository<SysAuthPerm> repo;

    public ApiGatherService(ISqlSugarRepository<SysAuthPerm> repo)
    {
        this.repo = repo;
    }


    public void Init()
    {
        List<Yurl> codeUrlList = GetScanList();

        List<string> dbUrlList = repo.Context.Queryable<SysAuthPerm>()
            // .Where(t => t.type == "get")
            .Select(t => t.id)
            .ToList();

        //收集最大权限位及最大权限位的最大权限码
        string sql = @"select max(u.pos) as pos,max(u.code) as code from sys_auth_perm u 
where u.pos = (select max(uu.pos) from sys_auth_perm uu)";
        List<dynamic> maxList = repo.Context.Ado.SqlQuery<dynamic>(sql);
        int pos = 0;
        long code = 0;
        if (maxList != null && maxList.Count > 0 && maxList[0].pos != null)
        {
            pos = maxList[0].pos;
            code = maxList[0].code;
        }

        //比较两个list得到insertList
        List<SysAuthPerm> insertList = new List<SysAuthPerm>();
        List<SysAuthPerm> updateList = new List<SysAuthPerm>();
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
                    var perm = new SysAuthPerm();
                    perm.id = codeUrl.id;
                    perm.name = codeUrl.name;
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

                    var perm = new SysAuthPerm();
                    perm.id = codeUrl.id;
                    perm.name = codeUrl.name;
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
                var perm = new SysAuthPerm();
                perm.id = codeUrl.id;
                perm.name = codeUrl.name;
                perm.pid = codeUrl.pid;
                perm.type = codeUrl.type;
                perm.avtag = true;
                updateList.Add(perm);
            }
        }

        repo.Context.Updateable<SysAuthPerm>().SetColumns(it => it.avtag == false).Where(it => it.avtag == true)
            .ExecuteCommand();
        if (updateList.Count > 0)
        {
            repo.UpdateAsync(updateList);
        }

        if (insertList.Count > 0)
        {
            repo.InsertAsync(insertList);
        }

        string getsSql = "SELECT id as url,pos,code from sys_auth_perm where avtag= 1 and code<>0 and type='get'";
        List<Yperm> cacheGets = repo.Ado.SqlQuery<Yperm>(getsSql);

        string postsSql = "SELECT id as url,pos,code from sys_auth_perm where avtag= 1 and code<>0 and type='post'";
        List<Yperm> cachePosts = repo.Ado.SqlQuery<Yperm>(postsSql);

        string putsSql = "SELECT id as url,pos,code from sys_auth_perm where avtag= 1 and code<>0 and type='put'";
        List<Yperm> cachePuts = repo.Ado.SqlQuery<Yperm>(putsSql);

        string deletesSql = "SELECT id as url,pos,code from sys_auth_perm where avtag= 1 and code<>0 and type='delete'";
        List<Yperm> cacheDeletes = repo.Ado.SqlQuery<Yperm>(deletesSql);

        SysAuthPermCache.GET_URLS = cacheGets.ToArray();
        SysAuthPermCache.POST_URLS = cachePosts.ToArray();
        SysAuthPermCache.PUT_URLS = cachePuts.ToArray();
        SysAuthPermCache.DELETE_URLS = cacheDeletes.ToArray();
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
                name = preUrl,
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
                            type = "get"
                        };
                        yurl.name = yurl.id;
                        list.Add(yurl);
                    }
                    else if (mInfo.Name.StartsWith("Post"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(4));
                        var yurl = new Yurl
                        {
                            id = "~POST:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "post"
                        };
                        yurl.name = yurl.id;
                        list.Add(yurl);
                    }
                    else if (mInfo.Name.StartsWith("Put"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(3));
                        var yurl = new Yurl
                        {
                            id = "~PUT:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "put"
                        };
                        yurl.name = yurl.id;
                        list.Add(yurl);
                    }
                    else if (mInfo.Name.StartsWith("Delete"))
                    {
                        var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(6));
                        var yurl = new Yurl
                        {
                            id = "~DEL:" + (nextUrl == "" ? preUrl : preUrl + "/" + nextUrl),
                            pid = preUrl,
                            type = "delete"
                        };
                        yurl.name = yurl.id;
                        list.Add(yurl);
                    }
                }
            }
        });
        return list;
    }
}