using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    /// <summary>
    /// Tin tức trong ngày_Tổng quan thu nhập toàn viện
    /// </summary>
    public class DailyUpdatesEntiy
    {
        /// <summary>
        /// Khoa ngoại trú
        /// </summary>
        public string mzks { get; set; } 

        /// <summary>
        /// Đã khám
        /// </summary>
        public int yjz { get; set; }

        /// <summary>
        /// Đang khám
        /// </summary>
        public int jzz { get; set; }
    }
    /// <summary>
    /// Tra cứu của viện trưởng-Tin tức hôm nay-banner
    /// </summary>
    public class JRDT_Banner
    {
        /// <summary>
        /// Tổng thu nhập bệnh viện
        /// </summary>
        public decimal? yyzsr { get; set; }
        /// <summary>
        /// Thu nhập ngoại trú
        /// </summary>
        public decimal? mzsr { get; set; }
        /// <summary>
        /// Lượt khám ngoại trú
        /// </summary>
        public int? mzrc { get; set; }
        /// <summary>
        /// Thu nhập nội trú
        /// </summary>
        public decimal? zysr { get; set; }
        /// <summary>
        /// Số người xuất viện
        /// </summary>
        public int? cyrs { get; set; }
    }
    /// <summary>
    /// Tin tức trong ngày_Tin tức ngoại trú hôm nay
    /// </summary>
    public class DailyUpdates_GetJrdtmz
    {
        /// <summary>
        /// Khoa ngoại trú
        /// </summary>
        public string mzks { get; set; }

        /// <summary>
        /// Đã khám
        /// </summary>
        public int yjz { get; set; }

        /// <summary>
        /// Đang khám
        /// </summary>
        public int jzz { get; set; }
        /// <summary>
        /// Chờ khám
        /// </summary>
        public int djz { get; set; }
    }

    /// <summary>
    /// Tin tức trong ngày_Đơn thuốc ngoại trú
    /// </summary>
    public class DailyUpdates_GetMzcf
    {
        /// <summary>
        /// Hạng mục
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal? sl { get; set; }

    }

    /// <summary>
    /// Tin tức trong ngày_Chi phí ngoại trú
    /// </summary>
    public class DailyUpdates_GetMzfy
    {
        /// <summary>
        /// Phân loại chi phí
        /// </summary>
        public string fyfl { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// Tỷ trọng
        /// </summary>
        public decimal? zb { get; set; }
    }

    /// <summary>
    /// Tin tức trong ngày_Tỷ lệ chiếm giường nội trú
    /// </summary>
    public class DailyUpdates_GetZyzcl
    {
        /// <summary>
        /// Hạng mục
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// Tin tức trong ngày_Thống kê đăng ký khám ngoại trú
    /// </summary>
    public class DailyUpdates_GetMzghtj
    {
        /// <summary>
        /// Loại đăng ký khám
        /// </summary>
        public string ghlx { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }
    }

    /// <summary>
    /// Tin tức trong ngày_Thống kê bệnh nhân nội trú
    /// </summary>
    public class DailyUpdates_GetZyhztj
    {
        /// <summary>
        /// Hạng mục
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int sl { get; set; }
    }

}
