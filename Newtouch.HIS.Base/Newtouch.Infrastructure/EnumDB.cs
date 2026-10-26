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
        [Description("其他")]
        Other = 0,
        /// <summary>
        /// Đăng nhập
        /// </summary>
        [Description("登录")]
        Login = 1,
        /// <summary>
        /// Thoát
        /// </summary>
        [Description("退出")]
        Exit = 2,
        /// <summary>
        /// Truy cập
        /// </summary>
        [Description("访问")]
        Visit = 3,
        /// <summary>
        /// Thêm mới
        /// </summary>
        [Description("新增")]
        Create = 4,
        /// <summary>
        /// Xóa
        /// </summary>
        [Description("删除")]
        Delete = 5,
        /// <summary>
        /// Sửa đổi
        /// </summary>
        [Description("修改")]
        Update = 6,
        /// <summary>
        /// Gửi
        /// </summary>
        [Description("提交")]
        Submit = 7,
        /// <summary>
        /// Ngoại lệ
        /// </summary>
        [Description("异常")]
        Exception = 8,
    }

    /// <summary>
    /// Tính chất tự trả
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Có thể báo
        /// </summary>
        [Description("可报")]
        KB = 0,
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// Tự trả theo phân loại
        /// </summary>
        [Description("分类自负")]
        FLZF = 2,
        /// <summary>
        /// Tự trả tuyệt đối
        /// </summary>
        [Description("绝对自理")]
        JueDuiZiLi = 3,
        /// <summary>
        /// Loại A
        /// </summary>
        [Description("甲类")]
        Jia,
        /// <summary>
        /// Loại B
        /// </summary>
        [Description("乙类")]
        Yi,
        /// <summary>
        /// Loại C
        /// </summary>
        [Description("丙类")]
        Bing,
    }
    /// <summary>
    /// Tính chất tự trả v2 
    /// Sử dụng lại ở Trùng Khánh
    /// </summary>
    public enum EnumZFXZv2 {
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// Loại A
        /// </summary>
        [Description("甲类")]
        J = 4,
        /// <summary>
        /// Loại B
        /// </summary>
        [Description("乙类")]
        Y = 5,
        /// <summary>
        /// Loại C
        /// </summary>
        [Description("丙类")]
        B = 6,
    }

    /// <summary>
    /// Loại làm tròn ghi sổ khu bệnh
    /// </summary>
    public enum EnumBQJZQZ
    {
        /// <summary>
        /// Làm tròn hàng ngày
        /// </summary>
        [Description("每天取整")]
        Day = 1,
        /// <summary>
        /// Làm tròn mỗi lần
        /// </summary>
        [Description("每次取整")]
        Times
    }

    /// <summary>
    /// Phương thức tính phí hạng mục thu phí phục hồi chức năng
    /// </summary>
    public enum EnumCollectionMethod
    {
        /// <summary>
        /// Số lần dịch vụ
        /// </summary>
        [Description("服务次数")]
        Service = 1,
        /// <summary>
        /// Thời lượng
        /// </summary>
        [Description("时长")]
        Time
    }

    /// <summary>
    /// Nhóm thu phí lớn, loại nhóm lớn
    /// </summary>
    public enum EnumSfdlDllb
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("药品")]
        yp = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("治疗项目")]
        zlxm,
        /// <summary>
        /// 
        /// </summary>
        [Description("非治疗项目")]
        flzxm
    }
    /// <summary>
    /// Phân loại cấp cha thu phí
    /// </summary>
    public enum EnumSffjdlCode
    {
        [Description("药品费")]
        ypf=1001,
        [Description("检查费")]
        jcf,
        [Description("检验费")]
        jyf,
        [Description("治疗费")]
        zlf,
        [Description("床位费")]
        cwf,
        [Description("材料费")]
        clf,
        [Description("麻醉费")]
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
        [Description("单点登录")]
        SSO = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("系统登录")]
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
        [Description("按时长")]
        Time = 1,
        /// <summary>
        /// Theo số lượng
        /// </summary>
        [Description("按数量")]
        Amount,
        /// <summary>
        /// Theo diện tích
        /// </summary>
        [Description("按面积")]
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
        /// Trang con (không hiển thị trong menu)
        /// </summary>
        subpage,
        /// <summary>
        /// Cửa sổ hiện tại
        /// </summary>
        toplocation,
    }

    /// <summary>
    /// Loại giường bệnh, nam nữ hỗn hợp
    /// </summary>
    public enum EnumWardBedType
    {
        /// <summary>
        /// Nam
        /// </summary>
        [Description("男")]
        male = 1,
        /// <summary>
        /// Nữ
        /// </summary>
        [Description("女")]
        female = 2,
        /// <summary>
        /// Hỗn hợp
        /// </summary>
        [Description("混")]
        Mix = 3
    }

    public enum EnumTcfw
    {
        [Description("个人")]
        Person = 1,
        [Description("科室")]
        Dept = 2,
        [Description("病区")]
        Ward = 3,
        [Description("全院")]
        Hosp = 4
    }

    /// <summary>
    /// Cờ nhập/xuất kho
    /// </summary>
    public enum EnumCrkbz
    {
        [Description("入库")]
        In = 0,
        [Description("出库")]
        Out = 1,
    }

    public enum EnumJybz
    {
        [Description("国基药")]
        Gjy = 1,
        [Description("贵安增补基药")]
        Gazbjy = 2,
	    [Description("非基药")]
	    Fjy = 3,
		[Description("非前述几种")]
        dbs = 4,
    }

    public enum EnumZjlx
    {
        [Description("身份证")]
        Sfz = 1,
    }

    public enum EnumYpsx {
        [Description("麻醉药品")]
        Mzyp = 9,
        [Description("毒性药品")]
        Dxyp = 4,
        [Description("精一药品")]
        Jyyp = 5,
        [Description("精二药品")]
        Jeyp = 6,
    }

    public enum EnumKss {

        [Description("非限制使用药物")]
        Fxzsyyw = 0,
        [Description("限制使用药物")]
        Xzsyyw = 1,
        [Description("特殊使用药物")]
        Tssyyw = 2,
    }

	public enum EnumSSLX
	{

		[Description("诊断型")]
		zdx = 0,
		[Description("治疗型")]
		zlx = 1,
		[Description("操作型")]
		czx = 2,
		[Description("手术")]
		ss = 3,
	}
    /// <summary>
    /// Trạng thái kết nối người dùng tin nhắn
    /// </summary>
	public enum EnumNoticeUserConnStu
	{

		[Description("已连接")]
		conn = 1,
		[Description("离线")]
        disconn = 2,
		[Description("重连")]
		reconn = 3,
	}
}
