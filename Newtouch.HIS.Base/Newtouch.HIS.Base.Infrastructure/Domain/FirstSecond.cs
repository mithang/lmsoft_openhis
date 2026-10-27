using Newtouch.HIS.Domain;
namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// Generic two-field carrier (original: NewtouchHIS.Base.Domain.CommonBO.FirstSecond,
    /// in the WebAPI.Manage module — re-declared here so Base Infra stays self-contained).
    /// </summary>
    public class FirstSecond
    {
        public string First { get; set; }
        public string Second { get; set; }
    }
}
