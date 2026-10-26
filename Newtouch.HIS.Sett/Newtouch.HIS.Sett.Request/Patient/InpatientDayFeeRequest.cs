using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Patient
{
    public class InpatientDayFeeRequest : BookingReqBaseDto
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Thời gian bắt đ ầu
        /// </summary>
        public DateTime kssj { get; set; }
        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime jssj { get; set; }
        /// <summary>
        /// Số dòng hiển thị mỗi trang
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// Số trang hiện tại
        /// </summary>
        public int pageNum { get; set; }
    }
}
