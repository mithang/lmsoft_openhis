using System.ComponentModel;

namespace Newtouch.Infrastructure
{

    /// <summary>
    /// Loại ghi nhật ký
    /// </summary>
    public enum DbLogType
    {
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        Other = 0,
        /// <summary>
        /// Đăng nhập
        /// </summary>
        [Description("Đăng nhập")]
        Login = 1,
        /// <summary>
        /// Đăng xuất
        /// </summary>
        [Description("Đăng xuất")]
        Exit = 2,
        /// <summary>
        /// Truy cập
        /// </summary>
        [Description("Truy cập")]
        Visit = 3,
        /// <summary>
        /// Thêm mới
        /// </summary>
        [Description("Thêm mới")]
        Create = 4,
        /// <summary>
        /// Xóa
        /// </summary>
        [Description("Xóa")]
        Delete = 5,
        /// <summary>
        /// Sửa
        /// </summary>
        [Description("Sửa")]
        Update = 6,
        /// <summary>
        /// Gửi
        /// </summary>
        [Description("Gửi")]
        Submit = 7,
        /// <summary>
        /// Ngoại lệ
        /// </summary>
        [Description("Ngoại lệ")]
        Exception = 8,
    }

    /// <summary>
    /// Tính chất tự trả
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Được thanh toán
        /// </summary>
        [Description("Được thanh toán")]
        KB = 0,
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("Tự trả")]
        ZF = 1,
        /// <summary>
        /// Tự trả theo loại
        /// </summary>
        [Description("Tự trả theo loại")]
        FLZF = 2,
        /// <summary>
        /// Tự túc hoàn toàn
        /// </summary>
        [Description("Tự túc hoàn toàn")]
        JueDuiZiLi = 3,
        /// <summary>
        /// Loại A
        /// </summary>
        [Description("Loại A")]
        Jia,
        /// <summary>
        /// Loại B
        /// </summary>
        [Description("Loại B")]
        Yi,
        /// <summary>
        /// Loại C
        /// </summary>
        [Description("Loại C")]
        Bing,
    }
    /// <summary>
    /// Tính chất tự trả v2 
    /// Trùng Khánh dùng lại
    /// </summary>
    public enum EnumZFXZv2 {
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("Tự trả")]
        ZF = 1,
        /// <summary>
        /// Loại A
        /// </summary>
        [Description("Loại A")]
        J = 4,
        /// <summary>
        /// Loại B
        /// </summary>
        [Description("Loại B")]
        Y = 5,
        /// <summary>
        /// Loại C
        /// </summary>
        [Description("Loại C")]
        B = 6,
    }

    /// <summary>
    /// Loại làm tròn ghi sổ khu bệnh
    /// </summary>
    public enum EnumBQJZQZ
    {
        /// <summary>
        /// Làm tròn theo ngày
        /// </summary>
        [Description("Làm tròn theo ngày")]
        Day = 1,
        /// <summary>
        /// Làm tròn theo lần
        /// </summary>
        [Description("Làm tròn theo lần")]
        Times
    }

    /// <summary>
    /// Hạng mục thu phí phục hồi chức năng Phương thức tính phí
    /// </summary>
    public enum EnumCollectionMethod
    {
        /// <summary>
        /// Số lần dịch vụ
        /// </summary>
        [Description("Số lần dịch vụ")]
        Service = 1,
        /// <summary>
        /// Thời lượng
        /// </summary>
        [Description("Thời lượng")]
        Time
    }

    /// <summary>
    /// Nhóm thu phí lớn Loại nhóm lớn
    /// </summary>
    public enum EnumSfdlDllb
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Thuốc")]
        yp = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Hạng mục điều trị")]
        zlxm,
        /// <summary>
        /// 
        /// </summary>
        [Description("Hạng mục không điều trị")]
        flzxm
    }
    /// <summary>
    /// Phân loại cha thu phí
    /// </summary>
    public enum EnumSffjdlCode
    {
        [Description("Phí thuốc")]
        ypf=1001,
        [Description("Phí khám")]
        jcf,
        [Description("Phí xét nghiệm")]
        jyf,
        [Description("Phí điều trị")]
        zlf,
        [Description("Phí giường bệnh")]
        cwf,
        [Description("Phí vật tư")]
        clf,
        [Description("Phí gây mê")]
        mzf
    }
    public enum EnumSffjdlmc
    {
        药品费 = 1001,
        检查费,
        检验费,
        治疗费,
        床位费,
        材料费,
        麻醉费
    }
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

    /// <summary>
    /// Chiến lược tính giá hạng mục thu phí
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// Theo thời lượng
        /// </summary>
        [Description("Theo thời lượng")]
        Time = 1,
        /// <summary>
        /// Theo số lượng
        /// </summary>
        [Description("Theo số lượng")]
        Amount,
        /// <summary>
        /// Theo diện tích
        /// </summary>
        [Description("Theo diện tích")]
        Acreage,
    }

    /// <summary>
    /// Loại đích menu
    /// </summary>
    public enum EnumModuleTargetType
    {
        /// <summary>
        /// Không có trang
        /// </summary>
        expand,
        /// <summary>
        /// Trang khung
        /// </summary>
        iframe,
        /// <summary>
        /// Trang bật lên
        /// </summary>
        open,
        /// <summary>
        /// Cửa sổ mới
        /// </summary>
        blank,
        /// <summary>
        /// Trang con（Không hiển thị trong menu）
        /// </summary>
        subpage,
        /// <summary>
        /// Cửa sổ hiện tại
        /// </summary>
        toplocation,
    }

    /// <summary>
    /// Loại giường bệnh Nam nữ lẫn
    /// </summary>
    public enum EnumWardBedType
    {
        /// <summary>
        /// Nam
        /// </summary>
        [Description("Nam")]
        male = 1,
        /// <summary>
        /// Nữ
        /// </summary>
        [Description("Nữ")]
        female = 2,
        /// <summary>
        /// Hỗn hợp
        /// </summary>
        [Description("Hỗn hợp")]
        Mix = 3
    }

    public enum EnumTcfw
    {
        [Description("Cá nhân")]
        Person = 1,
        [Description("Khoa")]
        Dept = 2,
        [Description("Khu bệnh")]
        Ward = 3,
        [Description("Toàn viện")]
        Hosp = 4
    }

    /// <summary>
    /// Cờ xuất/nhập kho
    /// </summary>
    public enum EnumCrkbz
    {
        [Description("Nhập kho")]
        In = 0,
        [Description("Xuất kho")]
        Out = 1,
    }

    public enum EnumJybz
    {
        [Description("Thuốc cơ bản quốc gia")]
        Gjy = 1,
        [Description("Thuốc cơ bản bổ sung Quý An")]
        Gazbjy = 2,
	    [Description("Thuốc không cơ bản")]
	    Fjy = 3,
		[Description("Không thuộc các loại trên")]
        dbs = 4,
    }

    public enum EnumZjlx
    {
        [Description("CMND/CCCD")]
        Sfz = 1,
    }

    public enum EnumYpsx {
        [Description("Thuốc gây mê")]
        Mzyp = 9,
        [Description("Thuốc độc")]
        Dxyp = 4,
        [Description("Thuốc hướng thần loại 1")]
        Jyyp = 5,
        [Description("Thuốc hướng thần loại 2")]
        Jeyp = 6,
    }

    public enum EnumKss {

        [Description("Thuốc dùng không hạn chế")]
        Fxzsyyw = 0,
        [Description("Thuốc dùng hạn chế")]
        Xzsyyw = 1,
        [Description("Thuốc sử dụng đặc biệt")]
        Tssyyw = 2,
    }

	public enum EnumSSLX
	{

		[Description("Loại chẩn đoán")]
		zdx = 0,
		[Description("Loại điều trị")]
		zlx = 1,
		[Description("Loại thủ thuật")]
		czx = 2,
		[Description("Phẫu thuật")]
		ss = 3,
	}
    /// <summary>
    /// Trạng thái kết nối người dùng tin nhắn
    /// </summary>
	public enum EnumNoticeUserConnStu
	{

		[Description("Đã kết nối")]
		conn = 1,
		[Description("Ngoại tuyến")]
        disconn = 2,
		[Description("Kết nối lại")]
		reconn = 3,
	}
}
