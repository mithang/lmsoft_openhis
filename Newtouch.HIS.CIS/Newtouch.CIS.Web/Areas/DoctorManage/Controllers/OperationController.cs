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
        /// Sắp xếp phẫu thuậtnhập xemhình
        /// </summary>
        /// <returns></returns>
        public IActionResult ArrangeForm()
        {
            ViewBag.OperationAssistant = _sysConfigRepo.GetIntValueByCode("OperationAssistant", OrganizeId, 3);//Nhập việnPhẫu thuậtgiúptaycáisố
            return View();
        }

        /// <summary>
        /// Sắp xếp phẫu thuậttổngxem xemhình
        /// </summary>
        /// <returns></returns>
        public IActionResult ArrangeQuery()
        {
            return View();
        }

        /// <summary>
        /// Phẫu thuậtbổ sungghi xemhình
        /// </summary>
        /// <returns></returns>
        public IActionResult OperationSupplement()
        {
            return View();
        }

        /// <summary>
        /// Mục thu phíphiếudòngnhập xemhình
        /// </summary>
        /// <returns></returns>
        public IActionResult originalCharge()
        {
            return View();
        }

        /// <summary>
        /// Sắp xếp phẫu thuậtdanh sáchTra cứu
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
        /// Sắp xếp phẫu thuậtnội dung
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
        /// GửiLưuSắp xếp phẫu thuậtDữ liệu
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
                return Error("LưuPhẫu thuậtSắp xếpThông tinthất bại");
        }

        /// <summary>
        /// LấyPhẫu thuậtbệnh người(Phẫu thuậtbổ sungghi)
        /// </summary>
        /// <returns></returns>
        public IActionResult GetOperatPatSearchList()
        {
            return Content(_iIOperationDmnService.GetOperationPatSearchList(OrganizeId).ToJson());
        }

        /// <summary>
        /// GửiPhẫu thuậtbổ sungghiChi phí
        /// </summary>
        /// <param name="FeeDetails"></param>
        /// <returns></returns>
        public IActionResult submitForm_Supplement(List<InpatientFeeDetailEntity> RequestItems, string zyh)
        {

            InpatientPatientInfoEntity zybr = _IInpatientPatientInfoRepo.GetByZyh(OrganizeId, zyh);
            if (zybr == null)//Nhập việnbệnh ngườiphántrống
                throw new Exception(string.Format("orgId:{0}, Số nhập viện:{1} Bệnh nhânKhông tồn tại", OrganizeId, zyh));
            InpatientOperationArrangementEntity Arrangement = _inpatientOperationArrangementRepo.FindEntity(p => p.zyh == zyh && p.zt == "1");
            if (Arrangement == null)//Sắp xếp phẫu thuậtphántrống
                throw new Exception("Vui lòng vào giao diện “Sắp xếp phẫu thuật”, nhập thông tin liên quan đến phẫu thuật.");
            string lsDepartment = _IIBaseDataDmnService.GetDoctorDepartCode(OrganizeId, Arrangement.surgeonId);
            if (lsDepartment.Length == 0)//Phẫu thuậtBác sĩKhoaphántrống
                throw new Exception("LấyPhẫu thuậtBác sĩKhoathất bại!");
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
                item.yzxz = 7;//Phẫu thuậtChi phí
                item.gdzxbz = 1;
                item.zt = "1";
                item.ysksdm = item.DeptCode = lsDepartment;//Phẫu thuậtBác sĩKhoa
                item.WardCode = item.qrksdm = item.zxksdm = zybr.WardCode;
                item.cwdm = zybr.BedCode;
                item.ysgh = zybr.ysgh;
            }
            RequestItems = RequestItems.FindAll(p => p.sl > 0);//đitrừtínhphíbảngcủaHoàn phíthông tin
            _iIOperationDmnService.InpatientFeeDetailSubmit(OrganizeId, zyh, RequestItems);
            return Success();
        }

        /// <summary>
        /// LấyNhập việnbệnh ngườiPhẫu thuậtChi phí
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IActionResult getSupplement(string zyh)
        {
            return Content((new List<OperatFeeVO>()).ToJson());
            //return Content(_iIOperationDmnService.GetOperatFee(OrganizeId, zyh, "7").ToJson());//Phẫu thuậtChi phí
        }
    }
}