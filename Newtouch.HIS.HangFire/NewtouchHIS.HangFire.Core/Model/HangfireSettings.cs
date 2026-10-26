namespace NewtouchHIS.HangFire.Core.Model
{
    public class HangfireSettings
    {
        public string ServerName { get; set; }
        public string TablePrefix { get; set; }
        public string StartUpPath { get; set; }
        public string ReadOnlyPath { get; set; }
        public List<string> JobQueues { get; set; }
        public HttpAuthInfo HttpAuthInfo { get; set; } = new HttpAuthInfo();
        public int WorkerCount { get; set; } = 40;
        public bool DisplayStorageConnectionString { get; set; } = false;
        public JobAppAPIHost AppAPIHost { get; set; }
    }
    /// <summary>
    /// cấu hình giao diện các hệ thống
    /// </summary>
    public class JobAppAPIHost
    {
        public string JobHost { get; set; }
        /// <summary>
        /// trung tâm giao diện
        /// </summary>
        public string? SiteApiManageHost { get; set; }
        /// <summary>
        /// giao diện trung tâm tin nhắn
        /// </summary>
        public string? SiteNoticeCenterHost { get; set; }
    }
}
