
namespace Newtouch.HIS.Domain.ValueObjects.OutpatientManage
{
    /// <summary>
    /// Chi tiết chi phí loại lớn do thuật toán tính phí bệnh nhân tính ra
    /// </summary>
    public class PatientChargeAlgorythm_Category_FeesDetailVO
    {
        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Mã chi tiết(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// Hạng mục thu phí(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// Mã nội bộ chi tiết đơn thuốc(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// Số nhóm(Tạm thời chỉ dùng cho Bảo hiểm nông thôn mới)
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// Tự lo
        /// </summary>
        public decimal zl { get; set; }

        /// <summary>
        /// Chi phí được ghi sổ
        /// </summary>
        public decimal jzfy { get; set; }

        /// <summary>
        /// Tiền mặt
        /// </summary>
        public decimal xj { get; set; }

        /// <summary>
        /// Số tiền giảm miễn
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// Tự chi trả theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzf { get; set; }

        /// <summary>
        /// Tự lo theo thuật toán(Số tiền tự chi trả tính được qua thuật toán)
        /// </summary>
        public decimal sfzl { get; set; }

        /// <summary>
        /// Tổng chi phí(Tiền mặt bệnh nhân tự chi trả cần phải trả)
        /// </summary>
        public decimal total { get; set; }

        /// <summary>
        /// Số bảng tính phí hạng mục nội trú
        /// </summary>
        public int xmjfbbh { get; set; }

        /// <summary>
        /// Số bảng tính phí thuốc nội trú
        /// </summary>
        public int ypjfbbh { get; set; }

        /// <summary>
        /// Số tiền giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// Số tiền trong phạm vi giao dịch BHYT(Dữ liệu thu được sau khi tính toán)
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// Loại y lệnh1 Thuốc 2 Hạng mục
        /// </summary>
        public string yzlx { get; set; }
    }
}
