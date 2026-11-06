using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.Log;
using Newtouch.Infrastructure.Common;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Core.Common;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Tools;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;
using Newtouch.Infrastructure.Model;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Core.Common.Utils;

using System.Diagnostics;
using Newtouch.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtouch.Common.Web;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Đăng nhập
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysUserRepo _sysUserRepo;

        public LoginController(NewtouchHIS.Framework.Web.Implementation.ILoginAppService loginApp,
            NewtouchHIS.Base.Domain.Organize.ISysUserVDmnService sysUserVDmn,
            ISysUserExDmnService sysUserDmnService,
            ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo,
            ISysUserRepo sysUserRepo) : base(loginApp, sysUserVDmn)
        {
            _sysUserDmnService = sysUserDmnService;
            _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
            _sysUserRepo = sysUserRepo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override IActionResult Index()
        {
            //Ghi vào máy kháchId
            Response.Cookies.Append(Constants.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            ViewBag.IsCheckChkCode = ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // NOTE (FA-010, theo FA-006/FA-008): no own OutLogin here on purpose.
        // LoginBaseController.OutLogin is non-virtual; redeclaring it made routing
        // ambiguous (AmbiguousMatchException, HTTP 500 on every /Login/OutLogin)
        // because ASP.NET Core discovers both the base and the hiding method as
        // actions. Base OutLogin removes the session and redirects to /Login/Index.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opr"></param>
        public override void OprBeforeSave(OperatorModel opr)
        {
            //Cập nhật phần nhà thuốc người dùng
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_UserYfbmRelated") == true && opr.OrganizeId != null)
            {
                //Bộ phận nhà thuốc hiện cần thao tác
                if (!string.IsNullOrWhiteSpace(opr.UserId) && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId)?.ToList();
                }
                if (opr.yfbmCodeList != null && opr.yfbmCodeList.Count > 0)
                {
                    //Bộ phận nhà thuốc hiện cần thao tác    //tạiOrgIdĐã liên kết nhiềuyfbmLàm thế nào
                    var curYfbmCode = opr.yfbmCodeList.FirstOrDefault();
                    var curYfbmObj = _sysPharmacyDepartmentRepo.GetUserYfbmByCode(curYfbmCode, opr.OrganizeId);
                    if (curYfbmObj != null)
                    {
                        Constants.SetCurrentYfbm(opr.UserId, new LoginUserCurrentYfbmModel()
                        {
                            yfbmCode = curYfbmCode,
                            yfbmjb = curYfbmObj.yfbmjb,
                            mzzybz = curYfbmObj.mzzybz,
                            yfbmmc = curYfbmObj.yfbmmc
                        });
                    }
                }
                else
                {
                    Constants.SetCurrentYfbm(opr.UserId, null);  //Loại bỏ
                }
            }

            //
            Response.Cookies.Append(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", opr.IsHospAdministrator.ToString().ToLower());

            //
            var rpHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            if (!string.IsNullOrWhiteSpace(rpHost))
            {
                Response.Cookies.Append(Constants.AppId + "_" + "ReportServer_HOST", rpHost);
            }
            else
            {
                Response.Cookies.Delete(Constants.AppId + "_" + "ReportServer_HOST");
            }

        }


        #region Đăng nhập ủy quyền thống nhất

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login");  //Thiếu mã thông báo
            }
            var eOpr = OperatorProvider.GetCurrent();
            if (eOpr != null && eOpr.token == access_token)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            OperatorModel opr = null;
            try
            {
                var json = NewtouchHIS.Lib.Base.Utilities.RedisHelper.GetString(access_token);
                if (!string.IsNullOrWhiteSpace(json) && json != "SIDELINED")
                {
                    opr = JsonConvert.DeserializeObject<OperatorModel>(json);
                }
            }
            catch
            {
                opr = null;
            }
            if (opr == null || (string.IsNullOrWhiteSpace(opr.UserId) && string.IsNullOrWhiteSpace(opr.UserCode)))
            {
                return RedirectToAction("Index", "Login"); // Token het han/khong hop le
            }

            OperatorProvider.AddCurrent(opr);
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Lấy địa chỉ chuyển hướng ủy quyền thống nhất
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        // NOTE (FA-010, theo FA-008): no own GetUALoginAddres here on purpose.
        // LoginBaseController.GetUALoginAddres is non-virtual and resolves the
        // address from BusinessConfig (success + null data when unconfigured);
        // redeclaring it made routing ambiguous (AmbiguousMatchException,
        // HTTP 500 on every /Login/GetUALoginAddres) because ASP.NET Core
        // discovers both the base and the hiding method as actions.
        // PDS appsettings defines no {appId}_UALoginAddress key, so the derived
        // lookup always returned null data here; base is a strict improvement.


        #endregion

        private void WriteOutLoginLog()
        {
            try { AppLogger.Info(string.Format("{0}Thoát đăng nhập", Constants.AppId)); } catch { }
        }

    }
}
