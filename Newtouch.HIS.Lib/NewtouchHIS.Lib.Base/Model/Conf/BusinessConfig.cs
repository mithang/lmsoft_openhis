using NewtouchHIS.Lib.Base.Attributes;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Lib.Base.Model
{
    public class BusinessConfig
    {
        /// <summary>
        /// Giới hạn thời gian trực tuyến
        /// </summary>
        public int? LoginStatusKeepMinute { get; set; } = null;
        /// <summary>
        /// Bật giới hạn đăng nhập nhiều thiết bị
        /// </summary>
        public bool? EnableMultipleLoginLimit { get; set; } = null;
        /// <summary>
        /// Giới hạn số lượng đăng nhập đồng thời
        /// </summary>
        public int? MultipleLoginLimit_Count { get; set; } = null;
        /// <summary>
        /// Khoảng thời gian tài khoản tự động bị khóa khi số lần đăng nhập quá nhiều (đơn vị: phút)
        /// </summary>
        public int? LoginFailedTimesLimit_Minutes { get; set; } = null;
        /// <summary>
        /// Số lần tối đa tài khoản tự động bị khóa khi số lần đăng nhập quá nhiều
        /// </summary>
        public int? LoginFailedTimesLimit_Count { get; set; } = null;
        /// <summary>
        /// Mã tổ chức cấp cao nhất
        /// </summary>
        public string? Top_OrganizeId { get; set; } = null;
        /// <summary>
        /// Ứng dụng hiện tạiAppId
        /// </summary>
        public string? AppId { get; set; } = null;
        /// <summary>
        /// Tên ứng dụng
        /// </summary>
        public string? AppName { get; set; } = null;
        /// <summary>
        /// Có bật mã xác minh không 
        /// </summary>
        public bool? Is_CheckChkCode { get; set; } = null;
        /// <summary>
        /// Định danh bộ nhớ đệm token người dùng
        /// </summary>
        public string? Cache_UserToken { get; set; } = null;
        /// <summary>
        /// Bật giám sát nhật ký trọng điểm
        /// </summary>
        public bool? EnableLoggingMonitorAttr { get; set; } = false;
        /// <summary>
        /// Giám sát nhật ký trọng điểm có kèm Header không, mặc địnhfalse
        /// </summary>
        public bool? EnableLoggingMonitorWithHeaderAttr { get; set; } = false;
        /// <summary>
        /// Giao diện API của các hệ thống
        /// </summary>
        public AppAPIHostConfig? AppAPIHost { get; set; } = null;
        /// <summary>
        /// Các hệ thốngAPI AppId
        /// </summary>
        public AppAPIHostConfig? AppAPIHostName { get; set; } = null;
        /// <summary>
        /// HISĐịa chỉ đổi Token ủy quyền thống nhất của hệ thống cũ
        /// </summary>
        public string? UALoginAddress { get; set; }
        /// <summary>
        /// Của Token xin ủy quyềnAppId
        /// </summary>
        public string[]? AuthAppIds { get; set; } = null;
        /// <summary>
        /// DRGPhiên bản bộ nhóm
        /// </summary>
        public string? DrgGroupVersion { get; set; } = null;
        /// <summary>
        /// Cấu hình liên miền
        /// </summary>
        public string? CorsWithOrigins { get; set; } = null;
        /// <summary>
        /// Quản lý tệp
        /// </summary>
        public FileManageConfig? FileConfig { get; set; } = null;
        /// <summary>
        /// Giao diện dịch vụ bên thứ ba hội chẩn từ xa, nếu trống thì mặc định giá trị ban đầu
        /// </summary>
        public RemoteTreatedConfig? RemoteTreated { get; set; } = null;
    }

    public class AppAPIHostConfig
    {
        /// <summary>
        /// Webtài nguyên
        /// </summary>
        public string? SiteStaticHost { get; set; } = null;
        /// <summary>
        /// Quản lý hậu trườngWeb
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? SiteBaseWebHost { get; set; } = null;
        /// <summary>
        /// Quản lý hậu trườngAPI
        /// </summary>      
        [DbTag(DBEnum.BaseDb)]
        public string? SiteBaseAPIHost { get; set; } = null;

        /// <summary>
        /// Hệ thống quyết toánWeb
        /// </summary>
        [DbTag(DBEnum.SettDb)]
        public string? SiteSettWebHost { get; set; } = null;
        /// <summary>
        /// Hệ thống quyết toánAPI
        /// </summary>
        [DbTag(DBEnum.SettDb)]
        public string? SiteSettAPIHost { get; set; } = null;
        /// <summary>
        /// Phối hợp y tế - điều dưỡngWeb
        /// </summary>
        [DbTag(DBEnum.CisDb)]
        public string? SiteCisWebHost { get; set; } = null;
        /// <summary>
        /// Phối hợp y tế - điều dưỡngAPI
        /// </summary>
        [DbTag(DBEnum.CisDb)]
        public string? SiteCisAPIHost { get; set; } = null;
        /// <summary>
        /// Nhà thuốc - kho thuốcWeb
        /// </summary>
        [DbTag(DBEnum.PdsDb)]
        public string? SiteYfykWebHost { get; set; } = null;
        /// <summary>
        /// Nhà thuốc - kho thuốcAPI
        /// </summary>
        [DbTag(DBEnum.PdsDb)]
        public string? SiteYfykAPIHost { get; set; } = null;
        /// <summary>
        /// Bệnh án điện tửWeb
        /// </summary>
        [DbTag(DBEnum.EmrDb)]
        public string? SiteEmrWebHost { get; set; } = null;
        /// <summary>
        /// Bệnh án điện tửAPI
        /// </summary>
        [DbTag(DBEnum.EmrDb)]
        public string? SiteEmrAPIHost { get; set; } = null;
        /// <summary>
        /// Hệ thống bệnh ánWeb
        /// </summary>
        [DbTag(DBEnum.MrmsDb)]
        public string? SiteMrmsWebHost { get; set; } = null;
        /// <summary>
        /// Hệ thống bệnh ánAPI
        /// </summary>
        [DbTag(DBEnum.MrmsDb)]
        public string? SiteMrmsAPIHost { get; set; } = null;
        /// <summary>
        /// Hệ thống phẫu thuậtWeb
        /// </summary>
        [DbTag(DBEnum.OrDb)]
        public string? SiteOrWebHost { get; set; } = null;
        /// <summary>
        /// Hệ thống phẫu thuậtAPI
        /// </summary>
        [DbTag(DBEnum.OrDb)]
        public string? SiteOrAPIHost { get; set; } = null;
        /// <summary>
        /// Hệ thống vật tư tiêu haoWeb
        /// </summary>
        public string? SiteHerpWebHost { get; set; } = null;
        /// <summary>
        /// Vật tư tiêu haoAPI
        /// </summary>
        public string? SiteHerpAPIHost { get; set; } = null;
        /// <summary>
        /// Hệ thống kiểm soát chất lượngWeb
        /// </summary>
        [DbTag(DBEnum.MrQcDb)]
        public string? SiteMrqcWebHost { get; set; } = null;
        /// <summary>
        /// Hệ thống kiểm soát chất lượngAPI
        /// </summary>
        [DbTag(DBEnum.MrQcDb)]
        public string? SiteMrqcAPIHost { get; set; } = null;
        /// <summary>
        /// Nền tảng thống nhất
        /// </summary>
        [DbTag(DBEnum.UnionDb)]
        public string? HisSSOHost { get; set; } = null;
        /// <summary>
        /// Hệ thống lập lịch nhiệm vụ
        /// </summary>
        public string? ScheduleJobHost { get; set; } = null;
        /// <summary>
        /// Trung tâm tin nhắn
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? SiteNoticeCenterHost { get; set; } = null;
        /// <summary>
        /// API cơ sở chung của hệ thống (phụ thuộc framework)
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? HisAppBaseAPIHost { get; set; } = null;
        /// <summary>
        /// Trung tâm giao diện
        /// </summary>
        public string? SiteApiManageHost { get; set; } = null;
        /// <summary>
        /// Trung tâm xác thực
        /// </summary>
        public string? SiteAuthCenterHost { get; set; } = null;
        /// <summary>
        /// Địa chỉ giao diện âm thanh - video bên ngoài
        /// </summary>
        public string? OuterMediaMeettingAPIHost { get; set; } = null;
        /// <summary>
        /// Phòng khám đám mây
        /// </summary>
        public string? OuterClinicSystemAPIHost { get; set; } = null;
        /// <summary>
        /// Giao diện PACS bên thứ ba
        /// </summary>
        public string? OuterPacsServiceHost { get; set; } = null;
        /// <summary>
        /// Giao diện Lis bên thứ ba
        /// </summary>
        public string? OuterLisServiceHost { get; set; } = null;

    }
}
/// <summary>
/// Cấu hình liên quan đến tệp
/// </summary>
public class FileManageConfig
{
    /// <summary>
    /// Đường dẫn mặc định của tệp cục bộ
    /// </summary>
    public string? LocalFileBaseDir { get; set; }
    /// <summary>
    /// Đường dẫn truy cập mạng mặc định của tệp cục bộHost
    /// </summary>
    public string? LocalFileBaseNetUrl { get; set; }
    /// <summary>
    /// DrgThư mục tải lên
    /// </summary>
    public string? DrgDataFileDir { get; set; }
    /// <summary>
    /// DrgThư mục đầy đủ
    /// </summary>
    public string? DrgDataFileDirPath
    {
        get
        {
            return $"{LocalFileBaseDir}{DrgDataFileDir}";
        }
    }
    /// <summary>
    /// DrgThư mục (ngày)
    /// </summary>
    public string? DrgDataFileDirPathWithDate
    {
        get
        {
            return $"{LocalFileBaseDir}{DrgDataFileDir}\\{DateTime.Now:yyyyMMdd}" + "\\";
        }
    }
}

public class RemoteTreatedConfig
{
    public string? SKey { get; set; }
    public string? Organization { get; set; }
}

public class AppAPIHostDic
{
    public string AppId { get; set; }
    public string Host { get; set; }
    public List<AppAPIMethods> Methods { get; set; }
}

public class AppAPIMethods
{
    public string Method { get; set; }
    public string Route { get; set; }
}

