using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;





namespace Newtouch.Sett.Web.Areas.OutpatientManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("OutpatientManage")]

    public class RefundController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: BusinessManage/Refund

        private readonly IRefundApp _IRefundApp;

        public RefundController(IRefundApp IRefundApp)
        {
            _IRefundApp = IRefundApp;
        }

        /// <summary>
        /// danh sách hóa đơn
        /// </summary>
        /// <returns></returns>
        public IActionResult QueFpList()
        {
            return View();
        }


        /// <summary>
        /// tra số hóa đơn
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult btnQueryFph(string kh, string startTime, string endTime)
        { 

            var data = _IRefundApp.GetFPHByKh(kh, startTime, endTime, "");
            return Success("", data);
   
        }
        /// <summary>
        /// theo hóa đơn của quyết toán tra cứu theo mã nội bộ đăng ký khám ngoại trú khoản mục
        /// </summary>
        /// <param name="jsnm">quyết toán mã nội bộ</param>
        /// <returns></returns>
        public IActionResult GetMZJSByJsnm(int jsnm)
        {
 
            var data = _IRefundApp.GetMZJSByJsnm(jsnm);
            return Success("", data);
        }
        /// <summary>
        /// theo quyết toán tra theo mã nội bộ ngoại trú khám khoản mục
        /// </summary>
        /// <param name="jsnm">quyết toán mã nội bộ</param>
        /// <returns></returns>
        public IActionResult GetGridViewMxByJsnm(int jsnm)
        {
            var data = _IRefundApp.GetGridViewMx(jsnm);
            foreach (var da in data)
            {
                da.IS_RETURN = da.IS_RETURN + "," + da.czh;

            }
            return Success("", data);
        }

        /// <summary>
        /// hoàn phí
        /// </summary>
        public IActionResult btnReturn(string kh, int jsnm, string refundData)
        { 
            bool isReturnAll = true;
            List<GridViewMx> GridViewMx;
            try
            {
                GridViewMx = JsonConvert.DeserializeObject<List<GridViewMx>>(refundData);
            }
            catch (JsonException)
            {
                return Error("Dữ liệu gửi lên không đúng định dạng.");
            }
            //foreach (var da in GridViewMx)
            //{
            //    string[] sArray = da.IS_RETURN.Split(',');

            //    da.IS_RETURN = sArray[0];

            //    //thuận tiện khoản mục đăng ký khám cùng nhau đã chọn cùng hủy mà thiết lập của đặc biệt số thành nhóm
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
        //public IActionResult SaveData(object rowData)
        //{

        //   return Success("", rowData);
        //}
        /////*************************************
        /////
        //public IActionResult refund()
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
