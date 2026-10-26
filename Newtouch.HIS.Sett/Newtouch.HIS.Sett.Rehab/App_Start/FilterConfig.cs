using Newtouch.HIS.Web.Core.ActionFilters;
using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab
{
    /// <summary>
    /// 全局 qua 滤 thiết bị đ ăng ký 
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //Hệ thống truy cập thống kê và 踪 nhật ký 
            filters.Add(new StatisticsTrackerAttribute());

            //全局bất thư ờng bắt 
            filters.Add(new HandlerErrorAttribute());


        }
    }

}