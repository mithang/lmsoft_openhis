using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	public class OutPatChongQingVO
	{
		/// <summary>
		/// Số khám ngoại trú
		/// </summary>
		public string mzh { get; set; }
		/// <summary>
		/// Mã nội bộ quyết toán
		/// </summary>
		public int jsnm { get; set; }

		/// <summary>
		/// Ngày thu phí
		/// </summary>
		public DateTime? sfrq { get; set; }

		/// <summary>
		/// Nhân viên quyết toán
		/// </summary>
		public string CreatorCode { get; set; }

		/// <summary>
		/// Thời gian quyết toán
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// Tên nhân viên quyết toán
		/// </summary>
		public string CreatorUserName { get; set; }

		/// <summary>
		/// Đăng ký khám ngoại trú hoặc Ghi sổ thu phí ngoại trú
		/// </summary>
		public string jslx { get; set; }

		/// <summary>
		/// Số hóa đơn
		/// </summary>
		public string fph { get; set; }

		/// <summary>
		/// Quyết toán BHYT Số quyết toán BHYT
		/// </summary>
		public string ybjsh { get; set; }

		/// <summary>
		/// Tổng số tiền
		/// </summary>
		public decimal jszje { get; set; }

		/// <summary>
		/// Thanh toán quyết toán
		/// </summary>
		public decimal jsxjzf { get; set; }
		/// <summary>
		/// Tên phương thức thanh toán tiền mặt
		/// </summary>
		public string xjzffsmc { get; set; }
        /// <summary>
        /// Nguồn đăng ký khám
        /// </summary>
        public string ghly { get; set; } 
        public string jzid { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
        public string bzbm { get; set; }
    }
}
