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
        /// Giá bán lẻ Tổng số tiền
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// Giá bán buôn Tổng số tiền
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// vàogiá Tổng số tiền
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}
