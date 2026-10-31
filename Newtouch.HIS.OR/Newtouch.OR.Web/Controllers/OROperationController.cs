using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using System;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-10-31 14:15
    /// Mô tả:Bảng từ điển phẫu thuật
    /// </summary>
    public class OROperationController : NewtouchHIS.Framework.Web.Controllers.OrgControllerBase
    {
        private readonly IOROperationRepo _oROperationRepo;

        /// <summary>
        /// Hàm tạo mặc định
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
        /// <summary>
        /// QA phase 2 (O-01): controller goc chi co action JSON, khong co view Index
        /// (lop co so tra View() nen route nay truoc day loi "view not found").
        /// Chuyen ve trang chinh cua module thay vi tao view gia.
        /// </summary>
        public override IActionResult Index()
        {
            return Redirect("/Operation/OpOperation/Index");
        }

        public IActionResult GetPagintionGridJson(Pagination pagination, string keyword)
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
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _oROperationRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm(OROperationEntity entity, string keyValue)
        {
            _oROperationRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _oROperationRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }

    }
}