using System;
using System.IO;

namespace Newtouch.Infrastructure.Log
{
    /// <summary>
    /// 日志组件
    /// </summary>
    public class AppLogger
    {
        public static readonly AppLogger Instance = new AppLogger();
        public void Error(string title, Exception ex, string message = "") => LogCore.Error(title, ex, message);
        public static void WriteEntityChangeRecordLog(object oldEntity, object newEntity, string table, string key) { /* audit-log stub: no collector in this deployment */ }
        public static void Info(string message)
        {
            try
            {
                var root = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pds-log", "log_yibao", "PDS");
                var date = DateTime.Now.ToString("yyyyMMddHHmm");
                var dirPath = string.Format("{0}\\{1}", root, date.Substring(0, 8));
                System.IO.Directory.CreateDirectory(dirPath);
                var filePath = System.IO.Path.Combine(dirPath, date.Substring(8, 2) + ".txt");
                File.AppendAllText(filePath, string.Format("\r\n\r\n{0}.Info.{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
            }
            catch { }
        }
    }
}