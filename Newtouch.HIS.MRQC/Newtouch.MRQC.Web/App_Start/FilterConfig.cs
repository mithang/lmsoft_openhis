using Newtouch.HIS.Web.Core.ActionFilters;
using System.Web.Mvc;

namespace Newtouch.MRQC.Web
{
    /// <summary>
    /// Đăng ký bộ lọc toàn cục
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Đăng ký bộ lọc toàn cục
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //nhật ký theo dõi thống kê truy cập hệ thống
            filters.Add(new StatisticsTrackerAttribute());

            //bắt ngoại lệ toàn cục
            filters.Add(new HandlerErrorAttribute());


        }
    }

}