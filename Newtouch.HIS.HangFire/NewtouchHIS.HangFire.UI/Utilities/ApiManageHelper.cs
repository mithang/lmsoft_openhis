using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.HangFire.UI.Utilities
{
    public static class ApiManageHelper
    {
        /// <summary>
        /// trung tâm giao diệnHost
        /// </summary>
        public static string ApiManageHost
        {
            get
            {
                var host = ConfigInitHelper.SysConfig?.AppAPIHost?.SiteApiManageHost;
                if (string.IsNullOrWhiteSpace(host))
                {
                    throw new FailedException("接口中心初始化异常：请检查配置");
                }
                return host;
            }
        }
        /// <summary>
        /// danh sách tin nhắn đang chờ xử lý
        /// </summary>
        public static string NoticeImmediatelyWaitProcApi = $"{ApiManageHost}/api/NoticeJob/NoticeImmediatelyWaitProc";
        /// <summary>
        /// danh sách tin nhắn chưa đọc quá hạn
        /// </summary>
        public static string NoticeUnReadProcApi = $"{ApiManageHost}/api/NoticeJob/NoticeUnReadProc";
        /// <summary>
        /// xử lý tin nhắn thành công
        /// </summary>
        public static string NoticeProcSuccessApi = $"{ApiManageHost}/api/NoticeJob/NoticeProcSuccess";
        /// <summary>
        /// xử lý tin nhắn thất bại
        /// </summary>
        public static string NoticeProcFailedApi = $"{ApiManageHost}/api/NoticeJob/NoticeProcFailed";
        /// <summary>
        /// gửi tin nhắn thành công
        /// </summary>
        public static string NoticeSendSuccessApi = $"{ApiManageHost}/api/NoticeJob/NoticeSendSuccess";
        /// <summary>
        /// gửi tin nhắn thất bại
        /// </summary>
        public static string NoticeSendFailedApi = $"{ApiManageHost}/api/NoticeJob/NoticeSendFailed";

    }
}
