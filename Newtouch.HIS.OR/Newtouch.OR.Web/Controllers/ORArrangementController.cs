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
    /// Mô tả:Hồ sơ xếp lịch phẫu thuật
    /// </summary>
    public class ORArrangementController : OrgControllerBase
    {
        private readonly IORArrangementRepo _oRArrangementRepo;
        private readonly IORApplyInfoRepo _oRApplyInfoRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="oRArrangementRepo"></param>
        public ORArrangementController(IORArrangementRepo oRArrangementRepo, IORApplyInfoRepo oRApplyInfoRepo)
        {
            this._oRArrangementRepo = oRArrangementRepo;
            this._oRApplyInfoRepo = oRApplyInfoRepo;
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
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        //public IActionResult SubmitForm(ORArrangementEntity entity, string keyValue)
        //{
        //    _oRArrangementRepo.SubmitForm(entity, keyValue);
        //    return Success("Thao tác thành công.");
        //}

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            // G25: khoa rong/khong co lich trong cung co so -> loi ro rang
            // (mau OpOperation/DeleteData G24); tra don ve cho duyet
            // (sqzt=1) khi lich vua huy dang o trang thai da xep (sqzt=2)
            // (mau OpArrangement/DeleteForm G23).
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            var dbEntity = _oRArrangementRepo.FindEntity(p => p.Id == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
            if (dbEntity == null)
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            var wasArranged = dbEntity.sqzt == "2";
            var applyId = dbEntity.ApplyId;
            _oRArrangementRepo.DeleteForm(keyValue);
            if (wasArranged)
            {
                _oRApplyInfoRepo.UpdateSqzt(applyId, "1");
            }
            return Success("Thao tác thành công.");
        }

    }
}