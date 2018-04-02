using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMonitor.Exceptions.ExceptionNotify
{
    class EmailNotifyChannel : INotifyChannel
    {
        private readonly EmailNotifyChannelOptions opt;

        public EmailNotifyChannel(EmailNotifyChannelOptions opt)
        {
            this.opt = opt;
        }

        public Task NotifyAsync(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
