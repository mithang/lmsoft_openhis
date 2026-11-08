using Newtouch.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Y lệnh chi tiết
    /// </summary>
    public class OrderExecutionDto
    {
        /// <summary>
        /// Y lệnh ID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// lĩnh thuốc thứ tự số
        /// </summary>
        public int? lyxh { get; set; }
        ///// <summary>
        ///// Kiểu y lệnh
        ///// </summary>
        //public int? yzlx { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }
        /// <summary>
        /// Số nhập viện
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
        /// uống,tĩnh giọt...
        /// </summary>
        public string zlff { get; set; }
        /// <summary>
        /// Thời gian thực hiện:04,06,08...
        /// </summary>
        public string sjap { get; set; }
        /// <summary>
        /// và Thực hiện Số lượng tương ứng
        /// </summary>
        public int? pcmc { get; set; }
        /// <summary>
        /// dùng lượng
        /// </summary>
        public string yl { get; set; }
        /// <summary>
        /// dùng lượng Đơn vị
        /// </summary>
        public string yldw { get; set; }
        /// <summary>
        /// Bác sĩ Mã nhân viên
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
        /// phát thuốc Nhà thuốc
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
        /// Tỷ lệ tự trả
        /// </summary>
        public decimal? zfbl { get; set; }
        /// <summary>
        /// Tính chất tự trả
        /// </summary>
        public char zfxz { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// Khu bệnh Tên
        /// </summary>
        public string bqmc { get; set; }
        /// <summary>
        /// giường ký tự
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// Cờ phát thuốc  0:chưa phát;1:đã xếp;2:đã phát;3:đã trả
        /// </summary>
        public char fybz { get; set; }
        /// <summary>
        /// Y lệnh Thực hiện Đăng ký người
        /// </summary>
        public string yzzxsqr { get; set; }

    }
    /// <summary>
    /// Thực hiện kết nếu
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// Thực hiện kết nếu
        /// </summary>
        public ActResult()

        {

            IsSucceed = true;

        }
        /// <summary>
        /// là Không thành công
        /// </summary>
        public bool IsSucceed { get; set; }
        /// <summary>
        /// Lỗi Mã
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Lỗi Mô tả
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// Quay lại thông tin
        /// </summary>
        public dynamic Data { get; set; }
    }
}

/// <summary>
/// dự định thất bại mục
/// </summary>
public class FailYzDetail : OrderExecutionDto
{
    /// <summary>
    /// thất bại nguyên nhân
    /// </summary>
    public string FailMsg { get; set; }
    /// <summary>
    /// thất bại Mã
    /// </summary>
    public string FailCode { get; set; }
}




