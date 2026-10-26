using Newtouch.HIS.Web.Core.ActionFilters;
using System.Web.Mvc;

namespace Newtouch.HIS.Web
{
    /// <summary>
    /// toàn cục bộ lọc đăng ký
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 
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