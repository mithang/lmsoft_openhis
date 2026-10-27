using Newtouch.HIS.Application.Interface;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// Phương thức nhập/xuất kho
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

        /// <summary>
        /// Lấy danh sách phương thức nhập/xuất kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetCrkfsGridJson(Pagination pagination, string keyWord = "")
        {
            pagination.sidx = "crkfsmc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _wzCrkfsRepo.FindList(p => "" == keyWord || p.crkfsmc.Contains(keyWord.Trim()), pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            return _wzCrkfsRepo.DeleteCrkfsById(keyValue) > 0 ? Success("Xóa thành công") : Error("Xóa thất bại");
        }

        /// <summary>
        /// Lấy thông tin phương thức nhập/xuất kho
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyWord)
        {
            var supplier = _wzCrkfsRepo.FindEntity(p => p.Id == keyWord);
            return Content(supplier.ToJson());
        }

        /// <summary>
        /// Bảo trì đơn vị vật tư Gửi biểu mẫu
        /// </summary>
        /// <param name="wzCrkfsEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [HandlerAjaxOnly]
        public IActionResult SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            wzCrkfsEntity.zt = wzCrkfsEntity.zt == "true" ? "1" : "0";
            return _wzCrkfsApp.SubmitForm(wzCrkfsEntity, keyWord) > 0 ? Success("Thao tác thành công") : Error("Thao tác thất bại");
        }
    }
}