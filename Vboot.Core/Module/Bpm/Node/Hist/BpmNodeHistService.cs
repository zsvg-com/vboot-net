using System;
using System.Collections.Generic;
using Furion.DependencyInjection;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmNodeHistService : ITransient
{
    private readonly ISqlSugarRepository<BpmNodeHist> _repo;

    public BpmNodeHistService(ISqlSugarRepository<BpmNodeHist> repo)
    {
        _repo = repo;
    }

    public void SaveStartNode(Zbpm zbpm)
    {
        BpmNodeHist startNode = new BpmNodeHist();
        startNode.facno = "N1";
        startNode.facna = "开始节点";
        startNode.facty = "start";
        startNode.proid = zbpm.proid;
        startNode.state = "30";
        startNode.entim = DateTime.Now;
        startNode.tarno = "N2";
        startNode.tarna = "起草节点";
        startNode.id = YitIdHelper.NextId() + "";
        _repo.Insert(startNode);
    }
    
    public void SaveDraftNode(Zbpm zbpm, Znode znode) {
        BpmNodeHist draftNode = new BpmNodeHist();
        draftNode.facno="N2";
        draftNode.facna="起草节点";
        draftNode.facty="draft";
        draftNode.proid=zbpm.proid;
        draftNode.state="30";
        draftNode.id=znode.nodid;
        draftNode.entim=DateTime.Now;
        draftNode.tarno=znode.tarno;
        draftNode.tarna=znode.tarna;
        _repo.Insert(draftNode);
    }
    
    public string SaveEndNode(Zbpm zbpm) {
        BpmNodeHist endNode = new BpmNodeHist();
        endNode.facno="N3";
        endNode.facna="结束节点";
        endNode.facty="end";
        endNode.proid=zbpm.proid;
        endNode.state="30";
        endNode.id=YitIdHelper.NextId() + "";
        endNode.entim=DateTime.Now;;
        _repo.Insert(endNode);
        return endNode.id;
    }

    public void SaveNodeList(Zbpm zbpm, List<Znode> list) {
        foreach (Znode znode in list)
        {
            BpmNodeHist node = new BpmNodeHist();
            node.facno=znode.facno;
            node.facna=znode.facna;
            node.facty=znode.facty;
            node.proid=zbpm.proid;
            node.tarno=znode.tarno;
            node.tarna=znode.tarna;
            node.state="30";
            node.id= YitIdHelper.NextId() + "";
            node.entim=DateTime.Now;
            _repo.Insert(node);
        }
    }

    public BpmNodeHist SaveNode(BpmNodeMain main) {
        BpmNodeHist node = new BpmNodeHist();
        node.id=main.id;
        node.facno=main.facno;
        node.facna=main.facna;
        node.facty=main.facty;
        node.proid=main.proid;
        node.state="20";
         _repo.Insert(node);
         return node;
    }
    
    public BpmNodeHist FindOne(string id) {
        return _repo.Single(t => t.id == id);;
    }
    
    public void Update(BpmNodeHist hist) {
        _repo.Context.Updateable(hist).ExecuteCommand();
    }
    
}