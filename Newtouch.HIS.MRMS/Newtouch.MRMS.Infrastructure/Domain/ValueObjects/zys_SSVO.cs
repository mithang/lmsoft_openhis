using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
	public class zys_ssVO
	{

		/// <summary>
		/// id
		/// </summary>
		public string id { set; get; }
		/// <summary>
		/// 住院号
		/// </summary>
		public string zyh { set; get; }
		/// <summary>
		/// 手术名称
		/// </summary>
		public string ssmc { get; set; }
		/// <summary>
		/// 麻醉方法
		/// </summary>
		public string mzfs { get; set; }
		/// <summary>
		/// 手术医师
		/// </summary>
		public string ssys { get; set; }
		/// <summary>
		/// 手术日期
		/// </summary>
		public DateTime? ssrq { get; set; }
	}
}
