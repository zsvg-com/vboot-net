using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    [SugarTable("sys_log_op")]
    [Description("操作日志表")]
    public class SysLogOp 
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true)] 
        public string Id { get; set; }

        
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(100)]
        [SugarColumn(ColumnDescription = "名称", IsNullable = true)]
        public string Name { get; set; }

        /// <summary>
        /// 是否执行成功（Y-是，N-否）
        /// </summary>
        [SugarColumn(ColumnDescription = "是否执行成功", IsNullable = true)]
        public YesOrNot Success { get; set; }

        /// <summary>
        /// 具体消息
        /// </summary>
        [SugarColumn(ColumnDescription = "具体消息", IsNullable = true)]
        public string Message { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [MaxLength(20)]
        [SugarColumn(ColumnDescription = "Ip", IsNullable = true)]
        public string Ip { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(500)]
        [SugarColumn(ColumnDescription = "地址", IsNullable = true)]
        public string Location { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        [MaxLength(100)]
        [SugarColumn(ColumnDescription = "浏览器", IsNullable = true)]
        public string Browser { get; set; }

        /// <summary>
        /// 操作系统
        /// </summary>
        [MaxLength(100)]
        [SugarColumn(ColumnDescription = "操作系统", IsNullable = true)]
        public string Os { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [MaxLength(100)]
        [SugarColumn(ColumnDescription = "请求地址", IsNullable = true)]
        public string Url { get; set; }

        /// <summary>
        /// 类名称
        /// </summary>
        [MaxLength(100)]
        [SugarColumn(ColumnDescription = "类名称", IsNullable = true)]
        public string ClassName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [SugarColumn(ColumnDescription = "方法名称", IsNullable = true)]
        [MaxLength(100)]
        public string MethodName { get; set; }

        /// <summary>
        /// 请求方式（GET POST PUT DELETE)
        /// </summary>
        [MaxLength(10)]
        [SugarColumn(ColumnDescription = "请求方式", IsNullable = true)]
        public string ReqMethod { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [SugarColumn(ColumnDescription = "请求参数", IsNullable = true)]
        public string Param { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        [SugarColumn(ColumnDescription = "返回结果", IsNullable = true)]
        public string Result { get; set; }

        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        [SugarColumn(ColumnDescription = "耗时（毫秒）", IsNullable = true)]
        public long ElapsedTime { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [SugarColumn(ColumnDescription = "耗操作时间", IsNullable = true)]
        public DateTime OpTime { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [MaxLength(20)]
        [SugarColumn(ColumnDescription = "操作人", IsNullable = true)]
        public string Account { get; set; }
    }
}