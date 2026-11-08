using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// trang mặt Gửi qua đến của
    /// </summary>
    public class PrescriptionHtmlDTO
    {
        /// <summary>
        /// Đơn thuốc Loại
        /// </summary>
        public string cflx { get; set; }

        /// <summary>
        /// Đơn thuốc thông tin
        /// </summary>
        public List<PrescriptionHtmlVO> cfHtml;
    }
}
