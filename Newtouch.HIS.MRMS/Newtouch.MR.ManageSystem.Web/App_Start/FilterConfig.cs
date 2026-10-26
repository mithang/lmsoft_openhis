using Newtouch.HIS.Web.Core.ActionFilters;
using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web
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
            //Theo dõi thống kê truy cập hệ thốngNhật ký
            filters.Add(new StatisticsTrackerAttribute());

            //Bắt ngoại lệ toàn cục
            filters.Add(new HandlerErrorAttribute());


        }
    }

}