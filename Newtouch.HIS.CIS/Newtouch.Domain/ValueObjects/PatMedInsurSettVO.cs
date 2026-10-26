using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PatMedInsurSettVO
    {
        public string prejs_id { get; set; }
        public string mdtrt_id { get; set; }
        public string zyh { get; set; }
        public string psn_no { get; set; }
        public string psn_name { get; set; }
        public string setl_id { get; set; }
        public string certno { get; set; }
        public string gend { get; set; }
        public DateTime? brdy { get; set; }
        public decimal? age { get; set; }
        public string insutype { get; set; }
        /// <summary>
        /// Loại nhân viên
        /// </summary>
        public string psn_type { get; set; }
        /// <summary>
        /// Cờ công chức
        /// </summary>
        public string cvlserv_flag { get; set; }
        public DateTime? setl_time { get; set; }
        /// <summary>
        /// Loại chứng từ khám bệnh
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// Loại hình y tế
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// Tổng chi phí y tế
        /// </summary>
        public decimal? medfee_sumamt { get; set; }
        /// <summary>
        /// Số tiền hoàn toàn tự chi trả
        /// </summary>
        public decimal? fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// Chi phí tự chi trả vượt giá giới hạn
        /// </summary>
        public decimal? overlmt_selfpay { get; set; }
        /// <summary>
        /// Số tiền tự trả trước
        /// </summary>
        public decimal? preselfpay_amt { get; set; }
        /// <summary>
        /// Số tiền phù hợp phạm vi chính sách
        /// </summary>
        public decimal? inscp_scp_amt { get; set; }
        /// <summary>
        ///  Mức khởi điểm thực tế phải trả
        /// </summary>
        public decimal? act_pay_dedc { get; set; }
        /// <summary>
        /// Chi quỹ chung bảo hiểm y tế cơ bản
        /// </summary>
        public decimal? hifp_pay { get; set; }
        /// <summary>
        /// Quỹ chung bảo hiểm y tế cơ bản Tỷ lệ thanh toán
        /// </summary>
        public decimal? pool_prop_selfpay { get; set; }
        /// <summary>
        /// Chi quỹ trợ cấp y tế cho công chức
        /// </summary>
        public decimal? cvlserv_pay { get; set; }
        /// <summary>
        /// Quỹ BHYT bổ sung doanh nghiệp Chi
        /// </summary>
        public decimal? hifes_pay { get; set; }
        /// <summary>
        /// Chi quỹ bảo hiểm bệnh nặng cư dân
        /// </summary>
        public decimal? hifmi_pay { get; set; }
        /// <summary>
        /// Trợ cấp chi phí y tế lớn cho nhân viên Chi quỹ
        /// </summary>
        public decimal? hifob_pay { get; set; }
        /// <summary>
        /// Chi quỹ cứu trợ y tế
        /// </summary>
        public decimal? maf_pay { get; set; }
        /// <summary>
        /// Số tiền bệnh viện chịu
        /// </summary>
        public decimal? hosp_part_amt { get; set; }
        public decimal? oth_pay { get; set; }
        /// <summary>
        /// Tổng số tiền quỹ thanh toán
        /// </summary>
        public decimal? fund_pay_sumamt { get; set; }
        /// <summary>
        /// Tổng số tiền cá nhân chịu
        /// </summary>
        public decimal? psn_part_amt { get; set; }
        /// <summary>
        /// Chi tài khoản cá nhân
        /// </summary>
        public decimal? acct_pay { get; set; }
        /// <summary>
        /// Chi tiền mặt cá nhân
        /// </summary>
        public decimal? psn_cash_pay { get; set; }
        /// <summary>
        /// Số dư
        /// </summary>
        public decimal? balc { get; set; }
        /// <summary>
        /// Số tiền thanh toán chung tài khoản cá nhân
        /// </summary>
        public decimal? acct_mulaid_pay { get; set; }
        /// <summary>
        /// Quyết toán cơ sở y dược ID
        /// </summary>
        public string medins_setl_id { get; set; }
        public string clr_optins { get; set; }
        public string clr_way { get; set; }
        public string clr_type { get; set; }
        public string czydm { get; set; }
        public DateTime? czrq { get; set; }
        public string mdtrt_cert_no { get; set; }
        public string mid_setl_flag { get; set; }
    }

    public class PatQfWarnVo
    {
        public string zyh { get; set; }
        public string brxzdm { get; set; }
        public decimal? zje { get; set; }
        public decimal? zhye { get; set; }
        public decimal? yjfy { get; set; }
        public decimal? bje { get; set; }
        public decimal? ybxjzf { get; set; }
        public decimal? zhsy { get; set; }
    }
}
