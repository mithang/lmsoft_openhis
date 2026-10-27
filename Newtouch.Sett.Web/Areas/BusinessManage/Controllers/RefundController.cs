using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.BusinessManage.Controllers
{

    [Area("BusinessManage")]

    public class RefundController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: BusinessManage/Refund

        private IRefundApp _IRefundApp;

        public RefundController(IRefundApp RefundApp)
        {
            this._IRefundApp = RefundApp;
        }
      
        /// <summary>
        /// traphátphiếusố
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        public IActionResult btnQueryFph(string kh)
        {

            var data = _IRefundApp.GetFPHByKh(kh, "", "", "");
            if (data.ToList().Count > 0)
            {
                
                return Success("", data);
            }
            else
            {
                return Error("tra tìm không đến Thông tin quyết toán！");
            }

        }
        /// <summary>
        /// căndữ liệuphátphiếucủakếttínhtrongmãtratra cứungoại trúkhámtreosốhạngmục
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <returns></returns>
        public IActionResult GetMZJSByJsnm(int jsnm)
        {
            if (jsnm == 0)
            {
                return Error("Dữ liệu chưa đầy đủ，Vui lòng xác nhận");
            }
            var data = _IRefundApp.GetMZJSByJsnm(jsnm);
            return Success("", data);

        }
        /// <summary>
        /// theokếttínhtrongmãtrangoại trúkhámhạngmục
        /// </summary>
        /// <param name="jsnm">Mã nội bộ quyết toán</param>
        /// <returns></returns>
        public IActionResult GetGridViewMxByJsnm(int jsnm)
        {
            var data = GetGridViewMx(jsnm);
            //var test = new List<RefundVO>();
            //data.ForEach(p => { p.IS_RETURN = false; test.Add(p); });
            return Success("", data);
        }
        public List<GridViewMx> GetGridViewMx(int jsnm)
        {
            return _IRefundApp.GetGridViewMx(jsnm);
        }
        /// <summary>
        /// Hoàn phí
        /// </summary>
        public IActionResult btnReturn(string kh,List<GridViewMx> GridViewMx, int jsnm)
        {
            if (jsnm == 0)
            {
                return Error("Dữ liệu chưa đầy đủ，Vui lòng xác nhận");
            }
            var result = _IRefundApp.btnReturn(kh, GridViewMx, jsnm, out bool isReturnAll);

             return Success("", result);
        }
     
  
    }
}
