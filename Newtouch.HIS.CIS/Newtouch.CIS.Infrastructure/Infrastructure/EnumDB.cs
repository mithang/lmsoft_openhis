using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// ngàychíghi chépLoại
    /// </summary>
    public enum DbLogType_Ex
    {

    }

    /// <summary>
    /// chứngmụcLoại
    /// </summary>
    public enum EnumZjlx
    {
        /// <summary>
        /// CMND/CCCD
        /// </summary>
        [Description("身份证")]
        sfz = 1,
        /// <summary>
        /// Hộ chiếu
        /// </summary>
        [Description("护照")]
        hz,
        /// <summary>
        /// Giấy chứng nhận sĩ quan
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
    /// Tính chất tự trả（Thuốc、Mụctừ điểncủaThuộc tính）
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Được thanh toán
        /// </summary>
        [Description("可报")]
        KB = 0,
        /// <summary>
        /// tự chi trả
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// Tự trả theo loại
        /// </summary>
        [Description("分类自负")]
        FLZF = 2,
        /// <summary>
        /// Tự túc hoàn toàn
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
	/// ThuốcLoại
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
	/// Kháng sinhcấploại
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
	/// Khám bệnhTrạng thái
	/// </summary>
	public enum EnumJzzt
    {
        /// <summary>
        /// chưaKhám bệnh
        /// </summary>
        [Description("未就诊")]
        NotYetTreate = 1,
        /// <summary>
        /// Khám bệnhtrong
        /// </summary>
        [Description("就诊中")]
        Treating = 2,
        /// <summary>
        /// Khám bệnhKết thúc
        /// </summary>
        [Description("就诊结束")]
        Treated = 3,
    }

    /// <summary>
    /// Ngoại trúLoại mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// thông thườngNgoại trú
        /// </summary>
        [Description("普通门诊")]
        generalOutpat = 1,
        /// <summary>
        /// gấpkhám
        /// </summary>
        [Description("急诊")]
        emerDiagnosis = 2,
        /// <summary>
        /// chuyên giaNgoại trú
        /// </summary>
        [Description("专家门诊")]
        expertOutpat = 3,
        /// <summary>
        /// chuyên giaNgoại trú
        /// </summary>
        [Description("特病门诊")]
        SpecialOutpat = 4,
        /// <summary>
        /// trọnglớnbệnhbệnhNgoại trú
        /// </summary>
        [Description("重大疾病门诊")]
        MajorDiseases = 5,
        /// <summary>
        /// Bệnh mạn tínhNgoại trú
        /// </summary>
        [Description("慢性病门诊")]
        ChronicDisease = 6,
        /// <summary>
        /// cư dânhaibệnh
        /// </summary>
        [Description("居民两病")]
        jmlb = 7,
        /// <summary>
        /// ýngoài伤害Ngoại trú
        /// </summary>
        [Description("意外伤害门诊")]
        ywshmz = 8,
        /// <summary>
        /// sinhdụcNgoại trú
        /// </summary>
        [Description("生育门诊")]
        symz = 9,
        /// <summary>
        /// 耐nhiềuthuốckếthạt nhânNgoại trú
        /// </summary>
        [Description("耐多药结核门诊")]
        ndyjhmz = 10,
        /// <summary>
        /// trẻ emhaibệnhNgoại trú
        /// </summary>
        [Description("儿童两病门诊")]
        etlbmz = 11,
        /// <summary>
        /// đơn giản易Ngoại trú
        /// </summary>
        [Description("简易门诊")]
        jymz = 12
    }

    /// <summary>
    /// Đơn thuốcLoại
    /// </summary>
    public enum EnumCflx
    {
        /// <summary>
        /// Thuốc TâyĐơn thuốc
        /// </summary>
        [Description("西药处方")]
        WMPres = 1,
        /// <summary>
        /// Thuốc Đông yĐơn thuốc
        /// </summary>
        [Description("中药处方")]
        TCMPres = 2,
        /// <summary>
        /// Phục hồi chức năngĐơn thuốc
        /// </summary>
        [Description("康复处方")]
        RehabPres = 3,
        /// <summary>
        /// Xét nghiệmĐơn thuốc
        /// </summary>
        [Description("检验处方")]
        InspectionPres = 4,
        /// <summary>
        /// Kiểm traĐơn thuốc
        /// </summary>
        [Description("检查处方")]
        ExaminationPres = 5,
        /// <summary>
        /// Hạng mục thông thườngĐơn thuốc
        /// </summary>
        [Description("常规项目处方")]
        RegularItemPres = 6,
        /// <summary>
        /// điện tửĐơn thuốc
        /// </summary>
		[Description("电子处方")]
		Dzcf =7,
        /// <summary>
        /// ydùngVật tư tiêu hao
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
        /// chínhChẩn đoán
        /// </summary>
        [Description("主诊断")]
        Main = 1,
        /// <summary>
        /// phụChẩn đoán
        /// </summary>
        [Description("副诊断")]
        Deputy = 2
    }

    /// <summary>
    /// Đơn thuốcmẫuLoại
    /// （Hồ sơmẫuLoạicũnglàdùngnàyLiệt kê）
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
    /// Xét nghiệmKiểm traBộ mẫuLoại  （Xét nghiệmKiểm trachungdùngmộttrangmặt，chỉlàtham số（tứctype）khôngcùng）
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
    /// Xét nghiệmKiểm tramẫuLoại  （Xét nghiệmKiểm trachungdùngmộttrangmặt，chỉlàtham số（tứctype）khôngcùng）
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
    /// Mục thu phíChiến lược tính giá
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
    /// lịch sửHồ sơđếnnguồn
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
    /// Đăng nhậpCờ
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
    /// uốngănLoại
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
    /// Ngoại trúđăng ký khámđếnnguồn
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
    /// xếpđộigọisốTrạng thái
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
    /// Dấu hiệu ngoại trú/nội trú  0 Chung 1 chỉNgoại trú 2 chỉNhập viện 3 Hệ thống（Ngoại trúNhập việnđềukhôngcó thểdùng）VDnếu：nhàgiường
    /// </summary>
    public enum Enummzzybz
    {

        /// <summary>
        /// chỉNgoại trú
        /// </summary>
        [Description("门诊")]
        mz = 1,
        /// <summary>
        /// chỉNhập viện
        /// </summary>
        [Description("住院")]
        zy = 2,
        ///// <summary>
        ///// Chung
        ///// </summary>
        //[Description("Chung")]
        //ty = 9,
    }
    public enum Enumzxzt
    {
        /// <summary>
        /// chưaThực hiện
        /// </summary>
        [Description("未执行")]
        wzx = 0,
        /// <summary>
        /// đãThực hiện
        /// </summary>
        [Description("已执行")]
        yzx = 1
       
    } 
    /// <summary>
    /// phútkhámgọisốTrạng thái
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
    /// xatrìnhkhámtrịTrạng thái
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
    /// lãi lỗCờ
    /// </summary>
    public enum EnumSybz
    {
        /// <summary>
        /// Báo hỏng
        /// </summary>
        [Description("报损")]
        Loss = 0,

        /// <summary>
        /// báo thừa
        /// </summary>
        [Description("报溢")]
        Profit = 1,
    }
    public enum EnumOutOrInStorageBillType
    {
        /// <summary>
        /// ngoàibộNhập kho
        /// </summary>
        [Description("外部入库")]
        Wbrk = 1,

        /// <summary>
        /// ngoàibộXuất kho
        /// </summary>
        [Description("外部出库")]
        Wbck = 2,

        /// <summary>
        /// trực tiếpXuất kho
        /// </summary>
        [Description("直接出库")]
        Zjck = 3,

        /// <summary>
        /// đăng kýlĩnhXuất kho
        /// </summary>
        [Description("申领出库")]
        Slck = 4,

        /// <summary>
        /// trongbộtrả hàng
        /// </summary>
        [Description("内部退货")]
        Nbth = 5,

        /// <summary>
        /// Báo hỏngbáo thừa
        /// </summary>
        [Description("报损报溢")]
        Bsby = 6,

        /// <summary>
        /// phốigửiđếnKhoa
        /// </summary>
        [Description("出库至科室")] //phiếusố phiếuckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// Khoađăng kýlĩnh
        /// </summary>
        [Description("科室申领")]
        kssl = 8,

        /// <summary>
        /// Khoađăng kýlĩnhXuất kho
        /// </summary>
        [Description("科室申领出库")]
        ksSlck = 9,

        /// <summary>
        /// Mua hàngtínhvạch
        /// </summary>
        [Description("采购计划")]
        purchasingPlan = 10,

        /// <summary>
        /// Mua hàngđặtphiếu
        /// </summary>
        [Description("采购订单")]
        purchasingOrder = 11,
    }
    /// <summary>
    /// Hiệu lực/Vô hiệu
    /// </summary>
    public enum Enumzt
    {
        /// <summary>
        /// Vô hiệu
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
    /// Phê duyệtTrạng thái
    /// </summary>
    public enum EnumAuditState
    {

        /// <summary>
        /// chờchỗlý
        /// </summary>
        [Description("待处理")]
        Waiting = 0,

        /// <summary>
        /// Phê duyệtthông qua
        /// </summary>
        [Description("审核通过")]
        Adopt = 1,

        /// <summary>
        /// Phê duyệtkhôngthông qua
        /// </summary>
        [Description("审核不通过")]
        Refuse = 2,

        /// <summary>
        /// Đã hủy bỏ
        /// </summary>
        [Description("已作废")]
        Cancelled = 3,

        /// <summary>
        /// tạmtồn tại
        /// </summary>
        [Description("暂存")]
        Temporary = 4,
    }
    /// <summary>
    /// Tồn khoHiển thị
    /// </summary>
    public enum EnumKCXS
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("全部")]
        None = -1,
        /// <summary>
        /// Hiển thịkhôngTồn kho
        /// </summary>
        [Description("显示零库存")]
        Xslkc = 0,
        /// <summary>
        /// khôngHiển thịlýluậnSố lượnglàkhông
        /// </summary>
        [Description("不显示理论数量为零")]
        Bxsllslwl = 1,
        /// <summary>
        /// khôngHiển thịthực tếSố lượnglàkhông
        /// </summary>
        [Description("不显示实际数量为零")]
        Bxssjslwl = 2,
        /// <summary>
        /// khôngHiển thịthực tếSố lượnglàkhông
        /// </summary>
        [Description("不显示两者都为零")]
        Bxslzdwl = 4
    }
}
