using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;

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
        private readonly IORArrangementRepo _oRArrangementRepo;
        private readonly IOROpStaffRecordRepo _oROpStaffRecordRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="oRRegistrationRepo"></param>
        public ORRegistrationController(IORRegistrationRepo oRRegistrationRepo, IORArrangementRepo oRArrangementRepo, IOROpStaffRecordRepo oROpStaffRecordRepo)
        {
            this._oRRegistrationRepo = oRRegistrationRepo;
            this._oRArrangementRepo = oRArrangementRepo;
            this._oROpStaffRecordRepo = oROpStaffRecordRepo;
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
            // G25: khoi phuc repo call (base comment out nen luon tra
            // Success gia); validate nhu OpRegister/submitForm (G24) tru
            // phan staff records — duong OR* chi luu dang ky, khong tao
            // staff (repo khong lam; OpRegister lam o controller qua
            // GetStafflist doc demo DB — hardcode ho FA-006-B4).
            var isEdit = !(string.IsNullOrWhiteSpace(keyValue) || keyValue == "null");
            ORRegistrationEntity regEntity = null;
            if (isEdit)
            {
                regEntity = _oRRegistrationRepo.FindEntity(keyValue);
                if (regEntity == null || regEntity.OrganizeId != OrganizeId || regEntity.zt != "1")
                {
                    return Error("Không tìm thấy đăng ký phẫu thuật");
                }
                if (string.IsNullOrWhiteSpace(entity.arrangeId))
                {
                    entity.arrangeId = regEntity.ArrangeId;
                }
                if (entity.arrangeId != regEntity.ArrangeId)
                {
                    return Error("Không thể đổi lịch xếp của đăng ký phẫu thuật");
                }
            }
            // Validate lich TRUOC KHI dot serial / ghi gi ca (mau G24).
            if (string.IsNullOrWhiteSpace(entity.arrangeId))
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            var arrangement = _oRArrangementRepo.FindEntity(entity.arrangeId);
            if (arrangement == null || arrangement.OrganizeId != OrganizeId || arrangement.zt != "1")
            {
                return Error("Không tìm thấy lịch xếp phẫu thuật");
            }
            if (!isEdit && arrangement.sqzt != "2")
            {
                return Error("Lịch phẫu thuật đã được đăng ký hoặc đã hủy");
            }
            string ssxh;
            if (isEdit)
            {
                ssxh = string.IsNullOrWhiteSpace(entity.ssxh) ? regEntity.ssxh : entity.ssxh;
            }
            else
            {
                ssxh = EFDBBaseFuncHelper.GetOPSsxh(OrganizeId);
            }
            _oRRegistrationRepo.SubmitForm(entity, keyValue, ssxh);
            // Chuyen lich sang trang thai da dang ky (mau OpRegister G24).
            _oRArrangementRepo.UpdateSqzt(entity.arrangeId, "4");
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            // G25: validate tat ca TRUOC KHI doi gi ca (mau OpRegister/
            // DeleteData G24); arrangeId/ssxh lay tu chinh dang ky, khong
            // lay tu client. Dang ky khong co lich (ArrangeId rong — dong
            // cu) thi bo qua buoc tra lich, van xoa staff + dang ky.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Không tìm thấy đăng ký phẫu thuật");
            }
            var dbEntity = _oRRegistrationRepo.FindEntity(keyValue);
            if (dbEntity == null || dbEntity.OrganizeId != OrganizeId || dbEntity.zt != "1")
            {
                return Error("Không tìm thấy đăng ký phẫu thuật");
            }
            if (!string.IsNullOrWhiteSpace(dbEntity.ArrangeId))
            {
                var arrangement = _oRArrangementRepo.FindEntity(dbEntity.ArrangeId);
                if (arrangement == null || arrangement.OrganizeId != OrganizeId || arrangement.zt != "1")
                {
                    return Error("Không tìm thấy lịch xếp phẫu thuật");
                }
                // Tra lich ve trang thai da xep.
                _oRArrangementRepo.UpdateSqzt(dbEntity.ArrangeId, "2");
            }
            // Xoa ho so nhan vien theo ssxh cua chinh dang ky.
            var list = _oROpStaffRecordRepo.getIdBySsxh(dbEntity.ssxh);
            foreach (var obj in list)
            {
                _oROpStaffRecordRepo.DeleteForm(obj.Id);
            }
            _oRRegistrationRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }

    }
}