using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    /// Nhóm tin nhắnBảng cấu hình
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Msg_NoticeQueue", "MsgNoticeQueueEntity")]
    public class MsgNoticeQueueEntity:IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        /// <summary>
        /// Tin nhắnId Duy nhất trong lần này
        /// Để tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        [Required(ErrorMessage = "NoticeId không được để trống")]
        public string NoticeId { get; set; }
        /// <summary>
        /// Người gửi
        /// </summary>
        [Required(ErrorMessage = "SendFrom không được để trống")]
        public string SendFrom { get; set; }
        /// <summary>
        /// Nhóm tin nhắnSố hiệu
        /// </summary>
        [Required(ErrorMessage = "NoticeGroupId không được để trống")]
        public string NoticeGroupId { get; set; }
        /// <summary>
        /// Loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "GroupYwlx không được để trống")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// Quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "NoticeRange không được để trống")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// Chủ đề hiển thị 
        /// Như Bạn có thông báo kiểm soát chất lượng mớiTin nhắn,vui lòng xem ngay
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Nội dung chi tiết
        /// Mở rộng hiển thị
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Dữ liệu liên kết nội dung JsonĐịnh dạng Đối tượng có thể chuyển đổi
        /// </summary>
        public string? ContentData { get; set; }
        /// <summary>
        /// Đường dẫn chuyển mặc định 
        /// Khi không rỗng yêu cầu AppId  không được để trống
        /// </summary>
        public string? OpenPath { get; set; }
        /// <summary>
        /// Tham số đường dẫn
        /// Định dạng: zyh={0},id={1}
        /// </summary>
        public string? OpenPathPara { get; set; }
        /// <summary>
        /// Người nhận Mã nhân viên mặc định,Khi loại người nhận là nhóm người dùng,trường rỗng
        /// Nhiều người thì , Phân tách,Độ dài lớn hơn1000 khi,gửi nhiều lần
        /// </summary>
        [Required(ErrorMessage = "Recipient không được để trống")]
        public string Recipient { get; set; }
        /// <summary>
        /// Loại người nhận Người dùng / Nhóm người dùng
        /// </summary>
        [Required(ErrorMessage = "RecipientType không được để trống")]
        public int RecipientType { get; set; }
        /// <summary>
        /// Kiểu thực thi hàng đợi MsgQueueExecType
        /// </summary>
        [Required(ErrorMessage = "QueueExecType không được để trống")]
        public int QueueExecType { get; set; }
        /// <summary>
        /// Biểu thức thực thi
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// Tin nhắnTrạng thái NoticeStuEnum
        /// </summary>
        public int NoticeStu { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// Ứng dụngId
        /// </summary>
        public string? AppId { get; set; }     


    }
}
