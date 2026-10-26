using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// bộ phận nhà thuốc
    /// </summary>
    [Table("V_S_xt_yfbm")]
    public class SysPharmacyDepartmentVEntity : IEntity<SysPharmacyDepartmentVEntity>
    {
        /// <summary>
        /// bộ phận nhà thuốc ID
        /// </summary>
        public int yfbmId { get; set; }

        /// <summary>
        /// tên bộ phận nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// tổ chức mã
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// cấp bộ phận dược
        /// </summary>
        public byte yjbmjb { get; set; }

        /// <summary>
        /// khoa mã
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// trạng thái 0: không hiệu lực  1: còn hiệu lực
        /// </summary>
        public string zt { get; set; }

    }
}
