using System;
using XMonitor.DriveInfo;

namespace XMonitor.Core
{
    /// <summary>
    /// 磁盘监控集合扩展
    /// </summary>
    public static class MonitorCollectionExtend
    {
        /// <summary>
        /// 添加所有磁盘监控
        /// </summary>
        /// <param name="monitors">监控集合</param>      
        /// <param name="options">配置选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static MonitorCollection AddDriveInfosMonitor(this MonitorCollection monitors, Action<DeriveInfoOptions> options)
        {
            var drives = System.IO.DriveInfo.GetDrives();
            foreach (var item in drives)
            {
                monitors.AddDriveInfoMonitor(item.Name, item.Name, options);
            }
            return monitors;
        }

        /// <summary>
        /// 添加磁盘监控
        /// </summary>
        /// <param name="monitors">监控集合</param>
        /// <param name="alias">磁盘别名</param>
        /// <param name="driveName">磁盘名称，a到z</param>
        /// <param name="options">配置选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static MonitorCollection AddDriveInfoMonitor(this MonitorCollection monitors, string alias, string driveName, Action<DeriveInfoOptions> options)
        {
            var opt = new DeriveInfoOptions();
            options?.Invoke(opt);

            var service = new DriveInfoMonitor(opt, alias, driveName);
            monitors.Add(service);
            return monitors;
        }
    }
}
