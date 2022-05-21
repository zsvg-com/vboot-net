using Furion.DependencyInjection;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmNodeMainService : ITransient
{
    public BpmNodeMain SaveNode(Zbpm zbpm, Znode znode)
    {
        BpmNodeMain node = new BpmNodeMain();
        node.facno = znode.facno;
        node.facna = znode.facna;
        node.facty = znode.facty;
        node.proid = zbpm.proid;
        node.state = "20";
        node.id = YitIdHelper.NextId() + "";
        _repo.Insert(node);
        return node;
    }
    
    
    public BpmNodeMain FindOne(string id) {
        return _repo.Single(t => t.id == id);;
    }

    public void Delete(string id) {
        _repo.Context.Deleteable<BpmNodeMain>().Where(it => it.id == id).ExecuteCommand();
    }


    private readonly ISqlSugarRepository<BpmNodeMain> _repo;

    public BpmNodeMainService(ISqlSugarRepository<BpmNodeMain> repo)
    {
        _repo = repo;
    }
}