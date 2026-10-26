using Newtouch.HIS.Web.Core.ActionFilters;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab
{
    /// <summary>
    /// Đăng ký bộ lọc toàn cục 
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Thống kê truy cập hệ thống và nhật ký theo dõi 
            filters.Add(new StatisticsTrackerAttribute());

            //Bắt bất thường toàn cục 
            filters.Add(new HandlerErrorAttribute());


        }
    }

}