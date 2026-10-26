using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// xt_yp_sldmx
    /// </summary>
    [Table("xt_yp_sldmx")]
    public class SysMedicineRequisitionDetailEntity : IEntity<SysMedicineRequisitionDetailEntity>
    {
        /// <summary>
        /// chi tiết phiếu lĩnh Id
        /// </summary>
        [Key]
        public string sldmxId { get; set; }

        /// <summary>
        /// phiếu lĩnh Id
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// ngày hiệu lực
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// hệ số chuyển đổi phát thuốc kho thuốc của hệ số chuyển đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lượng lĩnh đơn vị nhỏ nhất
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// số lượng đã phát đơn vị nhỏ nhất
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// trạng thái  1: còn hiệu lực  0: không hiệu lực
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

    }
}
