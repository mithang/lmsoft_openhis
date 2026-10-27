using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Tồn kho vật tư
    /// </summary>
    public class VProductStorageEntity
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
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Số lượng kèm đơn vị của khoa phòng
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Tổng tồn kho  Đơn vị nhỏ nhất
        /// </summary>
        public int zkc { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// Đơn giá nhập（với kcxx.zhyz tương ứng）
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Đơn giá nhập（Đơn vị nhỏ nhất）
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// Tổng giá nhập
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }
        
        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Hãng sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Hạn sử dụng varchar（10）
        /// </summary>
        public string yxqStr { get; set; }

        /// <summary>
        /// Hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Tồn kho khả dụng
        /// </summary>
        public int kykcsl { get; set; }
        /// <summary>
        /// Giá trị cảnh báo tồn kho
        /// </summary>
        public int? kcyjz { get; set; }
        //Giá nhập đơn vị nhỏ nhất của vật tư
        public decimal? wzjj { get; set; }
        //Giá bán lẻ đơn vị nhỏ nhất của vật tư
        public decimal? wzlsj { get; set; }

    }
}
