using System;
using System.Net.Http;
using XMonitor.Core;

namespace XMonitor.WebSite
{
    /// <summary>
    /// 表示站点请求异常
    /// </summary>
    class WebSiteReqeustException : MonitorException
    {
        /// <summary>
        /// 站点监控异常
        /// </summary>
        /// <param name="uri">站点uri</param>
        /// <param name="inner">内部异常</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WebSiteReqeustException(Uri uri, HttpRequestException inner)
            : base(GetMessage(uri, inner), inner)
        {
        }

        /// <summary>
        /// 获取内部异常的消息
        /// </summary>
        /// <param name="uri">站点uri</param>
        /// <param name="inner">内部异常</param>
        /// <returns></returns>
        private static string GetMessage(Uri uri, HttpRequestException inner)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (inner == null)
            {
                throw new ArgumentNullException(nameof(inner));
            }
            return $"请求站点{uri}遇到问题：{inner.Message}";
        }
    }
}
