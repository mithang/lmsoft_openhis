using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// Nhóm thu phí lớn hồ sơ bệnh án hệ thống
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
        /// TheoorganizeIdLấy thông tin nhóm lớn hiệu lực
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult GetListSelectJson(string organizeId)
        {
            var list = _sysMedicalRecordChargeCategoryRepo.GetValidList(organizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysMedicalRecordChargeCategoryRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysMedicalRecordChargeCategoryRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysMedicalRecordChargeCategoryEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                throw new FailedException("请选择组织机构");
            }
            else if (!_SysOrganizeDmnService.IsMedicalOrganize(entity.OrganizeId))
            {
                throw new FailedException("请选择医疗机构（医院或诊所）");
            }
            _sysMedicalRecordChargeCategoryRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
    }
}