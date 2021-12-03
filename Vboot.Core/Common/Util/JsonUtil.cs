using System.Collections.Generic;
using Furion.JsonSerialization;
using Newtonsoft.Json.Linq;

namespace Vboot.Core.Common.Util
{
    /// <summary>
    /// Json序列化工具类
    /// </summary>
    public static class JsonUtil
    {
        /// <summary>
        /// JSON 字符串转 Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            json = json.Replace("&nbsp;", "");
            return JSON.Deserialize<T>(json);
        }

        /// <summary>
        /// Object 转 JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return JSON.Serialize(obj);
        }

        /// <summary>
        /// JSON 字符串转 JObject
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject ToJObject(this string json)
        {
            return json == null ? JObject.Parse("{}") : JObject.Parse(json.Replace("&nbsp;", ""));
        }

        /// <summary>
        /// Dictionary 字符串转 Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static T ToObject<T>(this IDictionary<string, object> dictionary)
        {
            return dictionary.ToJsonString().ToObject<T>();
        }
    }
}