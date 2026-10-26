using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết đơn hàng ngoại trú--2023-9-8 chl
    /// </summary>
    [Table("order_mz")]
    public class OrderMzEntity : IEntity<OrderMzEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// Số tiền đơn thuốc
        /// </summary>
        public decimal? cfje { get; set; }
        /// <summary>
        /// Hạng mục thu phí đơn thuốc
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// Tên hạng mục thu phí đơn thuốc
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Chi tiết đơn thuốc
        /// </summary>
        public string cfxmmx { get; set; }
        /// <summary>
        /// Loại đơn thuốc EnumCflx
        /// </summary>
        public string cflx { get; set; }
        /// <summary>
        /// Có phải là đơn thuốc hay không
        /// </summary>
        public string isyp { get; set; }
        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
