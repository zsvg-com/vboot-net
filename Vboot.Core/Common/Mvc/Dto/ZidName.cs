using SqlSugar;

namespace Vboot.Core.Common
{
    public class ZidName
    {
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true)]
        public string id {get; set;}

        public string name{get; set;}
    }
}