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
            // Giai doan 7-fix (P0): DA XOA backdoor "root" — moi dang nhap deu phai qua DB.
            // Truoc day co nhanh dac biet tra success voi Id/Account "root" ma khong can
            // tai khoan DB nao, chay TRUOC ca kiem tra Locked. Nhanh do va CheckRootPwd
            // da bi xoa hoan toan; userId "root" tu day se tra DB nhu moi tai khoan khac.
            var user = await GetFirstOrDefaultWithAttr<SysUserVEntity>(p => p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1" && p.Account == login.Username);
            if (user == null)
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy thông tin người dùng" };
            }

            if (user.Locked.HasValue && user.Locked.Value)
            {
                //Không cung cấp Tự động mở khóa Chức năng — mo khoa chi bang can thiep thu cong
                //(UPDATE Sys_UserLogOn SET Locked=0, xem bao cao giai doan 7-fix).
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = string.Format("Nhập sai mật khẩu liên tiếp quá {0} lần, tài khoản đã bị khóa", FailureLimit) };
            }
            string dbPassword = MD5Encrypt.Md5(DESEncrypt.Encrypt(login.Password.ToLower(), user.UserSecretkey).ToLower(), 32).ToLower();
            if (dbPassword != user.UserPassword)
            {
                // Giai doan 7-fix (P0): LOCKOUT THAT — ghi 1 dong that bai vao Sys_Log roi dem
                // so that bai LIEN TIEP trong cua so cau hinh; du nguong thi set Locked=1.
                // (Truoc day ham nay chi kiem tra co Locked san, khong bao gio dem/set.)
                // Loi ghi log/kiem tra khong duoc lam hong duong bao sai mat khau: neu
                // bookkeeping loi, van tra thong diep sai mat khau nhu cu.
                try
                {
                    await RecordLoginFailureAsync(user);
                    if (await HasReachedFailureLimitAsync(user))
                    {
                        await base.Context.Ado.ExecuteCommandAsync(
                            "update Sys_UserLogOn set Locked=1 where UserId=@userId and zt='1'",
                            new SugarParameter("@userId", user.Id));
                        return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = string.Format("Nhập sai mật khẩu liên tiếp quá {0} lần, tài khoản đã bị khóa", FailureLimit) };
                    }
                }
                catch
                {
                    // Giu nguyen hanh vi cu khi bookkeeping loi (khong khoa, chi bao sai mat khau).
                }
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAILOfPwdError, msg = "Mật khẩu không đúng, vui lòng nhập lại" };
            }

            // 7-fix: ghi 1 dong thanh cong vao Sys_Log de chuoi that bai LIEN TIEP
            // duoc reset dung nghia (neu khong, sau mo khoa, 1 lan sai moi lai du
            // ngay nguong nho cac dong that bai cu chua co dong thanh cong xen giua).
            // Loi ghi log khong duoc lam hong login thanh cong -> try/catch nuot.
            try
            {
                var now = DateTime.Now;
                await base.Context.Ado.ExecuteCommandAsync(
                    "insert into Sys_Log (Id, TopOrganizeId, Date, Account, Type, ModuleName, Result, Description, CreateTime) " +
                    "values (@id, @topOrg, @date, @account, 'Login', 'Dang nhap he thong', 1, @desc, @now)",
                    new SugarParameter("@id", Guid.NewGuid().ToString()),
                    new SugarParameter("@topOrg", user.TopOrganizeId),
                    new SugarParameter("@date", now),
                    new SugarParameter("@account", user.Account),
                    new SugarParameter("@desc", "Dang nhap thanh cong"),
                    new SugarParameter("@now", now));
            }
            catch
            {
            }
            return new BusResult<SysUserVEntity> { code = ResponseResultCode.SUCCESS, Data = new SysUserVEntity { Id = user.Id, Account = user.Account, TopOrganizeId = user.TopOrganizeId } };
        }

        public async Task<OperatorModel?> BuildLoginStatusOpr(string userId, string staffOrganizeId = null)
        {
            // Giai doan 7-fix (P0): DA XOA nhanh dac biet "root" khong qua DB.
            // userId "root" tu day tra DB nhu moi Id khac: khong thay thi null -> tu choi.
            SysUserVEntity user = await GetFirstOrDefaultWithAttr<SysUserVEntity>(p => p.Id == userId);
            if (user == null)
            {
                return null;
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
                Account = user?.Account,
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
                // Giai doan 7-fix: khong con mien lien ket nhan vien cho tai khoan "root".
                // Tai khoan DB (ke ca ten "root" neu benh vien tu tao) phai co staff link
                // va vai tro nhu moi tai khoan khac; chi "admin" giu dac quyen nhu cu.
                if (userEntity.Account != "admin")
                {
                    //admin không liên kết bệnh việnId,dù đã lập liên kết cũng bỏ qua (7-fix: root khong con mien)
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

                // Giai doan 7-fix: khong con co IsRoot dac biet cho tai khoan "root".
                // (IsRoot tren OperatorModel giu nguyen kieu, mac dinh false.)
                if (userEntity.Account == "admin")
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

        #region private — lockout (giai doan 7-fix)
        /// <summary>
        /// Nguong khoa: doc tu BusinessConfig, fallback giong legacy (3 lan / 60 phut).
        /// AuthCenter/Manage khong co 2 key nay trong appsettings nen nhan fallback,
        /// cung gia tri 3/60 nhu BaseAPI (co key) — xem bao cao giai doan 7-fix.
        /// </summary>
        private int FailureLimit
        {
            get
            {
                var v = sysConfig.LoginFailedTimesLimit_Count;
                return v.HasValue && v.Value > 0 ? v.Value : 3;
            }
        }

        private int FailureWindowMinutes
        {
            get
            {
                var v = sysConfig.LoginFailedTimesLimit_Minutes;
                return v.HasValue && v.Value > 0 ? v.Value : 60;
            }
        }

        /// <summary>
        /// Ghi 1 dong that bai vao Sys_Log (bang cua SysUserLoginLog).
        /// Liet ke cot tuong minh vi Sys_Log THUC TE khong co cot OrganizeId
        /// ma entity co — insert ca entity se loi "Invalid column name 'OrganizeId'".
        /// Dong that bai nay cung la nguon dem cho logic legacy (sys_log Type='Login')
        /// neu duong legacy duoc khoi phuc sau nay.
        /// </summary>
        private async Task RecordLoginFailureAsync(SysUserVEntity user)
        {
            var now = DateTime.Now;
            await base.Context.Ado.ExecuteCommandAsync(
                "insert into Sys_Log (Id, TopOrganizeId, Date, Account, Type, ModuleName, Result, Description, CreateTime) " +
                "values (@id, @topOrg, @date, @account, 'Login', 'Dang nhap he thong', 0, @desc, @now)",
                new SugarParameter("@id", Guid.NewGuid().ToString()),
                new SugarParameter("@topOrg", user.TopOrganizeId),
                new SugarParameter("@date", now),
                new SugarParameter("@account", user.Account),
                new SugarParameter("@desc", "Mat khau khong dung"),
                new SugarParameter("@now", now));
        }

        /// <summary>
        /// Dem so that bai LIEN TIEP moi nhat trong cua so (gap dong thanh cong
        /// thi dung dem). Tra true khi du nguong — tuc lan that bai hien tai
        /// la lan thu N lien tiep, dung ngu nghia legacy "N-1 dong truoc toan that bai".
        /// </summary>
        private async Task<bool> HasReachedFailureLimitAsync(SysUserVEntity user)
        {
            int limit = FailureLimit;
            var start = DateTime.Now.AddMinutes(-FailureWindowMinutes);
            // limit la int noi bo da clamp (>0), khong phai input nguoi dung.
            var table = await base.Context.Ado.GetDataTableAsync(
                $"select top {limit} Result from Sys_Log where Type='Login' and Account=@account " +
                "and TopOrganizeId=@topOrg and CreateTime>@start order by CreateTime desc",
                new SugarParameter("@account", user.Account),
                new SugarParameter("@topOrg", user.TopOrganizeId),
                new SugarParameter("@start", start));
            if (table == null || table.Rows.Count < limit)
            {
                return false;
            }
            foreach (System.Data.DataRow row in table.Rows)
            {
                var result = row["Result"];
                if (result != null && result != System.DBNull.Value && (bool)result)
                {
                    return false; // co dong thanh cong xen giua -> chua du N that bai lien tiep
                }
            }
            return true;
        }

        #endregion



    }
}
;