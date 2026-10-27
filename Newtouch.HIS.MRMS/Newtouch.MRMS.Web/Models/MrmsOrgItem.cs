namespace Newtouch.MRMS.Web.Models
{
    /// <summary>
    /// Minimal org item for the portal Home views (replaces the binary
    /// FrameworkBase SysOrganizeVEntity — the views only use Id and Name).
    /// </summary>
    public class MrmsOrgItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
