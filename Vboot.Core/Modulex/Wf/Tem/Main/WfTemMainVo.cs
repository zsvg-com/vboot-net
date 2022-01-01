using System;
using System.Collections.Generic;
using SqlSugar;

namespace Vboot.Core.Modulex.Wf
{
    public class WfTemMainVo
    {
        [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true, Length = 36)]
        public string id { get; set; }

        public string name { get; set; }

        public string pid { get; set; }

        public List<WfTemMainVo> children { get; set; }

        public string type { get; set; }
    }
}