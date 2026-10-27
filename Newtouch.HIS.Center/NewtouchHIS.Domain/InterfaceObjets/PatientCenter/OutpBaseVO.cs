using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.PatientCenter
{
    /// <summary>
    /// 门诊Bệnh nhân基本信息
    /// </summary>
    public class OutpBaseVO
    {
        /// <summary>
        /// Mã tổ chức
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// Bệnh nhânId
        /// </summary>
        public string? patid { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
