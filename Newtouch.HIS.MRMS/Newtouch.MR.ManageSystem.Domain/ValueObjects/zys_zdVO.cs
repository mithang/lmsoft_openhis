using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
	public class zys_zdVO
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
		/// Loại chẩn đoán
		/// </summary>
		public int zdType { set; get; }
		/// <summary>
		/// Mô tả chẩn đoán
		/// </summary>
		public string zdms { get; set; }
		/// <summary>
		/// Kết quả điều trị
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