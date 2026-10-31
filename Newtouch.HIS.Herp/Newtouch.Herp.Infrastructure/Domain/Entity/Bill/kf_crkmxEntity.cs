using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Chi tiết chứng từ nhập/xuất kho
    /// </summary>
    [SugarTable("kf_crkmx")]
    public class KfCrkmxEntity : IEntity<KfCrkmxEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// Chứng từ nhập/xuất kho ID
        /// </summary>
        public long crkId { get; set; }

        /// <summary>
        /// Chi tiết phiếu lĩnh ID
        /// </summary>
        public long? applyDetailId { get; set; }

        /// <summary>
        /// Đơn mua sắm Id
        /// </summary>
        public long? purchaseId { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Số lượng,sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Đơn vị ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô hàng
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime yxq { get; set; }

        /// <summary>
        /// Giá nhập,với sl cùng đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal jj { get; set; }

        /// <summary>
        /// Giá bán lẻ,với sl cùng đơn vị
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Tồn kho khoa nhập kho,Thống kê trước thao tác,Đơn vị nhỏ nhất
        /// </summary>
        public int rkbmkc { get; set; }

        /// <summary>
        /// Tồn kho khoa xuất kho,Thống kê trước thao tác,Đơn vị nhỏ nhất
        /// </summary>
        public int ckbmkc { get; set; }

        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
