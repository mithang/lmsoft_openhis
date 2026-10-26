using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.MR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Bảng chẩn đoán trang đầu bệnh án
    /// </summary>
    public class MrbasyzdController : OrgControllerBase
    {
        private readonly IMrbasyzdRepo _mrbasyzdRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="mrbasyzdRepo"></param>
        public MrbasyzdController(IMrbasyzdRepo mrbasyzdRepo)
        {
            this._mrbasyzdRepo = mrbasyzdRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _mrbasyzdRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(MrbasyzdEntity entity, string keyValue)
        {
            _mrbasyzdRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _mrbasyzdRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}