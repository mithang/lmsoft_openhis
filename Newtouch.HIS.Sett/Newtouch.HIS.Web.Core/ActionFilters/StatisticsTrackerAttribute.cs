using Newtouch.Common.Exceptions;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;
using Newtouch.Common.Attributes;
using Newtouch.Common.Operator;
using Newtouch.Common;

namespace Newtouch.HIS.Web.Core.ActionFilters
{
    /// <summary>
    /// Thống kê truy cập hệ thống và nhật ký theo dõi 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class StatisticsTrackerAttribute : ActionFilterAttributeBase
    {
        private readonly string Key = "_thisOnActionMonitorLog_";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(false).Any(p => p is TrackerIgnoreAttribute))
            {
                return;
            }

            filterContext.Controller.ViewData[Key] = DateTime.Now;

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes(false).Any(p => p is TrackerIgnoreAttribute))
            {
                return;
            }

            var enterTime = filterContext.Controller.ViewData[Key] as DateTime?;

            if (enterTime.HasValue)
            {
                // bản ghi truy cập nhật ký 
                var areaName = filterContext.RouteData.Values["area"] as string;
                var controllerName = filterContext.RouteData.Values["controller"] as string;
                var actionName = filterContext.RouteData.Values["action"] as string;
                var costMSec = (DateTime.Now - enterTime.Value).Milliseconds;
                var ip = "";
                var isAjax = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                //Mã trạng thái phản hồi 
                int responseStatusCode = 200;
                string responseSubCode = null;
                string responseSubMsg = null;
                if (filterContext.Exception != null)
                {
                    if (filterContext.Exception is FailedException)
                    {
                        //Là nghiệp vụ nhắc nhở
                        responseStatusCode = -200;
                        responseSubCode = ((FailedException)filterContext.Exception).Code;
                        if (!string.IsNullOrWhiteSpace(responseSubCode))
                        {
                            responseSubCode = responseSubCode.ToUpper();
                        }
                        responseSubMsg = ((FailedException)filterContext.Exception).Msg;
                    }
                    else
                    {
                        responseStatusCode = -500;  //Có bất thư ờng
                    }
                }

                NameValueCollection requestQueryString = null, requestForm = null;
                RouteValueDictionary routeDataCollections = null;
                requestQueryString = filterContext.HttpContext.Request.QueryString; //Url Tham số
                routeDataCollections = filterContext.RouteData.Values;
                if (!filterContext.ActionDescriptor.GetCustomAttributes(false).Any(p => p is TrackerRequestFormIgnoreAttribute))
                {
                    requestForm = filterContext.HttpContext.Request.Form;   //form biểu mẫu gửi của dữ liệu
                }

                // Định danh người dùng đăng nhập
                var op = OperatorProvider.GetCurrent();
                string userIdentity = op != null ? op.UserCode : null;

                var str = Json.ToJson(new
                {
                    Id = Guid.NewGuid().ToString(),
                    AreaName = areaName,
                    ControllerName = controllerName,
                    ActionName = actionName,
                    EnterTime = enterTime,
                    CostMSec = costMSec,
                    IP = ip,
                    IsAjax = isAjax,
                    RequestQueryString = toDictionary(requestQueryString), //Url Tham số
                    RequestForm = toDictionary(requestForm),   //form biểu mẫu gửi của dữ liệu
                    RouteDataCollections = toDictionary(routeDataCollections),
                    ResponseStatusCode = responseStatusCode,
                    ResponseSubCode = responseSubCode,
                    ResponseSubMsg = responseSubMsg,
                    UserIdentity = userIdentity,
                });

                AppLogger.Instance.Moniter(string.Format(@"
-statisticstrackerlog-S----------------##---------------------
{0}
-statisticstrackerlog-E----------------##---------------------
                    ", str));

                if (str.Length > 1000)
                {
                    AppLogger.Instance.Warn("存在比较大的Monitor记录，请核查");
                }
            }

            base.OnActionExecuted(filterContext);
        }


        #region private methods

        /// <summary>
        /// Lấy tham số định tuyến
        /// </summary>
        /// <param name="rDict"></param>
        /// <returns></returns>
        private Dictionary<string, object> toDictionary(RouteValueDictionary rDict)
        {
            Dictionary<string, object> dict = null;
            if (rDict != null)
            {
                foreach (var i in rDict)
                {
                    if (i.Key != "controller" && i.Key != "action" && i.Key != "area" && i.Value != null
                        && (i.Value.GetType() == typeof(string) || i.Value.GetType() == typeof(int) || i.Value.GetType() == typeof(bool))
                    )
                    {
                        dict = dict ?? new Dictionary<string, object>();
                        dict.Add(i.Key, i.Value.ToString());
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// LấyPost HoặcGet Tham số
        /// </summary>
        /// <param name="collections"></param>
        /// <returns></returns>
        private Dictionary<string, object> toDictionary(NameValueCollection collections)
        {
            Dictionary<string, object> dict = null;
            if (collections != null && collections.Count > 0)
            {
                dict = new Dictionary<string, object>();
                foreach (string key in collections.Keys)
                {
                    dict.Add(key, collections[key]);
                }
            }
            return dict;
        }

        #endregion

    }

}
