using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Furion;
using Furion.DependencyInjection;
using Furion.EventBus;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Common
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
    {
        private readonly IEventPublisher _eventPublisher;

        public LogExceptionHandler(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            Console.WriteLine("出错了");
            var userContext = App.User;
            _eventPublisher.PublishAsync(new ChannelEventSource("Create:ExLog",
                new SysLogEx
                {
                    Id=YitIdHelper.NextId()+"",
                    Account = userContext?.FindFirstValue(ClaimConst.CLAINM_ACCOUNT),
                    Name = userContext?.FindFirstValue(ClaimConst.CLAINM_NAME),
                    ClassName = context.Exception.TargetSite.DeclaringType?.FullName,
                    MethodName = context.Exception.TargetSite.Name,
                    ExceptionName = context.Exception.Message,
                    ExceptionMsg = context.Exception.Message,
                    ExceptionSource = context.Exception.Source,
                    StackTrace = context.Exception.StackTrace,
                    ParamsObj = context.Exception.TargetSite.GetParameters().ToString(),
                    ExceptionTime = DateTime.Now
                }));
            Log.Error(context.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}