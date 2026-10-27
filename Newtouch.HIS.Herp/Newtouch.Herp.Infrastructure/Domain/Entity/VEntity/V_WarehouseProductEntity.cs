namespace Newtouch.Herp.Domain.Entity.VEntity
{
    public class VWarehouseProductEntity
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
        /// Cờ kiểm soát
        /// </summary>
        public string kzbz { get; set; }

        /// <summary>
        /// Trạng thái vật tư
        /// </summary>
        public string wzzt { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// Số lượng đặt tối thiểu  Đơn vị nhỏ nhất
        /// </summary>
        public int? zxqds { get; set; }

        /// <summary>
        /// Giá bán lẻ  Đơn vị nhỏ nhất
        /// </summary>
        public decimal lsj { get; set; }

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
        /// Có phải tồn kho bằng 0 không
        /// </summary>
        public string sflkc { get; set; }

        /// <summary>
        /// Có tái sử dụng không
        /// </summary>
        public string sffy { get; set; }

        /// <summary>
        /// Có theo ca mổ không
        /// </summary>
        public string sfgt { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }
    }
}
