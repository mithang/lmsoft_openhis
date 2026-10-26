using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.YibaoInterfaceManage
{
    public class Output_2207VO
    {
        public int jsnm { get; set; }
        /// <summary>
        /// 30|Khám bệnhID|Kiểu ký tự|  |Y|  
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 30|Quyết toánID|Kiểu ký tự||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 30|Mã nhân viên|Kiểu ký tự|  |Y|  
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 50|Tên nhân viên|Kiểu ký tự|  |Y|  
        /// </summary> 
		public string psn_name { get; set; }

        /// <summary>
        /// 6|Loại giấy tờ nhân viên|Kiểu ký tự|Y|Y|  
        /// </summary> 
		public string psn_cert_type { get; set; }

        /// <summary>
        /// 50|Số giấy tờ|Kiểu ký tự|  |Y|  
        /// </summary> 
		public string certno { get; set; }

        /// <summary>
        /// 6|Giới tính|Kiểu ký tự|Y|  |  
        /// </summary> 
		public string gend { get; set; }

        /// <summary>
        /// 3|Dân tộc|Kiểu ký tự|Y|  |  
        /// </summary> 
		public string naty { get; set; }

        /// <summary>
        ///   |Ngày sinh|Kiểu ngày tháng|  |  |yyyy-MM-dd
        /// </summary> 
		public string brdy { get; set; }

        /// <summary>
        /// 4,1|Tuổi|Kiểu số|  |  |  
        /// </summary> 
		public string age { get; set; }

        /// <summary>
        /// 6|Loại hình bảo hiểm|Kiểu ký tự|Y|  |  
        /// </summary> 
		public string insutype { get; set; }

        /// <summary>
        /// 6|Loại nhân viên|Kiểu ký tự|Y|Y|  
        /// </summary> 
		public string psn_type { get; set; }

        /// <summary>
        /// 3|Cờ công chức|Kiểu ký tự|Y|Y|  
        /// </summary> 
		public string cvlserv_flag { get; set; }

        /// <summary>
        ///   |Thời gian quyết toán|Kiểu ngày giờ|  |Y|yyyy-MM-dd HH:mm:ss
        /// </summary> 
		public DateTime? setl_time { get; set; }

        /// <summary>
        /// 3|Loại chứng từ khám bệnh|Kiểu ký tự|Y|  |  
        /// </summary> 
		public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 6|Loại hình y tế|Kiểu ký tự|Y|Y|  
        /// </summary> 
        public string med_type { get; set; }

        /// <summary>
        /// 16,2|Tổng chi phí y tế|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? medfee_sumamt { get; set; }

        /// <summary>
        /// 16,2|Tổng số tiền tự chi trả toàn bộ|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 16,2|Chi phí tự chi trả vượt giá giới hạn|Kiểu số|  |Y|  
        /// </summary> 
		public decimal? overlmt_selfpay { get; set; }

        /// <summary>
        /// 16,2|Số tiền tự trả trước|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? preselfpay_amt { get; set; }

        /// <summary>
        /// 16,2|Số tiền phù hợp phạm vi chính sách|Kiểu số|  |Y|  
        /// </summary> 
		public decimal? inscp_scp_amt { get; set; }

        /// <summary>
        /// 16,2|Mức khởi điểm thực tế phải trả|Kiểu số|  |  |  
        /// </summary> 
		public decimal? act_pay_dedc { get; set; }

        /// <summary>
        /// 16,2|Chi từ quỹ chung BHYT cơ bản|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? hifp_pay { get; set; }

        /// <summary>
        /// 5,4|Tỷ lệ chi trả của quỹ chung BHYT cơ bản|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? pool_prop_selfpay { get; set; }

        /// <summary>
        /// 16,2|Chi từ quỹ trợ cấp y tế cho công chức|Kiểu số|  |Y|  
        /// </summary> 
		public decimal? cvlserv_pay { get; set; }

        /// <summary>
        /// 16,2|Chi từ quỹ BHYT bổ sung của doanh nghiệp|Kiểu số|  |Y  |  
        /// </summary> 
        public decimal? hifes_pay { get; set; }

        /// <summary>
        /// 16,2|Chi từ quỹ bảo hiểm bệnh nặng cho cư dân|Kiểu số|  |Y  |  
        /// </summary> 
        public decimal? hifmi_pay { get; set; }

        /// <summary>
        /// 16,2|Chi từ quỹ trợ cấp chi phí y tế lớn cho cán bộ công nhân viên|Kiểu số|  |Y  |  
        /// </summary> 
        public decimal? hifob_pay { get; set; }

        /// <summary>
        /// 16,2|Chi từ quỹ cứu trợ y tế|Kiểu số|  |Y  |  
        /// </summary> 
        public decimal? maf_pay { get; set; }

        /// <summary>
        /// 16,2|Chi khác|Kiểu số|  |Y  |  
        /// </summary> 
        public decimal? oth_pay { get; set; }

        /// <summary>
        /// 16,2|Tổng số tiền quỹ chi trả|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? fund_pay_sumamt { get; set; }

        /// <summary>
        /// 16,2|Tổng số tiền cá nhân phải chịu|Kiểu số|  |Y|  
        /// </summary> 
		public decimal? psn_part_amt { get; set; }

        /// <summary>
        /// 16,2|Chi từ tài khoản cá nhân|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? acct_pay { get; set; }

        /// <summary>
        /// 16,2|Chi tiền mặt cá nhân|Kiểu số|  |Y|  
        /// </summary> 
        public decimal? psn_cash_pay { get; set; }

        /// <summary>
        /// 16,2|Số tiền bệnh viện chịu|Kiểu số||Y|
        /// </summary> 
        public decimal? hosp_part_amt { get; set; }

        /// <summary>
        /// 16,2|Số dư|Kiểu số||Y|
        /// </summary> 
		public decimal? balc { get; set; }

        /// <summary>
        /// 16,2|Số tiền chi trả từ tài khoản cá nhân dùng chung|Kiểu số||Y|
        /// </summary> 
        public decimal? acct_mulaid_pay { get; set; }

        /// <summary>
        /// 30|Quyết toán cơ sở y dượcID|Kiểu ký tự||Y|Lưu trữ bản tin của bên gửiID
        /// </summary> 
		public string medins_setl_id { get; set; }

        /// <summary>
        /// 6|Cơ sở xử lý thanh toán|Kiểu ký tự||  |
        /// </summary> 
		public string clr_optins { get; set; }

        /// <summary>
        /// 6|Phương thức thanh toán|Kiểu ký tự|Y  |  |  
        /// </summary> 
        public string clr_way { get; set; }

        /// <summary>
        /// 6|Loại thanh toán|Kiểu ký tự|Y|Y  |
        /// </summary> 
        public string clr_type { get; set; }

        public string pch { get; set; }
        public string yllb { get; set; }
    }
}
