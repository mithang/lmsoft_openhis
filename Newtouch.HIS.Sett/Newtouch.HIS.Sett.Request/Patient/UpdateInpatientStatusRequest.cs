using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// Cập nhậtNhập việnBệnh nhânDấu hiệu đang nằm viện
    /// </summary>
    public class UpdateInpatientStatusRequest : RequestBase
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        [Required]
        public string zyh { get; set; }

        /// <summary>
        /// Dấu hiệu đang nằm viện
        /// </summary>
        [Required]
        public string zybz { get; set; }
    }
}
