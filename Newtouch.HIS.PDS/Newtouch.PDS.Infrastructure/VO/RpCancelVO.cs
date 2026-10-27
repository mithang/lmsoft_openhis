namespace Newtouch.HIS.Domain.VO
{

    /// <summary>
    /// Hủy thông tin xếp thuốc
    /// </summary>
    public class RpCancelVo
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get;set;}

        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }
    }
}
