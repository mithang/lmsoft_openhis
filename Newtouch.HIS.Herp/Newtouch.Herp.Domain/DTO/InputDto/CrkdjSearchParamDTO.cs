using System;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// Điều kiện tra cứu chứng từ nhập/xuất kho
    /// </summary>
    public class CrkdjSearchParamDTO
    {
        /// <summary>
        /// Trạng thái duyệt
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Thời gian bắt đầu
        /// </summary>
        public DateTime kssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc
        /// </summary>
        public DateTime jssj { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Tổ chức
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// Số phiếu
        /// </summary>
        public string pdh { get; set; }

        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public string djlx { get; set; }

        /// <summary>
        /// Số phiếu giao hàng
        /// </summary>
        public string deliveryNo { get; set; }
    }
}
