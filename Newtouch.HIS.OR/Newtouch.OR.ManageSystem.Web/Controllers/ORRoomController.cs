using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Tạo lập：chl
    /// Ngày tạo：2019-11-06 10:22
    /// Mô tả：Phòng mổ
    /// </summary>
    public class ORRoomController : OrgControllerBase
    {
        private readonly IORRoomRepo _oRRoomRepo;

        /// <summary>
        /// Hàm khởi tạo mặc định
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
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRRoomRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(ORRoomEntity entity, string keyValue)
        {
            _oRRoomRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            _oRRoomRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }

    }
}