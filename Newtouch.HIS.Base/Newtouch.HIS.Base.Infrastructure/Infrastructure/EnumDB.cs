using System.ComponentModel;

namespace Newtouch.Infrastructure
{

    /// <summary>
    /// 日志记录类型
    /// </summary>
    public enum DbLogType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("Khác")]
        Other = 0,
        /// <summary>
        /// 登录
        /// </summary>
        [Description("Đăng nhập")]
        Login = 1,
        /// <summary>
        /// 退出
        /// </summary>
        [Description("Đăng xuất")]
        Exit = 2,
        /// <summary>
        /// 访问
        /// </summary>
        [Description("Truy cập")]
        Visit = 3,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("Thêm mới")]
        Create = 4,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("Xóa")]
        Delete = 5,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("Sửa")]
        Update = 6,
        /// <summary>
        /// 提交
        /// </summary>
        [Description("Gửi")]
        Submit = 7,
        /// <summary>
        /// 异常
        /// </summary>
        [Description("Ngoại lệ")]
        Exception = 8,
    }

    /// <summary>
    /// 自负性质
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// 可报
        /// </summary>
        [Description("Được thanh toán")]
        KB = 0,
        /// <summary>
        /// 自费
        /// </summary>
        [Description("Tự trả")]
        ZF = 1,
        /// <summary>
        /// 分类自负
        /// </summary>
        [Description("Tự trả theo loại")]
        FLZF = 2,
        /// <summary>
        /// 绝对自理
        /// </summary>
        [Description("Tự túc hoàn toàn")]
        JueDuiZiLi = 3,
        /// <summary>
        /// 甲类
        /// </summary>
        [Description("Loại A")]
        Jia,
        /// <summary>
        /// 乙类
        /// </summary>
        [Description("Loại B")]
        Yi,
        /// <summary>
        /// 丙类
        /// </summary>
        [Description("Loại C")]
        Bing,
    }
    /// <summary>
    /// 自负性质 v2 
    /// 重庆再用
    /// </summary>
    public enum EnumZFXZv2 {
        /// <summary>
        /// 自费
        /// </summary>
        [Description("Tự trả")]
        ZF = 1,
        /// <summary>
        /// 甲类
        /// </summary>
        [Description("Loại A")]
        J = 4,
        /// <summary>
        /// 乙类
        /// </summary>
        [Description("Loại B")]
        Y = 5,
        /// <summary>
        /// 丙类
        /// </summary>
        [Description("Loại C")]
        B = 6,
    }

    /// <summary>
    /// 病区记账取整类型
    /// </summary>
    public enum EnumBQJZQZ
    {
        /// <summary>
        /// 每天取整
        /// </summary>
        [Description("Làm tròn theo ngày")]
        Day = 1,
        /// <summary>
        /// 每次取整
        /// </summary>
        [Description("Làm tròn theo lần")]
        Times
    }

    /// <summary>
    /// 康复收费项目 计费方式
    /// </summary>
    public enum EnumCollectionMethod
    {
        /// <summary>
        /// 服务次数
        /// </summary>
        [Description("Số lần dịch vụ")]
        Service = 1,
        /// <summary>
        /// 时长
        /// </summary>
        [Description("Thời lượng")]
        Time
    }

    /// <summary>
    /// 收费大类 大类类别
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
    /// 收费父级分类
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
    /// 登录标志
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
    /// 收费项目计价策略
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// 按时长
        /// </summary>
        [Description("Theo thời lượng")]
        Time = 1,
        /// <summary>
        /// 按数量
        /// </summary>
        [Description("Theo số lượng")]
        Amount,
        /// <summary>
        /// 按面积
        /// </summary>
        [Description("Theo diện tích")]
        Acreage,
    }

    /// <summary>
    /// 菜单目标类型
    /// </summary>
    public enum EnumModuleTargetType
    {
        /// <summary>
        /// 无页面
        /// </summary>
        expand,
        /// <summary>
        /// 框架页
        /// </summary>
        iframe,
        /// <summary>
        /// 弹出页
        /// </summary>
        open,
        /// <summary>
        /// 新窗口
        /// </summary>
        blank,
        /// <summary>
        /// 子页面（不显示在菜单中）
        /// </summary>
        subpage,
        /// <summary>
        /// 当前窗口
        /// </summary>
        toplocation,
    }

    /// <summary>
    /// 床位类型 男女混
    /// </summary>
    public enum EnumWardBedType
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("Nam")]
        male = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("Nữ")]
        female = 2,
        /// <summary>
        /// 混
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
    /// 出入库标志
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
    /// 消息用户连接状态
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
