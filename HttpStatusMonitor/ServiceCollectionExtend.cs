using Services;
using System;

namespace HttpStatusMonitor
{
    /// <summary>
    ///  ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtend
    {
        /// <summary>
        /// 添加http状态码监控服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">配置选项</param>
        /// <returns></returns>
        public static ServiceCollection AddHttpStatusMonitor(this ServiceCollection services, Action<Options> options)
        {
            var opt = new Options();
            options?.Invoke(opt);

            var service = new HttpStatusService(opt);
            services.Add(service);
            return services;
        }
    }
}
