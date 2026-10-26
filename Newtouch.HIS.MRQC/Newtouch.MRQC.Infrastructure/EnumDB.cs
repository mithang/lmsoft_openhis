using System.ComponentModel;

namespace Newtouch.MRQC.Infrastructure
{
    /// <summary>
    /// cờ đăng nhập
    /// </summary>
    public enum EnumLoginFlag
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("单点登录")]
        SSO = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("系统登录")]
        App = 2
    }
    public enum EnumSex
    {
        [Description("男")]
        M = 1,
        [Description("女")]
        F = 2
    }
    public enum EnumDiagType
    {
        [Description("入院诊断")]
        ryzd = 1,
        [Description("出院诊断")]
        cyzd = 2
    }
    public enum EnumRecordStu
    {
        [Description("未提交")]
        wtj = 0,
        [Description("已提交")]
        ytj = 1,
        [Description("退回")]
        th = 2,
        [Description("已签收")]
        yqs = 3,
        [Description("病案归档")]
        bagd = 4
    }
    /// <summary>
    /// trạng thái y lệnh
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// chờ duyệt 
        /// </summary>
        [Description("待审")]
        Ds = 0,
        /// <summary>
        /// Duyệt
        /// </summary>
        [Description("审核")]
        Sh = 1,
        /// <summary>
        /// thực hiện
        /// </summary>
        [Description("执行")]
        Zx = 2,
        /// <summary>
        /// rút lạiDC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// dừng hoặc hủy bỏ
        /// </summary>
        [Description("停止")]
        TZ = 4
    }
    public enum EnumYzlx
    {
        ///// <summary>
        ///// chung
        ///// </summary>
        //[Description("chung")]
        //Ty = 1,
        /// <summary>
        /// thuốc
        /// </summary>
        [Description("药品")]
        Yp = 2,
        /// <summary>
        /// văn bản
        /// </summary>
        [Description("文字")]
        Wz = 3,
        /// <summary>
        /// thuốc mang về khi xuất viện
        /// </summary>
        [Description("出院带药")]
        Cydy = 4,
        /// <summary>
        /// hạng mục
        /// </summary>
        [Description("项目")]
        sfxm = 5,
        /// <summary>
        /// hạng mục
        /// </summary>
        [Description("检验")]
        jy = 6,
        /// <summary>
        /// hạng mục
        /// </summary>
        [Description("检查")]
        jc = 7,
        /// <summary>
        /// hạng mục
        /// </summary>
        [Description("膳食医嘱")]
        ssyz = 8,
        /// <summary>
        /// phẫu thuật
        /// </summary>
        [Description("手术")]
        oper = 9,
        /// <summary>
        /// thuốc Đông y
        /// </summary>
        [Description("中草药")]
        zcy = 10,
        /// <summary>
        /// phục hồi chức năng
        /// </summary>
        [Description("康复")]
        rehab = 11

    }
    [Description("收信人类型")]
    public enum RecipientTypeEnum
    {
        [Description("用户")]
        user = 1,
        [Description("用户组")]
        usergroup = 2,
    }
    /// <summary>
    /// Loại nghiệp vụ của nhóm tin nhắn
    /// </summary>
    [Description("消息组业务类型")]
    public enum GroupYwlxEnum
    {
        [Description("通用")]
        None = 0,
        [Description("门诊")]
        Mz = 1,
        [Description("住院")]
        Zy = 2,
    }
    /// <summary>
    /// Loại thực thi hàng đợi
    /// </summary>
    [Description("队列执行类型")]
    public enum MsgQueueExecTypeEnum
    {
        [Description("即时任务")]
        Immediately = 1,
        [Description("延时任务")]
        Waitime = 2,
        [Description("定时任务")]
        Fixedtime = 3,
    }
    /// <summary>
    /// giới hạn phạm vi áp dụng của tin nhắn
    /// </summary>
    [Description("消息组适用范围")]
    public enum MsgNoticeRangeEnum
    {
        [Description("无限制")]
        None = 0,
        [Description("全院")]
        Org = 1,
        [Description("指定用户")]
        User = 2,
        [Description("消息组内")]
        Group = 10,
        [Description("科室范围")]
        Dept = 20,
        [Description("科室内部")]
        DeptGroup = 21,
        [Description("病区范围")]
        Area = 30,
        [Description("病区内部")]
        AreaGroup = 31,
    }
    /// <summary>
    /// Trạng thái tin nhắn
    /// </summary>
    [Description("消息状态")]
    public enum NoticeStuEnum
    {
        [Description("未发送")]
        UnSend = 0,
        [Description("已发送")]
        Send = 1,
        [Description("已读")]
        Read = 2,
        [Description("待处理")]
        Wait = 9
    }
    public enum ApplyTypeEnum
    {
        [Description("新建病历")]
        xjbl = 0,
        [Description("修改病历")]
        xgbl = 1
    }
    /// <summary>
    /// trạng thái phê duyệt đề nghị bệnh án
    /// </summary>
    public enum ApplyStatusEnum
    {
        /// <summary>
        /// chưa phê duyệt
        /// </summary>
        [Description("未批准")]
        wpz = 0,
        /// <summary>
        /// Đã phê duyệt
        /// </summary>
        [Description("已审批")]
        ysp = 1,
        /// <summary>
        /// trả lại
        /// </summary>
        [Description("退回")]
        th = 2
    }
}