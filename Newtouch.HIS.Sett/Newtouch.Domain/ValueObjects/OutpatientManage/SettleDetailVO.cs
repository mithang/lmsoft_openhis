
using System;

namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    public class SettleDetailVO
    {
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string Patient_Name { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Số in danh sách
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string MedicalRecordNo { get; set; }

        ///// <summary>
        ///// Số ngày khám chữa bệnh
        ///// </summary>
        //public string ZLDate { get; set; }

        /// <summary>
        /// In -năm
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// In-tháng
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// In-Ngày
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Tổng chi phí
        /// </summary>
        public string SummOfCharges { get; set; }

        /// <summary>
        /// Ngày bắt đầu quyết toán
        /// </summary>
        public DateTime jsKsrq { get; set; }

        /// <summary>
        /// Ngày kết thúc quyết toán
        /// </summary>
        public DateTime jsJsrq { get; set; }

        /// <summary>
        /// Số ngày khám chữa bệnh
        /// </summary>
        public string ZLDate
        {
            get
            {
                decimal ksDate = 0m;
                decimal jsDate = 0m;

                TimeSpan ks = jsKsrq - jsKsrq.Date;
                TimeSpan js = jsJsrq - jsJsrq.Date;
                TimeSpan durations = jsJsrq.Date - jsKsrq.Date;

                if (ks.TotalDays - Math.Truncate(ks.TotalDays) > 0.5)
                    ksDate = 1.0m;
                else
                    ksDate = 0.5m;

                if (js.TotalDays - Math.Truncate(js.TotalDays) > 0.5)
                    jsDate = 1.0m;
                else
                    jsDate = 0.5m;

                if (ksDate == jsDate && jsDate == 0.5m || ksDate < jsDate)
                {
                    return (ksDate + Convert.ToDecimal(Math.Truncate(durations.TotalDays)) - 1 + jsDate).ToString("0.0");
                }
                else
                {
                    return (ksDate + Convert.ToDecimal(Math.Truncate(durations.TotalDays)) - 2 + jsDate).ToString("0.0");
                }
            }
        }

    }
}
