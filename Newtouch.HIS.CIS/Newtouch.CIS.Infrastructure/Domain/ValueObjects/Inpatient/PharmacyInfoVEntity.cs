using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class PharmacyInfoVEntity
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }
    }
}
