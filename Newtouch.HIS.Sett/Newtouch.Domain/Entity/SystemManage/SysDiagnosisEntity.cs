using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chẩn đoán hệ thống
    /// </summary>
    [Table("xt_zd")]
    [Obsolete("please use the view")]
    public class SysDiagnosisEntity : IEntity<SysDiagnosisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int zdbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short zdnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string icd10fjm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Mỗi khi sử dụng＋1,Có thể làm cho tiền tố thường dùng
        /// </summary>
        public int orderid { get; set; }

        /// <summary>
        /// 0 Không thường dùng   1 Thường dùng(Khi hiển thị thì cộng thêm trên cơ sở số sắp xếp1000)
        /// </summary>
        public string cybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
