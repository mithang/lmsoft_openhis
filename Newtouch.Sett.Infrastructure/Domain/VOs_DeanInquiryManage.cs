using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source: Newtouch.Domain/Entity/DeanInquiryManage/NumberTrendEntity.cs
    public class NumberTrendList
    {
        public List<NumberTrendEntity> ryqs { get; set; }
        public List<NumberTrendEntity> cyqs { get; set; }
        public List<NumberTrendEntity> zzqs { get; set; }
    }

    public class NumberTrendEntity
    {
        public string mount { get; set; }
        public int? rysl { get; set; }
        public int? cysl { get; set; }
        public int? zysl { get; set; }
    }

    public class NumberPersonEntity
    {
        public string mount { get; set; }
        public int? rysl { get; set; }
        public int? cysl { get; set; }
        public int? zysl { get; set; }
    }
}
