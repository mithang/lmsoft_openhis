using System.ComponentModel;

namespace Newtouch.Herp.Infrastructure.Enum
{
    /// <summary>
    /// Cấp kho
    /// </summary>
    public enum EnumWarehouseLevel
    {
        /// <summary>
        /// Cấp 1
        /// </summary>
        [Description("一级")]
        OneLevel = 1,

        /// <summary>
        /// Cấp 2
        /// </summary>
        [Description("二级")]
        TwoLevel = 2,

        /// <summary>
        /// Cấp 3
        /// </summary>
        [Description("三级")]
        ThreeLevel = 3,

        /// <summary>
        /// Cấp 4
        /// </summary>
        [Description("四级")]
        FourLevel = 4,

        /// <summary>
        /// Cấp 5
        /// </summary>
        [Description("五级")]
        FiveLevel = 5,

        /// <summary>
        /// Cấp 6
        /// </summary>
        [Description("六级")]
        SixLevel = 6,

    }

    /// <summary>
    /// Hiệu lực/Không hiệu lực
    /// </summary>
    public enum Enumzt
    {
        /// <summary>
        /// Không hiệu lực
        /// </summary>
        [Description("无效")]
        Disable = 0,

        /// <summary>
        /// Hiệu lực
        /// </summary>
        [Description("有效")]
        Enable = 1

    }

    /// <summary>
    /// Trạng thái duyệt
    /// </summary>
    public enum EnumAuditState
    {

        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("待处理")]
        Waiting = 0,

        /// <summary>
        /// Duyệt đạt
        /// </summary>
        [Description("审核通过")]
        Adopt = 1,

        /// <summary>
        /// Duyệt không đạt
        /// </summary>
        [Description("审核不通过")]
        Refuse = 2,

        /// <summary>
        /// Đã hủy bỏ
        /// </summary>
        [Description("已作废")]
        Cancelled = 3,

