namespace Newtouch.HIS.Domain.VO
{

    /// <summary>
    /// hủy xếp thuốc thông tin
    /// </summary>
    public class RpCancelVo
    {
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get;set;}

        /// <summary>
        /// tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// bộ phận nhà thuốc mã
        /// </summary>
        public string yfbmCode { get; set; }
    }
}
