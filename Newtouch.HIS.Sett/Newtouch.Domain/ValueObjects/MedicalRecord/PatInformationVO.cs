using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin bệnh án bệnh nhân nội trú
    /// </summary>
    public class PatInformationVO
    {
        public string bah { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string nlt { get; set; }
        public string cstz { get; set; }
        public string ks { get; set; }
        public string ts { get; set; }
        public string LYFS { get; set; }
    }

}
