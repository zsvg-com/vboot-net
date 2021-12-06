using Furion.DataValidation;
using Furion.TaskScheduler;
using System.ComponentModel.DataAnnotations;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    /// <summary>
    /// 任务调度参数
    /// </summary>
    public class JobInput 
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string code { get; set; }
        
        /// <summary>
        /// 任务名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string cron { get; set; }

        /// <summary>
        /// 请求url
        /// </summary>
        public string reurl { get; set; }

        /// <summary>
        /// 请求参数（Post，Put请求用）
        /// </summary>
        public string repar { get; set; }
        
        /// <summary>
        /// 请求类型
        /// </summary>
        public RequestTypeEnum retyp { get; set; }
        
        /// <summary>
        /// Headers(可以包含如：Authorization授权认证)
        /// 格式：{"Authorization":"userpassword.."}
        /// </summary>
        public string rehea { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string notes { get; set; }
        
        /// <summary>
        /// 执行类型(并行、列队)
        /// </summary>
        public SpareTimeExecuteTypes extyp { get; set; }
        
        
        
        /// <summary>
        /// 只执行一次
        /// </summary>
        public bool DoOnce { get; set; } = false;

        /// <summary>
        /// 立即执行（默认等待启动）
        /// </summary>
        public bool StartNow { get; set; } = false;
    }

    public class DeleteJobInput : JobInput
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        [Required(ErrorMessage = "任务Id不能为空"), DataValidation(ValidationTypes.Numeric)]
        public string id { get; set; }
    }

    public class UpdateJobInput : DeleteJobInput
    {

    }

    public class QueryJobInput : DeleteJobInput
    {

    }
}
