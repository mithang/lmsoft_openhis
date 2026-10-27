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
        /// trasố hóa đơn
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult btnQueryFph(string kh, string startTime, string endTime)
        { 

            var data = _IRefundApp.GetFPHByKh(kh, startTime, endTime, "");
            return Success("", data);
   
        }
        /// <summary>
        /// theo hóa đơncủaquyết toántra cứu theo mã nội bộđăng ký khám ngoại trúkhoản mục
        /// </summary>
        /// <param name="jsnm">quyết toánmã nội bộ</param>
        /// <returns></returns>
        public IActionResult GetMZJSByJsnm(int jsnm)
        {
 
            var data = _IRefundApp.GetMZJSByJsnm(jsnm);
            return Success("", data);
        }
        /// <summary>
        /// theoquyết toántra theo mã nội bộngoại trúkhámkhoản mục
        /// </summary>
        /// <param name="jsnm">quyết toánmã nội bộ</param>
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
            var GridViewMx = JsonConvert.DeserializeObject<List<GridViewMx>>(refundData);
            //foreach (var da in GridViewMx)
            //{
            //    string[] sArray = da.IS_RETURN.Split(',');

            //    da.IS_RETURN = sArray[0];

            //    //thuận tiệnkhoản mục đăng ký khámcùng nhauđã chọncùng hủy màthiết lậpcủađặc biệtsố thành nhóm
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
