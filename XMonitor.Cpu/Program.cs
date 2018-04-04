using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace XMonitor.Cpu
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory = new PerformanceCounter("Memory", "Available MBytes");  //可用内存计算正确;
            var cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            while (true)
            {

                Console.WriteLine("---------------- 内存使用情况 ----------------");
                Console.WriteLine("CPU 使用率：{0}", cpu.NextValue());
                Console.WriteLine("总共内存：{0} MB", GetPhisicalMemory());
                Console.WriteLine("剩余内存：{0} MB", memory.NextValue());
                Console.WriteLine("当前硬件大小：{0}", GetDiskSize());
                GetDriveSize();
                Thread.Sleep(5000);
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        private static int GetPhisicalMemory()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象 
            searcher.Query = new SelectQuery("Win32_PhysicalMemory ", "", new string[] { "Capacity" });//设置查询条件 
            ManagementObjectCollection collection = searcher.Get();   //获取内存容量 
            ManagementObjectCollection.ManagementObjectEnumerator em = collection.GetEnumerator();

            long capacity = 0;
            while (em.MoveNext())
            {
                ManagementBaseObject baseObj = em.Current;
                if (baseObj.Properties["Capacity"].Value != null)
                {
                    try
                    {
                        capacity += long.Parse(baseObj.Properties["Capacity"].Value.ToString());
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return (int)(capacity / 1024 / 1024);
        }

        /// <summary>
        /// 获取硬盘容量
        /// </summary>
        private static string GetDiskSize()
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                string hdId = string.Empty;
                ManagementClass hardDisk = new ManagementClass("win32_DiskDrive");
                ManagementObjectCollection hardDiskC = hardDisk.GetInstances();
                foreach (ManagementObject m in hardDiskC)
                {
                    long capacity = Convert.ToInt64(m["Size"].ToString());
                    sb.Append(ToGB(capacity, 1000.0) + "+");
                }
                result = sb.ToString().TrimEnd('+');
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// 获取每个盘剩余空间
        /// </summary>
        private static void GetDriveSize()
        {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                if (drive.DriveType == System.IO.DriveType.Fixed)
                {
                    Console.WriteLine("{0} 盘剩余：{1}", drive.Name, ToGB(drive.TotalFreeSpace, 1024));
                }

            }
        }

        /// <summary>  
        /// 将字节转换为GB
        /// </summary>  
        /// <param name="size">字节值</param>  
        /// <param name="mod">除数，硬盘除以1000，内存除以1024</param>  
        /// <returns></returns>  
        private static string ToGB(double size, double mod)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size, 2) + units[i];
        }
    }
}
