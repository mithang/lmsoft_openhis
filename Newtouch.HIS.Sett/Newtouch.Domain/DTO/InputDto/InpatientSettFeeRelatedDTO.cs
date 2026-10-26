using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Liên quan thanh toán số tiền quyết toán xuất viện
    /// </summary>
    public class InpatientSettFeeRelatedDTO
    {
        /// <summary>
        /// Tổng số tiền quyết toán(dj*slTổng)
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// Khoản phải thu thanh toán ban đầu
        /// </summary>
        public decimal? orglxjzfys { get; set; }

        /// <summary>
        /// Khoản phải thu thanh toán(Khoản phải thu thanh toán ban đầu sau chiết khấu)
        /// </summary>
        public decimal? xjzfys { get; set; }
        /// <summary>
        /// Số tiền thực thu
        /// </summary>
        public decimal? ssk { get; set; }
        public string zffs1 { get; set; }
        public decimal? zfje1 { get; set; }
        public string zffs2 { get; set; }
        public decimal? zfje2 { get; set; }
        /// <summary>
        /// Tiền thối lại
        /// </summary>
        public decimal? zhaoling { get; set; }
        /// <summary>
        /// Số giao dịch khấu trừ,Khấu trừ tài khoản BHYT Trùng Khánh
        /// </summary>
        public string dyjylsh { get; set; }
        public string ver { get; set; }
        /// <summary>
        /// Số giao dịch BHYT
        /// </summary>
        public string ybjslsh { get; set; }

        #region new
        /// <summary>
        /// Số tiền thanh toán tạm ứng
        /// </summary>
        public decimal? yjjzfje { get; set; }
        /// <summary>
        /// Số dư tạm ứng có thể trả lại
        /// </summary>
        public decimal? yjjtye { get; set; }
        /// <summary>
        /// Số tiền thực thu tại cửa sổ
        /// </summary>
        public decimal? djjess { get; set; }
        /// <summary>
        /// Phương thức thanh toán thực thu tại cửa sổ
        /// </summary>
        public string djjesszffs { get; set; }

        /// <summary>
        /// Tỷ lệ chiết khấu
        /// </summary>
        public decimal? zkbl { get; set; }
        public decimal jmje { get; set; }

        /// <summary>
        /// Mã chứng từ điện tử
        /// </summary>
        public string ecToken { get; set; }

        public List<PatZfList> patZflist { get; set; }
        public decimal? xjwc { get; set; }
        #endregion
    }

}
