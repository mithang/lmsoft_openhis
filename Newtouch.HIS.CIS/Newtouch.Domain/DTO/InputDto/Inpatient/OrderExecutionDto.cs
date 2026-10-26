using Newtouch.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Chi tiết y lệnh
    /// </summary>
    public class OrderExecutionDto
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// Số thứ tự lĩnh thuốc
        /// </summary>
        public int? lyxh { get; set; }
        ///// <summary>
        ///// Loại y lệnh
        ///// </summary>
        //public int? yzlx { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }
        /// <summary>
        /// Số nội trú
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// Nhóm lớn
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int? sl { get; set; }
        /// <summary>
        /// Uống,Truyền tĩnh mạch...
        /// </summary>
        public string zlff { get; set; }
        /// <summary>
        /// Thời gian thực hiện:04,06,08...
        /// </summary>
        public string sjap { get; set; }
        /// <summary>
        /// Tương ứng với số lượng thực hiện
        /// </summary>
        public int? pcmc { get; set; }
        /// <summary>
        /// Liều dùng
        /// </summary>
        public string yl { get; set; }
        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string yldw { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string ysgh { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTime ksrq { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public DateTime jsrq { get; set; }
        /// <summary>
        /// Nhà thuốc phát thuốc
        /// </summary>
        public string fyyf { get; set; }
        /// <summary>
        /// 1:Tạm thời;2:Dài hạn
        /// </summary>
        public int? yzxz { get; set; }
        /// <summary>
        /// Dặn dò
        /// </summary>
        public string yzbz { get; set; }
        /// <summary>
        /// Tần suất
        /// </summary>
        public int? zxsl { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal je { get; set; }
        /// <summary>
        /// Tỷ lệ tự chi trả
        /// </summary>
        public decimal? zfbl { get; set; }
        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public char zfxz { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        public string bqmc { get; set; }
        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// Cờ phát thuốc  0:Chưa phát;1:Đã xếp;2:Đã phát;3:Đã trả
        /// </summary>
        public char fybz { get; set; }
        /// <summary>
        /// Người yêu cầu thực hiện y lệnh
        /// </summary>
        public string yzzxsqr { get; set; }

    }
    /// <summary>
    /// Kết quả thực hiện
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// Kết quả thực hiện
        /// </summary>
        public ActResult()

        {

            IsSucceed = true;

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
        /// Thông tin trả về
        /// </summary>
        public dynamic Data { get; set; }
    }
}

/// <summary>
/// Hạng mục đặt trước thất bại
/// </summary>
public class FailYzDetail : OrderExecutionDto
{
    /// <summary>
    /// Lý do thất bại
    /// </summary>
    public string FailMsg { get; set; }
    /// <summary>
    /// Mã thất bại
    /// </summary>
    public string FailCode { get; set; }
}




