using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class InpatientRefundJsonVO
    {
        /// <summary>
        /// Có phải là thuốc hay không 0:không phải 1:là
        /// </summary>
        public string isYp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jfbbh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? tsl { get; set; }
        /// <summary>
        /// Mã tính phí của bộ
        /// </summary>
        public string jfbbhs { get; set; }
    }
}
