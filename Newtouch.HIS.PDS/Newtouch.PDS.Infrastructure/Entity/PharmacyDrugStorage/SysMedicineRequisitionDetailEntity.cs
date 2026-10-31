using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// xt_yp_sldmx
    /// </summary>
    [Table("xt_yp_sldmx")]
    [SugarTable("xt_yp_sldmx")]
    public class SysMedicineRequisitionDetailEntity : IEntity<SysMedicineRequisitionDetailEntity>
    {
        /// <summary>
        /// Chi tiết đơn lĩnhId
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string sldmxId { get; set; }

        /// <summary>
        /// Đơn lĩnhId
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Ngày hiệu lực
        /// </summary>
        public DateTime? Yxrq { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi Hệ số chuyển đổi của kho thuốc phát thuốc
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lượng lĩnh Đơn vị nhỏ nhất
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// Số lượng đã phát Đơn vị nhỏ nhất
        /// </summary>
        public int yfsl { get; set; }

        /// <summary>
        /// Trạng thái  1:Hiệu lực  0:Không hiệu lực
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
