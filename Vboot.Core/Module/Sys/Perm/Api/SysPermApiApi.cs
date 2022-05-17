using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

[ApiDescriptionSettings("Sys", Tag = "权限管理-权限标识")]
public class SysPermApiApi : IDynamicApiController
{
    private readonly ISqlSugarRepository<SysPermApi> _repo;

    public SysPermApiApi(
        ISqlSugarRepository<SysPermApi> repo)
    {
        _repo = repo;
    }

    public async Task<dynamic> GetList()
    {
        return await _repo.ToListAsync();
    }
}