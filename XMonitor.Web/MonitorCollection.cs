using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.Web
{

    /// <summary>
    /// 表示监控的网站集合
    /// </summary>
    public class MonitorCollection : MonitorCollection<WebMonitorChecker>
    {
        /// <summary>
        /// 添加监控网站
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="uri">网址</param>
        public void Add(string alias, Uri uri, WebOptions options)
        {
            var monitor = new WebMonitor
            {
                Alias = alias,
                Uri = uri
            };
            var mnitorChecker = new WebMonitorChecker(monitor, options);
            base.Add(mnitorChecker);
        }
    }
}
