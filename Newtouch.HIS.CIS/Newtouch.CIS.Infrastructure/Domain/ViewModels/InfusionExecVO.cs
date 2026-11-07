using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Lưu ýxạ Thông tin bệnh nhân
    /// </summary>
    public class InfusionExecVO
    {
        /// <summary>
        /// Tên bệnh nhân
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
        /// phối Dược sĩ Mã nhân viên
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// phối Dược sĩ Họ tên
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// Thực hiệnngười Mã nhân viên
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Thực hiệnngười Họ tên
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// truyền dịch Thời gian bắt đầu
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// truyền dịch Thời gian kết thúc
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

    }
}
