using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo bởi:chl
    /// Ngày:2019-11-06 10:22
    /// Mô tả:Phòng phẫu thuật
    /// </summary>
    public class ORRoomController : OrgControllerBase
    {
        private readonly IORRoomRepo _oRRoomRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="oRRoomRepo"></param>
        public ORRoomController(IORRoomRepo oRRoomRepo)
        {
            this._oRRoomRepo = oRRoomRepo;
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
            return Redirect("/Operation/OpRoom/Index");
        }

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _oRRoomRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm(ORRoomEntity entity, string keyValue)
        {
            // G25: mac dinh co so tu session (mau OpRoom/submitForm G24).
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            _oRRoomRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            // G25: khoa rong/khong ton tai/khac co so -> loi ro rang
            // (mau OpRoom/DeleteData G24).
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Không tìm thấy phòng phẫu thuật");
            }
            var dbEntity = _oRRoomRepo.FindEntity(p => p.Id == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
            if (dbEntity == null)
            {
                return Error("Không tìm thấy phòng phẫu thuật");
            }
            _oRRoomRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }

    }
}