using Newtouch.HIS.Domain.Entity;
using System;

namespace FrameworkBase.MultiOrg.DmnService
{
    // The Framework DmnServiceBase is the module's own DmnServiceBase for all
    // PDS services (they resolve it via their parent namespace); this subclass
    // exists only so `using FrameworkBase.MultiOrg.DmnService;` resolves.
    public class DmnServiceBase : Newtouch.HIS.DomainServices.DmnServiceBase
    {
        public DmnServiceBase(Newtouch.Infrastructure.IDatabaseFactory databaseFactory) : base(databaseFactory) { }
        public DmnServiceBase(Newtouch.Infrastructure.IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc) { }
    }
}

namespace FrameworkBase.MultiOrg.Domain
{
}

namespace FrameworkBase.MultiOrg.Domain.Entity
{
}

namespace FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure
{
    public class DefaultDatabaseFactory : Newtouch.Infrastructure.IBaseDatabaseFactory { }
}

namespace Newtouch.Infrastructure.EF.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute(int precision, int scale) { Precision = precision; Scale = scale; }
        public int Precision { get; }
        public int Scale { get; }
    }
}

namespace Newtouch.Tools.Net
{
    public static class HttpClientHelper
    {
        public static T HttpPostStringAndRead<T>(string url, string json, Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType contentType)
        {
            return Newtouch.Core.Common.Utils.HttpClientHelper.HttpPostStringAndRead<T>(url, json, contentType);
        }
    }
}

namespace Newtouch.HIS.API.Common
{
    // Minimal port of the shared API request base (original: his-dll-common, no source).
    // Only the members the PDS code actually touches are declared; the build
    // will surface any additional member that is needed.
    public class RequestBase
    {
        public string OrganizeId { get; set; }
        public string UserCode { get; set; }
        public string TopOrganizeId { get; set; }
        public string AppId { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public DateTime Timestamp { get; set; }
        public string ResponseColumns { get; set; }
    }
}
