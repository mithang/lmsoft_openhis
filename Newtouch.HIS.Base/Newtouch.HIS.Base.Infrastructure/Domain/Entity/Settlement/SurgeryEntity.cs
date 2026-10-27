using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.Settlement
{
	[SugarTable("xt_ssm")]
	public class SurgeryEntity : EntityBase
	{

		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true)]
		public int id { get; set; }

		/// <summary>
		/// Mã phẫu thuật
		/// </summary>
		public string ssm { get; set; }

		/// <summary>
		/// Tên phẫu thuật
		/// </summary>
		public string ssmc { get; set; }

		/// <summary>
		/// Cấp phẫu thuật
		/// </summary>
		public int ssjb { get; set; }

		/// <summary>
		/// Loại phẫu thuật
		/// </summary>
		public string sslx { get; set; }

		/// <summary>
		/// Tên loại phẫu thuật
		/// </summary>
		public string sslxmc { get; set; }

		/// <summary>
		/// Mã Pinyin
		/// </summary>
		public string pym { get; set; }

		public bool zt { get; set; }

		/// <summary>
		/// Mã phẫu thuật BHYT
		/// </summary>
		public string ssm_yb { get; set; }

		/// <summary>
		/// Tên phẫu thuật BHYT
		/// </summary>
		public string ssmc_yb { get; set; }
	}
}
