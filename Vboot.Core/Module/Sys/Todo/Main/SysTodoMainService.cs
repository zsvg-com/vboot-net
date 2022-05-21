using System;
using System.Collections.Generic;
using Furion.DependencyInjection;
using SqlSugar;
using Vboot.Core.Common;
using Vboot.Core.Module.Bpm;
using Yitter.IdGenerator;

namespace Vboot.Core.Module.Sys;

public class SysTodoMainService : BaseService<SysTodoMain>, ITransient
{
    public void SendTodo(Zbpm zbpm, Znode znode)
    {
        SysTodoMain todo = new SysTodoMain();
        todo.id = YitIdHelper.NextId() + "";
        todo.name = zbpm.prona;
        todo.link = "/#/page/ofmv?id=" + zbpm.proid;
        todo.modid = zbpm.proid;

        SysTodoUser todoTarget = new SysTodoUser();
        todoTarget.id = YitIdHelper.NextId() + "";
        todoTarget.tid = todo.id;
        todoTarget.uid = znode.exman;
        repo.InsertAsync(todo);
        repo.Context.Insertable(todoTarget).ExecuteCommand();
    }

    public void DoneTodo(Zbpm zbpm)
    {
        string sql = "select m.id,t.id as tid from sys_todo_main m inner join sys_todo_user t on t.tid=m.id " +
                     "where t.uid=@uid and m.modid=@modid";

        dynamic map = repo.Context.Ado.SqlQuerySingle<dynamic>(sql, new {uid = zbpm.haman, modid = zbpm.proid});
        if (map != null)
        {
            string tid = map.tid;
            repo.Context.Deleteable<SysTodoUser>().Where(it => it.id == tid).ExecuteCommand();
            SysTodoDone done = new SysTodoDone();
            done.id = YitIdHelper.NextId() + "";
            done.tid = "" + map.id;
            done.uid = zbpm.haman;
            repo.Context.Insertable(done);
        }
    }


    public SysTodoMainService(ISqlSugarRepository<SysTodoMain> repo)
    {
        this.repo = repo;
    }
}