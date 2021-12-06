using Furion.TaskScheduler;
using System;

namespace Vboot.Core.Module.Sys
{
    /// <summary>
    /// 日志定时任务类
    /// </summary>
    public class LogJobWorker : ISpareTimeWorker
    {

        [SpareTime(10000, "LogExWritingService", Description = "后台批量写错误日志，配置项参数：{\"quantity\": 2}，不填默认为2",
            DoOnce = false, StartNow = true, ExecuteType = SpareTimeExecuteTypes.Serial)]
        public void DoLogEx(SpareTimer timer, long count)
        {
            Console.WriteLine("任务测试" + DateTime.Now);
        }
    }
}