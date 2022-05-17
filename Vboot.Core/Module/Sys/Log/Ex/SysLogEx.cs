using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

/// <summary>
/// 异常日志
/// </summary>
[SugarTable("sys_log_ex")]
[Description("异常日志")]
public class SysLogEx
{
    /// <summary>
    /// 主键Id
    /// </summary>
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true)]
    public string Id { get; set; }

    /// <summary>
    /// 操作人
    /// </summary>
    [MaxLength(20)]
    [SugarColumn(ColumnDescription = "操作人", IsNullable = true)]
    public string Account { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(100)]
    [SugarColumn(ColumnDescription = "名称", IsNullable = true)]
    public string Name { get; set; }

    /// <summary>
    /// 类名
    /// </summary>
    [MaxLength(100)]
    [SugarColumn(ColumnDescription = "类名", IsNullable = true)]
    public string ClassName { get; set; }

    /// <summary>
    /// 方法名
    /// </summary>
    [MaxLength(100)]
    [SugarColumn(ColumnDescription = "方法名", IsNullable = true)]
    public string MethodName { get; set; }

    /// <summary>
    /// 异常名称
    /// </summary>
    [SugarColumn(ColumnDescription = "异常名称", IsNullable = true)]
    public string ExceptionName { get; set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    [SugarColumn(ColumnDescription = "异常信息", IsNullable = true)]
    public string ExceptionMsg { get; set; }

    /// <summary>
    /// 异常源
    /// </summary>
    [SugarColumn(ColumnDescription = "异常源", IsNullable = true)]
    public string ExceptionSource { get; set; }

    /// <summary>
    /// 堆栈信息
    /// </summary>
    [SugarColumn(ColumnDescription = "堆栈信息", IsNullable = true, Length = 2000)]
    public string StackTrace { get; set; }

    /// <summary>
    /// 参数对象
    /// </summary>
    [SugarColumn(ColumnDescription = "参数对象", IsNullable = true)]
    public string ParamsObj { get; set; }

    /// <summary>
    /// 异常时间
    /// </summary>
    [SugarColumn(ColumnDescription = "异常时间", IsNullable = true)]
    public DateTime ExceptionTime { get; set; }
}