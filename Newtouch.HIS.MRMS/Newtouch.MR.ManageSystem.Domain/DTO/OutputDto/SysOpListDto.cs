using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.DTO.OutputDto
{
    /// <summary>
    /// Hệ thốngPhẫu thuậtTừ điển
    /// </summary>
    public class SysOpListDto
    {
        /// <summary>
        /// Phẫu thuậtMã
        /// </summary>
        public string ssdm { get; set; }
        /// <summary>
        /// Tên phẫu thuật
        /// </summary>
        public string ssmc { get; set; }
        /// <summary>
        /// Phẫu thuậtMã ghi nhớ
        /// </summary>
        public string zjm { get; set; }
        /// <summary>
        /// Cấp độ phẫu thuật
        /// </summary>
        public string ssjb { get; set; }
    }
}
