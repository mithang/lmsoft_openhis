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
    public class SysMedicineUnitController : ControllerBase
    {       
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysMedicineUnitRepo _SysMedicineUnitRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysMedicineUnitRepo"></param>
        public SysMedicineUnitController(ISysMedicineUnitRepo SysMedicineUnitRepo)
        {
            this._SysMedicineUnitRepo = SysMedicineUnitRepo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _SysMedicineUnitRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        // GET: PharmacyUnit
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _SysMedicineUnitRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        public IActionResult submitForm(SysMedicineUnitEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.ypdwCode))
            {
                throw new FailedException("Vui lòng nhập mã");
            }
            _SysMedicineUnitRepo.submitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }

    }
}