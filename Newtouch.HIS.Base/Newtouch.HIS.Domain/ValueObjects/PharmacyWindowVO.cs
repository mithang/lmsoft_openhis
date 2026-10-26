using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class PharmacyWindowVO
    {
        /// <summary>
        /// Id cửa sổ phòng dược
        /// </summary>
        public int yfckId { get; set; }

        /// <summary>
        /// Mã cửa sổ phòng dược
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// Tên cửa sổ phòng dược
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người dùng sửa đổi
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Id tổ chức cấp cao nhất
        /// </summary>
        public string TopOrganizeId { get; set; }
    }
}
