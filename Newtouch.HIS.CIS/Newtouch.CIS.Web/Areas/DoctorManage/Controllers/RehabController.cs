using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Compat;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class RehabController : OrgControllerBase
	{
        private readonly IDoctorserviceDmnService _iDoctorserviceDmnService;

        public RehabController(IDoctorserviceDmnService iDoctorserviceDmnService)
        {
            this._iDoctorserviceDmnService = iDoctorserviceDmnService;
        }
        public IActionResult GetSfxmZxksSelectJson(string sfxmCode)
        {
            var Zxks = _iDoctorserviceDmnService.GetSfxmZxksSelectJson(OrganizeId);
            return Content(Zxks.ToJson());
        }
        public IActionResult GetSfxmZxksSelectJson2(string sfxmCode, string keyword)
       {
            var Zxks = _iDoctorserviceDmnService.GetSfxmZxksSelectJson(OrganizeId, keyword);
            return Content(Zxks.ToJson());
        }
    }
}