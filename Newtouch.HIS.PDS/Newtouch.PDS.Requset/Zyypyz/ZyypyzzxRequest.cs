using System;
using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.Zyypyz
{
    /// <summary>
    /// Thực hiện y lệnh thuốc nội trú
    /// </summary>
    public class ZyypyzzxRequest : RequestBase
    {
        /// <summary>
        /// Tổ chứcID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Danh sách y lệnh
        /// </summary>
        public List<YzDetail> yzList { get; set; }

        /// <summary>
        /// Số giao dịch gọi giao diện
        /// </summary>
        public string ClientNo { get; set; }
    }

    /// <summary>
    /// Chi tiết y lệnh
    /// </summary>
    public class YzDetail
    {
        /// <summary>
        /// Y lệnhID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// Số thứ tự lĩnh thuốc
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
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
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Loại lớn
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Số lượng (đơn vị bộ phận）
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi  sl*zhyz=Số lượng đơn vị nhỏ nhất
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// Uống, truyền tĩnh mạch...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// Thời gian thực hiện：04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// Tương ứng với số lượng thực hiện
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// Đơn vị liều lượng
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
        /// Tính chất y lệnh 1：Tạm thời；2：Dài hạn
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// Dặn dò
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// Số lượng thực hiện
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
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// Người đề nghị thực hiện y lệnh
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// Ngày thực hiện
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// Số nhóm
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// Số thang
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// Loại y lệnh
        /// </summary>
        public int? yzlx { get; set; }
    }
}
