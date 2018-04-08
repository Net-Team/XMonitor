using System;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.DriveInfo
{
    /// <summary>
    /// 表示磁盘监控对象
    /// </summary>
    class DriveInfoMonitor : Monitor<DeriveInfoOptions>
    {
        /// <summary>
        /// 获取磁盘名称
        /// A到Z的字母
        /// </summary>
        public string DriveName { get; private set; }

        /// <summary>
        /// 磁盘监控对象
        /// </summary>
        /// <param name="options">监控选项</param>
        /// <param name="alias">磁盘别名</param>
        /// <param name="driveName">磁盘名称A到Z的字母</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DriveInfoMonitor(DeriveInfoOptions options, string alias, string driveName)
            : base(options, alias, driveName)
        {
            this.DriveName = driveName;
        }

        /// <summary>
        /// 执行一次监控
        /// </summary>
        /// <returns></returns>
        protected override async Task OnCheckMonitorAsync()
        {
            var drive = new System.IO.DriveInfo(this.DriveName);
            if (drive.IsReady == false)
            {
                var ex = new MonitorException($"磁盘{this.DriveName}不存在");
                await this.NotifyAsync(ex);
            }
            else
            {
                var freeSpaceMB = drive.TotalFreeSpace / 1024 / 1024;
                if (freeSpaceMB < this.Options.MinFreeSpaceMB)
                {
                    var ex = new MonitorException($"磁盘{this.DriveName}的可用空间已经不足{this.Options.MinFreeSpaceMB}MB");
                    await this.NotifyAsync(ex);
                }
            }
        }
    }
}
