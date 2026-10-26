using Microsoft.AspNetCore.Mvc.Filters;

namespace NewtouchHIS.Lib.Framework.Filter
{
    /// <summary>
    /// MVClớp cơ sở bộ lọc
    /// </summary>
    public abstract class ActionFilterAttributeBase : ActionFilterAttribute
    {
        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public ActionFilterAttributeBase()
        {

        }

        /// <summary>
        /// đối tượng yêu cầu
        /// </summary>
        //public HttpRequest request
        //{
        //    get
        //    {
        //        return request;
        //    }
        //}

        /// <summary>
        /// giá trị định tuyến
        /// </summary>
        //public RouteValueDictionary routeValues
        //{
        //    get
        //    {
        //        return this.routeValues;

        //    }
        //}

        /// <summary>
        /// lấy trong định tuyếnIntgiá trị
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dftVal">giá trị mặc định0</param>
        /// <returns></returns>
        public int GetRouteIntVal(string name, int dftVal = 0)
        {
            if (string.IsNullOrEmpty(name)) return dftVal;
            return this.GetRouteIntVal(name);
            //object rVal = null;
            //if (rVal == null)
            //    rVal = this.request.Params[name];
            //if (rVal == null)
            //    rVal = this.GetRouteIntVal[name];
            //if (rVal != null)
            //{
            //    int.TryParse(rVal.ToString(), out dftVal);
            //}
            //return dftVal;
        }

        /// <summary>
        /// lấy trong định tuyếnStringgiá trị
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dftVal">giá trị mặc địnhnull</param>
        /// <returns></returns>
        public string GetRouteStringVal(string name, string dftVal = null)
        {
            return this.GetRouteStringVal(name);
            //if (string.IsNullOrEmpty(name)) return (dftVal ?? string.Empty);
            //object rVal = null;
            //if (rVal == null)
            //    rVal = request.Params[name];
            //if (rVal == null)
            //    rVal = routeValues[name];
            //if (rVal != null)
            //{
            //    return rVal.ToString();
            //}
            //return dftVal;
        }

    }
}
