using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin điều chỉnh giá
    /// </summary>
    public class VPriceAdjustmentInfoEntity
    {
        /// <summary>
        /// Khóa chính Điều chỉnh giá vật tư Id
        /// </summary>
        public string wztjId { get; set; }

        /// <summary>
        /// Trạng thái duyệt   0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã hủy
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Cờ thực hiện   0:Chưa thực hiện 1:Đã thực hiện
        /// </summary>
        public string zxbz { get; set; }

        /// <summary>
        /// Tên loại cột
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Tên vật tư  
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc Tương ứng với hệ số chuyển đổi,Thông thường là giá đơn vị khoa phòng
        /// </summary>
        public decimal ylsj { get; set; }

        /// <summary>
        /// Giá bán lẻ Tương ứng với hệ số chuyển đổi,Thông thường là giá đơn vị khoa phòng
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Đơn vị Và zhyz Tương ứng,Thông thường là đơn vị khoa phòng
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Điều chỉnh tệp
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// Thời gian điều chỉnh
        /// </summary>
        public DateTime tzsj { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime? zxsj { get; set; }

        /// <summary>
        /// Có hết hạn không
        /// </summary>
        public string Isgq { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Người gửi
        /// </summary>
        public string CreatorCode { get; set; }
    }
}
