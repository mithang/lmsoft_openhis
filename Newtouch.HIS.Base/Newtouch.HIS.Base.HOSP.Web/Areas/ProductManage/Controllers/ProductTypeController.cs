using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Newtouch.HIS.Application.Interface;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// Bảo trì loại vật tư
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

        /// <summary>
        /// Lấy danh sách loại vật tư
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            return _wzTypeRepo.DeleteUnitById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// get product type information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzTypeRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// Bảo trì loại vật tư Gửi biểu mẫu
        /// </summary>
        /// <param name="wzTypeEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzTypeEntity wzTypeEntity, string keyWord)
        {
            wzTypeEntity.zt = wzTypeEntity.zt == "true" ? "1" : "0";
            return _productTypeApp.SubmitForm(wzTypeEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }

        /// <summary>
        /// Lấy cấp trên
        /// </summary>
        /// <returns></returns>
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