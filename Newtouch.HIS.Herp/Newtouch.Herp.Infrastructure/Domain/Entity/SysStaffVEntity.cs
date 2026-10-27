namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Staff view (Herp-local replacement for the Framework binary
    /// SysStaffVEntity; only the fields Herp code reads: gh + Name + py).
    /// Source table: NewtouchHIS_Base.dbo.Sys_Staff.
    /// py added at integration: ProfitAndLoss GetZRRList's selector renders a
    /// py column that was empty without it.
    /// </summary>
    public class SysStaffVEntity
    {
        public string Id { get; set; }
        public string gh { get; set; }
        public string Name { get; set; }
        public string py { get; set; }
    }
}
