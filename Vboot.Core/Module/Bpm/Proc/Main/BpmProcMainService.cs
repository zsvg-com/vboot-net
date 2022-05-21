using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Common.Util;
using Vboot.Core.Module.Sys;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmProcMainService : BaseMainService<BpmProcMain>, ITransient
{
    public async Task<Znode> Start(Zbpm zbpm)
    {
        // int i = 0;
        // int j = 3 / i;
        // Thread.Sleep(5000);
        
        //1 保存流程实例
        BpmProcMain bpmProcMain = new BpmProcMain(zbpm);
        bpmProcMain.crtim = DateTime.Now;
        bpmProcMain.avtag = true;
        bpmProcMain.state = "20";
        await repo.InsertAsync(bpmProcMain);

        //2 历史节点表保存开始节点
        _nodeHistService.SaveStartNode(zbpm);

        //3 流程流转（收集流转过的节点，计算出下一个待审批节点）
        Znode draftNode = new Znode("N2");
        draftNode.facna="起草节点";
        draftNode.facty="draft";
        List<Znode> list = new List<Znode>();
        string xmlSql = "select t.chxml from bpm_proc_temp t where t.id=@temid";
        zbpm.chxml = repo.Context.Ado.SqlQuerySingle<string>(xmlSql, new {zbpm.temid});
        Znode nextNode = _hand.ProcFlow(zbpm, list, draftNode);//流转核心逻辑
        
        
        //4.1 历史节点表保存起草节点
        draftNode.nodid=YitIdHelper.NextId()+"";
        _nodeHistService.SaveDraftNode(zbpm, draftNode);
        //4.2 评审表保存起草节点的评审信息
        _auditMainService.SaveDraftAudit(zbpm, draftNode);
        //4.3 历史节点表保存其他已流节点（条件分支等非审批节点）
        _nodeHistService.SaveNodeList(zbpm, list);
        //4.4 当前节点表保存下一个待审批节点
        BpmNodeMain nodeMain = _nodeMainService.SaveNode(zbpm, nextNode);
        nextNode.nodid=nodeMain.id;
        //4.5 历史节点表保存下一个待审批节点
        _nodeHistService.SaveNode(nodeMain);
        
        //5.1 当前任务表创建待审节点的任务
        BpmTaskMain mainTask = _taskMainService.CreateTask(zbpm, nextNode);
        //5.2 历史任务表创建待审节点的任务
        _taskHistService.CreateTask(mainTask);
        
        //6 发起待办
        _todoService.SendTodo(zbpm, nextNode);
        return nextNode;
    }
    
    public Znode HandlerPass(Zbpm zbpm) {
        zbpm.haman = XuserUtil.getUserId();
        string sql = "select m.id as proid,m.name as prona from bpm_proc_main m where m.id=@proid";
        dynamic map = repo.Context.Ado.SqlQuerySingle<dynamic>(sql, new {proid = zbpm.proid});
        zbpm.prona = "" + map.prona;
        
        BpmProcMain bpmProcMain = repo.Context.Queryable<BpmProcMain>()
            .Where(it => it.id == zbpm.proid).First();;
        
        zbpm.opkey="pass";
        zbpm.opinf="通过";

        //1 评审表保存当前节点的评审信息
        _auditMainService.SaveAudit(zbpm);
        if(!string.IsNullOrEmpty(zbpm.bacid)){
             _paramService.Delete(zbpm.bacid);
        }

        //2.1 将历史任务变成已办
        BpmTaskHist histTask = _taskHistService.FindOne(zbpm.tasid);
        histTask.haman=zbpm.haman;
        histTask.entim=DateTime.Now;
        histTask.state="30";
        _taskHistService.Update(histTask);
        //2.2 删除当前任务表记录
        _taskMainService.Delete(zbpm.tasid);

        //3 流程流转
        Znode currNode = new Znode(zbpm.facno);
        currNode.nodid=zbpm.nodid;
        currNode.facno=zbpm.facno;
        currNode.facty="review";
        if (!string.IsNullOrEmpty(zbpm.bacid))
        {
            currNode.tarno=zbpm.tarno;
            currNode.tarna=zbpm.tarna;
        }
        List<Znode> list = new List<Znode>();
        
        string xmlSql = @"select t.chxml from bpm_proc_temp t 
inner join bpm_proc_main m on m.temid=t.id  where m.id=@proid";
        zbpm.chxml = repo.Context.Ado.SqlQuerySingle<string>(xmlSql, new {zbpm.proid});
        Znode nextNode = _hand.ProcFlow(zbpm, list, currNode);//流转核心逻辑

        //4.1 将历史节点变成已办
        BpmNodeHist histNode = _nodeHistService.FindOne(zbpm.nodid);
        histNode.entim=DateTime.Now;
        histNode.state="30";
        histNode.tarno=currNode.tarno;
        histNode.tarna = currNode.tarna;
        _nodeHistService.Update(histNode);
        currNode.facna=histNode.facna;
        //4.2 历史节点表保存已流节点
        _nodeHistService.SaveNodeList(zbpm, list);
        //4.3 删除当前节点表记录
        _nodeMainService.Delete(zbpm.nodid);

        if ("end"!=nextNode.facty) {
            //5.1 当前节点表保存下一个待审批节点
            BpmNodeMain nodeMain = _nodeMainService.SaveNode(zbpm, nextNode);
            nextNode.nodid=nodeMain.id;
            //5.2 历史节点表保存下一个待审批节点
            _nodeHistService.SaveNode(nodeMain);

            //6.1 当前任务表创建待审节点的任务
            BpmTaskMain mainTask = _taskMainService.CreateTask(zbpm, nextNode);
            //6.2 历史任务表创建待审节点的任务
            _taskHistService.CreateTask(mainTask);

            //7.1 删除之前的待办
            _todoService.DoneTodo(zbpm);
            //7.2 发起新待办
            _todoService.SendTodo(zbpm, nextNode);
        } else {
            //5 历史节点表保存结束节点
            string endNodeId=_nodeHistService.SaveEndNode(zbpm);

            //6 评审表保存结束节点的评审信息
            _auditMainService.SaveEndAudit(zbpm,endNodeId);

            //7 删除之前的待办
            _todoService.DoneTodo(zbpm);
            
            //8 将流程更新成完结
            bpmProcMain.state = "30";
            repo.Context.Updateable(bpmProcMain)
                .UpdateColumns(it => new { it.state }).ExecuteCommand();
            
        }

        return nextNode;
    }
    
    
    public Znode HandlerRefuse(Zbpm zbpm) {
        zbpm.haman = XuserUtil.getUserId();
        string sql = "select m.id as proid,m.name as prona from bpm_proc_main m where m.id=?";
        dynamic map = repo.Context.Ado.SqlQuerySingle<dynamic>(sql, new {proid = zbpm.proid});
        zbpm.prona = "" + map.prona;
        
        //驳回: "起草节点"（返回本人）
        if(zbpm.retag){
            zbpm.opinf="驳回: "+zbpm.tarno+"."+zbpm.tarna+"（返回本人）";
        }else{
            zbpm.opinf="驳回: "+zbpm.tarno+"."+zbpm.tarna;
        }

        //1 评审表保存当前节点的评审信息
        _auditMainService.SaveAudit(zbpm);

        //2.1 将历史任务变成已办
        BpmTaskHist histTask = _taskHistService.FindOne(zbpm.tasid);
        histTask.haman=zbpm.haman;
        histTask.entim=DateTime.Now;
        histTask.state="30";
        //2.2 删除当前任务表记录
        _taskMainService.Delete(zbpm.tasid);

        //3 创建驳回节点
//        Znode refuseNode = hand.getNodeInfo(zbpm,zbpm.getTarno());
        Znode refuseNode = new Znode();
        refuseNode.facno=zbpm.tarno;
        refuseNode.facna=zbpm.tarna;
        refuseNode.exman=zbpm.exman;
        refuseNode.facty="review";

        //4.1 将历史节点变成已办
        BpmNodeHist histNode = _nodeHistService.FindOne(zbpm.nodid);
        histNode.tarno=zbpm.tarno;
        histNode.tarna=zbpm.tarna;
        histNode.entim=DateTime.Now;
        histNode.state="30";
        //4.2 删除当前节点表记录
        _nodeMainService.Delete(zbpm.nodid);

        //5.1 当前节点表保存下一个待审批节点
        BpmNodeMain nodeMain = _nodeMainService.SaveNode(zbpm, refuseNode);
        refuseNode.nodid=nodeMain.id;
        //5.2 历史节点表保存下一个待审批节点
        _nodeHistService.SaveNode(nodeMain);
        //5.3 如果驳回时勾选了 驳回的节点通过后直接返回本节点
        if(zbpm.retag){
            BpmProcParam param = new BpmProcParam();
            param.id=YitIdHelper.NextId()+"";
            param.proid=zbpm.proid;
            param.offty="proc";
            param.offid=zbpm.proid;
            param.pakey=zbpm.tarno+"#refuse";
            param.paval=zbpm.facno;
            _paramService.Save(param);
        }

        //6.1 当前任务表创建待审节点的任务
        BpmTaskMain mainTask = _taskMainService.CreateTask(zbpm, refuseNode);
        //6.2 历史任务表创建待审节点的任务
        _taskHistService.CreateTask(mainTask);

        //7.1 删除之前的待办
        _todoService.DoneTodo(zbpm);
        //7.2 发起新待办
        _todoService.SendTodo(zbpm, refuseNode);
        return refuseNode;
    }


    private readonly BpmNodeHistService _nodeHistService;
    
    private readonly BpmNodeMainService _nodeMainService;
    
    private readonly BpmAuditMainService _auditMainService;
    
    private readonly BpmTaskHistService _taskHistService;
    
    private readonly BpmTaskMainService _taskMainService;
    
    private readonly SysTodoMainService _todoService;
    
    private readonly BpmProcParamService _paramService;
    
    private readonly BpmProcMainHand _hand;


    public BpmProcMainService(ISqlSugarRepository<BpmProcMain> repo,
        BpmProcMainHand hand,
        BpmNodeHistService nodeHistService,
        BpmNodeMainService nodeMainService,
        BpmAuditMainService auditMainService,
        BpmTaskHistService taskHistService,
        BpmTaskMainService taskMainService,
        SysTodoMainService todoService,
        BpmProcParamService paramService)
    {
        this.repo = repo;
        _hand = hand;
        _nodeHistService = nodeHistService;
        _nodeMainService = nodeMainService;
        _auditMainService = auditMainService;
        _taskHistService = taskHistService;
        _taskMainService = taskMainService;
        _todoService = todoService;
        _paramService = paramService;
    }
}