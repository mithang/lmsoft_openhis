using System.ComponentModel;

namespace Newtouch.Herp.Infrastructure.Enum
{
    /// <summary>
    /// 库房等级
    /// </summary>
    public enum EnumWarehouseLevel
    {
        /// <summary>
        /// 一级
        /// </summary>
        [Description("Cấp 1")]
        OneLevel = 1,

        /// <summary>
        /// 二级
        /// </summary>
        [Description("Cấp 2")]
        TwoLevel = 2,

        /// <summary>
        /// 三级
        /// </summary>
        [Description("Cấp 3")]
        ThreeLevel = 3,

        /// <summary>
        /// 四级
        /// </summary>
        [Description("Cấp 4")]
        FourLevel = 4,

        /// <summary>
        /// 五级
        /// </summary>
        [Description("Cấp 5")]
        FiveLevel = 5,

        /// <summary>
        /// 六级
        /// </summary>
        [Description("Cấp 6")]
        SixLevel = 6,

    }

    /// <summary>
    /// 有效/无效
    /// </summary>
    public enum Enumzt
    {
        /// <summary>
        /// 无效
        /// </summary>
        [Description("Không hiệu lực")]
        Disable = 0,

        /// <summary>
        /// 有效
        /// </summary>
        [Description("Hiệu lực")]
        Enable = 1

    }

    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EnumAuditState
    {

        /// <summary>
        /// 待处理
        /// </summary>
        [Description("Chờ xử lý")]
        Waiting = 0,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("Đã duyệt")]
        Adopt = 1,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("Không duyệt")]
        Refuse = 2,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("Đã hủy")]
        Cancelled = 3,

        /// <summary>
        /// 暂存
        /// </summary>
        [Description("Lưu tạm")]
        Temporary = 4,
    }

    /// <summary>
    /// 单据类型
    /// </summary>
    public enum EnumOutOrInStorageBillType
    {
        /// <summary>
        /// 外部入库
        /// </summary>
        [Description("外部入库")]
        Wbrk = 1,

        /// <summary>
        /// 外部出库
        /// </summary>
        [Description("外部出库")]
        Wbck = 2,

        /// <summary>
        /// 直接出库
        /// </summary>
        [Description("直接出库")]
        Zjck = 3,

        /// <summary>
        /// 申领出库
        /// </summary>
        [Description("申领出库")]
        Slck = 4,

        /// <summary>
        /// 内部退货
        /// </summary>
        [Description("内部退货")]
        Nbth = 5,

        /// <summary>
        /// 报损报溢
        /// </summary>
        [Description("报损报溢")]
        Bsby = 6,

        /// <summary>
        /// 配送至科室
        /// </summary>
        [Description("出库至科室")] //单据号ckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// 科室申领
        /// </summary>
        [Description("科室申领")]
        kssl = 8,

        /// <summary>
        /// 科室申领出库
        /// </summary>
        [Description("科室申领出库")]
        ksSlck = 9,

        /// <summary>
        /// 采购计划
        /// </summary>
        [Description("采购计划")]
        purchasingPlan = 10,

        /// <summary>
        /// 采购订单
        /// </summary>
        [Description("采购订单")]
        purchasingOrder = 11,
    }

    /// <summary>
    /// 库存显示
    /// </summary>
    public enum EnumKCXS
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("Tất cả")]
        None = -1,
        /// <summary>
        /// 显示零库存
        /// </summary>
        [Description("Hiện tồn kho bằng 0")]
        Xslkc = 0,
        /// <summary>
        /// 不显示理论数量为零
        /// </summary>
        [Description("Không hiện số lượng lý thuyết bằng 0")]
        Bxsllslwl = 1,
        /// <summary>
        /// 不显示实际数量为零
        /// </summary>
        [Description("Không hiện số lượng thực tế bằng 0")]
        Bxssjslwl = 2,
        /// <summary>
        /// 不显示实际数量为零
        /// </summary>
        [Description("Không hiện khi cả hai bằng 0")]
        Bxslzdwl = 4
    }

    /// <summary>
    /// 库存锁
    /// </summary>
    public enum EnumKcLock
    {
        /// <summary>
        /// 无锁
        /// </summary>
        [Description("Không khóa")]
        none = 0,

        /// <summary>
        /// 开始盘点
        /// </summary>
        [Description("Bắt đầu kiểm kê")]
        InventoryStart = 1,

        /// <summary>
        /// 结束盘点
        /// </summary>
        [Description("Kết thúc kiểm kê")]
        InventoryEnd = 2,

        /// <summary>
        /// 损益
        /// </summary>
        [Description("Lãi lỗ")]
        ProfitAndLoss = 3,

        /// <summary>
        /// 出入库单据审核
        /// </summary>
        [Description("Duyệt phiếu nhập/xuất kho")]
        OutgoingOrWarehousing = 4
    }

    /// <summary>
    /// 损益标志
    /// </summary>
    public enum EnumSybz
    {
        /// <summary>
        /// 报损
        /// </summary>
        [Description("Báo hỏng")]
        Loss = 0,

        /// <summary>
        /// 报溢
        /// </summary>
        [Description("Báo thừa")]
        Profit = 1,
    }

    #region 调价

    /// <summary>
    /// 调价审核标志 0:未审核 1:已审核 2:已拒绝 3.已撤销
    /// </summary>
    public enum EnumTjShzt
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("Chưa duyệt")]
        Waiting = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("Đã duyệt")]
        Audited = 1,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description("Đã từ chối")]
        Refuse = 2,

        /// <summary>
        /// 已撤销
        /// </summary>
        [Description("Đã thu hồi")]
        Revoke = 3
    }

    /// <summary>
    /// 调价执行标志
    /// </summary>
    public enum EnumTjZxbz
    {
        /// <summary>
        /// 未执行
        /// </summary>
        [Description("Chưa thực hiện")]
        Not = 0,

        /// <summary>
        /// 已执行
        /// </summary>
        [Description("Đã thực hiện")]
        Already = 1
    }
    #endregion

    /// <summary>
    /// 生产商/经销商/其他  公司类型
    /// </summary>
    public enum EnumSupplierType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("Khác")]
        Other = 0,

        /// <summary>
        /// 生产商
        /// </summary>
        [Description("Nhà sản xuất")]
        Producer = 1,

        /// <summary>
        /// 经销商
        /// </summary>
        [Description("Nhà phân phối")]
        Distributor = 2,
    }

    /// <summary>
    /// 处理过程   1:待处理；2:审核通过；3:审核不通过；4:配送；5:部分完成；6:完成；7:拒收
    /// </summary>
    public enum EnumApplyProcess
    {
        /// <summary>
        /// 等待处理
        /// </summary>
        [Description("Chờ xử lý")]
        Waiting = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("Đã duyệt")]
        AuditApproved = 2,

        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("Không duyệt")]
        AuditFailed = 3,

        /// <summary>
        /// 配送中
        /// </summary>
        [Description("Đang giao")]
        Distributing = 4,

        /// <summary>
        /// 部分完成
        /// </summary>
        [Description("Hoàn thành một phần")]
        PartialCompletion = 5,

        /// <summary>
        /// 全部完成
        /// </summary>
        [Description("Hoàn thành tất cả")]
        Completion = 6,

        /// <summary>
        /// 配送拒收
        /// </summary>
        [Description("Từ chối nhận hàng")]
        Rejection = 7,
    }

    /// <summary>
    /// 单据类型 1-科室申领；2-库房申领
    /// </summary>
    public enum EnumApplyType
    {
        /// <summary>
        /// 待处理
        /// </summary>
        [Description("Khoa phòng lĩnh")]
        DepartmentApply = 1,

        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("Kho lĩnh")]
        WarehouseApply = 2,
    }

    /// <summary>
    /// 采购订单处理流程 -1：拒处理； 0：待处理； 1：备货； 2：配送； 3：签收； 4：完成； 5：拒签 
    /// </summary>
    public enum EnumOrderProcess
    {
        /// <summary>
        /// 拒处理
        /// </summary>
        [Description("Từ chối xử lý")]
        Refusal =-1,

        /// <summary>
        /// 待处理
        /// </summary>
        [Description("Chờ xử lý")]
        Waiting = 0,

        /// <summary>
        /// 备货
        /// </summary>
        [Description("Chuẩn bị hàng")]
        PreparingGoods = 1,

        /// <summary>
        /// 配送
        /// </summary>
        [Description("Giao hàng")]
        Delivering = 2,

        /// <summary>
        /// 签收
        /// </summary>
        [Description("Đã ký nhận")]
        SignFor = 3,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("Hoàn thành")]
        Complete = 4,

        /// <summary>
        /// 拒签
        /// </summary>
        [Description("Từ chối ký")]
        RefusalSign = 5
    }

    /// <summary>
    /// 订单类型 0：暂存单；1：正式单
    /// </summary>
    public enum EnumOrderTypeHrp
    {

        /// <summary>
        /// 暂存单
        /// </summary>
        [Description("Phiếu lưu tạm")]
        TempOrder = 0,

        /// <summary>
        /// 正式单
        /// </summary>
        [Description("Phiếu chính thức")]
        OfficialOrder = 1,

        /// <summary>
        /// 作废单
        /// </summary>
        [Description("Phiếu hủy")]
        BadOrder = 2,
    }

	/// <summary>
	/// 自负性质 v2 
	/// 重庆再用
	/// </summary>
	public enum EnumZFXZv2
	{
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


    #region 采购

    //4.1.1 操作类型
    public enum EnumCZLX
    {
        [Description("Thêm mới")] XZ=1,
        [Description("Hủy")] ZF=3,
    }

    //4.1.2 采购单类型
    public enum EnumCGDLX
    {
        [Description("Phiếu mua sắm tự túc của bệnh viện")] YYZXCGD= 1,
        [Description("Phiếu mua sắm kho thuốc ủy thác")] TGYKCGD= 2,
    }

    //4.1.3 采购类型
    public enum EnumCGLX
    {
        [Description("Mua sắm đấu thầu")]
        ZBCG = 1,
        [Description("Mua sắm theo số lượng")]
        DLCG = 2,
        [Description("Mua sắm qua mạng")]
        GWCG = 3,
        [Description("Khác")]
        QT = 9,
    }

    //4.1.4 配送要求
    public enum EnumPSYQ
    {
        [Description("Giao theo phiếu")] ADPS= 1,
        [Description("Giao theo nhu cầu")] AXPS= 2,
    }

    //4.1.5 退货类型

    public enum EnumTHLX
    {
        [Description("Trả hàng bình thường")] ZCTH= 1,
        [Description("Trả hàng bù chênh lệch")] BCJTH= 2,

    }

    //4.1.6 配送验收类型
    public enum EnumPSYSLX
    {
        [Description("Nghiệm thu trước")] YYS= 1,
        [Description("Nghiệm thu thực tế")] SYS= 2,

    }

    //4.1.7 单据填写方
    public enum EnumDJTXF
    {
        [Description("Bên mua điền")]
        CGFTX = 1,
        [Description("Bên bán điền hộ")]
        XSFDT = 2,
    }

    //4.1.8 配送单状态
    public enum EnumPSDZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Hủy")] ZF= 1,
        [Description("Đã xác nhận, chưa nghiệm thu")] YQR= 10,

    }
    //4.1.9 采购明细状态
    public enum EnumCGMXZT
    {
        [Description("Chờ gửi")] DTJ= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Chờ duyệt")] DSH= 10,
        [Description("Không duyệt")] SHBTG= 11,
        [Description("Chờ doanh nghiệp xác nhận tiếp nhận")] DQYJSQR= 20,
        [Description("Doanh nghiệp đã xác nhận giao hàng")] QYYJSQRPS= 21,
        [Description("Doanh nghiệp đã xác nhận không giao hàng")] QYYJSQRBYPS= 22,
        [Description("Doanh nghiệp đã giao xong")] QYYPSWC= 23,
    }

    //4.1.10 企业库存
    public enum EnumQYKC
    {
        [Description("Đầy đủ")] CZ= 1,
        [Description("Có hàng, giới hạn")] YHXL= 2,
        [Description("Có hàng, số lượng ít")] YHSL= 3,
        [Description("Tạm hết hàng")] ZWH= 4,
        [Description("Không công bố")] BYPL= 5,
    }

    //4.1.11 发票状态
    public enum EnumFPZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Chưa nghiệm thu")] WYS= 10,
        [Description("Đã nghiệm thu")] YYS= 20,
        [Description("Đã từ chối nhận")] YJS= 21,
        [Description("Đã thanh toán")] FPYZF= 30,
    }

    //4.1.12 配送明细状态
    public enum EnumPSMXZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã gửi, chưa nghiệm thu")] YTJWYS= 10,
        [Description("Đang nghiệm thu")] YSZ= 20,
        [Description("Nghiệm thu nhập kho xong")] YSRKWC= 30,
    }

    //4.1.13 退货明细状态
    public enum EnumTHMXZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã xác nhận, chờ doanh nghiệp tiếp nhận")] YQRDQYJS= 10,
        [Description("Doanh nghiệp đã xác nhận xử lý trả hàng")] QYYQRTHCL= 20,
        [Description("Doanh nghiệp đã xác nhận không xử lý")] QYYQRBYCL= 21,
    }

    //4.1.14 证件类型
    public enum EnumZJLX
    {
        [Description("Giấy chứng nhận mã tổ chức")] ZZJGDMZ= 1,
        [Description("Giấy phép kinh doanh")] YYZZ= 2,
        [Description("Giấy phép hoạt động")] JYXKZ= 3,
        [Description("Giấy phép sản xuất")] SCXKZ= 4,
    }

    //4.1.15 配送明细条码类型
    public enum EnumPSMXTMLX
    {
        [Description("Mã vạch GS1")] GSI= 1,
        [Description("Mã vạch HIBC")] HIBC= 2,
        [Description("Khác")] QT= 99,
    }

    //4.1.16 发票验收结果
    public enum EnumFPYSJG
    {
        [Description("Nghiệm thu đạt")] YSTG= 1,
        [Description("Nghiệm thu không đạt")] YSBTG= 0,
    }

    //4.1.17 采购单状态
    public enum EnumCGDZT
    {
        [Description("Chờ gửi")] DTJ= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã gửi")] YTJ= 20,
    }

    //4.1.18 退货单状态
    public enum EnumTHDZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã xác nhận")] YQR= 10,

    }

    //4.1.19 采购方式

    public enum EnumCGFS
    {
        [Description("Mua sắm ngoài hệ thống")]
        FXTCG = 1,
        [Description("Mua sắm theo hệ thống")]
        AXTCG = 2,
    }

    //4.1.20 是否含伴随服务
    public enum EnumSFHBSFW
    {
        [Description("Không")] F= 0,
        [Description("Có")] S= 1,
    }

    
    #endregion
}
