using System;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Bpm;

namespace Vboot.Core.Modulex.Oa;

public class OaFlowMainService : BaseMainService<OaFlowMain>, ITransient
{
    public async Task Insertx(OaFlowMain oaFlowMain)
    {
        oaFlowMain.state = "20";
        oaFlowMain.uptim = DateTime.Now;
        await InsertAsync(oaFlowMain);
        Zbpm zbpm = oaFlowMain.zbpm;
        zbpm.proid = oaFlowMain.id;
        zbpm.prona = oaFlowMain.name;
        zbpm.haman = oaFlowMain.crmid;
        zbpm.temid = oaFlowMain.protd;
        Znode znode = await _procService.Start(zbpm);
        if (znode.facno == "N3")
        {
            oaFlowMain.state = "30";
            await UpdateAsync(oaFlowMain);
        }
    }

    public async Task Updatex(OaFlowMain oaFlowMain)
    {
        await UpdateAsync(oaFlowMain);
        if (oaFlowMain.zbpm.opkey == "pass")
        {
            Znode znode = _procService.HandlerPass(oaFlowMain.zbpm);
            if (znode.facno == "N3")
            {
                oaFlowMain.state = "30";
                await UpdateAsync(oaFlowMain);
            }
        }
        else if (oaFlowMain.zbpm.opkey == "refuse")
        {
            _procService.HandlerRefuse(oaFlowMain.zbpm);
        }
    }

    private readonly BpmProcMainService _procService;


    public OaFlowMainService(ISqlSugarRepository<OaFlowMain> repo,
        BpmProcMainService procService)
    {
        this.repo = repo;
        _procService = procService;
    }
}