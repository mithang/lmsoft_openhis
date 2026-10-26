using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Chi tiết chứng từ nhập/xuất kho
    /// </summary>
    public class VCrkdjmxInfoEntity
    {
        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Số bán buôn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal minjj { get; set; }

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
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Tổng tiền  Giá nhập
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// Tồn kho hiện có
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Tồn kho khoa nhập kho 
        /// </summary>
        public int rkbmkc { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Ngày sản xuất
        /// </summary>
        public DateTime? scrq { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất ID
        /// </summary>
        public string zxdwId { get; set; }
                
        /// <summary>
        /// Hệ số chuyển đổi đơn vị nhỏ nhất
        /// </summary>
        public int zxdwzhyz { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng Id
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi đơn vị khoa phòng
        /// </summary>
        public int bmdwzhyz { get; set; }

        /// <summary>
        /// Giá bán lẻ nhỏ nhất
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// Khoa xuất kho
        /// </summary>
        public string ckbm { get; set; }

        /// <summary>
        /// Số lượng tồn kho khả dụng
        /// </summary>
        public int kykcsl { get; set; }
        /// <summary>
        /// Mã bảo hiểm y tế quốc gia
        /// </summary>
        public string gjybdm { get; set; }
    }
}
