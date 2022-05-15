using System;
using Furion;
using SqlSugar;

namespace Vboot.Core.Common.Util;

public class XreqUtil
{
    public static PagingParam GetPp()
    {
        var pp = new PagingParam();
        var request = App.HttpContext.Request;
        var strPageSize = request.Query["pageSize"].ToString();
        if (!string.IsNullOrEmpty(strPageSize))
        {
            pp.pageSize=int.Parse(strPageSize);
        }
        var strPage = request.Query["page"].ToString();
        if (!string.IsNullOrEmpty(strPage))
        {
            pp.page=int.Parse(strPage);
        }
        return pp;
    }
}

public class PagingParam
{
    public RefAsync<int> total { get; set; } = 0;

    public int page { get; set; } = 1;

    public int pageSize { get; set; } = 10;


}