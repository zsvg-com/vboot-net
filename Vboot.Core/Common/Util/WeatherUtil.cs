using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.RemoteRequest.Extensions;

namespace Vboot.Core.Common.Util
{
    /// <summary>
    /// 天气预报工具类
    /// </summary>
    public static class WeatherUtil
    {
        private static readonly string _getWeatherUrl = "http://wthrcdn.etouch.cn/weather_mini";

        public static async Task<WeatherInfoOutPut> GetWeatherInfo(string cityName = "北京")
        {
            var weatherOutPut = await $"{_getWeatherUrl}?city={cityName}".SetClient("wthrcdn").GetAsAsync<WeatherOutPut>();
            if (weatherOutPut.Status != 1000 || weatherOutPut.Desc != "OK")
            {
                return new WeatherInfoOutPut { Success = false, Desc = weatherOutPut.Desc };
            }

            weatherOutPut.Data.Success = true;

            return weatherOutPut.Data;
        }
    }

    /// <summary>
    /// 天气信息
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// 时间
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 最高温度
        /// </summary>
        public string High { get; set; }

        /// <summary>
        /// 风力
        /// </summary>
        public string Fengli { get; set; }

        /// <summary>
        /// 最低温度
        /// </summary>
        public string Low { get; set; }

        /// <summary>
        /// 分向
        /// </summary>
        public string Fengxiang { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
    }

    public class WeatherInfoOutPut
    {
        /// <summary>
        /// 昨日天气
        /// </summary>
        public WeatherInfo Yesterday { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 未来五天天气
        /// </summary>
        public List<WeatherInfo> Forecast { get; set; }

        /// <summary>
        /// 感冒
        /// </summary>
        public string Ganmao { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public string Wendu { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 失败描述
        /// </summary>
        public string Desc { get; set; }
    }

    public class WeatherOutPut
    {
        /// <summary>
        /// 数据
        /// </summary>
        public WeatherInfoOutPut Data { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}