using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Sett.Request.Booking.Response
{
    public class SysPatInfoResp
    {
        public int patid { get; set; }
        /// <summary>
        /// ID tổ chức (có nghiệp vụ cụ thể của bệnh viện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tính chất gần nhất của bệnh nhân xt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// số thẻ từ của bệnh viện (bao gồm bệnh nhân BHYT nghỉ hưu), hoặc số thẻ BHYT
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        public string xb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dwdz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dh { get; set; }
        public string kh { get; set; }
        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string klx { get; set; }
    }
}
