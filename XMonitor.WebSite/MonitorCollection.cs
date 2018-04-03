using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.WebSite
{
    /// <summary>
    /// 表示监控的网站集合
    /// </summary>
    public class MonitorCollection : IEnumerable<WebSiteMonitor>
    {
        /// <summary>
        /// 监控的网站列表
        /// </summary>
        private readonly List<WebSiteMonitor> monitors = new List<WebSiteMonitor>();

        /// <summary>
        /// 监控网站的数量
        /// </summary>
        public int Count => this.monitors.Count;

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
            this.monitors.Add(monitor);
        }

        /// <summary>
        /// 返回监控网站迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<WebSiteMonitor> GetEnumerator()
        {
            return this.monitors.GetEnumerator();
        }

        /// <summary>
        /// 返回监控网站迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}