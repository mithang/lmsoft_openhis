namespace Newtouch.EMR.Web.Models
{
    /// <summary>
    /// Minimal stand-in for the Framework-era SysOrganizeVEntity (V_S_Sys_Organize),
    /// used only by the Home org-switcher views for Id/Name. The Framework portal
    /// base leaves ViewBag.MediOrgList unset, so these views always see null here;
    /// the cast target only needs to compile.
    /// </summary>
    public class SysOrganizeV
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
