using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc tồn kho kết chuyển
    /// </summary>
    [Table("xt_yp_kcjzk")]
    public class SysMedicineStockCarryDownEntity : IEntity<SysMedicineStockCarryDownEntity>
    {
        /// <summary>
        /// thành phần
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// giá trị mặc định Getdate()
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// số lượng tồn kho
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// kho thuốc giá nhập
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// thời gian kết chuyển
        /// </summary>
        public DateTime Jzsj { get; set; }

        /// <summary>
        /// trạng thái 0: không hiệu lực  1: còn hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

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
