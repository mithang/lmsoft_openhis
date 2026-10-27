using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Tổng hợp tồn kho vật tư theo lô và số lô
    /// </summary>
    public class VProductStorageDetailEntity
    {
        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô hàng
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Số lượng đóng băng của khoa phòng  Kèm đơn vị
        /// </summary>
        public string bmdjslStr { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// Số lượng đóng băng
        /// </summary>
        public int? djsl { get; set; }

        /// <summary>
        /// Số lượng tồn kho của khoa phòng Kèm đơn vị
        /// </summary>
        public string bmkcslStr { get; set; }

        /// <summary>
        /// Tổng số tiền giá nhập
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lb { get; set; }

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
        /// Đơn vị đơn giá
        /// </summary>
        public string bmjjdjdw { get; set; }
    }
}
