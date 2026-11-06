namespace Newtouch.MRQC.Web.Models
{
    /// <summary>
    /// Minimal stand-in for the Framework-era SysOrganizeVEntity (V_S_Sys_Organize),
    /// used only by the Home org-switcher views for Id/Name. Same shape as the
    /// EMR port's SysOrganizeV (Newtouch.EMR.Web.Models.SysOrganizeV).
    /// </summary>
    public class SysOrganizeV
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
