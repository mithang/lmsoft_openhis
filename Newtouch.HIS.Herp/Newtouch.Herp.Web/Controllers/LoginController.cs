using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Đăng nhập/đăng xuất. Core login goes through LoginBaseController.CheckLogin
    /// -&gt; BaseAPI. OprBeforeSave is the real Herp warehouse enrichment (ported
    /// from the original LoginController): it loads the user's warehouses and
    /// stores the current-warehouse context; users without a warehouse who are
    /// not administrators are rejected, exactly as the original did.
    /// UALogin is restored with the original's pre-exchange branches (empty
    /// token -&gt; login page; already-logged-in same token -&gt; returnUrl/Home).
    /// The full token-exchange success path is NOT reproduced: it called an
    /// external UAUserInfoGetAPI host and QuickLoginSuccess, neither of which
    /// exists in the .NET 6 base or this deployment's config, so a token that
    /// is not already the current session token falls through to the login
    /// page exactly like the original's missing-config / failed-fetch branch.
    /// No fake identity is ever created here.
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        private readonly IWarehouseDmnService _warehouseDmnService;

        public LoginController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn,
            IWarehouseDmnService warehouseDmnService) : base(loginApp, sysUserVDmn)
        {
            _warehouseDmnService = warehouseDmnService;
        }

        public override void OprBeforeSave(OperatorModel opr)
        {
            if (string.IsNullOrWhiteSpace(opr.UserId) || string.IsNullOrWhiteSpace(opr.OrganizeId)) return;
            var kflist = _warehouseDmnService.GetKfUserInfo(opr.UserId, opr.OrganizeId);
            if (kflist != null && kflist.Count > 0)
            {
                var currentKfInfo = new LoginUserCurrentKfModel();
                foreach (var item in kflist)
                {
                    currentKfInfo.kfList.Add(new KfInfoModel
                    {
                        kfId = item.kfId,
                        kfName = item.kfName,
                        kfLeve = item.kfLeve
                    });
                }
                List<KfInfoModel> distinctkf =
                    currentKfInfo.kfList.Where((x, i) => currentKfInfo.kfList.FindIndex(z => z.kfId == x.kfId && z.kfName == x.kfName && z.kfLeve == x.kfLeve) == i).ToList();
                currentKfInfo.kfList = distinctkf;
                currentKfInfo.userId = opr.UserId;
                currentKfInfo.currentKfId = kflist[0].kfId;
                currentKfInfo.currentKfName = kflist[0].kfName;
                currentKfInfo.currentKfLevel = kflist[0].kfLeve;
                currentKfInfo.dutyName = kflist[0].dutyName;
                currentKfInfo.gh = kflist[0].gh;
                currentKfInfo.OrganizeId = opr.OrganizeId;
                currentKfInfo.staffName = kflist[0].staffName;
                Constants.SetCurrentKf(opr.UserId, currentKfInfo);
            }
            else
            {
                if (opr.IsAdministrator || opr.IsHospAdministrator || opr.IsRoot)
                {
                    return;
                }
                throw new FailedException("Tài khoản chưa được cấu hình kho, vui lòng liên hệ quản trị viên!");
            }
        }

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất (UA callback, port từ bản gốc).
        /// Chỉ khôi phục các nhánh không cần máy chủ UA ngoài: thiếu token
        /// hoặc chưa cấu hình UA đều về trang đăng nhập như bản gốc; token
        /// trùng phiên hiện tại thì về returnUrl/trang chủ.
        /// </summary>
        [HttpGet]
        public IActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login");
            }

            var currentToken = OperatorProvider.GetCurrentToken();
            if (!string.IsNullOrWhiteSpace(currentToken) && currentToken == access_token)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            // Không có máy chủ UAUserInfoGetAPI trong cấu hình triển khai này
            // (và bản gốc cũng về trang đăng nhập khi chưa cấu hình hoặc
            // đổi danh tính thất bại): không tạo danh tính giả.
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public override IActionResult Index()
        {
            //XóaSSOdấu nguồn đăng nhập
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }
    }
}
