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
		/// Số nội trú
		/// </summary>
		public string zyh { set; get; }
		/// <summary>
		/// Loại chẩn đoán
		/// </summary>
		public int zdType { set; get; }
		/// <summary>
		/// Mô tả chẩn đoán
		/// </summary>
		public string zdms { get; set; }
		/// <summary>
		/// Điều trịKết quả
		/// </summary>
		public string zljg { get; set; }
		/// <summary>
		/// Số ngày điều trị
		/// </summary>
		public string zlts { get; set; }
		/// <summary>
		/// Ngày chẩn đoán
		/// </summary>
		public DateTime? zdrq { get; set; }

	}
}