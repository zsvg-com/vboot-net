using System.Collections.Generic;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Vboot.Core.Module.Gen
{
    [ApiDescriptionSettings("Gen")]
    public class GenOrgReceApi: IDynamicApiController
    {
        public void Post()
        {
            
        }
        
        public List<dynamic> GetList()
        {
            return new List<dynamic>();
        }
    }
}