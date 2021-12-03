using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Gen
{
    [ApiDescriptionSettings("Gen")]
    public class GenOrgMainApi: IDynamicApiController
    {
        private readonly ISqlSugarRepository<SysOrg> repo;

        public GenOrgMainApi(ISqlSugarRepository<SysOrg> Repo)
        {
            repo = Repo;
        }
        
        //根据部门ID，查询下级所有的部门,岗位,用户
        [QueryParameters]
        public async Task<List<ZidName>> GetList(string deptid, int type,string name)
        {
            List<ZidName> list = new List<ZidName>();
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(deptid))
            {
                return list;
            }
            if ((type & 2) != 0) {//部门
                Sqler deptSqler = new Sqler("sys_org_dept");
                if(!string.IsNullOrEmpty(name)){
                    deptSqler.addLike("t.name", name);
                }else{
                    deptSqler.addEqual("t.pid", deptid);
                }
                List<ZidName> deptList = await 
                    repo.Ado.SqlQueryAsync<ZidName>(deptSqler.getSql(),deptSqler.getParams());
                list.AddRange(deptList);
            }
            if ((type & 4) != 0) {//岗位
                Sqler postSqler = new Sqler("sys_org_post");
                if(!string.IsNullOrEmpty(name)){
                    postSqler.addLike("t.name", name);
                }else{
                    postSqler.addEqual("t.deptid", deptid);
                }
                List<ZidName> postList = await 
                    repo.Ado.SqlQueryAsync<ZidName>(postSqler.getSql(),postSqler.getParams());
                list.AddRange(postList);
            }
            if ((type & 8) != 0) {//用户
                Sqler userSqler = new Sqler("sys_org_user");
                if(!string.IsNullOrEmpty(name)){
                    userSqler.addLike("t.name", name);
                }else{
                    userSqler.addEqual("t.deptid", deptid);
                }
                List<ZidName> userList = await 
                    repo.Ado.SqlQueryAsync<ZidName>(userSqler.getSql(),userSqler.getParams());
                list.AddRange(userList);
            }
            return list;
        }
        
        // List<ZidName> deptList = await 
        //     repo.Context.SqlQueryable<ZidName>("select id,name from sys_org_dept")
        //         .Where("name like @name",new{name="%"+name+"%"})
        //         .ToListAsync();
        
    }
}