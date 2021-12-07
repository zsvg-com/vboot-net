using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Furion.RemoteRequest.Extensions;
using Furion.TaskScheduler;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    
    public class SysJobMainService : BaseMainService<SysJobMain>, ITransient
    {
        
        private readonly SysCacheService _cache;
        public SysJobMainService(ISqlSugarRepository<SysJobMain> repo,
            SysCacheService cache)
        {
            this.repo = repo;
            _cache = cache;
        }
        
        
        
        /// <summary>
        /// 新增定时任务
        /// </summary>
        /// <param name="input"></param>
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
        public void StartTimerJob()
        {
            var sysTimerList = repo.Where(t => t.StartNow).Select<JobInput>().ToList();
            sysTimerList.ForEach(AddTimerJob);
        }

        
          /// <summary>
        /// 获取所有本地任务
        /// </summary>
        /// <returns></returns>
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