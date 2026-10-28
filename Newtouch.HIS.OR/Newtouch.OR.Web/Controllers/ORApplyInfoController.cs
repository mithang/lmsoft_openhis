using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    public class ORApplyInfoController : BaseController
    {
        //// GET: ORApplyInfo
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IORApplyInfoRepo _oRApplyInfoRepo;

        /// <summary>
        /// Hàm tạo mặc định
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
        /// <summary>
        /// QA phase 2 (O-01): controller goc chi co action JSON, khong co view Index
        /// (lop co so tra View() nen route nay truoc day loi "view not found").
        /// Chuyen ve trang chinh cua module thay vi tao view gia.
        /// </summary>
        public override IActionResult Index()
        {
            return Redirect("/Operation/OpApply/ApplyIndex");
        }

        public IActionResult GetFormJson(string keyValue)
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
        public IActionResult SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            _oRApplyInfoRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            //_oRApplyInfoRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công.");
        }
    }
}