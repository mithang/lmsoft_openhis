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
    /// Loại phiếu
    /// </summary>
    public enum EnumDanJuLX
    {
        /// <summary>
        /// Nhập kho thuốc
        /// </summary>
        [Description("药品入库")]   //Số phiếuYPRKD111111111111
        yaopinruku = 1,

        /// <summary>
        /// Xuất kho ngoài
        /// </summary>
        [Description("外部出库")]   //Số phiếuWBCKD111111111111
        waibucuku = 2,

        /// <summary>
        /// Phát thuốc trực tiếp
        /// </summary>
        [Description("直接出库")]   //Số phiếuNBFYD111111111111
        zhijiefayao = 3,

        /// <summary>
        /// Phát thuốc theo phiếu lĩnh
        /// </summary>
        [Description("申领出库")]   //Số phiếuSLCKD111111111111
        shenlingfayao = 4,

        /// <summary>
        /// Trả lại phát thuốc nội bộ
        /// </summary>
        [Description("内部发药退回")] //Số phiếuNBFYTHD111111111111
        neibufayaotuihui = 5,

        /// <summary>
        /// Nhà thuốc phát thuốc cho khoa
        /// </summary>
        [Description("科室发药")] //Số phiếuKSFYD111111111111
        keshifayao = 6,

        /// <summary>
        /// Xuất kho thuốc thiết yếu
        /// </summary>
        [Description("基药出库")] // Thống kê thông tin phiếu theo phương thức xuất kho
        jiyaochuku = 13,
        /// <summary>
        /// Xuất kho hàng loạt
        /// </summary>
        [Description("批量出库")] // Thống kê thông tin phiếu theo phương thức xuất kho
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
        [Description("启用")]
        Enabled = 1,
        /// <summary>
        /// Ngừng dùng
        /// </summary>
        [Description("停用")]
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
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// Kiểm soát
        /// </summary>
        [Description("控制")]
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
        [Description("启用")]
        Enabled = 1,
        /// <summary>
        /// Ngừng dùng
        /// </summary>
        [Description("停用")]
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
        [Description("全部")]
        None = -1,
        /// <summary>
        /// Hiển thị tồn kho bằng 0
        /// </summary>
        [Description("显示零库存")]
        Xslkc = 0,
        /// <summary>
        /// Không hiển thị khi số lượng lý thuyết bằng 0
        /// </summary>
        [Description("不显示理论数量为零")]
        Bxsllslwl = 1,
        /// <summary>
        /// Không hiển thị khi số lượng thực tế bằng 0
        /// </summary>
        [Description("不显示实际数量为零")]
        Bxssjslwl = 2,
        /// <summary>
        /// Không hiển thị khi số lượng thực tế bằng 0
        /// </summary>
        [Description("不显示两者都为零")]
        Bxslzdwl = 4
    }

    /// <summary>
    /// Trạng thái cấp phát phiếu lĩnh
    /// </summary>
    public enum EnumSLDDeliveryStatus
    {
        /// <summary>
        /// Chưa phát
        /// </summary>
        [Description("未发")]
        None = 0,

        /// <summary>
        /// Đã phát một phần
        /// </summary>
        [Description("已发部分")]
        Part,

        /// <summary>
        /// Đã phát hết
        /// </summary>
        [Description("已全发")]
        All,

        /// <summary>
        /// Đã chấm dứt (một số (thậm chí tất cả) chi tiết về sau sẽ không phát nữa, nhưng việc này sẽ được xem là một phiếu lĩnh đã kết thúc）
        /// </summary>
        [Description("已终止")]
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
        [Description("未审核")]
        WaitingApprove = 0,
        /// <summary>
        /// Đã đạt
        /// </summary>
        [Description("已通过")]
        Approved = 1,
        /// <summary>
        /// Chưa đạt (trạng thái cuối cùng）
        /// </summary>
        [Description("未通过")]
        Rejected = 2,
        /// <summary>
        /// Đã hủy bỏ (trạng thái cuối cùng) (chỉ khi duyệt đạt -> Mới có thể đến trạng thái này）
        /// </summary>
        [Description("已作废")]
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
        [Description("报损")]
        Loss = 0,

        /// <summary>
        /// Báo thừa
        /// </summary>
        [Description("报溢")]
        Profit = 1
    }

    /// <summary>
    /// Loại thao tác điều chỉnh giá thuốc (hủy, phê duyệt, từ chối, thực hiện）
    /// </summary>
    public enum EnumPriceAdjustOperationType
    {
        /// <summary>
        /// Chờ duyệt
        /// </summary>
        [Description("待审核")]
        None = 0,
        /// <summary>
        /// Phê duyệt
        /// </summary>
        [Description("批准")]
        Approval = 1,
        /// <summary>
        /// Từ chối
        /// </summary>
        [Description("拒绝")]
        Refuse = 2,
        /// <summary>
        /// Hủy
        /// </summary>
        [Description("撤销")]
        Withdraw = 3,
        /// <summary>
        /// Thực hiện
        /// </summary>
        [Description("执行")]
        Execute = 4
    }

    /// <summary>
    /// Trạng thái thực hiện điều chỉnh giá thuốc（0：Chưa thực hiện 1：Đã thực hiện）
    /// </summary>
    public enum EnumPriceAdjustZXStatus
    {
        /// <summary>
        /// Chưa thực hiện
        /// </summary>
        [Description("未执行")]
        Not = 0,
        /// <summary>
        /// Đã thực hiện
        /// </summary>
        [Description("已执行")]
        Executed = 1
    }

    /// <summary>
    /// Loại phí lớn, loại loại lớn
    /// </summary>
    public enum EnumSfdlDllb
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("药品")]
        Yp = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("治疗项目")]
        Zlxm,
        /// <summary>
        /// 
        /// </summary>
        [Description("非治疗项目")]
        Flzxm
    }

    /// <summary>
    /// Ghi chú phát thuốc 0：Chưa phát；1：Đã xếp；2：Đã phát；3：Đã trả
    /// </summary>
    public enum EnumFybz
    {
        /// <summary>
        /// Chưa xếp
        /// </summary>
        [Description("未排")]
        Wp = 0,

        /// <summary>
        /// Đã xếp
        /// </summary>
        [Description("已排")]
        Yp = 1,

        /// <summary>
        /// Đã phát
        /// </summary>
        [Description("已发")]
        Yf = 2,

        /// <summary>
        /// Đã trả
        /// </summary>
        [Description("已退")]
        Yt = 3
    }

    /// <summary>
    /// Tính chất y lệnh: tạm thời, dài hạn
    /// </summary>
    public enum Yzxz
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("全部")]
        None = 0,

        /// <summary>
        /// Tạm thời
        /// </summary>
        [Description("临时")]
        Ls = 1,

        /// <summary>
        /// Dài hạn
        /// </summary>
        [Description("长期")]
        Cq = 2,
    }
    public enum Yzlx
    {
        [Description("药品")]
        Yp = 2,
        /// <summary>
        /// Thuốc thảo dược Trung Quốc
        /// </summary>
        [Description("中草药")]
        zcy = 10,
    }

    /// <summary>
    /// Bản ghi thao tác thuốc zy_ypyzczjl.operateType Và mz_cfypczjl.operateType
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
        [Description("发药")]
        Fy = 1,

        /// <summary>
        /// Trả thuốc
        /// </summary>
        [Description("退药")]
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
        [Description("正常出库")]
        Zcck = 11,

        /// <summary>
        /// Xuất kho trả hàng
        /// </summary>
        [Description("退货出库")]
        Thck = 5,

        /// <summary>
        /// Xuất kho tặng
        /// </summary>
        [Description("赠送出库")]
        Zsck = 12,

        /// <summary>
        /// Xuất kho cứu trợ thiên tai
        /// </summary>
        [Description("救灾出库")]
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
        [Description("男")]
        male = 1,
        /// <summary>
        /// Nữ
        /// </summary>
        [Description("女")]
        female = 2,
        /// <summary>
        /// Không rõ
        /// </summary>
        [Description("不详")]
        other = 3
    }

    public enum EnumSF
    {
        /// <summary>
        /// Không
        /// </summary>
        [Description("否")]
        f = 0,
        /// <summary>
        /// Là
        /// </summary>
        [Description("是")]
        s = 1,
    }
    /// <summary>
    /// Chế độ mua hàng
    /// </summary>
    public enum EnumCGLX
    {
        [Description("医保范围招标")]
        ybfwzb = 1,
        [Description("医保范围未招标")]
        ybfwwzb = 2,
        [Description("医保范围招标未中标")]
        ybfwzbwzb = 3,
        [Description("医保范围挂网")]
        ybfwgw = 4,
        [Description(" 自费范围挂网")]
        zffwgw = 5,
        [Description("市级量价挂钩集中采购")]
        sjljggjzcg = 6,
        [Description("不允许采购")]
        byxcg = 7,
        [Description("其他")]
        qt = 9,
    }
    /// <summary>
    /// Loại hàng hóa
    /// </summary>
    public enum EnumSPLX
    {
        [Description("药品类")]
        ypl = 1,
        [Description("医用耗材器械类")]
        yyhcqxl = 2,
        [Description("其他")]
        qt = 9,
    }
    /// <summary>
    /// Đơn vị đo lường mua hàng
    /// </summary>
    public enum EnumCGJLDW
    {
        [Description("计价单位")]
        jjdw = 1,
        [Description("最小使用单位")]
        zxsydw = 2,
    }

    /// <summary>
    /// Loại đóng thùng
    /// </summary>
    public enum EnumZXLX
    {
        [Description("多品规货品拼箱配送")]
        yxdp = 1,
        [Description("整箱为同一品规的货品")]
        yxyp = 2,
        [Description("同一次配送的同一品规货品分布在多箱内")]
        ypdx = 3,
    }
    
    /// <summary>
    /// Loại thuốc
    /// </summary>
    public enum EnumYPLX
    {
        [Description("生物制剂")]
        swzj = 1,
        [Description("中成药")]
        zcy = 2,
        [Description("中药饮片")]
        zyyp = 3,
        [Description("自制制剂")]
        zzzj = 4,
        [Description("免疫用疫苗")]
        myyym = 5,
        [Description("实验室检验化验用药品或试剂")]
        sysjyhyyyphsj = 6,
        [Description("医学检查用药品或试剂")]
        yxjcyyphsj = 7,
        [Description("化学药品")]
        hxyp = 8,
        [Description("其他")]
        qt = 9,
    }
    /// <summary>
    /// Tính chất đơn vị đóng gói
    /// </summary>
    public enum EnumBZDWXZ
    {
        [Description("商品最小零售单位")]
        spzxlsdw = 1,
        [Description("大包装简包装")]
        dbzjbz = 2,
    }
}
