using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class SysMedicineUnitController : ControllerBase
    {
        private readonly ISysMedicineUnitRepo _sysMedicineUnitRepo;

        public SysMedicineUnitController(ISysMedicineUnitRepo sysMedicineUnitRepo)
        {
            this._sysMedicineUnitRepo = sysMedicineUnitRepo;
        }

        /// <summary>
        /// Lấy danh sách đơn vị thuốc
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public IActionResult GetListSelectJson(string keyword)
        {
            var data = _sysMedicineUnitRepo.GetValidList(keyword);
            return Json(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysMedicineUnitRepo.GetList(keyword);
            return Content(data.ToJson());
        }

    }
}