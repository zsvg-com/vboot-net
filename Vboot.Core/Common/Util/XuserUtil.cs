using System;
using Furion;
using Vboot.Core.Module.Sys;

namespace Vboot.Core.Common.Util;

public class XuserUtil
{
    public static SysOrg getUser() {
        String userId = null;
        var user = App.GetService<IUserManager>();
        return new SysOrg(user.UserId, user.Name);
    }

    public static String getUserId() {
        String userId = null;
        var user = App.GetService<IUserManager>();
        userId = user.UserId;
        return userId;
    }

}