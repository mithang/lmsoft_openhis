using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Được gửi từ trang
    /// </summary>
    public class PrescriptionHtmlDTO
    {
        /// <summary>
        /// Loại đơn thuốc
        /// </summary>
        public string cflx { get; set; }

        /// <summary>
        /// Thông tin đơn thuốc
        /// </summary>
        public List<PrescriptionHtmlVO> cfHtml;
    }
}
