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
    /// Mô tả: Bảng ghi phẫu thuật trang đầu bệnh án
    /// </summary>
    public class MrbasyssController : OrgControllerBase
    {
        private readonly IMrbasyssRepo _mrbasyssRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="mrbasyssRepo"></param>
        public MrbasyssController(IMrbasyssRepo mrbasyssRepo)
        {
            this._mrbasyssRepo = mrbasyssRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _mrbasyssRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(MrbasyssEntity entity, string keyValue)
        {
            _mrbasyssRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _mrbasyssRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}