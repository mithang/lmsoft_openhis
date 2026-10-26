using Newtouch.HIS.Domain.ValueObjects;
using System;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// Quyết toán nội trú Bệnh nhân Thu phí theo phân loại Xem trước
    /// </summary>
    public class HosSettPatClassifyChargePreviewDto
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        ///// <summary>
        ///// Số thẻ
        ///// </summary>
        //public string kh { get; set; }
        /// <summary>
        /// Ghi sổ
        /// </summary>
        public decimal jzfy { get; set; }
        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzf { get; set; }
        /// <summary>
        /// Tự chi trả
        /// </summary>
        public decimal zifei { get; set; }
        /// <summary>
        /// Tự chi trả
        /// </summary>
        public decimal zifu { get; set; }
        /// <summary>
        /// Số tiền giảm miễn(Khác)
        /// </summary>
        public decimal jmje { get; set; }
        /// <summary>
        /// Khoản phải thu
        /// </summary>
        public decimal yingshoukuan { get; set; }
        /// <summary>
        /// Tiền thối lại
        /// </summary>
        public decimal zhaoling { get; set; }
        /// <summary>
        /// Chênh lệch làm tròn
        /// </summary>
        public decimal srce { get; set; }
        /// <summary>
        /// Số tiền thực thu
        /// </summary>
        public decimal ssk { get; set; }

        /// <summary>
        /// Số dư tài khoản(Số tiền thực thu)(Hạng mục chưa khấu trừ/Tính phí thuốc)
        /// </summary>
        public decimal zhye { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Ngày hóa đơn
        /// </summary>
        public string fprq { get; set; }

        /// <summary>
        /// Thông tin cơ bản bệnh nhân nội trú
        /// </summary>
        public HospSettPatInfoVO HospSettPatInfo { get; set; }

        /// <summary>
        /// Số tiền quyết toán
        /// </summary>
        public decimal expectedjsje { get; set; }

        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime expectedcyrq { get; set; }

    }
}
