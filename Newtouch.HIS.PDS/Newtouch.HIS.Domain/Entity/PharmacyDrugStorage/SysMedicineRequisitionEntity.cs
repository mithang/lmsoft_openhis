using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc phiếu lĩnh
    /// </summary>
    [Table("xt_yp_sld")]
    public class SysMedicineRequisitionEntity : IEntity<SysMedicineRequisitionEntity>
    {
        /// <summary>
        /// phiếu lĩnh ID
        /// </summary>
        [Key]
        public string sldId { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// số phiếu lĩnh
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// bộ phận lĩnh
        /// </summary>
        public string Slbm { get; set; }

        /// <summary>
        /// bộ phận xuất kho
        /// </summary>
        public string Ckbm { get; set; }

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
