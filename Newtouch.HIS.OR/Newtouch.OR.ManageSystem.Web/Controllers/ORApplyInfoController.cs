using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using System.Web.Mvc;
using Newtouch.Tools;
using System;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    public class ORApplyInfoController : BaseController
    {
        //// GET: ORApplyInfo
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private readonly IORApplyInfoRepo _oRApplyInfoRepo;

        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// <param name="oRApplyInfoRepo"></param>
        public ORApplyInfoController(IORApplyInfoRepo oRApplyInfoRepo)
        {
            this._oRApplyInfoRepo = oRApplyInfoRepo;
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var entity = _oRApplyInfoRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu（Thêm mới、Sửa）
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            _oRApplyInfoRepo.SubmitForm(entity, keyValue);
            return Success("操作成功。");
        }

        private ActionResult Success(string v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            //_oRApplyInfoRepo.DeleteForm(keyValue);
            return Success("操作成功。");
        }
    }
}