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
        /// Enum 1 Đơn thuốc tây 2 Đơn thuốc đông y 3 Đơn thuốc phục hồi chức năng 4 Đơn xét nghiệm 5 Đơn kiểm tra 6 Đơn truyền dịch
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
        /// Số thang
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
        /// Lý do lâm sàng
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Ghi chú yêu cầu
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốc
        /// </summary>
        public List<PrescriptionDetailVO> cfmxList { get; set; }

        /// <summary>
        /// Nhãn đơn thuốc JI Hướng thầnI JII Hướng thầnII MZ Gây mê
        /// </summary>
        public string cftag { get; set; }
        public string jzId { get; set; }


        public string isdzcf { get; set; }//Đơn thuốc điện tử 1 Có  0 Không
        public string gjybdm { get; set; }

        /// <summary>
        /// Cách sắc hộ
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// Số thang sắc hộ
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }

		/// <summary>
		/// Cờ đồng bộ
		/// </summary>
		public bool? tbbz { get; set; }

		/// <summary>
		/// Cờ trả lại
		/// </summary>
		public bool? tbz { get; set; }

		/// <summary>
		/// Số phiếu yêu cầu
		/// </summary>
		public string sqdh { get; set; }

		/// <summary>
		/// Trạng thái tải lên y tế từ xa
		/// </summary>
		public string SyncStatus { get; set; }


		/// <summary>
		/// Giải thích y lệnh nguyên thang đơn thuốc
		/// </summary>
		public string rxDrordDscr { get; set; }

		/// <summary>
		/// Số ngày hiệu lực đơn thuốc
		/// </summary>
		public int? valiDays { get; set; }

		/// <summary>
		/// Tái sử dụng(Nhiều lần)Cờ sử dụng
		/// </summary>
		public string reptFlag { get; set; }

		/// <summary>
		/// Số lần sử dụng tối đa
		/// </summary>
		public int? maxReptCnt { get; set; }

		/// <summary>
		/// Khoảng cách tối thiểu khi sử dụng(Số ngày)
		/// </summary>
		public int? minInrvDays { get; set; }

		/// <summary>
		/// Cờ kê đơn tiếp
		/// </summary>
		public string rxCotnFlag { get; set; }

		/// <summary>
		/// Cờ đơn thuốc dài hạn
		/// </summary>
		public string longRxFlag { get; set; }

		/// <summary>
		/// Mã truy xuất đơn thuốc
		/// </summary>
		public string rxTraceCode { get; set; }

		/// <summary>
		/// Số đơn thuốc BHYT
		/// </summary>
		public string hiRxno { get; set; }
	}
}
