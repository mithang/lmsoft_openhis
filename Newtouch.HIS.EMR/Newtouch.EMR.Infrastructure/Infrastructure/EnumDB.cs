using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Quyền sử dụng mẫu
    /// </summary>
    public enum Enummbqx
    {
        /// <summary>
        /// Chung
        /// </summary>
        [Description("Chung")]
        pub=0,
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("Cá nhân")]
        prv =1,
        /// <summary>
        /// Khoa
        /// </summary>
        [Description("Khoa")]
        dept =2
    }
    /// <summary>
    /// Phân bổ quyền mẫu
    /// </summary>
    public enum EnummbqxFp
    {
        /// <summary>
        /// Không có bất kỳ quyền nào
        /// </summary>
        [Description("Không có quyền")]
        non = 0,
        /// <summary>
        /// chỉ đọc
        /// </summary>
        [Description("Chỉ đọc")]
        read = 1,
        /// <summary>
        /// Đọc/ghi
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
    /// Trạng thái bệnh án
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
        //[Description("Lưu trữ bệnh án")]
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
    /// Đã kết hôn hay chưa
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// Chưa kết hôn
        /// </summary>
        [Description("Chưa kết hôn")]
        wh = 0,
        /// <summary>
        /// Đã kết hôn
        /// </summary>
        [Description("Đã kết hôn")]
        yh,
        /// <summary>
        /// Không rõ
        /// </summary>
        [Description("Không rõ")]
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
        [Description("Chưa khám")]
        NotYetTreate = 1,
        /// <summary>
        /// Đang khám
        /// </summary>
        [Description("Đang khám")]
        Treating = 2,
        /// <summary>
        /// Kết thúc khám
        /// </summary>
        [Description("Đã kết thúc khám")]
        Treated = 3,
    }
    /// <summary>
    /// Dấu hiệu nghiệp vụ loại lớn bệnh án và mẫu
    /// </summary>
    public enum EnumMzbz
    {
        /// <summary>
        /// Nội trú
        /// </summary>
        [Description("Nội trú")]
        zy = 0,
        /// <summary>
        /// Ngoại trú
        /// </summary>
        [Description("Ngoại trú")]
        mz = 1,
        /// <summary>
        /// Không giới hạn
        /// </summary>
        [Description("Không giới hạn")]
        all = 2,
    }
    /// <summary>
    /// Loại ngoại trú mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// Khám ngoại trú thông thường
        /// </summary>
        [Description("Ngoại trú")]
        generalOutpat = 1,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("Cấp cứu")]
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
        /// Chung
        /// </summary>
        [Description("Trình soạn thảo Duchang")]
        DcWriter = 0,
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("Chế độ xem độc lập")]
        View = 1
    }

    /// <summary>
	/// Cờ sử dụng
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
