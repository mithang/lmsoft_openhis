using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// Xác định theo tính chất bệnh nhân và chẩn đoán,Giới hạn chi phí hoàn trả
    /// </summary>
    [Serializable]
    public class BrxzZdBxsxBO
    {
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// Giới hạn hoàn trả
        /// </summary>
        public decimal bxsx { get; set; }
    }
}
