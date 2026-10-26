using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// Nhóm thu phí lớn bảo hiểm nông nghiệp hệ thống
    /// </summary>
    public class SysAgriInsuranceChargeCategoryController : ControllerBase
    {
        private readonly ISysAgriInsuranceChargeCategoryRepo _sysAgriInsuranceChargeCategoryRepo;
        private readonly ISysOrganizeDmnService _SysOrganizeDmnService;

        public SysAgriInsuranceChargeCategoryController(ISysAgriInsuranceChargeCategoryRepo sysAgriInsuranceChargeCategoryRepo
            , ISysOrganizeDmnService sysOrganizeDmnService)
        {
            this._sysAgriInsuranceChargeCategoryRepo = sysAgriInsuranceChargeCategoryRepo;
            this._SysOrganizeDmnService = sysOrganizeDmnService;
        }
        
        /// <summary>
        /// TheoorganizeIdLấy thông tin nhóm lớn hiệu lực
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult GetListSelectJson(string organizeId)
        {
            var list = _sysAgriInsuranceChargeCategoryRepo.GetValidList(organizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGridJson(string organizeId, string keyword)
        {
            var data = _sysAgriInsuranceChargeCategoryRepo.GetList(organizeId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(int keyValue)
        {
            var entity = _sysAgriInsuranceChargeCategoryRepo.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(SysAgriInsuranceChargeCategoryEntity entity, int? keyValue)
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
            _sysAgriInsuranceChargeCategoryRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }
    }
}