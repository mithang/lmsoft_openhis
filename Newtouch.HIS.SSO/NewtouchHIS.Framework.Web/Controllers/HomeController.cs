using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Framework.Web.Controllers
{
    public abstract class HomeController : OrgControllerBase
    {
        private readonly IOrganizeAppService _organizeApp;
        private readonly BusinessConfig _config = ConfigInitHelper.SysConfig;
        public HomeController(IOrganizeAppService organizeApp)
        {
            _organizeApp = organizeApp;
        }
        public override IActionResult Index()
        {
            return View();
        }

        public virtual IActionResult DashBoard()
        {
            var nLanguageTypeKey = _config.AppId + "_" + "Newtouch_language_type";
            HttpWebHelper.RemoveCookie(nLanguageTypeKey);
            if (!string.IsNullOrWhiteSpace(UserIdentity?.OrganizeId))
            {
                //hiện tạiOrgtên（liên kếtStaff）
                ViewBag.OrgName = _organizeApp.GetNameByOrgId(this.UserIdentity.OrganizeId);
            }
            else
            {

                //hiển thị tên tổ chức cấp cao nhất
                ViewBag.OrgName = _organizeApp.GetNameByOrgId(this.UserIdentity.TopOrganizeId);
            }

            //if (!this.UserIdentity.IsRoot && !this.UserIdentity.IsAdministrator)    //hằngtrue
            //{
            //    //hiện tạiUserId tất cả những cái tương ứng cơ sở y tế List
            //    var mediOrgList = _organizeApp.GetMedicalOrganizeListByUserId(this.UserIdentity.UserId);

            //    ViewBag.MediOrgList = mediOrgList;

            //    if (mediOrgList != null && mediOrgList.Count() > 1)
            //    {
            //        //khi tải trang có bắt buộc phải chọn hay khôngOrg
            //        var needChooseOrg = HttpWebHelper.GetCookie(_config.AppId + "_" + "CookieKey_ChooseOrg");
            //        ViewBag.NeedChooseOrg = !string.IsNullOrWhiteSpace(needChooseOrg);
            //    }
            //}

            IndexBeforeReturnView();

            return View();
        }

        /// <summary>
        /// Index return Viewtrước đó
        /// </summary>
        public virtual void IndexBeforeReturnView()
        {

        }
    }
}
