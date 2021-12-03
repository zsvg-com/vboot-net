using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using Furion;
using Furion.RemoteRequest.Extensions;
using UAParser;

namespace Vboot.Core.Common.Util
{
    /// <summary>
    /// HTTP网络工具
    /// </summary>
    public static class HttpNewUtil
    {
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public static string Ip
        {
            get
            {
                var result = string.Empty;
                if (App.HttpContext != null)
                {
                    result = GetWebClientIp();
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = GetLanIp();
                }

                return result;
            }
        }

        /// <summary>
        /// 得到客户端IP地址
        /// </summary>
        /// <returns></returns>
        private static string GetWebClientIp()
        {
            var ip = GetWebRemoteIp();
            foreach (var hostAddress in Dns.GetHostAddresses(ip))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 得到局域网IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 得到远程Ip地址
        /// </summary>
        /// <returns></returns>
        private static string GetWebRemoteIp()
        {
            if (App.HttpContext?.Connection?.RemoteIpAddress == null)
                return string.Empty;
            var ip = App.HttpContext?.Connection?.RemoteIpAddress.ToString();
            if (App.HttpContext == null)
                return ip;
            if (App.HttpContext.Request.Headers.ContainsKey("X-Real-IP"))
            {
                ip = App.HttpContext.Request.Headers["X-Real-IP"].ToString();
            }

            if (App.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ip = App.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            }

            return ip;
        }

        /// <summary>
        /// 请求UserAgent信息
        /// </summary>
        public static string UserAgent
        {
            get
            {
                string userAgent = App.HttpContext?.Request?.Headers["User-Agent"];

                return userAgent;
            }
        }

        /// <summary>
        /// 请求Url
        /// </summary>
        public static string Url
        {
            get
            {
                var url = new StringBuilder().Append(App.HttpContext?.Request?.Scheme).Append("://")
                    .Append(App.HttpContext?.Request?.Host).Append(App.HttpContext?.Request?.PathBase)
                    .Append(App.HttpContext?.Request?.Path).Append(App.HttpContext?.Request?.QueryString).ToString();
                return url;
            }
        }

        /// <summary>
        /// 得到操作系统版本
        /// </summary>
        /// <returns></returns>
        public static string GetOSVersion()
        {
            var osVersion = string.Empty;
            var userAgent = UserAgent;
            if (userAgent.Contains("NT 10"))
            {
                osVersion = "Windows 10";
            }
            else if (userAgent.Contains("NT 6.3"))
            {
                osVersion = "Windows 8";
            }
            else if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Android"))
            {
                osVersion = "Android";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }

            return osVersion;
        }

        /// <summary>
        /// 公网信息
        /// 慎用，如果不是直接请求接口，而是通过代理转发，拿到的是服务器的公网信息
        /// </summary>
        /// <returns></returns>
        public static async Task<WhoisIPInfoModel> WanInfo()
        {
            const string url = "http://whois.pconline.com.cn/ipJson.jsp";
            var resultStr = await url.GetAsStringAsync();
            resultStr = resultStr[(resultStr.IndexOf("IPCallBack(", StringComparison.Ordinal) + "IPCallBack(".Length)..]
                .TrimEnd();
            resultStr = resultStr[..^3];
            var result = resultStr.ToObject<WhoisIPInfoModel>();
            return result;
        }

        /// <summary>
        /// 根据IP地址获取公网信息
        /// </summary>
        /// <returns></returns>
        public static async Task<WhoisIPInfoModel> WanInfo(string ip)
        {
            var url = $"http://whois.pconline.com.cn/ipJson.jsp?ip={ip}";
            var resultStr = await url.GetAsStringAsync();
            resultStr = resultStr[(resultStr.IndexOf("IPCallBack(", StringComparison.Ordinal) + "IPCallBack(".Length)..]
                .TrimEnd();
            resultStr = resultStr[..^3];
            var result = resultStr.ToObject<WhoisIPInfoModel>();
            return result;
        }

        /// <summary>
        /// UserAgent信息
        /// </summary>
        /// <returns></returns>
        public static UserAgentInfoModel UserAgentInfo()
        {
            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(UserAgent);
            var result = new UserAgentInfoModel
            {
                PhoneModel = clientInfo.Device.ToString(), OS = clientInfo.OS.ToString(), Browser = clientInfo.UA.ToString()
            };
            return result;
        }

        private static readonly char[] reserveChar = { '/', '?', '*', ':', '|', '\\', '<', '>', '\"' };

        /// <summary>
        /// 远程路径Encode处理,会保证开头是/，结尾也是/
        /// </summary>
        /// <param name="remotePath"></param>
        /// <returns></returns>
        public static string EncodeRemotePath(string remotePath)
        {
            if (remotePath == "/")
            {
                return remotePath;
            }

            var endWith = remotePath.EndsWith("/");
            var part = remotePath.Split('/');
            remotePath = "";
            foreach (var s in part)
            {
                if (s == "")
                    continue;
                if (remotePath != "")
                {
                    remotePath += "/";
                }

                remotePath += HttpUtility.UrlEncode(s).Replace("+", "%20");
            }

            remotePath = (remotePath.StartsWith("/") ? "" : "/") + remotePath + (endWith ? "/" : "");
            return remotePath;
        }

        /// <summary>
        /// 标准化远程目录路径,会保证开头是/，结尾也是/ ,如果命名不规范，存在保留字符，会返回空字符
        /// </summary>
        /// <param name="remotePath">要标准化的远程路径</param>
        /// <returns></returns>
        public static string StandardizationRemotePath(string remotePath)
        {
            if (string.IsNullOrEmpty(remotePath))
            {
                return "";
            }

            if (!remotePath.StartsWith("/"))
            {
                remotePath = "/" + remotePath;
            }

            if (!remotePath.EndsWith("/"))
            {
                remotePath = remotePath + "/";
            }

            var index1 = 1;
            while (index1 < remotePath.Length)
            {
                var index2 = remotePath.IndexOf('/', index1);
                if (index2 == index1)
                {
                    return "";
                }

                var folderName = remotePath.Substring(index1, index2 - index1);
                if (folderName.IndexOfAny(reserveChar) != -1)
                {
                    return "";
                }

                index1 = index2 + 1;
            }

            return remotePath;
        }
    }

    /// <summary>
    /// 万网Ip信息Model类
    /// </summary>
    public class WhoisIPInfoModel
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Pro { get; set; }

        /// <summary>
        /// 省份邮政编码
        /// </summary>
        public string ProCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 城市邮政编码
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 地理信息
        /// </summary>
        [JsonPropertyName("addr")]
        public string Address { get; set; }

        /// <summary>
        /// 运营商
        /// </summary>
        public string Operator => Address[(Pro.Length + City.Length)..].Trim();
    }

    /// <summary>
    /// UserAgent 信息Model类
    /// </summary>
    public class UserAgentInfoModel
    {
        /// <summary>
        /// 手机型号
        /// </summary>
        public string PhoneModel { get; set; }

        /// <summary>
        /// 操作系统（版本）
        /// </summary>
        public string OS { get; set; }

        /// <summary>
        /// 浏览器（版本）
        /// </summary>
        public string Browser { get; set; }
    }
}