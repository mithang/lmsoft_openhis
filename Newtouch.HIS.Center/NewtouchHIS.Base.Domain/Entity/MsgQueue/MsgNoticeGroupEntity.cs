using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    /// Nhóm tin nhắnBảng cấu hình
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Msg_NoticeGroup", "MsgNoticeGroupEntity")]
    public class MsgNoticeGroupEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        
        /// <summary>
        /// Nhãn nhóm
        /// Hệ thốngTin nhắn / Trong việnTin nhắn /Nội bộ khoaTin nhắn  Kiểm soát chất lượngTin nhắn
        /// </summary>
        [Required(ErrorMessage = "GroupTag không được để trống")]
        public string GroupTag { get; set; }
        /// <summary>
        /// Tên nhóm
        /// </summary>
        [Required(ErrorMessage = "GroupName không được để trống")]
        public string GroupName { get; set; }
        /// <summary>
        /// Nhóm tin nhắnGiải thích
        /// </summary>
        public string? GroupDesc { get; set; }
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
        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //[Required(ErrorMessage = "CreatorCode không được để trống")]
        //[StringLength(50, ErrorMessage = "CreatorCode giới hạn độ dài là 50")]
        //public string CreatorCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(23, ErrorMessage = "LastModifyTime giới hạn độ dài là 23")]
        //public DateTime? LastModifyTime { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(50, ErrorMessage = "LastModifierCode giới hạn độ dài là 50")]
        //public string? LastModifierCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //[Required(ErrorMessage = "zt không được để trống")]
        //[StringLength(1, ErrorMessage = "zt giới hạn độ dài là 1")]
        //public string zt { get; set; }

    }
}
