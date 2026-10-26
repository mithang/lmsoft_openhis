
using Newtouch.HIS.Domain.DTO;
using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tra cứu thu phí đăng ký khám ngoại trú
    /// </summary>
    public class OutPatientRegChargeMVO : OutpatientSettYbFeeRelatedDTO
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
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// Nhân viên lập hồ sơ
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Tên bác sĩ đăng ký khám(Có thể là cập nhật sau)
        /// </summary>
        public string ghysmc { get; set; }
        /// <summary>
        /// Tên khoa đăng ký khám
        /// </summary>
        public string ghksmc { get; set; }
        /// <summary>
        /// Số hóa đơn cũ
        /// </summary>
        public string oldfph { get; set; }
        /// <summary>
        /// isxfph
        /// </summary>
        public string isxfph { get; set; }
        /// <summary>
        /// tCreatorCode
        /// </summary>
        public string tCreatorCode { get; set; }

        /// <summary>
        /// Tên nhân viên thu phí
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// Loại quyết toán
        /// </summary>
        public string jslx { get; set; }

        #endregion

        /// <summary>
        /// Tổng số tiền thu phí
        /// </summary>
        public decimal jszje { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public decimal? jsxjzf { get; set; }

        /// <summary>
        /// Ngày thu phí
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// Số ngoại trú của lần đăng ký khám tương ứng với quyết toán
        /// </summary>
        public string mzh { get; set; }
        /// <summary>
        /// Số quyết toán BHYT
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Số BHXH
        /// </summary>
        public string sbbh { get; set; }

        /// <summary>
        /// Phương thức thanh toán phần tiền mặt
        /// </summary>
        public string zffsmcstr { get; set; }
        public string xb { get; set; }
        public string zjh { get; set; }
        public string phone { get; set; }
        public DateTime? jzsj { get; set; }
        public string jzys { get; set; }
        public string zdmc { get; set; }
        public decimal? jsjz { get; set; }
        public string zxlsh { get; set; }
        public string sfyb { get; set; }
    }

    /// <summary>
    /// Phương thức thanh toán quyết toán
    /// </summary>
    public class SettZffsResult
    {
        public int jsnm { get; set; }
        public string xjzffs { get; set; }
        public string xjzffsmc { get; set; }
    }

    public class RoleUnionUser
    {
        public string First { get; set; }
        public string Second { get; set; }
    }
}
