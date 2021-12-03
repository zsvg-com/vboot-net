using SqlSugar;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys
{
    /// <summary>
    /// 访问日志表
    /// </summary>
    [SugarTable("sys_log_visit")]
    [Description("访问日志表")]
    public class SysLogVisit
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
        [SugarColumn(ColumnDescription = "名称", IsNullable = true, Length = 32)]
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
        public string Ip { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(100)]
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
        /// 访问类型
        /// </summary>
        [SugarColumn(ColumnDescription = "访问类型", IsNullable = true)]
        public LoginType VisType { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        [SugarColumn(ColumnDescription = "访问时间", IsNullable = true)]
        public DateTime VisTime { get; set; }

        /// <summary>
        /// 访问人
        /// </summary>
        [MaxLength(36)]
        [SugarColumn(ColumnDescription = "访问人", IsNullable = true)]
        public string Account { get; set; }
    }
}