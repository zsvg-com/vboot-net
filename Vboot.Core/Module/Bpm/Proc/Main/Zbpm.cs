namespace Vboot.Core.Module.Bpm;

public class Zbpm
{
    public string todid { get; set; }//待办ID
    
    public string temid { get; set; }//流程模板ID
    
    public string proid { get; set; }//流程实例ID
    
    public string prona { get; set; }//流程实例名称
    
    public string nodid { get; set; }//当前节点ID
    
    public string facno { get; set; }//当前节点编号
    
    public string facna { get; set; }//当前节点名称
    
    public string tarno { get; set; }//目标节点编号
    
    public string tarna { get; set; }//目标节点名称

    public bool retag { get; set; } = true;//驳回标记，驳回的节点通过后直接返回本节点
    
    public string bacid { get; set; }//驳回后的流程重新提交时的bpm_proc_param的id
    
    public string tasid { get; set; }//任务ID
    
    public string opnot { get; set; }//操作：处理意见
    
    public string opurg { get; set; }//操作：紧急程度
    
    public string opkey { get; set; }//操作key:pass, reject
    
    public string opinf { get; set; }//操作名称:通过，驳回到谁，沟通谁
    
    public string chxml { get; set; }//优化过的vboot可解析的的xml
    
    public string haman { get; set; }//当前处理人ID
    
    public string exman { get; set; }//应处理人ID
    
}