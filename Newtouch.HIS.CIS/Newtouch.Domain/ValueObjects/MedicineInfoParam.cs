using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Điều kiện tra cứu thuốc
    /// </summary>
    public class MedicineInfoParam
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Loại thuốc
        /// </summary>
        public string yplb { get; set; }

        /// <summary>
        /// Thuộc tính thuốc
        /// </summary>
        public string ypsx { get; set; }

        /// <summary>
        /// Dạng bào chế thuốc
        /// </summary>
        public string ypjx { get; set; }

        /// <summary>
        /// Trạng thái sử dụng Kiểm soát,Bình thường
        /// </summary>
        public string syzt { get; set; }

        /// <summary>
        /// Trạng thái thuốc Tắt,Bật
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// Mã nhập
        /// </summary>
        public string srm { get; set; }
    }
}
