using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Ngoại trú phút khám,mỗi Phòng khám chờ Khám bệnh Số lượng
    /// </summary>
    public class OutpatientConsultCountVO
    {
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên phòng khám
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// còn lại chờ Khám bệnh Số lượng
        /// </summary>
        public int num { get; set; }
    }
}
