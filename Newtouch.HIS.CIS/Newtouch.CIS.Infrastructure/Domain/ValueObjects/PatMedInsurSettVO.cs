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
        ///  người viên Loại
        /// </summary>
        public string psn_type { get; set; }
        /// <summary>
        /// công vụ viên Cờ
        /// </summary>
        public string cvlserv_flag { get; set; }
        public DateTime? setl_time { get; set; }
        /// <summary>
        /// Khám bệnh Chứng từ Loại
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// y tế Loại
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// y tế phí tổng mức
        /// </summary>
        public decimal? medfee_sumamt { get; set; }
        /// <summary>
        /// toàn tự chi trả Số tiền
        /// </summary>
        public decimal? fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// vượt hạn giá tự chi trả Chi phí
        /// </summary>
        public decimal? overlmt_selfpay { get; set; }
        /// <summary>
        /// trước dòng tự trả Số tiền
        /// </summary>
        public decimal? preselfpay_amt { get; set; }
        /// <summary>
        /// ký hợp chính sách phạm vi Số tiền
        /// </summary>
        public decimal? inscp_scp_amt { get; set; }
        /// <summary>
        ///  thực tế chi trả từ trả đường
        /// </summary>
        public decimal? act_pay_dedc { get; set; }
        /// <summary>
        /// cơ sở bản y tếbảonguy hiểm thống chuẩn bị cơ sở kim chi ra
        /// </summary>
        public decimal? hifp_pay { get; set; }
        /// <summary>
        /// cơ sở bản y tếbảonguy hiểm thống chuẩn bị cơ sở kim chi trả soVD
        /// </summary>
        public decimal? pool_prop_selfpay { get; set; }
        /// <summary>
        /// công vụ viên y tế bổ sung giúp tài kim chi ra
        /// </summary>
        public decimal? cvlserv_pay { get; set; }
        /// <summary>
        /// Chi từ quỹ BHYT bổ sung của doanh nghiệp
        /// </summary>
        public decimal? hifes_pay { get; set; }
        /// <summary>
        /// cư dânlớnbệnhbảonguy hiểm tài kim chi ra
        /// </summary>
        public decimal? hifmi_pay { get; set; }
        /// <summary>
        /// chức công lớn mức y tế Chi phí bổ sung giúp cơ sở kim chi ra
        /// </summary>
        public decimal? hifob_pay { get; set; }
        /// <summary>
        /// y tế cứu giúp cơ sở kim chi ra
        /// </summary>
        public decimal? maf_pay { get; set; }
        /// <summary>
        /// y viện gánh nặng Số tiền
        /// </summary>
        public decimal? hosp_part_amt { get; set; }
        public decimal? oth_pay { get; set; }
        /// <summary>
        /// cơ sở kim chi trả tổng mức
        /// </summary>
        public decimal? fund_pay_sumamt { get; set; }
        /// <summary>
        /// Cá nhân gánh nặng Tổng số tiền
        /// </summary>
        public decimal? psn_part_amt { get; set; }
        /// <summary>
        /// Cá nhân Tài khoản chi ra
        /// </summary>
        public decimal? acct_pay { get; set; }
        /// <summary>
        /// Cá nhân hiện kim chi ra
        /// </summary>
        public decimal? psn_cash_pay { get; set; }
        /// <summary>
        /// dư mức
        /// </summary>
        public decimal? balc { get; set; }
        /// <summary>
        /// Số tiền chi trả chung từ tài khoản cá nhân
        /// </summary>
        public decimal? acct_mulaid_pay { get; set; }
        /// <summary>
        /// y thuốc Cơ sở quyết toán ID
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
