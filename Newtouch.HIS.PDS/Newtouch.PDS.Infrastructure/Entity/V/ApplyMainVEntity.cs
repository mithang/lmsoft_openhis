using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// Thông tin chính đơn lĩnh
    /// </summary>
    public class ApplyMainVEntity
    {

        /// <summary>
        /// Đơn lĩnhID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// Số đơn lĩnh
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// Trạng thái phát
        /// </summary>
        public int ffzt { get; set; }

        /// <summary>
        /// Tên bộ phận lĩnh
        /// </summary>
        public string slbmmc { get; set; }

        /// <summary>
        /// Tên bộ phận xuất kho
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// Thời gian xin
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người xin
        /// </summary>
        public string CreatorCode { get; set; }
        
        /// <summary>
        /// Thời gian sửa đổi
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
