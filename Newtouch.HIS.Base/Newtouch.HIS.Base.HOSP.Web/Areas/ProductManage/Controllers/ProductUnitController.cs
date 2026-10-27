using Newtouch.HIS.Application.Interface;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// 物资单位
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

        /// <summary>
        /// 获取物资单位列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetUnitGridJson(Pagination pagination, string keyWord = "")
        {
            pagination.sidx = "name";
            pagination.sord = "asc";
            var data = new
            {
                rows = _wzUnitRepo.FindList(p => "" == keyWord || p.name.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// get enable product unit 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProductUnitSelectJson()
        {
            var list = _wzUnitRepo.IQueryable(p => p.zt == "1").ToList();
            return Content(list.ToJson());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            return _wzUnitRepo.DeleteUnitById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// get unit information
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzUnitRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// 物资单位维护 表单提交
        /// </summary>
        /// <param name="wzUnitEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzUnitEntity wzUnitEntity, string keyWord)
        {
            wzUnitEntity.zt = wzUnitEntity.zt == "true" ? "1" : "0";
            return _wzUnitApp.SubmitForm(wzUnitEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
    }
}