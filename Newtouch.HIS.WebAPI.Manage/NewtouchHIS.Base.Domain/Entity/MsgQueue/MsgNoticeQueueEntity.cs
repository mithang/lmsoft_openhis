using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    /// bảng cấu hình nhóm tin nhắn
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Msg_NoticeQueue", "MsgNoticeQueueEntity")]
    public class MsgNoticeQueueEntity:IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// tin nhắnId duy nhất lần này
        /// thuận tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        [Required(ErrorMessage = "NoticeId不可为空")]
        public string NoticeId { get; set; }
        /// <summary>
        /// người gửi
        /// </summary>
        [Required(ErrorMessage = "SendFrom不可为空")]
        public string SendFrom { get; set; }
        /// <summary>
        /// số hiệu nhóm tin nhắn
        /// </summary>
        [Required(ErrorMessage = "NoticeGroupId不可为空")]
        public string NoticeGroupId { get; set; }
        /// <summary>
        /// loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "GroupYwlx不可为空")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "NoticeRange不可为空")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// hiển thị chủ đề 
        /// Ví dụ Bạn có tin nhắn kiểm soát chất lượng mới，Vui lòng xem ngay
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// nội dung chi tiết
        /// hiển thị mở rộng
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// dữ liệu liên quan đến nội dung Jsonđịnh dạng đối tượng có thể chuyển đổi
        /// </summary>
        public string? ContentData { get; set; }
        /// <summary>
        /// đường dẫn chuyển hướng mặc định 
        /// yêu cầu khi không trống AppId không được để trống
        /// </summary>
        public string? OpenPath { get; set; }
        /// <summary>
        /// tham số đường dẫn
        /// định dạng： zyh={0}，id={1}
        /// </summary>
        public string? OpenPathPara { get; set; }
        /// <summary>
        /// người nhận thư số hiệu nhân viên mặc định，khi loại người nhận là nhóm người dùng，trường trống
        /// nhiều người thì , phân tách，độ dài lớn hơn1000 khi，gửi nhiều lần
        /// </summary>
        [Required(ErrorMessage = "Recipient不可为空")]
        public string Recipient { get; set; }
        /// <summary>
        /// loại người nhận người dùng / nhóm người dùng
        /// </summary>
        [Required(ErrorMessage = "RecipientType不可为空")]
        public int RecipientType { get; set; }
        /// <summary>
        /// loại thực thi hàng đợi MsgQueueExecType
        /// </summary>
        [Required(ErrorMessage = "QueueExecType不可为空")]
        public int QueueExecType { get; set; }
        /// <summary>
        /// thực thi biểu thức
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// trạng thái tin nhắn NoticeStuEnum
        /// </summary>
        public int NoticeStu { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }     


    }
}
