using NewtouchHIS.Base.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class MsgNoticeQueueVO : IEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Tin nhắnId Duy nhất trong lần này
        /// Để tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        [Required(ErrorMessage = "Tin nhắnId  không được để trống")]
        public string NoticeId { get; set; }
        /// <summary>
        /// Người gửi
        /// </summary>
        [Required(ErrorMessage = "Người gửi không được để trống")]
        public string SendFrom { get; set; }
        /// <summary>
        /// Nhóm tin nhắnSố hiệu
        /// </summary>
        [Required(ErrorMessage = "Mã số nhóm tin nhắn không được để trống")]
        public string NoticeGroupId { get; set; }
        /// <summary>
        /// Loại nghiệp vụ liên quan GroupYwlxEnum
        /// </summary>
        [Required(ErrorMessage = "Loại nghiệp vụ liên quan không được để trống")]
        public int GroupYwlx { get; set; }
        /// <summary>
        /// Quyết định phạm vi đối tượng nhận thông báo  MsgNoticeRangeEnum
        /// </summary>
        [Required(ErrorMessage = "Giới hạn phạm vi áp dụng tin nhắn không được để trống")]
        public int NoticeRange { get; set; }
        /// <summary>
        /// Chủ đề hiển thị 
        /// Như Bạn có thông báo kiểm soát chất lượng mớiTin nhắn,vui lòng xem ngay
        /// </summary>
        [Required(ErrorMessage = "Chủ đề tin nhắn không được để trống")]
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
        [Required(ErrorMessage = "Người nhận không được để trống")]
        public string Recipient { get; set; }
        /// <summary>
        /// Loại người nhận Người dùng / Nhóm người dùng
        /// </summary>
        [Required(ErrorMessage = "Giới hạn phạm vi áp dụng tin nhắn không được để trống")]
        public int RecipientType { get; set; }
        /// <summary>
        /// Kiểu thực thi hàng đợi Mặc định thực thi ngay
        /// MsgQueueExecType
        /// </summary>
        [Required(ErrorMessage = "Loại thực thi hàng đợi không được để trống")]
        public int QueueExecType { get; set; } = (int)MsgQueueExecTypeEnum.Immediately;
        /// <summary>
        /// Biểu thức thực thi
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// Tin nhắnTrạng thái 
        /// NoticeStuEnum
        /// </summary>
        public int NoticeStu { get; set; } = (int)NoticeStuEnum.UnSend;
        public int? px { get; set; }
        /// <summary>
        /// Ứng dụngId
        /// </summary>
        public string? AppId { get; set; }

    }

    public class MsgNoticeQueueBasicVO
    {
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Tin nhắnId Duy nhất trong lần này
        /// Để tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        public string? NoticeId { get; set; }
        /// <summary>
        /// Người gửi
        /// </summary>
        public string? SendFrom { get; set; }
        /// <summary>
        /// Nhóm tin nhắnSố hiệu
        /// </summary>
        public string? NoticeGroupId { get; set; }
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
        public string? Recipient { get; set; }
        /// <summary>
        /// Loại người nhận Người dùng / Nhóm người dùng
        /// </summary>
        public int? RecipientType { get; set; }
        /// <summary>
        /// Kiểu thực thi hàng đợi Mặc định thực thi ngay
        /// MsgQueueExecType
        /// </summary>
        public int? QueueExecType { get; set; }
        /// <summary>
        /// Biểu thức thực thi
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// Tin nhắnTrạng thái 
        /// NoticeStuEnum
        /// </summary>
        public int? NoticeStu { get; set; }
        public int? px { get; set; }
        /// <summary>
        /// Ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? CreatorCode { get; set; }

        public DateTime? LastModifyTime { get; set; }

        public string? LastModifierCode { get; set; }
        public string? zt { get; set; } = "1";
    }
    /// <summary>
    /// Tin nhắnTruy vấn thông báo
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
        /// Tin nhắnHiển thị nhắc nhở
        /// </summary>
        public string? NoticeShow { get; set; }
        /// <summary>
        /// Số hiệu người liên quan trọng yếu 
        /// Ngoại trú:Số ngoại trú;Nội trú:Số nhập viện
        /// </summary>
        public string? NoticePrimaryPartyCode { get; set; }
        /// <summary>
        /// Người liên quan trọng yếu
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
