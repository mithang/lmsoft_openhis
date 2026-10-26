using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.PatientManage
{
	/// <summary>
	/// BHYT Trùng Khánh_Đăng ký khám bệnh02Trả về và ghi nhận
	/// </summary>
	[Table("cqyb_OutPut02")]
	public class CqybMedicalReg02Entity : IEntity<CqybMedicalReg02Entity>
	{
		/// <summary>
		/// Khóa chínhID
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// Bệnh nhânid
		/// </summary>
		public string patid { get; set; }
		/// <summary>
		/// Số ngoại trú/nội trú
		/// </summary>
		public string zymzh { get; set; }
		/// <summary>
		/// Số giao dịch
		/// </summary>
		public string jylsh { get; set; }
		/// <summary>
		/// Số dư tài khoản
		/// </summary>
		public string zhye { get; set; }
		/// <summary>
		/// Tên bệnh viện chuyển đi ban đầu
		/// </summary>
		public string yzcyymc { get; set; }
		/// <summary>
		/// Loại tham gia bảo hiểm
		/// </summary>
		public string cblx { get; set; }
		/// <summary>
		/// Loại đăng ký1Khám ngoại trú,2Nhập viện
		/// </summary>
		public string jytype { get; set; }
		/// <summary>
		/// Người tạoID
		/// </summary>
		public string CreatorCode { get; set; }
		/// <summary>
		/// Thời gian tạo
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// Thời gian sửa đổi lần cuối
		/// </summary>
		public DateTime? LastModifyTime { get; set; }
		/// <summary>
		/// Người sửa đổi lần cuốiID
		/// </summary>
		public string LastModifierCode { get; set; }
		/// <summary>
		/// Sắp xếp
		/// </summary>
		public int px { get; set; }
		/// <summary>
		/// Trạng thái
		/// </summary>
		public string zt { get; set; }

	}
}
