using System;

namespace Newtouch.Domain.ViewModels
{
    /// <summary>
    /// Lưu ýxạThông tin bệnh nhân
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
        /// phốiDược sĩMã nhân viên
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// phốiDược sĩHọ tên
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// Thực hiệnngườiMã nhân viên
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// Thực hiệnngườiHọ tên
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// truyền dịchThời gian bắt đầu
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// truyền dịchThời gian kết thúc
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string remark { get; set; }

    }
}
