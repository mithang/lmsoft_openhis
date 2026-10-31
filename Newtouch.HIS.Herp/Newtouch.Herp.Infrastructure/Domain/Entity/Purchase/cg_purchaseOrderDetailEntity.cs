using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Mua sắm_Chi tiết kế hoạch mua sắm
    /// </summary>
    [SugarTable("cg_purchaseOrderDetail")]
    public class CgPurchaseOrderDetailEntity : IEntity<CgPurchaseOrderDetailEntity>
    {
        /// <summary>
        /// Khóa chính ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Phiếu kế hoạch mua sắm ID
        /// </summary>
        public long purchaseId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lượng mua theo kế hoạch sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng mua thực tế sjsl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// Đơn vị ID(Dư thừa)
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Trạng thái 0:Hủy bỏ;1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người dùng tạo ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối ID
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string supplierId { get; set; }
        public string supplierName { get; set; }
    }
}