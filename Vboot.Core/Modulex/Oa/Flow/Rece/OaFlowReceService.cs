using System.Collections.Generic;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common.Util;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class OaFlowReceService : ITransient
{
    public void update(List<OaFlowRece> reces)
    {
        string userid = XuserUtil.getUserId();
        foreach (var rece in reces)
        {
            rece.flowid = rece.id;
            rece.id = YitIdHelper.NextId() + "";
            rece.userid = userid;
        }
    }


    private readonly ISqlSugarRepository<OaFlowRece> _repo;

    public OaFlowReceService(ISqlSugarRepository<OaFlowRece> repo)
    {
        _repo = repo;
    }
}