using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Đăng ký phẫu thuật
    /// </summary>
    public class ORRegistrationController : OrgControllerBase
    {
        private readonly IORRegistrationRepo _oRRegistrationRepo;

        /// <summary>
        /// Hàm tạo mặc định
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
        /// <summary>
        /// QA phase 2 (O-01): controller goc chi co action JSON, khong co view Index
        /// (lop co so tra View() nen route nay truoc day loi "view not found").
        /// Chuyen ve trang chinh cua module thay vi tao view gia.
        /// </summary>
        public override IActionResult Index()
        {
            return Redirect("/Operation/OpRegister/Index");
        }

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _oRRegistrationRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm(RegistrationListVO entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            //_oRRegistrationRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _oRRegistrationRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }

    }
}