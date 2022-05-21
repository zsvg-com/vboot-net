using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Bpm;

namespace Vboot.Core.Modulex.Oa;

[SugarTable("oa_flow_main", TableDescription = "OA流程实例表")]
[Description("OA流程实例表")]
public class OaFlowMain : BaseMainEntity
{

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 128)]
    public string notes { get; set; }


    [SugarColumn(ColumnDescription = "全局流程模板ID", IsNullable = true, Length = 32)]
    public string protd { get; set; }
    
    [SugarColumn(ColumnDescription = "全局流程模板ID", IsNullable = true, Length = 32)]
    public string temid { get; set; }
    
    [SugarColumn(IsIgnore = true)] 
    public Zbpm zbpm { get; set; }
    
    [SugarColumn(ColumnDescription = "状态", IsNullable = true, Length = 8)]
    public string state { get; set; }
    
    
    

}