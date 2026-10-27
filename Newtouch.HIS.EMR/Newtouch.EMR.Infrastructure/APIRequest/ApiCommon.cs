using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.API.Common
{
    /// <summary>
    /// Minimal port of the API request base classes the EMR APIRequest DTOs
    /// originally inherited from Newtouch.HIS.API.Common (binary, not in the
    /// EMR source set). Only the shape the EMR web/API code relies on is
    /// reproduced: an AppId + OrganizeId + Timestamp request envelope.
    /// The full vendor API framework (signing, DefaultResponse handling) is
    /// NOT part of this classlib; the EMR web agent owns that surface.
    /// </summary>
    public class RequestBase
    {
        [Required(ErrorMessage = "AppId là bắt buộc")]
        public string AppId { get; set; }

        public string OrganizeId { get; set; }

        [Required(ErrorMessage = "Timestamp là bắt buộc")]
        public DateTime Timestamp { get; set; }
    }

    public class JSONRequestBase : RequestBase
    {
    }
}
