namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Staff view (Herp-local replacement for the Framework binary
    /// SysStaffVEntity; only the fields Herp code reads: gh + Name).
    /// Source table: NewtouchHIS_Base.dbo.Sys_Staff.
    /// </summary>
    public class SysStaffVEntity
    {
        public string Id { get; set; }
        public string gh { get; set; }
        public string Name { get; set; }
    }
}
