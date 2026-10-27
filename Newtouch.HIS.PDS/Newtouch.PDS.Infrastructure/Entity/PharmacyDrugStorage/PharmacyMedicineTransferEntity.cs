using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    /// <summary>
    /// Điều chuyển thuốc nhà thuốc
    /// </summary>
    [Table("yf_ypdbsq")]
    [SugarTable("yf_ypdbsq")]
    public class PharmacyMedicineTransferEntity : IEntity<PharmacyMedicineTransferEntity>
    {
        /// <summary>
        /// Đơn lĩnhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long dbId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số đơn lĩnh
        /// </summary>
        public string Dbdh { get; set; }

        /// <summary>
        /// Nhà thuốc xin
        /// </summary>
        public string SqyfCode { get; set; }

        /// <summary>
        /// Nhà thuốc xuất kho
        /// </summary>
        public string CkyfCode { get; set; }

        /// <summary>
        /// Trạng thái phát 0:Chưa phát 1:Đã phát một phần 2:Đã phát hết 3:Đã chấm dứt
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
