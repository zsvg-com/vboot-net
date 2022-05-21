using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;

namespace Vboot.Core.Module.Bpm;

[ApiDescriptionSettings("Bpm", Tag = "流程引擎-实例")]
public class BpmProcMainApi : IDynamicApiController
{
    private readonly BpmProcMainService _service;

    private readonly BpmProcMainHand _hand;

    public BpmProcMainApi(BpmProcMainService service,
        BpmProcMainHand hand)
    {
        _service = service;
        _hand = hand;
    }

    [QueryParameters]
    public async Task<dynamic> Get(string name)
    {
        var pp = XreqUtil.GetPp();
        var items = await _service.repo.Context.Queryable<BpmProcMain>()
            .WhereIF(!string.IsNullOrWhiteSpace(name), t => t.name.Contains(name.Trim()))
            .Select((t) => new {t.id, t.name})
            .ToPageListAsync(pp.page, pp.pageSize, pp.total);
        return RestPageResult.Build(pp.total.Value, items);
    }

    public async Task<BpmProcMain> GetOne(string id)
    {
        var cate = await _service.repo.Context.Queryable<BpmProcMain>()
            .Where(it => it.id == id).FirstAsync();
        return cate;
    }

    public async Task<Dictionary<string, object>> GetZbpm(string proid)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        //审批记录
        string sql = "select t.id,t.crtim,t.facna,t.facno,t.opnot,t.opinf,o.name as haman from bpm_audit_main t " +
                     "inner join sys_org o on o.id=t.haman " +
                     "where t.proid=@proid order by t.crtim";
        List<dynamic> audits = _service.repo.Context.Ado.SqlQuery<dynamic>(sql, new {proid});
        dict.Add("audits", audits);

        //历史处理人
        string hiHamen = "";
        foreach (var audit in audits)
        {
            if (!hiHamen.Contains("" + audit.haman))
            {
                hiHamen += audit.haman + ";";
            }
        }

        if (hiHamen.Contains(";"))
        {
            hiHamen = hiHamen.Substring(0, hiHamen.Length - 1);
        }

        dict.Add("hiHamen", hiHamen);

        //当前处理人与当前用户是否在当前处理人中
        string sql2 =
            "select n.id as tasid,t.id as nodid,o.name exnam,n.exman,t.proid,t.facno,t.facna from bpm_node_main t" +
            " inner join bpm_task_main n on n.nodid=t.id " +
            "inner join sys_org o on o.id=n.exman " +
            "where t.proid=@proid and n.actag=1 order by n.ornum";
        List<dynamic> tasks = _service.repo.Context.Ado.SqlQuery<dynamic>(sql2, new {proid});
        string cuExmen = "";
        bool cutag = false;
        string userid = XuserUtil.getUserId();
        Zbpm zbpm = new Zbpm();
        foreach (var task in tasks)
        {
            if (string.IsNullOrEmpty(zbpm.proid))
            {
                zbpm.proid = "" + task.proid;
                zbpm.nodid = "" + task.nodid;
                zbpm.facno = "" + task.facno;
                zbpm.facna = "" + task.facna;
            }

            cuExmen += task.exnam + ";";
            if (userid == "" + task.exman)
            {
                zbpm.tasid = "" + task.tasid;
                zbpm.exman = "" + task.exman;
                cutag = true;
            }
        }

        if (cuExmen.Contains(";"))
        {
            cuExmen = cuExmen.Substring(0, cuExmen.Length - 1);
        }

        dict.Add("cuExmen", cuExmen);
        dict.Add("cutag", cutag);
        dict.Add("zbpm", zbpm);
        return dict;
    }

    //获取即将流向的节点
    [QueryParameters]
    public async Task<Dictionary<string, object>> GetTarget(string proid, string facno)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        Znode nextNode;
        //如果是之前被驳回的节点则，通过后要判断是否直接返回驳回的节点
        string refuseSql = "select t.id,t.paval from bpm_proc_param t where t.proid=@proid and t.pakey=@pakey";
        dynamic bacMap =
            _service.repo.Context.Ado.SqlQuerySingle<dynamic>(refuseSql, new {proid, pakey = facno + "#refuse"});
        string xmlSql = @"select t.chxml from bpm_proc_temp t 
