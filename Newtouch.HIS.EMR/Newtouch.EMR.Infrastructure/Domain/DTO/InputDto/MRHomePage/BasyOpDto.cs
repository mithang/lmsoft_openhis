using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.InputDto
{
	public class BasyOpDto
	{
		public string Id { get; set; }

		/// <summary>
		/// BAH
		/// </summary>
		/// <returns></returns>
		public string BAH { get; set; }

		/// <summary>
		/// ZYH
		/// </summary>
		/// <returns></returns>
		public string ZYH { get; set; }

		/// <summary>
		/// SSOrder
		/// </summary>
		/// <returns></returns>
		public int SSOrder { get; set; }

		/// <summary>
		/// Mã phẫu thuật và thủ thuật
		/// </summary>
		/// <returns></returns>
		public string SSJCZBM { get; set; }

		/// <summary>
		/// Ngày phẫu thuật và thao tác
		/// </summary>
		/// <returns></returns>
		public DateTime SSJCZRQ { get; set; }

		/// <summary>
		/// Cấp độ phẫu thuật
		/// </summary>
		/// <returns></returns>
		public string SSJB { get; set; }

		/// <summary>
		/// Tên phẫu thuật và thủ thuật
		/// </summary>
		/// <returns></returns>
		public string SSJCZMC { get; set; }

		/// <summary>
		/// Người thực hiện phẫu thuật
		/// </summary>
		/// <returns></returns>
		public string SZ { get; set; }

		/// <summary>
		/// Phụ mổ I
		/// </summary>
		/// <returns></returns>
		public string YZ { get; set; }

		/// <summary>
		/// Phụ mổ II
		/// </summary>
		/// <returns></returns>
		public string EZ { get; set; }

		/// <summary>
		/// Cấp độ vết mổ
		/// </summary>
		/// <returns></returns>
		public string QKDJ { get; set; }

		/// <summary>
		/// Loại lành vết mổ
		/// </summary>
		/// <returns></returns>
		public string QKYHLB { get; set; }

		/// <summary>
		/// Phương pháp gây mê
		/// </summary>
		/// <returns></returns>
		public string MZFS { get; set; }

		/// <summary>
		/// Bác sĩ gây mê
		/// </summary>
		/// <returns></returns>
		public string MZYS { get; set; }

		/// <summary>
		/// zt
		/// </summary>
		/// <returns></returns>
		public string zt { get; set; }
		public string QKYHDJ { get; set; }
		/// <summary>
		/// Tên bác sĩ phẫu thuật chính
		/// </summary>
		public string SZMC { get; set; }
		/// <summary>
		/// Tên phụ mổ I
		/// </summary>
		public string YZMC { get; set; }
		/// <summary>
		/// Tên phụ mổ II
		/// </summary>
		public string EZMC { get; set; }
		/// <summary>
		/// Tên bác sĩ gây mê
		/// </summary>
		public string MZYSMC { get; set; }
		/// <summary>
		/// Mô tả phương pháp gây mê
		/// </summary>
		public string MZFSMS { get; set; }

		/// <summary>
		/// Có phải phẫu thuật cấp cứu không 0:Không,1:Có
		/// </summary>
		public int? SSLX { get; set; }

		/// <summary>
		/// Ngày bắt đầu phẫu thuật
		/// </summary>
		public DateTime? SSKSSJ { get; set; }

		/// <summary>
		/// Ngày kết thúc phẫu thuật
		/// </summary>
		public DateTime? SSJSSJ { get; set; }

		/// <summary>
		/// Số ngày chuẩn bị trước mổ
		/// </summary>
		public decimal? SQZBSJT { get; set; }

		/// <summary>
		/// Thời gian dùng thuốc kháng khuẩn dự phòng trước phẫu thuật
		/// </summary>
		public DateTime? SQYFKJGYS { get; set; }

		/// <summary>
		/// Thời gian bắt đầu gây mê
		/// </summary>
		public DateTime? MZKSSJNYR { get; set; }


		/// <summary>
		/// Cấp độ gây mê ASA
		/// </summary>
		public string MZFJ_ASA { get; set; }

		/// <summary>
		/// Vị trí vết mổ
		/// </summary>
		public string QKBW { get; set; }

		/// <summary>
		/// Nhiễm trùng vết mổ
		/// </summary>
		public string SSQKGR { get; set; }

		/// <summary>
		/// Biến chứng phẫu thuật
		/// </summary>
		public string SSBFZ { get; set; }

		/// <summary>
		/// Tên biến chứng phẫu thuật
		/// </summary>
		public string SSBFZMC { get; set; }

	}
}
