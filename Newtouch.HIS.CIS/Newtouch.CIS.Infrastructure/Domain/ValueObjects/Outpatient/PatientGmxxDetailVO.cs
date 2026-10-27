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
        /// quamẫnthông tinbảngKhóa chính
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// rasinhnămTháng
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
        /// quamẫnMục
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// quamẫnTên mục
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// quamẫnLoại
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// quamẫnTên loại
        /// </summary>
        public string gmlxmc { get; set; }

        /// <summary>
        /// quamẫnThuốc
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
