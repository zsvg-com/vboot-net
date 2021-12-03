using Furion.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Vboot.Core.Module.Pub
{
    /// <summary>
    /// 用户登录输出参数
    /// </summary>
   [SuppressSniffer]
    public class LoginOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string usnam { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string ninam { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        public String email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public String monum { get; set; }


        /// <summary>
        /// 管理员类型（0超级管理员 1非管理员）
        /// </summary>
        public int AdminType { get; set; }

        /// <summary>
        /// 最后登陆IP
        /// </summary>
        public string laloi { get; set; }

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime lalot { get; set; }

        /// <summary>
        /// 最后登陆地址
        /// </summary>
        public string LastLoginAddress { get; set; }

        /// <summary>
        /// 最后登陆所用浏览器
        /// </summary>
        public string LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登陆所用系统
        /// </summary>
        public string LastLoginOs { get; set; }

        // public EmpOutput LoginEmpInfo { get; set; } = new EmpOutput();

        // public List<AppOutput> Apps { get; set; } = new List<AppOutput>();

        // public List<RoleOutput> Roles { get; set; } = new List<RoleOutput>();

        /// <summary>
        /// 权限信息
        /// </summary>
        public List<string> Permissions { get; set; } = new List<string>();

        // public List<AntDesignTreeNode> Menus { get; set; } = new List<AntDesignTreeNode>();

        /// <summary>
        /// 数据范围（机构）信息
        /// </summary>
        public List<long> DataScopes { get; set; } = new List<long>();

        ///// <summary>
        ///// 租户信息
        ///// </summary>
        //public List<long> Tenants { get; set; }

        ///// <summary>
        ///// 密码
        ///// </summary>
        //public string Password { get; set; }

        ///// <summary>
        ///// 账户过期
        ///// </summary>
        //public string AccountNonExpired { get; set; }

        ///// <summary>
        ///// 凭证过期
        ///// </summary>
        //public string CredentialsNonExpired { get; set; }

        ///// <summary>
        ///// 账户锁定
        ///// </summary>
        //public bool AccountNonLocked { get; set; }

        ///// <summary>
        ///// 用户名称
        ///// </summary>
        //public string UserName { get; set; }

        ///// <summary>
        ///// 权限
        ///// </summary>
        //public List<long> Authorities { get; set; } = new List<long>();

        ///// <summary>
        ///// 是否启动
        ///// </summary>
        //public bool Enabled { get; set; }
    }
}
