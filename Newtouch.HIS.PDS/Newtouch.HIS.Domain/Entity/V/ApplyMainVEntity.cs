using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// phiếu lĩnh chính thông tin
    /// </summary>
    public class ApplyMainVEntity
    {

        /// <summary>
        /// phiếu lĩnh ID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// số phiếu lĩnh
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// trạng thái cấp phát
        /// </summary>
        public int ffzt { get; set; }

        /// <summary>
        /// bộ phận lĩnh tên
        /// </summary>
        public string slbmmc { get; set; }

        /// <summary>
        /// bộ phận xuất kho tên
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// thời gian đề nghị
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người đề nghị
        /// </summary>
        public string CreatorCode { get; set; }
        
        /// <summary>
        /// thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
