using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Bpm;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Oa;

public class OaFlowTempService : BaseMainService<OaFlowTemp>, ITransient
{
    public async Task Insertx(OaFlowTemp oaFlowTemp)
    {
        BpmProcTemp bpmProcTemp = new BpmProcTemp();
        bpmProcTemp.name = oaFlowTemp.name;
        bpmProcTemp.crman = oaFlowTemp.crman;
        bpmProcTemp.crtim = oaFlowTemp.crtim;
        bpmProcTemp.orxml = oaFlowTemp.prxml;
        bpmProcTemp.chxml = "<?xml version=\"1.0\" encoding=\"gb2312\"?>"
                            + "\n<process" + bpmProcTemp.orxml.Split("bpmn2:process")[1]
                                .Replace("bpmn2:", "").Replace("activiti:", "") + "process>";
        bpmProcTemp.id = YitIdHelper.NextId() + "";
        await repo.Context.Insertable(bpmProcTemp).ExecuteCommandAsync();
        oaFlowTemp.protd = bpmProcTemp.id;
        await InsertAsync(oaFlowTemp);
    }

    public async Task Updatex(OaFlowTemp oaFlowTemp)
    {
        var bpmProcTemp = await repo.Context.Queryable<BpmProcTemp>()
            .Where(it => it.id == oaFlowTemp.protd).FirstAsync();
        bpmProcTemp.orxml = oaFlowTemp.prxml;
        bpmProcTemp.chxml = "<?xml version=\"1.0\" encoding=\"gb2312\"?>"
                            + "\n<process" + bpmProcTemp.orxml.Split("bpmn2:process")[1]
                                .Replace("bpmn2:", "").Replace("activiti:", "") + "process>";
        await repo.Context.Updateable(bpmProcTemp).ExecuteCommandAsync();
        await UpdateAsync(oaFlowTemp);
    }


    public OaFlowTempService(ISqlSugarRepository<OaFlowTemp> repo)
    {
        this.repo = repo;
    }
}