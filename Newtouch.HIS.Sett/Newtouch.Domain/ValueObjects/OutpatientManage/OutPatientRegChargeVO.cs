
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tra cứu thu phí đăng ký khám ngoại trú
    /// </summary>
    public class OutPatientRegChargeVO
    {
        #region 挂号收费查询的主记录

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }
        /// <summary>
        /// Tra cứu mã nội bộ quyết toán
        /// </summary>
        public int cxjsnm { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Mã nội bộ đăng ký khám
        /// </summary>
        public int ghnm { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// Nhân viên lập hồ sơ
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string rymc { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// Tên bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Có trả lại hay không
        /// </summary>
        public string isreturned { get; set; }
        /// <summary>
        /// Số hóa đơn cũ
        /// </summary>
        public string oldfph { get; set; }
        /// <summary>
        /// isxfph
        /// </summary>
        public string isxfph { get; set; }
        /// <summary>
        /// Cờ quyết toán
        /// </summary>
        public string jsbz { get; set; }
        /// <summary>
        /// tCreatorCode
        /// </summary>
        public string tCreatorCode { get; set; }

        #endregion

    }
}
