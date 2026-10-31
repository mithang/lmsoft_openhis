using System;

namespace Newtouch.Domain.ValueObjects.Storage
{
    /// <summary>
    /// vật tưTồn kho
    /// </summary>
    public class VProductStorageEntity
    {
        public string ks { get; set; }
        public string ksName { get; set; }
        /// <summary>
        /// vật tưID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// vật tưTên
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
        /// Khoa phòngmangĐơn vịSố lượng
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// tổngTồn kho  Đơn vị nhỏ nhất
        /// </summary>
        public int zkc { get; set; }
        /// <summary>
        /// Giá bán lẻmangĐơn vị
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
        /// vàogiáĐơn giá(vàkcxx.zhyztương ứng)
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// vàogiáĐơn giá(Đơn vị nhỏ nhất)
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// vàogiátổngmức
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Khoa phòngTên đơn vị
        /// </summary>
        public string bmdwmc { get; set; }
        
        /// <summary>
        /// Đơn vị nhỏ nhấtTên
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
        /// có thểdùngTồn kho
        /// </summary>
        public int kykcsl { get; set; }
        /// <summary>
        /// Giá trị cảnh báo tồn kho
        /// </summary>
        public int? kcyjz { get; set; }
        //vật tưĐơn vị nhỏ nhấtvàogiá
        public decimal? wzjj { get; set; }
        //vật tưĐơn vị nhỏ nhấtGiá bán lẻ
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
