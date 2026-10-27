using System.ComponentModel;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// Cờ đăng nhập
    /// </summary>
    public enum EnumLoginFlag
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Đăng nhập một lần (SSO)")]
        SSO = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Đăng nhập hệ thống")]
        App = 2
    }
    public enum EnumSex
    {
        [Description("Nam")]
        M = 1,
        [Description("Nữ")]
        F = 2
    }
    public enum EnumDiagType
    {
        [Description("Chẩn đoán khi nhập viện")]
        ryzd = 1,
        [Description("Chẩn đoán khi xuất viện")]
        cyzd = 2
    }
    public enum EnumRecordStu
    {
        [Description("Chưa nộp")]
        wtj = 0,
        [Description("Đã nộp")]
        ytj = 1,
        [Description("Trả lại")]
        th = 2,
        [Description("Đã ký nhận")]
        yqs = 3,
        [Description("Lưu trữ bệnh án")]
        bagd = 4
    }
    /// <summary>
    /// Trạng thái y lệnh
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// Chờ duyệt 
        /// </summary>
        [Description("Chờ duyệt")]
        Ds = 0,
        /// <summary>
        /// Duyệt
        /// </summary>
        [Description("Duyệt")]
        Sh = 1,
        /// <summary>
        /// Thực hiện
        /// </summary>
        [Description("Thực hiện")]
        Zx = 2,
        /// <summary>
        /// Thu hồi DC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// Dừng hoặc hủy
        /// </summary>
        [Description("Dừng")]
        TZ = 4
    }
    public enum EnumYzlx
    {
        ///// <summary>
        ///// Chung
        ///// </summary>
        //[Description("Chung")]
        //Ty = 1,
        /// <summary>
        /// Thuốc
        /// </summary>
        [Description("Thuốc")]
        Yp = 2,
        /// <summary>
        /// Văn bản
        /// </summary>
        [Description("Văn bản")]
        Wz = 3,
        /// <summary>
        /// Thuốc mang về khi xuất viện
        /// </summary>
        [Description("Thuốc mang về khi xuất viện")]
        Cydy = 4,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("Hạng mục")]
        sfxm = 5,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("Xét nghiệm")]
        jy = 6,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("Kiểm tra")]
        jc = 7,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("Y lệnh ăn uống")]
        ssyz = 8,
        /// <summary>
        /// Phẫu thuật
        /// </summary>
        [Description("Phẫu thuật")]
        oper = 9,
        /// <summary>
        /// Thuốc đông y
        /// </summary>
        [Description("Thuốc đông y")]
        zcy = 10,
        /// <summary>
        /// Phục hồi chức năng
        /// </summary>
        [Description("Phục hồi chức năng")]
        rehab = 11

    }
    [Description("Loại người nhận")]
    public enum RecipientTypeEnum
    {
        [Description("Người dùng")]
        user = 1,
        [Description("Nhóm người dùng")]
        usergroup = 2,
    }
    /// <summary>
    /// Loại nghiệp vụ nhóm thông báo
    /// </summary>
    [Description("Loại nghiệp vụ nhóm thông báo")]
    public enum GroupYwlxEnum
    {
        [Description("Chung")]
        None = 0,
        [Description("Ngoại trú")]
        Mz = 1,
        [Description("Nội trú")]
        Zy = 2,
    }
    /// <summary>
    /// Loại thực thi hàng đợi
    /// </summary>
    [Description("Loại thực thi hàng đợi")]
    public enum MsgQueueExecTypeEnum
    {
        [Description("Nhiệm vụ tức thì")]
        Immediately = 1,
        [Description("Nhiệm vụ trì hoãn")]
        Waitime = 2,
        [Description("Nhiệm vụ định kỳ")]
        Fixedtime = 3,
    }
    /// <summary>
    /// Giới hạn phạm vi áp dụng thông báo
    /// </summary>
    [Description("Phạm vi áp dụng nhóm thông báo")]
    public enum MsgNoticeRangeEnum
    {
        [Description("Không giới hạn")]
        None = 0,
        [Description("Toàn viện")]
        Org = 1,
        [Description("Người dùng chỉ định")]
        User = 2,
        [Description("Trong nhóm thông báo")]
        Group = 10,
        [Description("Phạm vi khoa")]
        Dept = 20,
        [Description("Trong khoa")]
        DeptGroup = 21,
        [Description("Phạm vi khu bệnh")]
        Area = 30,
        [Description("Trong khu bệnh")]
        AreaGroup = 31,
    }
    /// <summary>
    /// Trạng thái thông báo
    /// </summary>
    [Description("Trạng thái thông báo")]
    public enum NoticeStuEnum
    {
        [Description("Chưa gửi")]
        UnSend = 0,
        [Description("Đã gửi")]
        Send = 1,
        [Description("Đã đọc")]
        Read = 2,
        [Description("Chờ xử lý")]
        Wait = 9
    }
    public enum ApplyTypeEnum
    {
        [Description("Hồ sơ mới")]
        xjbl = 0,
        [Description("Sửa hồ sơ")]
        xgbl = 1
    }
    /// <summary>
    /// Trạng thái phê duyệt đơn xin bệnh án
    /// </summary>
    public enum ApplyStatusEnum
    {
        /// <summary>
        /// Chưa phê duyệt
        /// </summary>
        [Description("Chưa phê duyệt")]
        wpz = 0,
        /// <summary>
        /// Đã phê duyệt
        /// </summary>
        [Description("Đã phê duyệt")]
        ysp = 1,
        /// <summary>
        /// Trả lại
        /// </summary>
        [Description("Trả lại")]
        th = 2
    }
}