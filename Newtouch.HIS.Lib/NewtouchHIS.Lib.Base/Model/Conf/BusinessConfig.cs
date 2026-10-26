using NewtouchHIS.Lib.Base.Attributes;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Lib.Base.Model
{
    public class BusinessConfig
    {
        /// <summary>
        /// giới hạn thời lượng trực tuyến
        /// </summary>
        public int? LoginStatusKeepMinute { get; set; } = null;
        /// <summary>
        /// bật giới hạn đăng nhập đa thiết bị
        /// </summary>
        public bool? EnableMultipleLoginLimit { get; set; } = null;
        /// <summary>
        /// giới hạn số lượng đăng nhập đồng bộ
        /// </summary>
        public int? MultipleLoginLimit_Count { get; set; } = null;
        /// <summary>
        /// số lần đăng nhập quá nhiều，tài khoản tự động khóa của khoảng thời gian（đơn vị：phân）
        /// </summary>
        public int? LoginFailedTimesLimit_Minutes { get; set; } = null;
        /// <summary>
        /// số lần đăng nhập quá nhiều，tài khoản tự động khóa của số lần tối đa
        /// </summary>
        public int? LoginFailedTimesLimit_Count { get; set; } = null;
        /// <summary>
        /// mã tổ chức cấp cao nhất
        /// </summary>
        public string? Top_OrganizeId { get; set; } = null;
        /// <summary>
        /// ứng dụng hiện tạiAppId
        /// </summary>
        public string? AppId { get; set; } = null;
        /// <summary>
        /// tên ứng dụng
        /// </summary>
        public string? AppName { get; set; } = null;
        /// <summary>
        /// có bật mã xác minh hay không 
        /// </summary>
        public bool? Is_CheckChkCode { get; set; } = null;
        /// <summary>
        /// người dùngtokenđánh dấu bộ nhớ đệm
        /// </summary>
        public string? Cache_UserToken { get; set; } = null;
        /// <summary>
        /// bật giám sát nhật ký trọng điểm
        /// </summary>
        public bool? EnableLoggingMonitorAttr { get; set; } = false;
        /// <summary>
        /// giám sát nhật ký trọng điểm có mang theo hay khôngHeader，mặc địnhfalse
        /// </summary>
        public bool? EnableLoggingMonitorWithHeaderAttr { get; set; } = false;
        /// <summary>
        /// các hệ thốngAPIgiao diện
        /// </summary>
        public AppAPIHostConfig? AppAPIHost { get; set; } = null;
        /// <summary>
        /// các hệ thốngAPI AppId
        /// </summary>
        public AppAPIHostConfig? AppAPIHostName { get; set; } = null;
        /// <summary>
        /// HIShệ thống cũ ủy quyền thống nhấtTokenđổi lấy địa chỉ
        /// </summary>
        public string? UALoginAddress { get; set; }
        /// <summary>
        /// đơn xin ủy quyềnTokencủaAppId
        /// </summary>
        public string[]? AuthAppIds { get; set; } = null;
        /// <summary>
        /// DRGphiên bản bộ phân nhóm
        /// </summary>
        public string? DrgGroupVersion { get; set; } = null;
        /// <summary>
        /// cấu hình đa miền
        /// </summary>
        public string? CorsWithOrigins { get; set; } = null;
        /// <summary>
        /// quản lý tệp
        /// </summary>
        public FileManageConfig? FileConfig { get; set; } = null;
        /// <summary>
        /// giao diện dịch vụ bên thứ ba hội chẩn từ xa，nếu trống thì mặc định giá trị ban đầu
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
        /// quản trị phía sauWeb
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? SiteBaseWebHost { get; set; } = null;
        /// <summary>
        /// quản trị phía sauAPI
        /// </summary>      
        [DbTag(DBEnum.BaseDb)]
        public string? SiteBaseAPIHost { get; set; } = null;

        /// <summary>
        /// hệ thống thanh toánWeb
        /// </summary>
        [DbTag(DBEnum.SettDb)]
        public string? SiteSettWebHost { get; set; } = null;
        /// <summary>
        /// hệ thống thanh toánAPI
        /// </summary>
        [DbTag(DBEnum.SettDb)]
        public string? SiteSettAPIHost { get; set; } = null;
        /// <summary>
        /// hợp tác y tế - điều dưỡngWeb
        /// </summary>
        [DbTag(DBEnum.CisDb)]
        public string? SiteCisWebHost { get; set; } = null;
        /// <summary>
        /// hợp tác y tế - điều dưỡngAPI
        /// </summary>
        [DbTag(DBEnum.CisDb)]
        public string? SiteCisAPIHost { get; set; } = null;
        /// <summary>
        /// nhà thuốc - kho thuốcWeb
        /// </summary>
        [DbTag(DBEnum.PdsDb)]
        public string? SiteYfykWebHost { get; set; } = null;
        /// <summary>
        /// nhà thuốc - kho thuốcAPI
        /// </summary>
        [DbTag(DBEnum.PdsDb)]
        public string? SiteYfykAPIHost { get; set; } = null;
        /// <summary>
        /// bệnh án điện tửWeb
        /// </summary>
        [DbTag(DBEnum.EmrDb)]
        public string? SiteEmrWebHost { get; set; } = null;
        /// <summary>
        /// bệnh án điện tửAPI
        /// </summary>
        [DbTag(DBEnum.EmrDb)]
        public string? SiteEmrAPIHost { get; set; } = null;
        /// <summary>
        /// hệ thống bệnh ánWeb
        /// </summary>
        [DbTag(DBEnum.MrmsDb)]
        public string? SiteMrmsWebHost { get; set; } = null;
        /// <summary>
        /// hệ thống bệnh ánAPI
        /// </summary>
        [DbTag(DBEnum.MrmsDb)]
        public string? SiteMrmsAPIHost { get; set; } = null;
        /// <summary>
        /// hệ thống phẫu thuậtWeb
        /// </summary>
        [DbTag(DBEnum.OrDb)]
        public string? SiteOrWebHost { get; set; } = null;
        /// <summary>
        /// hệ thống phẫu thuậtAPI
        /// </summary>
        [DbTag(DBEnum.OrDb)]
        public string? SiteOrAPIHost { get; set; } = null;
        /// <summary>
        /// hệ thống vật tư tiêu haoWeb
        /// </summary>
        public string? SiteHerpWebHost { get; set; } = null;
        /// <summary>
        /// vật tư tiêu haoAPI
        /// </summary>
        public string? SiteHerpAPIHost { get; set; } = null;
        /// <summary>
        /// hệ thống kiểm soát chất lượngWeb
        /// </summary>
        [DbTag(DBEnum.MrQcDb)]
        public string? SiteMrqcWebHost { get; set; } = null;
        /// <summary>
        /// hệ thống kiểm soát chất lượngAPI
        /// </summary>
        [DbTag(DBEnum.MrQcDb)]
        public string? SiteMrqcAPIHost { get; set; } = null;
        /// <summary>
        /// nền tảng thống nhất
        /// </summary>
        [DbTag(DBEnum.UnionDb)]
        public string? HisSSOHost { get; set; } = null;
        /// <summary>
        /// hệ thống lập lịch nhiệm vụ
        /// </summary>
        public string? ScheduleJobHost { get; set; } = null;
        /// <summary>
        /// trung tâm tin nhắn
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? SiteNoticeCenterHost { get; set; } = null;
        /// <summary>
        /// cơ sở dùng chung của hệ thốngAPI（phụ thuộc khung）
        /// </summary>
        [DbTag(DBEnum.BaseDb)]
        public string? HisAppBaseAPIHost { get; set; } = null;
        /// <summary>
        /// trung tâm giao diện
        /// </summary>
        public string? SiteApiManageHost { get; set; } = null;
        /// <summary>
        /// trung tâm xác thực
        /// </summary>
        public string? SiteAuthCenterHost { get; set; } = null;
        /// <summary>
        /// địa chỉ giao diện âm thanh - hình ảnh bên ngoài
        /// </summary>
        public string? OuterMediaMeettingAPIHost { get; set; } = null;
        /// <summary>
        /// phòng khám đám mây
        /// </summary>
        public string? OuterClinicSystemAPIHost { get; set; } = null;
        /// <summary>
        /// bên thứ baPACS giao diện
        /// </summary>
        public string? OuterPacsServiceHost { get; set; } = null;
        /// <summary>
        /// bên thứ baLis giao diện
        /// </summary>
        public string? OuterLisServiceHost { get; set; } = null;

    }
}
/// <summary>
/// cấu hình liên quan đến tệp
/// </summary>
public class FileManageConfig
{
    /// <summary>
    /// đường dẫn mặc định của tệp cục bộ
    /// </summary>
    public string? LocalFileBaseDir { get; set; }
    /// <summary>
    /// đường dẫn truy cập mạng mặc định của tệp cục bộHost
    /// </summary>
    public string? LocalFileBaseNetUrl { get; set; }
    /// <summary>
    /// Drgthư mục tải lên
    /// </summary>
    public string? DrgDataFileDir { get; set; }
    /// <summary>
    /// Drgthư mục đầy đủ
    /// </summary>
    public string? DrgDataFileDirPath
    {
        get
        {
            return $"{LocalFileBaseDir}{DrgDataFileDir}";
        }
    }
    /// <summary>
    /// Drgthư mục（ngày tháng）
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

