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
    /// Mô tả: Trang đầu bệnh án
    /// </summary>
    public class MrbasyController : OrgControllerBase
    {
        private readonly IMrbasyRepo _mrbasyRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="mrbasyRepo"></param>
        public MrbasyController(IMrbasyRepo mrbasyRepo)
        {
            this._mrbasyRepo = mrbasyRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _mrbasyRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(MrbasyEntity entity, string keyValue)
        {
            _mrbasyRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _mrbasyRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}