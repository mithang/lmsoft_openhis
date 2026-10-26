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
	[Table("cqyb_OutPutInPar04")]
	public class CqybUploadInPres04Entity : IEntity<CqybUploadInPres04Entity>
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
		/// Số đơn thuốc
		/// </summary>
		public string cfh { get; set; }
        /// <summary>
        /// Số lô
        /// </summary>
        public string pch { get; set; }
		/// <summary>
		/// Loại tải lên1Khám ngoại trú,2Nhập viện
		/// </summary>
		public string jytype { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
		/// Cờ phê duyệt 0Chưa phê duyệt 1Phê duyệt
		/// </summary>
		public string spbz { get; set; }
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
        /// Mã danh mục quốc gia
        /// </summary>
        public string gjmldm { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia
        /// </summary>
        public string gjysbm { get; set; }
        /// <summary>
        /// Mã bác sĩ(Số giấy tờ)
        /// </summary>
        public string ysbm { get; set; }
    }
}
