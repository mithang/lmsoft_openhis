using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.DaySettleManage
{
	public class YBdzsqList
	{

	}
	public class ybzdinfo
	{
		/// <summary>
		/// Tên loại thanh toán
		/// </summary>
		public string label { get; set; }
		/// <summary>
		/// Mã loại thanh toán
		/// </summary>
		public string value { get; set; }
	}

	/// <summary>
	/// Thanh toán đối chiếu sổ tổng
	/// </summary>
	public class Qsdzz
	{
		/// <summary>
		/// Loại thanh toán
		/// </summary>
		public string qslb { get; set; }

		public string qslbmc { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary>
		public string xz { get; set; }
		public string xzmc { get; set; }
		/// <summary>
		/// Có phải khác địa phương hay không
		/// </summary>
		public string sfyd { get; set; }
		public DateTime ksrq { get; set; }
		public DateTime jsrq { get; set; }
		public decimal zfy { get; set; }
		public decimal jjzfje { get; set; }
		public decimal grzh { get; set; }
		public decimal xjzfje { get; set; }
		public int jsbs { get; set; }
		public string sftf { get; set; }

		public decimal czzgtcjj { get; set; }/*Quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public int czzgtcjjrc { get; set; }/*Lượt người quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public decimal czzgzhjj { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public int czzgzhjjrc { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public decimal gywbzjj { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public int gywbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public decimal deylbzjj { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public int deylbzjjrc { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public decimal lxrybzjj { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public int lxrybzjjrc { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public decimal cjbzjj { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public int cjbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public decimal qybzyljj { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public int qybzyljjrc { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public decimal cxjmjbyljj { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public int cxjmjbyljjrc { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public decimal cxjmdbyljj { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public int cxjmdbyljjrc { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public decimal yljzjj { get; set; }/*Quỹ cứu trợ y tế*/
		public int yljzjjrc { get; set; }/*Lượt người quỹ cứu trợ y tế*/
		public decimal yfjj { get; set; }/*Quỹ ưu đãi*/
		public int yfjjrc { get; set; }/*Lượt người quỹ ưu đãi*/
		public decimal syjj { get; set; }/*Quỹ sinh đẻ*/
		public int syjjrc { get; set; }/*Lượt người quỹ sinh đẻ*/
		public decimal qtjj { get; set; }/*Quỹ khác*/
		public int qtjjrc { get; set; }/*Lượt người quỹ khác*/
		public decimal eyjj { get; set; }/*Quỹ hạng 2B*/
		public int eyjjrc { get; set; }/*Lượt người quỹ hạng 2B*/
	}

	/// <summary>
	/// Chi tiết thanh toán(Nửa trên)
	/// </summary>
	public class Qsmx
	{
		/// <summary>
		/// Mã cá nhân
		/// </summary>
		public string grbm { get; set; }
		/// <summary>
		/// Khám bệnhID
		/// </summary>
		public string jzid { get; set; }
		/// <summary>
		/// Quyết toánID
		/// </summary>
		public string jsid { get; set; }
		/// <summary>
		/// Họ tên
		/// </summary>
		public string xm { get; set; }
		/// <summary>
		/// Ngày quyết toán BHYT
		/// </summary>
		public DateTime ybjsrq { get; set; }
		/// <summary>
		/// Tổng chi phí
		/// </summary>
		public decimal zfy { get; set; }
		/// <summary>
		/// Số tiền quỹ chi trả
		/// </summary>
		public decimal jjzfje { get; set; }

		/// <summary>
		/// Thanh toán từ tài khoản cá nhân
		/// </summary>
		public decimal acct_pay { get; set; }
		/// <summary>
		/// Tiền mặt cá nhân thanh toán
		/// </summary>
		public decimal psn_cash_pay { get; set; }

		/// <summary>
		/// Số tiền thanh toán dùng chung
		/// </summary>
		public decimal gjje { get; set; }

		/// <summary>
		/// Có hoàn phí hay không
		/// </summary>
		public string sftf { get; set; }
		/// <summary>
		/// Loại hoàn trả
		/// </summary>
		public string bxlx { get; set; }
		/// <summary>
		/// CMND/CCCD
		/// </summary>
		public string sfz { get; set; }
		/// <summary>
		/// Số biên lai
		/// </summary>
		public string sjh { get; set; }
		/// <summary>
		/// Loại thanh toán
		/// </summary>
		public string qslb { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary>
		public string xz { get; set; }
		/// <summary>
		/// Ngày bắt đầu
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// Ngày kết thúc
		/// </summary>
		public DateTime jsrq { get; set; }
	}

	/// <summary>
	/// Chi tiết thanh toán(Nửa dưới)
	/// </summary>
	public class Qsmx_1
	{
		/// <summary>
		/// Nhân viênID
		/// </summary>
		public string ryid { get; set; }
		/// <summary>
		/// Khám bệnhID
		/// </summary>
		public string jzid { get; set; }
		/// <summary>
		/// Quyết toánID
		/// </summary>
		public string jsid { get; set; }
		/// <summary>
		/// Kết quả đối soát
		/// </summary>
		public string dzjg { get; set; }
		/// <summary>
		/// Ghi chú
		/// </summary>
		public string bz { get; set; }
		/// <summary>
		/// Tổng chi phí y tế
		/// </summary>
		public decimal ylfze { get; set; }
		/// <summary>
		/// Tổng số tiền quỹ chi trả
		/// </summary>
		public decimal jjzfze { get; set; }
		/// <summary>
		/// Tổng số tiền thanh toán từ tài khoản cá nhân
		/// </summary>
		public decimal grzhzfze { get; set; }
		/// <summary>
		/// Bản tin bên gửiID
		/// </summary>
		public string fsfbwid { get; set; }
		/// <summary>
		/// Cờ quyết toán hoàn phí
		/// </summary>
		public string tfjsbz { get; set; }
		/// <summary>
		/// Ngày bắt đầu
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// Ngày kết thúc
		/// </summary>
		public DateTime jsrq { get; set; }
	}

	/// <summary>
	/// Đối soát thành công thì lưu
	/// </summary>
	public class ybqssq
	{
		public string qslb { get; set; }
		public string xz { get; set; }
		public string sfyd { get; set; }
		public string ksrq { get; set; }
		public string jsrq { get; set; }
		public decimal zfy { get; set; }
		public decimal jjzfje { get; set; }
		public decimal grzh { get; set; }
		public decimal ybrkfyze { get; set; }
		public decimal xjzfze { get; set; }
		public int qsrc { get; set; }
		public string qsny { get; set; }
		public decimal czzgtcjj { get; set; }/*Quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public int czzgtcjjrc { get; set; }/*Lượt người quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public decimal czzgzhjj { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public int czzgzhjjrc { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public decimal gywbzjj { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public int gywbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public decimal deylbzjj { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public int deylbzjjrc { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public decimal lxrybzjj { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public int lxrybzjjrc { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public decimal cjbzjj { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public int cjbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public decimal qybzyljj { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public int qybzyljjrc { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public decimal cxjmjbyljj { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public int cxjmjbyljjrc { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public decimal cxjmdbyljj { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public int cxjmdbyljjrc { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public decimal yljzjj { get; set; }/*Quỹ cứu trợ y tế*/
		public int yljzjjrc { get; set; }/*Lượt người quỹ cứu trợ y tế*/
		public decimal yfjj { get; set; }/*Quỹ ưu đãi*/
		public int yfjjrc { get; set; }/*Lượt người quỹ ưu đãi*/
		public decimal syjj { get; set; }/*Quỹ sinh đẻ*/
		public int syjjrc { get; set; }/*Lượt người quỹ sinh đẻ*/
		public decimal qtjj { get; set; }/*Quỹ khác*/
		public int qtjjrc { get; set; }/*Lượt người quỹ khác*/
		public decimal eyjj { get; set; }/*Quỹ hạng 2B*/
		public int eyjjrc { get; set; }/*Lượt người quỹ hạng 2B*/
		public string create_user { get; set; }
		public int sqzt { get; set; }

	}

	/// <summary>
	/// Đơn thanh toán
	/// </summary>
	public class Qssq
	{

		/// <summary>
		/// Trạng thái đăng ký
		/// </summary>
		public string sqzt { get; set; }
		/// <summary>
		/// Loại thanh toán
		/// </summary>
		public string qslb { get; set; }

		public string qslbmc { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary>
		public string xzlx { get; set; }
		/// <summary>
		/// Có phải khác địa phương hay không
		/// </summary>
		public string sfyd { get; set; }
		/// <summary>
		/// Ngày bắt đầu
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// Ngày kết thúc
		/// </summary>
		public DateTime jsrq { get; set; }
		/// <summary>
		/// Tổng chi phí
		/// </summary>
		public decimal zfy { get; set; }
		/// <summary>
		/// Tổng số tiền quỹ chi trả
		/// </summary>
		public decimal jjzfze { get; set; }
		/// <summary>
		/// Thanh toán từ tài khoản cá nhân
		/// </summary>
		public decimal gzzf { get; set; }
		/// <summary>
		/// Tổng chi phí BHYT công nhận
		/// </summary>
		public decimal ybrkfyze { get; set; }
		/// <summary>
		/// Tổng số tiền thanh toán tiền mặt
		/// </summary>
		public decimal xjzfze { get; set; }
		/// <summary>
		/// Lượt người thanh toán
		/// </summary>
		public int qsrc { get; set; }
		/// <summary>
		/// Năm tháng thanh toán
		/// </summary>
		public string qsny { get; set; }
		/// <summary>
		/// Số tiền khai báo ngoại trú
		/// </summary>
		public decimal mzsbje { get; set; }
		/// <summary>
		/// Số tiền khai báo nội trú
		/// </summary>
		public decimal zysbje { get; set; }



		public decimal czzgtcjj { get; set; }/*Quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public int czzgtcjjrc { get; set; }/*Lượt người quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public decimal czzgzhjj { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public int czzgzhjjrc { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public decimal gywbzjj { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public int gywbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public decimal deylbzjj { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public int deylbzjjrc { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public decimal lxrybzjj { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public int lxrybzjjrc { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public decimal cjbzjj { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public int cjbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public decimal qybzyljj { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public int qybzyljjrc { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public decimal cxjmjbyljj { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public int cxjmjbyljjrc { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public decimal cxjmdbyljj { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public int cxjmdbyljjrc { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public decimal yljzjj { get; set; }/*Quỹ cứu trợ y tế*/
		public int yljzjjrc { get; set; }/*Lượt người quỹ cứu trợ y tế*/
		public decimal yfjj { get; set; }/*Quỹ ưu đãi*/
		public int yfjjrc { get; set; }/*Lượt người quỹ ưu đãi*/
		public decimal syjj { get; set; }/*Quỹ sinh đẻ*/
		public int syjjrc { get; set; }/*Lượt người quỹ sinh đẻ*/
		public decimal qtjj { get; set; }/*Quỹ khác*/
		public int qtjjrc { get; set; }/*Lượt người quỹ khác*/
		public decimal eyjj { get; set; }/*Quỹ hạng 2B*/
		public int eyjjrc { get; set; }/*Lượt người quỹ hạng 2B*/

	}

	/// <summary>
	/// Quay lại thanh toán
	/// </summary>
	public class Qsht
	{
		/// <summary>
		/// Số kỳ thanh toán
		/// </summary>
		public string qsqh { get; set; }
		/// <summary>
		/// Loại thanh toán
		/// </summary>
		public string qslb { get; set; }
		/// <summary>
		/// Tên loại thanh toán
		/// </summary>
		public string qslbmc { get; set; }
		/// <summary>
		/// Loại hình bảo hiểm
		/// </summary>
		public string xzlx { get; set; }
		/// <summary>
		/// Có phải khác địa phương hay không
		/// </summary>
		public string sfyd { get; set; }
		/// <summary>
		/// Số giao dịch thanh toán
		/// </summary>
		public string qslsh { get; set; }
		/// <summary>
		/// Tổng chi phí
		/// </summary>
		public decimal zfy { get; set; }
		/// <summary>
		/// Tổng số tiền quỹ khai báo
		/// </summary>
		public decimal jjsbze { get; set; }
		/// <summary>
		/// Tổng số tiền thanh toán từ tài khoản cá nhân
		/// </summary>
		public decimal gzzfze { get; set; }
		/// <summary>
		/// Tổng số tiền mặt cá nhân thanh toán
		/// </summary>
		public decimal grxjzfze { get; set; }
		/// <summary>
		/// Lượt người thanh toán
		/// </summary>
		public int qsrc { get; set; }
		/// <summary>
		/// Ngày bắt đầu
		/// </summary>
		public DateTime ksrq { get; set; }
		/// <summary>
		/// Ngày kết thúc
		/// </summary>
		public DateTime jsrq { get; set; }
		/// <summary>
		/// Ngày nộp đơn thanh toán
		/// </summary>
		public DateTime qssqrq { get; set; }
		/// <summary>
		/// Người nộp đơn thanh toán
		/// </summary>
		public string qssqr { get; set; }
		/// <summary>
		/// Mã cơ sở y tế
		/// </summary>
		public string yljgdm { get; set; }
		/// <summary>
		/// Tên cơ sở y tế
		/// </summary>
		public string yljgmc { get; set; }
		/// <summary>
		/// Tên cơ sở xử lý thanh toán
		/// </summary>
		public string qsjbjgmc { get; set; }
		/// <summary>
		/// Mã cơ sở xử lý thanh toán
		/// </summary>
		public string qsjbjgbm { get; set; }
		/// <summary>
		/// Lượt khám ngoại trú
		/// </summary>
		public int mzrc { get; set; }
		/// <summary>
		/// Lượt người nội trú
		/// </summary>
		public int zyrc { get; set; }
		/// <summary>
		/// Tổng chi phí ngoại trú
		/// </summary>
		public decimal mzzfy { get; set; }
		/// <summary>
		/// Tổng chi phí nội trú
		/// </summary>
		public decimal zyzfy { get; set; }
		/// <summary>
		/// Số tiền khai báo ngoại trú
		/// </summary>
		public decimal mzsbje { get; set; }
		/// <summary>
		/// Số tiền khai báo nội trú
		/// </summary>
		public decimal zysbje { get; set; }



		public decimal czzgtcjj { get; set; }/*Quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public int czzgtcjjrc { get; set; }/*Lượt người quỹ chung BHYT cơ bản cho cán bộ công nhân viên thành thị*/
		public decimal czzgzhjj { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public int czzgzhjjrc { get; set; }/*Quỹ tài khoản cá nhân BHYT cho cán bộ công nhân viên thành thị*/
		public decimal gywbzjj { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public int gywbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho công chức*/
		public decimal deylbzjj { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public int deylbzjjrc { get; set; }/*Quỹ trợ cấp chi phí y tế lớn*/
		public decimal lxrybzjj { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public int lxrybzjjrc { get; set; }/*Quỹ bảo đảm y tế cho cán bộ nghỉ hưu*/
		public decimal cjbzjj { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public int cjbzjjrc { get; set; }/*Quỹ trợ cấp y tế cho quân nhân khuyết tật hạng 1-6*/
		public decimal qybzyljj { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public int qybzyljjrc { get; set; }/*Quỹ BHYT bổ sung của doanh nghiệp*/
		public decimal cxjmjbyljj { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public int cxjmjbyljjrc { get; set; }/*Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn*/
		public decimal cxjmdbyljj { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public int cxjmdbyljjrc { get; set; }/*Quỹ BHYT bệnh nặng cho cư dân thành thị và nông thôn*/
		public decimal yljzjj { get; set; }/*Quỹ cứu trợ y tế*/
		public int yljzjjrc { get; set; }/*Lượt người quỹ cứu trợ y tế*/
		public decimal yfjj { get; set; }/*Quỹ ưu đãi*/
		public int yfjjrc { get; set; }/*Lượt người quỹ ưu đãi*/
		public decimal syjj { get; set; }/*Quỹ sinh đẻ*/
		public int syjjrc { get; set; }/*Lượt người quỹ sinh đẻ*/
		public decimal qtjj { get; set; }/*Quỹ khác*/
		public int qtjjrc { get; set; }/*Lượt người quỹ khác*/
		public decimal eyjj { get; set; }/*Quỹ hạng 2B*/
		public int eyjjrc { get; set; }/*Lượt người quỹ hạng 2B*/
									   /// <summary>
									   /// Tổng các quỹ
									   /// </summary>
		public decimal gxjjzh { get; set; }
	}

	public class RdrlsList
	{
		/// <summary>
		/// Thời gian đối soát
		/// </summary>
		public DateTime? czrq { get; set; }
		/// <summary>
		/// Ngày đối soát Định dạng ngày tháng 8 Không được để trống
		/// </summary>
		public string daycollate { get; set; }
		/// <summary>
		/// Số giao dịch trung tâm ngày đối soát Định dạng số B Không được để trống
		/// </summary>
		public int daycount { get; set; }
		/// <summary>
		/// Tổng số tiền chi từ tài khoản trong năm Định dạng số A Không được để trống
		/// </summary>
		public decimal totalcuraccpay { get; set; }
		/// <summary>
		/// Tổng số tiền chi từ tài khoản các năm trước Định dạng số A Không được để trống
		/// </summary>
		public decimal totalhisaccpay { get; set; }
		/// <summary>
		/// Tổng số tiền mặt tự chi trả Định dạng số A Không được để trống
		/// </summary>
		public decimal totalcashpay { get; set; }
		/// <summary>
		/// Tổng số tiền chi trả từ quỹ chung Định dạng số A Không được để trống
		/// </summary>
		public decimal totaltcpay { get; set; }
		/// <summary>
		/// Tổng số tiền thanh toán bổ sung Định dạng số A Không được để trống
		/// </summary>
		public decimal totaldffjpay { get; set; }
		/// <summary>
		/// Tổng số tiền tự chi trả theo phân loại Định dạng số A Không được để trống
		/// </summary>
		public decimal totalflzf { get; set; }
		/// <summary>
		/// Tổng chi phí ngoài phạm vi quyết toán BHYT
		/// </summary>
		public decimal totalfybjsfw { get; set; }

		/// <summary>
		/// Trả về kết quả đối soát
		/// </summary>
		public string resultcollate { get; set; }
		/// <summary>
		/// Trả về ngày đối soát
		/// </summary>
		public string outdaycollate { get; set; }
	}

	public class RdrNewList
	{
		/// <summary>
		/// Loại mz:Dữ liệu ngoại trú zy:Dữ liệu nội trú all:Dữ liệu tổng hợp toàn bộ
		/// </summary>
		public string typetext { get; set; }
		/// <summary>
		/// Ngày đối soát Định dạng ngày tháng 8 Không được để trống
		/// </summary>
		public string daycollate { get; set; }
		/// <summary>
		/// Số giao dịch trung tâm ngày đối soát Định dạng số B Không được để trống
		/// </summary>
		public int daycount { get; set; }
		/// <summary>
		/// Tổng số tiền chi từ tài khoản trong năm Định dạng số A Không được để trống
		/// </summary>
		public decimal totalcuraccpay { get; set; }
		/// <summary>
		/// Tổng số tiền chi từ tài khoản các năm trước Định dạng số A Không được để trống
		/// </summary>
		public decimal totalhisaccpay { get; set; }
		/// <summary>
		/// Tổng số tiền mặt tự chi trả Định dạng số A Không được để trống
		/// </summary>
		public decimal totalcashpay { get; set; }
		/// <summary>
		/// Tổng số tiền chi trả từ quỹ chung Định dạng số A Không được để trống
		/// </summary>
		public decimal totaltcpay { get; set; }
		/// <summary>
		/// Tổng số tiền thanh toán bổ sung Định dạng số A Không được để trống
		/// </summary>
		public decimal totaldffjpay { get; set; }
		/// <summary>
		/// Tổng số tiền tự chi trả theo phân loại Định dạng số A Không được để trống
		/// </summary>
		public decimal totalflzf { get; set; }
		/// <summary>
		/// Tổng chi phí ngoài phạm vi quyết toán BHYT
		/// </summary>
		public decimal totalfybjsfw { get; set; }
	}

    public class HistoryCheckVO
    {
        public string lsh {get;set;}
        public string cardid { get; set; }
        public DateTime jysj { get; set; }
        public string lx { get; set; }
        public decimal dn { get; set; }
        public decimal ln { get; set; }
        public decimal xj { get; set; }
        public decimal tc { get; set; }
        public int df { get; set; }
        public decimal flzf { get; set; }
        public decimal grzf { get; set; }
        public string xm { get; set; }
        public string mzbh { get; set; }
        public string hm { get; set; }
        public string zd { get; set; }
        public string bz { get; set; }

    }
}
