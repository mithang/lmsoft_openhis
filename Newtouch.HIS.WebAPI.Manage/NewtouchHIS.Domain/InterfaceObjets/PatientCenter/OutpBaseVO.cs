using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.PatientCenter
{
    /// <summary>
    /// thông tin cơ bản bệnh nhân ngoại trú
    /// </summary>
    public class OutpBaseVO
    {
        /// <summary>
        /// mã cơ quan
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// bệnh nhânId
        /// </summary>
        public string? patid { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
