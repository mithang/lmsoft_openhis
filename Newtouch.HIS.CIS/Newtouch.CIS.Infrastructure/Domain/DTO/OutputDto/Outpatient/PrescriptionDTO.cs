using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    public class PrescriptionDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// Liệt kê 1 Thuốc Tây Đơn thuốc 2 Thuốc Đông y Đơn thuốc 3 Phục hồi chức năng Đơn thuốc 4 Xét nghiệm Đơn thuốc 5 Kiểm tra Đơn thuốc 6 truyền dịch Đơn thuốc
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// dán số
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Nguyên nhân lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng ký Ghi chú
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Đơn thuốc chi tiết
        /// </summary>
        public List<PrescriptionDetailVO> cfmxList { get; set; }

        /// <summary>
        /// Đơn thuốc nhãn JI tinhI JII tinh II MZ tê say
        /// </summary>
        public string cftag { get; set; }
        public string jzId { get; set; }


        public string isdzcf { get; set; }//điện tử Đơn thuốc 1 là  0 Không
        public string gjybdm { get; set; }

        /// <summary>
        /// sắc hộ phương kiểu
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// sắc hộ trả số
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }

		/// <summary>
		/// Đồng bộ Cờ
		/// </summary>
		public bool? tbbz { get; set; }

		/// <summary>
		/// trả Cờ
		/// </summary>
		public bool? tbz { get; set; }

		/// <summary>
		/// Số phiếu
		/// </summary>
		public string sqdh { get; set; }

		/// <summary>
		/// xa trình y tế Tải lên Trạng thái
		/// </summary>
		public string SyncStatus { get; set; }


		/// <summary>
		/// Đơn thuốc chỉnh liềuY lệnh Mô tả
		/// </summary>
		public string rxDrordDscr { get; set; }

		/// <summary>
		/// Đơn thuốc Hiệu lực Số ngày
		/// </summary>
		public int? valiDays { get; set; }

		/// <summary>
		/// lại dùng(nhiều lần)Sử dụng Cờ
		/// </summary>
		public string reptFlag { get; set; }

		/// <summary>
		/// nhất lớn Sử dụng lần số
		/// </summary>
		public int? maxReptCnt { get; set; }

		/// <summary>
		/// Sử dụngnhấtnhỏgiữacách(Số ngày)
		/// </summary>
		public int? minInrvDays { get; set; }

		/// <summary>
		/// tiếp phương Cờ
		/// </summary>
		public string rxCotnFlag { get; set; }

		/// <summary>
		/// Dài hạn Đơn thuốc Cờ
		/// </summary>
		public string longRxFlag { get; set; }

		/// <summary>
		/// Đơn thuốc truy xuất mã
		/// </summary>
		public string rxTraceCode { get; set; }

		/// <summary>
		/// Bảo hiểm y tế Đơn thuốc Số hiệu
		/// </summary>
		public string hiRxno { get; set; }
	}
}
