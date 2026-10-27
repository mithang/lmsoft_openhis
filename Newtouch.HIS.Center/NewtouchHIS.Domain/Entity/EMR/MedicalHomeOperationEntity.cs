using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.EMR
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.MrmsDb)]
    [SugarTable("mr_basy_ss", "")]
    public partial class MedicalHomeOperationEntity
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
        [Required(ErrorMessage = "SSOrder không được để trống")]
        public int SSOrder { get; set; }

        /// <summary>
        /// Desc:Phẫu thuật và thủ thuậtMã
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SSJCZBM giới hạn độ dài là 100")]
        public string SSJCZBM { get; set; }

        /// <summary>
        /// Desc:Ngày phẫu thuật và thủ thuật
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "SSJCZRQ giới hạn độ dài là 23")]
        public DateTime? SSJCZRQ { get; set; }

        /// <summary>
        /// Desc:Cấp phẫu thuật
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SSJB giới hạn độ dài là 100")]
        public string SSJB { get; set; }

        /// <summary>
        /// Desc:Tên phẫu thuật và thủ thuật
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "SSJCZMC giới hạn độ dài là 200")]
        public string SSJCZMC { get; set; }

        /// <summary>
        /// Desc:Người phẫu thuật
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "SZ giới hạn độ dài là 100")]
        public string SZ { get; set; }

        /// <summary>
        /// Desc:ITrợ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "YZ giới hạn độ dài là 100")]
        public string YZ { get; set; }

        /// <summary>
        /// Desc:IITrợ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "EZ giới hạn độ dài là 100")]
        public string EZ { get; set; }

        /// <summary>
        /// Desc:Cấp vết mổ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "QKDJ giới hạn độ dài là 100")]
        public string QKDJ { get; set; }

        /// <summary>
        /// Desc:Loại lành vết mổ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "QKYHLB giới hạn độ dài là 100")]
        public string QKYHLB { get; set; }

        /// <summary>
        /// Desc:Phương pháp gây mê
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MZFS giới hạn độ dài là 100")]
        public string MZFS { get; set; }

        /// <summary>
        /// Desc:Bác sĩ gây mê
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MZYS giới hạn độ dài là 100")]
        public string MZYS { get; set; }

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

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "QKYHDJ giới hạn độ dài là 50")]
        public string QKYHDJ { get; set; }

    }
}
