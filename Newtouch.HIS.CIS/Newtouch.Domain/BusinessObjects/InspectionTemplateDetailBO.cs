using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class InspectionTemplateDetailBO
    {
        /// <summary>
        /// Mẫu
        /// </summary>
        public InspectionTemplateVO mbEntity { get; set; }

        /// <summary>
        /// Gói mẫu
        /// </summary>
        public List<GroupPackageEntity> mbztList { get; set; }
    }
}
