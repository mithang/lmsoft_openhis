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
	[SugarTable("Sys_ShortcutMenu", "系统快捷菜单")]
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
		[StringLength(10, ErrorMessage = "AppId长度限制为10")]
		public string AppId { get; set; }

		/// <summary>
		/// Desc:tên menu phím tắt
		/// Default:
		/// Nullable:False
		/// </summary>           
		[Required(ErrorMessage = "MenuName不可为空")]
		[StringLength(50, ErrorMessage = "MenuName长度限制为30")]
		public string MenuName { get; set; }

		/// <summary>
		/// Desc:địa chỉ menuurl
		/// Default:
		/// Nullable:False
		/// </summary>           
		[Required(ErrorMessage = "MenuUrl不可为空")]
		[StringLength(50, ErrorMessage = "MenuUrl长度限制为200")]
		public string MenuUrl { get; set; }

		/// <summary>
		/// Desc:tổ chức
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "OrganizeIdkhông được để trống")]
		//[StringLength(50, ErrorMessage = "OrganizeIdgiới hạn độ dài là50")]
		//public string OrganizeId { get; set; }
		///// <summary>
		/// Desc:người tạo
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "CreatorCodekhông được để trống")]
		//[StringLength(20, ErrorMessage = "CreatorCodegiới hạn độ dài là20")]
		//public string CreatorCode { get; set; }

		/// <summary>
		/// Desc:thời gian tạo
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "Createtimekhông được để trống")]
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
		//[StringLength(20, ErrorMessage = "LastModifierCodegiới hạn độ dài là500")]
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
		//[StringLength(1, ErrorMessage = "ztgiới hạn độ dài là1")]
		//public string zt { get; set; }
	}
}
