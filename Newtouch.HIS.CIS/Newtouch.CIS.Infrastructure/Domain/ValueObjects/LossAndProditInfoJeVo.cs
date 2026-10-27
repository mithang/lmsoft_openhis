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
        /// Giá bán lẻTổng số tiền
        /// </summary>
        public decimal? Ljze { get; set; }

        /// <summary>
        /// Giá bán buônTổng số tiền
        /// </summary>
        public decimal? Pjze { get; set; }

        /// <summary>
        /// vàogiáTổng số tiền
        /// </summary>
        public decimal? Jjze { get; set; }
    }
}
