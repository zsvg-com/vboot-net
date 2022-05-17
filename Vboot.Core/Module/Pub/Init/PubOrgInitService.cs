using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Pub;

public class PubOrgInitService : ITransient
{
    private readonly ISqlSugarRepository<SysOrgDept> _deptRepo;
    private readonly ISqlSugarRepository<SysOrg> _orgRepo;
    private readonly ISqlSugarRepository<SysOrgUser> _userRepo;

    public PubOrgInitService(ISqlSugarRepository<SysOrgDept> deptRepo,
        ISqlSugarRepository<SysOrg> orgRepo,
        ISqlSugarRepository<SysOrgUser> userRepo)
    {
        _deptRepo = deptRepo;
        _orgRepo = orgRepo;
        _userRepo = userRepo;
    }


    public async Task InitAllDept()
    {
        await InitDept("刘", "LIU", 1);
        await InitDept("陈", "CHEN", 2);
        await InitDept("张", "ZHANG", 3);
        await InitDept("李", "LI", 4);
        await InitDept("王", "WANG", 5);
        await InitDept("赵", "ZHAO", 6);
        await InitDept("孙", "SUN", 7);
        await InitDept("周", "ZHOU", 8);
        await InitDept("吴", "WU", 9);
        await InitDept("郑", "ZHENG", 10);
    }

    //部门初始化
    private async Task InitDept(string name, string id, int ornum)
    {
        var deptList = new List<SysOrgDept>();
        var orgList = new List<SysOrg>();
        var corp = new SysOrgDept();
        corp.type = 1;
        corp.id = id;
        corp.name = (name + "家科技");
        corp.label = id;
        corp.ornum = ornum;
        corp.avtag = true;
        corp.tier = "x" + id + "x";
        corp.crtim = DateTime.Now;
        corp.notes = "系统演示用";
        deptList.Add(corp);
        orgList.Add(new SysOrg(id, name + "家科技"));

        var dept1 = new SysOrgDept();
        dept1.type = 2;
        dept1.id = id + "-1";
        dept1.name = name + "一总金办";
        dept1.label = id;
        dept1.pid = id;
        dept1.ornum = 1;
        dept1.avtag = true;
        dept1.tier = "x" + id + "x" + id + "-1" + "x";
        dept1.crtim = DateTime.Now;
        dept1.notes = "系统演示用";
        deptList.Add(dept1);
        orgList.Add(new SysOrg(id + "-1", name + "一总金办"));

        var dept2 = new SysOrgDept();
        dept2.type = 2;
        dept2.id = id + "-2";
        dept2.name = name + "二人事部";
        dept2.label = id;
        dept2.pid = id;
        dept2.ornum = 2;
        dept2.avtag = true;
        dept2.tier = "x" + id + "x" + id + "-2" + "x";
        dept2.crtim = DateTime.Now;
        dept2.notes = "系统演示用";
        deptList.Add(dept2);
        orgList.Add(new SysOrg(id + "-2", name + "二人事部"));

        SysOrgDept dept3 = new SysOrgDept();
        dept3.type = 2;
        dept3.id = id + "-3";
        dept3.name = name + "三销售部";
        dept3.label = id;
        dept3.pid = id;
        dept3.ornum = 3;
        dept3.avtag = true;
        dept3.tier = "x" + id + "x" + id + "-3" + "x";
        dept3.crtim = DateTime.Now;
        dept3.notes = "系统演示用";
        deptList.Add(dept3);
        orgList.Add(new SysOrg(id + "-3", name + "三销售部"));

        SysOrgDept dept4 = new SysOrgDept();
        dept4.type = 2;
        dept4.id = id + "-4";
        dept4.name = name + "四财务部";
        dept4.label = id;
        dept4.pid = id;
        dept4.ornum = 4;
        dept4.avtag = true;
        dept4.tier = "x" + id + "x" + id + "-4" + "x";
        dept4.crtim = DateTime.Now;
        dept4.notes = "系统演示用";
        deptList.Add(dept4);
        orgList.Add(new SysOrg(id + "-4", name + "四财务部"));

        SysOrgDept dept5 = new SysOrgDept();
        dept5.type = 2;
        dept5.id = id + "-5";
        dept5.name = name + "五采购部";
        dept5.label = id;
        dept5.pid = id;
        dept5.ornum = 5;
        dept5.avtag = true;
        dept5.tier = "x" + id + "x" + id + "-5" + "x";
        dept5.crtim = DateTime.Now;
        dept5.notes = "系统演示用";
        deptList.Add(dept5);
        orgList.Add(new SysOrg(id + "-5", name + "五采购部"));

        SysOrgDept dept6 = new SysOrgDept();
        dept6.type = 2;
        dept6.id = id + "-6";
        dept6.name = name + "六技术部";
        dept6.label = id;
        dept6.pid = id;
        dept6.ornum = 6;
        dept6.avtag = true;
        dept6.tier = "x" + id + "x" + id + "-6" + "x";
        dept6.crtim = DateTime.Now;
        dept6.notes = "系统演示用";
        deptList.Add(dept6);
        orgList.Add(new SysOrg(id + "-6", name + "六技术部"));

        SysOrgDept dept7 = new SysOrgDept();
        dept7.type = 2;
        dept7.id = id + "-7";
        dept7.name = name + "七制造部";
        dept7.label = id;
        dept7.pid = id;
        dept7.ornum = 7;
        dept7.avtag = true;
        dept7.tier = "x" + id + "x" + id + "-7" + "x";
        dept7.crtim = DateTime.Now;
        dept7.notes = "系统演示用";
        deptList.Add(dept7);
        orgList.Add(new SysOrg(id + "-7", name + "七制造部"));

        SysOrgDept dept8 = new SysOrgDept();
        dept8.type = 2;
        dept8.id = id + "-8";
        dept8.name = name + "八法务部";
        dept8.label = id;
        dept8.pid = id;
        dept8.ornum = 8;
        dept8.avtag = true;
        dept8.tier = "x" + id + "x" + id + "-8" + "x";
        dept8.crtim = DateTime.Now;
        dept8.notes = "系统演示用";
        deptList.Add(dept8);
        orgList.Add(new SysOrg(id + "-8", name + "八法务部"));

        SysOrgDept dept9 = new SysOrgDept();
        dept9.type = 2;
        dept9.id = id + "-9";
        dept9.name = name + "九信息部";
        dept9.label = id;
        dept9.pid = id;
        dept9.ornum = 9;
        dept9.avtag = true;
        dept9.tier = "x" + id + "x" + id + "-9" + "x";
        dept9.crtim = DateTime.Now;
        dept9.notes = "系统演示用";
        deptList.Add(dept9);
        orgList.Add(new SysOrg(id + "-9", name + "九信息部"));

        for (int i = 1; i <= 10; i++)
        {
            var rsid8 = YitIdHelper.NextId() + "";
            var xdept = new SysOrgDept();
            xdept.type = 2;
            xdept.id = id + "--" + i;
            xdept.name = rsid8;
            xdept.label = id;
            xdept.pid = id + "-" + (i % 10 == 0 ? 1 : i % 10);
            xdept.avtag = true;
            xdept.tier = ("x" + id + "x" + id + "-" + (i % 10 == 0 ? 1 : i % 10) + "x" + id + "--" + i + "x");
            xdept.crtim = DateTime.Now;
            xdept.notes = "系统演示用";
            deptList.Add(xdept);
            orgList.Add(new SysOrg(id + "--" + i, rsid8));
        }

        using var tran = _deptRepo.Context.UseTran();
        await _deptRepo.InsertAsync(deptList);
        await _orgRepo.InsertAsync(orgList);
        tran.CommitTran();
    }


