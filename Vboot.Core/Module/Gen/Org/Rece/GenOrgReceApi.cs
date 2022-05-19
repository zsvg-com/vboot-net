using System.Collections.Generic;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Gen;

[ApiDescriptionSettings("Gen")]
public class GenOrgReceApi : IDynamicApiController
{
    
    private readonly SysOrgReceService _service;

    public GenOrgReceApi(SysOrgReceService service)
    {
        _service = service;
    }
    
    public void Post(List<SysOrgRece> reces)
    {
        if (reces != null && reces.Count > 0)
        {
            _service.update(reces);
        }
    }
    

    public List<dynamic> Get()
    {
        return new List<dynamic>();
    }
}