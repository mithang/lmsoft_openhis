using Newtouch.HIS.API.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Therapist
{
    /// <summary>
    /// Cập nhật trạng thái chuyển đổi của đề xuất
    /// </summary>
    public class UpdateSuggestionCvStatusRequest : OrgRequestBase
    {
        /// <summary>
        /// Danh sách
        /// </summary>
        [Required]
        public IList<SuggestionCvStatuDTO> CvList { get; set; }

    }
}
