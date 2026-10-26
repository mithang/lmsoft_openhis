using Newtouch.HIS.Domain.Entity.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
	class ChongQingMzjsDto
	{
	}
	/// <summary>
	/// Thêm chi tiết đơn thuốc(Mã loại giao dịch:04)Tham số đầu vào
	/// </summary>
	public class UploadPrescriptionsInPut
	{
		/// <summary>
		/// Số ngoại trú/nội trú
		/// </summary> 
		public string zymzh { get; set; }
		/// <summary>
		/// Danh sách chi tiết đơn thuốc
		/// </summary> 
		public IList<UploadPrescriptionsListInPut> cflist { get; set; }

	}
	public class UploadPrescriptionsListInPut
	{
		/// <summary>
		/// Số đơn thuốc
		/// </summary> 
		public string cfh { get; set; }
		/// <summary>
		/// Ngày kê đơn
		/// </summary> 
		public string kfrq { get; set; }
		/// <summary>
		/// Số giao dịch BHYT hạng mục
		/// </summary> 
		public string xmyblsh { get; set; }
		/// <summary>
		/// Mã nội bộ bệnh viện
		/// </summary> 
		public string yynm { get; set; }
		/// <summary>
		/// Tên hạng mục
		/// </summary> 
		public string xmmc { get; set; }
		/// <summary>
		/// Đơn giá
		/// </summary> 
		public decimal dj { get; set; }
		/// <summary>
		/// Số lượng
		/// </summary> 
		public decimal? sl { get; set; }
		/// <summary>
		/// Cờ cấp cứu
		/// </summary> 
		public string jzbz { get; set; }
		/// <summary>
		/// Tên bác sĩ đơn thuốc
		/// </summary> 
		public string cfssmc { get; set; }
		/// <summary>
		/// Người xử lý
		/// </summary> 
		public string jbr { get; set; }
		/// <summary>
		/// Đơn vị
		/// </summary> 
		public string dw { get; set; }
		/// <summary>
		/// Quy cách
		/// </summary> 
		public string gg { get; set; }
		/// <summary>
		/// Dạng bào chế
		/// </summary> 
		public string jx { get; set; }
		/// <summary>
		/// Số giao dịch chi tiết đã xóa
		/// </summary> 
		public string cxmxlsh { get; set; }
		/// <summary>
		/// Số tiền
		/// </summary> 
		public decimal? je { get; set; }
		/// <summary>
		/// Mã khoa
		/// </summary> 
		public string ksbm { get; set; }
		/// <summary>
		/// Tên khoa
		/// </summary> 
		public string ksmc { get; set; }
		/// <summary>
		/// Mã bác sĩ
		/// </summary> 
		public string ysbm { get; set; }
		/// <summary>
		/// Liều lượng mỗi lần
		/// </summary> 
		public decimal? mcyl { get; set; }
		/// <summary>
		/// Tiêu chuẩn cách dùng
		/// </summary> 
		public string yfbz { get; set; }
		/// <summary>
		/// Chu kỳ thực hiện
		/// </summary> 
		public string zxzq { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary> 
		public string xzlb { get; set; }
		/// <summary>
		/// Dấu chuyển sang tự chi trả
		/// </summary> 
		public string zzfbz { get; set; }
		/// <summary>
		/// Liều dùng thuốc mỗi lần
		/// </summary> 
		public decimal? dcyyjl { get; set; }
		/// <summary>
		/// Đơn vị liều dùng thuốc mỗi lần
		/// </summary> 
		public string dcyyjldw { get; set; }
		/// <summary>
		/// Liều lượng mỗi lần
		/// </summary> 
		public decimal? dcyl { get; set; }
		/// <summary>
		/// Đơn vị đo nhỏ nhất
		/// </summary> 
		public string zxjldw { get; set; }
		/// <summary>
		/// Tổng lượng thuốc lấy
		/// </summary> 
		public decimal?  qyzl { get; set; }
		/// <summary>
		/// Đường dùng thuốc
		/// </summary> 
		public string yytj { get; set; }
		/// <summary>
		/// Tần suất sử dụng
		/// </summary> 
		public string sypc { get; set; }
		/// <summary>
		/// Kiểm tra ID
		/// </summary> 
		public string shid { get; set; }
		/// <summary>
		/// Số ngày dùng thuốc
		/// </summary> 
		public string yyts { get; set; }
		/// <summary>
		/// Liệu trình dùng thuốc
		/// </summary> 
		public string yylc { get; set; }
        /// <summary>
        /// Mã danh mục quốc gia
        /// </summary>
        public string gjmldm { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia
        /// </summary>
        public string gjysbm { get; set; }
	}
    public class PreSettInPut
	{
		/// <summary>
		/// Nhập viện(Khám ngoại trú)Số
		/// </summary> 
		public string zymzh { get; set; }
		/// <summary>
		/// Ngày hết hạn
		/// </summary> 
		public string jzrq { get; set; }
		/// <summary>
		/// Ngày giường nội trú
		/// </summary> 
		public string zycr { get; set; }
		/// <summary>
		/// Tổng số tiền quyết toán lần này
		/// </summary> 
		public string bcjszje { get; set; }
		/// <summary>
		/// Cờ thanh toán số dư tài khoản
		/// </summary> 
		public string zhyezfbz { get; set; }
		/// <summary>
		/// Tổng số dòng chi tiết quyết toán lần này
		/// </summary> 
		public string bcjsmxzts { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary> 
		public string xzlb { get; set; }
		/// <summary>
		/// Số xác định tai nạn lao động
		/// </summary> 
		public string gsrdbh { get; set; }
		/// <summary>
		/// Mã bệnh được xác định tai nạn lao động
		/// </summary> 
		public string gsrdjbbm { get; set; }
		/// <summary>
		/// Loại quyết toán bệnh bụi phổi
		/// </summary> 
		public string cfjslx { get; set; }
		/// <summary>
		/// Mã khoa xuất viện
		/// </summary> 
		public string cyksbm { get; set; }
		/// <summary>
		/// Mã bác sĩ xuất viện
		/// </summary> 
		public string cyysbm { get; set; }
	}

	/// <summary>
	/// Cập nhật thông tin khám bệnh (Mã loại giao dịch:03)Tham số đầu vào
	/// </summary>
	public class MedicalUpdateInPut
	{
		/// <summary>
		/// Số nhập viện(Số khám ngoại trú)
		/// </summary> 
		public string zymzh { get; set; }
		/// <summary>
		/// Cập nhật cờ
		/// </summary> 
		public string gxbz { get; set; }
		/// <summary>
		/// Loại hình y tế
		/// </summary> 
		public string yllb { get; set; }
		/// <summary>
		/// Mã khoa nhập viện
		/// </summary> 
		public string ryksbm { get; set; }
		/// <summary>
		/// Mã bác sĩ nhập viện
		/// </summary> 
		public string ryysbm { get; set; }
		/// <summary>
		/// Ngày nhập viện
		/// </summary> 
		public string ryrq { get; set; }
		/// <summary>
		/// Chẩn đoán nhập viện
		/// </summary> 
		public string ryzd { get; set; }
		/// <summary>
		/// Ngày xuất viện
		/// </summary> 
		public string cyrq { get; set; }
		/// <summary>
		/// Mã bệnh đã xác định
		/// </summary> 
		public string qzjbbm { get; set; }
		/// <summary>
		/// Lý do xuất viện
		/// </summary> 
		public string cyyy { get; set; }
		/// <summary>
		/// Người xử lý
		/// </summary> 
		public string jbr { get; set; }
		/// <summary>
		/// Biến chứng
		/// </summary> 
		public string bfz { get; set; }
		/// <summary>
		/// Số bệnh án
		/// </summary> 
		public string bah { get; set; }
		/// <summary>
		/// Số giấy chứng sinh
		/// </summary> 
		public string syzhm { get; set; }
		/// <summary>
		/// Ngày sinh trẻ sơ sinh
		/// </summary> 
		public DateTime? xsrcsrq { get; set; }
		/// <summary>
		/// Dấu khám đặc biệt cho cư dân
		/// </summary> 
		public string jmtsjzbj { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary> 
		public string xzlb { get; set; }
		/// <summary>
		/// Mã bệnh viện chuyển vào
		/// </summary> 
		public string zryybm { get; set; }
		/// <summary>
		/// Mã bác sĩ chẩn đoán xuất viện
		/// </summary> 
		public string cyzdysbm { get; set; }
		/// <summary>
		/// Mô tả tình trạng bệnh chính
		/// </summary> 
		public string zybqms { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 1
		/// </summary> 
		public string cycyjbzdbm1 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 2
		/// </summary> 
		public string cycyjbzdbm2 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 3
		/// </summary> 
		public string cycyjbzdbm3 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 4
		/// </summary> 
		public string cycyjbzdbm4 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 5
		/// </summary> 
		public string cycyjbzdbm5 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 6
		/// </summary> 
		public string cycyjbzdbm6 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 7
		/// </summary> 
		public string cycyjbzdbm7 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 8
		/// </summary> 
		public string cycyjbzdbm8 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 9
		/// </summary> 
		public string cycyjbzdbm9 { get; set; }
		/// <summary>
		/// Mã chẩn đoán bệnh phụ khi xuất viện 10
		/// </summary> 
		public string cycyjbzdbm10 { get; set; }
		/// <summary>
		/// Lời khai chính
		/// </summary> 
		public string zs { get; set; }
		/// <summary>
		/// Mô tả triệu chứng
		/// </summary> 
		public string zzms { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia khi nhập viện
        /// </summary>
        public string rygjysbm { get; set; }
        /// <summary>
        /// Mã bác sĩ quốc gia chẩn đoán xuất viện
        /// </summary>
        public string cyzdgjysbm { get; set; }
       
    }
	/// <summary>
	/// Hợp nhất Trùng Khánh,Quý An,Trường BHYT Thường Thục trả về
	/// </summary>
	public class CQMzjs05Dto: OutpatientSettGAYbFeeRelatedDTO
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
        public string pch { get; set; }
        public string bzbm { get; set; }
        public string bzmc { get; set; }
        public string yllb { get; set; }
        public string jzlx { get; set; }
        public string jzpzm { get; set; }
        /// <summary>
        /// Có phải là đối tượng rỗng hay không
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
		{
			return prm_yka055 == 0 && string.IsNullOrWhiteSpace(prm_aac002) && !ZFY.HasValue && !XJZF.HasValue &&
			       string.IsNullOrEmpty(jylsh);
		}
	}

	public class ZYToYBDto
	{
		public string sfzh { get; set; }
		public string kh { get; set; }
		public string cblb { get; set; }
        public string ryfs { get; set; }
        public string jzid { get; set; }
        public string jzpzlx { get; set; }
        public string xzlx { get; set; }
        public string grbh { get; set; }
        public string cbdbm { get; set; }
        public string jzh { get; set; }
        public string ybver { get; set; }

    }
	/// <summary>
	/// Trả đơn thuốc (Mã loại giao dịch:10)Tham số đầu vào
	/// </summary>
	public class RefundPrescriptionsInPut
	{
		/// <summary>
		/// Nhập viện(Khám ngoại trú)Số
		/// </summary>
		public string zymzh { get; set; }
		/// <summary>
		/// Số đơn thuốc
		/// </summary>
		public string cfh { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary>
		public string xzlb { get; set; }
	}
    public class Input_2205
    {
        public string cfh { get; set; }
        public string pch { get; set; }
    }

    public class Input_2204
    {
        public decimal? je { get; set; }
        public int? issc { get; set; }
    }

    public class Input_2203A
    {
        public string mdtrt_id { get; set; }
        public string psn_no { get; set; }
        public string med_type { get; set; }
        public string birctrl_type { get; set; }
        public string birctrl_matn_date { get; set; }
        public string matn_type { get; set; }
        public string dise_codg { get; set; }
        public string dise_name { get; set; }
    }
    public class Input_Bbrxx
    {
        /// <summary>
        /// Loại hình y tế
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string hisId { get; set; }
        /// <summary>
        /// Khám bệnhID
        /// </summary>
        public string mdtrt_id { get; set; }
        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// Loại nhân viên
        /// </summary>
        public string psn_no { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        /// <summary>
        /// 01 Token chứng từ điện tử 02 Số CMND/CCCD 03 Số thẻ BHXH
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        public string mdtrt_cert_no { get; set; }
        /// <summary>
        /// Loại hình bảo hiểm
        /// </summary>
        public string insutype { get; set; }
        public string dise_codg { get; set; }
        public string dise_name { get; set; }
        public string orgId { get; set; }
    }
    public class CancelSettInfo {
        public string mdtrt_id { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string insuplc_admdvs { get; set; }
        public string insutype { get; set; }
        public string operatorId { get; set; }
        public string operatorName { get; set; }
        public string hisId { get; set; }
    }
}
