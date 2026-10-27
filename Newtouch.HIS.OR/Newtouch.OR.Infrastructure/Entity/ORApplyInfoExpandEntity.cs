using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
	[SugarTable("OR_ApplyInfo_Expand")]
	public class ORApplyInfoExpandEntity : IEntityBase
	{
		/// <summary>
		/// OrganizeId
		/// </summary>
		/// <returns></returns>
		public string OrganizeId { get; set; }

		/// <summary>
		/// Khóa chính
		/// </summary>
		/// <returns></returns>
		[SugarColumn(IsPrimaryKey = true)]
		public string Id { get; set; }

		/// <summary>
		/// Mã đơn đăng ký
		/// </summary>
		/// <returns></returns>
		public string Applyno { get; set; }

		/// <summary>
		/// Số nhập viện
		/// </summary>
		/// <returns></returns>
		public string zyh { get; set; }

		/// <summary>
		/// Tên phẫu thuật
		/// </summary>
		public string ssmc { get; set; }

		/// <summary>
		/// Mã phẫu thuật
		/// </summary>
		public string ssdm { get; set; }

		/// <summary>
		/// Số thứ tự
		/// </summary>
		public int px { get; set; }

		/// <summary>
		/// Trạng thái
		/// </summary>
		public string zt { get; set; }

		/// <summary>
		/// Thời gian tạo
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// Người tạo
		/// </summary>
		public string CreatorCode { get; set; }

		/// <summary>
		/// Thời gian sửa
		/// </summary>
		public DateTime? LastModifyTime { get; set; }
		
		/// <summary>
		/// Người sửa
		/// </summary>
		public string LastModifierCode { get; set; }
	}
}
