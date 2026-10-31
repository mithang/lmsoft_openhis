using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Security;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysUserApp : ISysUserApp
    {
        /// <summary>
        /// Số lần đăng nhập quá nhiều,Tài khoản tự động khóa của Khoảng thời gian(Đơn vị:phút)
        /// </summary>
        private static int _loginFailedTimesLimit_Minutes = 60;
        /// <summary>
        /// Số lần đăng nhập quá nhiều,Tài khoản tự động khóa của Số lần tối đa
        /// </summary>
        private static int _loginFailedTimesLimit_Count = 3;

        static SysUserApp()
        {
            _loginFailedTimesLimit_Minutes = ConfigurationHelper.GetAppConfigIntValue("LoginFailedTimesLimit_Minutes", defaultValue: _loginFailedTimesLimit_Minutes);

            _loginFailedTimesLimit_Count = ConfigurationHelper.GetAppConfigIntValue("LoginFailedTimesLimit_Count", defaultValue: _loginFailedTimesLimit_Count);
        }

        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysUserLogOnRepository _sysUserLogOnRepository;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysRoleRepo _sysRoleRepository;
        private readonly ISysLogRepository _sysLogRepository;

        public SysUserApp(ISysUserRepo sysUserRepo, ISysUserLogOnRepository sysUserLogOnRepository
            , ISysUserDmnService sysUserDmnService, ISysRoleRepo sysRoleRepository
            , ISysLogRepository sysLogRepository)
        {
            this._sysUserRepo = sysUserRepo;
            this._sysUserLogOnRepository = sysUserLogOnRepository;
            this._sysUserDmnService = sysUserDmnService;
            this._sysRoleRepository = sysRoleRepository;
            this._sysLogRepository = sysLogRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SysUserEntity CheckLogin(string username, string password)
        {
            var userEntity = _sysUserRepo.FindEntity(t => t.Account == username
                && t.TopOrganizeId == Constants.TopOrganizeId && t.zt == "1");
            if (userEntity != null)
            {
                var userLogOnEntity = _sysUserLogOnRepository.FindEntity(p => p.UserId == userEntity.Id);
                if (userLogOnEntity.Locked.HasValue && userLogOnEntity.Locked.Value)
                {
                    //Không cung cấp Tự động mở khóa Chức năng
                    throw new FailedException(string.Format("Nhập sai mật khẩu liên tiếp quá {0} lần, tài khoản đã bị khóa", _loginFailedTimesLimit_Count));
                }
                string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                if (dbPassword == userLogOnEntity.UserPassword)
                {
                    var lastVisitTime = DateTime.Now;
                    int LogOnCount = (userLogOnEntity.LogOnCount).ToInt() + 1;
                    if (userLogOnEntity.LastVisitTime != null)
                    {
                        userLogOnEntity.PreviousVisitTime = userLogOnEntity.LastVisitTime.ToDate();
                    }
                    userLogOnEntity.LastVisitTime = lastVisitTime;
                    userLogOnEntity.LogOnCount = LogOnCount;
                    _sysUserLogOnRepository.Update(userLogOnEntity);
                    return userEntity;
                }
                else
                {
                    if (username == "admin")
                    {
                        throw new FailedException("Mật khẩu không đúng,Vui lòng nhập lại");
                    }

                    var startTime = DateTime.Now.AddMinutes(-_loginFailedTimesLimit_Minutes);
                    //LấyMtrong vòng phút mới nhấtN-1bản ghi nhật ký đăng nhập
                    var loginLogList = _sysLogRepository.IQueryable().Where(p => p.Type == "Login" && p.Account == username && p.TopOrganizeId == Constants.TopOrganizeId && p.CreateTime > startTime).OrderByDescending(p => p.CreateTime).Take(_loginFailedTimesLimit_Count - 1).Select(p => p.Result).ToList();
                    if (loginLogList.Count == _loginFailedTimesLimit_Count - 1 && !loginLogList.Any(p => p.HasValue && p.Value))  //Nếu làN-1dòng,và không có bản ghi đăng nhập thành công
                    {
                        //Đã đến số lần lỗi tối đa,Khóa nó
                        //Cập nhật trạng thái thành‘Khóa’
                        //Cập nhậtBASE.SYS_USERLOGON
                        userLogOnEntity.Locked = false;
                        userLogOnEntity.Modify();
                        _sysUserLogOnRepository.Update(userLogOnEntity);
                        throw new FailedException(string.Format("Nhập sai mật khẩu liên tiếp{0} lần, tài khoản bị khóa", _loginFailedTimesLimit_Count));
                    }
                    else
                    {
                        throw new FailedException("Mật khẩu không đúng,Vui lòng nhập lại");
                    }
                }
            }
            else
            {
                throw new FailedException("Tài khoản không tồn tại,Vui lòng nhập lại");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userPassword"></param>
        /// <param name="keyValue"></param>
        public void RevisePassword(string userPassword, string keyValue)
        {
            var userLogOnEntity = _sysUserLogOnRepository.FindEntity(p => p.UserId == keyValue);    //CóUserId

            if (userLogOnEntity == null)
            {
                throw new Exception();
            }
			userLogOnEntity.UserPwdPlaintext = userPassword;
			userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
            userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();

            _sysUserLogOnRepository.Update(userLogOnEntity, dstnFieldNameList: new[] { "UserPassword", "UserSecretkey", "UserPwdPlaintext" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEntity"></param>
        public void UpdateUserZT(SysUserEntity userEntity)
        {
            _sysUserRepo.Update(userEntity, dstnFieldNameList: new[] { "zt" });
        }

        /// <summary>
        /// Tạo một quản trị viên hệ thống cho tổ chức admin
        /// </summary>
        /// <param name="organizeId"></param>
        public void CreateDefaultAdminToOrg(string topOrgId, string userPassword)
        {
            var userEntity = new SysUserEntity()
            {
                TopOrganizeId = topOrgId,
                Account = "admin",
                zt = "1"
            };
            userEntity.Create(true);

            var userLogOnEntity = new SysUserLogOnEntity()
            {
                UserId = userEntity.Id,
                zt = "1"
            };
            userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
            userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
            userLogOnEntity.Create(true);

            //Lưu vào cơ sở dữ liệu
            _sysUserDmnService.CreateUser(userEntity, userLogOnEntity);
        }

        /// <summary>
        /// MicrosoftSSOĐăng nhập(Email)
        /// </summary>
        /// <param name="userId">Người dùngID</param>
        /// <param name="organizeId">Cơ sởID</param>
        /// <returns></returns>
        public SysUserEntity MsSsoLogin(string userId, string organizeId)
        {
            var userEntity = _sysUserRepo.FindEntity(t => t.Id == userId && t.zt == "1");
            return userEntity;
        }

    }
}
