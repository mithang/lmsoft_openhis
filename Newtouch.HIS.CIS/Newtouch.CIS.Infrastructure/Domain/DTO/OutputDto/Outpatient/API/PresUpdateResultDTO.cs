using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// thông quaAPIhơnĐơn thuốc mớicủaQuay lạikếtnếu
    /// </summary>
    public class PresUpdateResultDTO
    {
        /// <summary>
        /// đãkếtĐơn thuốc Số đơn thuốc List（哪些đãkếtđã cònyêu cầuCập nhật）
        /// </summary>
        public IList<string> settledCfhList { get; set; }
    }
}
