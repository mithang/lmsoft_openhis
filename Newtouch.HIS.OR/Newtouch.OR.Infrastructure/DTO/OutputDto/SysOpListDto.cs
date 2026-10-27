using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.DTO.OutputDto
{
	/// <summary>
	/// Hệ thốngTừ điển phẫu thuật
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
		/// Phẫu thuậtMã gợi nhớ
		/// </summary>
		public string zjm { get; set; }
		/// <summary>
		/// Cấp phẫu thuật
		/// </summary>
		public string ssjb { get; set; }
	}
}
