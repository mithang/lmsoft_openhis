using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure.Attributes
{
    /// <summary>
    /// Tư ơng ứngXml nút nút tên(không 加 này thuộc tính giờ mặc đ ịnh取 thuộc tính 名/ loại 名)
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
