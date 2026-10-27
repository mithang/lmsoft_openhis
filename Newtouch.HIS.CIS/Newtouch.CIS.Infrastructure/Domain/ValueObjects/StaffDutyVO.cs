using SqlSugar;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>Row of Base view V_C_Sys_StaffDuty (staff &lt;-&gt; duty mapping).</summary>
    [SugarTable("V_C_Sys_StaffDuty")]
    public class StaffDutyVO
    {
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffPY { get; set; }
        public string StaffGh { get; set; }
        public string OrganizeId { get; set; }
        public string DutyCode { get; set; }
        public string DutyName { get; set; }
        public string zt { get; set; }
        public string kflb { get; set; }
    }
}
