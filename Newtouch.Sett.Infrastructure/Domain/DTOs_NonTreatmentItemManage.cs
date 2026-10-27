using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    public class INonTreatmentItemDmnService_GetChargeCategoryTreeListDto
    {
        public object Where { get; set; }
        public object dlCode { get; set; }
        public int? dlId { get; set; }
        public object dlmc { get; set; }
        public int? ParentId { get; set; }
    }

}