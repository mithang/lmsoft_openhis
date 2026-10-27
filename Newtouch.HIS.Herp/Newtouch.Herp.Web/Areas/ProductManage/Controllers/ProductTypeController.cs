using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资类别维护
    /// </summary>
    [Area("ProductManage")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IWzTypeRepo _wzTypeRepo;
        private readonly IProductTypeApp _productTypeApp;

        public ProductTypeController(IWzTypeRepo wzTypeRepo, IProductTypeApp productTypeApp)
        {
            _wzTypeRepo = wzTypeRepo;
            _productTypeApp = productTypeApp;
        }

        public IActionResult GetTypeGridJson(string keyWord = "")
        {
            var data = _wzTypeRepo.IQueryable().ToList();
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                data = data.TreeWhere(t => t.name.Contains(keyWord), parentId: "ParentId");
            }
            var treeList = (from item in data
                            let hasChildren = data.Count(t => t.parentId == item.Id) != 0
                            select new TreeGridModel
                            {
                                id = item.Id,
                                isLeaf = hasChildren,
                                parentId = item.parentId,
                                expanded = hasChildren,
                                entityJson = item.ToJson()
                            }).ToList();
            return Content(treeList.TreeGridJson(null));
        }

        public IActionResult DeleteForm(string keyValue)
        {
            return _wzTypeRepo.DeleteUnitById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzTypeRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzTypeEntity wzTypeEntity, string keyWord)
        {
            wzTypeEntity.zt = wzTypeEntity.zt == "true" ? "1" : "0";
            return _productTypeApp.SubmitForm(wzTypeEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        public IActionResult GetPatientTreeSelectJson()
        {
            var warehouse = _wzTypeRepo.IQueryable(p => p.zt == "1").ToList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in warehouse)
            {
                var treeModel = new TreeSelectModel
                {
                    id = item.Id,
                    text = item.name,
                    parentId = item.parentId
                };
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
    }
}
