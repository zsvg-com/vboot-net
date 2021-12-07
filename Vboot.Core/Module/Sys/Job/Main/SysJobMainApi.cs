using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.TaskScheduler;
using Mapster;
using SqlSugar;
using System.Linq;
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
        
        private readonly SysJobMainService _service;
        
        public SysJobMainApi(
            ISqlSugarRepository<SysJobMain> sysTimerRep, 
            SysJobMainService sysJobMainService)
        {
            _sysTimerRep = sysTimerRep;
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
        public async Task<dynamic> GetLocalJobs()
        {
            // 获取本地所有任务方法
            var LocalJobs = await _service.GetTaskMethods();

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
            _service.AddTimerJob(input);
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
            _service.AddTimerJob(input);
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
        public async Task Stop(JobInput input)
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
        public async Task Start(JobInput input)
        {
            var dbTimer = _sysTimerRep.FirstOrDefault(m => m.name == input.name);
            if (dbTimer == null)
                throw Oops.Oh(ErrorCode.D1002);
            dbTimer.StartNow = true;
            // _sysTimerRep.Context.Updateable(dbTimer).CallEntityMethod(m => m.Modify()).ExecuteCommand();
            await _service.UpdateAsync(dbTimer);
            var timer = SpareTime.GetWorkers().ToList().Find(u => u.WorkerName == input.name);
            if (timer == null) 
                _service.AddTimerJob(input);

         
            // 如果 StartNow 为 flase , 执行 AddTimerJob 并不会启动任务
            SpareTime.Start(input.name);
        }

   
      
    }
}
