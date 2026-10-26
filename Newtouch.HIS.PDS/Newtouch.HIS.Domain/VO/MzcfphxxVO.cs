namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// ngoại trú đơn thuốc xếp thuốc thông tin
    /// </summary>
    public class MzcfphxxVO
    {
        /// <summary>
        /// số lượng đơn vị nhỏ nhất
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// lô
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// số lô
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

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