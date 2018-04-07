using System;
using XMonitor.WebSite;

namespace XMonitor.Core
{
    /// <summary>
    /// 站点监控集合扩展
    /// </summary>
    public static class MonitorCollectionExtend
    {
        /// <summary>
        /// 添加站点监控
        /// </summary>
        /// <param name="monitors">监控集合</param>
        /// <param name="alias">站点名称</param>
        /// <param name="uri">站点地址</param>
        /// <param name="options">配置选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static MonitorCollection AddWebSiteMonitor(this MonitorCollection monitors, string alias, Uri uri, Action<WebSiteOptions> options)
        {
            var opt = new WebSiteOptions();
            options?.Invoke(opt);

            var service = new WebSiteMonitor(opt, alias, uri);
            monitors.Add(service);
            return monitors;
        }
    }
}
