using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo bởi：chl
    /// Ngày：2019-11-06 10:22
    /// Mô tả：Hồ sơ xếp lịch phẫu thuật
    /// </summary>
    public class ORArrangementController : OrgControllerBase
    {
        private readonly IORArrangementRepo _oRArrangementRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="oRArrangementRepo"></param>
        public ORArrangementController(IORArrangementRepo oRArrangementRepo)
        {
            this._oRArrangementRepo = oRArrangementRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        /// <summary>
        /// QA phase 2 (O-01): controller goc chi co action JSON, khong co view Index
        /// (lop co so tra View() nen route nay truoc day loi "view not found").
        /// Chuyen ve trang chinh cua module thay vi tao view gia.
        /// </summary>
        public override IActionResult Index()
        {
            return Redirect("/Operation/OpArrangement/Index");
        }

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _oRArrangementRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        //public IActionResult SubmitForm(ORArrangementEntity entity, string keyValue)
        //{
        //    _oRArrangementRepo.SubmitForm(entity, keyValue);
        //    return Success("Thao tác thành công。");
        //}

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _oRArrangementRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }

    }
}