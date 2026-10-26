using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure.Attributes
{
    /// <summary>
    /// Tư ơng ứngXml nút nút tên(không thêm này thuộc tính giờ mặc đ ịnhlấy thuộc tính tên/ loại tên)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class APIBaseAttribute : Attribute
    {
        public APIBaseAttribute(string name) {
            this.Name = name;
        }
        public string Name { get; set; }
        public string ErrorMsg { get; set; }

        
    }
}
