using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// Cập nhật tình trạng sử dụng giường bệnh
    /// </summary>
    public class UpdateBedOccupyRequest : RequestBase
    {
        /// <summary>
        /// Mã giường bệnh
        /// </summary>
        [Required]
        public string cwCode { get; set; }

        /// <summary>
        /// Có đang sử dụng hay không
        /// </summary>
        [Required]
        public bool isOccupy { get; set; }

    }
}

