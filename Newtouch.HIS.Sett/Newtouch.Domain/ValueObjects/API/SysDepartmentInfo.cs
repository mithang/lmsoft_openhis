using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysDepartmentInfo
    {
        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Nhưng là mã số
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// Cờ ngoại trú/nội trú
        /// </summary>
        public byte mzzybz { get; set; }

    }
}
