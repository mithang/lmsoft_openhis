using System;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// vật tư Tồn kho phút Lô Số lô hội tổng
    /// </summary>
    public class VProductStorageDetailEntity
    {
        public string Id { get;set; }
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tư Tên
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
        /// Khoa phòng Tên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất Tên
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// chuyển hóa vì con
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Khoa phòng đông kết Số lượng  mang Đơn vị
        /// </summary>
        public string bmdjslStr { get; set; }

        /// <summary>
        /// Tồn kho Số lượng
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// đông kết Số lượng
        /// </summary>
        public int? djsl { get; set; }

        /// <summary>
        /// Khoa phòng Tồn kho Số lượng mang Đơn vị
        /// </summary>
        public string bmkcslStr { get; set; }

        /// <summary>
        /// vào giá Tổng số tiền
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// vào giá
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// vào giá mang Đơn vị
        /// </summary>
        public string jjStr { get; set; }
        /// <summary>
        /// vào giá
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal lsze { get; set; }
        /// <summary>
        /// Giá bán lẻ mang Đơn vị
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
        /// sinh sản xuất Tên nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Đơn giá Đơn vị
        /// </summary>
        public string bmjjdjdw { get; set; }
    }
}
