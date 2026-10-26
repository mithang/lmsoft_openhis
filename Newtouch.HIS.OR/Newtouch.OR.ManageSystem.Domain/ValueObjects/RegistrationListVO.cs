using Newtouch.OR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    public class RegistrationListVO
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số đăng ký
        /// </summary>
        /// <returns></returns>
        public string Id { get; set; }
        /// <summary>
        /// Số sắp xếp
        /// </summary>
        /// <returns></returns>
        public string arrangeId { get; set; }
        /// <summary>
        /// Số đơn đăng ký
        /// </summary>
        /// <returns></returns>
        public string applyId { get; set; }
        /// <summary>
        /// Mã đăng ký
        /// </summary>
        public string applyno { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        /// <returns></returns>
        public string xm { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        /// <returns></returns>
        public string xb { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        /// <returns></returns>
        public string nl { get; set; }
        /// <summary>
        /// Số đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssxh { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// Khu bệnh
        /// </summary>
        /// <returns></returns>
        public string bq { get; set; }
        /// <summary>
        /// Số giường
        /// </summary>
        /// <returns></returns>
        public string ch { get; set; }
        /// <summary>
        /// Chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string zd { get; set; }
        /// <summary>
        /// Chẩn đoán khi nhập viện
        /// </summary>
        /// <returns></returns>
        public string ryzd { get; set; }
        /// <summary>
        /// Tên chẩn đoán khi nhập viện
        /// </summary>
        /// <returns></returns>
        public string ryzdmc { get; set; }
        /// <summary>
        /// Chẩn đoán sau phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string sszd { get; set; }
        /// <summary>
        /// Tên chẩn đoán sau phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string sszdmc { get; set; }
        /// <summary>
        /// Tình trạng bệnh
        /// </summary>
        /// <returns></returns>
        public string shbq { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }
        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssjb { get; set; }
        /// <summary>
        /// Tên phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssmc { get; set; }
        /// <summary>
        /// Mã phẫu thuật
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// Thời gian đăng ký phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? sssqsj { get; set; }
        /// <summary>
        /// Thời gian sắp xếp phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? ssapsj { get; set; }
        /// <summary>
        /// Thời gian bắt đầu phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? sskssj { get; set; }
        /// <summary>
        /// Thời gian kết thúc phẫu thuật
        /// </summary>
        /// <returns></returns>
        public DateTime? ssjssj { get; set; }
        /// <summary>
        /// Phòng mổ
        /// </summary>
        /// <returns></returns>
        public string oproom { get; set; }
        /// <summary>
        /// Lượt mổ
        /// </summary>
        /// <returns></returns>
        public string oporder { get; set; }
        /// <summary>
        /// Phương pháp gây mê
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }
        /// <summary>
        /// Vị trí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public string ssbw { get; set; }
        /// <summary>
        /// Cấp độ vết mổ
        /// </summary>
        /// <returns></returns>
        public string qkdj { get; set; }
        /// <summary>
        /// Có cách ly không 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string isgl { get; set; }
        /// <summary>
        /// Có nhiễm khuẩn không 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string isjun { get; set; }
        /// <summary>
        /// Lượng máu truyền
        /// </summary>
        /// <returns></returns>
        public decimal? shuxl { get; set; }
        /// <summary>
        /// Lượng máu mất
        /// </summary>
        /// <returns></returns>
        public decimal? shixl { get; set; }
        /// <summary>
        /// Tổng lượng vào
        /// </summary>
        /// <returns></returns>
        public decimal? zrxl { get; set; }
        /// <summary>
        /// Tổng lượng ra
        /// </summary>
        /// <returns></returns>
        public decimal? zcxl { get; set; }
        /// <summary>
        /// Bác sĩ phẫu thuật chính
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// Bác sĩ phụ tá1
        /// </summary>
        /// <returns></returns>
        public string zlys1 { get; set; }
        /// <summary>
        /// Bác sĩ phụ tá2
        /// </summary>
        /// <returns></returns>
        public string zlys2 { get; set; }
        /// <summary>
        /// Y tá lưu động
        /// </summary>
        /// <returns></returns>
        public string xhhs { get; set; }
        /// <summary>
        /// Y tá rửa tay
        /// </summary>
        /// <returns></returns>
        public string xshs { get; set; }
        /// <summary>
        /// Bác sĩ gây mê
        /// </summary>
        /// <returns></returns>
        public string mzys { get; set; }
		/// <summary>
		/// Tên bác sĩ
		/// </summary>
		public string ysxm { get; set; }
		/// <summary>
		/// Bác sĩ phụ tá1Tên
		/// </summary>
		public string zlys1name { get; set; }
		/// <summary>
		/// Bác sĩ phụ tá2Tên
		/// </summary>
		public string zlys2name { get; set; }
		/// <summary>
		/// Tên y tá lưu động
		/// </summary>
		public string xhhsname { get; set; }
		/// <summary>
		/// Tên y tá rửa tay
		/// </summary>
		public string xshsname { get; set; }
		/// <summary>
		/// Tên bác sĩ gây mê
		/// </summary>
		public string mzysname { get; set; }

		/// <summary>
		/// Phẫu thuật
		/// </summary>
		public List<ORApplyInfoExpandEntity> ss { get; set; }
	}
}
