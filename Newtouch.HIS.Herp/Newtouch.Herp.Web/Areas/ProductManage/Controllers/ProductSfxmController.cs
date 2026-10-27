using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity.Product;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资收费项目对照表
    /// </summary>
    [Area("ProductManage")]
    public class ProductSfxmController : ControllerBase
    {
        private readonly IBaseDataDmnService _baseDataDmnService;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IRelProductAndsfxmApp _relProductAndsfxmApp;
        private readonly IRelProductAndsfxmDmnService _relProductAndsfxmDmnService;
        private readonly IRelProductAndsfxmRepo _relProductAndsfxmRepo;

        public ProductSfxmController(IBaseDataDmnService baseDataDmnService, IWzProductRepo wzProductRepo,
            IRelProductAndsfxmApp relProductAndsfxmApp, IRelProductAndsfxmDmnService relProductAndsfxmDmnService,
            IRelProductAndsfxmRepo relProductAndsfxmRepo)
        {
            _baseDataDmnService = baseDataDmnService;
            _wzProductRepo = wzProductRepo;
            _relProductAndsfxmApp = relProductAndsfxmApp;
            _relProductAndsfxmDmnService = relProductAndsfxmDmnService;
            _relProductAndsfxmRepo = relProductAndsfxmRepo;
        }

        public override IActionResult Index()
        {
            return View();
        }

        public override IActionResult Form()
        {
            return View();
        }

        public IActionResult GetProductSfxmGridJson(Pagination pagination, string sfdlCode, string productTypeId, string zt)
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

        public IActionResult GetProductSfxmFormJson(string keyWord)
        {
            var entity = _relProductAndsfxmDmnService.SelectProductAndsfxm(keyWord, OrganizeId);
            return Content((entity ?? new RelProductAndsfxmVo()).ToJson());
        }

        public IActionResult GetAllSfdl()
        {
            var data = _baseDataDmnService.SelectSfdl(OrganizeId);
            if (data == null || data.Count <= 0)
            {
                var result = new List<TreeSelectModel>
                {
                    new TreeSelectModel
                    {
                        id = "",
                        text = "==Chọn hạng mục lớn==",
                        parentId = ""
                    }
                };
                return Content(result.TreeSelectJson());
            }
            var treeList = data.Select(item => new TreeSelectModel { id = item.dlCode, text = item.dlmc, parentId = (item.ParentId ?? 0).ToString() }).ToList();
            return Content(treeList.TreeSelectJson());
        }

        public IActionResult GetSfmxBySfdl(string sfdlCode)
        {
            var data = _baseDataDmnService.SelectSfxm(sfdlCode, OrganizeId);
            if (data != null && data.Count > 0) return Content(data.ToJson());
            return Content(new { sfxmCode = "", sfxmmc = "==Chọn hạng mục==" }.ToJson());
        }

        public IActionResult SubmitForm(string key, string zt, string productId, string sfxmCode, string sfxmmc, string sfdlCode, string sfdlmc)
        {
            var entity = new RelProductAndsfxmEntity
            {
                Id = key,
                zt = zt,
                OrganizeId = OrganizeId,
                productId = productId,
                sfxmCode = sfxmCode,
                sfxmmc = sfxmmc,
                sfdlCode = sfdlCode,
                sfdlmc = sfdlmc
            };
            var result = _relProductAndsfxmApp.SubmitRelProductAndsfxm(entity);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        public IActionResult DeleteForm(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Vui lòng chỉ định quan hệ đối chiếu cần xóa");
            }
            var entity = _relProductAndsfxmRepo.FindEntity(p => p.Id == keyValue && p.OrganizeId == OrganizeId);
            if (entity == null) return Error("Không tìm thấy quan hệ đối chiếu đã chỉ định");
            return _relProductAndsfxmRepo.Delete(entity) > 0 ? Success() : Error("Xóa quan hệ đối chiếu đã chỉ định thất bại");
        }
    }
}
