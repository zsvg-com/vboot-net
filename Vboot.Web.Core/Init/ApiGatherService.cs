using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Furion.DependencyInjection;
using Microsoft.CodeAnalysis.Text;
using Serilog;
using SqlSugar;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Pub;
using Vboot.Core.Module.Sys;

namespace Vboot.Web.Core.Init
{
    public class ApiGatherService : IScoped
    {
        private readonly ISqlSugarRepository<SysAuthPerm> repo;


        public ApiGatherService(ISqlSugarRepository<SysAuthPerm> repo)
        {
            this.repo = repo;
        }


        public void Init()
        {
            //获取数据库中所有的权限标识
            List<SysAuthPerm> dbGets=repo.Context.Queryable<SysAuthPerm>()
                .Where(t => t.type == "get")
                .ToList();
            
            
            List<SysAuthPerm> codeGets = new List<SysAuthPerm>();
            List<SysAuthPerm> codePosts = new List<SysAuthPerm>();
            List<SysAuthPerm> codePuts = new List<SysAuthPerm>();
            List<SysAuthPerm> codeDeletes = new List<SysAuthPerm>();
            //通过反射获取最新代码里所有的权限标识
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
                MethodInfo[] methodInfo = t.GetMethods();
                foreach (MethodInfo mInfo in methodInfo)
                {
                    if ("GetType" != mInfo.Name && "ToString" != mInfo.Name && "Equals" != mInfo.Name &&
                        "GetHashCode" != mInfo.Name)
                    {
                        if (mInfo.Name.StartsWith("Get"))
                        {
                            var nextUrl = XstrUtil.RenameUrlCase(mInfo.Name.Substring(3));
                            SysAuthPerm yperm = new SysAuthPerm();
                            yperm.id = nextUrl == "" ? preUrl : preUrl + "/" + nextUrl;
                            codeGets.Add(yperm);
                        }
                    }
                }
                // var customAttributeDatas = t.CustomAttributes;
                // foreach (var attribute in customAttributeDatas)
                // {
                //     if (attribute.ToString().Contains("SugarTable"))
                //     {
                //         repo.Context.CodeFirst.InitTables(t);
                //         Console.WriteLine(attribute);
                //     }
                // }
            });
            
            
            List<Yperm> cacheGets = new List<Yperm>();
            List<Yperm> cachePosts = new List<Yperm>();
            List<Yperm> cachePuts = new List<Yperm>();
            List<Yperm> cacheeletes = new List<Yperm>();


            JwtHandler.GET_URLS = cacheGets.ToArray();
        }
    }
}