using System;
using System.Linq;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlSugar;
using Vboot.Core.Module.Sys;

namespace Vboot.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSqlSugar(new ConnectionConfig
            {
                ConnectionString = "Data Source=localhost;Database=vboot22;User ID=root;Password=123456;pooling=true;port=3306;sslmode=none;CharSet=utf8;",//连接符字串
                DbType = DbType.MySql,
                //ConnectionString = "Data Source=localhost/orcl;User ID=vboot;Password=123456;",//连接符字串
                //DbType = DbType.Oracle,
                //ConnectionString = "server=.;uid=sa;pwd=123456;database=vboot",//连接符字串
                //DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            },db =>
            {
                //处理日志事务
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    if (sql.StartsWith("SELECT"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (sql.StartsWith("UPDATE") || sql.StartsWith("INSERT"))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (sql.StartsWith("DELETE"))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.WriteLine(sql);
                    Console.WriteLine(string.Join(",", pars?.Select(it => it.ParameterName + ":" + it.Value)));
                    Console.WriteLine();
                    App.PrintToMiniProfiler("SqlSugar", "Info", sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                };
              
              
            });
            
            
            
            // 注册EventBus服务
            services.AddEventBus(builder =>
            {
                // 注册 Log 日志订阅者
                builder.AddSubscriber<LogEventSubscriber>();
            });
          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
            }
            
        }
    }
}