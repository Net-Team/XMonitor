using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSiteMonitor.Exceptions.ExceptionNotify;

namespace WebSiteMonitor.Exceptions
{
    public class Options
    {
        private readonly List<INotifyChannel> notifyChannels = new List<INotifyChannel>();

        public List<HttpStatusCode> SuccessStatusCodes => new List<HttpStatusCode>();

        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        public Options()
        {
        }

        public void UseEmailNotifyChannel(Action<EmailNotifyChannelOptions> options)
        {
            var opt = new EmailNotifyChannelOptions();
            options?.Invoke(opt);

            var channel = new EmailNotifyChannel(opt);
            this.notifyChannels.Add(channel);
        }
    }
}
