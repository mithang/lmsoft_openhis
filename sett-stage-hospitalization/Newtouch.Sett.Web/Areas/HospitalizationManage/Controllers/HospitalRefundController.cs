using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.HospitalizationManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("HospitalizationManage")]
    public class HospitalRefundController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IHospitalRefundApp _hospitalRefundApp;
        public HospitalRefundController(IHospitalRefundApp hospitalRefundApp)
        {
            _hospitalRefundApp = hospitalRefundApp;
        }


        /// <summary>
        /// Nạp trẻ sơ sinh 
        /// </summary>
        /// <param name="zyh">Mã nhập viện</param>
        /// <returns>Họ tên Số trẻ sơ sinh</returns>
        //public DataTable GetYehByZyh(string zyh)
        //{

        //    DataTable yeh = _hospitalRefundApp.GetYehByZyh(zyh);
        //    return yeh;
        //}
        /// <summary>
        /// Lấy thông tin bệnh nhân theo mã nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult GetPatInfoByZyh(string zyh)
        {
         
            var name= _hospitalRefundApp.GetPatInfoByZyh(zyh);
            return Success("", name);
        }

        /// <summary>
        /// Truy vấn
        /// </summary>

        public IActionResult BtnQuery( string zyh, string brxm, string yrh, string sfks, string zxks, string xmmc)
       {
            var data = new List<GridZyXmYpjfb>();
            if (string.IsNullOrWhiteSpace(zyh.Trim()))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
              
            }
            data = _hospitalRefundApp.GetZyXmjfbGroup(zyh, zxks, xmmc, yrh);        //Mục

            //Cấu hình hoàn phí nhập viện：Có bao gồm thuốc hay không 0 Không bao gồm thuốc


            /// Lấy thông tin cấu hình theo mã

            var pzInfo = _hospitalRefundApp.GetPZInfo();
            if (pzInfo == null)
            {
                var msg = "Hệ thống chưa cấu hình số hiệu：" + Constants.xtmzpz.ZYTFPZ_INCLUDEYP + " của cấu hình in hệ thống";
                return Error(msg);  
            }

            if (pzInfo.ToString() != "0")//0 Không bao gồm thuốc
            {
                var ypdata = new List<GridZyXmYpjfb>();
                ypdata = _hospitalRefundApp.GetZyYpjfbGroup(zyh, zxks, xmmc, yrh);        //Thuốc
                data.AddRange(ypdata);
            }
             
            return Success("", data);
        }

        /// <summary>
        /// Lấy chi tiết theo mục đã chọn
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="xsebh"></param>
        /// <param name="sfxmbh"></param>
        /// <param name="tdrq"></param>
        /// <param name="is_sfxm"></param>
        /// <returns></returns>

        public IActionResult Detail(string zyh, string xsebh, string sfxm, string CreateTime, string is_sfxm)
        {
           
            var data= LoadZyXmYpItemsjfb(zyh, xsebh, sfxm, CreateTime, is_sfxm);
            return Success("", data);
        }
        private List<gridZyXmYpItemsjfb> LoadZyXmYpItemsjfb(string zyh, string xsebh, string sfxm, string CreateTime, string is_sfxm)
        {
            var data = new List<gridZyXmYpItemsjfb>();
            if (is_sfxm == "0")
            {
                data = _hospitalRefundApp.GetZyYpjfbList(zyh, xsebh, sfxm, CreateTime); //thu phí thuốc
            }
            else
            {
                data = _hospitalRefundApp.GetZyXmjfbList(zyh, xsebh, sfxm, CreateTime); //thu phí mục
            }

            return data;
        }
        /// <summary>
        /// Hoàn phí
        /// </summary>
        /// <param name="cxzyjfbbh">Số hiệu</param>
        /// <param name="is_sfxm">Có phải thuốc hay không</param>
        /// <param name="refundNum">Số lượng hoàn</param>
        /// <returns></returns>
        public IActionResult btnTuiFei(string cxzyjfbbh, string is_sfxm,int refundNum)
        {
            string result = "Hoàn phí thất bại";
            Dictionary<string, decimal> returnDict = new Dictionary<string, decimal>();
            returnDict.Add(cxzyjfbbh, refundNum);
            if (is_sfxm == "0")
            {
                //thu phí thuốc                            
                result = _hospitalRefundApp.SaveReturnZyyp(returnDict);
            }
            else
            {
                //thu phí mục
                result = _hospitalRefundApp.SaveReturnZyxm(returnDict);
            }
         return Success("", result) ;
        }

    }
}
