using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.V
{
	public class CrkMxAll
	{
		/// <summary>
		/// chi tiết nhập/xuất kho ID
		/// </summary>
		public string crkmxId { get; set; }

		/// <summary>
		/// từ bảng XT_YP_CRKDJK của khóa chính
		/// </summary>
		public string crkId { get; set; }

		/// <summary>
		/// chi tiết phiếu lĩnh ID
		/// </summary>
		public string sldmxId { get; set; }

		/// <summary>
		/// mã thuốc
		/// </summary>
		public string ypdm { get; set; }

		/// <summary>
		/// số hóa đơn
		/// </summary>
		public string fph { get; set; }

		/// <summary>
		/// ngày lập hóa đơn
		/// </summary>
		public DateTime? Kprq { get; set; }

		/// <summary>
		/// ngày nhận hóa đơn
		/// </summary>
		public DateTime? Dprq { get; set; }

		/// <summary>
		/// số lô
		/// </summary>
		public string ph { get; set; }
		
		/// <summary>
		/// hạn sử dụng
		/// </summary>
		public DateTime? yxq { get; set; }

		/// <summary>
		/// giá bán buôn và zhyz và sl tương ứng
		/// </summary>
		public decimal pfj { get; set; }

		/// <summary>
		/// giá bán lẻ và zhyz và sl tương ứng
		/// </summary>
		public decimal lsj { get; set; }

		/// <summary>
		/// giá bán buôn kho thuốc
		/// </summary>
		public decimal ykpfj { get; set; }

		/// <summary>
		/// giá bán lẻ kho thuốc
		/// </summary>
		public decimal yklsj { get; set; }

		/// <summary>
		/// tổng số tiền
		/// </summary>
		public decimal Zje { get; set; }

		/// <summary>
		/// số lượng số lượng đơn vị bộ phận
		/// </summary>
		public int sl { get; set; }

		/// <summary>
		/// nhập kho hệ số chuyển đổi
		/// </summary>
		public int Rkzhyz { get; set; }

		/// <summary>
		/// bộ phận nhập kho tồn kho
		/// </summary>
		public int Rkbmkc { get; set; }

		/// <summary>
		/// nhập kho đơn vị (tên đơn vị)
		/// </summary>
		public string rkdw { get; set; }

		/// <summary>
		/// xuất kho hệ số chuyển đổi
		/// </summary>
		public int Ckzhyz { get; set; }

		/// <summary>
		/// bộ phận xuất kho tồn kho
		/// </summary>
		public int Ckbmkc { get; set; }

		/// <summary>
		/// đơn vị xuất kho (tên đơn vị)
		/// </summary>
		public string ckdw { get; set; }

		/// <summary>
		/// giao diện
		/// </summary>
		public string Wg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? zbbz { get; set; }

		/// <summary>
		/// giấy chứng nhận đăng ký nhập kho
		/// </summary>
		public string jkzcz { get; set; }

		/// <summary>
		/// giấy chứng nhận đạt chuẩn
		/// </summary>
		public string hgzm { get; set; }

		/// <summary>
		/// nghiệm thu kết quả
		/// </summary>
		public string ysjg { get; set; }

		/// <summary>
		/// lý do trả hàng
		/// </summary>
		public string thyy { get; set; }

		/// <summary>
		/// xử lý kết quả
		/// </summary>
		public string Cljg { get; set; }

		/// <summary>
		/// ngày sản xuất
		/// </summary>
		public DateTime? scrq { get; set; }

		/// <summary>
		/// tỷ lệ chiết khấu
		/// </summary>
		public decimal? kl { get; set; }

		/// <summary>
		/// giá nhập giá nhập theo đơn vị kho thuốc
		/// </summary>
		public decimal? jj { get; set; }

		/// <summary>
		/// nơi sản xuất
		/// </summary>
		public int? cd { get; set; }

		/// <summary>
		/// lô
		/// </summary>
		public string pc { get; set; }

		/// <summary>
		/// trạng thái
		/// </summary>
		public string zt { get; set; }

		/// <summary>
		/// sắp xếp
		/// </summary>
		public int? px { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreatorCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastModifyTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastModifierCode { get; set; }
		/// <summary>
		/// tổng tiền giá nhập giá nhập theo đơn vị kho thuốc
		/// </summary>
		public decimal? pfjze { get; set; }
		

		/// <summary>
		/// tên thuốc
		/// </summary>
		public string ypmc { get; set; }

		/// <summary>
		/// tên nhóm lớn
		/// </summary>
		public string dlmc { get; set; }

		/// <summary>
		/// quy cách thuốc
		/// </summary>
		public string ypgg { get; set; }

		/// <summary>
		/// nhà sản xuất
		/// </summary>
		public string ycmc { get; set; }
	}
}
