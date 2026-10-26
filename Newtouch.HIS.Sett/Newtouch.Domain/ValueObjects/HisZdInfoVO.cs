using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
	public class HisZdInfoVO
	{
	}

	public class HisKsZdVO
	{
		public string code { get; set; }
		public string name { get; set; }
		public string py { get; set; }
		public string mzzybz { get; set; }
		public string zxks { get; set; }
		public string ybksbm { get; set; }
		public string bmdm { get; set; }
	}

	public class HisZdinfoVO
	{
		public string code { get; set; }
		public string name { get; set; }
		public string py { get; set; }
	}

	public class Inparameter
	{
		public DateTime? ksrq { get; set; }
		public DateTime? jsrq { get; set; }
		/// <summary>
		/// Điều kiện lọc đầu tiên
		/// </summary>
		public string keywordo { get; set; }
		/// <summary>
		/// Điều kiện lọc thứ hai
		/// </summary>
		public string keywordt { get; set; }
	}

}
