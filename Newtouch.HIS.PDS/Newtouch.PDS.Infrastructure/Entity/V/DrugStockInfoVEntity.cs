using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Thông tin tồn kho thuốc Chọn thuốc để dùng
    /// </summary>
    public class DrugStockInfoVEntity
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// Hạn dùng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// Số lượng bộ phận
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// Số lượng bộ phận kèm đơn vị
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// Số lượng tồn kho bộ phận kèm đơn vị
        /// </summary>
        public string kcslStr { get; set; }

        /// <summary>
        /// Số lượng đóng băng bộ phận kèm đơn vị
        /// </summary>
        public string djslStr { get; set; }

        /// <summary>
        /// Đơn vị số đóng gói
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Đơn vị số đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// Số lượng đóng gói
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// Giá bán buôn bộ phận
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// Tổng giá bán buôn
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// Giá bán lẻ bộ phận
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwlsj { get; set; }

        /// <summary>
        /// Tổng giá bán lẻ
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// Tổng giá nhập
        /// </summary>
        public decimal? jjze { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// Lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Tồn kho khả dụng  Đơn vị nhỏ nhất
        /// </summary>
        public int? kykc { get; set; }

        /// <summary>
        /// Số văn bản phê duyệt
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// Đơn vị đơn giá bán lẻ
        /// </summary>
        public string lsjdjdw { get; set; }

        /// <summary>
        /// Đơn vị đơn giá bán buôn
        /// </summary>
        public string pfjdjdw { get; set; }

        /// <summary>
        /// Đơn vị đơn giá nhập
        /// </summary>
        public string jjdjdw { get; set; }

        /// <summary>
        /// Giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// Giá nhập đơn vị đóng gói
        /// </summary>
        public decimal? bzdwjj { get; set; }

        /// <summary>
        /// Giá nhập đơn vị bộ phận
        /// </summary>
        public decimal? bmdwjj { get; set; }

        /// <summary>
        /// Trạng thái tồn kho  0-Không hiệu lực  1-Hiệu lực
        /// </summary>
        public string kczt { get; set; }

		/// <summary>
		/// Tên nhà cung cấp
		/// </summary>
		public string gysmc { get; set; }
        /// <summary>
        /// Mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }

        public string fph { get; set; }
        public int? kcyjz { get; set; }
	}
}
