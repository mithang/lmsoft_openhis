using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    /// <summary>
    /// Bảng hạng mục dịch vụ y tế
    /// </summary>
    public class MedinsDaySetlResult
    {
        //	Mã nhân viên				
        public string psn_no { get; set; }
        //	Quyết toán cơ sở y dượcID				
        public string medinsSetlId { get; set; }
        //	Tổng chi phí y tế				
        public string medfee_sumamt { get; set; }
        //	Tổng số tiền quỹ chi trả	
        public string fund_pay_sumamt { get; set; }
        //	Chi từ tài khoản cá nhân	
        public string acct_pay { get; set; }
    }
}
