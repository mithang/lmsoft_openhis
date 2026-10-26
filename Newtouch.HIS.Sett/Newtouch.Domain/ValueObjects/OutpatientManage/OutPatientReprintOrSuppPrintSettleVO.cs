
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// In lại/In bổ sung hóa đơn(mz_js Đối tượng)
    /// </summary>
    public class OutPatientReprintOrSuppPrintSettleVO
    {
        public int jsnm { get; set; }
        public string xm { get; set; }
        public string kh { get; set; }
        public string xfph { get; set; }
        public string rymc { get; set; }
        public DateTime CreateTime { get; set; }  //Ngày quyết toán
        public decimal zje { get; set; }

    }
}
