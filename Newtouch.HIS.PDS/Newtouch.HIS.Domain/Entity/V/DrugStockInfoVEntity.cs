using System;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// thuốc thông tin tồn kho thuốc chọn dùng
    /// </summary>
    public class DrugStockInfoVEntity
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// phiên âm
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// tên nhóm lớn
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// tên nhóm lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// quy cách
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// số lượng tồn kho
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// số bộ phận lượng
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// kèm đơn vị số bộ phận lượng
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// kèm đơn vị bộ phận số lượng tồn kho
        /// </summary>
        public string kcslStr { get; set; }

        /// <summary>
        /// kèm đơn vị bộ phận số lượng đóng băng
        /// </summary>
        public string djslStr { get; set; }

        /// <summary>
        /// đơn vị số lượng đóng gói
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// đơn vị số lượng đóng gói
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// đơn vị nhỏ nhất
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// số lượng đóng gói
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// giá bán buôn bộ phận
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// tổng tiền bán buôn
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        /// giá bán lẻ bộ phận
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// giá bán lẻ đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwlsj { get; set; }

        /// <summary>
        /// tổng tiền bán lẻ
        /// </summary>
        public decimal? lsze { get; set; }

        /// <summary>
        /// tổng tiền giá nhập
        /// </summary>
        public decimal? jjze { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// nhà sản xuất
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// tồn kho khả dụng  đơn vị nhỏ nhất
        /// </summary>
        public int? kykc { get; set; }

        /// <summary>
        /// số giấy phép
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// giá bán lẻ đơn giá đơn vị
        /// </summary>
        public string lsjdjdw { get; set; }

        /// <summary>
        /// đơn vị đơn giá bán buôn
        /// </summary>
        public string pfjdjdw { get; set; }

        /// <summary>
        /// giá nhập đơn giá đơn vị
        /// </summary>
        public string jjdjdw { get; set; }

        /// <summary>
        /// giá nhập đơn vị nhỏ nhất
        /// </summary>
        public decimal? zxdwjj { get; set; }

        /// <summary>
        /// đơn vị đóng gói giá nhập
        /// </summary>
        public decimal? bzdwjj { get; set; }

        /// <summary>
        /// đơn vị bộ phận giá nhập
        /// </summary>
        public decimal? bmdwjj { get; set; }

        /// <summary>
        /// trạng thái tồn kho  0-không hiệu lực  1-còn hiệu lực
        /// </summary>
        public string kczt { get; set; }

		/// <summary>
		/// tên nhà cung cấp
		/// </summary>
		public string gysmc { get; set; }
        /// <summary>
        /// mã BHYT quốc gia
        /// </summary>
        public string gjybdm { get; set; }

        public string fph { get; set; }
        public int? kcyjz { get; set; }
	}
}
