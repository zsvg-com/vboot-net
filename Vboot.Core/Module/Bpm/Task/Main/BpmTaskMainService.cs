using System.Collections.Generic;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SqlSugar;
using Vboot.Core.Common;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Bpm;

public class BpmTaskMainService : ITransient
{
    public BpmTaskMain CreateTask(Zbpm zbpm, Znode znode)
    {
        BpmTaskMain task = new BpmTaskMain();
        task.id = YitIdHelper.NextId() + "";
        task.proid = zbpm.proid;
        task.state = "20";
        task.exman = znode.exman;
        task.ornum = 0;
        task.actag = true;
        task.nodid = znode.nodid;
        task.type = "1";
        _repo.Insert(task);
        return task;
    }

    public void FindCurrentExmen(List<dynamic> itemList)
    {
        string ids = "(";
        foreach (var item in itemList)
        {
            if (item.state != "30")
            {
                ids += "'" + item.id + "',";
            }
        }

        if ("(" != ids)
        {
            ids = ids.Substring(0, ids.Length - 1) + ")";
            string sql =
                "select n.id as tasid,t.id as nodid,o.name exnam,n.exman,t.proid,t.facno,t.facna from bpm_node_main t" +
                " inner join bpm_task_main n on n.nodid=t.id " +
                "inner join sys_org o on o.id=n.exman " +
                "where t.proid in " + ids + " and n.actag=1 order by t.proid, n.ornum";
            List<dynamic> tasks = _repo.Context.Ado.SqlQuery<dynamic>(sql);

            List<Zinp> list = new List<Zinp>();
            string proid = "";
            foreach (var task in tasks)
            {
                if (task.proid != proid)
                {
                    Zinp zinp = new Zinp();
                    zinp.id = task.proid;
                    zinp.name = task.facno + "." + task.facna;
                    zinp.pid = task.exnam;
                    list.Add(zinp);
                }
                else
                {
                    list[list.Count - 1].pid += ";" + task.exnam;
                }

                proid = task.proid;
            }

            foreach (dynamic item in itemList)
            {
                foreach (var zinp in list)
                {
                    if (zinp.id == item.id)
                    {
                        item.facno = zinp.name;
                        item.exmen = zinp.pid;
                        break;
                    }
                }
            }
        }
    }


    public void Delete(string id)
    {
        _repo.Context.Deleteable<BpmTaskMain>().Where(it => it.id == id).ExecuteCommand();
    }

    private readonly ISqlSugarRepository<BpmTaskMain> _repo;

    public BpmTaskMainService(ISqlSugarRepository<BpmTaskMain> repo)
    {
        _repo = repo;
    }
}