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
        /// Trong khu bệnh
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Xuất viện khỏi khu bệnh（Rời khỏi khu bệnh）（Chờ thanh toán）
        /// </summary>
        Djz = 2,
        /// <summary>
        /// Đã xuất viện（Thanh toán xuất viện）
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// Chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hủy bản ghi/Hủy nhập viện
        /// </summary>
        Wry = 9,
    }

    /// <summary>
    /// Trạng thái đăng ký
    /// </summary>
    public enum EnumSqzt
    {
        [Description("待审核")]
        /// <summary>
        /// Đã gửi đơn, chờ xét duyệt(Xếp lịch)
        /// </summary>
        dsh = 1,
        [Description("已审核")]
        /// <summary>
        /// Đã xét duyệt(Xếp lịch)Chờ phẫu thuật
        /// </summary>
        ypb = 2,
        [Description("已取消申请")]
        /// <summary>
        /// Đã hủy đăng ký
        /// </summary>
        yqx = 3,

    }

    /// <summary>
    /// Trạng thái phẫu thuật
    /// </summary>
    public enum EnumSSzt
    {
        [Description("待排班")]
        /// <summary>
        /// Đã xét duyệt(Xếp lịch)
        /// </summary>
        dpb = 1,
        [Description("已排班")]
        /// <summary>
        /// Đã xét duyệt(Xếp lịch)Chờ phẫu thuật
        /// </summary>
        ypb = 2,
        [Description("已作废")]
        /// <summary>
        /// Đã hủy đăng ký
        /// </summary>
        yzf = 3,
        [Description("已登记")]
        /// <summary>
        /// Đã hủy đăng ký
        /// </summary>
        yzx = 4

    }

    /// <summary>
    /// Trạng thái đăng ký phẫu thuật
    /// </summary>
    public enum EnumSSdjzt
    {
        [Description("待登记")]
        ddj = 1,
        [Description("已登记")]
        ydj = 2,
        [Description("登记作废")]
        yzf = 3
    }
    /// <summary>
    /// Cấp độ phẫu thuật
    /// </summary>
    public enum EnumSSjb
    {
        [Description("I级")]
        yj = 1,
        [Description("II级")]
        ej = 2,
        [Description("III级")]
        sj = 3,
        [Description("IV级")]
        fj = 4
    }

    /// <summary>
    /// Có cách ly không
    /// </summary>
    public enum EnumIsgl
    {
        [Description("否")]
        no=0,
        [Description("是")]
        yes=1
    }

    /// <summary>
    /// Có nhiễm khuẩn không
    /// </summary>
    public enum EnumIsjun
    {
        [Description("否")]
        no=0,
        [Description("是")]
        yes=1
    }
}