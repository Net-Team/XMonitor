using System;

namespace XMonitor.WebSite
{
    /// <summary>
    /// 表示监控的网站集合
    /// </summary>
    public class MonitorCollection : MonitorCollection<WebSiteMonitor>
    {
        /// <summary>
        /// 添加监控网站
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="uri">网址</param>
        public void Add(string alias, Uri uri)
        {
            var monitor = new WebSiteMonitor
            {
                Alias = alias,
                Uri = uri
            };
            base.Add(monitor);
        }
    }
}