using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class InpWardPatTreeVO
    {
        public string OrganizeId { get; set; }
        public string bqCode { get; set; }
        public string bqmc { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string hzxm { get; set; }

        public string sex { get; set; }

        public string nl { get; set; }

        /// <summary>
        /// giường ký tựsố
        /// </summary>
        public string BedNo { get; set; }
        public string rqrq { get; set; }
        public string cqrq { get; set; }
        public DateTime ryrq { get; set; }
        public DateTime birth { get; set; }
        public string inHosDays  { get; set; }
        public string brxzmc { get; set; }
    }
}
