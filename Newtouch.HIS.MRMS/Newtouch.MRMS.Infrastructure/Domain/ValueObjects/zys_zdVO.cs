using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
	public class zys_zdVO
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
		/// 诊断类型
		/// </summary>
		public int zdType { set; get; }
		/// <summary>
		/// 诊断描述
		/// </summary>
		public string zdms { get; set; }
		/// <summary>
		/// 治疗结果
		/// </summary>
		public string zljg { get; set; }
		/// <summary>
		/// 治疗天数
		/// </summary>
		public string zlts { get; set; }
		/// <summary>
		/// 诊断日期
		/// </summary>
		public DateTime? zdrq { get; set; }

	}
}