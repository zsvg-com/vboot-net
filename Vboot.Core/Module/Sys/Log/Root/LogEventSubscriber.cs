using System;
using System.Threading.Tasks;
using Furion.EventBus;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

public class LogEventSubscriber : IEventSubscriber
{
    public LogEventSubscriber(IServiceProvider services)
    {
        Services = services;
    }

    public IServiceProvider Services { get; }

    [EventSubscribe("reate:OpLog")]
    public async Task CreateOpLog(EventHandlerExecutingContext context)
    {
        using var scope = Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysLogOp>>();
        var log = (SysLogOp) context.Source.Payload;
        await repository.InsertAsync(log);
    }

    [EventSubscribe("Create:ExLog")]
    public async Task CreateExLog(EventHandlerExecutingContext context)
    {
        using var scope = Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysLogEx>>();
        var log = (SysLogEx) context.Source.Payload;
        await repository.InsertAsync(log);
    }

    [EventSubscribe("Create:LoginLog")]
    public async Task CreateVisLog(EventHandlerExecutingContext context)
    {
        using var scope = Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysLogLogin>>();
        var log = (SysLogLogin) context.Source.Payload;
        await repository.InsertAsync(log);
    }

    [EventSubscribe("Update:UserLoginInfo")]
    public async Task UpdateUserLoginInfo(EventHandlerExecutingContext context)
    {
        using var scope = Services.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ISqlSugarRepository<SysOrgUser>>();
        var log = (SysOrgUser) context.Source.Payload;
        await repository.Context.Updateable(log).UpdateColumns(m => new {m.lalot, m.laloi})
            .ExecuteCommandAsync();
    }
}