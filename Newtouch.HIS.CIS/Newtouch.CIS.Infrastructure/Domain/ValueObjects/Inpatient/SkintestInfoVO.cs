using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class SkintestInfoVO
    {
        /// <summary>
        /// Mã hạng mục
        /// </summary>
        public string xmCode { get; set; }
        /// <summary>
        /// Tên mục
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// dathửkếtnếu
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string remark { get; set; }
    }
}
