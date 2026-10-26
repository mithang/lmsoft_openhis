using Newtouch.HIS.API.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Therapist
{
    /// <summary>
    /// Cập nhật đ ề xuất của chuyển 换trạng thái
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
