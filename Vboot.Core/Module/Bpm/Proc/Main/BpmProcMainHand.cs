using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Furion.DependencyInjection;
using Vboot.Core.Common;

namespace Vboot.Core.Module.Bpm;

public class BpmProcMainHand : ITransient
{
    public Znode ProcFlow(Zbpm zbpm, List<Znode> list, Znode znode)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(zbpm.chxml);
        XmlElement rootNode = xmlDoc.DocumentElement;
        string nextNode = "";
        //查找当前节点的目标节点，如果已有（比如是驳回返回的）则不需要额外查找
        if (znode.tarno == null)
        {
            foreach (XmlElement node in rootNode.ChildNodes)
            {
                if (node.Name == "sequenceFlow")
                {
                    if (znode.facno == node.GetAttribute("sourceRef"))
                    {
                        znode.tarno = node.GetAttribute("targetRef");
                        break;
                    }
                }
            }
        }

        //判断nextNode是否为审批节点
        return NodeFlow(rootNode, list, znode);
    }

    //节点流转
    //判断节点是否为审批节点，如果为审批节点则取处理人
    //      如果是条件分支，则根据分支条件流转到下一个节点，直到找到审批节点
    private Znode NodeFlow(XmlElement rootNode, List<Znode> list, Znode znode)
    {
        if ("N3" == znode.tarno)
        {
            znode.tarna = "结束节点";
            Znode endNode = new Znode();
            endNode.facno = "N3";
            endNode.facna = "结束节点";
            endNode.facty = "end";
            return endNode;
        }

        String userid = "";
        foreach (XmlElement node in rootNode.ChildNodes)
        {
            if ("task" == node.Name || "userTask" == node.Name)
            {
                //节点为审批节点
                if (znode.tarno == node.GetAttribute("id"))
                {
                    userid = node.GetAttribute("assignee");
                    znode.tarna = node.GetAttribute("name");
                    //list.add(znode);
                    Znode nextNode = new Znode();
                    nextNode.facno = znode.tarno;
                    nextNode.facna = znode.tarna;
                    nextNode.facty = "review";
                    nextNode.exman = userid;
                    return nextNode;
                }
            }
            else if ("exclusiveGateway" == node.Name)
            {
                //节点为条件分支
                String nextNodeId = "";
                if (znode.tarno == node.GetAttribute("id"))
                {
                    znode.tarna = node.GetAttribute("name");
                    // list.Add(znode);
                    foreach (XmlElement xnode in rootNode.ChildNodes)
                    {
                        if ("sequenceFlow" == xnode.Name)
                        {
                            if (znode.tarno == xnode.GetAttribute("sourceRef"))
                            {
                                if (checkConds(xnode))
                                {
                                    //满足条件时
                                    nextNodeId = xnode.GetAttribute("targetRef");
                                    Console.WriteLine("条件分支转到:" + nextNodeId);
                                    Znode nextNode = new Znode();
                                    nextNode.facno = znode.tarno;
                                    nextNode.facna = znode.tarna;
                                    nextNode.facty = "condtion";
                                    nextNode.tarno = nextNodeId;
                                    list.Add(nextNode);
                                    return NodeFlow(rootNode, list, nextNode);
                                }
                            }
                        }
                    }

                    break;
                }
            }

            if ("" != userid)
            {
                break;
            }
        }

        return null;
    }

    private bool checkConds(XmlElement it)
    {
        foreach (XmlElement son in it.ChildNodes)
        {
            if ("extensionElements" == son.Name)
            {
                Console.WriteLine("进入了：extensionElements");
                foreach (XmlElement sun in son.ChildNodes)
                {
                    if ("executionListener" == sun.Name)
                    {
                        Console.WriteLine("进入了：executionListener");
                        if (!checkCond(sun.GetAttribute("expression")))
                        {
                            return false;
                        }

                        break;
                    }
                }

                break;
            }
        }

        return true;
    }

    private bool checkCond(string expression)
    {
        Console.WriteLine("条件为：" + expression);
        return true;
    }

    public Znode GetNodeInfo(string chxml, string facno)
    {
        //根据temid寻找xml的filename
//         String xml = "<?xml version=\"1.0\" encoding=\"gb2312\"?>"
//                      + "\n<process" +
//                      xmlstr.Split("bpmn2:process")[1].Replace("bpmn2:", "").Replace("activiti:", "") + "process>";
// //        System.out.println(xml);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(chxml);
        XmlElement rootNode = xmlDoc.DocumentElement;

        foreach (XmlElement node in rootNode.ChildNodes)
        {
            if ("userTask" == node.Name || "task" == node.Name)
            {
                if (facno == node.GetAttribute("id"))
                {
                    Znode znode = new Znode();
                    znode.facno = facno;
                    znode.facna = node.GetAttribute("name");
                    znode.exman = node.GetAttribute("assignee");
                    return znode;
                }
            }
        }

        return null;
    }

    public Znode CalcTarget(string chxml, String facno)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(chxml);
        XmlElement rootNode = xmlDoc.DocumentElement;
        Znode currNode = new Znode();
        currNode.facno = facno;
        foreach (XmlElement node in rootNode.ChildNodes)
        {
            if ("sequenceFlow" == node.Name)
            {
                if (facno == node.GetAttribute("sourceRef"))
                {
                    currNode.tarno = node.GetAttribute("targetRef");
                    break;
                }
            }
        }

        List<Znode> list = new List<Znode>();
        Znode nextNode = NodeFlow(rootNode, list, currNode);
        return nextNode;
    }

    public Znode GetFirstNode(string xml, string facno)
    {
        //根据temid寻找xml的filename
        xml = "<?xml version=\"1.0\" encoding=\"gb2312\"?>"
              + "\n<process" +
              xml.Split("bpmn2:process")[1].Replace("bpmn2:", "").Replace("activiti:", "") + "process>";
        // Console.WriteLine(xml);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        XmlElement rootNode = xmlDoc.DocumentElement;


        Znode currNode = new Znode();
        currNode.facno = facno;
        foreach (XmlElement node in rootNode.ChildNodes)
        {
            if ("sequenceFlow" == node.Name)
            {
                if (facno == node.GetAttribute("sourceRef"))
                {
                    currNode.tarno = node.GetAttribute("targetRef");
                    break;
                }
            }
        }

        List<Znode> list = new List<Znode>();
        Znode nextNode = NodeFlow(rootNode, list, currNode);
        return nextNode;
    }

    public List<Zinp> GetAllLineList(string xml)
    {
        //根据temid寻找xml的filename
        xml = "<?xml version=\"1.0\" encoding=\"gb2312\"?>"
              + "\n<process" +
              xml.Split("bpmn2:process")[1].Replace("bpmn2:", "").Replace("activiti:", "") + "process>";
        // Console.WriteLine(xml);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xml);
        XmlElement rootNode = xmlDoc.DocumentElement;

        List<Zinp> list = new List<Zinp>();
        foreach (XmlElement node in rootNode.ChildNodes)
        {
            if ("sequenceFlow" == node.Name)
            {
                Zinp zinp = new Zinp();
                zinp.id = node.GetAttribute("id");
                zinp.name = node.GetAttribute("sourceRef");
                zinp.pid = node.GetAttribute("targetRef");
                list.Add(zinp);
            }
        }

        return list;
    }


    // string xmlstr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
    //                 "<bpmn2:definitions xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:bpmn2=\"http://www.omg.org/spec/BPMN/20100524/MODEL\" xmlns:bpmndi=\"http://www.omg.org/spec/BPMN/20100524/DI\" xmlns:dc=\"http://www.omg.org/spec/DD/20100524/DC\" xmlns:di=\"http://www.omg.org/spec/DD/20100524/DI\" xmlns:activiti=\"http://activiti.org/bpmn\" id=\"sample-diagram\" targetNamespace=\"http://activiti.org/bpmn\" xsi:schemaLocation=\"http://www.omg.org/spec/BPMN/20100524/MODEL BPMN20.xsd\">\n" +
    //                 "  <bpmn2:process id=\"Process_1\" name=\"1\" isExecutable=\"true\">\n" +
    //                 "    <bpmn2:startEvent id=\"N1\" name=\"开始节点\">\n" +
    //                 "      <bpmn2:outgoing>L1</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:startEvent>\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L1\" sourceRef=\"N1\" targetRef=\"N2\" />\n" +
    //                 "    <bpmn2:exclusiveGateway id=\"N9\" name=\"条件分支\">\n" +
    //                 "      <bpmn2:documentation>判断审批金额是否大于100</bpmn2:documentation>\n" +
    //                 "      <bpmn2:incoming>L4</bpmn2:incoming>\n" +
    //                 "      <bpmn2:outgoing>L6</bpmn2:outgoing>\n" +
    //                 "      <bpmn2:outgoing>L5</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:exclusiveGateway>\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L4\" sourceRef=\"N4\" targetRef=\"N9\" />\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L6\" name=\"王五分支线\" sourceRef=\"N9\" targetRef=\"N6\">\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:executionListener expression=\"money&#60;=100\" event=\"take\" />\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "    </bpmn2:sequenceFlow>\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L5\" name=\"李四分支线\" sourceRef=\"N9\" targetRef=\"N5\">\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:executionListener expression=\"money&#62;100\" event=\"take\" />\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "    </bpmn2:sequenceFlow>\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L7\" sourceRef=\"N5\" targetRef=\"N7\" />\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L8\" sourceRef=\"N6\" targetRef=\"N7\" />\n" +
    //                 "    <bpmn2:endEvent id=\"N3\" name=\"结束节点\">\n" +
    //                 "      <bpmn2:incoming>L2</bpmn2:incoming>\n" +
    //                 "    </bpmn2:endEvent>\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L2\" sourceRef=\"N7\" targetRef=\"N3\" />\n" +
    //                 "    <bpmn2:sequenceFlow id=\"L3\" sourceRef=\"N2\" targetRef=\"N4\" />\n" +
    //                 "    <bpmn2:userTask id=\"N2\" name=\"起草节点\" activiti:assignee=\"sa\" activiti:candidateUsers=\"\">\n" +
    //                 "      <bpmn2:documentation>起草节点，表单数据一般从绑定的表单提取</bpmn2:documentation>\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:formProperty id=\"userid\" type=\"string\" />\n" +
    //                 "        <activiti:formProperty id=\"money\" type=\"int\" />\n" +
    //                 "        <activiti:properties>\n" +
    //                 "          <activiti:property name=\"编辑\" value=\"edit\" />\n" +
    //                 "          <activiti:property name=\"撤回\" value=\"back\" />\n" +
    //                 "          <activiti:property name=\"提交\" value=\"commit\" />\n" +
    //                 "        </activiti:properties>\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "      <bpmn2:incoming>L1</bpmn2:incoming>\n" +
    //                 "      <bpmn2:outgoing>L3</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:userTask>\n" +
    //                 "    <bpmn2:userTask id=\"N4\" name=\"张三审批\" activiti:assignee=\"z3\" activiti:candidateUsers=\"\">\n" +
    //                 "      <bpmn2:documentation>审批节点</bpmn2:documentation>\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:properties>\n" +
    //                 "          <activiti:property name=\"审批\" value=\"approve\" />\n" +
    //                 "          <activiti:property name=\"驳回\" value=\"reject\" />\n" +
    //                 "        </activiti:properties>\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "      <bpmn2:incoming>L3</bpmn2:incoming>\n" +
    //                 "      <bpmn2:outgoing>L4</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:userTask>\n" +
    //                 "    <bpmn2:userTask id=\"N5\" name=\"李四审批\" activiti:assignee=\"l4\" activiti:candidateUsers=\"\">\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:properties>\n" +
    //                 "          <activiti:property name=\"审批\" value=\"approve\" />\n" +
    //                 "          <activiti:property name=\"驳回\" value=\"reject\" />\n" +
    //                 "        </activiti:properties>\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "      <bpmn2:incoming>L5</bpmn2:incoming>\n" +
    //                 "      <bpmn2:outgoing>L7</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:userTask>\n" +
    //                 "    <bpmn2:userTask id=\"N6\" name=\"王五审批\" activiti:assignee=\"w5\" activiti:candidateUsers=\"\">\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:properties>\n" +
    //                 "          <activiti:property name=\"审批\" value=\"approve\" />\n" +
    //                 "          <activiti:property name=\"驳回\" value=\"reject\" />\n" +
    //                 "        </activiti:properties>\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "      <bpmn2:incoming>L6</bpmn2:incoming>\n" +
    //                 "      <bpmn2:outgoing>L8</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:userTask>\n" +
    //                 "    <bpmn2:userTask id=\"N7\" name=\"赵六审批\" activiti:assignee=\"zhao6\" activiti:candidateUsers=\"\">\n" +
    //                 "      <bpmn2:extensionElements>\n" +
    //                 "        <activiti:formProperty id=\"userid\" type=\"string\" />\n" +
    //                 "        <activiti:taskListener class=\"do some thing\" event=\"complete\" />\n" +
    //                 "        <activiti:properties>\n" +
    //                 "          <activiti:property name=\"审批\" value=\"approve\" />\n" +
    //                 "        </activiti:properties>\n" +
    //                 "      </bpmn2:extensionElements>\n" +
    //                 "      <bpmn2:incoming>L7</bpmn2:incoming>\n" +
    //                 "      <bpmn2:incoming>L8</bpmn2:incoming>\n" +
    //                 "      <bpmn2:outgoing>L2</bpmn2:outgoing>\n" +
    //                 "    </bpmn2:userTask>\n" +
    //                 "  </bpmn2:process>\n" +
    //                 "  <bpmndi:BPMNDiagram id=\"BPMNDiagram_1\">\n" +
    //                 "    <bpmndi:BPMNPlane id=\"BPMNPlane_1\" bpmnElement=\"Process_1\">\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_0uh8wmt_di\" bpmnElement=\"L3\">\n" +
    //                 "        <di:waypoint x=\"560\" y=\"200\" />\n" +
    //                 "        <di:waypoint x=\"560\" y=\"250\" />\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_01hb865_di\" bpmnElement=\"L2\">\n" +
    //                 "        <di:waypoint x=\"560\" y=\"670\" />\n" +
    //                 "        <di:waypoint x=\"560\" y=\"732\" />\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_1t81399_di\" bpmnElement=\"L8\">\n" +
    //                 "        <di:waypoint x=\"690\" y=\"550\" />\n" +
    //                 "        <di:waypoint x=\"690\" y=\"630\" />\n" +
    //                 "        <di:waypoint x=\"610\" y=\"630\" />\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_00vdr2t_di\" bpmnElement=\"L7\">\n" +
    //                 "        <di:waypoint x=\"430\" y=\"550\" />\n" +
    //                 "        <di:waypoint x=\"430\" y=\"630\" />\n" +
    //                 "        <di:waypoint x=\"510\" y=\"630\" />\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_1bnjbaa_di\" bpmnElement=\"L5\">\n" +
    //                 "        <di:waypoint x=\"535\" y=\"400\" />\n" +
    //                 "        <di:waypoint x=\"430\" y=\"400\" />\n" +
    //                 "        <di:waypoint x=\"430\" y=\"470\" />\n" +
    //                 "        <bpmndi:BPMNLabel>\n" +
    //                 "          <dc:Bounds x=\"456\" y=\"382\" width=\"55\" height=\"14\" />\n" +
    //                 "        </bpmndi:BPMNLabel>\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_12ug1tp_di\" bpmnElement=\"L6\">\n" +
    //                 "        <di:waypoint x=\"585\" y=\"400\" />\n" +
    //                 "        <di:waypoint x=\"690\" y=\"400\" />\n" +
    //                 "        <di:waypoint x=\"690\" y=\"470\" />\n" +
    //                 "        <bpmndi:BPMNLabel>\n" +
    //                 "          <dc:Bounds x=\"611\" y=\"382\" width=\"56\" height=\"14\" />\n" +
    //                 "        </bpmndi:BPMNLabel>\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_1miguqf_di\" bpmnElement=\"L4\">\n" +
    //                 "        <di:waypoint x=\"560\" y=\"330\" />\n" +
    //                 "        <di:waypoint x=\"560\" y=\"375\" />\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNEdge id=\"Flow_1u6pmzo_di\" bpmnElement=\"L1\">\n" +
    //                 "        <di:waypoint x=\"560\" y=\"68\" />\n" +
    //                 "        <di:waypoint x=\"560\" y=\"120\" />\n" +
    //                 "      </bpmndi:BPMNEdge>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Event_0byql27_di\" bpmnElement=\"N1\">\n" +
    //                 "        <dc:Bounds x=\"542\" y=\"32\" width=\"36\" height=\"36\" />\n" +
    //                 "        <bpmndi:BPMNLabel>\n" +
    //                 "          <dc:Bounds x=\"539\" y=\"2\" width=\"43\" height=\"14\" />\n" +
    //                 "        </bpmndi:BPMNLabel>\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Gateway_0qd58o6_di\" bpmnElement=\"N9\" isMarkerVisible=\"true\">\n" +
    //                 "        <dc:Bounds x=\"535\" y=\"375\" width=\"50\" height=\"50\" />\n" +
    //                 "        <bpmndi:BPMNLabel>\n" +
    //                 "          <dc:Bounds x=\"538\" y=\"432\" width=\"44\" height=\"14\" />\n" +
    //                 "        </bpmndi:BPMNLabel>\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Event_1h4oob7_di\" bpmnElement=\"N3\">\n" +
    //                 "        <dc:Bounds x=\"542\" y=\"732\" width=\"36\" height=\"36\" />\n" +
    //                 "        <bpmndi:BPMNLabel>\n" +
    //                 "          <dc:Bounds x=\"539\" y=\"775\" width=\"43\" height=\"14\" />\n" +
    //                 "        </bpmndi:BPMNLabel>\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Activity_0g48n8q_di\" bpmnElement=\"N2\">\n" +
    //                 "        <dc:Bounds x=\"510\" y=\"120\" width=\"100\" height=\"80\" />\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Activity_04wpn1b_di\" bpmnElement=\"N4\">\n" +
    //                 "        <dc:Bounds x=\"510\" y=\"250\" width=\"100\" height=\"80\" />\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Activity_0j8meci_di\" bpmnElement=\"N5\">\n" +
    //                 "        <dc:Bounds x=\"380\" y=\"470\" width=\"100\" height=\"80\" />\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Activity_0pa1f64_di\" bpmnElement=\"N6\">\n" +
    //                 "        <dc:Bounds x=\"640\" y=\"470\" width=\"100\" height=\"80\" />\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "      <bpmndi:BPMNShape id=\"Activity_0qmb4lo_di\" bpmnElement=\"N7\">\n" +
    //                 "        <dc:Bounds x=\"510\" y=\"590\" width=\"100\" height=\"80\" />\n" +
    //                 "      </bpmndi:BPMNShape>\n" +
    //                 "    </bpmndi:BPMNPlane>\n" +
    //                 "  </bpmndi:BPMNDiagram>\n" +
    //                 "</bpmn2:definitions>\n";
}