using Furion.TaskScheduler;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    /// <summary>
    /// 定时任务
    /// </summary>
    [SugarTable("sys_job_main")]
    [Description("定时任务")]
    public class SysJobMain : BaseMainEntity
    {
        /// <summary>
        /// 任务code
        /// </summary>
        [SugarColumn(ColumnDescription = "任务代码", IsNullable = true)]
        public string code { get; set; }
        
        [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
        public int ornum { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        [MaxLength(200)]
        [SugarColumn(ColumnDescription = "请求url", IsNullable = true)]
        public string reurl { get; set; }
 
        
        /// <summary>
        /// 请求类型
        /// </summary>
        /// <example>2</example>
        [SugarColumn(ColumnDescription = "请求类型", IsNullable = true)]
        public RequestTypeEnum retyp { get; set; } = RequestTypeEnum.Post;
        
        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        [SugarColumn(ColumnDescription = "Headers", IsNullable = true)]
        public string rehea { get; set; }
        
        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        [SugarColumn(ColumnDescription = "请求参数", IsNullable = true)]
        public string repar { get; set; }
        
        /// <summary>
        /// Cron表达式
        /// </summary>
        /// <example></example>
        [MaxLength(20)]
        [SugarColumn(ColumnDescription = "Cron表达式", IsNullable = true)]
        public string cron { get; set; }

        /// <summary>
        /// 执行类型(并行、列队)
        /// </summary>
        [SugarColumn(ColumnDescription = "执行类型", IsNullable = true)]
        public SpareTimeExecuteTypes extyp { get; set; } = SpareTimeExecuteTypes.Parallel;
        
        
        /// <summary>
        /// 只执行一次
        /// </summary>
        [SugarColumn(ColumnDescription = "只执行一次", IsNullable = true)]
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        [SugarColumn(ColumnDescription = "立即执行", IsNullable = true)]
        public bool StartNow { get; set; } = false;

        


    
       
      
    }
}
