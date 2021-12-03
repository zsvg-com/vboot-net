using System.Threading.Tasks;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Common
{
    public interface IUserManager
    {
        string Account { get; }
        string Name { get; }
        bool SuperAdmin { get; }
        SysOrgUser User { get; }
        string UserId { get; }

        Task<SysOrgUser> CheckUserAsync(string userId, bool tracking = true);
        Task<SysOrgUser> GetUserEmpInfo(string userId);
    }
}