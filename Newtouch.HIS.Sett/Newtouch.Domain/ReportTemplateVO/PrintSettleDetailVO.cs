using System;

namespace Newtouch.HIS.Domain.ReportTemplateVO
{
    public class PrintSettleDetailVO
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

        ///// <summary>
        ///// Ngày bắt đầu quyết toán
        ///// </summary>
        //public string jsKsrq { get; set; }

        ///// <summary>
        ///// Ngày kết thúc quyết toán
        ///// </summary>
        //public string jsJsrq { get; set; }

        /// <summary>
        /// Số ngày khám chữa bệnh
        /// </summary>
        public string ZLDate { get; set; }

    }
}
