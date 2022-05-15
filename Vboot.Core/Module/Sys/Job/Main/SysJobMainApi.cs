using System;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.TaskScheduler;
using SqlSugar;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys
{
    [ApiDescriptionSettings("Sys",Tag ="定时任务-清单" )]
    public class SysJobMainApi : IDynamicApiController
    {
        private readonly SysJobMainService _service;

        public SysJobMainApi(SysJobMainService sysJobMainService)
        {
            _service = sysJobMainService;
        }


        [QueryParameters]
        public async Task<dynamic> Get(string name)
        {
            var pp= XreqUtil.GetPp(); 
            var items = await _service.repo.Context.Queryable<SysJobMain>()
                .Select((t) => new {t.id, t.name, t.crtim, t.uptim, t.code, t.reurl, t.avtag, t.cron})
                .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
                .ToPageListAsync(pp.page, pp.pageSize, pp.total);
            return RestPageResult.Build(pp.total.Value, items);
        }

        /// <summary>
        /// 查看任务
        /// </summary>
        public async Task<dynamic> GetOne(string id)
        {
            return await _service.SingleAsync(id);
        }

        /// <summary>
        /// 增加任务
        /// </summary>
        public async Task Post(SysJobMain job)
        {
            await _service.InsertAsync(job);
            if (job.avtag)
            {
                bool runOk = _service.AddTimerJob(job, false);
                if (!runOk)
                {
                    throw Oops.Oh($"定时任务委托创建失败！JobCode:{job.code}");
                }
            }
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        public async Task Put(SysJobMain job)
        {
            var dbJob = await _service.SingleAsync(job.id);
            // 先从调度器里取消
            if (dbJob.avtag)
            {
                SpareTime.Cancel(dbJob.code);
            }

            await _service.UpdateAsync(job);
            // 再添加到任务调度里
            if (job.avtag)
            {
                bool runOk = _service.AddTimerJob(job, false);
                if (!runOk)
                {
                    throw Oops.Oh($"定时任务委托创建失败！JobCode:{job.code}");
                }
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            foreach (var id in idArr)
            {
                SysJobMain job = await _service.SingleAsync(id);
                await _service.DeleteAsync(id);
                SpareTime.Cancel(job.code);
            }
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        public async Task Start([FromRoute] string ids)
        {
            var idArr = ids.Split(",");
            foreach (var id in idArr)
            {
                var job = await _service.SingleAsync(id);
                job.avtag = true;
                await _service.UpdateAsync(job);
                var timer = SpareTime.GetWorkers().ToList().Find(u => u.WorkerName == job.code);
                if (timer == null)
                {
                    bool runOk = _service.AddTimerJob(job, false);
                    if (!runOk)
                    {
                        throw Oops.Oh($"定时任务委托创建失败！JobCode:{job.code}");
                    }
                }
                else
                {
                    SpareTime.Start(job.code);
                }
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        public async Task Stop([FromRoute] string ids)
        {
            var idArr = ids.Split(",");
            foreach (var id in idArr)
            {
                var job = await _service.SingleAsync(id);
                job.avtag = false;
                await _service.UpdateAsync(job);
                SpareTime.Stop(job.code);
            }
        }

        /// <summary>
        /// 立即执行一次
        /// </summary>
        public async Task Once([FromRoute] string id)
        {
            var job = await _service.SingleAsync(id);
            if (job == null)
                throw Oops.Oh(ErrorCode.D1002);
            job.code = YitIdHelper.NextId() + "";
            bool runOk = _service.AddTimerJob(job, true);
            if (!runOk)
            {
                throw Oops.Oh($"定时任务委托创建失败！JobCode:{job.code}");
            }
        }
    }
}