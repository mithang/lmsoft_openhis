namespace Newtouch.Herp.Web.Models
{
    /// <summary>
    /// Minimal org item for the portal Home views (replaces the binary
    /// FrameworkBase SysOrganizeVEntity — the views only use Id and Name).
    /// Same shape as MRMS MrmsOrgItem (D-SHELL-002, single-org deployment).
    /// </summary>
    public class HerpOrgItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
