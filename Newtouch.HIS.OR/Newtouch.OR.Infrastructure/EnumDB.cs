using System.ComponentModel;

namespace Newtouch.OR.ManageSystem.Infrastructure
{
    public enum EnumZYBZ
    {
        /// <summary>
        /// Đăng ký nhập viện
        /// </summary>
        Xry = 0,
        /// <summary>
        /// Đang ở khoa
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Xuất khoa（Rời khoa）（Chờ thanh toán）
        /// </summary>
        Djz = 2,
        /// <summary>
        /// Đã xuất viện（Thanh toán xuất viện）
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// Chuyển khoa
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hồ sơ hủy bỏ/Hủy nhập viện
        /// </summary>
        Wry = 9,
    }

    /// <summary>
    /// Trạng thái đơn đăng ký
    /// </summary>
    public enum EnumSqzt
    {
        [Description("Chờ duyệt")]
        /// <summary>
        /// Đã đăng ký, chờ duyệt(Xếp lịch)
        /// </summary>
        dsh = 1,
        [Description("Đã duyệt")]
        /// <summary>
        /// Đã duyệt(Xếp lịch)Chờ phẫu thuật
        /// </summary>
        ypb = 2,
        [Description("Đã hủy đơn")]
        /// <summary>
        /// Đã hủy đơn đăng ký
        /// </summary>
        yqx = 3,

    }

    /// <summary>
    /// Phẫu thuậtTrạng thái
    /// </summary>
    public enum EnumSSzt
    {
        [Description("Chờ xếp lịch")]
        /// <summary>
        /// Đã duyệt(Xếp lịch)
        /// </summary>
        dpb = 1,
        [Description("Đã xếp lịch")]
        /// <summary>
        /// Đã duyệt(Xếp lịch)Chờ phẫu thuật
        /// </summary>
        ypb = 2,
        [Description("Đã hủy bỏ")]
        /// <summary>
        /// Đã hủy đơn đăng ký
        /// </summary>
        yzf = 3,
        [Description("Đã đăng ký")]
        /// <summary>
        /// Đã hủy đăng ký
        /// </summary>
        yzx = 4

    }

    /// <summary>
    /// Đăng ký phẫu thuậtTrạng thái
    /// </summary>
    public enum EnumSSdjzt
    {
        [Description("Chờ đăng ký")]
        ddj = 1,
        [Description("Đã đăng ký")]
        ydj = 2,
        [Description("Hủy đăng ký")]
        yzf = 3
    }
    /// <summary>
    /// Cấp phẫu thuật
    /// </summary>
    public enum EnumSSjb
    {
        [Description("Cấp I")]
        yj = 1,
        [Description("Cấp II")]
        ej = 2,
        [Description("Cấp III")]
        sj = 3,
        [Description("Cấp IV")]
        fj = 4
    }

    /// <summary>
    /// Có cách ly không
    /// </summary>
    public enum EnumIsgl
    {
        [Description("Không")]
        no=0,
        [Description("Có")]
        yes=1
    }

    /// <summary>
    /// Có nhiễm khuẩn không
    /// </summary>
    public enum EnumIsjun
    {
        [Description("Không")]
        no=0,
        [Description("Có")]
        yes=1
    }
}