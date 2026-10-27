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
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Đăng nhập/đăng xuất. SSO/UA callback actions from the Framework version
    /// are dropped: they depend on external SSO/UA hosts that do not exist in
    /// this deployment. Core login goes through LoginBaseController.CheckLogin
    /// -&gt; BaseAPI. OprBeforeSave is the real Herp warehouse enrichment (ported
    /// from the original LoginController): it loads the user's warehouses and
    /// stores the current-warehouse context; users without a warehouse who are
    /// not administrators are rejected, exactly as the original did.
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

        [HttpGet]
        public override IActionResult Index()
        {
            //移除SSO登录来源标记
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }
    }
}
