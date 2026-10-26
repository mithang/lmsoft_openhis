using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///bảng quan hệ bạn bè ứng dụng
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
        /// Desc:ứng dụngId
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AuthId不可为空")]
        [StringLength(50, ErrorMessage = "AuthId长度限制为50")]
        public string AppAuthId { get; set; }

        /// <summary>
        /// Desc:ứng dụng bạn bèId
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "FriendId不可为空")]
        [StringLength(50, ErrorMessage = "FriendId长度限制为50")]
        public string FriendId { get; set; }

        /// <summary>
        /// Desc:cấp ủy quyền
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AuthLev不可为空")]
        public string AuthLevs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode不可为空")]
        [StringLength(50, ErrorMessage = "CreatorCode长度限制为50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime长度限制为23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode长度限制为50")]
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt不可为空")]
        [StringLength(1, ErrorMessage = "zt长度限制为1")]
        public string zt { get; set; }

    }
}
