using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Thông tin bệnh nhân tiêm
    /// </summary>
    public class InfusionExecVO
    {
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// ghế/Số giường
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// Mã nhân viên dược sĩ pha thuốc
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// Họ tên dược sĩ pha thuốc
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// Mã nhân viên người thực hiện
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Họ tên người thực hiện
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// Thời gian bắt đầu truyền dịch
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// Thời gian kết thúc truyền dịch
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

    }
}
