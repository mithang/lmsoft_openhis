using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InpatientBasicInfoVO
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string zhcode { get; set; }
        public string ryrq { get; set; }
        public string cyrq { get; set; }
        public int? zybz { get; set; }
        /// <summary>
        /// Tài khoảnTính chất
        /// </summary>
        public int? zhxz { get; set; }
        /// <summary>
        /// tạm ứngkimdưmức
        /// </summary>
        public string zhye { get; set; }
        /// <summary>
        /// phátsinhcủatổngChi phí
        /// </summary>
        public decimal? zje { get; set; }
        /// <summary>
        /// đãkếtChi phí
        /// </summary>
        public decimal? yjfy { get; set; }
        /// <summary>
        /// tạm ứngkimbáocảnh báomức
        /// </summary>
        public decimal? bje { get; set; }
        /// <summary>
        /// Bảo hiểm y tếhiệnkimchitrả
        /// </summary>
        public decimal? ybxjzf { get; set; }
        public List<InpatientBedUseInfoVO> patTransList { get; set; }

    }
}
