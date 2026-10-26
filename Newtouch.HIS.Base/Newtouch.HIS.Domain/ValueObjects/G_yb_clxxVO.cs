using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	/// <summary>
	/// Bảng danh mục vật liệu bảo hiểm y tế
	/// </summary>
	public class G_yb_clxxVO
	{
		/// <summary>
		/// Mã vật tư tiêu hao
		/// </summary>
		public string hcdm { get; set; }
		/// <summary>
		/// Phân loại cấp một
		/// </summary>
		public string yjfl { get; set; }
		/// <summary>
		/// Phân loại cấp hai
		/// </summary>
		public string ejfl { get; set; }
		/// <summary>
		/// Phân loại cấp ba
		/// </summary>
		public string sjfl { get; set; }
		/// <summary>
		/// Tên chung bảo hiểm y tế
		/// </summary>
		public string ybtym { get; set; }
		/// <summary>
		/// Chất liệu
		/// </summary>
		public string cz { get; set; }
		/// <summary>
		/// Đặc điểm
		/// </summary>
		public string tz { get; set; }
		/// <summary>
		/// Số đăng ký lưu hồ sơ
		/// </summary>
		public string zcbah { get; set; }
		/// <summary>
		/// Tên sản phẩm đơn chiếc
		/// </summary>
		public string djcpmc { get; set; }
		/// <summary>
		/// Doanh nghiệp vật tư tiêu hao
		/// </summary>
		public string hcqy { get; set; }
		/// <summary>
		/// Số lượng model quy cách
		/// </summary>
		public double ggxhs { get; set; }
		/// <summary>
		/// Người đăng ký lưu hồ sơ
		/// </summary>
		public string zcbar { get; set; }
		/// <summary>
		/// Mã pinyin
		/// </summary>
		public string pym { get; set; }
		/// <summary>
		/// Tính chất bảo hiểm y tế
		/// </summary>
		public string ybxz { get; set; }

	}
}
