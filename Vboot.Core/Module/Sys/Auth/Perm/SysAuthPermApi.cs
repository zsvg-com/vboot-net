using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Vboot.Core.Module.Sys;

[ApiDescriptionSettings("Sys", Tag = "权限管理-权限标识")]
public class SysAuthPermApi : IDynamicApiController
{
    private readonly ISqlSugarRepository<SysAuthPerm> _repo;

    public SysAuthPermApi(
        ISqlSugarRepository<SysAuthPerm> repo)
    {
        _repo = repo;
    }

    public async Task<dynamic> GetList()
    {
        return await _repo.ToListAsync();
    }
}