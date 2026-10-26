using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Đăng ký phẫu thuật
    /// </summary>
    public class ORRegistrationController : OrgControllerBase
    {
        private readonly IORRegistrationRepo _oRRegistrationRepo;

        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="oRRegistrationRepo"></param>
        public ORRegistrationController(IORRegistrationRepo oRRegistrationRepo)
        {
            this._oRRegistrationRepo = oRRegistrationRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRRegistrationRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(RegistrationListVO entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            //_oRRegistrationRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _oRRegistrationRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}