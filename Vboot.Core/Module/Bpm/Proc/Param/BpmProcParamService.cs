using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SqlSugar;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmProcParamService : ITransient
{
    public void Save(BpmProcParam param){
        _repo.Insert(param);
    }

    public void Delete(string id) {
        _repo.Context.Deleteable<BpmProcParam>().Where(it => it.id == id).ExecuteCommand();
    }
    

    private readonly ISqlSugarRepository<BpmProcParam> _repo;

    public BpmProcParamService(ISqlSugarRepository<BpmProcParam> repo)
    {
        _repo = repo;
    }
}