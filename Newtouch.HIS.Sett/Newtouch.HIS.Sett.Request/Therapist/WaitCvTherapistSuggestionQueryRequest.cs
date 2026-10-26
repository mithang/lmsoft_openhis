using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Therapist
{
    /// <summary>
    /// Tra cứu đề xuất điều trị chuyển đổi đang chờ
    /// </summary>
    public class TherapistSuggestionQueryRequest : OrgRequestBase
    {
        /// <summary>
        /// Loại bệnh nhân 1Ngoại trú 2Nhập viện
        /// </summary>
        [Required]
        public string brlx { get; set; }

        /// <summary>
        /// Ngoại trú số nhập viện
        /// </summary>
        [Required]
        public string mzzyh { get; set; }

        /// <summary>
        /// Lọc thời gian bắt đầu lập
        /// </summary>
        [Required]
        public DateTime? startTime { get; set; }
    }
}
