using System;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// vật tưTồn khophútLôSố lôhộitổng
    /// </summary>
    public class VProductStorageDetailEntity
    {
        public string Id { get;set; }
        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tưTên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Khoa phòngTên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhấtTên
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Khoa phòngđôngkếtSố lượng  mangĐơn vị
        /// </summary>
        public string bmdjslStr { get; set; }

        /// <summary>
        /// Tồn khoSố lượng
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// đôngkếtSố lượng
        /// </summary>
        public int? djsl { get; set; }

        /// <summary>
        /// Khoa phòngTồn khoSố lượng mangĐơn vị
        /// </summary>
        public string bmkcslStr { get; set; }

        /// <summary>
        /// vàogiáTổng số tiền
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// vàogiá
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// vàogiámangĐơn vị
        /// </summary>
        public string jjStr { get; set; }
        /// <summary>
        /// vàogiá
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal lsze { get; set; }
        /// <summary>
        /// Giá bán lẻmangĐơn vị
        /// </summary>
        public string lsjStr { get; set; }
        /// <summary>
        /// Tên loại
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// sinhsản xuấtTên nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Đơn giáĐơn vị
        /// </summary>
        public string bmjjdjdw { get; set; }
    }
}
