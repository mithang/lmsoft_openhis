using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	public class G_yb_xmxxVO
	{
		/// <summary>
		/// Mã truyền tải thanh toán
		/// </summary>
		public string jscsbm { get; set; }
		/// <summary>
		/// Mã hạng mục quốc gia
		/// </summary>
		public string gjxmdm { get; set; }
		/// <summary>
		/// Tên hạng mục quốc gia
		/// </summary>
		public string gjxmmc { get; set; }
		/// <summary>
		/// Mã
		/// </summary>
		public string bm { get; set; }
		/// <summary>
		/// Tên hạng mục
		/// </summary>
		public string xmmc { get; set; }
		/// <summary>
		/// Nội hàm hạng mục
		/// </summary>
		public string xmnh { get; set; }
		/// <summary>
		/// Nội dung ngoại trừ
		/// </summary>
		public string cwnr { get; set; }
		/// <summary>
		/// Đơn vị tính giá
		/// </summary>
		public string jjdw { get; set; }
		/// <summary>
		/// Giải thích
		/// </summary>
		public string sm { get; set; }
		/// <summary>
		/// Tính chất bảo hiểm y tế
		/// </summary>
		public string ybxz { get; set; }
		/// <summary>
		/// Mã pinyin
		/// </summary>
		public string pym { get; set; }
	}
}
