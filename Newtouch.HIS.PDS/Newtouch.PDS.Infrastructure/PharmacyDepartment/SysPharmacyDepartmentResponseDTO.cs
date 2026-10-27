using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// Bộ phận nhà thuốc
    /// </summary>
    public class SysPharmacyDepartmentResponseDto 
    {
        /// <summary>
        /// Id
        /// </summary>
        public int yfbmId { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Mã
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tổ chức
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
        /// Khoa liên kết
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public string zt { get; set; }
    }
}