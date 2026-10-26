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
        /// ID tổ chức(có具体 nghiệp vụ của bệnh viện)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Bệnh nhân nhất 近của性质xt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// bản viện 磁số thẻ(bao gồmBHYT nghỉ hưu bệnh nhân),hoặcBHYTsố thẻ
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
