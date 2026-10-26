using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.MR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 13:45
    /// Mô tả: Khoa bệnh án
    /// </summary>
    public class MrdicdeptController : OrgControllerBase
    {
        private readonly IMrdicdeptRepo _mrdicdeptRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="mrdicdeptRepo"></param>
        public MrdicdeptController(IMrdicdeptRepo mrdicdeptRepo)
        {
            this._mrdicdeptRepo = mrdicdeptRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _mrdicdeptRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(MrdicdeptEntity entity, string keyValue)
        {
            _mrdicdeptRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _mrdicdeptRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}