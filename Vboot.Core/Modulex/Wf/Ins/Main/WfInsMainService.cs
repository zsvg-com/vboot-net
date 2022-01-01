using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Furion.DependencyInjection;
using Microsoft.CodeAnalysis.Text;
using SqlSugar;
using Vboot.Core.Common;
using Yitter.IdGenerator;

namespace Vboot.Core.Modulex.Wf
{
    
    public class WfInsMainService : BaseService<WfInsMain>, ITransient
    {
        public WfInsMainService(ISqlSugarRepository<WfInsMain> repo)
        {
            this.repo = repo;
        }
        
        
        public async Task InsertAsyncWithFlow(WfInsMain main)
        {
            List<Entity> entityList=null;
            XmlSerializer xs = new XmlSerializer(typeof(List<Entity>));
            string sr="<?xml version=\"1.0\" encoding=\"utf-8\" ?><Test></Test>";
            // entityList = xs.Deserialize() as List<Entity>;
            // xs.
            
            
            if (string.IsNullOrEmpty(main.id))
            {
                main.id = YitIdHelper.NextId() + "";
            }
            await repo.InsertAsync(main);
            //1. 查询开始节点的outgoing
            string startOut = findStartOutgoing(main.xml);
            string processXml ="<?xml version=\"1.0\" encoding=\"utf-8\" ?>"+
                               findProcessXml(main.xml).Replace("bpmn2:","")
                                   .Replace("activiti:","");
            // var xx= XSerializer.XmlToObject<Entity>(processXml);
            
            XmlDocument xmlDoc  = new XmlDocument();
            xmlDoc.LoadXml(processXml);
            string firstNode = findFirstNode(xmlDoc);
            Console.WriteLine(firstNode);

            string nextNode = findNextNode(xmlDoc, "N2");
            Console.WriteLine(nextNode);

            // Console.WriteLine(processXml);

        }
        
        private string findNextNode(XmlDocument xmlDoc,string currNode)
        {
            XmlElement rootElement = xmlDoc.DocumentElement;
            string nextNode = "";
            foreach(var item in rootElement.ChildNodes)
            {
                var e = item as XmlElement;
                if (e.Name == "sequenceFlow")
                {
                    if (currNode == e.GetAttribute("sourceRef"))
                    {
                        nextNode = e.GetAttribute("targetRef");
                        return nextNode;
                    }
                }
            }
            return nextNode;
        }


        private string findFirstNode(XmlDocument xmlDoc)
        {
            XmlElement rootElement = xmlDoc.DocumentElement;
            string startLine = "";
            string firstNode = "";
            foreach(var item in rootElement.ChildNodes)
            {
                var e = item as XmlElement;
                //获取节点名称
                // Console.WriteLine(e.Name);
                if (e.Name == "startEvent")
                {
                    // Console.WriteLine(e.GetAttribute("id"));
                    foreach (var item2 in e.ChildNodes)
                    {
                        var e2 = item2 as XmlElement;
                        if (e2.Name == "outgoing")
                        {
                            startLine = e2.InnerText;
                            Console.WriteLine(e2.InnerText);
                            break;
                        }
                    }
                }else if (e.Name == "sequenceFlow")
                {
                    if (startLine == e.GetAttribute("id"))
                    {
                        firstNode = e.GetAttribute("targetRef");
                        return firstNode;
                    }
                }
                
                //获取节点属性
                // Console.WriteLine(e.GetAttribute("姓名"));
                //遍历子节点
                // foreach (var jtem in e.ChildNodes)
                // {
                //     var k = jtem as XmlElement;
                //     WriteLine(k.Name+":"+k.InnerText);
                // }
            }
            return firstNode;
        }
        
        private string findStartOutgoing(string xml)
        {
            string[] arr= xml.Split("startEvent");
            int firstgo = arr[1].IndexOf("outgoing");
            int lastgo = arr[1].LastIndexOf("outgoing");
            return arr[1].Substring(firstgo+9,lastgo-firstgo-17);
        }
        
        private string findProcessXml(string xml)
        {
            int start = xml.IndexOf("<bpmn2:process");
            int end = xml.IndexOf("</bpmn2:process");
            return xml.Substring(start,end+16-start);
        }
        

    }
}