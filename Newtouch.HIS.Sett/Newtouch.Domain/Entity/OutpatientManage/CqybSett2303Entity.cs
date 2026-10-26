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
    /// Quyết toán trước BHYT Trùng Khánh trả về và ghi nhận
    /// </summary>
    [Table("Drjk_prejs_output")]
    public class CqybSett2303Entity:IEntity<CqybSett2303Entity>
    {
        /// <summary>
        /// Khóa chínhprejs_id
        /// </summary>
        [Key]
        public string prejs_id { get; set; }
        /// <summary>
        /// 1|Khám bệnhID|Kiểu ký tự|30|  |Y|  
        /// </summary> 
        public string mdtrt_id { get; set; }

        /// <summary>
        /// 2|Quyết toánID|Kiểu ký tự|30||Y|
        /// </summary> 
		public string setl_id { get; set; }

        /// <summary>
        /// 3|Mã nhân viên|Kiểu ký tự|30|  |Y|  
        /// </summary> 
		public string psn_no { get; set; }

        /// <summary>
        /// 4|Tên nhân viên|Kiểu ký tự|50|  |Y|  
        /// </summary> 
		public string psn_name { get; set; }

        /// <summary>
        /// 5|Loại giấy tờ nhân viên|Kiểu ký tự|6|Y|Y|  
        /// </summary> 
		public string psn_cert_type { get; set; }

        /// <summary>
        /// 6|Số giấy tờ|Kiểu ký tự|50|  |Y|  
        /// </summary> 
        public string certno { get; set; }

        /// <summary>
        /// 7|Giới tính|Kiểu ký tự|6|Y|  |  
        /// </summary> 
        public string gend { get; set; }

        /// <summary>
        /// 8|Dân tộc|Kiểu ký tự|3|Y|  |  
        /// </summary> 
        public string naty { get; set; }

        /// <summary>
        /// 9|Ngày sinh|Kiểu ngày tháng|  |  |  |yyyy-MM-dd
        /// </summary> 
        public string brdy { get; set; }

        /// <summary>
        /// 10|Tuổi|Kiểu số|4,1|  |  |  
        /// </summary> 
        public string age { get; set; }

        /// <summary>
        /// 11|Loại hình bảo hiểm|Kiểu ký tự|6|Y|  |  
        /// </summary> 
        public string insutype { get; set; }

        /// <summary>
        /// 12|Loại nhân viên|Kiểu ký tự|6|Y|Y|  
        /// </summary> 
        public string psn_type { get; set; }

        /// <summary>
        /// 13|Cờ công chức|Kiểu ký tự|3|Y|Y|  
        /// </summary> 
        public string cvlserv_flag { get; set; }

        /// <summary>
        /// 14|Thời gian quyết toán|Kiểu ngày giờ|  |  ||yyyy-MM-dd HH:mm:ss
        /// </summary> 
        public string setl_time { get; set; }

        /// <summary>
        /// 15|Loại chứng từ khám bệnh|Kiểu ký tự|3|Y|  |  
        /// </summary> 
        public string mdtrt_cert_type { get; set; }

        /// <summary>
        /// 16|Loại hình y tế|Kiểu ký tự|6|Y|Y|  
        /// </summary> 
        public string med_type { get; set; }

        /// <summary>
        /// 17|Tổng chi phí y tế|Kiểu số|16,2|  |Y|  
        /// </summary> 
        public string medfee_sumamt { get; set; }

        /// <summary>
        /// 18|Tổng số tiền tự chi trả toàn bộ|Kiểu số|16,2|  |Y|  
        /// </summary> 
		public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 19|Chi phí tự chi trả vượt giá giới hạn|Kiểu số|16,2|  |Y|  
        /// </summary> 
		public string overlmt_selfpay { get; set; }

        /// <summary>
        /// 20|Số tiền tự trả trước|Kiểu số|16,2|  |Y|  
        /// </summary> 
		public string preselfpay_amt { get; set; }

        /// <summary>
        /// 21|Số tiền phù hợp phạm vi chính sách|Kiểu số|16,2|  |Y|  
        /// </summary> 
		public string inscp_scp_amt { get; set; }

        /// <summary>
        /// 22|Mức khởi điểm thực tế phải trả|Kiểu số|16,2|  |  |  
        /// </summary> 
		public string act_pay_dedc { get; set; }

        /// <summary>
        /// 23|Chi từ quỹ chung BHYT cơ bản|Kiểu số|16,2|  |Y|  
        /// </summary> 
        public string hifp_pay { get; set; }

        /// <summary>
        /// 24|Tỷ lệ chi trả của quỹ chung BHYT cơ bản|Kiểu số|5,4|  |Y|  
        /// </summary> 
        public string pool_prop_selfpay { get; set; }

        /// <summary>
        /// 25|Chi từ quỹ trợ cấp y tế cho công chức|Kiểu số|16,2|  |Y|  
        /// </summary> 
		public string cvlserv_pay { get; set; }

        /// <summary>
        /// 26|Chi từ quỹ BHYT bổ sung của doanh nghiệp|Kiểu số|16,2|  |Y  |  
        /// </summary> 
        public string hifes_pay { get; set; }

        /// <summary>
        /// 27|Chi từ quỹ bảo hiểm bệnh nặng cho cư dân|Kiểu số|16,2|  |Y  |  
        /// </summary> 
        public string hifmi_pay { get; set; }

        /// <summary>
        /// 28|Chi từ quỹ trợ cấp chi phí y tế lớn cho cán bộ công nhân viên|Kiểu số|16,2|  |Y  |  
        /// </summary> 
        public string hifob_pay { get; set; }

        /// <summary>
        /// 29|Chi từ quỹ cứu trợ y tế|Kiểu số|16,2|  |Y  |  
        /// </summary> 
        public string maf_pay { get; set; }

        /// <summary>
        /// 30|Số tiền bệnh viện chịu|Kiểu số|16,2|||
        /// </summary> 
        public string hosp_part_amt { get; set; }

        /// <summary>
        /// 31|Chi khác|Kiểu số|16,2|  |Y  |  
        /// </summary> 
        public string oth_pay { get; set; }

        /// <summary>
        /// 32|Tổng số tiền quỹ chi trả|Kiểu số|16,2|  |Y|  
        /// </summary> 
        public string fund_pay_sumamt { get; set; }

        /// <summary>
        /// 33|Tổng số tiền cá nhân phải chịu|Kiểu số|16,2|  ||  
        /// </summary> 
		public string psn_part_amt { get; set; }

        /// <summary>
        /// 34|Chi từ tài khoản cá nhân|Kiểu số|16,2|  |Y|  
        /// </summary> 
        public string acct_pay { get; set; }

        /// <summary>
        /// 35|Chi tiền mặt cá nhân|Kiểu số|16,2|  |Y|  
        /// </summary> 
        public string psn_cash_pay { get; set; }



        /// <summary>
        /// 36|Số dư|Kiểu số|16,2||Y|
        /// </summary> 
        public string balc { get; set; }

        /// <summary>
        /// 37|Số tiền chi trả từ tài khoản cá nhân dùng chung|Kiểu số|16,2||Y|
        /// </summary> 
        public string acct_mulaid_pay { get; set; }

        /// <summary>
        /// 38|Quyết toán cơ sở y dượcID|Kiểu ký tự|30||Y|Lưu trữ bản tin của bên gửiID
        /// </summary> 
		public string medins_setl_id { get; set; }

        /// <summary>
        /// 39|Cơ sở xử lý thanh toán|Kiểu ký tự|6||  |
        /// </summary> 
		public string clr_optins { get; set; }

        /// <summary>
        /// 40|Phương thức thanh toán|Kiểu ký tự|6|Y  |  |  
        /// </summary> 
        public string clr_way { get; set; }

        /// <summary>
        /// 41|Loại thanh toán|Kiểu ký tự|6|Y|Y  |
        /// </summary> 
        public string clr_type { get; set; }
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string zyh { get; set; }


        /// <summary>
        /// Nhân viên thao tác
        /// </summary>
        public string czydm { get; set; }
        /// <summary>
        /// Ngày thao tác
        /// </summary>
        public DateTime? czrq { get; set; }
        /// <summary>
        /// Trạng thái 1 Bình thường 0Hủy
        /// </summary>
        public int zt { get; set; }
        /// <summary>
        /// Nhân viên thao tác trạng thái
        /// </summary>
        public string zt_czy { get; set; }
        /// <summary>
        /// Ngày trạng thái
        /// </summary>
        public DateTime? zt_rq { get; set; }

    }
}
