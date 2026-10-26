using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
	class ChongQingZyjsDto
	{
	}
	public class CQZyjs05Dto : InpatientSettGAYbFeeRelatedDTO
	{
		/// <summary>
		/// Số giao dịch
		/// </summary> 
		public string jylsh { get; set; }
		/// <summary>
		/// Chi trả từ quỹ chung
		/// </summary> 
		public decimal? cqtczf { get; set; }
		/// <summary>
		/// Thanh toán từ tài khoản
		/// </summary> 
		public decimal? zhzf { get; set; }
		/// <summary>
		/// Trợ cấp công chức
		/// </summary> 
		public decimal? gwybz { get; set; }
		/// <summary>
		/// Thanh toán tiền mặt
		/// </summary> 
		public decimal? cqxjzf { get; set; }
		/// <summary>
		/// Số tiền bồi thường lớn
		/// </summary> 
		public decimal? delpje { get; set; }
		/// <summary>
		/// Hoàn trả mức khởi điểm cho công chức trong lịch sử
		/// </summary> 
		public decimal? lsqfxgwyff { get; set; }
		/// <summary>
		/// Số dư tài khoản
		/// </summary> 
		public decimal? zhye { get; set; }
		/// <summary>
		/// Cơ sở y tế chỉ định ứng trước cho bệnh đơn lẻ
		/// </summary> 
		public decimal? dbzddyljgdz { get; set; }
		/// <summary>
		/// Số tiền cứu trợ dân chính
		/// </summary> 
		public decimal? mzjzje { get; set; }
		/// <summary>
		/// Số dư cứu trợ dân chính ngoại trú
		/// </summary> 
		public decimal? mzjzmzye { get; set; }
		/// <summary>
		/// Số tiền thanh toán hạng mục kháng đa thuốc
		/// </summary> 
		public decimal? ndyxmzfje { get; set; }
		/// <summary>
		/// Số lần thanh toán khám chữa bệnh thông thường
		/// </summary> 
		public decimal? ybzlzfs { get; set; }
		/// <summary>
		/// Số lần chi trả từ quỹ cứu trợ Thần Hoa
		/// </summary> 
		public decimal? shjzjjzfs { get; set; }
		/// <summary>
		/// Lũy kế chi trả từ quỹ chung trong năm
		/// </summary> 
		public decimal? bntczflj { get; set; }
		/// <summary>
		/// Lũy kế chi trả lớn trong năm
		/// </summary> 
		public decimal? bndezflj { get; set; }
		/// <summary>
		/// Lũy kế thanh toán mức khởi điểm cho bệnh đặc biệt
		/// </summary> 
		public decimal? tbqfxzflj { get; set; }
		/// <summary>
		/// Lũy kế hạng mục kháng đa thuốc
		/// </summary> 
		public decimal? ndyxmlj { get; set; }
		/// <summary>
		/// Lũy kế chi trả cứu trợ dân chính nội trú trong năm
		/// </summary> 
		public decimal? bnmzjzzyzflj { get; set; }
		/// <summary>
		/// Thời gian quyết toán trung tâm
		/// </summary> 
		public DateTime zxjssj { get; set; }
		/// <summary>
		/// Số tiền thanh toán mức khởi điểm lần này
		/// </summary> 
		public decimal? bcqfxzfje { get; set; }
		/// <summary>
		/// Chi phí lần này thuộc phạm vi BHYT
		/// </summary> 
		public decimal? bcjrybfwfy { get; set; }
		/// <summary>
		/// Số lần thanh toán dịch vụ dược
		/// </summary> 
		public decimal? ysfwzfs { get; set; }
		/// <summary>
		/// Số tiền bệnh viện bị khấu trừ do vượt chuẩn
		/// </summary> 
		public decimal? yycbkkje { get; set; }
		/// <summary>
		/// Chi từ quỹ sinh đẻ
		/// </summary> 
		public decimal? syjjzf { get; set; }
		/// <summary>
		/// Thanh toán tiền mặt sinh đẻ
		/// </summary> 
		public decimal? syxjzf { get; set; }
		/// <summary>
		/// Chi từ quỹ tai nạn lao động
		/// </summary> 
		public decimal? gsjjzf { get; set; }
		/// <summary>
		/// Thanh toán tiền mặt tai nạn lao động
		/// </summary> 
		public decimal? gsxjzf { get; set; }
		/// <summary>
		/// Cơ sở ứng trước cho bệnh đơn lẻ do tai nạn lao động
		/// </summary> 
		public decimal? gsdbzjgdz { get; set; }
		/// <summary>
		/// Lý do tai nạn lao động tự chi trả toàn bộ
		/// </summary> 
		public string gsqzfyy { get; set; }
		/// <summary>
		/// Trợ cấp khác
		/// </summary> 
		public decimal? qtbz { get; set; }
		/// <summary>
		/// Thanh toán từ tài khoản sinh đẻ
		/// </summary> 
		public decimal? syzhzf { get; set; }
		/// <summary>
		/// Thanh toán từ tài khoản tai nạn lao động
		/// </summary> 
		public decimal? gszhzf { get; set; }
		/// <summary>
		/// Lần quyết toán này có phải là đối tượng giảm nghèo vì sức khỏe hay không
		/// </summary> 
		public string bcjssfjkfpry { get; set; }
		/// <summary>
		/// Quỹ y tế giảm nghèo vì sức khỏe
		/// </summary> 
		public decimal? jkfpyljj { get; set; }
		/// <summary>
		/// Số tiền bảo hiểm giảm nghèo chính xác
		/// </summary> 
		public decimal? jztpbxje { get; set; }
	}
}
