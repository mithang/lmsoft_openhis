using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    [Area("ProductManage")]
    public class ProductCrkfsController : ControllerBase
    {
        private readonly IWzCrkfsRepo _wzCrkfsRepo;
        private readonly IWzCrkfsApp _wzCrkfsApp;

        public ProductCrkfsController(IWzCrkfsRepo wzCrkfsRepo, IWzCrkfsApp wzCrkfsApp)
        {
            _wzCrkfsRepo = wzCrkfsRepo;
            _wzCrkfsApp = wzCrkfsApp;
        }

        public IActionResult GetCrkfsGridJson(Pagination pagination, string keyWord = "")
        {
            // RepositoryBase in this copy has no FindList(predicate, pagination)
            // overload, so pagination is applied here on the filtered list.
            var all = _wzCrkfsRepo.IQueryable()
                .Where(p => "" == keyWord || p.crkfsmc.Contains(keyWord.Trim())).ToList();
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

        public IActionResult DeleteForm(string keyValue)
        {
            return _wzCrkfsRepo.DeleteCrkfsById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            wzCrkfsEntity.zt = wzCrkfsEntity.zt == "true" ? "1" : "0";
            return _wzCrkfsApp.SubmitForm(wzCrkfsEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
    }
}
