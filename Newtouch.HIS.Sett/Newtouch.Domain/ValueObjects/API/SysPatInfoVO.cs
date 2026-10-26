using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysPatInfoVO
    {
        public int patid { get; set; }
        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tính chất gần nhất của bệnh nhânxt_brxz.brxz
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Số thẻ từ của bệnh viện(Bao gồm bệnh nhân BHYT nghỉ hưu),Hoặc số thẻ BHYT
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
        public string dz { get; set; }

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
