namespace Newtouch.EMR.Domain.ValueObjects
{
    /// <summary>
    /// Staff-by-duty row. In the original this type came from the binary
    /// FrameworkBase.MultiOrg.Domain.ValueObjects; it is re-declared here
    /// with the exact shape CommonDmnService.GetStaffByDutyCode returns
    /// (V_C_Sys_StaffDuty + staff/dept views).
    /// </summary>
    public class SysDutyStaffVO
    {
        public string StaffName { get; set; }
        public string StaffPY { get; set; }
        public string StaffGh { get; set; }
        public string ks { get; set; }
        public string kspy { get; set; }
        public string ksmc { get; set; }
    }
}
