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
        ///  ngườiviênLoại
        /// </summary>
        public string psn_type { get; set; }
        /// <summary>
        /// côngvụviênCờ
        /// </summary>
        public string cvlserv_flag { get; set; }
        public DateTime? setl_time { get; set; }
        /// <summary>
        /// Khám bệnhChứng từLoại
        /// </summary>
        public string mdtrt_cert_type { get; set; }
        /// <summary>
        /// y tếLoại
        /// </summary>
        public string med_type { get; set; }
        /// <summary>
        /// y tếphítổngmức
        /// </summary>
        public decimal? medfee_sumamt { get; set; }
        /// <summary>
        /// toàntự chi trảSố tiền
        /// </summary>
        public decimal? fulamt_ownpay_amt { get; set; }
        /// <summary>
        /// vượthạngiátự chi trảChi phí
        /// </summary>
        public decimal? overlmt_selfpay { get; set; }
        /// <summary>
        /// trướcdòngtựtrảSố tiền
        /// </summary>
        public decimal? preselfpay_amt { get; set; }
        /// <summary>
        /// kýhợpchính sáchphạm viSố tiền
        /// </summary>
        public decimal? inscp_scp_amt { get; set; }
        /// <summary>
        ///  thực tếchitrảtừtrảđường
        /// </summary>
        public decimal? act_pay_dedc { get; set; }
        /// <summary>
        /// cơ sởbảny tếbảonguy hiểmthốngchuẩn bịcơ sởkimchira
        /// </summary>
        public decimal? hifp_pay { get; set; }
        /// <summary>
        /// cơ sởbảny tếbảonguy hiểmthốngchuẩn bịcơ sởkim chitrảsoVD
        /// </summary>
        public decimal? pool_prop_selfpay { get; set; }
        /// <summary>
        /// côngvụviêny tếbổ sunggiúptàikimchi ra
        /// </summary>
        public decimal? cvlserv_pay { get; set; }
        /// <summary>
        /// 企业Bổ sungy tếbảonguy hiểmcơ sởkim chira
        /// </summary>
        public decimal? hifes_pay { get; set; }
        /// <summary>
        /// cư dânlớnbệnhbảonguy hiểmtàikimchira
        /// </summary>
        public decimal? hifmi_pay { get; set; }
        /// <summary>
        /// chứccônglớnmứcy tếChi phíbổ sunggiúp cơ sởkimchira
        /// </summary>
        public decimal? hifob_pay { get; set; }
        /// <summary>
        /// y tếcứugiúpcơ sởkimchira
        /// </summary>
        public decimal? maf_pay { get; set; }
        /// <summary>
        /// yviệngánh nặngSố tiền
        /// </summary>
        public decimal? hosp_part_amt { get; set; }
        public decimal? oth_pay { get; set; }
        /// <summary>
        /// cơ sởkimchitrảtổngmức
        /// </summary>
        public decimal? fund_pay_sumamt { get; set; }
        /// <summary>
        /// Cá nhângánh nặngTổng số tiền
        /// </summary>
        public decimal? psn_part_amt { get; set; }
        /// <summary>
        /// Cá nhânTài khoảnchira
        /// </summary>
        public decimal? acct_pay { get; set; }
        /// <summary>
        /// Cá nhânhiệnkimchira
        /// </summary>
        public decimal? psn_cash_pay { get; set; }
        /// <summary>
        /// dưmức
        /// </summary>
        public decimal? balc { get; set; }
        /// <summary>
        /// Cá nhânTài khoảnchung济chitrảSố tiền
        /// </summary>
        public decimal? acct_mulaid_pay { get; set; }
        /// <summary>
        /// ythuốcCơ sởquyết toán ID
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
