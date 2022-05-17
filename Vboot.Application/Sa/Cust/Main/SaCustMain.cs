using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Application.Sa;


[SugarTable("sa_cust_main", TableDescription = "客户主信息")]
public class SaCustMain : BaseMainEntity
{
    
    [SugarColumn(ColumnDescription = "客户地址", IsNullable = true, Length = 255)]
    public string addre { get; set; }
}