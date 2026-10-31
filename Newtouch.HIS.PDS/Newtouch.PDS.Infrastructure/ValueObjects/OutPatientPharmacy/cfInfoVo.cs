using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin đơn thuốc
    /// </summary>
    public class cfInfoVo
    {
        /// <summary>
        /// Thời gian thu phí(Dùng để hiển thị)
        /// </summary>
        public string ShowSfsj { get; set; }

        /// <summary>
        /// Khóa chínhID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public long cfnm { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Số đơn thuốc đầy đủ
        /// </summary>
        public string cfhComplete { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal? Zje { get; set; }

        /// <summary>
        /// Mã nội bộ thanh toán
        /// </summary>
        public long jsnm { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string Fph { get; set; }

        /// <summary>
        /// Số hóa đơn đầy đủ
        /// </summary>
        public string FphComplete { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int? nl { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Số tiền đơn thuốc
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// Nhà thuốc nhận thuốc
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 0:Chưa xếp;1:Đã xếp;2:Đã phát;3:Đã trả
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Thời gian phát thuốc
        /// </summary>
        public DateTime? fysj { get; set; }

    }
}
