namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_PLAT_06
{
    /// <summary>
    /// Thân yêu cầu đẩy thông tin bệnh nhân
    /// </summary>
    public class PullUserRequestEntity : RequestBase
    {
        /// <summary>
        /// Thân yêu cầu loại xử lý
        /// </summary>
        public HandleType HandleType { get; set; }

        /// <summary>
        /// Thân thông tin nhân sự
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// Thân yêu cầu loại xử lý
    /// </summary>
    public class HandleType
    {
        /// <summary>
        /// Loại xử lý: A-Thêm mới, U-Sửa đổi
        /// Bắt buộc
        /// </summary>
        public string handleType { get; set; }
    }

    /// <summary>
    /// Thân thông tin nhân sự
    /// </summary>
    public class User
    {
        /// <summary>
        ///Mã nhân sự (tên đăng nhập)
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }

        /// <summary>
        ///Giá trị mặc định: 1
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string typeCode { get; set; }

        /// <summary>
        ///Mã nhân viên
        /// </summary>
        /// <returns></returns>
        public string jobNo { get; set; }

        /// <summary>
        ///Họ tên
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }

        /// <summary>
        ///Mã giới tính
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string genderCode { get; set; }

        /// <summary>
        ///Giới tính
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string gender { get; set; }

        /// <summary>
        ///Mã loại giấy tờ
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string cardTypeCode { get; set; }

        /// <summary>
        ///Loại giấy tờ
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string cardType { get; set; }

        /// <summary>
        ///Số giấy tờ
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string cardNo { get; set; }

        /// <summary>
        ///Ngày sinh -YYYYMMDD
        /// </summary>
        /// <returns></returns>
        public string birthday { get; set; }

        /// <summary>
        ///Điện thoại di động
        /// </summary>
        /// <returns></returns>
        public string telephone { get; set; }

        /// <summary>
        ///Email
        /// </summary>
        /// <returns></returns>
        public string email { get; set; }

        /// <summary>
        ///Giới thiệu
        /// </summary>
        /// <returns></returns>
        public string introduce { get; set; }

        /// <summary>
        ///Mã chức danh - Mã chức danh bác sĩ
        /// </summary>
        /// <returns></returns>
        public string proTitleCode { get; set; }

        /// <summary>
        ///Chức danh
        /// </summary>
        /// <returns></returns>
        public string proTitle { get; set; }

        /// <summary>
        ///Mã tổ chức trực thuộc
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string orgCode { get; set; }

        /// <summary>
        ///Mã khoa trực thuộc
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string deptCode { get; set; }

        /// <summary>
        ///Mã vai trò 0: Quản trị viên cấp tỉnh, 1: Quản trị viên phòng khám Đông y, 2: Bác sĩ
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string roleCode { get; set; }

        /// <summary>
        ///Mã tổ chức (định danh duy nhất)
        /// Bắt buộc
        /// </summary>
        /// <returns></returns>
        public string oid { get; set; }
    }
}