namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    /// <summary>
    /// Staff row for GetStaffByDutyCode (same shape as the MRQC pilot's
    /// SysDutyStaffVO: V_C_Sys_StaffDuty joined to V_S_Sys_Staff and
    /// V_S_Sys_Department).
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
