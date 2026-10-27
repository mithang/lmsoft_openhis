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
        /// Cửa sổ nhà thuốcId
        /// </summary>
        public int yfckId { get; set; }

        /// <summary>
        /// Mã cửa sổ nhà thuốc
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// Tên cửa sổ nhà thuốc
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sửa người dùng
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Tổ chức cấp cao nhấtId
        /// </summary>
        public string TopOrganizeId { get; set; }
    }
}
