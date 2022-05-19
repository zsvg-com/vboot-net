using System.Collections.Generic;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Oa;

[ApiDescriptionSettings("Ext", Tag = "流程管理-使用记录")]
public class OaFlowReceApi : IDynamicApiController
{
    
    private readonly OaFlowReceService _service;

    public OaFlowReceApi(OaFlowReceService service)
    {
        _service = service;
    }
    
    public void Post(List<OaFlowRece> reces)
    {
       
    }
    

    public List<dynamic> Get()
    {
        return new List<dynamic>();
    }
}