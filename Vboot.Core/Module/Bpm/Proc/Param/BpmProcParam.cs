using System;
using SqlSugar;

namespace Vboot.Core.Module.Bpm;

[SugarTable("bpm_proc_param", TableDescription = "流程参数表")]
public class BpmProcParam
{
    [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
    public string id { get; set; }

    [SugarColumn(ColumnDescription = "xx", IsNullable = true, Length = 64)]
    public string pakey { get; set; }

    [SugarColumn(ColumnDescription = "xx", IsNullable = true, Length = 512)]
    public string paval { get; set; }

    [SugarColumn(ColumnDescription = "xx", IsNullable = true, Length = 32)]
    public string offty { get; set; }

    [SugarColumn(ColumnDescription = "xx", IsNullable = true, Length = 32)]
    public string offid { get; set; }

    [SugarColumn(ColumnDescription = "xx", IsNullable = true, Length = 32)]
    public string proid { get; set; }
}