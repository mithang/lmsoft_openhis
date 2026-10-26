using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientWorkloadEntiy
    {
        /// <summary>
        /// Khoa ngoại trú
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// Lượt khám chữa bệnh ngoại trú
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// Số lượng đơn thuốc
        /// </summary>
        public int? cfsl { get; set; }
        /// <summary>
        /// Số đơn thuốc đã kê
        /// </summary>
        public int? kycfs { get; set; }
        /// <summary>
        /// Số đơn thuốc đã phát
        /// </summary>
        public int? fycfs { get; set; }
        /// <summary>
        /// Số đơn thuốc phục hồi chức năng
        /// </summary>
        public int? kfcfs { get; set; }
        public string DepartmentCode { get; set; }
    }

    public class OutpatientWorkloadEntiy_ysgzl
    {
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Lượt khám chữa bệnh ngoại trú
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// Số lượng đơn thuốc
        /// </summary>
        public int? cfsl { get; set; }
        /// <summary>
        /// Số đơn thuốc đã kê
        /// </summary>
        public int? kycfs { get; set; }
        /// <summary>
        /// Số đơn thuốc đã phát
        /// </summary>
        public int? fycfs { get; set; }
        /// <summary>
        /// Số đơn thuốc phục hồi chức năng
        /// </summary>
        public int? kfcfs { get; set; }
    }
}
