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
        /// Thoát
        /// </summary>
        [Description("Thoát")]
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
    /// Loại chứng từ
    /// </summary>
    public enum EnumDanJuLX
    {
        /// <summary>
        /// Nhập kho thuốc
        /// </summary>
        [Description("Nhập kho thuốc")]   //Số chứng từYPRKD111111111111
        yaopinruku = 1,

        /// <summary>
        /// Xuất kho ra ngoài
        /// </summary>
        [Description("Xuất kho ra ngoài")]   //Số chứng từWBCKD111111111111
        waibucuku = 2,

        /// <summary>
        /// Phát thuốc trực tiếp
        /// </summary>
        [Description("Xuất kho trực tiếp")]   //Số chứng từNBFYD111111111111
        zhijiefayao = 3,

        /// <summary>
        /// Phát thuốc theo đơn lĩnh
        /// </summary>
        [Description("Xuất kho theo đơn lĩnh")]   //Số chứng từSLCKD111111111111
        shenlingfayao = 4,

        /// <summary>
        /// Trả lại phát thuốc nội bộ
        /// </summary>
        [Description("Trả lại phát thuốc nội bộ")] //Số chứng từNBFYTHD111111111111
        neibufayaotuihui = 5,

        /// <summary>
        /// Nhà thuốc phát thuốc cho khoa
        /// </summary>
        [Description("Phát thuốc cho khoa")] //Số chứng từKSFYD111111111111
        keshifayao = 6,

        /// <summary>
        /// Xuất kho thuốc thiết yếu
        /// </summary>
        [Description("Xuất kho thuốc thiết yếu")] // Thống kê thông tin chứng từ theo phương thức xuất kho
        jiyaochuku = 13,
        /// <summary>
        /// Xuất kho hàng loạt
        /// </summary>
        [Description("Xuất kho hàng loạt")] // Thống kê thông tin chứng từ theo phương thức xuất kho
        piliangchuku = 14,
    }

    /// <summary>
    /// Trạng thái tồn kho
    /// </summary>
    public enum EnumKCZT
    {
        /// <summary>
        /// Bật
        /// </summary>
        [Description("Bật")]
        Enabled = 1,
        /// <summary>
        /// Ngừng dùng
        /// </summary>
        [Description("Ngừng dùng")]
        Disabled = 0
    }

    /// <summary>
    /// Trạng thái bộ phận này
    /// </summary>
    public enum BenBuMenZT
    {
        /// <summary>
        /// Bình thường
        /// </summary>
        [Description("Bình thường")]
        Normal = 1,
        /// <summary>
        /// Kiểm soát
        /// </summary>
        [Description("Điều khiển")]
        Control = 0
    }

    /// <summary>
    /// Trạng thái thuốc
    /// </summary>
    public enum EnumYPZT
    {
        /// <summary>
        /// Bật
        /// </summary>
        [Description("Bật")]
        Enabled = 1,
        /// <summary>
        /// Ngừng dùng
        /// </summary>
        [Description("Ngừng dùng")]
        Disabled = 0
    }

    /// <summary>
    /// Hiển thị tồn kho
    /// </summary>
    public enum EnumKCXS
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("Tất cả")]
        None = -1,
        /// <summary>
        /// Hiển thị tồn kho bằng 0
        /// </summary>
        [Description("Hiển thị tồn kho bằng 0")]
        Xslkc = 0,
        /// <summary>
        /// Không hiển thị số lượng lý thuyết bằng 0
        /// </summary>
        [Description("Không hiển thị số lượng lý thuyết bằng 0")]
        Bxsllslwl = 1,
        /// <summary>
        /// Không hiển thị số lượng thực tế bằng 0
        /// </summary>
        [Description("Không hiển thị số lượng thực tế bằng 0")]
        Bxssjslwl = 2,
        /// <summary>
        /// Không hiển thị số lượng thực tế bằng 0
        /// </summary>
        [Description("Không hiển thị cả hai đều bằng 0")]
        Bxslzdwl = 4
    }

    /// <summary>
    /// Trạng thái phát đơn lĩnh
    /// </summary>
    public enum EnumSLDDeliveryStatus
    {
        /// <summary>
        /// Chưa phát
        /// </summary>
        [Description("Chưa phát")]
        None = 0,

        /// <summary>
        /// Đã phát một phần
        /// </summary>
        [Description("Đã phát một phần")]
        Part,

        /// <summary>
        /// Đã phát hết
        /// </summary>
        [Description("Đã phát hết")]
        All,

        /// <summary>
        /// Đã chấm dứt(Một số(Thậm chí tất cả)Chi tiết sau này sẽ không phát nữa,Nhưng điều này sẽ được coi là đơn lĩnh đã kết thúc)
        /// </summary>
        [Description("Đã kết thúc")]
        Abandon
    }

    /// <summary>
    /// Kết quả duyệt
    /// </summary>
    public enum EnumDjShzt
    {
        /// <summary>
        /// Chưa duyệt
        /// </summary>
        [Description("Chưa duyệt")]
        WaitingApprove = 0,
        /// <summary>
        /// Đã đạt
        /// </summary>
        [Description("Đã đạt")]
        Approved = 1,
        /// <summary>
        /// Không đạt(Trạng thái cuối cùng)
        /// </summary>
        [Description("Không đạt")]
        Rejected = 2,
        /// <summary>
        /// Đã hủy bỏ(Trạng thái cuối cùng)(Chỉ khi duyệt đạt -> mới có thể tới trạng thái này)
        /// </summary>
        [Description("Đã hủy")]
        Cancelled = 3,
    }

    /// <summary>
    /// Cờ lãi lỗ
    /// </summary>
    public enum EnumProfitLossMark
    {
        /// <summary>
        /// Báo hỏng
        /// </summary>
        [Description("Báo hỏng")]
        Loss = 0,

        /// <summary>
        /// Báo thừa
        /// </summary>
        [Description("Báo thừa")]
        Profit = 1
    }

    /// <summary>
    /// Loại thao tác điều chỉnh giá thuốc(Thu hồi Phê duyệt Từ chối Thực hiện)
    /// </summary>
    public enum EnumPriceAdjustOperationType
    {
        /// <summary>
        /// Chờ duyệt
        /// </summary>
        [Description("Chờ duyệt")]
        None = 0,
        /// <summary>
        /// Phê duyệt
        /// </summary>
        [Description("Phê duyệt")]
        Approval = 1,
        /// <summary>
        /// Từ chối
        /// </summary>
        [Description("Từ chối")]
        Refuse = 2,
        /// <summary>
        /// Thu hồi
        /// </summary>
        [Description("Thu hồi")]
        Withdraw = 3,
        /// <summary>
        /// Thực hiện
        /// </summary>
        [Description("Thực hiện")]
        Execute = 4
    }

    /// <summary>
    /// Trạng thái thực hiện điều chỉnh giá thuốc(0:Chưa thực hiện 1:Đã thực hiện)
    /// </summary>
    public enum EnumPriceAdjustZXStatus
    {
        /// <summary>
        /// Chưa thực hiện
        /// </summary>
        [Description("Chưa thực hiện")]
        Not = 0,
        /// <summary>
        /// Đã thực hiện
        /// </summary>
        [Description("Đã thực hiện")]
        Executed = 1
    }

    /// <summary>
    /// Loại thu phí lớn Loại lớn
    /// </summary>
    public enum EnumSfdlDllb
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Thuốc")]
        Yp = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Hạng mục điều trị")]
        Zlxm,
        /// <summary>
        /// 
        /// </summary>
        [Description("Hạng mục không điều trị")]
        Flzxm
    }

    /// <summary>
    /// Ghi chú phát thuốc 0:Chưa phát;1:Đã xếp;2:Đã phát;3:Đã trả
    /// </summary>
    public enum EnumFybz
    {
        /// <summary>
        /// Chưa xếp
        /// </summary>
        [Description("Chưa xếp")]
        Wp = 0,

        /// <summary>
        /// Đã xếp
        /// </summary>
        [Description("Đã xếp")]
        Yp = 1,

        /// <summary>
        /// Đã phát
        /// </summary>
        [Description("Đã phát")]
        Yf = 2,

        /// <summary>
        /// Đã trả
        /// </summary>
        [Description("Đã trả")]
        Yt = 3
    }

    /// <summary>
    /// Tính chất y lệnh Tạm thời,Dài hạn
    /// </summary>
    public enum Yzxz
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("Tất cả")]
        None = 0,

        /// <summary>
        /// Tạm thời
        /// </summary>
        [Description("Tạm thời")]
        Ls = 1,

        /// <summary>
        /// Dài hạn
        /// </summary>
        [Description("Dài hạn")]
        Cq = 2,
    }
    public enum Yzlx
    {
        [Description("Thuốc")]
        Yp = 2,
        /// <summary>
        /// Thuốc thảo dược Trung Quốc
        /// </summary>
        [Description("Thuốc thảo dược Trung Quốc")]
        zcy = 10,
    }

    /// <summary>
    /// Bản ghi thao tác thuốc zy_ypyzczjl.operateType và mz_cfypczjl.operateType
    /// </summary>
    public enum EnumOperateType
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Phát thuốc
        /// </summary>
        [Description("Phát thuốc")]
        Fy = 1,

        /// <summary>
        /// Trả thuốc
        /// </summary>
        [Description("Trả thuốc")]
        Ty = 2
    }

    /// <summary>
    /// Phương thức phát thuốc
    /// </summary>
    public enum EnumFyfs
    {
        /// <summary>
        /// Xuất kho bình thường
        /// </summary>
        [Description("Xuất kho bình thường")]
        Zcck = 11,

        /// <summary>
        /// Xuất kho trả hàng
        /// </summary>
        [Description("Xuất kho trả hàng")]
        Thck = 5,

        /// <summary>
        /// Xuất kho tặng
        /// </summary>
        [Description("Xuất kho tặng")]
        Zsck = 12,

        /// <summary>
        /// Xuất kho cứu trợ
        /// </summary>
        [Description("Xuất kho cứu trợ")]
        Jzck = 13,
    }

    /// <summary>
    /// Giới tính
    /// </summary>
    public enum EnumSex
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
        /// Không rõ
        /// </summary>
        [Description("Không rõ")]
        other = 3
    }

    public enum EnumSF
    {
        /// <summary>
        /// Không
        /// </summary>
        [Description("Không")]
        f = 0,
        /// <summary>
        /// Có
        /// </summary>
        [Description("Có")]
        s = 1,
    }
    /// <summary>
    /// Phương thức mua hàng
    /// </summary>
    public enum EnumCGLX
    {
        [Description("Đấu thầu trong phạm vi BHYT")]
        ybfwzb = 1,
        [Description("Chưa đấu thầu trong phạm vi BHYT")]
        ybfwwzb = 2,
        [Description("Đấu thầu trong phạm vi BHYT chưa trúng thầu")]
        ybfwzbwzb = 3,
        [Description("Treo mạng trong phạm vi BHYT")]
        ybfwgw = 4,
        [Description(" Treo mạng trong phạm vi tự trả")]
        zffwgw = 5,
        [Description("Mua tập trung gắn lượng giá cấp thành phố")]
        sjljggjzcg = 6,
        [Description("Không cho phép mua hàng")]
        byxcg = 7,
        [Description("Khác")]
        qt = 9,
    }
    /// <summary>
    /// Loại hàng hóa
    /// </summary>
    public enum EnumSPLX
    {
        [Description("Loại thuốc")]
        ypl = 1,
        [Description("Loại vật tư y tế")]
        yyhcqxl = 2,
        [Description("Khác")]
        qt = 9,
    }
    /// <summary>
    /// Đơn vị đo lường mua hàng
    /// </summary>
    public enum EnumCGJLDW
    {
        [Description("Đơn vị tính giá")]
        jjdw = 1,
        [Description("Đơn vị sử dụng nhỏ nhất")]
        zxsydw = 2,
    }

    /// <summary>
    /// Loại đóng thùng
    /// </summary>
    public enum EnumZXLX
    {
        [Description("Giao hàng ghép thùng nhiều quy cách")]
        yxdp = 1,
        [Description("Cả thùng là hàng cùng quy cách")]
        yxyp = 2,
        [Description("Hàng cùng quy cách trong một lần giao phân bố trong nhiều thùng")]
        ypdx = 3,
    }
    
    /// <summary>
    /// Loại thuốc
    /// </summary>
    public enum EnumYPLX
    {
        [Description("Chế phẩm sinh học")]
        swzj = 1,
        [Description("Thuốc thành phẩm Trung dược")]
        zcy = 2,
        [Description("Thuốc phiến Trung dược")]
        zyyp = 3,
        [Description("Chế phẩm tự chế")]
        zzzj = 4,
        [Description("Vắc-xin miễn dịch")]
        myyym = 5,
        [Description("Thuốc hoặc thuốc thử xét nghiệm phòng thí nghiệm")]
        sysjyhyyyphsj = 6,
        [Description("Thuốc hoặc thuốc thử kiểm tra y học")]
        yxjcyyphsj = 7,
        [Description("Thuốc hóa học")]
        hxyp = 8,
        [Description("Khác")]
        qt = 9,
    }
    /// <summary>
    /// Tính chất đơn vị đóng gói
    /// </summary>
    public enum EnumBZDWXZ
    {
        [Description("Đơn vị bán lẻ tối thiểu hàng hóa")]
        spzxlsdw = 1,
        [Description("Đóng gói lớn đóng gói đơn giản")]
        dbzjbz = 2,
    }
}
