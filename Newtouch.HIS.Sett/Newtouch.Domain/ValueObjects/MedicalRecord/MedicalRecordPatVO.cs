using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Tính phí hạng mục nội trú Hạng mục của Thông tin chi tiết
    /// </summary>
    public class MedicalRecordPatVO
    {
        public string bah { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string nlt { get; set; }
        public string cstz { get; set; }
        public string cykb { get; set; }
        public string zyts { get; set; }
        public string lyfs { get; set; }
        public decimal zfy { get; set; }
        public string zdlist { get; set; }
        public string sslist { get; set; }
        public string brxzmc { get; set; }
    }

}
