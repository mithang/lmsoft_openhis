namespace NewtouchHIS.Lib.Base.Model
{

    /// <summary>
    /// Thông tin danh tính người dùng đăng nhập
    /// </summary>
    public class OperatorModel
    {
        /// <summary>
        /// Người dùng hệ thống Id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Nhân viên hệ thống Id
        /// </summary>
        public string? StaffId { get; set; }

        /// <summary>
        /// Mã người dùng hệ thống (tài khoản đăng nhập)
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// Mã số công việc của nhân viên hệ thống
        /// </summary>
        public string? rygh { get; set; }

        /// <summary>
        /// Họ tên nhân viên hệ thống
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Khoa của nhân viên hệ thốngCode
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Bộ phận dược hệ thống mở cho người dùng đăng nhập hiện tạiCode List
        /// </summary>
        public List<string>? yfbmCodeList { get; set; }

        /// <summary>
        /// Vai trò người dùng hệ thốngList
        /// </summary>
        public List<string>? RoleIdList { get; set; }

        /// <summary>
        /// Vai trò người dùng hệ thốngList
        /// </summary>
        public List<string>? RoleCodeList { get; set; }

        /// <summary>
        /// Người dùng hệ thống có phải là quản trị viên của một cơ sở y tế nào đó không (một bệnh viện, phòng khám cụ thể duy nhất)
        /// Cũng yêu cầu liên kết với một nhân viên hệ thống duy nhất
        /// </summary>
        public bool IsHospAdministrator { get; set; }

        /// <summary>
        /// Người dùng hệ thống có phải là quản trị viên hệ thống không
        /// Điểm khác với IsHospAdministrator: không phục vụ cho cơ sở y tế cụ thể (bệnh viện, phòng khám)
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// Người dùng hệ thống có phải là tài khoản root không
        /// Điểm khác với Administrator: thậm chí quyền cấp cao hơn, thông thường không mở cho khách hàng
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// Đăng nhậpIP
        /// </summary>
        public string? LoginIPAddress { get; set; }

        /// <summary>
        /// Địa chỉ mà IP đăng nhập xác định được
        /// </summary>
        public string? LoginIPAddressName { get; set; }

        /// <summary>
        /// Thời gian đăng nhập
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// Id tổ chức (đã xác định được bệnh viện cụ thể) (hệ thống dùng cho nhiều tổ chức, và khi triển khai thống nhất có thể nhờ vào trường này)
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// Tổ chức cấp cao nhất
        /// </summary>
        public string? TopOrganizeId { get; set; }

        /// <summary>
        /// Id ứng dụng (cấu hình AppId trong config)
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Mã thông báo danh tính
        /// </summary>
        public string? token { get; set; }
        /// <summary>
        /// SSO Chungtoken
        /// </summary>
        public string? access_token { get; set; }
        /// <summary>
        /// Có cần chọn tổ chức không
        /// </summary>
        public bool NeedChooseOrgFlag { get; set; } = false;
    }

}
