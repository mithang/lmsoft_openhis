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
    ///Tin nhắnLớp cơ sở thông báo
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("dic_msg_queue", "MsgQueueEntity")]
    public partial class MsgQueueEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "OrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "OrganizeId giới hạn độ dài là 50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AppId không được để trống")]
        [StringLength(50, ErrorMessage = "AppId giới hạn độ dài là 50")]
        public string AppId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "msgtypecode không được để trống")]
        public int msgtypecode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "msgcontent giới hạn độ dài là 500")]
        public string msgcontent { get; set; }

        /// <summary>
        /// Desc:Tin nhắnXử lý định dạng nội dung cần truyền tham số
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "msgformat giới hạn độ dài là 100")]
        public string msgformat { get; set; }

        /// <summary>
        /// Desc:Trạng thái thông báo MsgNoticeEnum 0 Chưa gửi ,1 Đã gửi, 2 Đã đọc 
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? msgstu { get; set; }

        /// <summary>
        /// Desc:Ngoại trú:Số ngoại trú  Nội trú:Số nhập viện
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "patno giới hạn độ dài là 100")]
        public string patno { get; set; }

        /// <summary>
        /// Desc:Ngoại trú:Số đơn thuốc v.v.  Nội trú:Số y lệnh v.v.
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ywlsh giới hạn độ dài là 100")]
        public string ywlsh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ks giới hạn độ dài là 50")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "bq giới hạn độ dài là 50")]
        public string bq { get; set; }

        /// <summary>
        /// Desc:,Mã nhân viên vận hành phân tách bằng dấu phẩy
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1000, ErrorMessage = "czys giới hạn độ dài là 1000")]
        public string czys { get; set; }

        /// <summary>
        /// Desc:,Vị trí phân tách bằng dấu phẩy
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "dutylimit giới hạn độ dài là 200")]
        public string dutylimit { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime không được để trống")]
        [StringLength(23, ErrorMessage = "CreateTime giới hạn độ dài là 23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode không được để trống")]
        [StringLength(50, ErrorMessage = "CreatorCode giới hạn độ dài là 50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime giới hạn độ dài là 23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode giới hạn độ dài là 50")]
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt không được để trống")]
        [StringLength(1, ErrorMessage = "zt giới hạn độ dài là 1")]
        public string zt { get; set; }
    }
}
