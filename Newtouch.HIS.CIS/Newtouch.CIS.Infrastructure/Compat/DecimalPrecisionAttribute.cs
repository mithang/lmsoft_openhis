using System;

namespace Newtouch.CIS.Compat
{
    /// <summary>
    /// Stand-in for the Framework-only Newtouch.Infrastructure.EF DecimalPrecision
    /// attribute. SqlSugar maps decimals by convention; this attribute is kept so
    /// ported value objects compile unchanged (it carries no runtime behavior here).
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute(int precision, int scale)
        {
            Precision = precision;
            Scale = scale;
        }

        public int Precision { get; }
        public int Scale { get; }
    }
}
