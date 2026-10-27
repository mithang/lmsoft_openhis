using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.EMR
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.MrmsDb)]
    [SugarTable("mr_basy_zd", "")]
    public partial class MedicalHomeDiagEntity
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
        [Required(ErrorMessage = "BAH không được để trống")]
        [StringLength(100, ErrorMessage = "BAH giới hạn độ dài là 100")]
        public string BAH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ZYH không được để trống")]
        [StringLength(50, ErrorMessage = "ZYH giới hạn độ dài là 50")]
        public string ZYH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ZDOrder không được để trống")]
        public int ZDOrder { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "JBDM giới hạn độ dài là 100")]
        public string JBDM { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "JBMC giới hạn độ dài là 100")]
        public string JBMC { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "RYBQ giới hạn độ dài là 100")]
        public string RYBQ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "RYBQMS giới hạn độ dài là 200")]
        public string RYBQMS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "CYQK giới hạn độ dài là 100")]
        public string CYQK { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "CYQKMS giới hạn độ dài là 200")]
        public string CYQKMS { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt không được để trống")]
        [StringLength(1, ErrorMessage = "zt giới hạn độ dài là 1")]
        public string zt { get; set; }

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
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "OrganizeId giới hạn độ dài là 50")]
        public string OrganizeId { get; set; }
    }
}
