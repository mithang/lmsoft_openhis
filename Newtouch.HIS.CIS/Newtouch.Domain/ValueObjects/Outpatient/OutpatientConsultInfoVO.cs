using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Thông tin phòng khám(Màn hình nhỏ)
    /// </summary>
    public class OutpatientConsultInfoVO
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
        /// Họ tên bác sĩ
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// Giới thiệu bác sĩ
        /// </summary>
        public string ysjs { get; set; }
        /// <summary>
        /// Đang khám
        /// </summary>
        public string jzz { get; set; }
        /// <summary>
        /// Số thứ tự đang khám
        /// </summary>
        public Int16? jzzxh { get; set; }
        /// <summary>
        /// Chờ khám
        /// </summary>
        public string djz { get; set; }
        /// <summary>
        /// Số thứ tự chờ khám
        /// </summary>
        public Int16? djzxh { get; set; }
    }
}
