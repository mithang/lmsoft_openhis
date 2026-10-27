using System;
using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.Zyypyz
{
    /// <summary>
    /// Nhập việnThuốcY lệnhThực hiện
    /// </summary>
    public class ZyypyzzxRequest : RequestBase
    {
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Y lệnhdanh sách
        /// </summary>
        public List<YzDetail> yzList { get; set; }

        /// <summary>
        /// giao diệngọidòngchảysố
        /// </summary>
        public string ClientNo { get; set; }
    }

    /// <summary>
    /// Y lệnhchi tiết
    /// </summary>
    public class YzDetail
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// lĩnhthuốcthứ tựsố
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
        /// Số lượng（Khoa phòngĐơn vị）
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// chuyểnhóavìcon  sl*zhyz=Đơn vị nhỏ nhấtSố lượng
        /// </summary>
        public int zhyz { get; set; }

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
        public string pcmc { get; set; }

        /// <summary>
        /// dùnglượng
        /// </summary>
        public decimal yl { get; set; }

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
        /// Y lệnhTính chất 1：Tạm thời；2：Dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// Dặn dò
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// Thực hiệnSố lượng
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
        /// Y lệnhThực hiệnĐăng ký người
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// Thực hiệnNgày
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// dánsố
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// Kiểu y lệnh
        /// </summary>
        public int? yzlx { get; set; }
    }
}
