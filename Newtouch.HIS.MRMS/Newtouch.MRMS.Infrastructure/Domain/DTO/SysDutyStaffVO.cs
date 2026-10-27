namespace Newtouch.MR.ManageSystem.Domain.DTO
{
    /// <summary>
    /// Staff-by-duty lookup row (FrameworkBase VO, re-created here from the
    /// columns selected by CommonDmnService.GetStaffByDutyCode).
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
