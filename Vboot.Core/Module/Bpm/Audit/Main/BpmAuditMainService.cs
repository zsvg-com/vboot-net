using System.Collections.Generic;
using Furion.DependencyInjection;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmAuditMainService : ITransient
{
    public BpmAuditMain SaveDraftAudit(Zbpm zbpm, Znode znode)
    {
        BpmAuditMain audit = new BpmAuditMain();
        audit.id = YitIdHelper.NextId() + "";
        audit.facno = znode.facno;
        audit.facna = znode.facna;
        audit.nodid = znode.nodid;
        audit.haman = zbpm.haman;
        audit.proid = zbpm.proid;
        audit.opnot = zbpm.opnot;
        audit.opkey = "dsubmit";
        audit.opinf = "起草人提交";
        _repo.Insert(audit);
        return audit;
    }

    public BpmAuditMain SaveAudit(Zbpm zbpm)
    {
        BpmAuditMain audit = new BpmAuditMain();
        audit.id = YitIdHelper.NextId() + "";
        audit.facno = zbpm.facno;
        audit.facna = zbpm.facna;
        audit.nodid = zbpm.nodid;
        audit.haman = zbpm.haman;
        audit.proid = zbpm.proid;
        audit.opnot = zbpm.opnot;
        audit.opkey = zbpm.opkey;
        audit.opinf = zbpm.opinf;
        audit.tasid = zbpm.tasid;
        _repo.Insert(audit);
        return audit;
    }

    public BpmAuditMain SaveEndAudit(Zbpm zbpm, string nodid)
    {
        BpmAuditMain audit = new BpmAuditMain();
        audit.id = YitIdHelper.NextId() + "";
        audit.facno = "N3";
        audit.facna = "结束节点";
        audit.nodid = nodid;
        audit.proid = zbpm.proid;
        audit.opkey = "end";
        _repo.Insert(audit);
        return audit;
    }

    private readonly ISqlSugarRepository<BpmAuditMain> _repo;

    public BpmAuditMainService(ISqlSugarRepository<BpmAuditMain> repo)
    {
        _repo = repo;
    }
}