using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Core.Common;
using System;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-10-31 14:15
    /// Mô tả：Bảng từ điển phẫu thuật
    /// </summary>
    public class OROperationController : Controller
    {
        private readonly IOROperationRepo _oROperationRepo;

        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="oROperationRepo"></param>
        public OROperationController(IOROperationRepo oROperationRepo)
        {
            this._oROperationRepo = oROperationRepo;
        }

        /// <summary>
        /// Lấy danh sách thực thể phân trang
        /// </summary>
        /// <param name="pagination">Phân trang</param>
        /// <param name="keyword">Từ khóa lọc</param>
        /// <returns></returns>
        public ActionResult GetPagintionGridJson(Pagination pagination, string keyword)
        {
            var list = new
            {
                rows = _oROperationRepo.GetPagintionList(pagination, keyword,""),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oROperationRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(OROperationEntity entity, string keyValue)
        {
            _oROperationRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        private ActionResult Success(string v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _oROperationRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}