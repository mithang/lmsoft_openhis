using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Quý An Đọc thẻ BHYT Thông tin thẻ
    /// </summary>
    public class GACardReadInfoDTO
    {
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string prm_aac001 { get; set; }

        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        public string prm_aac002 { get; set; }

        /// <summary>
        /// Mã trung tâm chi nhánh
        /// </summary>
        public string prm_yab003 { get; set; }

		/// <summary>
		/// ////////////////////////Trường BHYT Trùng Khánh sử dụng
		/// </summary>
		public string sfzh { get; set; }
		public string kh { get; set; }
		public string cblb { get; set; }
    }
}
