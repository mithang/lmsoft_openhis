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
        /// định danh duy nhất danh tính bệnh nhân
        /// </summary>
        public int? PatId { get; set; }
        /// <summary>
        /// họ tên
        /// </summary>
        public string? PatName { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? Jzkh { get; set; }
        /// <summary>
        /// chứng minh nhân dân
        /// </summary>
        public string? IdCard { get; set; }
        /// <summary>
        /// tính chất bệnh nhân bảo hiểm y tế/tự trả
        /// </summary>
        public string? Brxz { get; set; }

    }
}
