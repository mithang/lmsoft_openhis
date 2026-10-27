using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.MRQC.Domain;
using Newtouch.MRQC.Infrastructure;
using System;

namespace Newtouch.MRQC.Web.Areas.QualityControlManage.Controllers
{
    [Area("QualityControlManage")]
    public class ApplyController : OrgControllerBase
    {
        private readonly IMrApplyDmnService _mrapplyDmnservice;

        public ApplyController(IMrApplyDmnService mrapplyDmnservice)
        {
            _mrapplyDmnservice = mrapplyDmnservice;
        }

        public override IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Danh sách đơn xin bệnh án EMR
        /// </summary>
        public IActionResult GetBlApplyList(Pagination pagination, string applyStutas, DateTime ksrq, DateTime jsrq)
        {
            var data = new
            {
                rows = _mrapplyDmnservice.GetBlApplyList(pagination, this.OrganizeId, applyStutas, ksrq, jsrq),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Phê duyệt đơn xin (central API first, then local status update — same as original)
        /// </summary>
        public IActionResult ApprovalApply(string shbhlist)
        {
            var reqObj = new
            {
                Data = new
                {
                    ApplyNos = shbhlist,
                    ApprovePerson = this.UserIdentity.UserCode,
                    ApproveDept = this.UserIdentity.DepartmentCode,
                    ApproveStatus = (int)ApplyStatusEnum.ysp
                },
                AppId = AuthenManageHelper.appId,
                OrganizeId = OrganizeId,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            var outstr = AuthenManageHelper.HttpPost<bool>(reqObj.ToJson(), AuthenManageHelper.SiteConmonAPIHost + "api/MedicalApplyHandle/ModifyApplyApprove", this.UserIdentity);
            if (outstr.data == false)
            {
                return Error("Phê duyệt thất bại, vui lòng làm mới danh sách chờ phê duyệt");
            }
            _mrapplyDmnservice.UpdateApplyStatus(shbhlist, this.OrganizeId, this.UserIdentity.DepartmentCode, this.UserIdentity.UserCode);
            return Success("Phê duyệt thành công");
        }
    }
}
