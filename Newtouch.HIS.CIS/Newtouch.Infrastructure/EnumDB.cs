using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Loại bản ghi nhật ký
    /// </summary>
    public enum DbLogType_Ex
    {

    }

    /// <summary>
    /// Loại giấy tờ
    /// </summary>
    public enum EnumZjlx
    {
        /// <summary>
        /// Chứng minh nhân dân
        /// </summary>
        [Description("身份证")]
        sfz = 1,
        /// <summary>
        /// Hộ chiếu
        /// </summary>
        [Description("护照")]
        hz,
        /// <summary>
        /// Chứng nhận sĩ quan
        /// </summary>
        [Description("军官证")]
        jgz,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        other = 9
    }

    /// <summary>
    /// Tính chất tự chi trả (thuộc tính của từ điển thuốc, hạng mục)
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Có thể báo
        /// </summary>
        [Description("可报")]
        KB = 0,
        /// <summary>
        /// Tự chi trả
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// Tự chi trả theo phân loại
        /// </summary>
        [Description("分类自负")]
        FLZF = 2,
        /// <summary>
        /// Tự lo hoàn toàn
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
	/// Loại thuốc
	/// </summary>
	public enum EnumYpsx
	{
		[Description("麻精药品（麻醉、精一、精二）")]
		Mzyp = 9,
		[Description("毒性药品")]
		Dxyp = 4,
		[Description("血液制品")]
		Xyzp = 5,
		[Description("激素药品")]
		Jsyp = 6,
		[Description("贵重药品")]
		Gzyp = 7,
		[Description("抗肿瘤药物药品")]
		Kzlywyp = 8,
	}
	/// <summary>
	/// Cấp kháng sinh
	/// </summary>
	public enum EnumKss
	{

		[Description("非限制使用药物")]
		Fxzsyyw = 0,
		[Description("限制使用药物")]
		Xzsyyw = 1,
		[Description("特殊使用药物")]
		Tssyyw = 2,
	}

	/// <summary>
	/// Trạng thái khám bệnh
	/// </summary>
	public enum EnumJzzt
    {
        /// <summary>
        /// Chưa khám
        /// </summary>
        [Description("未就诊")]
        NotYetTreate = 1,
        /// <summary>
        /// Đang khám
        /// </summary>
        [Description("就诊中")]
        Treating = 2,
        /// <summary>
        /// Kết thúc khám
        /// </summary>
        [Description("就诊结束")]
        Treated = 3,
    }

    /// <summary>
    /// Loại ngoại trú mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// Khám ngoại trú thông thường
        /// </summary>
        [Description("普通门诊")]
        generalOutpat = 1,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("急诊")]
        emerDiagnosis = 2,
        /// <summary>
        /// Khám chuyên gia
        /// </summary>
        [Description("专家门诊")]
        expertOutpat = 3,
        /// <summary>
        /// Khám chuyên gia
        /// </summary>
        [Description("特病门诊")]
        SpecialOutpat = 4,
        /// <summary>
        /// Khám ngoại trú bệnh hiểm nghèo
        /// </summary>
        [Description("重大疾病门诊")]
        MajorDiseases = 5,
        /// <summary>
        /// Khám ngoại trú bệnh mạn tính
        /// </summary>
        [Description("慢性病门诊")]
        ChronicDisease = 6,
        /// <summary>
        /// Hai bệnh của cư dân
        /// </summary>
        [Description("居民两病")]
        jmlb = 7,
        /// <summary>
        /// Khám ngoại trú tai nạn thương tích
        /// </summary>
        [Description("意外伤害门诊")]
        ywshmz = 8,
        /// <summary>
        /// Khám ngoại trú sinh đẻ
        /// </summary>
        [Description("生育门诊")]
        symz = 9,
        /// <summary>
        /// Khám ngoại trú lao đa kháng thuốc
        /// </summary>
        [Description("耐多药结核门诊")]
        ndyjhmz = 10,
        /// <summary>
        /// Khám ngoại trú hai bệnh trẻ em
        /// </summary>
        [Description("儿童两病门诊")]
        etlbmz = 11,
        /// <summary>
        /// Khám ngoại trú đơn giản
        /// </summary>
        [Description("简易门诊")]
        jymz = 12
    }

    /// <summary>
    /// Loại đơn thuốc
    /// </summary>
    public enum EnumCflx
    {
        /// <summary>
        /// Đơn thuốc Tây y
        /// </summary>
        [Description("西药处方")]
        WMPres = 1,
        /// <summary>
        /// Đơn thuốc Đông y
        /// </summary>
        [Description("中药处方")]
        TCMPres = 2,
        /// <summary>
        /// Đơn thuốc phục hồi chức năng
        /// </summary>
        [Description("康复处方")]
        RehabPres = 3,
        /// <summary>
        /// Đơn thuốc xét nghiệm
        /// </summary>
        [Description("检验处方")]
        InspectionPres = 4,
        /// <summary>
        /// Đơn thuốc kiểm tra
        /// </summary>
        [Description("检查处方")]
        ExaminationPres = 5,
        /// <summary>
        /// Đơn thuốc hạng mục thông thường
        /// </summary>
        [Description("常规项目处方")]
        RegularItemPres = 6,
        /// <summary>
        /// Đơn thuốc điện tử
        /// </summary>
		[Description("电子处方")]
		Dzcf =7,
        /// <summary>
        /// Vật tư y tế tiêu hao
        /// </summary>
        [Description("医用耗材")]
        Yyhc = 8
    }

    public enum EnumYpyf
    {
        [Description("西医")]
        WM=1,
        [Description("中医")]
        TCM=2
    }

    /// <summary>
    /// Loại chẩn đoán
    /// </summary>
    public enum EnumZdlx
    {
        /// <summary>
        /// Chẩn đoán chính
        /// </summary>
        [Description("主诊断")]
        Main = 1,
        /// <summary>
        /// Chẩn đoán phụ
        /// </summary>
        [Description("副诊断")]
        Deputy = 2
    }

    /// <summary>
    /// Loại mẫu đơn thuốc
    /// (Loại mẫu bệnh án cũng dùng enum này)
    /// </summary>
    public enum EnumCfMbLx
    {
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("个人")]
        personal = 1,
        /// <summary>
        /// Khoa
        /// </summary>
        [Description("科室")]
        department = 2,
        /// <summary>
        /// Toàn viện
        /// </summary>
        [Description("全院")]
        hospital = 3,
    }

    /// <summary>
    /// Loại bộ xét nghiệm/kiểm tra (xét nghiệm và kiểm tra dùng chung một trang, chỉ khác tham số (tức type))
    /// </summary>
    public enum EnumjyjczutaoLx
    {
        /// <summary>
        /// Xét nghiệm
        /// </summary>
        [Description("检验")]
        jy = 1,
        /// <summary>
        /// Kiểm tra
        /// </summary>
        [Description("检查")]
        jc = 2
    }

    /// <summary>
    /// Loại mẫu xét nghiệm/kiểm tra (xét nghiệm và kiểm tra dùng chung một trang, chỉ khác tham số (tức type))
    /// </summary>
    public enum EnumjyjcmbLx
    {
        /// <summary>
        /// Xét nghiệm
        /// </summary>
        [Description("检验")]
        jy = 1,
        /// <summary>
        /// Kiểm tra
        /// </summary>
        [Description("检查")]
        jc = 2
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
    /// Nguồn bệnh án trong quá khứ
    /// </summary>
    public enum Enumlsblly
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("本系统")]
        self = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("api")]
        api,
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
    /// Loại ăn uống
    /// </summary>
    public enum Enumyslb {
        /// <summary>
        /// 
        /// </summary>
        [Description("回民普食")]
        hmps = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("回民半流")]
        hmbl = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("回民流质")]
        hmlz = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("一般普食")]
        ybps = 4,
        /// <summary>
        /// 
        /// </summary>
        [Description("一般半流")]
        ybbl = 5,
        /// <summary>
        /// 
        /// </summary>
        [Description("一般流质")]
        yblz = 6,
        /// <summary>
        /// 
        /// </summary>
        [Description("膳食自备")]
        sszb = 7,
        /// <summary>
        /// 
        /// </summary>
        [Description("禁食")]
        js = 8,
        /// <summary>
        /// 
        /// </summary>
        [Description("肠内营养")]
        cnyy = 9,
        /// <summary>
        /// 
        /// </summary>
        [Description("营养补充剂")]
        yybcj = 10,
    }

    /// <summary>
    /// Nguồn đặt lịch hẹn khám ngoại trú
    /// </summary>
    public enum EnumMzghly
    {
        [Description("His")]
        his = 0,
        [Description("预约")]
        yy = 1,
        [Description("银医通")]
        yyt = 3,
        [Description("微信")]
        wechat = 4,
        [Description("支付宝")]
        alipay = 5,
        [Description("体检")]
        tj = 6,
    }

    /// <summary>
    /// Trạng thái xếp hàng gọi số
    /// </summary>
    public enum EmunQueueCalledStu
    {
        [Description("已签到")]
        sign = 1,
        [Description("已叫")]
        call = 2,
        [Description("过号")]
        pass = 3,
        [Description("应答")]
        reply = 4,
    }

    /// <summary>
    /// Cờ ngoại trú/nội trú 0 Dùng chung 1 Chỉ ngoại trú 2 Chỉ nội trú 3 Hệ thống (ngoại trú và nội trú đều không dùng được) Ví dụ: Giường tại nhà
    /// </summary>
    public enum Enummzzybz
    {

        /// <summary>
        /// Chỉ ngoại trú
        /// </summary>
        [Description("门诊")]
        mz = 1,
        /// <summary>
        /// Chỉ nội trú
        /// </summary>
        [Description("住院")]
        zy = 2,
        ///// <summary>
        ///// Dùng chung
        ///// </summary>
        //[Description("通用")]
        //ty = 9,
    }
    public enum Enumzxzt
    {
        /// <summary>
        /// Chưa thực hiện
        /// </summary>
        [Description("未执行")]
        wzx = 0,
        /// <summary>
        /// Đã thực hiện
        /// </summary>
        [Description("已执行")]
        yzx = 1
       
    } 
    /// <summary>
    /// Trạng thái gọi số phân chẩn
    /// </summary>
    public enum EmunConsultCalledStu
    {
        [Description("待叫号")]
        wait = 1,
        [Description("已叫")]
        call = 2,
        [Description("过号")]
        pass = 3,
        [Description("应答")]
        reply = 4,
        [Description("重新叫号")]
        recall = 5,
    }
    /// <summary>
    /// Trạng thái chẩn trị từ xa
    /// </summary>
    public enum Emunzlzt
    {
        [Description("待确认")]
        dqr = 1,
        [Description("就诊中")]
        jzz = 2,
        [Description("已结束")]
        yjs = 3,
        [Description("已退回")]
        yth = 4,
        [Description("已撤销")]
        ycx = 5,
        [Description("已发药")]
        yfy = 6,
    }
    /// <summary>
    /// Cờ hỏng/thừa
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
    public enum EnumOutOrInStorageBillType
    {
        /// <summary>
        /// Nhập kho bên ngoài
        /// </summary>
        [Description("外部入库")]
        Wbrk = 1,

        /// <summary>
        /// Xuất kho bên ngoài
        /// </summary>
        [Description("外部出库")]
        Wbck = 2,

        /// <summary>
        /// Xuất kho trực tiếp
        /// </summary>
        [Description("直接出库")]
        Zjck = 3,

        /// <summary>
        /// Xuất kho theo đơn xin
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
        /// Phân phối tới khoa
        /// </summary>
        [Description("出库至科室")] //Số chứng từ ckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// Khoa xin lĩnh
        /// </summary>
        [Description("科室申领")]
        kssl = 8,

        /// <summary>
        /// Xuất kho theo đơn xin của khoa
        /// </summary>
        [Description("科室申领出库")]
        ksSlck = 9,

        /// <summary>
        /// Kế hoạch mua sắm
        /// </summary>
        [Description("采购计划")]
        purchasingPlan = 10,

        /// <summary>
        /// Đơn đặt hàng mua sắm
        /// </summary>
        [Description("采购订单")]
        purchasingOrder = 11,
    }
    /// <summary>
    /// Có hiệu lực/Không hiệu lực
    /// </summary>
    public enum Enumzt
    {
        /// <summary>
        /// Không hiệu lực
        /// </summary>
        [Description("无效")]
        Disable = 0,

        /// <summary>
        /// Có hiệu lực
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
        /// Đã hủy
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
        /// Không hiển thị số lượng lý thuyết bằng 0
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
}
