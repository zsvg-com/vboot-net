using System.Collections.Generic;
using System.Text.Json.Serialization;
using SqlSugar;

namespace Vboot.Core.Common
{
    public class Ztree
    {
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true)]
        public string id {get; set;}

        public string name{get; set;}

        [JsonIgnore]
        public string pid{get; set;}

        [SugarColumn(IsIgnore = true)]
        public List<Ztree> children{get; set;}
        

    }
}