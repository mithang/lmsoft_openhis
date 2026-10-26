using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
	public class zys_ssVO
	{

		/// <summary>
		/// id
		/// </summary>
		public string id { set; get; }
		/// <summary>
		/// Số nhập viện
		/// </summary>
		public string zyh { set; get; }
		/// <summary>
		/// Tên phẫu thuật
		/// </summary>
		public string ssmc { get; set; }
		/// <summary>
		/// Phương pháp gây mê
		/// </summary>
		public string mzfs { get; set; }
		/// <summary>
		/// Bác sĩ phẫu thuật
		/// </summary>
		public string ssys { get; set; }
		/// <summary>
		/// Ngày phẫu thuật
		/// </summary>
		public DateTime? ssrq { get; set; }
	}
}
