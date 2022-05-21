using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmTaskHistService : ITransient
{
    public BpmTaskHist CreateTask(BpmTaskMain mainTask)
    {
        BpmTaskHist histTask = new BpmTaskHist();
        histTask.id = mainTask.id;
        histTask.proid = mainTask.proid;
        histTask.state = "20";
        histTask.exman = mainTask.exman;
        histTask.nodid = mainTask.nodid;
        histTask.type = "1";
        _repo.Insert(histTask);
        return histTask;
    }
    
    public BpmTaskHist FindOne(string id) {
        return _repo.Single(t => t.id == id);;
    }

    public void Delete(string id) {
        _repo.Context.Deleteable<BpmTaskHist>().Where(it => it.id == id).ExecuteCommand();
    }
    
    public void Update(BpmTaskHist hist) {
        _repo.Context.Updateable(hist).ExecuteCommand();
    }

    private readonly ISqlSugarRepository<BpmTaskHist> _repo;

    public BpmTaskHistService(ISqlSugarRepository<BpmTaskHist> repo)
    {
        _repo = repo;
    }
}