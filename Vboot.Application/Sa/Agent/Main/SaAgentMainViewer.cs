using SqlSugar;

namespace Vboot.Application.Sa;

[SugarTable("sa_agent_main_viewer", TableDescription = "代理商可查看者")]
public class SaAgentMainViewer
{
    [SugarColumn(ColumnDescription = "代理商ID",IsNullable = true)]
    public string mid{ get; set; }
        
    [SugarColumn(ColumnDescription = "可查看者ID",IsNullable = true)]
    public string oid{ get; set; }
}