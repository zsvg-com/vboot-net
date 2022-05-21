using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Bpm;

[SugarTable("bpm_proc_temp", TableDescription = "流程模板表")]
public class BpmProcTemp : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "原始XML", ColumnDataType = "varchar(max)", IsNullable = true)]
    public string orxml { get; set; }
    
    [SugarColumn(ColumnDescription = "变动后的XML", ColumnDataType = "varchar(max)", IsNullable = true)]
    public string chxml { get; set; }
}