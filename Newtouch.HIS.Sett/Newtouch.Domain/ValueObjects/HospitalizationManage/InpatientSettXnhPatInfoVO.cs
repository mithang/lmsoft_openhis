using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Quyết toán Bảo hiểm nông thôn mới nội trú Thông tin bệnh nhân
    /// </summary>
    public class InpatientSettXnhPatInfoVO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Khóa chính bệnh nhân
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// Số thứ tự bồi thường
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        public string doctor { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Số thẻ y tế
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// Mã gia đình
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// Số cá nhân
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// Cờ nội trú
        /// </summary>
        public string zybz { get; set; }
    }
}
