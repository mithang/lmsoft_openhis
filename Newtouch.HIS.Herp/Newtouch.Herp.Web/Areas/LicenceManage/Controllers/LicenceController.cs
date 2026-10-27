using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Web.Areas.LicenceManage.Controllers
{
    /// <summary>
    /// 证照管理 (ported from Framework MVC5 to ASP.NET Core 6.0)
    /// </summary>
    [Area("LicenceManage")]
    public class LicenceController : ControllerBase
    {
        private readonly ILicLicenceBelongedRepo _licLicenceBelongedRepo;
        private readonly ILicLicenceDmnService _licLicenceDmnService;
        private readonly ILicLicenceTypeRepo _licLicenceTypeRepo;
        private readonly ILicLicenceRepo _licLicenceRepo;
        private readonly ILicLicenceApp _licLicence;

        public LicenceController(ILicLicenceBelongedRepo licLicenceBelongedRepo,
            ILicLicenceDmnService licLicenceDmnService,
            ILicLicenceTypeRepo licLicenceTypeRepo,
            ILicLicenceRepo licLicenceRepo,
            ILicLicenceApp licLicence)
        {
            _licLicenceBelongedRepo = licLicenceBelongedRepo;
            _licLicenceDmnService = licLicenceDmnService;
            _licLicenceTypeRepo = licLicenceTypeRepo;
            _licLicenceRepo = licLicenceRepo;
            _licLicence = licLicence;
        }

        #region 主功能

        /// <summary>
        /// 证照维护
        /// </summary>
        /// <returns></returns>
        public override IActionResult Form()
        {
            var maxSize = Constants.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            ViewData["maxSize"] = Convert.ToInt32(maxSize);
            return View();
        }

        /// <summary>
        /// 获取证照列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="belongedId"></param>
        /// <param name="licenceTypeId"></param>
        /// <returns></returns>
        public IActionResult GetLicenceGridJson(Pagination pagination, string keyWord, string belongedId, string licenceTypeId)
        {
            var data = new
            {
                rows = _licLicenceDmnService.GetLicenceList(pagination, keyWord, belongedId, licenceTypeId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除证照
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteLicForm(string keyValue)
        {
            var entity = _licLicenceRepo.FindEntity(p => p.Id == keyValue);
            if (entity == null) return Error("Xóa thất bại");
            return _licLicenceRepo.Delete(entity) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// 获取证照信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetLicenceFormJson(string keyWord)
        {
            var entity = _licLicenceRepo.FindEntity(p => p.Id == keyWord);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 证照维护 表单提交
        /// </summary>
        /// <param name="licLicenceEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitLicenceForm(LicLicenceEntity licLicenceEntity, string keyValue)
        {
            var file = Request.HasFormContentType ? Request.Form.Files["fileUpload"] : null;
            licLicenceEntity.zt = licLicenceEntity.zt == "on" ? "1" : "0";
            licLicenceEntity.OrganizeId = OrganizeId;
            licLicenceEntity.fileUrl = _licLicence.UploadFile(file);
            return _licLicenceRepo.SubmitForm(licLicenceEntity, keyValue) > 0 ? Success("Thao tác thành công", null) : Error("Thao tác thất bại");
        }

        /// <summary>
        /// 获取文件地址
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFileUrl(string t)
        {
            var entity = _licLicenceRepo.FindEntity(p => p.Id == t);
            if (entity != null && !string.IsNullOrWhiteSpace(entity.fileUrl))
            {
                var result = new
                {
                    Authority = Request.Host.Value,
                    uri = entity.fileUrl
                }.ToJson();
                return Content(result);
            }
            return Content("");
        }

        #endregion

        #region 证照类型

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public IActionResult LicenceType()
        {
            return View();
        }

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public IActionResult LicenceTypeForm()
        {
            return View();
        }

        /// <summary>
        /// 获取证照类型列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetLicenceTypeGridJson(Pagination pagination, string keyWord = "")
        {
            var data = new
            {
                rows = _licLicenceDmnService.GetLicenceTypeList(pagination, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除证照类型
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteLicenceType(string keyValue)
        {
            var entity = _licLicenceTypeRepo.FindEntity(p => p.Id == keyValue);
            if (entity == null) return Error("Xóa thất bại");
            return _licLicenceTypeRepo.Delete(entity) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// 获取证照类型信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetLicenceTypeFormJson(string keyWord)
        {
            var entity = _licLicenceTypeRepo.FindEntity(p => p.Id == keyWord);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Submit SyyyForm
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult SubmitLicenceTypeForm(LicLicenceTypeEntity source, string keyWord)
        {
            source.zt = source.zt == "true" ? "1" : "0";
            return _licLicenceTypeRepo.SubmitForm(source, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        /// <summary>
        /// 根据所属ID获取类型明细
        /// </summary>
        /// <param name="belonged"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetlicenceType(string belonged = "")
        {
            return Content(_licLicenceTypeRepo.IQueryable(p => p.belongedId.Contains(belonged) && p.zt == ((int)Enumzt.Enable).ToString()).Select(s => new { id = s.Id, text = s.typeName }).ToList().ToJson());
        }

        #endregion

        #region 证照所属

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public IActionResult LicenceBelonged()
        {
            return View();
        }

        /// <summary>
        /// 证照类型
        /// </summary>
        /// <returns></returns>
        public IActionResult LicenceBelongedForm()
        {
            return View();
        }

        /// <summary>
        /// 获取证照所属列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetLicenceBelongedGridJson(Pagination pagination, string keyWord = "")
        {
            // The ported IRepositoryBase has no paged FindList overload, so page
            // here (same JSON shape: rows/total/page/records) instead of in the repo.
            keyWord = keyWord ?? "";
            var all = _licLicenceBelongedRepo.FindList(p => "" == keyWord || p.belonged.Contains(keyWord.Trim()));
            int rows = pagination.rows ?? 0;
            if (rows <= 0) rows = 15;
            int page = pagination.page ?? 0;
            if (page <= 0) page = 1;
            pagination.records = all.Count;
            var pageRows = all.Skip((page - 1) * rows).Take(rows).ToList();
            var data = new
            {
                rows = pageRows,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 删除证照所属
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteLicenceBelonged(string keyValue)
        {
            var entity = _licLicenceBelongedRepo.FindEntity(p => p.Id == keyValue);
            if (entity == null) return Error("Xóa thất bại");
            return _licLicenceBelongedRepo.Delete(entity) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// 获取证照所属信息
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetLicenceBelongedFormJson(string keyWord)
        {
            var entity = _licLicenceBelongedRepo.FindEntity(p => p.Id == keyWord);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// 获取证照所属信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetLicenceBelongedForSelect()
        {
            var entity = _licLicenceBelongedRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString()).Select(p => new { id = p.Id, text = p.belonged }).ToList().Distinct();
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Submit LicenceBelongedForm
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult SubmitLicenceBelongedForm(LicLicenceBelongedEntity source, string keyWord)
        {
            source.zt = source.zt == "true" ? "1" : "0";
            return _licLicenceBelongedRepo.SubmitForm(source, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        /// <summary>
        /// 获取所属
        /// </summary>
        /// <returns></returns>
        public IActionResult Getbelonged()
        {
            return Content(_licLicenceBelongedRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString())
                .Select(s => new { id = s.Id, text = s.belonged })
                .ToList()
                .ToJson());
        }
        #endregion
    }
}
