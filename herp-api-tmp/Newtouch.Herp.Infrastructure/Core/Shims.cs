using System;

namespace Newtouch.Herp.Domain.Entity
{
    /// <summary>
    /// Marker replacing FrameworkBase.MultiOrg.Domain.Entity.IEntity&lt;T&gt; (EF base interface, binary-only).
    /// Entities keep their original declaration shape; SqlSugar ignores this interface.
    /// </summary>
    public interface IEntity<T> where T : class
    {
    }

    /// <summary>
    /// Shim for the EF-era DecimalPrecision attribute (precision metadata only; SqlSugar ignores it).
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
