using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Pub.Auth;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Module.Pub
{
    public class LoginService : ITransient
    {
        private readonly ISqlSugarRepository<SysOrgUser> _repo;

        public LoginService(ISqlSugarRepository<SysOrgUser> repo)
        {
            _repo = repo;
        }
        
        public async Task<DbUser> getDbUser(string username)
        {
            const string sql = "select id,name,pacod,retag from sys_org_user where usnam=? and avtag=1";
            var dbUser = await _repo.Ado.SqlQuerySingleAsync<DbUser>(sql, new{username});
            // if (dbUser == null)
            // {
            //     throw new Exception("账号不存在或密码错误");
            // }
            if (dbUser == null)
            {
                throw Oops.Oh(ErrorCode.D1000);
            }
            return dbUser;
        }
    }
}