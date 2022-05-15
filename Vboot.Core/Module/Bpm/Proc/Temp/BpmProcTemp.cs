using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Bpm;

[SugarTable("bpm_proc_temp", TableDescription = "流程模板表")]
public class BpmProcTemp : BaseMainEntity
{
    [SugarColumn(ColumnDescription = "XML", ColumnDataType = "longtext", IsNullable = true)]
    public string xml { get; set; }
}