using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class PatientIndexVO
    {
        /// <summary>
        /// Bệnh nhânMã định danh duy nhất
        /// </summary>
        public int? PatId { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string? PatName { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? Jzkh { get; set; }
        /// <summary>
        /// CMND
        /// </summary>
        public string? IdCard { get; set; }
        /// <summary>
        /// Tính chất bệnh nhân BHYT/Tự chi trả
        /// </summary>
        public string? Brxz { get; set; }

    }
}
