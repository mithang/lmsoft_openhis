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
        [Description("Cấp 1")]
        OneLevel = 1,

        /// <summary>
        /// Cấp 2
        /// </summary>
        [Description("Cấp 2")]
        TwoLevel = 2,

        /// <summary>
        /// Cấp 3
        /// </summary>
        [Description("Cấp 3")]
        ThreeLevel = 3,

        /// <summary>
        /// Cấp 4
        /// </summary>
        [Description("Cấp 4")]
        FourLevel = 4,

        /// <summary>
        /// Cấp 5
        /// </summary>
        [Description("Cấp 5")]
        FiveLevel = 5,

        /// <summary>
        /// Cấp 6
        /// </summary>
        [Description("Cấp 6")]
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
        [Description("Không hiệu lực")]
        Disable = 0,

        /// <summary>
        /// Hiệu lực
        /// </summary>
        [Description("Hiệu lực")]
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
        [Description("Chờ xử lý")]
        Waiting = 0,

        /// <summary>
        /// Đã duyệt
        /// </summary>
        [Description("Đã duyệt")]
        Adopt = 1,

        /// <summary>
        /// Không duyệt
        /// </summary>
        [Description("Không duyệt")]
        Refuse = 2,

        /// <summary>
        /// Đã hủy bỏ
        /// </summary>
        [Description("Đã hủy")]
        Cancelled = 3,

        /// <summary>
        /// Lưu tạm
        /// </summary>
        [Description("Lưu tạm")]
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
        [Description("Nhập kho ngoài")]
        Wbrk = 1,

        /// <summary>
        /// Xuất kho ngoài
        /// </summary>
        [Description("Xuất kho ngoài")]
        Wbck = 2,

        /// <summary>
        /// Xuất kho trực tiếp
        /// </summary>
        [Description("Xuất kho trực tiếp")]
        Zjck = 3,

        /// <summary>
        /// Xuất kho theo phiếu lĩnh
        /// </summary>
        [Description("Xuất kho theo đơn lĩnh")]
        Slck = 4,

        /// <summary>
        /// Trả hàng nội bộ
        /// </summary>
        [Description("Trả lại nội bộ")]
        Nbth = 5,

        /// <summary>
        /// Báo hỏng/báo thừa
        /// </summary>
        [Description("Báo hỏng/báo thừa")]
        Bsby = 6,

        /// <summary>
        /// Phân phối đến khoa phòng
        /// </summary>
        [Description("Xuất kho đến khoa")] //Số chứng từ ckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// Khoa phòng lĩnh
        /// </summary>
        [Description("Khoa lĩnh")]
        kssl = 8,

        /// <summary>
        /// Xuất kho theo phiếu lĩnh khoa phòng
        /// </summary>
        [Description("Khoa lĩnh xuất kho")]
        ksSlck = 9,

        /// <summary>
        /// Kế hoạch mua sắm
        /// </summary>
        [Description("Kế hoạch mua sắm")]
        purchasingPlan = 10,

        /// <summary>
        /// Đơn hàng mua sắm
        /// </summary>
        [Description("Đơn hàng mua sắm")]
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
        [Description("Tất cả")]
        None = -1,
        /// <summary>
        /// Hiển thị tồn kho bằng 0
        /// </summary>
        [Description("Hiện tồn kho bằng 0")]
        Xslkc = 0,
        /// <summary>
        /// Không hiển thị số lượng lý thuyết bằng 0
        /// </summary>
        [Description("Không hiện số lượng lý thuyết bằng 0")]
        Bxsllslwl = 1,
        /// <summary>
        /// Không hiển thị số lượng thực tế bằng 0
        /// </summary>
        [Description("Không hiện số lượng thực tế bằng 0")]
        Bxssjslwl = 2,
        /// <summary>
        /// Không hiển thị số lượng thực tế bằng 0
        /// </summary>
        [Description("Không hiện khi cả hai bằng 0")]
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
        [Description("Không khóa")]
        none = 0,

        /// <summary>
        /// Bắt đầu kiểm kê
        /// </summary>
        [Description("Bắt đầu kiểm kê")]
        InventoryStart = 1,

        /// <summary>
        /// Kết thúc kiểm kê
        /// </summary>
        [Description("Kết thúc kiểm kê")]
        InventoryEnd = 2,

        /// <summary>
        /// Chênh lệch
        /// </summary>
        [Description("Lãi lỗ")]
        ProfitAndLoss = 3,

        /// <summary>
        /// Duyệt chứng từ nhập/xuất kho
        /// </summary>
        [Description("Duyệt phiếu nhập/xuất kho")]
        OutgoingOrWarehousing = 4
    }

    /// <summary>
    /// Cờ chênh lệch
    /// </summary>
    public enum EnumSybz
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
        [Description("Chưa duyệt")]
        Waiting = 0,

        /// <summary>
        /// Đã duyệt
        /// </summary>
        [Description("Đã duyệt")]
        Audited = 1,

        /// <summary>
        /// Đã từ chối
        /// </summary>
        [Description("Đã từ chối")]
        Refuse = 2,

        /// <summary>
        /// Đã hủy
        /// </summary>
        [Description("Đã thu hồi")]
        Revoke = 3
    }

    /// <summary>
    /// Cờ thực thi điều chỉnh giá
    /// </summary>
    public enum EnumTjZxbz
    {
        /// <summary>
        /// Chưa thực thi
        /// </summary>
        [Description("Chưa thực hiện")]
        Not = 0,

        /// <summary>
        /// Đã thực thi
        /// </summary>
        [Description("Đã thực hiện")]
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
        [Description("Khác")]
        Other = 0,

        /// <summary>
        /// Nhà sản xuất
        /// </summary>
        [Description("Nhà sản xuất")]
        Producer = 1,

        /// <summary>
        /// Nhà phân phối
        /// </summary>
        [Description("Nhà phân phối")]
        Distributor = 2,
    }

    /// <summary>
    /// Quá trình xử lý   1:Chờ xử lý；2:Đã duyệt；3:Không duyệt；4:Phân phối；5:Hoàn thành một phần；6:Hoàn thành；7:Từ chối nhận
    /// </summary>
    public enum EnumApplyProcess
    {
        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("Chờ xử lý")]
        Waiting = 1,

        /// <summary>
        /// Đã duyệt
        /// </summary>
        [Description("Đã duyệt")]
        AuditApproved = 2,

        /// <summary>
        /// Không duyệt
        /// </summary>
        [Description("Không duyệt")]
        AuditFailed = 3,

        /// <summary>
        /// Đang phân phối
        /// </summary>
        [Description("Đang giao")]
        Distributing = 4,

        /// <summary>
        /// Hoàn thành một phần
        /// </summary>
        [Description("Hoàn thành một phần")]
        PartialCompletion = 5,

        /// <summary>
        /// Hoàn thành tất cả
        /// </summary>
        [Description("Hoàn thành tất cả")]
        Completion = 6,

        /// <summary>
        /// Từ chối nhận phân phối
        /// </summary>
        [Description("Từ chối nhận hàng")]
        Rejection = 7,
    }

    /// <summary>
    /// Loại chứng từ 1-Khoa phòng lĩnh；2-Kho lĩnh
    /// </summary>
    public enum EnumApplyType
    {
        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("Khoa phòng lĩnh")]
        DepartmentApply = 1,

        /// <summary>
        /// Đã duyệt
        /// </summary>
        [Description("Kho lĩnh")]
        WarehouseApply = 2,
    }

    /// <summary>
    /// Quy trình xử lý đơn mua sắm -1：Từ chối xử lý； 0：Chờ xử lý； 1：Chuẩn bị hàng； 2：Phân phối； 3：Ký nhận； 4：Hoàn thành； 5：Từ chối ký nhận 
    /// </summary>
    public enum EnumOrderProcess
    {
        /// <summary>
        /// Từ chối xử lý
        /// </summary>
        [Description("Từ chối xử lý")]
        Refusal =-1,

        /// <summary>
        /// Chờ xử lý
        /// </summary>
        [Description("Chờ xử lý")]
        Waiting = 0,

        /// <summary>
        /// Chuẩn bị hàng
        /// </summary>
        [Description("Chuẩn bị hàng")]
        PreparingGoods = 1,

        /// <summary>
        /// Phân phối
        /// </summary>
        [Description("Giao hàng")]
        Delivering = 2,

        /// <summary>
        /// Ký nhận
        /// </summary>
        [Description("Đã ký nhận")]
        SignFor = 3,

        /// <summary>
        /// Hoàn thành
        /// </summary>
        [Description("Hoàn thành")]
        Complete = 4,

        /// <summary>
        /// Từ chối ký nhận
        /// </summary>
        [Description("Từ chối ký")]
        RefusalSign = 5
    }

    /// <summary>
    /// Loại đơn hàng 0：Phiếu lưu tạm；1：Phiếu chính thức
    /// </summary>
    public enum EnumOrderTypeHrp
    {

        /// <summary>
        /// Phiếu lưu tạm
        /// </summary>
        [Description("Phiếu lưu tạm")]
        TempOrder = 0,

        /// <summary>
        /// Phiếu chính thức
        /// </summary>
        [Description("Phiếu chính thức")]
        OfficialOrder = 1,

        /// <summary>
        /// Phiếu hủy bỏ
        /// </summary>
        [Description("Phiếu hủy")]
        BadOrder = 2,
    }

	/// <summary>
	/// Tính chất tự trả v2 
	/// Tái sử dụng ở Trùng Khánh
	/// </summary>
	public enum EnumZFXZv2
	{
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


    #region Mua sắm

    //4.1.1 Loại thao tác
    public enum EnumCZLX
    {
        [Description("Thêm mới")] XZ=1,
        [Description("Hủy")] ZF=3,
    }

    //4.1.2 Loại đơn mua sắm
    public enum EnumCGDLX
    {
        [Description("Phiếu mua sắm tự túc của bệnh viện")] YYZXCGD= 1,
        [Description("Phiếu mua sắm kho thuốc ủy thác")] TGYKCGD= 2,
    }

    //4.1.3 Loại mua sắm
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

    //4.1.4 Yêu cầu phân phối
    public enum EnumPSYQ
    {
        [Description("Giao theo phiếu")] ADPS= 1,
        [Description("Giao theo nhu cầu")] AXPS= 2,
    }

    //4.1.5 Loại trả hàng

    public enum EnumTHLX
    {
        [Description("Trả hàng bình thường")] ZCTH= 1,
        [Description("Trả hàng bù chênh lệch")] BCJTH= 2,

    }

    //4.1.6 Loại nghiệm thu phân phối
    public enum EnumPSYSLX
    {
        [Description("Nghiệm thu trước")] YYS= 1,
        [Description("Nghiệm thu thực tế")] SYS= 2,

    }

    //4.1.7 Bên lập chứng từ
    public enum EnumDJTXF
    {
        [Description("Bên mua điền")]
        CGFTX = 1,
        [Description("Bên bán điền hộ")]
        XSFDT = 2,
    }

    //4.1.8 Trạng thái phiếu phân phối
    public enum EnumPSDZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Hủy")] ZF= 1,
        [Description("Đã xác nhận, chưa nghiệm thu")] YQR= 10,

    }
    //4.1.9 Trạng thái chi tiết mua sắm
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

    //4.1.10 Tồn kho doanh nghiệp
    public enum EnumQYKC
    {
        [Description("Đầy đủ")] CZ= 1,
        [Description("Có hàng, giới hạn")] YHXL= 2,
        [Description("Có hàng, số lượng ít")] YHSL= 3,
        [Description("Tạm hết hàng")] ZWH= 4,
        [Description("Không công bố")] BYPL= 5,
    }

    //4.1.11 Trạng thái hóa đơn
    public enum EnumFPZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Chưa nghiệm thu")] WYS= 10,
        [Description("Đã nghiệm thu")] YYS= 20,
        [Description("Đã từ chối nhận")] YJS= 21,
        [Description("Đã thanh toán")] FPYZF= 30,
    }

    //4.1.12 Trạng thái chi tiết phân phối
    public enum EnumPSMXZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã gửi, chưa nghiệm thu")] YTJWYS= 10,
        [Description("Đang nghiệm thu")] YSZ= 20,
        [Description("Nghiệm thu nhập kho xong")] YSRKWC= 30,
    }

    //4.1.13 Trạng thái chi tiết trả hàng
    public enum EnumTHMXZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã xác nhận, chờ doanh nghiệp tiếp nhận")] YQRDQYJS= 10,
        [Description("Doanh nghiệp đã xác nhận xử lý trả hàng")] QYYQRTHCL= 20,
        [Description("Doanh nghiệp đã xác nhận không xử lý")] QYYQRBYCL= 21,
    }

    //4.1.14 Loại giấy tờ
    public enum EnumZJLX
    {
        [Description("Giấy chứng nhận mã tổ chức")] ZZJGDMZ= 1,
        [Description("Giấy phép kinh doanh")] YYZZ= 2,
        [Description("Giấy phép hoạt động")] JYXKZ= 3,
        [Description("Giấy phép sản xuất")] SCXKZ= 4,
    }

    //4.1.15 Loại mã vạch chi tiết phân phối
    public enum EnumPSMXTMLX
    {
        [Description("Mã vạch GS1")] GSI= 1,
        [Description("Mã vạch HIBC")] HIBC= 2,
        [Description("Khác")] QT= 99,
    }

    //4.1.16 Kết quả nghiệm thu hóa đơn
    public enum EnumFPYSJG
    {
        [Description("Nghiệm thu đạt")] YSTG= 1,
        [Description("Nghiệm thu không đạt")] YSBTG= 0,
    }

    //4.1.17 Trạng thái đơn mua sắm
    public enum EnumCGDZT
    {
        [Description("Chờ gửi")] DTJ= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã gửi")] YTJ= 20,
    }

    //4.1.18 Trạng thái phiếu trả hàng
    public enum EnumTHDZT
    {
        [Description("Chờ xác nhận")] DQR= 0,
        [Description("Đã hủy")] YZF= 1,
        [Description("Đã xác nhận")] YQR= 10,

    }

    //4.1.19 Phương thức mua sắm

    public enum EnumCGFS
    {
        [Description("Mua sắm ngoài hệ thống")]
        FXTCG = 1,
        [Description("Mua sắm theo hệ thống")]
        AXTCG = 2,
    }

    //4.1.20 Có kèm dịch vụ đi kèm không
    public enum EnumSFHBSFW
    {
        [Description("Không")] F= 0,
        [Description("Có")] S= 1,
    }

    
    #endregion
}
