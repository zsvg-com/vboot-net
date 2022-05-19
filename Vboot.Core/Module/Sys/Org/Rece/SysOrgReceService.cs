using System.Collections.Generic;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class SysOrgReceService:  ITransient
{

    public void update(List<SysOrgRece> reces)
    {
        string userid = XuserUtil.getUserId();
        foreach (var rece in reces)
        {
            rece.orgid = rece.id;
            rece.id = YitIdHelper.NextId() + "";
            rece.userid = userid;
        }

        // //1.如果当前数量小于10，则去数据库查询最新的差额记录数
        // if (reces.Count < 10) {
        //     
        //     
        //     Sqler sqler = new Sqler("t.*", "sys_org_rece", 1, 10 - reces.size());
        //     sqler.addDescOrder("t.uptim");
        //     sqler.addEqual("t.userid", userid);
        //     
        //     for (SysOrgRece dbRece : list) {
        //         boolean flag = false;
        //         for (SysOrgRece rece : reces) {
        //             if (dbRece.getOrgid().equals(rece.getOrgid())) {
        //                 flag = true;
        //             }
        //         }
        //         if (!flag) {
        //             reces.add(dbRece);
        //         }
        //     }
        //     
        // }
        //
        // //2.清空当前用户的最近使用记录
        // jdbcDao.update("delete from sys_org_rece where userid=?", userid);
        //
        // //3.更新记录
        // repo.saveAll(reces);
        // return reces.size();
    }
    
    
    
    
    private readonly ISqlSugarRepository<SysOrgRece> _repo;

    public SysOrgReceService(ISqlSugarRepository<SysOrgRece> repo)
    {
        _repo = repo;
    }
}