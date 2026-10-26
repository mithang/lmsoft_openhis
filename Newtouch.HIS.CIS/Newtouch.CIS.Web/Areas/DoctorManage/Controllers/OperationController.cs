using System;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage
{
    public class OperationController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService _IBaseDataDmnService;
        private readonly IInpatientOperationArrangementRepo _inpatientOperationArrangementRepo;
        private readonly IOperationDmnService _iIOperationDmnService;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly IInpatientFeeDetailRepo _IInpatientFeeDetailRepo;
        private readonly IInpatientPatientInfoRepo _IInpatientPatientInfoRepo;
        private readonly IBaseDataDmnService _IIBaseDataDmnService;

        /// <summary>
        /// Nhập lịch phẫu thuật Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrangeForm()
        {
            ViewBag.OperationAssistant = _sysConfigRepo.GetIntValueByCode("OperationAssistant", OrganizeId, 3);//Số lượng trợ lý phẫu thuật nhập viện
            return View();
        }

        /// <summary>
        /// Tổng quan lịch phẫu thuật Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult ArrangeQuery()
        {
            return View();
        }

        /// <summary>
        /// Bổ sung lịch phẫu thuật Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult OperationSupplement()
        {
            return View();
        }

        /// <summary>
        /// Nhập từng hạng mục thu phí Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult originalCharge()
        {
            return View();
        }

        /// <summary>
        /// Tra cứu danh sách lịch phẫu thuật
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public ActionResult ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req)
        {
            req.orgId = OrganizeId;
            var data = new
            {
                rows = _iIOperationDmnService.ArrangeQueryGridView(pagination, req),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Nội dung lịch phẫu thuật
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public ActionResult ArrangementForID(Pagination pagination, ArrangementRequestVO req)
        {
            req.orgId = OrganizeId;
            return Content(_iIOperationDmnService.ArrangementForID(pagination, req).ToJson());
        }

        /// <summary>
        /// Gửi và lưu dữ liệu lịch phẫu thuật
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public ActionResult submitForm_Arrangement(InpatientOperationArrangementEntity xm, string keyvalue)
        {
            xm.OrganizeId = OrganizeId;
            xm.zt = "1";
            if (_inpatientOperationArrangementRepo.SubmitForm(xm, keyvalue))
                return Success();
            else
                return Error("保存手术安排信息失败");
        }

        /// <summary>
        /// Lấy bệnh nhân phẫu thuật(Bổ sung lịch phẫu thuật)
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOperatPatSearchList()
        {
            return Content(_iIOperationDmnService.GetOperationPatSearchList(OrganizeId).ToJson());
        }

        /// <summary>
        /// Gửi chi phí bổ sung phẫu thuật
        /// </summary>
        /// <param name="FeeDetails"></param>
        /// <returns></returns>
        public ActionResult submitForm_Supplement(List<InpatientFeeDetailEntity> RequestItems, string zyh)
        {

            InpatientPatientInfoEntity zybr = _IInpatientPatientInfoRepo.GetByZyh(OrganizeId, zyh);
            if (zybr == null)//Kiểm tra trống bệnh nhân nhập viện
                throw new Exception(string.Format("orgId:{0}, 住院号:{1} 病人不存在", OrganizeId, zyh));
            InpatientOperationArrangementEntity Arrangement = _inpatientOperationArrangementRepo.FindEntity(p => p.zyh == zyh && p.zt == "1");
            if (Arrangement == null)//Kiểm tra trống lịch phẫu thuật
                throw new Exception("请先进入“手术安排”界面，录入手术相关信息。");
            string lsDepartment = _IIBaseDataDmnService.GetDoctorDepartCode(OrganizeId, Arrangement.surgeonId);
            if (lsDepartment.Length == 0)//Kiểm tra trống khoa của bác sĩ phẫu thuật
                throw new Exception("获取手术医生科室失败！");
            //Gán giá trị
            foreach (InpatientFeeDetailEntity item in RequestItems)
            {
                item.jzks = UserIdentity.DepartmentCode;
                item.czyh = UserIdentity.UserCode;
                item.OrganizeId = OrganizeId;
                item.zyh = zyh;
                item.yzxh = "999999999999";
                item.qqrq = item.zxrq = DateTime.Now;
                item.zfdj = item.yhdj = item.yhje = item.flzfje = 0;
                item.zfje = item.zje;
                item.yzxz = 7;//Chi phí phẫu thuật
                item.gdzxbz = 1;
                item.zt = "1";
                item.ysksdm = item.DeptCode = lsDepartment;//Khoa của bác sĩ phẫu thuật
                item.WardCode = item.qrksdm = item.zxksdm = zybr.WardCode;
                item.cwdm = zybr.BedCode;
                item.ysgh = zybr.ysgh;
            }
            RequestItems = RequestItems.FindAll(p => p.sl > 0);//Loại bỏ thông tin hoàn phí của bảng tính phí
            _iIOperationDmnService.InpatientFeeDetailSubmit(OrganizeId, zyh, RequestItems);
            return Success();
        }

        /// <summary>
        /// Lấy chi phí phẫu thuật của bệnh nhân nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public ActionResult getSupplement(string zyh)
        {
            return Content((new List<OperatFeeVO>()).ToJson());
            //return Content(_iIOperationDmnService.GetOperatFee(OrganizeId, zyh, "7").ToJson());//Chi phí phẫu thuật
        }
    }
}