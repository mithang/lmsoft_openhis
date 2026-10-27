using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineClassificationController : ControllerBase
    {
        private readonly ISysMedicineClassificationRepo _sysMedicineClassificationRepo;

        public SysMedicineClassificationController(ISysMedicineClassificationRepo sysMedicineClassificationRepo)
        {
            this._sysMedicineClassificationRepo = sysMedicineClassificationRepo;
        }

        /// <summary>
        /// Tra cứu phân loại thuốc theo từ khóa
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
    }
}