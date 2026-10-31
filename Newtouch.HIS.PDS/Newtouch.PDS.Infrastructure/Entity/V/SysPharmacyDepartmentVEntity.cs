using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bộ phận nhà thuốc
    /// </summary>
    [Table("V_S_xt_yfbm")]
    [SugarTable("V_S_xt_yfbm")]
    public class SysPharmacyDepartmentVEntity : IEntity<SysPharmacyDepartmentVEntity>
    {
        /// <summary>
        /// Bộ phận nhà thuốcID
        /// </summary>
        public int yfbmId { get; set; }

        /// <summary>
        /// Tên bộ phận nhà thuốc
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Mã bộ phận nhà thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// Cấp bộ phận dược
        /// </summary>
        public byte yjbmjb { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Trạng thái 0:Không hiệu lực  1:Hiệu lực
        /// </summary>
        public string zt { get; set; }

    }
}
