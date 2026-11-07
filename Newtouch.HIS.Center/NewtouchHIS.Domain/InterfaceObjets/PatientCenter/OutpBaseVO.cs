using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.PatientCenter
{
    /// <summary>
    /// Ngoại trú Bệnh nhân Thông tin cơ bản
    /// </summary>
    public class OutpBaseVO
    {
        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Bệnh nhân Id
        /// </summary>
        public string? patid { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
