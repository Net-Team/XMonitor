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
    public static class ServiceCollectionExtend
    {
        /// <summary>
        /// 使用http状态码监控服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">配置选项</param>
        /// <returns></returns>
        public static ServiceCollection UseWebMonitorService(this ServiceCollection services, Action<WebOptions> options)
        {
            var opt = new WebOptions();
            options?.Invoke(opt);

            var service = new WebMonitorService(opt);
            services.Add(service);
            return services;
        }
    }
}
