using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin chính chứng từ nhập/xuất kho
    /// </summary>
    public class VCrkdjEntity
    {
        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Số phiếu
        /// </summary>
        public string Pdh { get; set; }

        /// <summary>
        /// Tên khoa xuất kho
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// Khoa xuất kho ID
        /// </summary>
        public string ckbmId { get; set; }

        /// <summary>
        /// Tên khoa nhập kho
        /// </summary>
        public string rkbmmc { get; set; }

        /// <summary>
        /// Khoa nhập kho ID
        /// </summary>
        public string rkbmId { get; set; }

        /// <summary>
        /// Hình thức nhập/xuất kho ID
        /// </summary>
        public string crkfsId { get; set; }

        /// <summary>
        /// Tên hình thức nhập/xuất kho
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime czsj { get; set; }

        /// <summary>
        /// Thời gian xuất kho
        /// </summary>
        public string cksj { get; set; }

        /// <summary>
        /// Thời gian nhập kho
        /// </summary>
        public string rksj { get; set; }

        /// <summary>
        /// Tổng tiền bán lẻ
        /// </summary>
        public decimal ljze { get; set; }

        /// <summary>
        /// Tổng tiền nhập
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Chênh lệch tổng tiền nhập-xuất
        /// </summary>
        public decimal jxcj { get; set; }

        /// <summary>
        /// Chứng từ nhập/xuất kho ID
        /// </summary>
        public long crkId { get; set; }

        /// <summary>
        /// Loại chứng từ ID
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// Số phiếu giao hàng
        /// </summary>
        public string deliveryNo { get; set; }

    }
}
