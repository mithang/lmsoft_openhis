using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// Vật tư
    /// </summary>
    public class VProductInfoEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tổ chức Id(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên vật tư(Bắt buộc)
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Loại,Nhóm lớn(Bắt buộc)
        /// </summary>
        public string typeId { get; set; }

        /// <summary>
        /// Số giấy đăng ký
        /// </summary>
        public string zczh { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Đường dẫn hình ảnh
        /// </summary>
        public string imageUrl { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Model và quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Nhà cung cấp ID
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string minUnit { get; set; }

        /// <summary>
        /// Số lượng đặt tối thiểu
        /// </summary>
        public int? zxqds { get; set; }

        /// <summary>
        /// Giá nhập,Đơn vị nhỏ nhất 
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// Giá bán lẻ Đơn vị nhỏ nhất
        /// </summary>
        public decimal lsj { get; set; }

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
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Mã vật tư
        /// </summary>
        public string productCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối
        /// </summary>
        public string LastModifierCode { get; set; }

		/// <summary>
		/// Mã phân loại chi tiết vật tư(Mua sắm)
		/// </summary>
		public string hcgjybdm { get; set; }

		/// <summary>
		/// Cờ vật tư y tế(Đồng bộ vào BASE CSDL) 1:Vật tư y tế 0: Vật tư khác(không cần đồng bộ)
		/// </summary>
		public string iswzsame { get; set; }

		/// <summary>
		/// Tính chất tự trả
		/// </summary>
		public string zfxz { get; set; }

		/// <summary>
		/// Tỷ lệ tự trả
		/// </summary>
		public decimal? zfbl { get; set; }

		/// <summary>
		/// Mã BHYT quốc gia
		/// </summary>
		public string gjybdm { get; set; }

		/// <summary>
		/// Mã BHYT tỉnh
		/// </summary>
		public string ybdm { get; set; }
        
        /// <summary>
        /// Loại sổ sách
        /// </summary>
        public string zblb { get; set; }
        /// <summary>
        /// Loại hạch toán
        /// </summary>
        public string hslb { get; set; }
        public int? kcyjz { get; set; }
	}

    /// <summary>
    /// Thông tin sản phẩm
    /// </summary>
    public class VProductInfoDo
    {
        /// <summary>
        /// Sản phẩm ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// Tên loại
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// Thương hiệu
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Tên đơn vị nhỏ nhất
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Hãng sản xuất
        /// </summary>
        public string sccj { get; set; }
    }

    /// <summary>
    /// Đơn vị vật tư
    /// </summary>
    public class ProductUnit
    {

        /// <summary>
        /// Đơn vị ID
        /// </summary>
        public string unitId { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string dwmc { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Sản phẩm ID
        /// </summary>
        public string productId { get; set; }
    }
}
