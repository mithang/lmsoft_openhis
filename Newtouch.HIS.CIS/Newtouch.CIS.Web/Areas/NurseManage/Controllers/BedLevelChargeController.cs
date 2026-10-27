using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.CIS.Compat;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.NurseManage.Controllers
{
    [Area("NurseManage")]
    public class BedLevelChargeController : OrgControllerBase
    {
        private readonly IOrderAuditDmnService _OrderAuditDmnService;
        private readonly IBaseDataDmnService _iBaseDataDmnService;
        private readonly ILevelChargeDmnService _ilevelChargeDmnService;
        private readonly IInpatientBedLevelChargeItemRepo _inpatientBedBaseChargeItemRepo;

        public BedLevelChargeController(IOrderAuditDmnService orderAuditDmnService, IBaseDataDmnService iBaseDataDmnService, ILevelChargeDmnService ilevelChargeDmnService, IInpatientBedLevelChargeItemRepo inpatientBedBaseChargeItemRepo)
        {
            this._OrderAuditDmnService = orderAuditDmnService;
            this._iBaseDataDmnService = iBaseDataDmnService;
            this._ilevelChargeDmnService = ilevelChargeDmnService;
            this._inpatientBedBaseChargeItemRepo = inpatientBedBaseChargeItemRepo;
        }
        /// <summary>
        /// đợicấpChi phídanh sách
        /// </summary>
        /// <param name="aa"></param>
        /// <returns></returns>
        public IActionResult GetIndexGridJson(Pagination pagination,string LevelCode)
        {
            var data = new
            {
                rows = _ilevelChargeDmnService.GetLevelCharge(pagination, OrganizeId, LevelCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult submitForm(SysLevelChargeVO xm,string keyvalue) {
            InpatientBedLevelChargeItemEntity entity = new InpatientBedLevelChargeItemEntity();
            entity= xm.MapperTo(entity);
            entity.OrganizeId = OrganizeId;
            _inpatientBedBaseChargeItemRepo.SubmitForm(entity,keyvalue);
            return Success();
        }

        public IActionResult GetFormJson(string keyvalue) {
           var data= _ilevelChargeDmnService.GetFormJson(OrganizeId, keyvalue);
            return Content(data.ToJson());
        }

        public IActionResult DeleteData(string keyValue) {
            _inpatientBedBaseChargeItemRepo.DeleteForm(keyValue, OrganizeId);
            return Success();
        }
    }
}