using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Bảng quan hệ bạn bè của ứng dụng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Auth_AppFriend", "SysAuthAppFriendEntity")]
    public partial class SysAuthAppFriendEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:Ứng dụngId
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AuthId không được để trống")]
        [StringLength(50, ErrorMessage = "AuthId giới hạn độ dài là 50")]
        public string AppAuthId { get; set; }

        /// <summary>
        /// Desc:Ứng dụng bạn bèId
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "FriendId không được để trống")]
        [StringLength(50, ErrorMessage = "FriendId giới hạn độ dài là 50")]
        public string FriendId { get; set; }

        /// <summary>
        /// Desc:Cấp ủy quyền
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AuthLev không được để trống")]
        public string AuthLevs { get; set; }

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
