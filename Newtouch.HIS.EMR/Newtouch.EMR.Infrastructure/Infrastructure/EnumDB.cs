using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 模板使用权限
    /// </summary>
    public enum Enummbqx
    {
        /// <summary>
        /// 通用
        /// </summary>
        [Description("Chung")]
        pub=0,
        /// <summary>
        /// 个人
        /// </summary>
        [Description("Cá nhân")]
        prv =1,
        /// <summary>
        /// 科室
        /// </summary>
        [Description("Khoa")]
        dept =2
    }
    /// <summary>
    /// 模板权限分配
    /// </summary>
    public enum EnummbqxFp
    {
        /// <summary>
        /// 无任何权限
        /// </summary>
        [Description("Không có quyền")]
        non = 0,
        /// <summary>
        /// 只读
        /// </summary>
        [Description("Chỉ đọc")]
        read = 1,
        /// <summary>
        /// 读写
        /// </summary>
        [Description("Đọc/ghi")]
        edit = 2
    }

    public enum EnumSex
    {
        [Description("Nam")]
        M =1,
        [Description("Nữ")]
        F =2
    }
    /// <summary>
    /// 病历状态
    /// </summary>
    public enum EnumRecordStu
    {
        [Description("Chưa nộp")]
        wtj = 0,
        [Description("Đã nộp")]
        ytj = 1,
        [Description("Trả lại")]
        th = 2,
        [Description("Đã nhận")]
        yqs = 3,
        //[Description("病案归档")]
        //bagd = 4
    }

    public enum EnumPlanStu {
        [Description("Đang thực hiện")]
        zc = 0,
        [Description("Đã dừng")]
        tz = 1,
        [Description("Hủy dừng")]
        tzqx = 2,
    }

    /// <summary>
    /// 婚否
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// 未婚
        /// </summary>
        [Description("Chưa kết hôn")]
        wh = 0,
        /// <summary>
        /// 已婚
        /// </summary>
        [Description("Đã kết hôn")]
        yh,
        /// <summary>
        /// 不详
        /// </summary>
        [Description("Không rõ")]
        UnKnown = 9,
    }

    /// <summary>
    /// 门诊就诊状态
    /// </summary>
    public enum EnumJzzt
    {
        /// <summary>
        /// 未就诊
        /// </summary>
        [Description("Chưa khám")]
        NotYetTreate = 1,
        /// <summary>
        /// 就诊中
        /// </summary>
        [Description("Đang khám")]
        Treating = 2,
        /// <summary>
        /// 就诊结束
        /// </summary>
        [Description("Đã kết thúc khám")]
        Treated = 3,
    }
    /// <summary>
    /// 病历大类及模板 业务标志
    /// </summary>
    public enum EnumMzbz
    {
        /// <summary>
        /// 住院
        /// </summary>
        [Description("Nội trú")]
        zy = 0,
        /// <summary>
        /// 门诊
        /// </summary>
        [Description("Ngoại trú")]
        mz = 1,
        /// <summary>
        /// 不限
        /// </summary>
        [Description("Không giới hạn")]
        all = 2,
    }
    /// <summary>
    /// 门诊类型 mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// 普通门诊
        /// </summary>
        [Description("Ngoại trú")]
        generalOutpat = 1,
        /// <summary>
        /// 急诊
        /// </summary>
        [Description("Cấp cứu")]
        emerDiagnosis = 2,
        ///// <summary>
        ///// 专家门诊
        ///// </summary>
        //[Description("专家门诊")]
        //expertOutpat = 3,
        ///// <summary>
        ///// 专家门诊
        ///// </summary>
        //[Description("特病门诊")]
        //SpecialOutpat = 4,
        ///// <summary>
        ///// 重大疾病门诊
        ///// </summary>
        //[Description("重大疾病门诊")]
        //MajorDiseases = 5,
        ///// <summary>
        ///// 慢性病门诊
        ///// </summary>
        //[Description("慢性病门诊")]
        //ChronicDisease = 6
    }
    /// <summary>
    /// 模板加载方式
    /// </summary>
    public enum EnummbqxTempLoadWay
    {
        /// <summary>
        /// 通用
        /// </summary>
        [Description("都昌编辑器")]
        DcWriter = 0,
        /// <summary>
        /// 个人
        /// </summary>
        [Description("Chế độ xem độc lập")]
        View = 1
    }

    /// <summary>
	/// 使用标志
	/// </summary>
	public enum EnumSybz
    {
        [Description("Có")]
        Y = 1,
        [Description("Không")]
        N = 0
    }
    public enum EnumBlys
    {
        [Description("Danh mục")]
        ml = -1,
        [Description("Văn bản")]
        wb = 0,
        [Description("Danh sách thả xuống")]
        xl = 1,
        [Description("Danh sách thả xuống nhiều chọn")]
        xldx = 2,
        [Description("Ngày (yyyy-MM-dd)")]
        rq = 3,
        [Description("Giờ (HH:mm)")]
        sj = 4,
        [Description("Ngày giờ (yyyy-MM-dd HH:mm)")]
        rqsj = 5,
        [Description("Số")]
        sz = 6
    }
    public enum ApplyStatusEnum
    {
        /// <summary>
        /// 未批准
        /// </summary>
        [Description("Chưa phê duyệt")]
        wpz = 0,
        /// <summary>
        /// 已审批
        /// </summary>
        [Description("Đã phê duyệt")]
        ysp = 1,
        /// <summary>
        /// 退回
        /// </summary>
        [Description("Trả lại")]
        th = 2
    }
}
