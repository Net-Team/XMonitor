using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMonitor.Exceptions
{
    class ExceptionService : IService
    {
        private readonly  Options options;

        public ExceptionService (Options options)
        {
            this.options = options;
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }
    }
}
