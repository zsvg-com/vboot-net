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
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class SysJobMainService : BaseMainService<SysJobMain>, ITransient
{
    private readonly SysCacheService _cache;

    public SysJobMainService(ISqlSugarRepository<SysJobMain> repo,
        SysCacheService cache)
    {
        this.repo = repo;
        _cache = cache;
    }

    //新增定时任务
    public bool AddTimerJob(SysJobMain job, bool doOnce)
    {
        Action<SpareTimer, long> action = null;

        switch (job.retyp)
        {
            // 创建本地方法委托
            case RequestTypeEnum.Run:
            {
                // 查询符合条件的任务方法
                var taskMethod = GetTaskMethods().Result.FirstOrDefault(m => m.RequestUrl == job.reurl);
                if (taskMethod == null) break;
                // 创建任务对象
                var typeInstance = Activator.CreateInstance(taskMethod.DeclaringType);
                action = (Action<SpareTimer, long>) Delegate.CreateDelegate(typeof(Action<SpareTimer, long>),
                    typeInstance, taskMethod.MethodName);
                break;
            }
            // 创建网络任务委托
            default:
            {
                action = async (_, _) =>
                {
                    var requestUrl = job.reurl.Trim();
                    requestUrl = requestUrl?.IndexOf("http") == 0 ? requestUrl : "http://" + requestUrl;
                    var requestParameters = job.repar;
                    var headersString = job.rehea;
                    var headers = string.IsNullOrEmpty(headersString)
                        ? null
                        : JSON.Deserialize<Dictionary<string, string>>(headersString);

                    switch (job.retyp)
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
        {
            return false;
        }

        // 缓存任务配置参数，以供任务运行时读取
        if (job.retyp == RequestTypeEnum.Run)
        {
            var jobParametersCode = $"{job.code}_Parameters";
            var jobParameters = _cache.Exists(jobParametersCode);
            var requestParametersIsNull = string.IsNullOrEmpty(job.repar);

            // 如果没有任务配置却又存在缓存，则删除缓存
            if (requestParametersIsNull && jobParameters)
                _cache.Del(jobParametersCode);
            else if (!requestParametersIsNull)
                _cache.Set(jobParametersCode, JSON.Deserialize<Dictionary<string, string>>(job.repar));
        }

        // 创建定时任务
        if (doOnce)
        {
            SpareTime.DoOnce(1000, action, job.code, job.notes, true, executeType: job.extyp);
        }
        else
        {
            SpareTime.Do(job.cron, action, job.code, job.notes, job.avtag, executeType: job.extyp);
        }

        return true;
    }

    //获取所有本地任务
    public async Task<IEnumerable<TaskMethodInfo>> GetTaskMethods()
    {
        // 有缓存就返回缓存
        var taskMethods = await _cache.GetAsync<IEnumerable<TaskMethodInfo>>("TaskMethodInfos");
        if (taskMethods != null) return taskMethods;

        // 获取所有本地任务方法，必须有spareTimeAttribute特性
        taskMethods = App.EffectiveTypes
            .Where(u => u.IsClass && !u.IsInterface && !u.IsAbstract &&
                        typeof(ISpareTimeWorker).IsAssignableFrom(u))
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
                        Interval = (int) spareTimeAttribute.Interval / 1000,
                        StartNow = spareTimeAttribute.StartNow,
                        RequestType = RequestTypeEnum.Run,
                        Remark = spareTimeAttribute.Description,
                        TimerType = string.IsNullOrEmpty(spareTimeAttribute.CronExpression)
                            ? SpareTimeTypes.Interval
                            : SpareTimeTypes.Cron,
                        MethodName = m.Name,
                        DeclaringType = m.DeclaringType
                    };
                }));

        await _cache.SetAsync("TaskMethodInfos", taskMethods);
        return taskMethods;
    }


    //启动自启动任务
    public void StartAllJob()
    {
        var jobMethodList = GetTaskMethods().Result;
        foreach (var jobMethod in jobMethodList)
        {
            if (!repo.Any(t => t.code == jobMethod.JobName))
            {
                SysJobMain job = new SysJobMain();
                if (jobMethod.Remark.Contains("###"))
                {
                    job.name = jobMethod.Remark.Split("###")[0];
                }
                else
                {
                    job.name = jobMethod.JobName;
                }

                job.code = jobMethod.JobName;
                job.avtag = false;
                job.cron = jobMethod.Cron;
                ;
                job.notes = jobMethod.Remark;
                job.reurl = jobMethod.RequestUrl;
                job.retyp = RequestTypeEnum.Run;
                job.crtim = DateTime.Now;
                job.id = YitIdHelper.NextId() + "";
                job.extyp = jobMethod.ExecuteType;
                repo.Insert(job);
            }
        }

        var jobList = repo.Where(t => t.avtag).ToList();
        foreach (var job in jobList)
        {
            AddTimerJob(job, false);
        }
    }
}