using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// Truy vấn phương thức nhập/xuất kho
    /// </summary>
    public class MedicineStorageIOModeQueryRequest : RequestBase
    {
        /// <summary>
        /// 0: Nhập kho; 1: Xuất kho
        /// </summary>
        [Required]
        public string crkbz { get; set; }

    }
}
