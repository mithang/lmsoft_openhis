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
        /// Có thành công hay không
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
        [Description("执行成功")]
        Success = 0,

        /// <summary>
        /// Số y lệnh không được để trống
        /// </summary>
        [Description("医嘱号不能为空")]
        Error1 = 1,

        /// <summary>
        /// Mã thuốc không được để trống
        /// </summary>
        [Description("药品编码不能为空")]
        Error2 = 2,

        /// <summary>
        /// Không thể tra cứu thông tin y lệnh đó
        /// </summary>
        [Description("无法查询该医嘱信息")]
        Error3 = 3,

        /// <summary>
        /// Chưa phát thuốc,Cấm trả thuốc
        /// </summary>
        [Description("未发药,禁止退药")]
        Error4 = 4,

        /// <summary>
        /// Thuốc đã trả rồi
        /// </summary>
        [Description("药品已经退过了")]
        Error5 = 5,

        /// <summary>
        /// Đã đề nghị trả thuốc, cấm,Ngăn thao tác lặp lại
        /// </summary>
        [Description("已经申请退药禁,止重复操作")]
        Error6 = 6,

        /// <summary>
        /// Người đề nghị trả thuốc không được để trống
        /// </summary>
        [Description("退药申请人不能为空")]
        Error7 = 7,

        /// <summary>
        /// Ngày thực hiện không được để trống
        /// </summary>
        [Description("执行日期不能为空")]
        Error8 = 8,
    }
}
