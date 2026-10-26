using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using NLog.Client.Util;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{

    /// <summary>
    /// Bảng đối chiếu khoản thu vật tư
    /// </summary>
    public class ProductSfxmController : ControllerBase
    {
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IRelProductAndsfxmApp _relProductAndsfxmApp;
        private readonly IRelProductAndsfxmDmnService _relProductAndsfxmDmnService;
        private readonly IRelProductAndsfxmRepo _relProductAndsfxmRepo;

        #region Khung nhìn

        /// <summary>
        /// Hiển thị dữ liệu Khung nhìn
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Thêm mới/Sửa Khung nhìn
        /// </summary>
        /// <returns></returns>
        public override ActionResult Form()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// Danh sách vật tư
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sfdlCode"></param>
        /// <param name="productTypeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public ActionResult GetProductSfxmGridJson(Pagination pagination, string sfdlCode, string productTypeId, string zt)
        {
            var data = new
            {
                rows = _relProductAndsfxmDmnService.SelectProductAndsfxm(pagination, sfdlCode, productTypeId, OrganizeId, zt),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy quan hệ liên kết khoản thu vật tư
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetProductSfxmFormJson(string keyWord)
        {
            var entity = _relProductAndsfxmDmnService.SelectProductAndsfxm(keyWord, OrganizeId);
            return Content((entity ?? new RelProductAndsfxmVo()).ToJson());
        }

        /// <summary>
        /// Lấy tất cả khoản thu
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllSfdl()
        {
            var data = _baseDataDmnService.SelectSfdl(OrganizeId);
            if (data == null || data.Count <= 0)
            {
                var result = new List<TreeSelectModel>
                {
                    new TreeSelectModel
                    {
                        id = "",
                        text = "==请选择项目大类==",
                        parentId = ""
                    }
                };
                return Content(result.TreeSelectJson());
            }
            var treeList = data.Select(item => new TreeSelectModel { id = item.dlCode, text = item.dlmc, parentId = (item.ParentId ?? 0).ToString() }).ToList();
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// Lấy khoản thu theo mã nhóm lớn
        /// </summary>
        /// <param name="sfdlCode"></param>
        /// <returns></returns>
        public ActionResult GetSfmxBySfdl(string sfdlCode)
        {
            var data = _baseDataDmnService.SelectSfxm(sfdlCode, OrganizeId);
            if (data != null && data.Count > 0) return Content(data.ToJson());
            return Content(new { sfxmCode = "", sfxmmc = "==请选择项目==" }.ToJson());
        }

        /// <summary>
        /// Gửi gắn kết khoản thu và vật tư
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult SubmitForm(string key)
        {
            var entity = new RelProductAndsfxmEntity
            {
                Id = key,
                zt = Request.Params["zt"],
                OrganizeId = OrganizeId,
                productId = Request.Params["productId"],
                sfxmCode = Request.Params["sfxmCode"],
                sfxmmc = Request.Params["sfxmmc"],
                sfdlCode = Request.Params["sfdlCode"],
                sfdlmc = Request.Params["sfdlmc"]
            };
            var result = _relProductAndsfxmApp.SubmitRelProductAndsfxm(entity);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Xóa dữ liệu liên quan
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("请指定要删除的关联关系");
            }

            var entity = _relProductAndsfxmRepo.FindEntity(p => p.Id == keyValue && p.OrganizeId == OrganizeId);
            if (entity == null) return Error("未找到指定的关联关系");
            return _relProductAndsfxmRepo.Delete(entity) > 0 ? Success() : Error("删除指定关联关系失败");
        }
    }
}