using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2020-03-13 11:29
    /// Mô tả: Trang đầu hồ sơ bệnh án - Ghi chép phẫu thuật
    /// </summary>
    [Table("mr_basy_ss")]
    public class MrbasyssEntity : IEntity<MrbasyssEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// SSOrder
        /// </summary>
        /// <returns></returns>
        public int SSOrder { get; set; }

        /// <summary>
        /// Mã phẫu thuật và thủ thuật
        /// </summary>
        /// <returns></returns>
        public string SSJCZBM { get; set; }

        /// <summary>
        /// Ngày phẫu thuật và thủ thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? SSJCZRQ { get; set; }

        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string SSJB { get; set; }

        /// <summary>
        /// Tên phẫu thuật và thủ thuật
        /// </summary>
        /// <returns></returns>
        public string SSJCZMC { get; set; }

        /// <summary>
        /// Phẫu thuật viên
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// Phụ mổ I
        /// </summary>
        /// <returns></returns>
        public string YZ { get; set; }

        /// <summary>
        /// Phụ mổ II
        /// </summary>
        /// <returns></returns>
        public string EZ { get; set; }

        /// <summary>
        /// Cấp độ vết mổ
        /// </summary>
        /// <returns></returns>
        public string QKDJ { get; set; }

        /// <summary>
        /// Loại liền vết mổ
        /// </summary>
        /// <returns></returns>
        public string QKYHLB { get; set; }

        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string MZFS { get; set; }

        /// <summary>
        /// Bác sĩ gây mê
        /// </summary>
        /// <returns></returns>
        public string MZYS { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// QKYHDJ
        /// </summary>
        /// <returns></returns>
        public string QKYHDJ { get; set; }


		/// <summary>
		/// Có phải phẫu thuật cấp cứu hay không: 0 Không, 1 Có
		/// </summary>
		public int? SSLX { get; set; }

		/// <summary>
		/// Ngày bắt đầu phẫu thuật
		/// </summary>
		public DateTime? SSKSSJ { get; set; }

		/// <summary>
		/// Ngày kết thúc phẫu thuật
		/// </summary>
		public DateTime? SSJSSJ { get; set; }

		/// <summary>
		/// Số ngày chuẩn bị trước mổ
		/// </summary>
		public decimal? SQZBSJT { get; set; }

		/// <summary>
		/// Thời gian dùng thuốc kháng khuẩn dự phòng trước mổ
		/// </summary>
		public DateTime? SQYFKJGYS { get; set; }

		/// <summary>
		/// Thời gian bắt đầu gây mê
		/// </summary>
		public DateTime? MZKSSJNYR { get; set; }


		/// <summary>
		/// Phân cấp gây mê ASA
		/// </summary>
		public string MZFJ_ASA { get; set; }

		/// <summary>
		/// Vị trí vết mổ
		/// </summary>
		public string QKBW { get; set; }

		/// <summary>
		/// Nhiễm trùng vết mổ
		/// </summary>
		public string SSQKGR { get; set; }

		/// <summary>
		/// Biến chứng phẫu thuật
		/// </summary>
		public string SSBFZ { get; set; }

		/// <summary>
		/// Tên biến chứng phẫu thuật
		/// </summary>
		public string SSBFZMC { get; set; }

	}
}