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
        /// Liệt kê 1 Thuốc TâyĐơn thuốc 2 Thuốc Đông yĐơn thuốc 3 Phục hồi chức năngĐơn thuốc 4 Xét nghiệmĐơn thuốc 5 Kiểm traĐơn thuốc 6 truyền dịchĐơn thuốc
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
        /// dánsố
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
        /// 临giườngnguyên nhân
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// Đăng kýGhi chú
        /// </summary>
        public string sqbz { get; set; }

        /// <summary>
        /// Đơn thuốcchi tiết
        /// </summary>
        public List<PrescriptionDetailVO> cfmxList { get; set; }

        /// <summary>
        /// Đơn thuốcnhãn JI tinhI JII tinhII MZ têsay
        /// </summary>
        public string cftag { get; set; }
        public string jzId { get; set; }


        public string isdzcf { get; set; }//điện tửĐơn thuốc 1 là  0 Không
        public string gjybdm { get; set; }

        /// <summary>
        /// sắc hộphươngkiểu
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// sắc hộtrảsố
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// Dặn dò
        /// </summary>
        public string cfzt { get; set; }

		/// <summary>
		/// Đồng bộCờ
		/// </summary>
		public bool? tbbz { get; set; }

		/// <summary>
		/// trảCờ
		/// </summary>
		public bool? tbz { get; set; }

		/// <summary>
		/// Số phiếu
		/// </summary>
		public string sqdh { get; set; }

		/// <summary>
		/// xatrìnhy tếTải lênTrạng thái
		/// </summary>
		public string SyncStatus { get; set; }


		/// <summary>
		/// Đơn thuốcchỉnhliềuY lệnhMô tả
		/// </summary>
		public string rxDrordDscr { get; set; }

		/// <summary>
		/// Đơn thuốcHiệu lựcSố ngày
		/// </summary>
		public int? valiDays { get; set; }

		/// <summary>
		/// lạidùng(nhiều lần)Sử dụngCờ
		/// </summary>
		public string reptFlag { get; set; }

		/// <summary>
		/// nhấtlớnSử dụng lầnsố
		/// </summary>
		public int? maxReptCnt { get; set; }

		/// <summary>
		/// Sử dụngnhấtnhỏgiữacách(Số ngày)
		/// </summary>
		public int? minInrvDays { get; set; }

		/// <summary>
		/// tiếpphươngCờ
		/// </summary>
		public string rxCotnFlag { get; set; }

		/// <summary>
		/// Dài hạnĐơn thuốcCờ
		/// </summary>
		public string longRxFlag { get; set; }

		/// <summary>
		/// Đơn thuốctruy xuấtmã
		/// </summary>
		public string rxTraceCode { get; set; }

		/// <summary>
		/// Bảo hiểm y tếĐơn thuốcSố hiệu
		/// </summary>
		public string hiRxno { get; set; }
	}
}
