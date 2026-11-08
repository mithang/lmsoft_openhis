using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_PLAT_06
{
    /// <summary>
    /// đẩy Thông tin bệnh nhân yêu cầu thể
    /// </summary>
    public class PullUserRequestEntity : RequestBase
    {
        /// <summary>
        /// chỗ lý Loại yêu cầu thể
        /// </summary>
        public HandleType HandleType { get; set; }

        /// <summary>
        /// Thông tin nhân viên thể
        /// </summary>
        public User User { get; set; }
    }

    /// <summary>
    /// chỗ lý Loại yêu cầu thể
    /// </summary>
    public class HandleType
    {
        /// <summary>
        /// chỗ lý Loại:A-Thêm mới,U-Sửa
        /// bắt buộc
        /// </summary>
        public string handleType { get; set; }
    }

    /// <summary>
    /// Thông tin nhân viên thể
    /// </summary>
    public class User
    {
        /// <summary>
        /// người viên Số hiệu(Người dùng tên)
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }

        /// <summary>
        ///Mặc định Giá trị:1
        /// bắt buộc
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
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }

        /// <summary>
        ///Giới tính Mã
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string genderCode { get; set; }

        /// <summary>
        ///Giới tính
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string gender { get; set; }

        /// <summary>
        ///chứng mục Mã loại
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string cardTypeCode { get; set; }

        /// <summary>
        ///chứng mục Loại
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string cardType { get; set; }

        /// <summary>
        ///Số giấy tờ
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string cardNo { get; set; }

        /// <summary>
        ///Ngày sinh-YYYYMMDD
        /// </summary>
        /// <returns></returns>
        public string birthday { get; set; }

        /// <summary>
        ///Điện thoại
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
        ///Chức danh Mã-Bác sĩ Chức danh Mã
        /// </summary>
        /// <returns></returns>
        public string proTitleCode { get; set; }

        /// <summary>
        ///Chức danh
        /// </summary>
        /// <returns></returns>
        public string proTitle { get; set; }

        /// <summary>
        ///sở thuộc Cơ sở Mã
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string orgCode { get; set; }

        /// <summary>
        ///sở thuộc Mã khoa
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string deptCode { get; set; }

        /// <summary>
        ///Vai trò Mã 0: tỉnh Quản lý viên,1: trong y quán Quản lý viên,2: Bác sĩ
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string roleCode { get; set; }

        /// <summary>
        ///Cơ sở Số hiệu(duy nhất một nhận dạng)
        /// bắt buộc
        /// </summary>
        /// <returns></returns>
        public string oid { get; set; }
    }
}