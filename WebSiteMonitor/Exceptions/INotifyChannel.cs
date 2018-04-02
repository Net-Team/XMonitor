using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMonitor.Exceptions
{
    interface INotifyChannel
    {
        Task NotifyAsync(Exception ex);
    }
}
