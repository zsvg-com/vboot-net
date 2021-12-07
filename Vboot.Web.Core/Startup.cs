using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Vboot.Core.Common;
using Vboot.Core.Module.Sys;
using Vboot.Web.Core.Init;

namespace Vboot.Web.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJwt<JwtHandler>(enableGlobalAuthorize:true);

            services.AddCorsAccessor();

            services.AddControllers().AddMvcFilter<RequestActionFilter>()
                    .AddInjectWithUnifyResult<RestResultProvider>()
                    .AddJsonOptions(options =>
                    {
                        //options.JsonSerializerOptions.DefaultBufferSize = 10_0000;//返回较大数据数据序列化时会截断，原因：默认缓冲区大小（以字节为单位）为16384。
                        options.JsonSerializerOptions.Converters.AddDateFormatString("yyyy-MM-dd HH:mm:ss");
                        options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // 忽略循环引用 仅.NET 6支持
                    });
            
            // App.GetService<ISysTimerService>().StartTimerJob();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();    // 请求日志必须在 UseStaticFiles 和 UseRouting 之间
            app.UseRouting();

            app.UseCorsAccessor();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseInject(string.Empty);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            App.GetService<DbSeedService>().Init();
            App.GetService<SysJobMainService>().StartAllJob();
        }
    }
}