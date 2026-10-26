using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Quyền sử dụng mẫu
    /// </summary>
    public enum Enummbqx
    {
        /// <summary>
        /// Dùng chung
        /// </summary>
        [Description("通用")]
        pub=0,
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("个人")]
        prv =1,
        /// <summary>
        /// Khoa
        /// </summary>
        [Description("科室")]
        dept =2
    }
    /// <summary>
    /// Phân bổ quyền mẫu
    /// </summary>
    public enum EnummbqxFp
    {
        /// <summary>
        /// Không có quyền nào
        /// </summary>
        [Description("无权限")]
        non = 0,
        /// <summary>
        /// Chỉ đọc
        /// </summary>
        [Description("只读")]
        read = 1,
        /// <summary>
        /// Đọc/ghi
        /// </summary>
        [Description("读写")]
        edit = 2
    }

    public enum EnumSex
    {
        [Description("男")]
        M =1,
        [Description("女")]
        F =2
    }
    /// <summary>
    /// Trạng thái bệnh án
    /// </summary>
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
        //[Description("Lưu trữ hồ sơ bệnh án")]
        //bagd = 4
    }

    public enum EnumPlanStu {
        [Description("进行中")]
        zc = 0,
        [Description("已停止")]
        tz = 1,
        [Description("停止取消")]
        tzqx = 2,
    }

    /// <summary>
    /// Đã kết hôn hay chưa
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// Chưa kết hôn
        /// </summary>
        [Description("未婚")]
        wh = 0,
        /// <summary>
        /// Đã kết hôn
        /// </summary>
        [Description("已婚")]
        yh,
        /// <summary>
        /// Không rõ
        /// </summary>
        [Description("不详")]
        UnKnown = 9,
    }

    /// <summary>
    /// Trạng thái khám ngoại trú
    /// </summary>
    public enum EnumJzzt
    {
        /// <summary>
        /// Chưa khám
        /// </summary>
        [Description("未就诊")]
        NotYetTreate = 1,
        /// <summary>
        /// Đang khám
        /// </summary>
        [Description("就诊中")]
        Treating = 2,
        /// <summary>
        /// Khám kết thúc
        /// </summary>
        [Description("就诊结束")]
        Treated = 3,
    }
    /// <summary>
    /// Loại lớn bệnh án và mẫu - cờ nghiệp vụ
    /// </summary>
    public enum EnumMzbz
    {
        /// <summary>
        /// Nội trú
        /// </summary>
        [Description("住院")]
        zy = 0,
        /// <summary>
        /// Ngoại trú
        /// </summary>
        [Description("门诊")]
        mz = 1,
        /// <summary>
        /// Không giới hạn
        /// </summary>
        [Description("不限")]
        all = 2,
    }
    /// <summary>
    /// Loại ngoại trú mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// Ngoại trú thông thường
        /// </summary>
        [Description("门诊")]
        generalOutpat = 1,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("急诊")]
        emerDiagnosis = 2,
        ///// <summary>
        ///// Phòng khám chuyên gia
        ///// </summary>
        //[Description("Phòng khám chuyên gia")]
        //expertOutpat = 3,
        ///// <summary>
        ///// Phòng khám chuyên gia
        ///// </summary>
        //[Description("Phòng khám bệnh đặc biệt")]
        //SpecialOutpat = 4,
        ///// <summary>
        ///// Phòng khám bệnh nặng
        ///// </summary>
        //[Description("Phòng khám bệnh nặng")]
        //MajorDiseases = 5,
        ///// <summary>
        ///// Phòng khám bệnh mạn tính
        ///// </summary>
        //[Description("Phòng khám bệnh mạn tính")]
        //ChronicDisease = 6
    }
    /// <summary>
    /// Cách tải mẫu
    /// </summary>
    public enum EnummbqxTempLoadWay
    {
        /// <summary>
        /// Dùng chung
        /// </summary>
        [Description("都昌编辑器")]
        DcWriter = 0,
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("独立视图")]
        View = 1
    }

    /// <summary>
	/// Cờ sử dụng
	/// </summary>
	public enum EnumSybz
    {
        [Description("是")]
        Y = 1,
        [Description("否")]
        N = 0
    }
    public enum EnumBlys
    {
        [Description("目录")]
        ml = -1,
        [Description("文本")]
        wb = 0,
        [Description("下拉")]
        xl = 1,
        [Description("下拉多选")]
        xldx = 2,
        [Description("日期(yyyy-MM-dd)")]
        rq = 3,
        [Description("时间(HH:mm)")]
        sj = 4,
        [Description("日期时间(yyyy-MM-dd HH:mm)")]
        rqsj = 5,
        [Description("数值")]
        sz = 6
    }
    public enum ApplyStatusEnum
    {
        /// <summary>
        /// Chưa phê duyệt
        /// </summary>
        [Description("未批准")]
        wpz = 0,
        /// <summary>
        /// Đã phê duyệt
        /// </summary>
        [Description("已审批")]
        ysp = 1,
        /// <summary>
        /// Trả lại
        /// </summary>
        [Description("退回")]
        th = 2
    }
}