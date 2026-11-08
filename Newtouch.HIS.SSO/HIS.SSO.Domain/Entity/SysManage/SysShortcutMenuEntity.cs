using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.SSO.Domain.Entity.SysManage
{
	///<summary>
	///mô-đun hệ thống
	///</summary>
	[Tenant(DBEnum.UnionDb)]
	[SugarTable("Sys_ShortcutMenu", "Menu phím tắt hệ thống")]
	public partial class SysShortcutMenuEntity : IEntity
	{
		/// <summary>
		/// Desc:khóa chính của mô-đun
		/// Default:
		/// Nullable:False
		/// </summary>           
		[SugarColumn(IsPrimaryKey = true)]
		public string Id { get; set; }
		/// <summary>
		/// Desc: hệ thống tương ứng
		/// Default:
		/// Nullable:False
		/// </summary>           
		[StringLength(10, ErrorMessage = "AppId giới hạn độ dài 10")]
		public string AppId { get; set; }

		/// <summary>
		/// Desc:tên menu phím tắt
		/// Default:
		/// Nullable:False
		/// </summary>           
		[Required(ErrorMessage = "MenuName không được để trống")]
		[StringLength(50, ErrorMessage = "MenuName giới hạn độ dài 30")]
		public string MenuName { get; set; }

		/// <summary>
		/// Desc:địa chỉ menuurl
		/// Default:
		/// Nullable:False
		/// </summary>           
		[Required(ErrorMessage = "MenuUrl không được để trống")]
		[StringLength(50, ErrorMessage = "MenuUrl giới hạn độ dài 200")]
		public string MenuUrl { get; set; }

		/// <summary>
		/// Desc:tổ chức
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "OrganizeId không được để trống")]
		//[StringLength(50, ErrorMessage = "OrganizeId giới hạn độ dài là50")]
		//public string OrganizeId { get; set; }
		///// <summary>
		/// Desc:người tạo
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "CreatorCode không được để trống")]
		//[StringLength(20, ErrorMessage = "CreatorCode giới hạn độ dài là20")]
		//public string CreatorCode { get; set; }

		/// <summary>
		/// Desc:thời gian tạo
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "Createtime không được để trống")]
		//public DateTime Createtime { get; set; }

		/// <summary>
		/// Desc:thời gian sửa đổi
		/// Default:
		/// Nullable:True
		/// </summary>           
		//public DateTime? LastModifyTime { get; set; }

		/// <summary>
		/// Desc:người sửa đổi
		/// Default:
		/// Nullable:True
		/// </summary>           
		//[StringLength(20, ErrorMessage = "LastModifierCode giới hạn độ dài là500")]
		//public string? LastModifierCode { get; set; }

		/// <summary>
		/// Desc:sắp xếp
		/// Default:
		/// Nullable:True
		/// </summary>           
		public int? px { get; set; }

		/// <summary>
		/// Desc:trạng thái
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[StringLength(1, ErrorMessage = "zt giới hạn độ dài là1")]
		//public string zt { get; set; }
	}
}
