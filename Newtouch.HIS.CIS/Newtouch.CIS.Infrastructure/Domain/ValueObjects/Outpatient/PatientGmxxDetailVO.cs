using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Bệnh nhânquamẫnthông tin
    /// </summary>
    public class PatientGmxxDetailVO
    {
        /// <summary>
        /// quamẫnthông tinbảng Khóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// rasinhnăm Tháng
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
        /// quamẫn Mục
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// quamẫn Tên mục
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// quamẫn Loại
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// quamẫn Tên loại
        /// </summary>
        public string gmlxmc { get; set; }

        /// <summary>
        /// quamẫn Thuốc
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