        /// <summary>
        /// Lưu tạm
        /// </summary>
        [Description("暂存")]
        Temporary = 4,
    }

    /// <summary>
    /// Loại chứng từ
    /// </summary>
    public enum EnumOutOrInStorageBillType
    {
        /// <summary>
        /// Nhập kho ngoài
        /// </summary>
        [Description("外部入库")]
        Wbrk = 1,

        /// <summary>
        /// Xuất kho ngoài
        /// </summary>
        [Description("外部出库")]
        Wbck = 2,

        /// <summary>
        /// Xuất kho trực tiếp
        /// </summary>
        [Description("直接出库")]
        Zjck = 3,

        /// <summary>
        /// Xuất kho lĩnh
        /// </summary>
        [Description("申领出库")]
        Slck = 4,

        /// <summary>
        /// Trả hàng nội bộ
        /// </summary>
        [Description("内部退货")]
        Nbth = 5,

        /// <summary>
        /// Báo hỏng/báo thừa
        /// </summary>
        [Description("报损报溢")]
        Bsby = 6,

        /// <summary>
        /// Giao hàng đến khoa
        /// </summary>
        [Description("出库至科室")] //Số chứng từ ckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// Lĩnh vật tư khoa
        /// </summary>
        [Description("科室申领")]
        kssl = 8,

        /// <summary>
        /// Xuất kho lĩnh vật tư khoa
        /// </summary>
        [Description("科室申领出库")]
        ksSlck = 9,

        /// <summary>
        /// Kế hoạch mua hàng
        /// </summary>
        [Description("采购计划")]
        purchasingPlan = 10,

        /// <summary>
        /// Đơn đặt mua hàng
        /// </summary>
        [Description("采购订单")]
        purchasingOrder = 11,
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
        /// Không hiển thị Số lượng lý thuyết bằng 0
        /// </summary>
        [Description("不显示理论数量为零")]
        Bxsllslwl = 1,
        /// <summary>
        /// Không hiển thị số lượng thực tế bằng 0
        /// </summary>
        [Description("不显示实际数量为零")]
        Bxssjslwl = 2,
        /// <summary>
        /// Không hiển thị số lượng thực tế bằng 0
        /// </summary>
        [Description("不显示两者都为零")]
        Bxslzdwl = 4
    }

    /// <summary>
    /// Khóa tồn kho
    /// </summary>
    public enum EnumKcLock
    {
        /// <summary>
        /// Không khóa
        /// </summary>
        [Description("无锁")]
        none = 0,

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        [Description("开始盘点")]
        InventoryStart = 1,

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        [Description("结束盘点")]
        InventoryEnd = 2,

        /// <summary>
        /// Lãi/lỗ
        /// </summary>
        [Description("损益")]
        ProfitAndLoss = 3,

        /// <summary>
        /// Duyệt chứng từ nhập/xuất kho
        /// </summary>
        [Description("出入库单据审核")]
        OutgoingOrWarehousing = 4
    }

    /// <summary>
    /// Cờ lãi/lỗ
    /// </summary>
    public enum EnumSybz
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
        Profit = 1,
    }

    #region Điều chỉnh giá

    /// <summary>
    /// Cờ duyệt điều chỉnh giá 0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã hủy
    /// </summary>
    public enum EnumTjShzt
    {
        /// <summary>
        /// Chưa duyệt
        /// </summary>
        [Description("未审核")]
        Waiting = 0,

        /// <summary>
        /// Đã duyệt
        /// </summary>
        [Description("已审核")]
        Audited = 1,

        /// <summary>
        /// Đã từ chối
        /// </summary>
        [Description("已拒绝")]
        Refuse = 2,

        /// <summary>
        /// Đã hủy
        /// </summary>
        [Description("已撤销")]
        Revoke = 3
    }

    /// <summary>
    /// Cờ thực hiện điều chỉnh giá
    /// </summary>
    public enum EnumTjZxbz
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
        Already = 1
    }
    #endregion

    /// <summary>
    /// Nhà sản xuất/Nhà phân phối/Khác  Loại công ty
    /// </summary>
    public enum EnumSupplierType
    {
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        [Description("生产商")]
        Producer = 1,

        /// <summary>
        /// Nhà phân phối
        /// </summary>
        [Description("经销商")]
        Distributor = 2,
    }

    /// <summary>
    /// Quá trình xử lý   1:Chờ xử lý;2:Duyệt đạt;3:Duyệt không đạt;4:Giao hàng;5:Hoàn thành một phần;6:Hoàn thành;7:Từ chối nhận
    /// </summary>
    public enum EnumApplyProcess
    {
        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("等待处理")]
        Waiting = 1,

        /// <summary>
        /// Duyệt đạt
        /// </summary>
        [Description("审核通过")]
        AuditApproved = 2,

        /// <summary>
        /// Duyệt không đạt
        /// </summary>
        [Description("审核不通过")]
        AuditFailed = 3,

        /// <summary>
        /// Đang giao hàng
        /// </summary>
        [Description("配送中")]
        Distributing = 4,

        /// <summary>
        /// Hoàn thành một phần
        /// </summary>
        [Description("部分完成")]
        PartialCompletion = 5,

        /// <summary>
        /// Tất cả hoàn thành
        /// </summary>
        [Description("全部完成")]
        Completion = 6,

        /// <summary>
        /// Từ chối nhận giao hàng
        /// </summary>
        [Description("配送拒收")]
        Rejection = 7,
    }

    /// <summary>
    /// Loại chứng từ 1-Lĩnh vật tư khoa;2-Lĩnh vật tư kho
    /// </summary>
    public enum EnumApplyType
    {
        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("科室申领")]
        DepartmentApply = 1,

        /// <summary>
        /// Duyệt đạt
        /// </summary>
        [Description("库房申领")]
        WarehouseApply = 2,
    }

    /// <summary>
    /// Quy trình xử lý đơn đặt mua hàng -1:Từ chối xử lý; 0:Chờ xử lý; 1:Chuẩn bị hàng; 2:Giao hàng; 3:Ký nhận; 4:Hoàn thành; 5:Từ chối ký 
    /// </summary>
    public enum EnumOrderProcess
    {
        /// <summary>
        /// Từ chối xử lý
        /// </summary>
        [Description("拒处理")]
        Refusal =-1,

        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("待处理")]
        Waiting = 0,

        /// <summary>
        /// Chuẩn bị hàng
        /// </summary>
        [Description("备货")]
        PreparingGoods = 1,

        /// <summary>
        /// Giao hàng
        /// </summary>
        [Description("配送")]
        Delivering = 2,

        /// <summary>
        /// Ký nhận
        /// </summary>
        [Description("签收")]
        SignFor = 3,

        /// <summary>
        /// Hoàn thành
        /// </summary>
        [Description("完成")]
        Complete = 4,

        /// <summary>
        /// Từ chối ký
        /// </summary>
        [Description("拒签")]
        RefusalSign = 5
    }

    /// <summary>
    /// Loại đơn hàng 0:Phiếu lưu tạm;1:Phiếu chính thức
    /// </summary>
    public enum EnumOrderTypeHrp
    {

        /// <summary>
        /// Phiếu lưu tạm
        /// </summary>
        [Description("暂存单")]
        TempOrder = 0,

        /// <summary>
        /// Phiếu chính thức
        /// </summary>
        [Description("正式单")]
        OfficialOrder = 1,

        /// <summary>
        /// Phiếu hủy bỏ
        /// </summary>
        [Description("作废单")]
        BadOrder = 2,
    }

	/// <summary>
	/// Tính chất tự trả v2 
	/// Trùng Khánh dùng lại
	/// </summary>
	public enum EnumZFXZv2
	{
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


    #region Mua hàng

    //4.1.1 Loại thao tác
    public enum EnumCZLX
    {
        [Description("新增")] XZ=1,
        [Description("作废")] ZF=3,
    }

    //4.1.2 Loại đơn mua hàng
    public enum EnumCGDLX
    {
        [Description("医院自行采购单")] YYZXCGD= 1,
        [Description("托管药库采购单")] TGYKCGD= 2,
    }

    //4.1.3 Loại mua hàng
    public enum EnumCGLX
    {
        [Description("招标采购")]
        ZBCG = 1,
        [Description("带量采购")]
        DLCG = 2,
        [Description("挂网采购")]
        GWCG = 3,
        [Description("其他")]
        QT = 9,
    }

    //4.1.4 Yêu cầu giao hàng
    public enum EnumPSYQ
    {
        [Description("按单配送")] ADPS= 1,
        [Description("按需配送")] AXPS= 2,
    }

    //4.1.5 Loại trả hàng

    public enum EnumTHLX
    {
        [Description("正常退货")] ZCTH= 1,
        [Description("补差价退货")] BCJTH= 2,

    }

    //4.1.6 Loại nghiệm thu giao hàng
    public enum EnumPSYSLX
    {
        [Description("预验收")] YYS= 1,
        [Description("实验收")] SYS= 2,

    }

    //4.1.7 Bên lập chứng từ
    public enum EnumDJTXF
    {
        [Description("采购方填写")]
        CGFTX = 1,
        [Description("销售方代填")]
        XSFDT = 2,
    }

    //4.1.8 Trạng thái phiếu giao hàng
    public enum EnumPSDZT
    {
        [Description("待确认")] DQR= 0,
        [Description("作废")] ZF= 1,
        [Description("已确认，未验收")] YQR= 10,

    }
    //4.1.9 Trạng thái chi tiết mua hàng
    public enum EnumCGMXZT
    {
        [Description("待提交")] DTJ= 0,
        [Description("已作废")] YZF= 1,
        [Description("待审核")] DSH= 10,
        [Description("审核不通过")] SHBTG= 11,
        [Description("待企业接受确认")] DQYJSQR= 20,
        [Description("企业已接收确认配送")] QYYJSQRPS= 21,
        [Description("企业已接收确认不予配送")] QYYJSQRBYPS= 22,
        [Description("企业已配送完成")] QYYPSWC= 23,
    }

    //4.1.10 Tồn kho doanh nghiệp
    public enum EnumQYKC
    {
        [Description("充足")] CZ= 1,
        [Description("有货， 限量")] YHXL= 2,
        [Description("有货， 少量")] YHSL= 3,
        [Description("暂无货")] ZWH= 4,
        [Description("不予披露")] BYPL= 5,
    }

    //4.1.11 Trạng thái hóa đơn
    public enum EnumFPZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("未验收")] WYS= 10,
        [Description("已验收")] YYS= 20,
        [Description("已拒收")] YJS= 21,
        [Description("已支付")] FPYZF= 30,
    }

    //4.1.12 Trạng thái chi tiết giao hàng
    public enum EnumPSMXZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("已提交未验收")] YTJWYS= 10,
        [Description("验收中")] YSZ= 20,
        [Description("验收入库完成")] YSRKWC= 30,
    }

    //4.1.13 Trạng thái chi tiết trả hàng
    public enum EnumTHMXZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("已确认待企业接受")] YQRDQYJS= 10,
        [Description("企业已确认退货处理")] QYYQRTHCL= 20,
        [Description("企业已确认不予处理")] QYYQRBYCL= 21,
    }

    //4.1.14 Loại giấy tờ
    public enum EnumZJLX
    {
        [Description("组织机构代码证")] ZZJGDMZ= 1,
        [Description("营业执照")] YYZZ= 2,
        [Description("经营许可证")] JYXKZ= 3,
        [Description("生产许可证")] SCXKZ= 4,
    }

    //4.1.15 Loại mã vạch chi tiết giao hàng
    public enum EnumPSMXTMLX
    {
        [Description("GS1 条码")] GSI= 1,
        [Description("HIBC 条码")] HIBC= 2,
        [Description("其他")] QT= 99,
    }

    //4.1.16 Kết quả nghiệm thu hóa đơn
    public enum EnumFPYSJG
    {
        [Description("验收通过")] YSTG= 1,
        [Description("验收不通过")] YSBTG= 0,
    }

    //4.1.17 Trạng thái đơn mua hàng
    public enum EnumCGDZT
    {
        [Description("待提交")] DTJ= 0,
        [Description("已作废")] YZF= 1,
        [Description("已提交")] YTJ= 20,
    }

    //4.1.18 Trạng thái phiếu trả hàng
    public enum EnumTHDZT
    {
        [Description("待确认")] DQR= 0,
        [Description("已作废")] YZF= 1,
        [Description("已确认")] YQR= 10,

    }

    //4.1.19 Hình thức mua hàng

    public enum EnumCGFS
    {
        [Description("非系统采购")]
        FXTCG = 1,
        [Description("按系统采购")]
        AXTCG = 2,
    }

    //4.1.20 Có bao gồm dịch vụ đi kèm không
    public enum EnumSFHBSFW
    {
        [Description("否")] F= 0,
        [Description("是")] S= 1,
    }

    
    #endregion
}
