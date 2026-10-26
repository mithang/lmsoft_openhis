using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO.OutputDto
{
	/// <summary>
	/// Từ điển phẫu thuật hệ thống
	/// </summary>
	public class SysOpListDto
	{
		/// <summary>
		/// Mã phẫu thuật
		/// </summary>
		public string ssdm { get; set; }
		/// <summary>
		/// Tên phẫu thuật
		/// </summary>
		public string ssmc { get; set; }
		/// <summary>
		/// Mã ghi nhớ phẫu thuật
		/// </summary>
		public string zjm { get; set; }
		/// <summary>
		/// Cấp độ phẫu thuật
		/// </summary>
		public string ssjb { get; set; }
	}
}
