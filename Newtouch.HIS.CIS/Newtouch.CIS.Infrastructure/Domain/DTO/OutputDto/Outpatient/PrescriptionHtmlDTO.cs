using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// trangmặtGửiquađếncủa
    /// </summary>
    public class PrescriptionHtmlDTO
    {
        /// <summary>
        /// Đơn thuốcLoại
        /// </summary>
        public string cflx { get; set; }

        /// <summary>
        /// Đơn thuốcthông tin
        /// </summary>
        public List<PrescriptionHtmlVO> cfHtml;
    }
}
