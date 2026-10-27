using System;
using System.ComponentModel;

namespace Newtouch.PDS.Requset
{
    /// <summary>
    /// Kết quả thực hiện trả thuốc
    /// </summary>
    public class DrugReturnResult
    {
        /// <summary>
        /// Kết quả thực hiện
        /// </summary>
        public DrugReturnResult()
        {
            IsSucceed = true;
            ErrorCode = 0;
            ErrorMsg = string.Empty;
        }

        /// <summary>
        /// Có thành công không
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Mô tả lỗi
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Trả về y lệnhID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// ThuốcCODE
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// Số nhóm
        /// </summary>
        public string zh { get; set; }
        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public DateTime? zxrq { get; set; }
    }

    public enum ErrorCode
    {
        /// <summary>
        /// Thực hiện thành công
        /// </summary>
        [Description("Thực hiện thành công")]
        Success = 0,

        /// <summary>
        /// Số y lệnh không được để trống
        /// </summary>
        [Description("Số y lệnh không được để trống")]
        Error1 = 1,

        /// <summary>
        /// Mã thuốc không được để trống
        /// </summary>
        [Description("Mã thuốc không được để trống")]
        Error2 = 2,

        /// <summary>
        /// Không thể truy vấn thông tin y lệnh này
        /// </summary>
        [Description("Không thể truy vấn thông tin y lệnh này")]
        Error3 = 3,

        /// <summary>
        /// Chưa phát thuốc,Cấm trả thuốc
        /// </summary>
        [Description("Chưa phát thuốc,Cấm trả thuốc")]
        Error4 = 4,

        /// <summary>
        /// Thuốc đã trả rồi
        /// </summary>
        [Description("Thuốc đã trả rồi")]
        Error5 = 5,

        /// <summary>
        /// Đã xin trả thuốc, cấm,thao tác trùng lặp
        /// </summary>
        [Description("Đã xin trả thuốc, cấm,thao tác trùng lặp")]
        Error6 = 6,

        /// <summary>
        /// Người xin trả thuốc không được để trống
        /// </summary>
        [Description("Người xin trả thuốc không được để trống")]
        Error7 = 7,

        /// <summary>
        /// Ngày thực hiện không được để trống
        /// </summary>
        [Description("Ngày thực hiện không được để trống")]
        Error8 = 8,
    }
}
