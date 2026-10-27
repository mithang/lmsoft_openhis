
namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Đồng bộ vật tư
    /// </summary>
    public class VSyncProductEntity
    {
        /// <summary>
        /// rel_productWarehouse Khóa chính của bảng
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Loại vật tư
        /// </summary>
        public string wzlb { get; set; }

        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// Hãng sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Cờ kiểm soát  1：Hiệu lực  0：Không hiệu lực（Bị kiểm soát）
        /// </summary>
        public string zt { get; set; }
    }
}
