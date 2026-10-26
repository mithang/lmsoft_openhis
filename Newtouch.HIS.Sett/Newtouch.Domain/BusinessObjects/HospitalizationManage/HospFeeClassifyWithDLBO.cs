using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Chi phí nội trú theo Loại lớn Phân loại
    /// </summary>
    public class HospFeeClassifyWithDLBO
    {
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Tổng số tiền quyết toán loại lớn(Chưa trừ phần giảm miễn)
        /// </summary>
        public decimal jsje { get; set; }

        /// <summary>
        /// Danh sách tính phí hạng mục
        /// </summary>
        public IList<HospItemFeeDetailVO> ItemFeeList { get; set; }

        /// <summary>
        /// Danh sách tính phí thuốc
        /// </summary>
        public IList<HospMedicinFeeDetailVO> MedicinFeeList { get; set; }



    }

}
