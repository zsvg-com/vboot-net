using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Application.Sd.Proj.Main
{
    [SugarTable("sd_proj_main", TableDescription = "项目信息")]
    public class SdProjMain:BaseMainEntity
    {
        /// <summary>
        /// 项目地址
        /// </summary>
        [SugarColumn(ColumnDescription = "项目地址", IsNullable = true, Length = 128)]
        public string addre { get; set; }
        
        /// <summary>
        /// 项目地址
        /// </summary>
        [SugarColumn(ColumnDescription = "项目备注", IsNullable = true, Length = 255)]
        public string notes { get; set; }
        
        
    }
}