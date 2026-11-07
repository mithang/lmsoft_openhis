using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// thông qua API hơn Đơn thuốc mớicủa Quay lạikếtnếu
    /// </summary>
    public class PresUpdateResultDTO
    {
        /// <summary>
        /// Danh sách số đơn thuốc đã quyết toán (những đơn đã quyết toán còn cần cập nhật)
        /// </summary>
        public IList<string> settledCfhList { get; set; }
    }
}
