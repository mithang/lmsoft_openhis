using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// Thông quaAPIKết quả trả về khi cập nhật đơn thuốc
    /// </summary>
    public class PresUpdateResultDTO
    {
        /// <summary>
        /// Đơn thuốc đã quyết toán Số đơn thuốc List(Những cái nào đã quyết toán Vẫn yêu cầu cập nhật)
        /// </summary>
        public IList<string> settledCfhList { get; set; }
    }
}
