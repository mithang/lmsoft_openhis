using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Thông tin bệnh nhân nội trú
    /// </summary>
    public class InHosPatientVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Cơ cấu tổ chứcid
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime birth { get; set; }

        /// <summary>
        /// Số giường
        /// </summary>
        public string BedCode { get; set; }

        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime ryrq { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        public string cardno { get; set; }
    }
}
