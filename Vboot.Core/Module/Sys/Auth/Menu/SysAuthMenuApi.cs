using System;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys
{
    [ApiDescriptionSettings("Sys",Tag ="权限管理-菜单" )]
    public class SysAuthMenuApi : IDynamicApiController
    {
        private readonly SysAuthMenuService _service;

        public SysAuthMenuApi(
            SysAuthMenuService service)
        {
            _service = service;
        }

        [QueryParameters]
        public async Task<dynamic> GetTree()
        {
            var treeList = await _service.repo.Context
                .SqlQueryable<SysAuthMenu>(
                    "select id,pid,name,type,crtim,uptim,notes from sys_auth_menu order by ornum")
                .ToTreeAsync(it => it.children, it => it.pid, null);
            return treeList;
        }

        public async Task<SysAuthMenu> GetOne(string id)
        {
            var menu = await _service.repo.Context.Queryable<SysAuthMenu>()
                .Where(it => it.id == id).FirstAsync();
            if (menu.pid != null)
            {
                menu.pname = await _service.repo.Context.Queryable<SysAuthMenu>()
                    .Where(it => it.id == menu.pid).Select(it => it.name).SingleAsync();
            }

            return menu;
        }

        public async Task Post(SysAuthMenu menu)
        {
            menu.id = YitIdHelper.NextId() + "";
            await _service.InsertAsync(menu);
        }

        public async Task Put(SysAuthMenu menu)
        {
            await _service.UpdateAsync(menu);
        }

        public async Task Delete(string ids)
        {
            var idArr = ids.Split(",");
            foreach (var id in idArr)
            {
                var count = await
                    _service.repo.Context.Queryable<SysAuthMenu>().Where(it => it.pid == id).CountAsync();
                if (count > 0)
                {
                    throw new Exception("有子菜单或按钮无法删除");
                }
            }
            await _service.DeleteAsync(idArr);
        }
    }
}