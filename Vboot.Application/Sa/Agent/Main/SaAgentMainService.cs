using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using SqlSugar;
using Vboot.Core.Common;
using Yitter.IdGenerator;

namespace Vboot.Application.Sa;

public class SaAgentMainService : BaseMainService<SaAgentMain>, ITransient
{
    public void JsTest()
    {
        // create a script engine
        using (var engine = new V8ScriptEngine())
        {
     //        // expose a host type
     //        engine.AddHostType("Console", typeof(Console));
     //        engine.Execute("Console.WriteLine('{0} is an interesting number.', Math.PI)");
     //
     //        // expose a host object
     //        engine.AddHostObject("random", new Random());
     //        engine.Execute("Console.WriteLine(random.NextDouble())");
     //
     //        // expose entire assemblies
     //        engine.AddHostObject("lib", new HostTypeCollection("mscorlib", "System.Core"));
     //        engine.Execute("Console.WriteLine(lib.System.DateTime.Now)");
     //
     //        // create a host object from script
     //        engine.Execute(@"
     //     birthday = new lib.System.DateTime(2007, 5, 22);
     //     Console.WriteLine(birthday.ToLongDateString());
     // ");
     //
     //        // use a generic class from script
     //        engine.Execute(@"
     //     Dictionary = lib.System.Collections.Generic.Dictionary;
     //     dict = new Dictionary(lib.System.String, lib.System.Int32);
     //     dict.Add('foo', 123);
     // ");
     //
     //        // call a host method with an output parameter
     //        engine.AddHostObject("host", new HostFunctions());
     //        engine.Execute(@"
     //     intVar = host.newVar(lib.System.Int32);
     //     found = dict.TryGetValue('foo', intVar.out);
     //     Console.WriteLine('{0} {1}', found, intVar);
     // ");
     //
     //        // create and populate a host array
     //        engine.Execute(@"
     //     numbers = host.newArr(lib.System.Int32, 20);
     //     for (var i = 0; i < numbers.Length; i++) { numbers[i] = i; }
     //     Console.WriteLine(lib.System.String.Join(', ', numbers));
     // ");
     //
     //        // create a script delegate
     //        engine.Execute(@"
     //     Filter = lib.System.Func(lib.System.Int32, lib.System.Boolean);
     //     oddFilter = new Filter(function(value) {
     //         return (value & 1) ? true : false;
     //     });
     // ");
     //
     //        // use LINQ from script
     //        engine.Execute(@"
     //     oddNumbers = numbers.Where(oddFilter);
     //     Console.WriteLine(lib.System.String.Join(', ', oddNumbers));
     // ");
     //
     //        // use a dynamic host object
     //        engine.Execute(@"
     //     expando = new lib.System.Dynamic.ExpandoObject();
     //     expando.foo = 123;
     //     expando.bar = 'qux';
     //     delete expando.foo;
     // ");
     //
     //        // call a script function
     //        engine.Execute("function print(x) { Console.WriteLine(x); }");
     //        engine.Script.print(DateTime.Now.DayOfWeek);

            // examine a script object
            engine.Execute(@"var a='6666';var person = { name: 'Fred', age: 5,xx:a }");
            Console.WriteLine(engine.Script.person.name);
            Console.WriteLine(engine.Script.person.xx);
        }
    }


    //新增
    public async Task<string> Insertx(SaAgentMain main)
    {
        main.id = YitIdHelper.NextId() + "";
        var mappings = new List<SaAgentMainViewer>();
        foreach (var viewer in main.viewers)
        {
            mappings.Add(new SaAgentMainViewer {mid = main.id, oid = viewer.id});
        }

        using var tran = repo.Context.UseTran();
        await base.InsertAsync(main);
        await repo.Context.Insertable(mappings).ExecuteCommandAsync();
        tran.CommitTran();
        return main.id;
    }

    //修改
    public async Task<string> Updatex(SaAgentMain main)
    {
        var mappings = new List<SaAgentMainViewer>();
        foreach (var viewer in main.viewers)
        {
            mappings.Add(new SaAgentMainViewer {mid = main.id, oid = viewer.id});
        }

        using var tran = repo.Context.UseTran();
        await base.UpdateAsync(main);
        await repo.Context.Deleteable<SaAgentMainViewer>().Where(it => it.mid == main.id).ExecuteCommandAsync();
        await repo.Context.Insertable(mappings).ExecuteCommandAsync();
        tran.CommitTran();
        return main.id;
    }

    //删除
    public async Task Deletex(string ids)
    {
        using var tran = repo.Context.UseTran();
        var idArr = ids.Split(",");
        await repo.Context.Deleteable<SaAgentMain>().In(idArr).ExecuteCommandAsync();
        for (int i = 0; i < idArr.Length; i++)
        {
            var mid = idArr[i];
            await repo.Context.Deleteable<SaAgentMainViewer>().Where(it => it.mid == mid).ExecuteCommandAsync();
        }

        tran.CommitTran();
    }

    public SaAgentMainService(ISqlSugarRepository<SaAgentMain> repo)
    {
        this.repo = repo;
    }
}