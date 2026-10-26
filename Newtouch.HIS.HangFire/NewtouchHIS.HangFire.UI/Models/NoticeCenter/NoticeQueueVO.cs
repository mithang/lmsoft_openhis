using System.ComponentModel.DataAnnotations;
using static NewtouchHIS.Lib.Base.BaseEnum;

namespace NewtouchHIS.HangFire.UI.Models
{
    public class MsgNoticeQueueBasicVO
    {
        public string? Id { get; set; } 

        /// <summary>
        /// tin nhắnId duy nhất lần này
        /// để tiện gửi nhiều lần khi có quá nhiều người nhận
        /// </summary>
        public string? NoticeId { get; set; }
        /// <summary>
        /// người gửi thư
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
        /// quyết định phạm vi đối tượng thông báo  MsgNoticeRangeEnum
        /// </summary>
        public int? NoticeRange { get; set; }
        /// <summary>
        /// hiển thị chủ đề 
        /// ví dụ bạn có tin nhắn kiểm soát chất lượng mới，vui lòng xem ngay
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// nội dung chi tiết
        /// mở rộng hiển thị
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// dữ liệu liên kết nội dung Jsonđịnh dạng đối tượng có thể chuyển đổi
        /// </summary>
        public string? ContentData { get; set; }
        /// <summary>
        /// đường dẫn chuyển hướng mặc định 
        /// khi không trống thì yêu cầu AppId không được để trống
        /// </summary>
        public string? OpenPath { get; set; }
        /// <summary>
        /// tham số đường dẫn
        /// định dạng： zyh={0}，id={1}
        /// </summary>
        public string? OpenPathPara { get; set; }
        /// <summary>
        /// người nhận thư mã nhân viên mặc định，khi loại người nhận là nhóm người dùng，trường trống
        /// nếu nhiều người thì , tách，độ dài lớn hơn1000 khi，gửi nhiều lần
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// loại người nhận người dùng / nhóm người dùng
        /// </summary>
        public int? RecipientType { get; set; }
        /// <summary>
        /// kiểu thực thi hàng đợi mặc định thực hiện ngay
        /// MsgQueueExecType
        /// </summary>
        public int? QueueExecType { get; set; } = (int)MsgQueueExecTypeEnum.Immediately;
        /// <summary>
        /// thực hiện biểu thức
        /// </summary>
        public string? ExecCron { get; set; }
        /// <summary>
        /// trạng thái tin nhắn 
        /// NoticeStuEnum
        /// </summary>
        public int? NoticeStu { get; set; } = (int)NoticeStuEnum.UnSend;
        public int? px { get; set; }
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? zt { get; set; }
    }
}
