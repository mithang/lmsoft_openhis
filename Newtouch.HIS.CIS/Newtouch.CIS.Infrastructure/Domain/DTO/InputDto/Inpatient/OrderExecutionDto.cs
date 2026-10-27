using Newtouch.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto
{
    /// <summary>
    /// Y lệnhchi tiết
    /// </summary>
    public class OrderExecutionDto
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }
        /// <summary>
        /// lĩnhthuốcthứ tựsố
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
        /// uống、tĩnhgiọt...
        /// </summary>
        public string zlff { get; set; }
        /// <summary>
        /// Thời gian thực hiện：04,06,08...
        /// </summary>
        public string sjap { get; set; }
        /// <summary>
        /// vàThực hiệnSố lượngtương ứng
        /// </summary>
        public int? pcmc { get; set; }
        /// <summary>
        /// dùnglượng
        /// </summary>
        public string yl { get; set; }
        /// <summary>
        /// dùnglượngĐơn vị
        /// </summary>
        public string yldw { get; set; }
        /// <summary>
        /// Bác sĩMã nhân viên
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
        /// phát thuốcNhà thuốc
        /// </summary>
        public string fyyf { get; set; }
        /// <summary>
        /// 1：Tạm thời；2：Dài hạn
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
        /// Khu bệnhTên
        /// </summary>
        public string bqmc { get; set; }
        /// <summary>
        /// giường ký tự
        /// </summary>
        public string cw { get; set; }
        /// <summary>
        /// Cờ phát thuốc  0：chưaphát；1：đãxếp；2：đãphát；3：đãtrả
        /// </summary>
        public char fybz { get; set; }
        /// <summary>
        /// Y lệnhThực hiệnĐăng ký người
        /// </summary>
        public string yzzxsqr { get; set; }

    }
    /// <summary>
    /// Thực hiệnkếtnếu
    /// </summary>
    public class ActResult
    {
        /// <summary>
        /// Thực hiệnkếtnếu
        /// </summary>
        public ActResult()

        {

            IsSucceed = true;

        }
        /// <summary>
        /// làKhôngthành công
        /// </summary>
        public bool IsSucceed { get; set; }
        /// <summary>
        /// LỗiMã
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// LỗiMô tả
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// Quay lạithông tin
        /// </summary>
        public dynamic Data { get; set; }
    }
}

/// <summary>
/// dựđịnhthất bại mục
/// </summary>
public class FailYzDetail : OrderExecutionDto
{
    /// <summary>
    /// thất bạinguyên nhân
    /// </summary>
    public string FailMsg { get; set; }
    /// <summary>
    /// thất bạiMã
    /// </summary>
    public string FailCode { get; set; }
}




