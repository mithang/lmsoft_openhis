using FrameworkBase.MultiOrg.Web;
using Newtouch.HIS.Application;
using System.Web.Mvc;
using Newtouch.HIS.Domain.DTO.OutputDto;
using System;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    public class AccoutManageController : OrgControllerBase
    {
        private readonly IAccountManageApp _accountApp;
        /// <summary>
        /// Tra cứu thông tin cơ bản bệnh nhân và Thông tin tài khoản
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public ActionResult PatAccountInfo(int patid,string zhxz)
        {
            ReserveDto dto=new ReserveDto();
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                dto= _accountApp.GetHosPatAccInfo(patid, zhxz);
            }
            else
            {
                dto = _accountApp.GetHosPatAccInfo(patid);
            }
            return Success("", dto);
        }

        public ActionResult GetZhyeByPatid(int patid, string zhxz = null)
        {
            if (!string.IsNullOrWhiteSpace(zhxz))
            {
                var zhInfo = _accountApp.GetZhyeByPatid(patid, Convert.ToInt16(zhxz));
                return Success(null, zhInfo);
            }
            else
            {
                var zhye = _accountApp.GetZhyeByPatid(patid);
                return Success(null, zhye);
            }

        }

    }
}