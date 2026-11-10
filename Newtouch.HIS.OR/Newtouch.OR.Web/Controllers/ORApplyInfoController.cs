using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using System;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    // G25: doi sang OrgControllerBase (nhu 5 controller OR* con lai) de
    // lay OrganizeId tu session — base BaseController khong co property nay.
    public class ORApplyInfoController : OrgControllerBase
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
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm(ORApplyInfoEntity entity, string keyValue)
        {
            // G25: mac dinh co so tu session (mau Op* G23/G24) — base
            // entity.OrganizeId null nen tao moi loi SQL OrganizeId NULL.
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            // G25: sua phai ton tai trong cung co so (repo cung loc org).
            if (!string.IsNullOrEmpty(keyValue) && keyValue != "null")
            {
                var dbEntity = _oRApplyInfoRepo.FindEntity(p => p.Id == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
                if (dbEntity == null)
                {
                    return Error("Không tìm thấy đơn phẫu thuật hợp lệ!");
                }
            }
            _oRApplyInfoRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue, string ssmcn)
        {
            // G25: khoi phuc repo call (base comment out nen huy don luon
            // tra Success gia); kiem tra ton tai trong cung co so truoc
            // (mau OpApply/DeleteApply G23). ssmcn de repo doi chieu lenh
            // dai han CIS zy_lsyz (xmmc), co the rong.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Error("Không tìm thấy đơn phẫu thuật hợp lệ!");
            }
            var dbEntity = _oRApplyInfoRepo.FindEntity(p => p.Applyno == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
            if (dbEntity == null)
            {
                return Error("Không tìm thấy đơn phẫu thuật hợp lệ!");
            }
            _oRApplyInfoRepo.DeleteForm(keyValue, OrganizeId, ssmcn);
            return Success("Hủy đơn thành công");
        }
    }
}