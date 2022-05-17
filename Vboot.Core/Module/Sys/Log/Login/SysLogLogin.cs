using SqlSugar;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

/// <summary>
/// 登录日志表
/// </summary>
[SugarTable("sys_log_login")]
[Description("登录日志表")]
public class SysLogLogin : BaseEntity
{
    // /// <summary>
    // /// 主键ID
    // /// </summary>
    // [SugarColumn(ColumnDescription = "ID主键", IsPrimaryKey = true)]
    // public string id { get; set; }
    //
    // /// <summary>
    // /// 用户姓名
    // /// </summary>
    // [MaxLength(100)]
    // [SugarColumn(ColumnDescription = "用户姓名", IsNullable = true, Length = 32)]
    // public string name { get; set; }

    /// <summary>
    /// 用户账号
    /// </summary>
    [MaxLength(36)]
    [SugarColumn(ColumnDescription = "用户账号", IsNullable = true)]
    public string usnam { get; set; }


    /// <summary>
    /// IP地址
    /// </summary>
    [MaxLength(32)]
    public string ip { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    [MaxLength(100)]
    [SugarColumn(ColumnDescription = "登录地点", IsNullable = true)]
    public string addre { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [MaxLength(64)]
    [SugarColumn(ColumnDescription = "操作系统", IsNullable = true)]
    public string ageos { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [MaxLength(64)]
    [SugarColumn(ColumnDescription = "浏览器", IsNullable = true)]
    public string agbro { get; set; }

    /// <summary>
    /// 客户端详情
    /// </summary>
    [MaxLength(512)]
    [SugarColumn(ColumnDescription = "客户端详情", IsNullable = true)]
    public string agdet { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    [SugarColumn(ColumnDescription = "登录时间", IsNullable = true)]
    public DateTime crtim { get; set; }
}