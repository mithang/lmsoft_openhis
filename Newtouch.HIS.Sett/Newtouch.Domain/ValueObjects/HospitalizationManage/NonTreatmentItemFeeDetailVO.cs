using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class NonTreatmentItemFeeDetailVO
    {
        /// <summary>
        /// Có phải là thuốc hay không 0:không phải 1:là
        /// </summary>
        public string isYP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fzlxmjfbId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? tsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
