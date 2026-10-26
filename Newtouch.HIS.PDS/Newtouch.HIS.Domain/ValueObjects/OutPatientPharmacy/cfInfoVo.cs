using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// thông tin đơn thuốc
    /// </summary>
    public class cfInfoVo
    {
        /// <summary>
        /// thời gian thu phí (展示用)
        /// </summary>
        public string ShowSfsj { get; set; }

        /// <summary>
        /// khóa chính ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 完整处方号
        /// </summary>
        public string cfhComplete { get; set; }

        /// <summary>
        /// tổng số tiền
        /// </summary>
        public decimal? Zje { get; set; }

        /// <summary>
        /// quyết toán trong mã
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// 完整发票号
        /// </summary>
        public string FphComplete { get; set; }

        /// <summary>
        /// tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// đơn thuốc số tiền
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// nhà thuốc lĩnh thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 0: chưa xếp; 1: đã xếp; 2: đã phát; 3: đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// tổ chức Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// thời gian phát thuốc
        /// </summary>
        public DateTime? fysj { get; set; }

    }
}
