using System.Collections.Generic;
using System.ComponentModel;
using SqlSugar;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Sys;

[SugarTable("sys_auth_menu", TableDescription = "权限管理菜单表")]
[Description("权限管理菜单表")]
public class SysAuthMenu : BaseMainEntity
{
    // [SugarColumn(ColumnDescription = "Id主键", IsPrimaryKey = true,Length = 36)]
    // public string id { get; set; }

    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 64)]
    public string notes { get; set; }

    [SugarColumn(ColumnDescription = "排序号", IsNullable = true)]
    public int ornum { get; set; }

    [SugarColumn(ColumnDescription = "父ID", IsNullable = true, Length = 32)]
    public string pid { get; set; }

    [SqlSugar.SugarColumn(IsIgnore = true)]
    public string pname { get; set; }

    [SqlSugar.SugarColumn(IsIgnore = true)]
    public List<SysAuthMenu> children { get; set; }

    [SugarColumn(ColumnDescription = "类型 C目录，M菜单，B按钮", IsNullable = true, Length = 32)]
    public string type { get; set; }

    [SugarColumn(ColumnDescription = "代码", IsNullable = true, Length = 32)]
    public string code { get; set; }

    [SugarColumn(ColumnDescription = "图标", IsNullable = true, Length = 64)]
    public string icon { get; set; }

    [SugarColumn(ColumnDescription = "路由地址", IsNullable = true, Length = 64)]
    public string path { get; set; }

    [SugarColumn(ColumnDescription = "组件路径", IsNullable = true, Length = 64)]
    public string comp { get; set; }

    [SugarColumn(ColumnDescription = "权限标识", IsNullable = true, Length = 64)]
    public string perm { get; set; }

    [SugarColumn(ColumnDescription = "跳转", IsNullable = true, Length = 64)]
    public string redirect { get; set; }

    [SugarColumn(ColumnDescription = "外链标记", IsNullable = true)]
    public bool extag { get; set; }

    [SugarColumn(ColumnDescription = "缓存标记", IsNullable = true)]
    public bool catag { get; set; }

    [SugarColumn(ColumnDescription = "是否显示", IsNullable = true)]
    public bool shtag { get; set; }
}