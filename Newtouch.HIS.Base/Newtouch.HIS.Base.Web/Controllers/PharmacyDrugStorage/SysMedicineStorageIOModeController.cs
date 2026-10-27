using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SysMedicineStorageIOModeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysMedicineStorageIOModeRepo _sysMedicineStorageIOModeRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysMedicineUnitRepo"></param>
        public SysMedicineStorageIOModeController(ISysMedicineStorageIOModeRepo sysMedicineStorageIOModeRepo)
        {
            this._sysMedicineStorageIOModeRepo = sysMedicineStorageIOModeRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysMedicineStorageIOModeRepo.GetList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var data = _sysMedicineStorageIOModeRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitForm(SysMedicineStorageIOModeEntity entity, int? keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.crkfsCode))
            {
                throw new FailedException("Vui lòng nhập mã");
            }
            _sysMedicineStorageIOModeRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công");
        }
    }
}