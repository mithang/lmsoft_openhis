using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    /// <summary>
    /// Hình thức nhập/xuất kho
    /// </summary>
    public class ProductCrkfsController : ControllerBase
    {
        private readonly IWzCrkfsRepo _wzCrkfsRepo;
        private readonly IWzCrkfsApp _wzCrkfsApp;

        /// <summary>
        /// Lấy danh sách hình thức nhập/xuất kho
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetCrkfsGridJson(Pagination pagination, string keyWord = "")
        {
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
        public ActionResult DeleteForm(string keyValue)
        {
            return _wzCrkfsRepo.DeleteCrkfsById(keyValue) > 0 ? Success("删除成功") : Error("删除失败");
        }

        /// <summary>
        /// Lấy thông tin hình thức nhập/xuất kho
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyWord)
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
        public ActionResult SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord)
        {
            wzCrkfsEntity.zt = wzCrkfsEntity.zt == "true" ? "1" : "0";
            return _wzCrkfsApp.SubmitForm(wzCrkfsEntity, keyWord) > 0 ? Success("操作成功") : Error("操作失败");
        }
    }
}