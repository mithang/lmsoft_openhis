using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    public class LossAndProditInfoJeVo
    {
        /// <summary>
        /// Tổng số tiền giá bán lẻ
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// Tổng số tiền giá bán sỉ
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// Tổng số tiền giá nhập
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}
