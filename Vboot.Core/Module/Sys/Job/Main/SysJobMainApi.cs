using Furion;


using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.RemoteRequest.Extensions;
using Furion.TaskScheduler;
using Mapster;
using Microsoft.AspNetCore.Mvc;

using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    /// <summary>
    /// 任务调度服务
    /// </summary>
    public class SysJobMainApi : IDynamicApiController, IScoped
    {
        private readonly ISqlSugarRepository<SysJobMain> _sysTimerRep;  // 任务表仓储 
        private readonly SysCacheService _cache;
        private readonly SysJobMainService _service;
        
        

        public SysJobMainApi(
            ISqlSugarRepository<SysJobMain> sysTimerRep, 
            SysCacheService cache,
            SysJobMainService sysJobMainService)
        {
            _sysTimerRep = sysTimerRep;
            _cache = cache;
            _service = sysJobMainService;
        }
        
        
        [QueryParameters]
        public async Task<dynamic> Get(int page, int pageSize)
        {
            RefAsync<int> total = 0;
            var items = await _service.repo.Context.Queryable<SysJobMain>()
                .Select((t) => new {t.id, t.name, t.crtim, t.uptim})
                .ToPageListAsync(page, pageSize, total);
            return RestPageResult.Build(total.Value, items);
        }
        

        /// <summary>
        /// 分页获取任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // [HttpGet("/sysTimers/page")]
        // public async Task<dynamic> GetTimerPageList([FromQuery] JobInput input)
        // {
        //     var workers = SpareTime.GetWorkers().ToList();
        //
        //     var timers = await _sysTimerRep.Context.Queryable<SysJobMain>()
        //                          .WhereIF(!string.IsNullOrWhiteSpace(input.JobName), u => u.JobName.Contains(input.JobName.Trim()))
        //                          .Select<JobOutput>()
        //                          .ToPagedListAsync(input.PageNo, input.PageSize);
        //
        //     timers.Items.ToList().ForEach(u =>
        //     {
        //         var timer = workers.FirstOrDefault(m => m.WorkerName == u.JobName);
        //         if (timer != null)
        //         {
        //             u.TimerStatus = timer.Status;
        //             u.RunNumber = timer.Tally;
        //             u.Exception = ""; // JSON.Serialize(timer.Exception);
        //         }
        //     });
        //     return XnPageResult<JobOutput>.SqlSugarPageResult(timers);
        // }

        /// <summary>
        /// 获取所有本地任务
        /// </summary>
        /// <returns></returns>
        [HttpGet("/sysTimers/localJobList")]
        public async Task<dynamic> GetLocalJobList()
        {
            // 获取本地所有任务方法
            var LocalJobs = await GetTaskMethods();

            // TaskMethodInfo继承自LocalJobOutput，直接强转为LocalJobOutput再返回
            return LocalJobs.Select(t => (LocalJobOutput)t);
        }

        
        /// <summary>
        /// 增加任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Post(JobInput input)
        {
            var isExist = await _sysTimerRep.AnyAsync(u => u.name == input.name);
            if (isExist)
                throw Oops.Oh(ErrorCode.D1100);

            var timer = input.Adapt<SysJobMain>();
            await _service.InsertAsync(timer);

            // 添加到任务调度里
            AddTimerJob(input);
        }

        
        // /// <summary>
        // /// 增加任务
        // /// </summary>
        // /// <param name="input"></param>
        // /// <returns></returns>
        // [HttpPost("/sysTimers/add")]
        // public async Task AddTimer(JobInput input)
        // {
        //     var isExist = await _sysTimerRep.AnyAsync(u => u.JobName == input.JobName);
        //     if (isExist)
        //         throw Oops.Oh(ErrorCode.D1100);
        //
        //     var timer = input.Adapt<SysJobMain>();
        //     await _service.repo.Context.Insertable(timer).CallEntityMethod(m => m.crtim).ExecuteCommandAsync();
        //
        //     // 添加到任务调度里
        //     AddTimerJob(input);
        // }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // [HttpPost("/sysTimers/delete")]
        // public async Task DeleteTimer(DeleteJobInput input)
        // {
        //     var timer = await _sysTimerRep.FirstOrDefaultAsync(u => u.Id == input.Id);
        //     if (timer == null)
        //         throw Oops.Oh(ErrorCode.D1101);
        //
        //     await _sysTimerRep.DeleteAsync(timer);
        //
        //     // 从调度器里取消
        //     SpareTime.Cancel(timer.JobName);
        // }
        
        public async Task DeleteOne(string id)
        {
            SysJobMain job= await _service.SingleAsync(id);
            await _service.DeleteAsync(new[]{id});
            SpareTime.Cancel(job.name);
            
        }
        
        // public async Task Delete(string input)
        // {
        //     var timer = await _sysTimerRep.FirstOrDefaultAsync(u => u.Id == input.Id);
        //     if (timer == null)
        //         throw Oops.Oh(ErrorCode.D1101);
        //
        //     await _sysTimerRep.DeleteAsync(timer);
        //
        //     // 从调度器里取消
        //     SpareTime.Cancel(timer.JobName);
        // }
        //
        
        public async Task Put(UpdateJobInput input)
        {

            // 先从调度器里取消
            var oldTimer = await _sysTimerRep.FirstOrDefaultAsync(u => u.id == input.id);
            // SpareTime.Cancel(oldTimer.name);

            var timer = input.Adapt<SysJobMain>();
            await _service.UpdateAsync(timer);

            // 再添加到任务调度里
            AddTimerJob(input);
        }

        

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        // public async Task Put(UpdateJobInput input)
        // {
        //     // 排除自己并且判断与其他是否相同
        //     var isExist = await _sysTimerRep.AnyAsync(u => u.JobName == input.JobName && u.Id != input.Id);
        //     if (isExist) throw Oops.Oh(ErrorCode.D1100);
        //
        //     // 先从调度器里取消
        //     var oldTimer = await _sysTimerRep.FirstOrDefaultAsync(u => u.Id == input.Id);
        //     SpareTime.Cancel(oldTimer.JobName);
        //
        //     var timer = input.Adapt<SysTimer>();
        //     await _sysTimerRep.Context.Updateable(timer).IgnoreColumns(ignoreAllNullColumns: true).CallEntityMethod(m=>m.Modify()).ExecuteCommandAsync();
        //
        //     // 再添加到任务调度里
        //     AddTimerJob(input);
        // }

        /// <summary>
        /// 查看任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> GetOne(string id)
        {
            return await _service.SingleAsync(id);
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTimers/stop")]
        public async Task StopTimerJob(JobInput input)
        {
            var timer = _sysTimerRep.FirstOrDefault(m => m.name == input.name);
            if (timer == null)
                throw Oops.Oh(ErrorCode.D1002);
            timer.StartNow = false;
            await _service.UpdateAsync(timer);
            SpareTime.Stop(input.name);
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("/sysTimers/start")]
        public async Task StartTimerJob(JobInput input)
        {
            var dbTimer = _sysTimerRep.FirstOrDefault(m => m.name == input.name);
            if (dbTimer == null)
                throw Oops.Oh(ErrorCode.D1002);
            dbTimer.StartNow = true;
            // _sysTimerRep.Context.Updateable(dbTimer).CallEntityMethod(m => m.Modify()).ExecuteCommand();
            await _service.UpdateAsync(dbTimer);
            var timer = SpareTime.GetWorkers().ToList().Find(u => u.WorkerName == input.name);
            if (timer == null) 
                AddTimerJob(input);

         
            // 如果 StartNow 为 flase , 执行 AddTimerJob 并不会启动任务
            SpareTime.Start(input.name);
        }

        /// <summary>
        /// 新增定时任务
        /// </summary>
        /// <param name="input"></param>
        [NonAction]
        public void AddTimerJob(JobInput input)
        {
            Action<SpareTimer, long> action = null;

            switch (input.retyp)
            {
                // 创建本地方法委托
                case RequestTypeEnum.Run:
                    {
                        // 查询符合条件的任务方法
                        var taskMethod = GetTaskMethods().Result.FirstOrDefault(m => m.RequestUrl == input.reurl);
                        if (taskMethod == null) break;

                        // 创建任务对象
                        var typeInstance = Activator.CreateInstance(taskMethod.DeclaringType);

                        // 创建委托
                        action = (Action<SpareTimer, long>)Delegate.CreateDelegate(typeof(Action<SpareTimer, long>), typeInstance, taskMethod.MethodName);
                        break;
                    }
                // 创建网络任务委托
                default:
                    {
                        action = async (_, _) =>
                        {
                            var requestUrl = input.reurl.Trim();
                            requestUrl = requestUrl?.IndexOf("http") == 0 ? requestUrl : "http://" + requestUrl;
                            var requestParameters = input.repar;
                            var headersString = input.rehea;
                            var headers = string.IsNullOrEmpty(headersString)
                                ? null
                                : JSON.Deserialize<Dictionary<string, string>>(headersString);

                            switch (input.retyp)
                            {
                                case RequestTypeEnum.Get:
                                    await requestUrl.SetHeaders(headers).GetAsync();
                                    break;
                                case RequestTypeEnum.Post:
                                    await requestUrl.SetHeaders(headers).SetQueries(requestParameters).PostAsync();
                                    break;
                                case RequestTypeEnum.Put:
                                    await requestUrl.SetHeaders(headers).SetQueries(requestParameters).PutAsync();
                                    break;
                                case RequestTypeEnum.Delete:
                                    await requestUrl.SetHeaders(headers).DeleteAsync();
                                    break;
                            }
                        };
                        break;
                    }
            }

            if (action == null)
                throw Oops.Oh($"定时任务委托创建失败！JobName:{input.name}");

            // 缓存任务配置参数，以供任务运行时读取
            if (input.retyp == RequestTypeEnum.Run)
            {
                var jobParametersName = $"{input.name}_Parameters";
                var jobParameters = _cache.Exists(jobParametersName);
                var requestParametersIsNull = string.IsNullOrEmpty(input.repar);

                // 如果没有任务配置却又存在缓存，则删除缓存
                if (requestParametersIsNull && jobParameters)
                    _cache.Del(jobParametersName);
                else if (!requestParametersIsNull)
                    _cache.Set(jobParametersName, JSON.Deserialize<Dictionary<string, string>>(input.repar));
            }

            // 创建定时任务
            SpareTime.Do(input.cron, action, input.name, input.notes, true, executeType: input.extyp);
        }

        /// <summary>
        /// 启动自启动任务
        /// </summary>
        [NonAction]
        public void StartTimerJob()
        {
            var sysTimerList = _sysTimerRep.Where(t => t.StartNow).Select<JobInput>().ToList();
            sysTimerList.ForEach(AddTimerJob);
        }

        /// <summary>
        /// 获取所有本地任务
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<IEnumerable<TaskMethodInfo>> GetTaskMethods()
        {
            // 有缓存就返回缓存
            var taskMethods = await _cache.GetAsync<IEnumerable<TaskMethodInfo>>("TaskMethodInfos");
            if (taskMethods != null) return taskMethods;

            // 获取所有本地任务方法，必须有spareTimeAttribute特性
            taskMethods = App.EffectiveTypes
                .Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(ISpareTimeWorker).IsAssignableFrom(u))
                .SelectMany(u => u.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.IsDefined(typeof(SpareTimeAttribute), false) &&
                       m.GetParameters().Length == 2 &&
                       m.GetParameters()[0].ParameterType == typeof(SpareTimer) &&
                       m.GetParameters()[1].ParameterType == typeof(long) && m.ReturnType == typeof(void))
                .Select(m =>
                {
                    // 默认获取第一条任务特性
                    var spareTimeAttribute = m.GetCustomAttribute<SpareTimeAttribute>();
                    return new TaskMethodInfo
                    {
                        JobName = spareTimeAttribute.WorkerName,
                        RequestUrl = $"{m.DeclaringType.Name}/{m.Name}",
                        Cron = spareTimeAttribute.CronExpression,
                        DoOnce = spareTimeAttribute.DoOnce,
                        ExecuteType = spareTimeAttribute.ExecuteType,
                        Interval = (int)spareTimeAttribute.Interval / 1000,
                        StartNow = spareTimeAttribute.StartNow,
                        RequestType = RequestTypeEnum.Run,
                        Remark = spareTimeAttribute.Description,
                        TimerType = string.IsNullOrEmpty(spareTimeAttribute.CronExpression) ? SpareTimeTypes.Interval : SpareTimeTypes.Cron,
                        MethodName = m.Name,
                        DeclaringType = m.DeclaringType
                    };
                }));

            await _cache.SetAsync("TaskMethodInfos", taskMethods);
            return taskMethods;
        }
    }
}