    public async Task InitAllUser()
    {
        await InitUser("刘", "liu", "LIU");
        await InitUser("陈", "c", "CHEN");
        await InitUser("张", "z", "ZHANG");
        await InitUser("李", "l", "LI");
        await InitUser("王", "w", "WANG");
        await InitUser("赵", "zhao", "ZHAO");
        await InitUser("孙", "s", "SUN");
        await InitUser("周", "zhou", "ZHOU");
        await InitUser("吴", "wu", "WU");
        await InitUser("郑", "zheng", "ZHENG");
    }

    private async Task InitUser(string name, string id, string pid)
    {
        List<SysOrgUser> userList = new List<SysOrgUser>();
        List<SysOrg> orgList = new List<SysOrg>();
        SysOrgUser user0 = new SysOrgUser();
        user0.id = id + "lao";
        user0.usnam = id;
        user0.name = name + "老";
        user0.deptid = pid;
        user0.pacod = MD5Encryption.Encrypt("abc1xyz");
        user0.avtag = true;
        user0.tier = "x" + pid + "x" + id + "lao";
        user0.crtim = DateTime.Now;
        user0.notes = "系统演示用";
        userList.Add(user0);
        orgList.Add(new SysOrg(id + "lao", name + "老"));

        for (int i = 1; i <= 9; i++)
        {
            SysOrgUser userx = new SysOrgUser();
            userx.id = id + i;
            userx.usnam = id + i;
            userx.name = name + NumChange("" + i);
            userx.deptid = pid + "-" + i;
            userx.ornum = i;
            userx.pacod = MD5Encryption.Encrypt("abc1xyz");
            userx.avtag = true;
            userx.tier = "x" + pid + "x" + pid + "-" + i + "x" + id + i + "x";
            userx.crtim = DateTime.Now;
            userx.notes = "系统演示用";
            userList.Add(userx);
            orgList.Add(new SysOrg(id + i, name + NumChange("" + i)));

            for (int j = 1; j <= 9; j++)
            {
                SysOrgUser usery = new SysOrgUser();
                usery.id = id + i + j;
                usery.usnam = id + i + j;
                usery.name = name + NumChange("" + i) + NumChange("" + j);
                usery.deptid = pid + "-" + i;
                usery.ornum = j;
                usery.pacod = MD5Encryption.Encrypt("abc1xyz");
                usery.avtag = true;
                usery.tier = "x" + pid + "x" + pid + "-" + i + "x" + id + i + j + "x";
                usery.crtim = DateTime.Now;
                usery.notes = "系统演示用";
                userList.Add(usery);
                orgList.Add(new SysOrg(id + i + j, name + NumChange("" + i) + NumChange("" + j)));
            }
        }

        using var tran = _deptRepo.Context.UseTran();
        await _userRepo.InsertAsync(userList);
        await _orgRepo.InsertAsync(orgList);
        tran.CommitTran();
    }


