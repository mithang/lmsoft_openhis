using System;

namespace Newtouch.HIS.Proxy.Attribute
{
    /// <summary>
    /// Giao diện mã 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InterfaceCodeAttribute : System.Attribute
    {
        /// <summary>
        /// Giao diện mã 
        /// </summary>
        public string InterfaceCode { get; set; }

        public InterfaceCodeAttribute(string interfaceCode)
        {
            this.InterfaceCode = interfaceCode;
        }
    }
}