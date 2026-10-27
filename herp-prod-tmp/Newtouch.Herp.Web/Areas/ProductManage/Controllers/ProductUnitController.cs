using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资单位维护
    /// </summary>
    [Area("ProductManage")]
    public class ProductUnitController : ControllerBase
    {
        private readonly IWzUnitRepo _wzUnitRepo;
        private readonly IWzUnitApp _wzUnitApp;

        public ProductUnitController(IWzUnitRepo wzUnitRepo, IWzUnitApp wzUnitApp)
        {
            _wzUnitRepo = wzUnitRepo;
            _wzUnitApp = wzUnitApp;
        }

        public IActionResult GetUnitGridJson(Pagination pagination, string keyWord = "")
        {
            // RepositoryBase in this copy has no FindList(predicate, pagination)
            // overload, so pagination is applied here on the filtered list.
            var all = _wzUnitRepo.IQueryable()
                .Where(p => "" == keyWord || p.name.Contains(keyWord.Trim())).ToList();
            int pageSize = pagination.rows ?? 15;
            if (pageSize <= 0) pageSize = 15;
            int pageIndex = pagination.page ?? 1;
            if (pageIndex <= 0) pageIndex = 1;
            pagination.rows = pageSize;
            pagination.page = pageIndex;
            pagination.records = all.Count;
            var data = new
            {
                rows = all.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        public IActionResult GetProductUnitSelectJson()
        {
            var list = _wzUnitRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString()).ToList();
            return Content(list.ToJson());
        }

        public IActionResult DeleteForm(string keyValue)
        {
            return _wzUnitRepo.DeleteUnitById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzUnitRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzUnitEntity wzUnitEntity, string keyWord)
        {
            var o = _wzUnitRepo.FindEntity(p => p.name.Trim() == wzUnitEntity.name.Trim());
            if (o != null) return Error("Đơn vị này đã tồn tại, vui lòng không gửi trùng!");
            wzUnitEntity.zt = wzUnitEntity.zt == "true" ? "1" : "0";
            return _wzUnitApp.SubmitForm(wzUnitEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
    }
}
