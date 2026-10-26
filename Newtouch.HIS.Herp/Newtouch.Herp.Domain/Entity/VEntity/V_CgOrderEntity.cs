using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin đơn mua hàng
    /// </summary>
    public class VCgOrderEntity
    {

        /// <summary>
        /// Loại đơn hàng 0:Phiếu lưu tạm;1:Phiếu chính thức
        /// </summary>
        public int orderType { get; set; }

        /// <summary>
        /// Quy trình xử lý đơn đặt mua hàng -1:Từ chối xử lý; 0:Chờ xử lý; 1:Chuẩn bị hàng; 2:Giao hàng; 3:Ký nhận; 4:Hoàn thành; 5:Từ chối ký 
        /// </summary>
        public int orderProcess { get; set; }

        /// <summary>
        /// Số đơn mua hàng
        /// </summary>
        public string orderNo { get; set; }

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
        public string CreatorName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string LastModifierName { get; set; }
    }

    /// <summary>
    /// Chi tiết đơn mua hàng
    /// </summary>
    public class VCgOrderDetailEntity
    {
        /// <summary>
        /// Số đơn hàng con
        /// </summary>
        public string subOrderNo { get; set; }

        /// <summary>
        /// Số phiếu kế hoạch mua hàng
        /// </summary>
        public string cgdh { get; set; }

        /// <summary>
        /// Tên khoa phòng
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Số lượng+Đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        ///Giá nhập+Đơn vị
        /// </summary>
        public string jjStr { get; set; }

        /// <summary>
        /// Tên nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// Ghi chú chi tiết
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Sản phẩm ID
        /// </summary>
        public string productId { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// Ngày đơn hàng
        /// </summary>
        public DateTime orderData { get; set; }
        /// <summary>
        /// Tổng tiền nhập
        /// </summary>
        public decimal? jjze { get; set; }
    }
    public class VCgOrderRkEntity
    {
        /// <summary>
        /// Thời gian đơn hàng
        /// </summary>
        public DateTime orderDate { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string orderNo { get; set; }
        /// <summary>
        /// Số đơn hàng con-Cùng một Số đơn hàng nhóm theo nhà cung cấp
        /// </summary>
        public string subOrderNo { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string gysmc { get; set; }
        /// <summary>
        /// Hóa đơn
        /// </summary>
        public string fp { get; set; }
        /// <summary>
        /// Hóa đơn
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// Số chi tiết mua hàng
        /// </summary>
        public int cgmxs { get; set; }
        /// <summary>
        /// Tiền mua hàng
        /// </summary>
        public decimal? cgje { get; set; }
    }

    public class VCgOrderImportEntity
    {
        public string wzmc { get; set; }
        public string lbmc { get; set; }
        public string gjybdm { get; set; }
        public int sl { get; set; }
        public string unitId { get; set; }
        public string unitName { get; set; }
        public decimal jjze { get; set; }
        public string fph { get; set; }
        public string remark { get; set; }
        public int kcsl { get; set; }
        public int kykcsl { get; set; }
        public string slStr { get; set; }
        public string gg { get; set; }
        public string sccj { get; set; }
        public string gysId { get; set; }
        public string gysmc { get; set; }
        public decimal lsj { get; set; }
        public decimal lsze { get; set; }
        public int zhyz { get; set; }
        public string productId { get; set; }
        public decimal jj { get; set; }
        public decimal minlsj { get; set; }
    }
}