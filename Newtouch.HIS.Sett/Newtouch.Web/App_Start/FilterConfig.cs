using Newtouch.HIS.Web.Core.ActionFilters;
using System.Web.Mvc;

namespace Newtouch.HIS.Web
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
            //系统访问统计跟踪日志
            filters.Add(new StatisticsTrackerAttribute());

            //Bắt bất thường toàn cục
            filters.Add(new HandlerErrorAttribute());
            filters.Add(new VerificationActionFIlterAttribute());

        }
    }

}