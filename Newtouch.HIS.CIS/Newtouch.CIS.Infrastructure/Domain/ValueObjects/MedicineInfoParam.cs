using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// Thuốc Điều kiện tìm kiếm
    /// </summary>
    public class MedicineInfoParam
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Thuốc Loại
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
        /// Sử dụng Trạng thái điều khiển,Bình thường
        /// </summary>
        public string syzt { get; set; }

        /// <summary>
        /// Thuốc Trạng thái Vô hiệu,Kích hoạt
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// nhập mã
        /// </summary>
        public string srm { get; set; }
    }
}
