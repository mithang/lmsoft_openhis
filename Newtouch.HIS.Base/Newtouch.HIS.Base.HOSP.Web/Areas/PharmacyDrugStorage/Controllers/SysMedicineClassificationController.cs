using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.PharmacyDrugStorage.Controllers
{
    [Area("PharmacyDrugStorage")]
    public class SysMedicineClassificationController : ControllerBase
    {
        private readonly ISysMedicineClassificationRepo _sysMedicineClassificationRepo;

        public SysMedicineClassificationController(ISysMedicineClassificationRepo sysMedicineClassificationRepo)
        {
            this._sysMedicineClassificationRepo = sysMedicineClassificationRepo;
        }

        /// <summary>
        /// 根据关键字查询药品分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineClassificationRepo.GetValidList(keyword);
            return Json(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(Pagination pagination, string keyword)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysMedicineClassificationRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _sysMedicineClassificationRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitForm(SysMedicineClassificationEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.ypflCode))
            {
                throw new FailedException("Vui lòng nhập mã");
            }
            if (string.IsNullOrWhiteSpace(entity.ypflmc))
            {
                throw new FailedException("Vui lòng nhập tên");
            }
            _sysMedicineClassificationRepo.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }




    }
}