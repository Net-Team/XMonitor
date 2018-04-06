using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;
using XMonitor.Web;

namespace XMonitor.Core
{
    /// <summary>
    ///  ServiceCollection扩展
    /// </summary>
    public static class MonitorCollectionExtend
    {
        /// <summary>
        /// 添加http状态码监控服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="alias">网站名称</param>
        /// <param name="uri">网站地址</param>
        /// <param name="options">配置选项</param>
        /// <returns></returns>
        public static MonitorCollection UseWebMonitorService(this MonitorCollection services, string alias, Uri uri, Action<WebOptions> options)
        {
            var opt = new WebOptions();
            options?.Invoke(opt);

            var service = new WebMonitor(alias, uri, opt);
            services.Add(service);
            return services;
        }
    }
}
