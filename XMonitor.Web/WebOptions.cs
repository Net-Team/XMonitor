using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.Web
{
    /// <summary>
    /// 网站服务选项
    /// </summary>
    public class WebOptions : IMonitorServiceOptions
    {
        /// <summary>
        /// 获取或设置日志工具
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 获取或设置检测的时间间隔
        /// 默认1分钟
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 请求超时时间
        /// 超时将处理为异常
        /// 默认1分钟
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 重试次数，当超过指定数次才定义为错误
        /// 默认3次
        /// </summary>
        public int Retry { get; set; } = 3;

        /// <summary>
        /// 获取或设置响应内容过滤器
        /// 默认为null
        /// </summary>
        public Func<string, bool> HttpContentFilter { get; set; }

        /// <summary>
        /// 获取或设置响应状态码过滤器
        /// 默认为200-299
        /// </summary>
        public Func<HttpStatusCode, bool> HttpStatusFilter { get; set; }

        /// <summary>
        /// 获取监控的网址集合
        /// </summary>
        public MonitorCollection Monitors { get; } = new MonitorCollection();

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        public List<INotifyChannel> NotifyChannels { get; } = new List<INotifyChannel>();


        /// <summary>
        /// 站点状态监控的配置项
        /// </summary>
        public WebOptions()
        {
            this.HttpStatusFilter = this.IsSuccessStatusCode;
        }

        /// <summary>
        /// 是否为正确的状态码
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        private bool IsSuccessStatusCode(HttpStatusCode httpStatusCode)
        {
            var httpStatus = (int)httpStatusCode;
            return httpStatus >= 200 && httpStatus <= 299;
        }
    }
}