inner join bpm_proc_main m on m.temid=t.id  where m.id=@proid";
        string chxml = _service.repo.Context.Ado.SqlQuerySingle<string>(xmlSql, new {proid});
        if (bacMap != null && !string.IsNullOrEmpty(bacMap.paval))
        {
            nextNode = _hand.GetNodeInfo(chxml, "" + bacMap.paval);
        }
        else
        {
            nextNode = _hand.CalcTarget(chxml, facno);
        }


        string sql = "select t.name as id from sys_org t where t.id=@exman";
        string tamen = _service.repo.Context.Ado.SqlQuerySingle<string>(sql, new {exman = nextNode.exman});
        dict.Add("tarno", nextNode.facno);
        dict.Add("tarna", nextNode.facna);
        dict.Add("tamen", tamen);
        if (bacMap != null)
        {
            dict.Add("bacid", bacMap.id);
        }

        return dict;
    }

    public Dictionary<string, object> GetXml(string proid)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        string xmlSql = "select t.orxml from bpm_proc_temp t inner join bpm_proc_main m on m.temid=t.id where m.id=@proid";
        string xml = _service.repo.Context.Ado.SqlQuerySingle<string>(xmlSql, new {proid});

        string sql = "select distinct t.facno from bpm_node_hist t where proid=@proid order by sttim";
        List<string> nodeList=  _service.repo.Context.Ado.SqlQuery<string>(sql, new {proid});

        List<Zinp> allLineList = _hand.GetAllLineList(xml);
        HashSet<string> lineSet = new HashSet<string>();
        foreach (var zinp in allLineList)
        {
            foreach (var node in nodeList)
            {
                if (zinp.name==node)
                {
                    foreach (var node2 in nodeList)
                    {
                        if (zinp.pid==node2)
                        {
                            lineSet.Add(zinp.id);
                            break;
                        }
                    }
                    break;
                }
            }
        }
        
        dict.Add("xml", xml);
        dict.Add("nodeList", nodeList);
        dict.Add("lineList", lineSet);
        return dict;
    }

    public Dictionary<string, object> GetTexml(string temid)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        string xmlSql = "select t.orxml from bpm_proc_temp t where t.id=@temid";
        string xml = _service.repo.Context.Ado.SqlQuerySingle<string>(xmlSql, new {temid});
        Znode nextNode = _hand.GetFirstNode(xml, "N2");

        string tamenSql = "select t.name as id from sys_org t where t.id=@exman";
        string tamen = _service.repo.Context.Ado.SqlQuerySingle<string>(tamenSql, new {exman = nextNode.exman});
        dict.Add("tarno", nextNode.facno);
        dict.Add("tarna", nextNode.facna);
        dict.Add("tamen", tamen);
        dict.Add("xml", xml);
        return dict;
    }

    public void PostHpass(Zbpm zbpm)
    {
        zbpm.haman = XuserUtil.getUserId();
        string sql = "select m.id as proid,m.name as prona from bpm_proc_main m where m.id=@proid";
        dynamic map = _service.repo.Context.Ado.SqlQuerySingle<dynamic>(sql, new {proid = zbpm.proid});
        zbpm.prona = "" + map.prona;
        _service.HandlerPass(zbpm);
    }

    public void PostHrefuse(Zbpm zbpm)
    {
        zbpm.haman = XuserUtil.getUserId();
        string sql = "select m.id as proid,m.name as prona from bpm_proc_main m where m.id=?";
        dynamic map = _service.repo.Context.Ado.SqlQuerySingle<dynamic>(sql, new {proid = zbpm.proid});
        zbpm.prona = "" + map.prona;
        _service.HandlerRefuse(zbpm);
    }

    //返回当前节点之前的已走过的节点
    [QueryParameters]
    public List<dynamic> GetRefnodes(string proid, string facno)
    {
        string sql =
            @"select distinct t.facno id,t.facna name,t.haman exman from bpm_audit_main t 
where proid=@proid and opkey in('dsubmit','pass') order by t.crtim";
        List<dynamic> allList=  _service.repo.Context.Ado.SqlQuery<dynamic>(sql, new {proid});
        List<dynamic> list = new List<dynamic>();
        foreach (var item in allList)
        {
            if (item.id == facno)
            {
                break;
            }
            list.Add(item);
        }
        return list;
    }


    public async Task Post(BpmProcMain main)
    {
        await _service.InsertAsync(main);
    }

    public async Task Put(BpmProcMain cate)
    {
        await _service.UpdateAsync(cate);
    }

    public async Task Delete(string ids)
    {
        await _service.DeleteAsync(ids);
    }
}