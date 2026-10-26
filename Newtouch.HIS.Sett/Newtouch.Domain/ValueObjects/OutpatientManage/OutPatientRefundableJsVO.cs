using System;
using Newtouch.HIS.Domain.DTO;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Hoàn phí ngoại trú Quyết toán Bản ghi chính Query
    /// </summary>
    public class OutPatientRefundableJsVO : OutpatientSettYbFeeRelatedDTO
    {
        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Ngày thu phí
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// Nhân viên quyết toán
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian quyết toán
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Tên nhân viên quyết toán
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// Đăng ký khám ngoại trú hoặc Ghi sổ thu phí ngoại trú
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Quyết toán BHYT Số quyết toán BHYT
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal jszje { get; set; }

        /// <summary>
        /// Thanh toán quyết toán
        /// </summary>
        public decimal jsxjzf { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }

    }

    /// <summary>
    /// Thông tin hoàn phí
    /// </summary>
    public class OutPatientRefundableGuiAnJsVO
    {
        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// Ngày thu phí
        /// </summary>
        public DateTime? sfrq { get; set; }

        /// <summary>
        /// Nhân viên quyết toán
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian quyết toán
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Tên nhân viên quyết toán
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// Đăng ký khám ngoại trú hoặc Ghi sổ thu phí ngoại trú
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Quyết toán BHYT Số quyết toán BHYT
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal jszje { get; set; }

        /// <summary>
        /// Thanh toán quyết toán
        /// </summary>
        public decimal jsxjzf { get; set; }

        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }
        /// <summary>
        /// Số khám bệnh
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// Mã trung tâm chi nhánh
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// Số quyết toán
        /// </summary>
        public string prm_yka103 { get; set; }
        /// <summary>
        /// Mã nhân viên xử lý
        /// </summary>
        public string prm_aae011 { get; set; }
        /// <summary>
        /// Tên người xử lý
        /// </summary>
        public string prm_ykc141 { get; set; }
        /// <summary>
        /// Thời gian xử lý
        /// </summary>
        public string prm_aae036 { get; set; }
        /// <summary>
        /// Lý do hoàn phí
        /// </summary>
        public string prm_aae013 { get; set; }
        /// <summary>
        /// Phương pháp BHXH
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string prm_aac001 { get; set; }

        /// <summary>
        /// Số thứ tự bồi thường ngoại trú
        /// </summary>
        public string outpId { get; set; }
    }
}
