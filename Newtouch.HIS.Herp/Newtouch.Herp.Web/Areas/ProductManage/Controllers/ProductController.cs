using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资管理
    /// </summary>
    [Area("ProductManage")]
    public class ProductController : ControllerBase
    {
        private readonly IWzProductDmnService _wzProductDmnService;
        private readonly IProductApp _productApp;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IRelProductUnitRepo _relProductUnitRepo;

        public ProductController(IWzProductDmnService wzProductDmnService, IProductApp productApp,
            IWzProductRepo wzProductRepo, IRelProductUnitRepo relProductUnitRepo)
        {
            _wzProductDmnService = wzProductDmnService;
            _productApp = productApp;
            _wzProductRepo = wzProductRepo;
            _relProductUnitRepo = relProductUnitRepo;
        }

        public override IActionResult Form()
        {
            var maxSize = Constants.GetAppConfigValue("imageMaxSize");
            maxSize = string.IsNullOrWhiteSpace(maxSize) ? 5.ToString() : maxSize;
            ViewData["maxSize"] = Convert.ToInt32(maxSize);
            return View();
        }

        public IActionResult GetProductGridJson(Pagination pagination, string lb, string zt, string keyWord)
        {
            var data = new
            {
                rows = _wzProductDmnService.GetList(pagination, OrganizeId, lb, zt, keyWord),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult DeleteForm(string keyValue)
        {
            try
            {
                _wzProductDmnService.DeleteProduct(keyValue);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
            return Success("Xóa thành công");
        }

        public IActionResult GetFormJson(string keyWord)
        {
            var entity = _wzProductDmnService.GetProductInfo(keyWord, OrganizeId);
            return Content(entity.ToJson());
        }

        public IActionResult GetProductUnitGridJson(string keyWord)
        {
            var units = _relProductUnitRepo.IQueryable(p => p.productId == keyWord && p.OrganizeId == OrganizeId).ToList();
            return Content(units.ToJson());
        }

        public IActionResult DeleteProductUnit(string relId)
        {
            return _relProductUnitRepo.DeleteById(relId) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        [HttpPost]
        public IActionResult SubmitUnit(ProductUnitRelVo productUnitRelVo)
        {
            return _productApp.SubmitRelProductUnit(productUnitRelVo, OrganizeId) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        [HandlerAjaxOnly]
        public IActionResult SubmitForm(VProductInfoEntity wzProductEntity, string keyValue, IFormFile imgUpload)
        {
            if (!string.IsNullOrEmpty(_wzProductDmnService.CheckProductCode(wzProductEntity.productCode, keyValue)))
            {
                return Error("Mã vật tư đã tồn tại");
            }
            wzProductEntity.zt = wzProductEntity.zt == "on" ? "1" : "0";
            wzProductEntity.sflkc = wzProductEntity.sflkc == "on" ? "1" : "0";
            wzProductEntity.sffy = wzProductEntity.sffy == "on" ? "1" : "0";
            wzProductEntity.sfgt = wzProductEntity.sfgt == "on" ? "1" : "0";
            wzProductEntity.OrganizeId = OrganizeId;
            wzProductEntity.imageUrl = _productApp.UploadImag(imgUpload);
            wzProductEntity.iswzsame = wzProductEntity.iswzsame == "on" ? "1" : "0";
            return _productApp.SubmitForm(wzProductEntity, keyValue) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        public IActionResult GetProductCode()
        {
            return Content(_wzProductDmnService.GetProductCode());
        }

        public IActionResult CheckProductCode(string productCode, string keyWord)
        {
            return Content(_wzProductDmnService.CheckProductCode(productCode, keyWord));
        }

        public IActionResult GetProductbyType(string typId)
        {
            var data = _wzProductRepo.IQueryable(p => p.typeId == typId && p.zt == "1" && p.OrganizeId == OrganizeId);
            if (data == null || !data.Any()) return Content(new { Id = "", name = "==Chọn vật tư==" }.ToJson());
            return Content(data.ToJson());
        }

        public IActionResult ProductDetailQuery(string keyword)
        {
            var result = _wzProductDmnService.SelectProductDetail(keyword, OrganizeId);
            return Content(result.ToJson());
        }

        [HttpGet]
        public IActionResult ProductUnitsQuery(string keyword)
        {
            var result = _wzProductDmnService.SelectProductUnits(keyword, OrganizeId);
            return Content(result.ToJson());
        }
    }
}
