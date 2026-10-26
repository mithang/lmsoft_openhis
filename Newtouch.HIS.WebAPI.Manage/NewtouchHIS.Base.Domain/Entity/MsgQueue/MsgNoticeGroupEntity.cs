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
    /// bảng cấu hình nhóm tin nhắn
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Msg_NoticeGroup", "MsgNoticeGroupEntity")]
    public class MsgNoticeGroupEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        
        /// <summary>
        /// nhãn nhóm
        /// tin nhắn hệ thống / tin nhắn trong viện /tin nhắn nội bộ khoa  tin nhắn kiểm soát chất lượng
        /// </summary>
        [Required(ErrorMessage = "GroupTag不可为空")]
        public string GroupTag { get; set; }
        /// <summary>
        /// tên nhóm
        /// </summary>
        [Required(ErrorMessage = "GroupName不可为空")]
        public string GroupName { get; set; }
        /// <summary>
        /// mô tả nhóm tin nhắn
        /// </summary>
        public string? GroupDesc { get; set; }
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
        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //[Required(ErrorMessage = "CreatorCodekhông được để trống")]
        //[StringLength(50, ErrorMessage = "CreatorCodegiới hạn độ dài là50")]
        //public string CreatorCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(23, ErrorMessage = "LastModifyTimegiới hạn độ dài là23")]
        //public DateTime? LastModifyTime { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(50, ErrorMessage = "LastModifierCodegiới hạn độ dài là50")]
        //public string? LastModifierCode { get; set; }

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //[Required(ErrorMessage = "ztkhông được để trống")]
        //[StringLength(1, ErrorMessage = "ztgiới hạn độ dài là1")]
        //public string zt { get; set; }

    }
}
