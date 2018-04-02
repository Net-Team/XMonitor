using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMonitor.Exceptions;

namespace WebSiteMonitor
{
    public static class ServiceCollectionExtend
    {
        public static ServiceCollection AddExceptionService(this ServiceCollection services, Action<Options> options)
        {
            var opt = new Options();
            options?.Invoke(opt);

            var service = new ExceptionService(opt);
            services.Add(service);
            return services;
        }
    }
}
