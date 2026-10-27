using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Domain
{
    // Source-true VO copied from Newtouch.Domain/ValueObjects/
    // TherapeutistCompleteManage/TherapeutistWorkPlanGridVO.cs (class
    // submitTimeVO). Property names kept verbatim: JSON field names used by
    // NonChargeableItems views.
    public class submitTimeVO
    {
        public string type { get; set; }
        public int Id { get; set; }
        public string jqRowId { get; set; }
        public string jzsj { get; set; }
        public string sc { get; set; }
        public string tzsc { get; set; }
        public string tzly { get; set; }
    }

    // Source: Newtouch.Domain/DTO/InputDto/TherapistSuggestionDto.cs
    public class MzZyPatInfoDto
    {
        public string mzzyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string blh { get; set; }
        public int nl { get; set; }
        public string ryzd { get; set; }
        public string zjh { get; set; }
        public string phone { get; set; }
        public DateTime csny { get; set; }
        public DateTime? ryrq { get; set; }
    }

    public class TherapistAdviceDto
    {
        public string jyId { get; set; }
        public string blh { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public decimal? mczll { get; set; }
        public decimal? sl { get; set; }
        public string pc { get; set; }
        public string pcmc { get; set; }
        public string zxksdm { get; set; }
        public string zxksmc { get; set; }
        public string bz { get; set; }
        public string zhbz { get; set; }
        public string bw { get; set; }
    }

    public class TherapistSuggestionDto
    {
        public MzZyPatInfoDto MzZyPatInfoDto { get; set; }
        public IList<TherapistAdviceDto> TherapistAdviceDto { get; set; }
    }

    // Source: Newtouch.Domain/Entity/TherapeutistManage/TherapistSuggestionEntity.cs
    // (IEntity base dropped: merged Infra uses plain POCOs, not EF6 entities.)
    public class TherapistSuggestionEntity
    {
        public string jyId { get; set; }
        public string OrganizeId { get; set; }
        public string blh { get; set; }
        public string brlx { get; set; }
        public string mzzyh { get; set; }
        public string itemCode { get; set; }
        public decimal mczll { get; set; }
        public decimal sl { get; set; }
        public string pc { get; set; }
        public string zxksdm { get; set; }
        public string bz { get; set; }
        public string zhbz { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime LastModifyTime { get; set; }
        public string zt { get; set; }
        public string bw { get; set; }
    }
}
