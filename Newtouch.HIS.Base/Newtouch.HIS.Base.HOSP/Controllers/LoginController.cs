using Newtouch.Common.Operator;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using Newtouch.HIS.Web.Core.Url;
using Newtonsoft.Json.Linq;
using Newtouch.Common.Web;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : Controller
    {
        private readonly ISysUserApp _sysUserApp;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;
        private readonly ISysUserRoleRepo _sysUserRoleRepository;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysLogRepository _sysLogRepository;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysDepartmentRepository _sysDepartmentRepository;

        /// <summary>
        /// 
        /// </summary>
        public LoginController(ISysUserApp sysUserApp, ISysUserRepo sysUserRepo, ISysUserLogOnRepository sysUserLogOnRepository
            , ISysUserRoleRepo sysUserRoleRepository, ISysUserDmnService sysUserDmnService
            , ISysLogRepository sysLogRepository, IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysDepartmentRepository sysDepartmentRepository)
        {
            this._sysUserApp = sysUserApp;
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysUserRoleRepository = sysUserRoleRepository;
            this._sysUserDmnService = sysUserDmnService;
            this._sysLogRepository = sysLogRepository;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysDepartmentRepository = sysDepartmentRepository;
        }

        /// <summary>
        /// 1.Callback đăng nhập một lần，Nhận token，Truy cập đăng nhập một lần API để lấy UserId
        /// </summary>
        /// <returns></returns>
        public ActionResult SSOCallback(string access_token)
        {
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(
                SiteUrl.GetSSOAPIUrl("api/Auth/GetOpenId"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            var openId = "";
            var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
            foreach (JProperty prop in jObj.Properties())
            {
                if (prop.Name == "openId")
                {
                    openId = prop.Value.ToString();
                }
            }
            WebHelper.WriteCookie(Constants.AppId + "_OpenId", openId);
            return RedirectToAction("CheckLogin");
        }

        /// <summary>
        /// 2.Xác minh đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ActionResult CheckLogin()
        {
            var username = WebHelper.GetCookie(Constants.AppId + "_OpenId");
            WebHelper.RemoveCookie(Constants.AppId + "_OpenId");
            //Nhật ký đăng nhậpEntity
            var logEntity = new SysLogEntity
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = Constants.TopOrganizeId,
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = username,
            };
            SysUserEntity userEntity = null;
            if (username == "root")
            {
                userEntity = new SysUserEntity()
                {
                    Account = "root"
                };
            }
            else
            {

                userEntity = _sysUserRepo.GetEntity(Constants.TopOrganizeId, username);
            }
            if (userEntity != null)
            {
                SaveLoginSuccessStatus(userEntity, logEntity);
                _sysLogRepository.Insert(logEntity);
                //Dùng để đánh dấu nguồn đăng nhập của người dùng
                WebHelper.WriteCookie(Constants.AppId + "_" + username + "_" + "LoginFlag", EnumLoginFlag.SSO.ToString());

                return RedirectToAction("Index", "Home");
            }
            else {
                return View();
            }
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            //Ghi vào clientId
            WebHelper.WriteCookie(Constants.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            ViewBag.IsCheckChkCode = ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode");

            return View();
        }

        [HttpGet]
        public ActionResult GetAuthCode()
        {
            var clientId = WebHelper.GetCookie(Constants.AppId + "_ClientId");
            string code;
            var stream = new VerifyCode().GetVerifyCode(out code);

            //Mã xác minh 5Hiệu lực trong vòng phút
            RedisHelper.StringSet(clientId + "_chkCode", code, new TimeSpan(0, 5, 0));

            AppLogger.Info(string.Format("生成验证码：{0}", code));

            return File(stream, @"image/Gif");
        }

        [HttpGet]
        public ActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                _sysLogRepository.Insert(new SysLogEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    TopOrganizeId = Constants.TopOrganizeId,
                    Date = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModuleName = "系统登录",
                    Type = DbLogType.Exit.ToString(),
                    Account = opr.UserCode,
                    NickName = opr.UserName,
                    Result = true,
                    Description = "安全退出系统",
                });
                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();
            var userCode = "";
            if (opr != null) { userCode = opr.UserCode; }
            var cookieLoginFlag = WebHelper.GetCookie(Constants.AppId + "_" + userCode + "_" + "LoginFlag");
            if (!string.IsNullOrWhiteSpace(cookieLoginFlag))
            {
                WebHelper.RemoveCookie(Constants.AppId + "_" + userCode + "_" + "LoginFlag");

                var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");  //ThoátSSOĐăng nhập

                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        /// <summary>
        /// Quay về trang chủ đăng nhập một lần
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReturnSSOHome()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                _sysLogRepository.Insert(new SysLogEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    TopOrganizeId = Constants.TopOrganizeId,
                    Date = DateTime.Now,
                    CreateTime = DateTime.Now,
                    ModuleName = "系统登录",
                    Type = DbLogType.Exit.ToString(),
                    Account = opr.UserCode,
                    NickName = opr.UserName,
                    Result = true,
                    Description = "安全退出系统",
                });
                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();
            var userCode = "";
            if (opr != null) { userCode = opr.UserCode; }
            WebHelper.RemoveCookie(Constants.AppId + "_" + userCode + "_" + "LoginFlag");

            var url = ConfigurationHelper.GetAppConfigValue("SSOHomeHost"); ;  //Chuyển tới trang đăng nhập một lần

            return Redirect(url);
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckLogin(string username, string password, string code)
        {
            //Nhật ký đăng nhậpEntity
            var logEntity = new SysLogEntity
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = Constants.TopOrganizeId,
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = username,
            };
            try
            {

                if (ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode") ?? true)
                {
                    //Mã xác minh đăng nhập Xác thực
                    var clientId = WebHelper.GetCookie(Constants.AppId + "_ClientId");
                    var chkCode = RedisHelper.StringGet(clientId + "_chkCode");
                    if (string.IsNullOrWhiteSpace(chkCode) || code.ToLower() != chkCode.ToLower())
                    {
                        AppLogger.Instance.Info(string.Format("验证码错误：input:{0}-db:{1}"
                            , code, chkCode));
                        throw new FailedException("验证码错误，请重新输入");
                    }
                }
                SysUserEntity userEntity = null;
                SysStaffEntity staff = null;
                if (username == "root")
                {
                    userEntity = new SysUserEntity()
                    {
                        Account = username
                    };
                }
                else
                {
                    userEntity = _sysUserApp.CheckLogin(username, password);
                }

                if (userEntity != null)
                {
                    SaveLoginSuccessStatus(userEntity, logEntity);
                }
                else
                {
                    throw new FailedException("登录失败");
                }

                _sysLogRepository.Insert(logEntity);

                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。" }.ToJson());
            }
            catch (FailedException ex)
            {
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + ex.Msg;
                _sysLogRepository.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Msg }.ToJson());
            }
            catch (StackExchange.Redis.RedisConnectionException ex)
            {
                logEntity.Result = false;
                logEntity.Description = "登录失败，" + "内存服务器无响应";
                _sysLogRepository.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = logEntity.Description }.ToJson());
            }
            catch (Exception ex)
            {
                AppLogger.Instance.Error(ex.Message, ex);

                logEntity.Result = false;
                logEntity.Description = "登录失败，" + ex.Message;
                _sysLogRepository.Insert(logEntity);
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败，发生内部异常" }.ToJson());
            }
        }

        /// <summary>
        /// LấyWebCủa clientIp
        /// </summary>
        public string GetWebClientIp()
        {
            string ipAddress = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                return ipAddress;
            }
            ipAddress = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                return ipAddress;
            }
            return string.Empty;
        }

        /// <summary>
        /// Sau khi xác thực danh tính thành công，Ghi lại trạng thái đăng nhập thành công
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        private void SaveLoginSuccessStatus(SysUserEntity userEntity, SysLogEntity logEntity)
        {
            SysStaffEntity staff = null;

            var writedNeedChooseOrgFlag = false;

            OperatorModel operatorModel = null;
            if (userEntity != null)
            {
                var staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                if (userEntity.Account != "root" && userEntity.Account != "admin")
                {
                    //root admin Không thể liên kết bệnh việnId，Dù đã thiết lập liên kết cũng bỏ qua
                    staffList = _sysUserDmnService.GetStaffListByUserId(userEntity.Id);
                    if (staffList.Count > 1)
                    {
                        //Trang chủ cần làmOrgChọn
                        WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg", "1");
                        writedNeedChooseOrgFlag = true;
                    }
                    else if (staffList.Count == 0)
                    {
                        //170904 Nếu chưa liên kết nhân sự hệ thống Xem như đăng nhập thất bại
                        throw new FailedException("登录失败，尚未关联系统人员");
                    }
                }

                staff = staffList != null && staffList.Count == 1 ? staffList[0] : null;
                operatorModel = new OperatorModel()
                {
                    UserId = userEntity.Id,
                    UserCode = userEntity.Account,
                    UserName = staff == null ? null : staff.Name,
                    DepartmentCode = staff == null ? null : staff.DepartmentCode,
                    OrganizeId = staff == null ? null : staff.OrganizeId,
                    LoginIPAddress = GetWebClientIp(),
                    LoginTime = DateTime.Now,
                    StaffId = staff == null ? null : staff.Id,  //Nhân sự Id
                    rygh = staff == null ? null : staff.gh, //Mã nhân viên
                    TopOrganizeId = Constants.TopOrganizeId,
                    DepartmentName = _sysDepartmentRepository.GetNameByCode(staff == null ? null : staff.DepartmentCode
                        , staff == null ? null : staff.OrganizeId),
                };

                operatorModel.LoginIPAddressName = operatorModel.LoginIPAddress;

                if (staff != null && !string.IsNullOrWhiteSpace(staff.OrganizeId))
                {
                    WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_OrganizeId", staff.OrganizeId);
                }
                else
                {
                    WebHelper.RemoveCookie(Constants.AppId + "_" + "CookieKey_OrganizeId");
                }

                WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_CurUserCode", userEntity.Account);

                if (userEntity.Account == "root")
                {
                    operatorModel.IsRoot = true;
                }
                else if (userEntity.Account == "admin")
                {
                    operatorModel.IsAdministrator = true;
                }
                else
                {
                    var roleList = _userRoleAuthDmnService.GetUserRoleList(userEntity.Id,operatorModel.OrganizeId);
                    //Trước Đã làm staff != null của Đánh giá tiền điều kiện
                    //170904 AdministratorKhông còn có thể liên kếtStaff，Chỉadmin
                    operatorModel.RoleIdList = roleList.Select(p => p.Id).ToList();
                    operatorModel.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");
                }

                WebHelper.WriteCookie(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", operatorModel.IsHospAdministrator.ToString().ToLower());

                if (!writedNeedChooseOrgFlag)
                {
                    //Trang chủ cần làmOrgChọn
                    WebHelper.RemoveCookie(Constants.AppId + "_" + "CookieKey_ChooseOrg");
                }

                logEntity.NickName = staff == null ? null : staff.Name;
                logEntity.Result = true;
                logEntity.Description = "系统登录成功";

                var rpHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
                if (!string.IsNullOrWhiteSpace(rpHost))
                {
                    WebHelper.WriteCookie(Constants.AppId + "_" + "ReportServer_HOST", rpHost);
                }
                else
                {
                    WebHelper.RemoveCookie(Constants.AppId + "_" + "ReportServer_HOST");
                }

                OperatorProvider.AddCurrent(operatorModel); //Đặt cái này cuối cùng
            }
        }
    }
}