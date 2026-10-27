namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Thông tin vật tư trong danh sách thả xuống
    /// </summary>
    public class VSelProductInfoEntity
    {
        /// <summary>
        /// Vật tư ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên vật tư
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tồn kho Đơn vị nhỏ nhất
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Tồn kho khả dụng Đơn vị nhỏ nhất
        /// </summary>
        public int kykcsl { get; set; }

        /// <summary>
        /// Số lượng đơn vị khoa phòng（Kèm đơn vị）
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal minlsj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị khoa phòng
        /// </summary>
        public decimal bmlsj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi đơn vị khoa phòng
        /// </summary>
        public int bmdwzhyz { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà sản xuất Id
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Tên nhà sản xuất
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất ID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// Đơn vị khoa phòng ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// Tên đơn vị khoa phòng
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Loại ID
        /// </summary>
        public string lbId { get; set; }
        /// <summary>
        /// Mã vật tư tiêu hao
        /// </summary>
        public string productCode { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia(Vật tư tiêu hao 27 ký tự)
        /// </summary>
        public string hcgjybdm { get; set; }
        /// <summary>
        /// Mã thống nhất vật tư tiêu hao
        /// </summary>
        public string gjybdm { get; set; }
        /// <summary>
        /// Giá nhập
        /// </summary>
        public decimal jj { get; set; }
    }
}
