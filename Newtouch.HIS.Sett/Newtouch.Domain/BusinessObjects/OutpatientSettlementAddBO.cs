using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Thêm quyết toán mới Cần thiết
    /// </summary>
    public class OutpatientSettlementAddBO
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<int> cfnmList { get; set; }
        /// <summary>
        /// Khi hạng mục chưa liên kết đơn thuốc,Khóa chính hạng mục ngoại trúxmnm List
        /// </summary>
        public IList<int> xmnmList { get; set; }
        /// <summary>
        /// (Phát sinh quyết toán mới)Có phải các ngày thu phí khác nhau tạo thành một lần quyết toán hay không Mặc địnhfalse
        /// </summary>
        public bool sfrqWithSameJs { get; set; }
        /// <summary>
        /// (Thu phí,Làm một lần quyết toán)Chỉ định ngày thu phí duy nhất
        /// </summary>
        public DateTime? settSfrq { get; set; }
        /// <summary>
        /// Tự động tạo kế hoạch ngoại trú
        /// </summary>
        public bool autoGenePlan { get; set; }
        /// <summary>
        /// Có phải là quyết toán trước khi nợ phí hay không
        /// </summary>
        public bool isQfyj { get; set; }
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

    }
}
