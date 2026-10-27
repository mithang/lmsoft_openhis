using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class InspectionTemplateDetailBO
    {
        /// <summary>
        /// mẫu
        /// </summary>
        public InspectionTemplateVO mbEntity { get; set; }

        /// <summary>
        /// mẫuBộ mẫu
        /// </summary>
        public List<GroupPackageEntity> mbztList { get; set; }
    }
}
