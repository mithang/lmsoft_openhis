namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    /// <summary>
    /// Thông tin thuốc đơn thuốc
    /// </summary>
    public class YP_XT_OP_PYListDTO
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// Số lượng Số lượng bảng quyết toán
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// Số lẻ đã tách
        /// </summary>
        public int cls { get; set; }
    }
}
