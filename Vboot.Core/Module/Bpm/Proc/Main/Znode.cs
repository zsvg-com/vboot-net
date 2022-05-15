namespace Vboot.Core.Module.Bpm;

public class Znode
{
    
    public string nodid { get; set; }//节点ID
    
    public string facno { get; set; }//当前节点编号:N1,N2
    
    public string facna { get; set; }//当前节点名称
    
    public string facty { get; set; }//当前节点类型
    
    public string tarno { get; set; }//目标节点编号
    
    public string tarna { get; set; }//目标节点名称
    
    public string exman { get; set; }//应处理人
    
}