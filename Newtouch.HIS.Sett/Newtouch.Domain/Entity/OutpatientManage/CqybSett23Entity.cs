using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
	/// <summary>
	/// Khấu trừ tài khoản BHYT Trùng Khánh và ghi nhận
	/// </summary>
	[Table("cqyb_OutPut23")]
	public class CqybSett23Entity : IEntity<CqybSett23Entity>
	{
		/// <summary>
		/// Khóa chínhID
		/// </summary>
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Mã tổ chức
		/// </summary>
		public string OrganizeId { get; set; }
		/// <summary>
		/// Mã nội bộ quyết toán
		/// </summary>
		public int jsnm { get; set; }
		/// <summary>
		/// Số ngoại trú/nội trú
		/// </summary> 
		public string zymzh { get; set; }
        /// <summary>
        /// Số tiền khấu trừ lần này
        /// </summary>
        public decimal? bcdyje { get; set; }
        /// <summary>
        /// Tên người khấu trừ
        /// </summary>
        public string dyrxm { get; set; }
        /// <summary>
        /// Số CMND người khấu trừ
        /// </summary>
        public string dyrsfzh { get; set; }
        /// <summary>
        /// Số dư tài khoản người khấu trừ
        /// </summary>
        public decimal? dyrzhye { get; set; }
        /// <summary>
        /// Số giao dịch khấu trừ
        /// </summary>
        public string dyjylsh { get; set; }
        /// <summary>
        /// Tổng số tiền đã khấu trừ
        /// </summary>
        public decimal? ydyzje { get; set; }
        /// <summary>
        /// Loại quyết toán 0Đăng ký khám 1Khám ngoại trú 2 Nhập viện
        /// </summary>
        public string jslx { get; set; }
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
