namespace Newtouch.Domain.ValueObjects
{
    /// <summary>Ward row for staff ward authorization (department -&gt; ward mapping).</summary>
    public class WardVO
    {
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string bqCode { get; set; }
        public string bqmc { get; set; }
    }
}
