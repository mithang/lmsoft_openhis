using System.ComponentModel.DataAnnotations;
using System;

namespace Newtouch.PDS.Requset.Hospitalizatiion
{
    /// <summary>
    /// Trả thuốc
    /// </summary>
    public class DrugRerurnRequest
    {

        /// <summary>
        /// Y lệnh ID
        /// </summary>
        [Required]
        public string yzId { get; set; }

        /// <summary>
        /// Thuốc code
        /// </summary>
        [Required]
        public string ypCode { get; set; }

        /// <summary>
        /// Người xin trả thuốc
        /// </summary>
        [Required]
        public string tysqr { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        [Required]
        public string zh { get; set; }

        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        [Required]
        public DateTime? zxrq { get; set; }
    }
}
