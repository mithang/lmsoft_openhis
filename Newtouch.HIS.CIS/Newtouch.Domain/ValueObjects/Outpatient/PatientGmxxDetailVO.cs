using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// Thông tin dị ứng bệnh nhân
    /// </summary>
    public class PatientGmxxDetailVO
    {
        /// <summary>
        /// Khóa chính bảng thông tin dị ứng
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Năm tháng sinh
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
        /// Số ngoại trú
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// Hạng mục dị ứng
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// Tên hạng mục dị ứng
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// Loại dị ứng
        /// </summary>
        public string gmlx { get; set; }

        /// <summary>
        /// Tên loại dị ứng
        /// </summary>
        public string gmlxmc { get; set; }

        /// <summary>
        /// Thuốc gây dị ứng
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
