using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Security;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// Quản lý người dùng hệ thống(query)
    /// </summary>
    public class SysUserVDmnService : BaseDmnService<SysUserVEntity>, ISysUserVDmnService
    {
        private readonly ISysStaffDmnService _sysStaffDmn;
        private readonly ISysDepartmentDmnService _sysDepartmentDmn;
        private readonly ISysRoleDmnService _roleDmn;
        /// <summary>
        /// Ứng dụng cần cấu hình cơ sở dữ liệu chính
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");

        public SysUserVDmnService(ISysStaffDmnService sysStaffDmn, ISysDepartmentDmnService sysDepartmentDmn, ISysRoleDmnService roleDmn)
        {
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
            _sysStaffDmn = sysStaffDmn;
            _sysDepartmentDmn = sysDepartmentDmn;
            _roleDmn = roleDmn;
        }        

        #region login
        public async Task<BusResult<SysUserVEntity>> UserLogin(LoginBO login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "Tên đăng nhập và mật khẩu không được để trống" };
            }
            if (sysConfig == null || string.IsNullOrWhiteSpace(sysConfig.Top_OrganizeId))
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "Vui lòng cấu hình thông tin tổ chức cấp cao nhất" };
            }
            if (login.Username == "root" && CheckRootPwd(login.Password))
            {
                return new BusResult<SysUserVEntity>
                {
                    code = ResponseResultCode.SUCCESS,
                    Data = new SysUserVEntity()
                    {
                        Id = "root",
                        Account = "root"
                    }
                };
            }
            var user = await GetFirstOrDefaultWithAttr<SysUserVEntity>(p => p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1" && p.Account == login.Username);
            if (user == null)
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin người dùng" };
            }

            if (user.Locked.HasValue && user.Locked.Value)
            {
                //Không cung cấp Tự động mở khóa Chức năng
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = string.Format("Nhập sai mật khẩu liên tiếp quá {0} lần, tài khoản đã bị khóa", sysConfig.LoginFailedTimesLimit_Count) };
            }
            string dbPassword = MD5Encrypt.Md5(DESEncrypt.Encrypt(login.Password.ToLower(), user.UserSecretkey).ToLower(), 32).ToLower();
            if (dbPassword != user.UserPassword)
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAILOfPwdError, msg = "Mật khẩu không đúng, vui lòng nhập lại" };
            }

            return new BusResult<SysUserVEntity> { code = ResponseResultCode.SUCCESS, Data = new SysUserVEntity { Id = user.Id, Account = user.Account, TopOrganizeId = user.TopOrganizeId } };
        }

        public async Task<OperatorModel?> BuildLoginStatusOpr(string userId, string staffOrganizeId = null)
        {
            SysUserVEntity user = new SysUserVEntity();
            if (userId == "root")
            {
                user.Id = userId;
                user.Account = userId;
                user.TopOrganizeId = staffOrganizeId;
            }
            else
            {
                user = await GetFirstOrDefaultWithAttr<SysUserVEntity>(p => p.Id == userId);
                if (user == null)
                {
                    return null;
                }
            }

            //Đăng nhập thành công,Xây dựng thông tin danh tính hệ thống
            var logEntity = new SysUserLoginLog
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = sysConfig.Top_OrganizeId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "Top_OrganizeId"),
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "Dang nhap he thong",
                Type = DbLogType.Login.ToString(),
                Account = userId == "root" ? userId : user?.Account,
            };

            return await BuildLoginStatusOpr(user, logEntity, staffOrganizeId);
        }
        /// <summary>
        /// Xây dựng danh tính đăng nhập(Danh tính phía trước đã kiểm chứng đạt)
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        /// <param name="staffOrganizeId">Đã chỉ địnhOrganizeId</param>
        public async Task<OperatorModel?> BuildLoginStatusOpr(SysUserVEntity userEntity, SysUserLoginLog logEntity, string staffOrganizeId = null)
        {
            SysStaffVEntity? staff = null;
            bool writedNeedChooseOrgFlag = false;
            OperatorModel? operatorModel = null;
            if (userEntity != null)
            {
                IList<SysStaffVEntity>? staffList = null;
                if (userEntity.Account != "root" && userEntity.Account != "admin")
                {
                    //root admin không thể liên kết bệnh việnId,dù đã lập liên kết cũng bỏ qua
                    staffList = await _sysStaffDmn.GetStaffListByUserId(userEntity.Id);
                    if (!string.IsNullOrWhiteSpace(staffOrganizeId))
                    {
                        staffList = staffList.Where(p => p.OrganizeId == staffOrganizeId).ToList();
                    }
                    if (staffList.Count > 1)
                    {
                        //Trang chủ cần làmOrgChọn
                        writedNeedChooseOrgFlag = true;
                    }
                    else if (staffList.Count == 0)
                    {
                        throw new FailedException("Đăng nhập thất bại, chưa liên kết nhân viên hệ thống");
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
                    LoginIPAddress = "",
                    LoginTime = DateTime.Now,
                    StaffId = staff == null ? null : staff.Id,
                    rygh = staff == null ? null : staff.gh,
                    TopOrganizeId = sysConfig.Top_OrganizeId,
                    AppId = sysConfig.AppId,
                    DepartmentName = staff == null ? null : await _sysDepartmentDmn.GetNameByCode(staff.DepartmentCode, staff.OrganizeId),
                    NeedChooseOrgFlag = writedNeedChooseOrgFlag
                };

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
                    var roleList = await _roleDmn.GetUserRoleList(userEntity.Id, operatorModel.OrganizeId);
                    operatorModel.RoleIdList = roleList.Select(p => p.Id).ToList();
                    operatorModel.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");
                }

                logEntity.NickName = staff == null ? null : staff.Name;
                logEntity.Result = true;
                logEntity.Description = "Dang nhap he thong thanh cong";
                //Liên kết nhiềuTổ chứckhi không chính xác
                logEntity.OrganizeId = staff == null ? null : staff?.OrganizeId;
            }
            return operatorModel;
        }
        #endregion

        #region private
        /// <summary>
        /// rootĐăng nhập(Trường hợp đặc biệt),Kiểm chứng mật khẩu
        /// Đạt thì trả vềtrue,nếu không thì trả vềfalse
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckRootPwd(string password)
        {
            return password == MD5Encrypt.Md5(DateTime.Now.ToString("HHMMdd"), 32).ToLower();
        }

        #endregion



    }
}
;