using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Application.Interface.BusinessManage;
using Newtouch.HIS.Domain.ValueObjects;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RefundController : ControllerBase
    {
        // GET: BusinessManage/Refund

        private readonly IRefundApp _IRefundApp;

        /// <summary>
        /// Danh sách hóa đơn
        /// </summary>
        /// <returns></returns>
        public ActionResult QueFpList()
        {
            return View();
        }


        /// <summary>
        /// Tra số hóa đơn
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public ActionResult btnQueryFph(string kh, string startTime, string endTime)
        { 

            var data = _IRefundApp.GetFPHByKh(kh, startTime, endTime, "");
            return Success("", data);
   
        }
        /// <summary>
        /// Tra cứu khoản mục đăng ký khám ngoại trú theo mã nội bộ quyết toán của hóa đơn
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <returns></returns>
        public ActionResult GetMZJSByJsnm(int jsnm)
        {
 
            var data = _IRefundApp.GetMZJSByJsnm(jsnm);
            return Success("", data);
        }
        /// <summary>
        /// Tra cứu khoản mục ngoại trú theo mã nội bộ quyết toán
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <returns></returns>
        public ActionResult GetGridViewMxByJsnm(int jsnm)
        {
            var data = _IRefundApp.GetGridViewMx(jsnm);
            foreach (var da in data)
            {
                da.IS_RETURN = da.IS_RETURN + "," + da.czh;

            }
            return Success("", data);
        }

        /// <summary>
        /// Hoàn phí
        /// </summary>
        public ActionResult btnReturn(string kh, int jsnm, string refundData)
        { 
            bool isReturnAll = true;
            var GridViewMx = Tools.Json.ToList<GridViewMx>(refundData);
            //foreach (var da in GridViewMx)
            //{
            //    string[] sArray = da.IS_RETURN.Split(',');

            //    da.IS_RETURN = sArray[0];

            //    //Mã nhóm đặc biệt được đặt để các khoản mục đăng ký khám có thể được chọn và hủy cùng nhau
            //    if (sArray[1]== "gh0209")
            //    {
            //        da.czh = "0";
            //    }

            //}
            var result = _IRefundApp.btnReturn(kh, GridViewMx, jsnm, out isReturnAll);
            if (result)
            {
                return Success("", isReturnAll);
            }
            else
            {
                throw new FailedCodeException("HOSP_REFUNG_FAIL");

            }
        }
        //public ActionResult SaveData(object rowData)
        //{

        //   return Success("", rowData);
        //}
        /////*************************************
        /////
        //public ActionResult refund()
        //{

        //    return View();
        //}
        //public string  refundData()
        //{

        //    var data = _IRefundApp.GetGridViewMx(225);

        //    return Tools.Json.ToString(data);
        //}
        //public string EditSave(string song)
        //{
        //    var show = Tools.Json.ToList<GridViewMx>(song);
        //    return "OK";

        //}
        //public void EditSave()
        //{


        //}
    }
}