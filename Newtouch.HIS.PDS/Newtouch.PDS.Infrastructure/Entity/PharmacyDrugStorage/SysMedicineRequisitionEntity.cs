using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Đơn lĩnh thuốc hệ thống
    /// </summary>
    [Table("xt_yp_sld")]
    [SugarTable("xt_yp_sld")]
    public class SysMedicineRequisitionEntity : IEntity<SysMedicineRequisitionEntity>
    {
        /// <summary>
        /// Đơn lĩnhID
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string sldId { get; set; }

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số đơn lĩnh
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// Bộ phận lĩnh
        /// </summary>
        public string Slbm { get; set; }

        /// <summary>
        /// Bộ phận xuất kho
        /// </summary>
        public string Ckbm { get; set; }

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
