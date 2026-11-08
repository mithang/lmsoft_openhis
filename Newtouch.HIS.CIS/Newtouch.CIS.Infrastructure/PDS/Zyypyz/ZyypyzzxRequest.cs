using System;
using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.Zyypyz
{
    /// <summary>
    /// Nhập viện ThuốcY lệnh Thực hiện
    /// </summary>
    public class ZyypyzzxRequest : RequestBase
    {
        /// <summary>
        /// Tổ chức ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Y lệnh danh sách
        /// </summary>
        public List<YzDetail> yzList { get; set; }

        /// <summary>
        /// giao diện gọi dòng chảy số
        /// </summary>
        public string ClientNo { get; set; }
    }

    /// <summary>
    /// Y lệnh chi tiết
    /// </summary>
    public class YzDetail
    {
        /// <summary>
        /// Y lệnh ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// lĩnh thuốc thứ tự số
        /// </summary>
        public long lyxh { get; set; }

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
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Nhóm lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Số lượng(Khoa phòng Đơn vị)
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// chuyển hóa vì con  sl*zhyz=Đơn vị nhỏ nhất Số lượng
        /// </summary>
        public int zhyz { get; set; }

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
        public string pcmc { get; set; }

        /// <summary>
        /// dùng lượng
        /// </summary>
        public decimal yl { get; set; }

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
        /// Y lệnh Tính chất 1:Tạm thời;2:Dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// Dặn dò
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// Thực hiện Số lượng
        /// </summary>
        public int zxsl { get; set; }

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
        public string zfxz { get; set; }

        /// <summary>
        /// Mã khoa
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// giường ký tự
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Y lệnh Thực hiện Đăng ký người
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// Thực hiện Ngày
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// dán số
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// Kiểu y lệnh
        /// </summary>
        public int? yzlx { get; set; }
    }
}
