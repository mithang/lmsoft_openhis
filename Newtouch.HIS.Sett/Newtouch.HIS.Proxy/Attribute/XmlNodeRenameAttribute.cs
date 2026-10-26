using System;

namespace Newtouch.HIS.Proxy.Attribute
{
    /// <summary>
    /// đổi tênxml nút tên
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class XmlNodeRenameAttribute : System.Attribute
    {
        public string NewName { get; set; }

        public XmlNodeRenameAttribute(string newName)
        {
            NewName = newName;
        }
    }
}