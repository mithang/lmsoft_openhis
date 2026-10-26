using NewtouchHIS.Base.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    /// <summary>
    /// chế độ xem nhóm tin nhắn
    /// khai báo vàEntitynhất quán
    /// </summary>
    public class MsgNoticeGroupVO: IEntity
    {
        [Required(ErrorMessage = "主键Id不可为空")]
        public string Id { get; set; }
        //[Required(ErrorMessage = "cơ quanIdkhông được để trống")]
        //public string OrganizeId { get; set; }
        /// <summary>
        /// nhãn nhóm
        /// tin nhắn hệ thống / tin nhắn trong viện /tin nhắn nội bộ khoa  tin nhắn kiểm soát chất lượng
        /// </summary>
        [Required(ErrorMessage = "组标签不可为空")]
        public string GroupTag { get; set; }
        /// <summary>
        /// tên nhóm
        /// </summary>
        [Required(ErrorMessage = "消息组名称不可为空")]
        public string GroupName { get; set; }
        /// <summary>
        /// mô tả nhóm tin nhắn
        /// </summary>
        public string? GroupDesc { get; set; }
        /// <summary>
        /// loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "相关业务类型不可为空")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "通知对象范围不可为空")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// hiển thị chủ đề 
        /// Ví dụ Bạn có tin nhắn kiểm soát chất lượng mới，Vui lòng xem ngay
        /// </summary>
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// nội dung chi tiết
        /// hiển thị mở rộng
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// dữ liệu liên quan đến nội dung Jsonđịnh dạng đối tượng có thể chuyển đổi
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// đường dẫn chuyển hướng mặc định 
        /// yêu cầu khi không trống AppId không được để trống
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// tham số đường dẫn
        /// định dạng： zyh={0}，id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        //public string CreatorCode { get; set; }
        //public DateTime CreateTime { get; set; } = DateTime.Now;
        //public DateTime? LastModifyTime { get; set; }
        //public string? LastModifierCode { get; set; }
        //public string zt { get; set; } = "1";
    }
    /// <summary>
    /// thông tin cơ sở nhóm tin nhắn 
    /// truy vấn/trả về để dùng
    /// </summary>
    public class MsgNoticeGroupBasicVO
    {
        public string? Id { get; set; }
        public string? OrganizeId { get; set; }
        /// <summary>
        /// nhãn nhóm
        /// tin nhắn hệ thống / tin nhắn trong viện /tin nhắn nội bộ khoa  tin nhắn kiểm soát chất lượng
        /// </summary>
        public string? GroupTag { get; set; }
        /// <summary>
        /// tên nhóm
        /// </summary>
        public string? GroupName { get; set; }
        /// <summary>
        /// mô tả nhóm tin nhắn
        /// </summary>
        public string? GroupDesc { get; set; }
        /// <summary>
        /// loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        public int? GroupYwlx { get; set; }
        /// <summary>
        /// quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        public int? NoticeRange { get; set; }
        /// <summary>
        /// hiển thị chủ đề 
        /// Ví dụ Bạn có tin nhắn kiểm soát chất lượng mới，Vui lòng xem ngay
        /// </summary>
        public string? DefaultTitle { get; set; }
        /// <summary>
        /// nội dung chi tiết
        /// hiển thị mở rộng
        /// </summary>
        public string? DefaultContent { get; set; }
        /// <summary>
        /// dữ liệu liên quan đến nội dung Jsonđịnh dạng đối tượng có thể chuyển đổi
        /// </summary>
        public string? DefaultContentData { get; set; }
        /// <summary>
        /// đường dẫn chuyển hướng mặc định 
        /// yêu cầu khi không trống AppId không được để trống
        /// </summary>
        public string? DefaultOpenPath { get; set; }
        /// <summary>
        /// tham số đường dẫn
        /// định dạng： zyh={0}，id={1}
        /// </summary>
        public string? DefaultOpenPathPara { get; set; }
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        public string? CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string? LastModifierCode { get; set; }
        public string? zt { get; set; }
    }
}
