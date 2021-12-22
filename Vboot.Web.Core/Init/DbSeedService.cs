using System;
using System.Linq;
using System.Reflection;
using Furion.DependencyInjection;
using Microsoft.CodeAnalysis.Text;
using Serilog;
using SqlSugar;
using Vboot.Core.Module.Pub;
using Vboot.Core.Module.Sys;

namespace Vboot.Web.Core.Init
{
    public class DbSeedService : IScoped
    {
        private readonly ISqlSugarRepository<SysOrg> repo;
        
        private readonly PubOrgInitService pubOrgInitService;
        
        private readonly PubAuthInitService pubAuthInitService;
        

        public DbSeedService(ISqlSugarRepository<SysOrg> repo
            , PubOrgInitService pubOrgInitService
            , PubAuthInitService pubAuthInitService)
        {
            this.repo = repo;
            this.pubOrgInitService = pubOrgInitService;
            this.pubAuthInitService = pubAuthInitService;
        }


        public async void Init()
        {
            
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var coreAssemblies = System.IO.Directory.GetFiles(path, "Vboot.Core.dll").Select(Assembly.LoadFrom)
                .ToArray();

            var coreModelTypes = coreAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x.IsClass && x.Namespace != null && x.Namespace.StartsWith("Vboot")).ToList();
            coreModelTypes.ForEach(t =>
            {
                var customAttributeDatas = t.CustomAttributes;
                foreach (var attribute in customAttributeDatas)
                {
                    if (attribute.ToString().Contains("SugarTable"))
                    {
                        repo.Context.CodeFirst.InitTables(t);
                        Console.WriteLine(attribute);
                    }
                }
            });
            
            var appAssemblies = System.IO.Directory.GetFiles(path, "Vboot.Application.dll").Select(Assembly.LoadFrom)
                .ToArray();

            var appModelTypes = appAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x.IsClass && x.Namespace != null && x.Namespace.StartsWith("Vboot")).ToList();
            appModelTypes.ForEach(t =>
            {
                var customAttributeDatas = t.CustomAttributes;
                foreach (var attribute in customAttributeDatas)
                {
                    if (attribute.ToString().Contains("SugarTable"))
                    {
                        repo.Context.CodeFirst.InitTables(t);
                        Console.WriteLine(attribute);
                    }
                }
            });
            
            var sysOrg= repo.Single(it => it.id == "sa");
            if (sysOrg == null)
            {
                await pubOrgInitService.InitAllDept();
                await pubOrgInitService.InitAllUser();
                await pubOrgInitService.InitZsf();
                await pubOrgInitService.InitSa();
                await pubAuthInitService.InitAllMenu();
            }
            

            // repo.Context.CodeFirst.InitTables(typeof(SysOrg));
            // repo.Context.CodeFirst.InitTables(typeof(SysOrgUser));
            // repo.Context.CodeFirst.InitTables(typeof(SysOrgDept));
            // repo.Context.CodeFirst.InitTables(typeof(SysOrgPost));
            // repo.Context.CodeFirst.InitTables(typeof(SysOrgPostUser));
            // repo.Context.CodeFirst.InitTables(typeof(SysOrgGroup));
            // repo.Context.CodeFirst.InitTables(typeof(SysOrgGroupOrg));
            // repo.Context.CodeFirst.InitTables(typeof(SysAuthMenu));
            // repo.Context.CodeFirst.InitTables(typeof(SysAuthRole));
            // repo.Context.CodeFirst.InitTables(typeof(SysAuthRoleMenu));
            // repo.Context.CodeFirst.InitTables(typeof(SysAuthRoleOrg));
            // repo.Context.CodeFirst.InitTables(typeof(SysLogAudit));
            // repo.Context.CodeFirst.InitTables(typeof(SysLogEx));
            // repo.Context.CodeFirst.InitTables(typeof(SysLogVisit));
            // repo.Context.CodeFirst.InitTables(typeof(SysLogOp));
            // repo.Context.CodeFirst.InitTables(typeof(AssDictCate));
            // repo.Context.CodeFirst.InitTables(typeof(AssDictMain));
            // repo.Context.CodeFirst.InitTables(typeof(AssDictData));
        }
    }
}