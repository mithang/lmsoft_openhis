using System;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// vật tư Tồn kho
    /// </summary>
    public class VProductStorageEntity
    {
        public string ks { get; set; }
        public string ksName { get; set; }
        /// <summary>
        /// vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tư Tên
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Khoa phòngmang Đơn vị Số lượng
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// tổng Tồn kho  Đơn vị nhỏ nhất
        /// </summary>
        public int zkc { get; set; }
        /// <summary>
        /// Giá bán lẻmang Đơn vị
        /// </summary>
        public string lsjStr { get; set; }
        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// tổng bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// vàogiá Đơn giá(vàkcxx.zhyztương ứng)
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// vàogiá Đơn giá(Đơn vị nhỏ nhất)
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// vàogiátổngmức
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Khoa phòng Tên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }
        
        /// <summary>
        /// Đơn vị nhỏ nhất Tên
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Hạn hiệu lực varchar(10)
        /// </summary>
        public string yxqStr { get; set; }

        /// <summary>
        /// Hạn hiệu lực
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// chuyểnhóavìcon
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// có thểdùng Tồn kho
        /// </summary>
        public int kykcsl { get; set; }
        /// <summary>
        /// Giá trị cảnh báo tồn kho
        /// </summary>
        public int? kcyjz { get; set; }
        //vật tư Đơn vị nhỏ nhấtvàogiá
        public decimal? wzjj { get; set; }
        //vật tư Đơn vị nhỏ nhất Giá bán lẻ
        public decimal? wzlsj { get; set; }
        
        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

    }
}
