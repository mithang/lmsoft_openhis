using Newtouch.HIS.Application;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Web.Areas.HospitalizationManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HospitalRefundController : ControllerBase
    {
        private readonly IHospitalRefundApp _hospitalRefundApp;

        /// <summary>
        /// Tải thông tin trẻ sơ sinh 
        /// </summary>
        /// <param name="zyh">Số nhập viện</param>
        /// <returns>Họ tên Mã trẻ sơ sinh</returns>
        //public DataTable GetYehByZyh(string zyh)
        //{

        //    DataTable yeh = _hospitalRefundApp.GetYehByZyh(zyh);
        //    return yeh;
        //}
        /// <summary>
        /// Lấy thông tin bệnh nhân theo số nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult GetPatInfoByZyh(string zyh)
        {
         
            var name= _hospitalRefundApp.GetPatInfoByZyh(zyh);
            return Success("", name);
        }

        /// <summary>
        /// Tra cứu
        /// </summary>

        public ActionResult BtnQuery( string zyh, string brxm, string yrh, string sfks, string zxks, string xmmc)
       {
            var data = new List<GridZyXmYpjfb>();
            if (string.IsNullOrWhiteSpace(zyh.Trim()))
            {
                throw new FailedCodeException("HOSP_ZYH_IS_EMPTY");
              
            }
            data = _hospitalRefundApp.GetZyXmjfbGroup(zyh, zxks, xmmc, yrh);        //Khoản mục

            //Cấu hình hoàn phí nội trú：Có bao gồm thuốc hay không 0 Không bao gồm thuốc


            /// Lấy thông tin cấu hình theo mã

            var pzInfo = _hospitalRefundApp.GetPZInfo();
            if (pzInfo == null)
            {
                var msg = "系统未配置编号为：" + Constants.xtmzpz.ZYTFPZ_INCLUDEYP + " 的系统打印配置";
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

        public ActionResult Detail(string zyh, string xsebh, string sfxm, string CreateTime, string is_sfxm)
        {
           
            var data= LoadZyXmYpItemsjfb(zyh, xsebh, sfxm, CreateTime, is_sfxm);
            return Success("", data);
        }
        private List<gridZyXmYpItemsjfb> LoadZyXmYpItemsjfb(string zyh, string xsebh, string sfxm, string CreateTime, string is_sfxm)
        {
            var data = new List<gridZyXmYpItemsjfb>();
            if (is_sfxm == "0")
            {
                data = _hospitalRefundApp.GetZyYpjfbList(zyh, xsebh, sfxm, CreateTime); //Thu phí thuốc
            }
            else
            {
                data = _hospitalRefundApp.GetZyXmjfbList(zyh, xsebh, sfxm, CreateTime); //Thu phí khoản mục
            }

            return data;
        }
        /// <summary>
        /// Hoàn phí
        /// </summary>
        /// <param name="cxzyjfbbh">Mã số</param>
        /// <param name="is_sfxm">Có phải là thuốc hay không</param>
        /// <param name="refundNum">Số lượng trả lại</param>
        /// <returns></returns>
        public ActionResult btnTuiFei(string cxzyjfbbh, string is_sfxm,int refundNum)
        {
            string result = "退费失败";
            Dictionary<string, decimal> returnDict = new Dictionary<string, decimal>();
            returnDict.Add(cxzyjfbbh, refundNum);
            if (is_sfxm == "0")
            {
                //Thu phí thuốc                            
                result = _hospitalRefundApp.SaveReturnZyyp(returnDict);
            }
            else
            {
                //Thu phí khoản mục
                result = _hospitalRefundApp.SaveReturnZyxm(returnDict);
            }
         return Success("", result) ;
        }

    }
}