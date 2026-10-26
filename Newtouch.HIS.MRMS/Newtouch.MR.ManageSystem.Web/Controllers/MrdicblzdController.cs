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
    /// Mô tả: Chẩn đoán bệnh lý
    /// </summary>
    public class MrdicblzdController : OrgControllerBase
    {
        private readonly IMrdicblzdRepo _mrdicblzdRepo;

        /// <summary>
        /// Hàm tạo mặc định
        /// </summary>
        /// <param name="mrdicblzdRepo"></param>
        public MrdicblzdController(IMrdicblzdRepo mrdicblzdRepo)
        {
            this._mrdicblzdRepo = mrdicblzdRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _mrdicblzdRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu (thêm mới, sửa đổi）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(MrdicblzdEntity entity, string keyValue)
        {
            _mrdicblzdRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _mrdicblzdRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}