namespace NewtouchHIS.Lib.Base.Model
{

    /// <summary>
    /// người dùng đăng nhập thông tin danh tính
    /// </summary>
    public class OperatorModel
    {
        /// <summary>
        /// người dùng hệ thống Id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// nhân viên hệ thống Id
        /// </summary>
        public string? StaffId { get; set; }

        /// <summary>
        /// người dùng hệ thống Code（tài khoản đăng nhập）
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// nhân viên hệ thống mã nhân viên
        /// </summary>
        public string? rygh { get; set; }

        /// <summary>
        /// nhân viên hệ thống họ tên
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// nhân viên hệ thống khoaCode
        /// </summary>
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// muốn người dùng đăng nhập hiện tại bộ phận dược của hệ thống mởCode List
        /// </summary>
        public List<string>? yfbmCodeList { get; set; }

        /// <summary>
        /// người dùng hệ thống vai tròList
        /// </summary>
        public List<string>? RoleIdList { get; set; }

        /// <summary>
        /// người dùng hệ thống vai tròList
        /// </summary>
        public List<string>? RoleCodeList { get; set; }

        /// <summary>
        /// người dùng hệ thống có phải là quản trị viên của một cơ sở y tế nào đó hay không（một bệnh viện cụ thể duy nhất、phòng khám）
        /// cũng yêu cầu liên kết với một nhân viên hệ thống duy nhất
        /// </summary>
        public bool IsHospAdministrator { get; set; }

        /// <summary>
        /// người dùng hệ thống có phải là quản trị viên hệ thống hay không
        /// vàIsHospAdministratorsự khác biệt của：không phục vụ một cơ sở y tế cụ thể（bệnh viện、phòng khám）
        /// </summary>
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// người dùng hệ thống có phải làroottài khoản
        /// vàAdministratorsự khác biệt của：thậm chí quyền cấp cao hơn，thông thường không mở cho khách hàng
        /// </summary>
        public bool IsRoot { get; set; }

        /// <summary>
        /// đăng nhậpIP
        /// </summary>
        public string? LoginIPAddress { get; set; }

        /// <summary>
        /// đăng nhậpIP địa chỉ đã xác định được
        /// </summary>
        public string? LoginIPAddressName { get; set; }

        /// <summary>
        /// thời gian đăng nhập
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// tổ chứcId（đã xác định được bệnh viện cụ thể）（hệ thống phục vụ nhiều tổ chức，và khi triển khai thống nhất có thể nhờ vào trường này）
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất
        /// </summary>
        public string? TopOrganizeId { get; set; }

        /// <summary>
        /// ứng dụngId（trongconfigcấu hình AppId）
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// tên khoa
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// mã thông báo danh tính
        /// </summary>
        public string? token { get; set; }
        /// <summary>
        /// SSO chungtoken
        /// </summary>
        public string? access_token { get; set; }
        /// <summary>
        /// có cần chọn tổ chức hay không
        /// </summary>
        public bool NeedChooseOrgFlag { get; set; } = false;
    }

}
