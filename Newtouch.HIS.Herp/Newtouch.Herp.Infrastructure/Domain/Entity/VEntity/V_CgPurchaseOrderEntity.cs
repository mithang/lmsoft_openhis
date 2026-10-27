using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Bảng chính kế hoạch mua sắm
    /// </summary>
    public class VCgPurchaseOrderEntity
    {
        /// <summary>
        /// Số phiếu kế hoạch mua sắm
        /// </summary>
        public string cgdh { get; set; }

        /// <summary>
        /// Khoa nhập kho Code
        /// </summary>
        public string rkbmCode { get; set; }

        /// <summary>
        /// Tên khoa nhập kho
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// Trạng thái duyệt 0-Chờ duyệt； 1-Đã duyệt； 2-Không duyệt；3-Đã hủy bỏ；4-Lưu tạm
        /// </summary>
        public int auditState { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// Tài khoản người sửa đổi
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Tên người sửa đổi
        /// </summary>
        public string LastModifierName { get; set; }
    }

    /// <summary>
    /// Chi tiết kế hoạch mua sắm
    /// </summary>
    public class VCgPurchaseOrderDetailEntity
    {
        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Số lượng mua theo kế hoạch sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Số lượng mua thực tế sjsl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sjsl { get; set; }

        /// <summary>
        /// Số lượng mua theo kế hoạch（Kèm đơn vị）
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Số lượng mua thực tế（Kèm đơn vị）
        /// </summary>
        public string sjslStr { get; set; }

        /// <summary>
        /// Tiếp tục mua sắm sl-sjsl
        /// </summary>
        public int jxcgs { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Đơn vị ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// Tên loại vật tư
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Tên hãng sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Giá nhập  jj/zhyz=Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Chi tiết kế hoạch mua sắm ID，cg_purchaseOrderDetail Khóa chính của bảng
        /// </summary>
        public long pdId { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// Số lượng đặt tối thiểu(Đơn vị nhỏ nhất)
        /// </summary>
        public int zxqds { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string supplierId { get; set; }
        public string supplierName { get; set; }
    }
}