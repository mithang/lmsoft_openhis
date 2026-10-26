using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Therapist
{
    /// <summary>
    /// chờ chuyển 换 điều trị đ ề xuất tra cứu
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
        /// 开立của起始thời gian lọc
        /// </summary>
        [Required]
        public DateTime? startTime { get; set; }
    }
}
