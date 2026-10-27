using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers.PharmacyDrugStorage
{
    public class SysMedicineUsageController : ControllerBase
    {
        private readonly ISysMedicineUsageRepo _sysMedicineUsageRepo;

        public SysMedicineUsageController(ISysMedicineUsageRepo sysMedicineUsageRepo)
        {
            this._sysMedicineUsageRepo = sysMedicineUsageRepo;
        }

            // GET: SysMedicineUsage
        public override IActionResult Index()
        {
            return View();
        }

        public override IActionResult Form()
        {
            return View();
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public IActionResult SubmitForm(SysMedicineUsageEntity entity, int? yfId)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysMedicineUsageRepo.SubmitForm(entity, yfId);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        public IActionResult GetGridJson(Pagination pagination, string keyword = null)
        {
            var list = _sysMedicineUsageRepo.GetPagintionList(pagination, keyword);
            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 新增或修改Form
        /// </summary>
        public IActionResult GetFormJson(int? yfId)
        {
            var entity = _sysMedicineUsageRepo.GetMedicineUsageEntity(yfId);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        public IActionResult DeleteForm(int yfId)
        {
            _sysMedicineUsageRepo.DeleteForm(yfId);
            return Success("Thao tác thành công.");
        }

    }
}