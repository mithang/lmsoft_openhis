using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// Đ ăng ký khám ngoại trú tra cứu
    /// </summary>
    public class OutPatientRegistrationQueryRequest : RequestBase
    {
        /// <summary>
        /// nhất sau cập nhật thời gian
        /// </summary>
        public DateTime? lastUpdateTime { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        public string outpatientNo { get; set; }

        /// <summary>
        /// Khoa mã 
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Bác sĩ mã nhân viên
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// cửa cấp cứu dấu hiệu 
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// Khám bệnh dấu hiệu 
        /// </summary>
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// Từ khóa có thể khớp mzh blh xm
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// Phân trang
        /// </summary>
        public Pagination pagination { get; set; }

    }
}
