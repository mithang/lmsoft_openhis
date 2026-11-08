using System;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.IRepository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage
{
    [Area("DoctorManage")]
    public class OperationController : OrgControllerBase
    {
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IBaseDataDmnService _IBaseDataDmnService;
        private readonly IInpatientOperationArrangementRepo _inpatientOperationArrangementRepo;
        private readonly IOperationDmnService _iIOperationDmnService;
        private readonly ISysStaffRepo _sysStaffRepo;
        private readonly IInpatientFeeDetailRepo _IInpatientFeeDetailRepo;
        private readonly IInpatientPatientInfoRepo _IInpatientPatientInfoRepo;
        private readonly IBaseDataDmnService _IIBaseDataDmnService;

        public OperationController(ISysConfigRepo sysConfigRepo, IBaseDataDmnService iBaseDataDmnService, IInpatientOperationArrangementRepo inpatientOperationArrangementRepo, IOperationDmnService iIOperationDmnService, ISysStaffRepo sysStaffRepo, IInpatientFeeDetailRepo iInpatientFeeDetailRepo, IInpatientPatientInfoRepo iInpatientPatientInfoRepo, IBaseDataDmnService iIBaseDataDmnService)
        {
            this._sysConfigRepo = sysConfigRepo;
            this._IBaseDataDmnService = iBaseDataDmnService;
            this._inpatientOperationArrangementRepo = inpatientOperationArrangementRepo;
            this._iIOperationDmnService = iIOperationDmnService;
            this._sysStaffRepo = sysStaffRepo;
            this._IInpatientFeeDetailRepo = iInpatientFeeDetailRepo;
            this._IInpatientPatientInfoRepo = iInpatientPatientInfoRepo;
            this._IIBaseDataDmnService = iIBaseDataDmnService;
        }

        /// <summary>
        /// Sắp xếp phẫu thuật nhập xem hình
        /// </summary>
        /// <returns></returns>
        public IActionResult ArrangeForm()
        {
            ViewBag.OperationAssistant = _sysConfigRepo.GetIntValueByCode("OperationAssistant", OrganizeId, 3);//Nhập viện Phẫu thuật giúp tay cái số
            return View();
        }

        /// <summary>
        /// Sắp xếp phẫu thuật tổng xem xem hình
        /// </summary>
        /// <returns></returns>
        public IActionResult ArrangeQuery()
        {
            return View();
        }

        /// <summary>
        /// Phẫu thuật bổ sungghi xem hình
        /// </summary>
        /// <returns></returns>
        public IActionResult OperationSupplement()
        {
            return View();
        }

        /// <summary>
        /// Mục thu phí phiếu dòng nhập xem hình
        /// </summary>
        /// <returns></returns>
        public IActionResult originalCharge()
        {
            return View();
        }

        /// <summary>
        /// Sắp xếp phẫu thuật danh sách Tra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult ArrangeQueryGridView(Pagination pagination, ArrangeQueryRequestVO req)
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
        /// Sắp xếp phẫu thuật nội dung
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public IActionResult ArrangementForID(Pagination pagination, ArrangementRequestVO req)
        {
            req.orgId = OrganizeId;
            return Content(_iIOperationDmnService.ArrangementForID(pagination, req).ToJson());
        }

        /// <summary>
        /// Gửi Lưu Sắp xếp phẫu thuật Dữ liệu
        /// </summary>
        /// <param name="xm"></param>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public IActionResult submitForm_Arrangement(InpatientOperationArrangementEntity xm, string keyvalue)
        {
            xm.OrganizeId = OrganizeId;
            xm.zt = "1";
            if (_inpatientOperationArrangementRepo.SubmitForm(xm, keyvalue))
                return Success();
            else
                return Error("Lưu Phẫu thuật Sắp xếp Thông tin thất bại");
        }

        /// <summary>
        /// Lấy Phẫu thuật bệnh người(Phẫu thuật bổ sungghi)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetOperatPatSearchList()
        {
            return Content(_iIOperationDmnService.GetOperationPatSearchList(OrganizeId).ToJson());
        }

        /// <summary>
        /// Gửi Phẫu thuật bổ sungghiChi phí
        /// </summary>
        /// <param name="FeeDetails"></param>
        /// <returns></returns>
        public IActionResult submitForm_Supplement(List<InpatientFeeDetailEntity> RequestItems, string zyh)
        {

            InpatientPatientInfoEntity zybr = _IInpatientPatientInfoRepo.GetByZyh(OrganizeId, zyh);
            if (zybr == null)//Nhập viện bệnh người phán trống
                throw new Exception(string.Format("orgId:{0}, Số nhập viện:{1} Bệnh nhân Không tồn tại", OrganizeId, zyh));
            InpatientOperationArrangementEntity Arrangement = _inpatientOperationArrangementRepo.FindEntity(p => p.zyh == zyh && p.zt == "1");
            if (Arrangement == null)//Sắp xếp phẫu thuật phán trống
                throw new Exception("Vui lòng vào giao diện “Sắp xếp phẫu thuật”, nhập thông tin liên quan đến phẫu thuật.");
            string lsDepartment = _IIBaseDataDmnService.GetDoctorDepartCode(OrganizeId, Arrangement.surgeonId);
            if (lsDepartment.Length == 0)//Phẫu thuật Bác sĩ Khoa phán trống
                throw new Exception("Lấy Phẫu thuật Bác sĩ Khoa thất bại!");
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
                item.yzxz = 7;//Phẫu thuật Chi phí
                item.gdzxbz = 1;
                item.zt = "1";
                item.ysksdm = item.DeptCode = lsDepartment;//Phẫu thuật Bác sĩ Khoa
                item.WardCode = item.qrksdm = item.zxksdm = zybr.WardCode;
                item.cwdm = zybr.BedCode;
                item.ysgh = zybr.ysgh;
            }
            RequestItems = RequestItems.FindAll(p => p.sl > 0);//đi trừ tính phí bảng của Hoàn phí thông tin
            _iIOperationDmnService.InpatientFeeDetailSubmit(OrganizeId, zyh, RequestItems);
            return Success();
        }

        /// <summary>
        /// Lấy Nhập viện bệnh người Phẫu thuật Chi phí
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult getSupplement(string zyh)
        {
            return Content((new List<OperatFeeVO>()).ToJson());
            //return Content(_iIOperationDmnService.GetOperatFee(OrganizeId, zyh, "7").ToJson());//Phẫu thuật Chi phí
        }
    }
}