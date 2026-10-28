using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// Lớp cơ sở trường dùng chung
    /// (Liên kếtThông tin tổ chức)
    /// </summary>
    public class IEntity
    {

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
        [Required(ErrorMessage = "CreateTime không được để trống")]
        // QA-phase2: StringLength tren DateTime la vo nghia va gay loi validation
        // ("Unable to cast DateTime to String") cho moi post entity. Cot DB la
        // datetime (khong co gioi han 23 ky tu) nen bo attribute, khong format lai gia tri.
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
        // QA-phase2: nhu CreateTime — StringLength tren DateTime? cung throw khi
        // gia tri khong null (duong edit set LastModifyTime = Now).
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode giới hạn độ dài là 50")]
        public string? LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt không được để trống")]
        [StringLength(1, ErrorMessage = "zt giới hạn độ dài là 1")]
        public string zt { get; set; }


        public void NewEntity(string organizeId, string user)
        {
            this.zt = "1";
            this.CreateTime = DateTime.Now;
            this.OrganizeId = organizeId;
            this.CreatorCode = user;
        }
        public void ModifiedEntity(string organizeId, string user, bool isDel = false)
        {
            if (isDel)
            {
                this.zt = "0";
            }
            this.LastModifyTime = DateTime.Now;
            this.OrganizeId = organizeId;
            this.LastModifierCode = user;
        }
    }
}
