using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Nhập việnThông tin bệnh nhân
    /// </summary>
    public class InHosPatientVO
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tổ chứckếtcấu trúcid
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Số nhập viện
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
        /// rasinhnămTháng
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
