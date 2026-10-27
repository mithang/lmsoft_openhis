using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// 系统病案收费大类
    /// </summary>
    public class SysMedicalRecordChargeCategoryController : ControllerBase
    {
        private readonly ISysMedicalRecordChargeCategoryRepo _sysMedicalRecordChargeCategoryRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysMedicalRecordChargeCategoryController(ISysMedicalRecordChargeCategoryRepo sysMedicalRecordChargeCategoryRepo
            , ISysOrganizeDmnService SysOrganizeDmnService)
        {
            this._sysMedicalRecordChargeCategoryRepo = sysMedicalRecordChargeCategoryRepo;
            this._SysOrganizeDmnService = SysOrganizeDmnService;
        }

        /// <summary>
        /// 根据organizeId获取有效大类信息
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IActionResult GetListSelectJson(string organizeId)
        {
            var list = _sysMedicalRecordChargeCategoryRepo.GetValidList(organizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysMedicalRecordChargeCategoryRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysMedicalRecordChargeCategoryRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysMedicalRecordChargeCategoryEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn tổ chức");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("Vui lòng chọn cơ sở y tế(Bệnh viện hoặc phòng khám)");
            }
            _sysMedicalRecordChargeCategoryRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }
    }
}