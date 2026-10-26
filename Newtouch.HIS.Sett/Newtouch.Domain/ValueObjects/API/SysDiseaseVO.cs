using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysDiseaseVO
    {
        /// <summary>
        /// BệnhID
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// Tên bệnh
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// ICD10
        /// </summary>
        public string icd10 { get; set; }
    }
}
