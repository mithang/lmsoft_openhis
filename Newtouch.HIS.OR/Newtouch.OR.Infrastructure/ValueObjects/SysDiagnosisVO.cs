namespace Newtouch.OR.ManageSystem.Domain.ValueObjects
{
    /// <summary>
    /// Diagnosis dictionary row (NewtouchHIS_Base..V_S_xt_zd columns).
    /// Local replacement for the Framework binary SysDiagnosis entity.
    /// </summary>
    public class SysDiagnosisVO
    {
        public int zdId { get; set; }
        public string zdCode { get; set; }
        public string icd10 { get; set; }
        public string icd10fjm { get; set; }
        public string zdmc { get; set; }
        public string py { get; set; }
        public string OrganizeId { get; set; }
        public string wb { get; set; }
        public string zt { get; set; }
        public string zdlx { get; set; }
        public string ybnhlx { get; set; }
    }
}
