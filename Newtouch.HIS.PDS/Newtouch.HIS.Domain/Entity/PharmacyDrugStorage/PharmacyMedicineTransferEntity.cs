using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    /// <summary>
    /// nhà thuốc thuốc điều chuyển
    /// </summary>
    [Table("yf_ypdbsq")]
    public class PharmacyMedicineTransferEntity : IEntity<PharmacyMedicineTransferEntity>
    {
        /// <summary>
        /// phiếu lĩnh ID
        /// </summary>
        [Key]
        public long dbId { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// số phiếu lĩnh
        /// </summary>
        public string Dbdh { get; set; }

        /// <summary>
        /// nhà thuốc đề nghị
        /// </summary>
        public string SqyfCode { get; set; }

        /// <summary>
        /// xuất kho nhà thuốc
        /// </summary>
        public string CkyfCode { get; set; }

        /// <summary>
        /// trạng thái cấp phát 0:chưa phát 1:đã phát một phần 2:đã toàn phát 3:đã chấm dứt
        /// </summary>
        public int ffzt { get; set; }

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
        public string zt { get; set; }

    }
}
