using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    public class PacsReportSqdhValueVo
    {
        public string OrganizeId { get; set; }
        public string xm { get; set; }
        //public string xb { get; set; }
        public string lissqdh { get; set; }
        public string sqdh { get; set; }
        //public string xmCode { get; set; }
        //public string xmmc { get; set; }
        public string ztId { get; set; }
        public string ztmc { get; set; }
        public string sqsj { get; set; }
        //public decimal zje { get; set; }
        /// <summary>
        /// Trạng thái đăng ký 0:Đã gửi đơn 1:đãnhậnthu 2:đãHoàn thành
        /// </summary>
        public string syncStatus { get; set; }
    }
}
