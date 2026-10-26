using NewtouchHIS.Base.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class MsgNoticeQueueVO : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// tin nhắnId duy nhất lần này
        /// thuận tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        [Required(ErrorMessage = "消息Id 不可为空")]
        public string NoticeId { get; set; }
        /// <summary>
        /// người gửi
        /// </summary>
        [Required(ErrorMessage = "发信人不可为空")]
        public string SendFrom { get; set; }
        /// <summary>
        /// số hiệu nhóm tin nhắn
        /// </summary>
        [Required(ErrorMessage = "消息组编号不可为空")]
        public string NoticeGroupId { get; set; }
        /// <summary>
        /// loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "相关业务类型不可为空")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "消息适用范围限制不可为空")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// hiển thị chủ đề 
        /// Ví dụ Bạn có tin nhắn kiểm soát chất lượng mới，Vui lòng xem ngay
        /// </summary>
        [Required(ErrorMessage = "消息主题不可为空")]
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
        [Required(ErrorMessage = "收信人不可为空")]
        public string Recipient { get; set; }
        /// <summary>
        /// loại người nhận người dùng / nhóm người dùng
        /// </summary>
        [Required(ErrorMessage = "消息适用范围限制不可为空")]
        public int RecipientType { get; set; }
        /// <summary>
        /// loại thực thi hàng đợi mặc định thực hiện ngay
        /// MsgQueueExecType
        /// </summary>
        [Required(ErrorMessage = "队列执行类型不可为空")]
        public int QueueExecType { get; set; } = (int)MsgQueueExecTypeEnum.Immediately;
        /// <summary>
        /// thực thi biểu thức
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// trạng thái tin nhắn 
        /// NoticeStuEnum
        /// </summary>
        public int NoticeStu { get; set; } = (int)NoticeStuEnum.UnSend;
        public int? px { get; set; }
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }

    }

    public class MsgNoticeQueueBasicVO
    {
        public string? OrganizeId { get; set; }
        /// <summary>
        /// tin nhắnId duy nhất lần này
        /// thuận tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        public string? NoticeId { get; set; }
        /// <summary>
        /// người gửi
        /// </summary>
        public string? SendFrom { get; set; }
        /// <summary>
        /// số hiệu nhóm tin nhắn
        /// </summary>
        public string? NoticeGroupId { get; set; }
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
        public string? Recipient { get; set; }
        /// <summary>
        /// loại người nhận người dùng / nhóm người dùng
        /// </summary>
        public int? RecipientType { get; set; }
        /// <summary>
        /// loại thực thi hàng đợi mặc định thực hiện ngay
        /// MsgQueueExecType
        /// </summary>
        public int? QueueExecType { get; set; }
        /// <summary>
        /// thực thi biểu thức
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// trạng thái tin nhắn 
        /// NoticeStuEnum
        /// </summary>
        public int? NoticeStu { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? CreatorCode { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public string? LastModifierCode { get; set; }
        public string? zt { get; set; } = "1";
    }
    /// <summary>
    /// truy vấn thông báo tin nhắn
    /// </summary>
    public class MsgNoticeQueueQueryVO
    {
        public int? NoticeStu { get; set; }
        public DateTime? ksrq { get; set; }
        public DateTime? jsrq { get; set; }
        public string? OrganizeId { get; set; }
        public string? SendFrom { get; set; }
        public string? Recipient { get; set; }
        public string? keyword { get; set; }
        public int? msgtype { get; set; } = 1;
    }
    public class MsgNoticeQueueQueryRspoVO : MsgNoticeQueueEntity
    {
        public string GroupTag { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public string? SendFromName { get; set; }
        public string? RecipientName { get; set; }
        public string? AppName { get; set; }
    }
    public class MsgNoticeQueueModelVO<T> : MsgNoticeQueueQueryRspoVO
    {
        

        public T? NoticeData { get; set; }
        /// <summary>
        /// hiển thị nhắc nhở tin nhắn
        /// </summary>
        public string? NoticeShow { get; set; }
        /// <summary>
        /// số hiệu người liên hệ quan trọng 
        /// khám ngoại trú：số khám ngoại trú；nội trú：số nội trú
        /// </summary>
        public string? NoticePrimaryPartyCode { get; set; }
        /// <summary>
        /// người liên hệ quan trọng
        /// </summary>
        public string? NoticePrimaryParty { get; set; }
    }


    #region old
    public class MsgQueueVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string AppId { get; set; }
        public int msgtypecode { get; set; }
        public string msgcontent { get; set; }
        public string msgformat { get; set; }
        public int? msgstu { get; set; }
        public string patno { get; set; }
        public string ywlsh { get; set; }
        public string ks { get; set; }
        public string bq { get; set; }
        public string czys { get; set; }
        public string dutylimit { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string CreatorCode { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
    }
    #endregion
}
