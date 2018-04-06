using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.Web
{

    /// <summary>
    /// 表示监控的站点集合
    /// </summary>
    public class WebMonitorCollection : MonitorCollection
    {
        /// <summary>
        /// 添加监控站点
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="uri">网址</param>
        /// <param name="options">监控选项</param>
        public void Add(string alias, Uri uri, WebOptions options)
        {
            var monitor = new WebMonitor(alias, uri, options);
            base.Add(monitor);
        }
    }
}
