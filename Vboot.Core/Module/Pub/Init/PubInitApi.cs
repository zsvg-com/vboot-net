using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vboot.Core.Module.Pub;

[ApiDescriptionSettings("Sys", Tag = "系统初始化")]
public class PubInitApi : IDynamicApiController, ITransient
{
    private readonly PubOrgInitService _orgInitService;
    private readonly PubAuthInitService _authInitService;

    public PubInitApi(PubOrgInitService orgInitService, PubAuthInitService authInitService)
    {
        _orgInitService = orgInitService;
        _authInitService = authInitService;
    }

    [HttpPost("/pub/org/init")]
    [AllowAnonymous]
    public async Task SysOrgInit()
    {
        await _orgInitService.InitAllDept();
        await _orgInitService.InitAllUser();
        await _orgInitService.InitZsf();
        await _orgInitService.InitSa();
    }

    [HttpPost("/pub/auth/init")]
    [AllowAnonymous]
    public async Task SysAuthInit()
    {
        await _authInitService.InitAllMenu();
    }
}