    public async Task InitZsf()
    {
        SysOrgDept dept1 = new SysOrgDept();
        dept1.type = 2;
        dept1.id = "ZHANG-3-1";
        dept1.name = "张三销售一室";
        dept1.label = "ZHANG";
        dept1.pid = "ZHANG-3";
        dept1.ornum = 1;
        dept1.avtag = true;
        dept1.tier = "xZHANGxZHANG-3xZHANG-3-1x";
        dept1.crtim = DateTime.Now;
        dept1.notes = "系统演示用";
        await _deptRepo.InsertAsync(dept1);
        await _orgRepo.InsertAsync(new SysOrg("ZHANG-3-1", "张三销售一室"));

        SysOrgDept dept2 = new SysOrgDept();
        dept2.type = 2;
        dept2.id = "ZHANG-3-2";
        dept2.name = "张三销售二室";
        dept2.label = "ZHANG";
        dept2.pid = "ZHANG-3";
        dept2.ornum = 1;
        dept2.avtag = true;
        dept2.tier = "xZHANGxZHANG-3xZHANG-3-2x";
        dept2.crtim = DateTime.Now;
        dept2.notes = "系统演示用";
        await _deptRepo.InsertAsync(dept2);
        await _orgRepo.InsertAsync(new SysOrg("ZHANG-3-2", "张三销售二室"));

        SysOrgUser user4 = new SysOrgUser();
        user4.id = "zsf";
        user4.usnam = "zsf";
        user4.name = "张三丰";
        user4.deptid = "ZHANG-3-2";
        user4.ornum = 1;
        user4.pacod = MD5Encryption.Encrypt("abc1xyz");
        user4.avtag = true;
        user4.tier = "xZHANGxZHANG-3xZHANG-3-2xzsfx";
        user4.crtim = DateTime.Now;
        user4.notes = "系统演示用";
        await _userRepo.InsertAsync(user4);
        await _orgRepo.InsertAsync(new SysOrg("zsf", "张三丰"));
    }

    public async Task InitSa()
    {
        Console.WriteLine("开始初始化数据");
        SysOrgUser user = new SysOrgUser();
        user.id = "sa";
        user.usnam = "sa";
        user.name = "管理员";
        user.pacod = MD5Encryption.Encrypt("abc1xyz");
        ;
        user.avtag = true;
        user.tier = "xsax";
        user.crtim = DateTime.Now;
        user.notes = "系统超级管理员";
        await _userRepo.InsertAsync(user);
        await _orgRepo.InsertAsync(new SysOrg("sa", "管理员"));

        SysOrgUser user2 = new SysOrgUser();
        user2.id = "vben";
        user2.usnam = "vben";
        user2.name = "小维";
        user2.pacod = MD5Encryption.Encrypt("abc123456xyz");
        user2.avtag = true;
        user2.tier = "xvbenx";
        user2.crtim = DateTime.Now;
        user2.notes = "系统管理员";
        await _userRepo.InsertAsync(user2);
        await _orgRepo.InsertAsync(new SysOrg("vben", "小维"));
    }


    private string NumChange(string num)
    {
        return num switch
        {
            "1" => "一",
            "2" => "二",
            "3" => "三",
            "4" => "四",
            "5" => "五",
            "6" => "六",
            "7" => "七",
            "8" => "八",
            "9" => "九",
            "10" => "十",
            _ => "零"
        };
    }
}