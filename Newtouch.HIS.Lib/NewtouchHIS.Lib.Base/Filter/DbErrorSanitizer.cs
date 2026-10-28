using System;

namespace NewtouchHIS.Lib.Base.Filter
{
    /// <summary>QA PHASE 2 (API-13/CTR-07): lỗi mất kết nối DB không được rò ra
    /// client (provider error + ConfigId + tiếng Trung của lớp DB). Dùng chung
    /// cho AppResultFilter (cả 2 host Center) và UseNewtouchExceptionHandler
    /// (8016). Chỉ lỗi DB mới bị thay bằng thông điệp chung; các lỗi khác giữ
    /// nguyên message gọi vào.</summary>
    public static class DbErrorSanitizer
    {
        public const string GenericDbMessage = "Không kết nối được cơ sở dữ liệu, vui lòng liên hệ quản trị viên";

        public static bool IsDbError(Exception ex)
        {
            for (var e = ex; e != null; e = e.InnerException)
            {
                var typeName = e.GetType().FullName ?? "";
                var msg = e.Message ?? "";
                if (typeName.Contains("Sql") || e is System.Data.Common.DbException
                    || msg.Contains("ConfigId=") || msg.Contains("network-related")
                    || msg.Contains("instance-specific") || msg.Contains("连接数据库"))
                {
                    return true;
                }
            }
            return false;
        }

        public static string Sanitize(Exception ex, string originalMessage)
        {
            return IsDbError(ex) ? GenericDbMessage : originalMessage;
        }
    }
}
