using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Tra cứu bệnh nhân nhập viện
    /// </summary>
    public class InPatientQueryRequest : RequestBase
    {
        /// <summary>
        /// nhất sau cập nhật thời gian
        /// </summary>
        public DateTime? lastUpdateTime { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Khu bệnh mã 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// Dấu hiệu đang nằm viện (nhiều dùng dấu phẩy tiếng Anh phân tách)
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// Từ khóa có thể khớp zyh blh xm
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// Phân trang
        /// </summary>
        public Pagination pagination { get; set; }

    }
}