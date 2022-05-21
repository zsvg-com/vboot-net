using System;
using SqlSugar;

namespace Vboot.Core.Module.Bpm;


[SugarTable("bpm_audit_main", TableDescription = "流程审批记录表")]
public class BpmAuditMain
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "当前节点编号", IsNullable = true, Length = 32)]
    public string facno { get; set; }

    [SugarColumn(ColumnDescription = "当前节点编号", IsNullable = true, Length = 126)]
    public string facna { get; set; }

    [SugarColumn(ColumnDescription = "开始时间", IsNullable = true, IsOnlyIgnoreUpdate = true)]
    public DateTime? crtim { get; set; }=DateTime.Now;
    
    [SugarColumn(ColumnDescription = "流程实例id", IsNullable = true, Length = 32)]
    public string proid { get; set; }
    
    [SugarColumn(ColumnDescription = "节点id", IsNullable = true, Length = 32)]
    public string nodid { get; set; }
    
    [SugarColumn(ColumnDescription = "任务id", IsNullable = true, Length = 32)]
    public string tasid { get; set; }
    
    [SugarColumn(ColumnDescription = "实处理人", IsNullable = true, Length = 32)]
    public string haman { get; set; }
    
    [SugarColumn(ColumnDescription = "操作的key：pass，refuse", IsNullable = true, Length = 32)]
    public string opkey { get; set; }
    
    [SugarColumn(ColumnDescription = "操作的名称: 通过，驳回，转办等", IsNullable = true, Length = 64)]
    public string opinf { get; set; }  
    
    [SugarColumn(ColumnDescription = "审核留言：具体写了什么审核内容", IsNullable = true, Length = 1000)]
    public string opnot { get; set; }
    
}