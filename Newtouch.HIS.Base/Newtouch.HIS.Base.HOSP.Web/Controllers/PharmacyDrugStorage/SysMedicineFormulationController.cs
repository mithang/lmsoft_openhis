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
    public class SysMedicineFormulationController : ControllerBase
    {
        private readonly ISysMedicineFormulationRepo _sysMedicineFormulationRepo;

        public SysMedicineFormulationController(ISysMedicineFormulationRepo sysMedicineFormulationRepo)
        {
            this._sysMedicineFormulationRepo = sysMedicineFormulationRepo;
        }

        /// <summary>
        /// 查询药品剂型
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetListSelectJson(string keyword)
        {
            var list = _sysMedicineFormulationRepo.GetValidList(keyword);
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
                rows = _sysMedicineFormulationRepo.GetPagintionList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }


    }
}