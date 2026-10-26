using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	/// <summary>
	/// Bảng danh mục thuốc bảo hiểm y tế
	/// </summary>
	public class G_yb_ypxxVO
	{
		/// <summary>
		/// Mã thuốc
		/// </summary>
		public string ypdm { get; set; }
		/// <summary>
		/// Nguồn dữ liệu
		/// </summary>
		public string sjly { get; set; }
		/// <summary>
		/// Tên đăng ký
		/// </summary>
		public string zcmc { get; set; }
		/// <summary>
		/// Tên thương phẩm
		/// </summary>
		public string spmc { get; set; }
		/// <summary>
		/// Dạng bào chế đăng ký
		/// </summary>
		public string zcjx { get; set; }
		/// <summary>
		/// Dạng bào chế thực tế
		/// </summary>
		public string sjjx { get; set; }
		/// <summary>
		/// Quy cách đăng ký
		/// </summary>
		public string zcgg { get; set; }
		/// <summary>
		/// Quy cách thực tế
		/// </summary>
		public string sjgg { get; set; }
		/// <summary>
		/// Chất liệu bao bì
		/// </summary>
		public string bzcz { get; set; }
		/// <summary>
		/// Số lượng đóng gói nhỏ nhất
		/// </summary>
		public string zxbzsl { get; set; }
		/// <summary>
		/// Đơn vị bào chế nhỏ nhất
		/// </summary>
		public string zxzjdw { get; set; }
		/// <summary>
		/// Đơn vị đóng gói nhỏ nhất
		/// </summary>
		public string zxbzdw { get; set; }
		/// <summary>
		/// Doanh nghiệp thuốc
		/// </summary>
		public string ypqy { get; set; }
		/// <summary>
		/// Người sở hữu thuốc đã lưu hành
		/// </summary>
		public string shypcyr { get; set; }
		/// <summary>
		/// Số văn bản phê duyệt
		/// </summary>
		public string pzwh { get; set; }
		/// <summary>
		/// Số văn bản phê duyệt gốc
		/// </summary>
		public string ypzwh { get; set; }
		/// <summary>
		/// Mã chuẩn thuốc
		/// </summary>
		public string ypbwm { get; set; }
		/// <summary>
		/// Tên doanh nghiệp đóng gói chia nhỏ
		/// </summary>
		public string fbzqymc { get; set; }
		/// <summary>
		/// Doanh nghiệp sản xuất
		/// </summary>
		public string scqy { get; set; }
		/// <summary>
		/// Trạng thái thị trường
		/// </summary>
		public string sczt { get; set; }
		/// <summary>
		/// Tên thuốc bảo hiểm y tế
		/// </summary>
		public string ybypmc { get; set; }
		/// <summary>
		/// Loại A/B bản 2021
		/// </summary>
		public string ybjyl { get; set; }
		/// <summary>
		/// Dạng bào chế bảo hiểm y tế
		/// </summary>
		public string ybjx { get; set; }
		/// <summary>
		/// Số hiệu
		/// </summary>
		public string bh { get; set; }
		/// <summary>
		/// Ghi chú
		/// </summary>
		public string bz { get; set; }
		/// <summary>
		/// Loại A/B
		/// </summary>
		public string jyl { get; set; }
		/// <summary>
		/// Ghi chú 1
		/// </summary>
		public string bz1 { get; set; }
		/// <summary>
		/// Ghi chú (dấu hiệu thuộc tính đặc biệt)
		/// </summary>
		public string bztssxbs { get; set; }
		/// <summary>
		/// Tiêu chuẩn thanh toán bảo hiểm y tế
		/// </summary>
		public string ybzfbz { get; set; }

		/// <summary>
		/// Mã pinyin
		/// </summary>
		public string pym { get; set; }

	}

	public class ypxxlist
	{
		public string ypid { get; set; }
	}
}
