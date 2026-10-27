using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO
{
    public class DiagnoseDTO
    {
        public string zdCode { get; set; }
        public string zdName { get; set; }
        public string orgId { get; set; }
        public string icd10 { get; set; }
        public string zdlx { get; set; }
    }

    public class DiagnoseDocDTO : DiagnoseDTO
    {
        /// <summary>
        /// Phân loại chẩn đoán 1	Chẩn đoán sơ bộ 2	Chẩn đoán sửa đổi 3	Chẩn đoán xác định 4	Chẩn đoán bổ sung
        /// </summary>
        public string zdfl { get; set; }
        public int? px { get; set; }
    }
}
