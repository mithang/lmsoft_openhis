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
	[Table("cqyb_OutPut04")]
	public class CqybUploadPresList04Entity : IEntity<CqybUploadPresList04Entity>
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
		/// Số ngoại trú/nội trú
		/// </summary>
		public string zymzh { get; set; }
		/// <summary>
		/// Số giao dịch
		/// </summary> 
		public string jylsh { get; set; }
		/// <summary>
		/// Loại tải lên1Khám ngoại trú,2Nhập viện
		/// </summary>
		public string jytype { get; set; }
		/// <summary>
		/// |Đơn giá hạng mục
		/// </summary> 
		public string dj { get; set; }
		/// <summary>
		/// Dấu phê duyệt
		/// </summary> 
		public string spbz { get; set; }
		/// <summary>
		/// Quy tắc phê duyệt
		/// </summary> 
		public string spgz { get; set; }
		/// <summary>
		/// Tổng chi phí hạng mục
		/// </summary> 
		public string xmfyze { get; set; }
		/// <summary>
		/// Cấp hạng mục
		/// </summary> 
		public string xmdj { get; set; }
		/// <summary>
		/// Tỷ lệ tự trả
		/// </summary> 
		public string zfbl { get; set; }
		/// <summary>
		/// Đơn giá tiêu chuẩn
		/// </summary> 
		public string bzdj { get; set; }
		/// <summary>
		/// Số tiền tự trả
		/// </summary> 
		public string zfje { get; set; }
		/// <summary>
		/// Số tiền tự chi trả
		/// </summary> 
		public string zfzje { get; set; }
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
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// Mã nội bộ bệnh viện
        /// </summary>
        public string yynm { get; set; }
    }
}
