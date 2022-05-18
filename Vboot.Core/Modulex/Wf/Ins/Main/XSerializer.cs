using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Vboot.Core.Modulex.Wf
{
    public class XSerializer
    {
        /// <summary>
        /// 将对象序列化为xml字符串
        /// </summary>
        /// <typeparam name="T">类型<peparam>
        /// <param name="t">对象</param>
        public static string ObjectToXml<T>(T t) where T : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                formatter.Serialize(stream, t, namespaces);
                string result = Encoding.UTF8.GetString(stream.ToArray());
                return result;
            }
        }
        /// <summary>
        /// 序列化成XML 清空格式
        /// </summary>
        public static string ObjectToXml<T>(T t, Encoding encoding) where T : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using MemoryStream stream = new MemoryStream();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, encoding);
            xmlTextWriter.Formatting = System.Xml.Formatting.None;
            formatter.Serialize(xmlTextWriter, t, namespaces);
            xmlTextWriter.Flush();
            xmlTextWriter.Close();
            string result = encoding.GetString(stream.ToArray());
            return result;
        }

        /// <summary>
        /// 字符串转换为对象
        /// </summary>
        public static T XmlToObject<T>(string xml) where T : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                T result = formatter.Deserialize(ms) as T;
                return result;
            }
        }
    }
}