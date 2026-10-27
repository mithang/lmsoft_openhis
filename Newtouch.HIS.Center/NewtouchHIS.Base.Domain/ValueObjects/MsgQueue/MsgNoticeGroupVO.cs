using NewtouchHIS.Base.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// Nhóm tin nhắnView
    /// Khai báo và EntityNhất quán
    /// </summary>
    public class MsgNoticeGroupVO: IEntity
    {
        [Required(ErrorMessage = "Khóa chínhId không được để trống")]
        public string Id { get; set; }
        //[Required(ErrorMessage = "Tổ chứcId không được để trống")]
        //public string OrganizeId { get; set; }
        /// <summary>
        /// Nhãn nhóm
        /// Hệ thốngTin nhắn / Trong việnTin nhắn /Nội bộ khoaTin nhắn  Kiểm soát chất lượngTin nhắn
        /// </summary>
        [Required(ErrorMessage = "Nhãn nhóm không được để trống")]
        public string GroupTag { get; set; }
        /// <summary>
        /// Tên nhóm
        /// </summary>
        [Required(ErrorMessage = "Tên nhóm tin nhắn không được để trống")]
        public string GroupName { get; set; }
        /// <summary>
        /// Nhóm tin nhắnGiải thích
        /// </summary>
        public string? GroupDesc { get; set; }
        /// <summary>
        /// Loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "Loại nghiệp vụ liên quan không được để trống")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// Quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "Phạm vi đối tượng thông báo không được để trống")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// Chủ đề hiển thị 
        /// Như Bạn có thông báo kiểm soát chất lượng mớiTin nhắn,vui lòng xem ngay
        /// </summary>
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// Nội dung chi tiết
        /// Mở rộng hiển thị
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// Dữ liệu liên kết nội dung JsonĐịnh dạng Đối tượng có thể chuyển đổi
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// Đường dẫn chuyển mặc định 
        /// Khi không rỗng yêu cầu AppId  không được để trống
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// Tham số đường dẫn
        /// Định dạng: zyh={0},id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// Ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        //public string CreatorCode { get; set; }
        //public DateTime CreateTime { get; set; } = DateTime.Now;
        //public DateTime? LastModifyTime { get; set; }
        //public string? LastModifierCode { get; set; }
        //public string zt { get; set; } = "1";
    }
    /// <summary>
    /// Nhóm tin nhắnThông tin cơ bản 
    /// Truy vấn/Dùng để trả về
    /// </summary>
    public class MsgNoticeGroupBasicVO
    {
        public string? Id { get; set; }
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Nhãn nhóm
        /// Hệ thốngTin nhắn / Trong việnTin nhắn /Nội bộ khoaTin nhắn  Kiểm soát chất lượngTin nhắn
        /// </summary>
        public string? GroupTag { get; set; }
        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string? GroupName { get; set; }
        /// <summary>
        /// Nhóm tin nhắnGiải thích
        /// </summary>
        public string? GroupDesc { get; set; }
        /// <summary>
        /// Loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        public int? GroupYwlx { get; set; }
        /// <summary>
        /// Quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        public int? NoticeRange { get; set; }
        /// <summary>
        /// Chủ đề hiển thị 
        /// Như Bạn có thông báo kiểm soát chất lượng mớiTin nhắn,vui lòng xem ngay
        /// </summary>
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// Nội dung chi tiết
        /// Mở rộng hiển thị
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// Dữ liệu liên kết nội dung JsonĐịnh dạng Đối tượng có thể chuyển đổi
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// Đường dẫn chuyển mặc định 
        /// Khi không rỗng yêu cầu AppId  không được để trống
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// Tham số đường dẫn
        /// Định dạng: zyh={0},id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// Ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        public string? CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string? LastModifierCode { get; set; }
        public string? zt { get; set; }
    }
}
