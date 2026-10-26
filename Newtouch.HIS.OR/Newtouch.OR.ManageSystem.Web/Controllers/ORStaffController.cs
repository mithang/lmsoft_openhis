using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Nhân viên phẫu thuật
    /// </summary>
    public class ORStaffController : OrgControllerBase
    {
        private readonly IORStaffRepo _oRStaffRepo;

        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="oRStaffRepo"></param>
        public ORStaffController(IORStaffRepo oRStaffRepo)
        {
            this._oRStaffRepo = oRStaffRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRStaffRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(ORStaffEntity entity, string keyValue)
        {
            _oRStaffRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _oRStaffRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}