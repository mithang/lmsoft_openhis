using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Bệnh nhân qua mẫn thông tin
    /// </summary>
    public class PatientGmxxDetailVO
    {
        /// <summary>
        /// qua mẫn thông tin bảng Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// ra sinh năm Tháng
        /// </summary>
        public DateTime csrq { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// qua mẫn Mục
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// qua mẫn Tên mục
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// qua mẫn Loại
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// qua mẫn Tên loại
        /// </summary>
        public string gmlxmc { get; set; }

        /// <summary>
        /// qua mẫn Thuốc
        /// </summary>
        public string gmyp { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
