using SqlSugar;

namespace Vboot.Application.Sa;

[SugarTable("sa_cust_main_viewer", TableDescription = "客户可查看者")]
public class SaCustMainViewer
{
    [SugarColumn(ColumnDescription = "客户ID",IsNullable = true)]
    public string mid{ get; set; }
        
    [SugarColumn(ColumnDescription = "可查看者ID",IsNullable = true)]
    public string oid{ get; set; }
}