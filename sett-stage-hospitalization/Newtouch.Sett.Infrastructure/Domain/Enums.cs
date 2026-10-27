using System;
using System.ComponentModel;

namespace Newtouch.Sett.Domain
{
    /// <summary>
    /// 日志Bản ghiLoại 枚举项扩展
    /// ValueVui lòngtừ101Bắt đầu
    /// </summary>
    public enum DbLogType_Ex
    {
        /// <summary>
        /// quyết toán xuất viện
        /// </summary>
        [Description("quyết toán xuất viện")]
        DischargeSettlement = 101,

    }

    /// <summary>
    /// Đang ở việncờ
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// Đăng ký nhập viện
        /// </summary>
        [Description("Nhập viện mới")]
        Xry = 0,
        /// <summary>
        /// Đang ở khoa bệnh
        /// </summary>
        [Description("Đang ở khoa bệnh")]
        Bqz = 1,
        /// <summary>
        /// Khoa bệnhXuất viện（出Khoa bệnh）（Chờ quyết toán）
        /// </summary>
        [Description("Chờ quyết toán")]
        Djz = 2,
        /// <summary>
        /// Đã xuất viện（quyết toán xuất viện）
        /// </summary>
        [Description("Đã xuất viện")]
        Ycy = 3,
        /// <summary>
        /// Đang chuyển khoa
        /// </summary>
        [Description("Đang chuyển khoa")]
        Zq = 7,

        /// <summary>
        /// Hủy bỏ bản ghi/HủyNhập viện
        /// </summary>
        [Description("Hủy bỏ bản ghi")]
        Wry = 9,
    }

    /// <summary>
    /// hệ thốngTài khoảnLoại
    /// </summary>
    public enum EnumXTZHXZ
    {
        /// <summary>
        /// Tài khoản đơn vị
        /// </summary>
        [Description("Tài khoản đơn vị")]
        DWZH = 0,
        /// <summary>
        /// Tài khoản cá nhân
        /// </summary>
        [Description("Tài khoản cá nhân")]
        GRZH = 1,
        /// <summary>
        /// Tài khoản giường gia đình
        /// </summary>
        [Description("Tài khoản giường gia đình")]
        JCZH = 2,
        /// <summary>
        /// Tài khoản tạm nộp nhập viện
        /// </summary>
        [Description("Tài khoản tạm nộp nhập viện")]
        ZYYJKZH = 3,
        /// <summary>
        /// Tài khoản trợ giúp khó khăn
        /// </summary>
        [Description("Tài khoản trợ giúp khó khăn")]
        BKZH = 4,
        /// <summary>
        /// Tài khoản đăng ký ngoại trú
        /// </summary>
        [Description("Tài khoản đăng ký ngoại trú")]
        MZDJZH = 5,
        /// <summary>
        /// Tài khoản tạm nộp ngoại trú
        /// </summary>
        [Description("Tài khoản tạm nộp ngoại trú")]
        MZYJKZH = 6
    }

    /// <summary>
    /// Giao dịch BHYTLoại  0 不交易 1 thông thường交易 2 大病交易 3 家床交易 4 工伤交易 5 nhập viện交易 6 bảo hiểm nông nghiệp交易
    /// xt_brxz.ybjylx
    /// </summary>
    public enum EnumYBJYLX
    {
        /// <summary>
        /// 不交易
        /// </summary>
        [Description("不交易")]
        ybjylx0 = 0,
        /// <summary>
        /// thông thường交易
        /// </summary>
        [Description("thông thường交易")]
        ybjylx1 = 1,
        /// <summary>
        /// 大病交易
        /// </summary>
        [Description("大病交易")]
        ybjylx2 = 2,
        /// <summary>
        /// 家床交易
        /// </summary>
        [Description("家床交易")]
        ybjylx3 = 3,
        /// <summary>
        /// 工伤交易
        /// </summary>
        [Description("工伤交易")]
        ybjylx4 = 4,
        /// <summary>
        /// nhập viện交易
        /// </summary>
        [Description("nhập viện交易")]
        ybjylx5 = 5,
        /// <summary>
        /// bảo hiểm nông nghiệp交易
        /// </summary>
        [Description("bảo hiểm nông nghiệp交易")]
        ybjylx6 = 6,
    }

    /// <summary>
    /// Tính chất tự trả（Thuốc、Mục字典củathuộc tính）
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Được báo
        /// </summary>
        [Description("Được báo")]
        KB = 0,
        /// <summary>
        /// Tự phí
        /// </summary>
        [Description("Tự phí")]
        ZF = 1,
        /// <summary>
        /// Tự trả theo loại
        /// </summary>
        [Description("Tự trả theo loại")]
        FLZF = 2,
        /// <summary>
        /// Tuyệt đối tự chi trả
        /// </summary>
        [Description("Tuyệt đối tự chi trả")]
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
    /// 算法 Tính chất tự trả
    /// Xác nhậntheoTỷ lệ tự trảTính出来củaTự trảmột phầncủatính chất
    /// </summary>
    public enum EnumSuanFaZFXZ
    {
        /// <summary>
        /// Tự trả（同Giao dịch BHYTsauTự trảTiền mặt）
        /// </summary>
        [Description("Tự trả")]
        ZF = 0,
        /// <summary>
        /// tự chi trả
        /// </summary>
        [Description("tự chi trả")]
        ZL = 1,
    }

    /// <summary>
    /// Tài khoản收支tính chất 
    /// xt_brzhszjl.szxz
    /// </summary>
    public enum AccSzxz
    {
        [Description("Ngoại trúTài khoản存取")]
        MZZH = 0,
        [Description("家床记帐")]
        JCZH = 1,
        [Description("Quyết toán ngoại trú")]
        MZJS = 2,
        [Description("Khoa bệnh记帐")]
        BQJZ = 3,
        [Description("quyết toán nhập viện")]
        ZYJS = 4,
        [Description("nhập việnTài khoản存取")]
        ZYZH = 5,
        [Description("nhập viện补nộp款")]
        ZYBJK = 6,
        [Description("ghi sổ ngoại trú登记")]
        MZJZDJ = 7
    }

    /// <summary>
    /// ThẻLoại/ThẻCMND/CCCD份标识
    /// </summary>
    [Description("ThẻLoại")]
    public enum EnumCardType
    {
        /// <summary>
        /// Thẻ từ BHYT
        /// </summary>
        [Description("Thẻ từ BHYT")]
        CTK = 0,
        /// <summary>
        /// Thẻ ảo/院trongThẻ（tự động生成lúcThẻ ảo/Nhập taylúc院trongThẻ）
        /// </summary>
        [Description("Thẻ ảo")]
        XNK = 1,
        /// <summary>
        /// Thẻ BHXH
        /// </summary>
        [Description("Thẻ BHXH")]
        YBJYK = 2,
        /// <summary>
        /// Chứng từ điện tử BHYT
        /// </summary>
        [Description("Chứng từ điện tử BHYT")]
        ybdzpz = 3,
        /// <summary>
        /// CMND/CCCDBHYT诊
        /// </summary>
        [Description("CMND/CCCD(BHYT)")]
        sfz = 4,
        /// <summary>
        /// Mã sức khỏe
        /// </summary>
        [Description("Mã sức khỏe")]
        XNHJYK = 5,
        /// <summary>
        /// Quét khuôn mặt
        /// </summary>
        [Description("Quét khuôn mặt")]
        YBfACE = 6,
    }

    /// <summary>
    /// Ngoại trúLoại mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// thông thườngNgoại trú
        /// </summary>
        [Description("thông thườngNgoại trú")]
        generalOutpat = 1,
        /// <summary>
        /// cấp cứu
        /// </summary>
        [Description("cấp cứu")]
        emerDiagnosis = 2,
        /// <summary>
        /// 专家Ngoại trú
        /// </summary>
        [Description("专家Ngoại trú")]
        expertOutpat = 3,
        /// <summary>
        /// 特病Ngoại trú
        /// </summary>
        [Description("特病Ngoại trú")]
        SpecialOutpat = 4,
        /// <summary>
        /// lớnBệnhNgoại trú
        /// </summary>
        [Description("lớnBệnhNgoại trú")]
        MajorDiseases = 5,
        /// <summary>
        /// 慢性病Ngoại trú
        /// </summary>
        [Description("慢性病Ngoại trú")]
        ChronicDisease = 6,
        /// <summary>
        /// dân cư两病
        /// </summary>
        [Description("dân cư两病")]
        jmlb = 7,
        /// <summary>
        /// 意ngoài伤害Ngoại trú
        /// </summary>
        [Description("意ngoài伤害Ngoại trú")]
        ywshmz = 8,
        /// <summary>
        /// 生育Ngoại trú
        /// </summary>
        [Description("生育Ngoại trú")]
        symz = 9,
        /// <summary>
        /// 耐多药结核Ngoại trú
        /// </summary>
        [Description("耐多药结核Ngoại trú")]
        ndyjhmz = 10,
        /// <summary>
        /// 儿童两病Ngoại trú
        /// </summary>
        [Description("儿童两病Ngoại trú")]
        etlbmz = 11,
        /// <summary>
        /// 简易Ngoại trú
        /// </summary>
        [Description("简易Ngoại trú")]
        jymz = 12
    }

    /// <summary>
    /// Ngoại trúnhập việncờ  0 通用 1 chỉNgoại trú 2 chỉnhập viện 3 hệ thống（Ngoại trúnhập việnđềukhông thể用）例như：家床
    /// </summary>
    public enum Enummzzybz
    {
        /// <summary>
        /// 通用
        /// </summary>
        [Description("通用")]
        ty = 0,
        /// <summary>
        /// chỉNgoại trú
        /// </summary>
        [Description("chỉNgoại trú")]
        mz = 1,
        /// <summary>
        /// chỉnhập viện
        /// </summary>
        [Description("chỉnhập viện")]
        zy = 2,
        /// <summary>
        /// hệ thống
        /// </summary>
        [Description("hệ thống")]
        sys = 3
    }

    /// <summary>
    /// 
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

    /// <summary>
    /// Trạng tháiquyết toán
    /// </summary>
    public enum EnumJieSuanZT
    {
        /// <summary>
        /// Đãquyết toán（chưahoàn）
        /// </summary>
        [Description("Đãquyết toán")]
        YJ = 1,
        /// <summary>
        /// Đã hoàn
        /// </summary>
        [Description("Đã hoàn")]
        YT,
    }

    /// <summary>
    /// y lệnhtính chất
    /// </summary>
    public enum EnumYiZhuXZ
    {
        /// <summary>
        /// Thuốc
        /// </summary>
        YP = 1,
        /// <summary>
        /// Mục
        /// </summary>
        XM,
    }

    /// <summary>
    /// 财务hóa đơn停用cờ
    /// </summary>
    public enum EnumCWFPTY
    {
        /// <summary>
        /// chưa停用
        /// </summary>
        WTY = 0,

        /// <summary>
        /// đã停用
        /// </summary>
        YTY = 1,
    }
    /// <summary>
    /// đăng ký khámtính chất 
    /// 0-thông thườngđăng ký khám
    /// 1-trốngđăng ký khám
    /// 2-家床đăng ký khám
    /// 3-Tự phíChuyểnBHYTđăng ký khám
    /// 4-ghi sổ ngoại trúđăng ký khám
    /// </summary>
    public enum EnumGhxz
    {
        PTGH = 0,
        KGH = 1,
        JCGH = 2,
        ZFZYBGH = 3,
        MZJZGH = 4
    }
    /// <summary>
    /// Loạiquyết toán
    /// </summary>
    public enum EnumJslx
    {
        /// <summary>
        /// đăng ký khám
        /// </summary>
        [Description("đăng ký khámthu phí")]
        GH = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("家床ghi sổ")]
        JCJZ = 1,
        /// <summary>
        /// ghi sổ ngoại trú
        /// </summary>
        [Description("ghi sổ ngoại trú")]
        MZJZ = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("nhập viện")]
        ZY = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("ZHSZ")]
        ZHSZ = 4
    }

    /// <summary>
    ///y lệnhtính chất 1 Tạm thờiy lệnh   2 Dài hạny lệnh   3 Xuất việny lệnh
    /// </summary>
    public enum EnumYZXZ
    {
        /// <summary>
        /// Tạm thờiy lệnh
        /// </summary>
        LSYZ = 1,
        /// <summary>
        /// Dài hạny lệnh
        /// </summary>
        CQYZ = 2,
        /// <summary>
        /// Xuất việny lệnh
        /// </summary>
        CYYZ = 3
    }


    /// <summary>
    ///y lệnhTrạng thái 1 chưaHủy bỏ   2 Đã hủy
    /// </summary>
    public enum EnumYZZT
    {
        /// <summary>
        /// chưaHủy bỏ
        /// </summary>
        WCX = 1,
        /// <summary>
        /// Đã hủy
        /// </summary>
        YCX = 2
    }

    /// <summary>
    /// ghi sổ nhập việnMụcthu phíLoại
    /// </summary>
    public enum EnumxmType
    {
        XM = 2,//Thuốc
        YP = 1 //Mục
    }

    /// <summary>
    /// 语言
    /// </summary>
    public enum Enumlan
    {
        Default = 0,
        Fanti = 1,
        English = 2,
        Jpanese = 3,
    }
    /// <summary>
    /// In方式 1:补打  2:重打 3:In hóa đơn
    /// </summary>
    public enum Enumdyfs
    {
        /// <summary>
        /// 补打
        /// </summary>
        BD = 1,
        /// <summary>
        /// 重打
        /// </summary>
        CD = 2,
        /// <summary>
        /// In hóa đơn
        /// </summary>
        Print = 3
    }

    /// <summary>
    /// đăng ký khámLoại 特cần 特cầnV
    /// </summary>
    [Obsolete("不同医疗机构củaMã不giống nhau")]
    public enum Enumghlx
    {
        TeXu = 7884,
        TeXuV = 7889,
    }

    /// <summary>
    /// 生效cờ
    /// </summary>
    public enum EnumSXBZ
    {
        /// <summary>
        /// tức làlúc生效
        /// </summary>
        [Description("tức làlúc生效")]
        Jssx = 0,
        /// <summary>
        /// chỉ định生效
        /// </summary>
        [Description("chỉ định生效")]
        Ddsx = 1

    }

    /// <summary>
    /// 地域cờ
    /// </summary>
    public enum EnumDY
    {
        /// <summary>
        /// này地
        /// </summary>
        [Description("này地")]
        bd = 0,
        /// <summary>
        /// ngoài地
        /// </summary>
        [Description("ngoài地")]
        wd = 1

    }

    /// <summary>
    /// hôn nhânkhông
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// chưahôn nhân
        /// </summary>
        [Description("chưahôn nhân")]
        wh = 0,
        /// <summary>
        /// đãhôn nhân
        /// </summary>
        [Description("đãhôn nhân")]
        yh,
        /// <summary>
        /// Không rõ
        /// </summary>
        [Description("Không rõ")]
        UnKnown = 9,
    }

    /// <summary>
    /// Nhập viện途径
    /// </summary>
    public enum EnumRYTJ
    {
        /// <summary>
        /// Ngoại trú
        /// </summary>
        [Description("thông thườngnhập viện")]
        mz = 21,
        /// <summary>
        /// cấp cứu
        /// </summary>
        [Description("cấp cứuChuyểnnhập viện")]
        jz = 24,
        /// <summary>
        /// Khác医疗机构Chuyển入
        /// </summary>
        [Description("Chuyển入nhập viện")]
        qtyljg = 9925,
        /// <summary>
        /// Chuyểnngoài诊治nhập viện
        /// </summary>
        [Description("Chuyểnngoài诊治nhập viện")]
        zwzzzy = 23,
        /// <summary>
        /// lớnBệnhnhập viện
        /// </summary>
        [Description("lớnBệnhnhập viện")]
        dbry = 9902,
        /// <summary>
        /// 生育nhập viện
        /// </summary>
        [Description("生育nhập viện")]
        syzy = 52,
        /// <summary>
        /// 儿童两病nhập viện
        /// </summary>
        [Description("儿童两病nhập viện")]
        etlb = 9904,
        /// <summary>
        /// 耐多药结核nhập viện
        /// </summary>
        [Description("耐多药结核nhập viện")]
        ndyzy = 9907,
        /// <summary>
        /// Trẻ sơ sinh随母nhập viện
        /// </summary>
        [Description("Trẻ sơ sinh随母nhập viện")]
        xsesmzy = 9921,
        /// <summary>
        /// 120
        /// </summary>
        [Description("120")]
        jj120,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        Else = 9,
    }

    /// <summary>
    /// 证件Loại
    /// </summary>
    public enum EnumZJLX
    {
        /// <summary>
        /// CMND/CCCD
        /// </summary>
        [Description("CMND/CCCD")]
        sfz = 1,
        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        hz,
        /// <summary>
        /// 军官证
        /// </summary>
        [Description("军官证")]
        jgz,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9

    }

    /// <summary>
    /// BHYT特殊Chờ遇
    /// </summary>
    public enum EnumYBTSDY
    {
        /// <summary>
        /// thông thường
        /// </summary>
        [Description("thông thường")]
        pt = 1,
        /// <summary>
        /// Nghỉ hưunghỉ hưu
        /// </summary>
        [Description("Nghỉ hưunghỉ hưu")]
        lx,
        /// <summary>
        /// Thương tật
        /// </summary>
        [Description("Thương tật")]
        sc

    }

    /// <summary>
    /// 适用BHYTphương pháp 0：城保 1：个体BHYT 2：小城镇 3：小lúc工
    // A:民政医疗帮困
    // B：老Năm遗嘱 C:năm保老人 D：重残nhân viên
    // E:trong小学生và婴幼儿 F： Khácdân cư
    //G：医疗互助帮困Đối tượng
    /// </summary>
    public enum EnumSYYBBF
    {
        /// <summary>
        /// 城保
        /// </summary>
        [Description("城保")]
        cb = 1,
        /// <summary>
        /// 个体BHYT
        /// </summary>
        [Description("个体BHYT")]
        gtyb,
        /// <summary>
        /// 小城镇
        /// </summary>
        [Description("小城镇")]
        xcz,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9

    }

    /// <summary>
    /// đăng ký khám排班Thời gian
    /// </summary>
    public enum EnumRegSchedule
    {
        /// <summary>
        /// không có
        /// </summary>
        [Description("không có")]
        none = 0,
        /// <summary>
        /// toàn bộNgày
        /// </summary>
        [Description("toàn bộNgày")]
        allday = 1,
        /// <summary>
        /// trên午
        /// </summary>
        [Description("trên午")]
        AM = 2,
        /// <summary>
        /// dưới午
        /// </summary>
        [Description("dưới午")]
        PM = 3,

    }

    /// <summary>
    /// Thực hiện kế hoạchghi sổTrạng thái
    /// </summary>
    public enum EnumJzjhZXZT
    {
        /// <summary>
        /// Chưa thực hiện
        /// </summary>
        [Description("Chưa thực hiện")]
        None = 0,
        /// <summary>
        /// Đang thực hiện
        /// </summary>
        [Description("Đang thực hiện")]
        Part = 1,
        /// <summary>
        /// Tất cả
        /// </summary>
        [Description("Đã hoàn thành")]
        Finished = 2,
        /// <summary>
        /// Đã dừng
        /// </summary>
        [Description("Đã dừng")]
        Stopped = 3,
    }

    /// <summary>
    /// nhắc nhởLoại
    /// </summary>
    public enum EnmuReminderType
    {
        /// <summary>
        /// Số tiềntrên限nhắc nhở
        /// </summary>
        [Description("Nhắc nhở vượt hạn mức số tiền")]
        TienTrenHanNhacNho = 1,    //比như：Trị liệu sưmộtThángtrongthu phí不得超过mườivạn
    }

    /// <summary>
    /// Đồng bộ bản ghi điều trị Xử lýTrạng thái
    /// </summary>
    public enum EnumTbzljlClztType
    {
        /// <summary>
        /// Chưa xác nhận
        /// </summary>
        [Description("Chưa xác nhận")]
        WQR = 1,
        /// <summary>
        /// Đã xác nhận
        /// </summary>
        [Description("Đã xác nhận")]
        YQR,
        /// <summary>
        /// hủy bỏ
        /// </summary>
        [Description("hủy bỏ")]
        ZF,
    }

    /// <summary>
    /// 登录cờ
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
        [Description("hệ thống登录")]
        App = 2
    }

    /// <summary>
    /// Mụcthu phí计价策略
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// theoThời lượng
        /// </summary>
        [Description("theoThời lượng")]
        Time = 1,
        /// <summary>
        /// theoSố lượng
        /// </summary>
        [Description("theoSố lượng")]
        Amount,
        /// <summary>
        /// theo面积
        /// </summary>
        [Description("theo面积")]
        Acreage,
    }

    /// <summary>
    /// Ngoại trúBệnh nhân诊Trạng thái（诊cờ）
    /// </summary>
    public enum EnumOutpatientJzbz
    {
        /// <summary>
        /// Chờ诊
        /// </summary>
        [Description("Chờ诊")]
        Djz = 1,
        /// <summary>
        /// 诊trong
        /// </summary>
        [Description("诊trong")]
        Jzz,
        /// <summary>
        /// Kết thúc诊
        /// </summary>
        [Description("Kết thúc诊")]
        Jsjz,
    }

    /// <summary>
    /// 枚举Loại
    /// </summary>
    public enum EnumPrescriptionType
    {
        /// <summary>
        /// Thuốc
        /// </summary>
        [Description("Thuốc")]
        Medicine = 1,
        /// <summary>
        /// Mục điều trị
        /// </summary>
        [Description("Mục điều trị")]
        Treament,
    }

    /// <summary>
    /// 收支tính chất
    /// </summary>
    public enum EnumSZXZ
    {
        [Description("nạp tiền")]
        cz = 1,
        [Description("Rút tiền")]
        qk = 2,
        [Description("Quyết toán ngoại trú")]
        mzjs = 3,
        [Description("Hoàn trả quyết toán ngoại trú")]
        mzjsth = 4,
        [Description("quyết toán nhập viện")]
        zyjs = 5,
        [Description("Hoàn trả quyết toán nhập viện")]
        zyjsth = 6,
        [Description("Hoàn số dư")]
        tye = 9
    }

    /// <summary>
    /// 危重级别
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// một般
        /// </summary>
        [Description("một般")]
        Normal = 1,
        /// <summary>
        /// 病重
        /// </summary>
        [Description("病重")]
        Serious = 2,
        /// <summary>
        /// 病危
        /// </summary>
        [Description("病危")]
        Bedying = 3
    }

    public enum EnumOrgshzt
    {
        /// <summary>
        /// Phê duyệtThông qua
        /// </summary>
        [Description("Thông qua")]
        TG = 1,
        /// <summary>
        /// Phê duyệtChưa đạt
        /// </summary>
        [Description("Chưa đạt")]
        WTG = 2,
        /// <summary>
        /// Chờ审
        /// </summary>
        [Description("Chờ审")]
        DS = 3,
        /// <summary>
        /// chưaGửi
        /// </summary>
        [Description("chưaGửi")]
        WTJ = 4,
        /// <summary>
        /// 预Gửi
        /// </summary>
        [Description("预Gửi")]
        YTJ = 5
    }

    public enum EnumPayStatus
    {
        [Description("Thanh toán thành công")]
        Success = 1,
        [Description("Thanh toán thất bại")]
        Failed = 2
    }

    public enum EnumRefundStatus
    {
        [Description("Hoàn tiền thành công")]
        Success = 1,
        [Description("Hoàn tiền thất bại")]
        Failed = 2,
        [Description("Chưa rõ")]
        UnKnown = 3
    }

    public enum EnumTradeType
    {

        /// <summary>
        /// thanh toán宝付款码
        /// </summary>
        [Description("thanh toán宝")]
        Alipay_Bar_Code = 1,
        /// <summary>
        /// 微信付款码
        /// </summary>
        [Description("微信")]
        Wechat_MICROPAY = 2,
    }

    /// <summary>
    /// 枚举-TuổiĐơn vị
    /// </summary>
    public enum EnumAgeUnit
    {
        Year = 1,
        Month = 2,
        Day = 3,
        Hour = 4,
    }
    /// <summary>
    /// Ngoại trúđặt hẹnTrạng thái
    /// </summary>
    public enum EnumMzyyzt
    {
        [Description("đã约")]
        book = 1,
        [Description("đãđăng ký khám")]
        reg = 2,
        [Description("đặt hẹnđãHủy")]
        cancel = 3,
        [Description("khi日đăng ký khám")]
        bookreg = 4,
        [Description("Hủyđăng ký khám")]
        regcancel = 5
    }
    /// <summary>
    /// Ngoại trú排班Loại
    /// </summary>
    public enum EnumMzpblx
    {
        [Description("Tất cả")]
        all = 1,
        [Description("开放đặt hẹn")]
        yy = 2,
        [Description("khôngđặt hẹn")]
        fyy = 3
    }
    /// <summary>
    /// 开放đặt hẹn
    /// </summary>
    public enum EnumMzpbBook
    {
        [Description("là")]
        Y = 1,
        [Description("không")]
        F = 0,
    }
    /// <summary>
    /// 处方Trạng thái
    /// </summary>
    public enum EnumZfzt
    {

        [Description("Bình thường处方")]
        zc = 0,
        [Description("hoàn药HoặcKháchủy bỏ处方")]
        ty = 1
    }

    /// <summary>
    /// cấp cứuTrạng thái
    /// </summary>
    public enum EnumJzzt
    {
        [Description("không")]
        zc = 0,
        [Description("là")]
        ty = 1
    }
    /// <summary>
    /// Ngoại trúđăng ký khám来源
    /// </summary>
    public enum EnumMzghly
    {
        [Description("His")]
        His = 0,
        [Description("đặt hẹn")]
        Yy = 1,
        [Description("银医通")]
        Yyt = 3,
        [Description("微信")]
        WeChat = 4,
        [Description("thanh toán宝")]
        Alipay = 5,
        [Description("体检")]
        Tj = 6,
    }
    /// <summary>
    /// BHYTthuộc tính
    /// </summary>
    public enum EnumCblb
    {
        [Description("Tất cả")]
        all = 0,
        [Description("người lao động")]
        zg = 1,
        [Description("dân cư")]
        jm = 2,
        [Description("Nghỉ hưunghỉ hưu")]
        lx = 3,
        [Description("异地người lao động")]
        ydzg = 4,
        [Description("异地dân cư")]
        ydjm = 5,

    }

    public enum EnumRportType
    {
        [Description("người lao độngphí申报表(民政救助)")]
        zgmzjz = 1,
        [Description("dân cưphí申Vui lòng表(民政救助)")]
        jmmzjz = 2,
        [Description("báo cáo thanh toán thông thường(người lao động)")]
        zgbz = 3,
        [Description("báo cáo thanh toán thông thường(dân cư)")]
        jmbz = 4,
        [Description("báo cáo thanh toán thông thường(Nghỉ hưunghỉ hưu)")]
        lxbz = 5,
        [Description("báo cáo thanh toán thông thường(异地người lao động)")]
        ydzgbz = 6,
        [Description("báo cáo thanh toán thông thường(异地dân cư)")]
        ydjmbz = 7,

    }
    /// <summary>
    /// nhân viênLoại
    /// </summary>
    public enum EnumRylb
    {
        [Description("người lao độngtại职")]
        zgzz = 1101,
        [Description("公务员tại职")]
        gwyzz = 1102,
        [Description("灵活业nhân viêntại职")]
        lhjyryzz = 1103,
        [Description("địa phươngKhác扩展nhân viên")]
        qtdfkzry = 1160,
        [Description("người lao độnghoàn休")]
        zgtx = 1201,
        [Description("公务员hoàn休")]
        gwytx = 1202,
        [Description("灵活业nhân viênhoàn休")]
        lhjyrytx = 1203,
        [Description("địa phươngKhác扩展nhân viên")]
        qtdfkzrytx = 1260,
        [Description("Nghỉ hưunghỉ hưunhân viên")]
        lxry = 1300,
        [Description("địa phươngKhác扩展nhân viên")]
        lxdfqtkzry = 1360,
        [Description("Trẻ sơ sinh")]
        xse = 1401,
        [Description("学龄trước儿童")]
        xlqet = 1402,
        [Description("trong小学生")]
        zxxs = 1403,
        [Description("大学生")]
        dxs = 1404,
        [Description("chưa成Năm（chưa入学）")]
        wcn = 1405,
        [Description("địa phươngKhác扩展CMND/CCCD份")]
        dfqtgkzf = 1460,
        [Description("thông thườngdân cư（成Năm）")]
        ptjm = 1501,
        [Description("địa phươngKhác扩展CMND/CCCD份")]
        jmdfqtgzsf = 1560,
        [Description("dân cư（老Năm）")]
        jmln = 16,
        [Description("dân cư（成Năm）")]
        jmcn = 15,
        [Description("dân cư（chưa成Năm）")]
        jmwcn = 14,
        [Description("Nghỉ hưunghỉ hưu")]
        lx = 13,
        [Description("hoàn休nhân viên")]
        txry = 12,
        [Description("tại职")]
        zz = 11

    }
    public enum EnumYbQuery
    {
        [Description("【2001】nhân viênChờ遇享受")]
        rydyxs = 1,
        [Description("【4207】Bệnh nhân tự phí医Chi tiết phíTruy vấn")]
        zfbrjyfymx = 4207,
        [Description("【4208】Bệnh nhân tự phí医诊thông tinTruy vấn")]
        zfbrjyjzxx = 4208,
        [Description("【4209】Bệnh nhân tự phí医Chẩn đoánthông tinTruy vấn")]
        zfbrjyzdxx = 4209,
        [Description("【5201】诊thông tinTruy vấn")]
        jzxxcx = 2,
        [Description("【5202】Chẩn đoánthông tinTruy vấn")]
        zdxxcx = 3,
        [Description("【5203】Thông tinquyết toánTruy vấn")]
        jsxxcx = 4,
        [Description("【5204】Chi tiết phíTruy vấn")]
        fymxcx = 5,
        [Description("【5205】nhân viên慢特病用药Bản ghiTruy vấn")]
        rymtbyyjl = 6,
        [Description("【5206】nhân viên累计thông tinTruy vấn")]
        ryljxxcx = 7,
        [Description("【5301】nhân viên特殊病备案Truy vấn")]
        rytsbbacx = 8,
        [Description("【5302】nhân viênchỉ địnhthông tinTruy vấn")]
        ryddxxcx = 9,
        [Description("【5303】Đang ở việnthông tinTruy vấn")]
        zyxxcx = 10,
        [Description("【5304】Chuyển việnthông tinTruy vấn")]
        zyxx = 11,
        [Description("【5401】Mục互认thông tinTruy vấn")]
        xmhrxx = 5401,
        [Description("【5402】Báo cáoChi tiếtthông tinTruy vấn")]
        bgmxxx = 5402,

    }

    public enum EnumYbJGQuery
    {
        [Description("【1201】医药机构thông tinlấy")]
        jgxx = 1201,
        [Description("【3508】chỉ định医药机构商品Tồn khothông tinTruy vấn")]
        yljgspkcxx = 3508,
        [Description("【3509】chỉ định医药机构商品Tồn kho变更Bản ghiTruy vấn")]
        yljgspkcbgjl = 3509,
        [Description("【3510】chỉ định医药机构商品采购thông tinTruy vấn")]
        yljgspcgxx = 3510,
        [Description("【3511】chỉ định医药机构商品销售thông tinTruy vấn")]
        yljgspxsxx = 3511,
        [Description("【3512】chỉ định医药机构入库商品追溯thông tinTruy vấn")]
        yljgrkspzsxx = 3512,
        [Description("【3513】chỉ định医药机构商品销售追溯thông tinTruy vấn")]
        yljgspxszsxx = 3513,
        [Description("【3607】quyết toánbảngKết quả kiểm soát chất lượngTruy vấn")]
        jsqdzkjg = 3607,
        [Description("【5101】Khoathông tinTruy vấn")]
        ksxxcx = 5101,
    }
    /// <summary>
    /// Chờ遇Kiểm traLoại
    /// </summary>
    public enum Enumdyjclx
    {
        [Description("quỹ项Kiểm tra")]
        jjxjc = 1,
        [Description("KhácKiểm tra")]
        qtjc = 99
    }
    /// <summary>
    /// quỹthanh toánLoại
    /// </summary>
    public enum Enumjjzflx
    {
        [Description("城镇người lao động基nàyBảo hiểm y tếquỹ chungquỹ")]
        tcjj = 310100,
        [Description("城镇người lao động基nàyBảo hiểm y tế个人Tài khoảnquỹ")]
        grzhjj = 310200,
        [Description("公务员医疗补助quỹ")]
        gwybzjj = 320100,
        [Description("大额医疗phí补助quỹ")]
        debzjj = 330100,
        [Description("Nghỉ hưunghỉ hưunhân viên医疗保障quỹ")]
        lxbzjj = 340100,
        [Description("một至sáu级残疾军人医疗补助quỹ")]
        cjjrbzjj = 350100,
        [Description("企业补充Bảo hiểm y tếquỹ")]
        qybxjj = 370100,
        [Description("Bệnh viện垫付quỹ")]
        yydfjj = 999996,
        [Description("区级公务员quỹ")]
        qgwyjj = 999901,
        [Description("城乡dân cư基nàyBảo hiểm y tếquỹ")]
        cxjmbxjj = 390100,
        [Description("城乡dân cư大病Bảo hiểm y tếquỹ")]
        cxjmdbbxjj = 390200,
        [Description("生育quỹ")]
        syjj = 510100,
        [Description("người lao động生育quỹ")]
        zgsyjj = 310103,
        [Description("医疗救助quỹ")]
        yljzjj = 610100,
        [Description("医疗再救助quỹ")]
        ylzjzjj = 610102,
        [Description("健康扶贫quỹ")]
        jkpfjj = 610103,
        [Description("精准脱贫quỹ")]
        jztpjj = 610104,
        [Description("Khácquỹ")]
        qtjj = 999997
    }
    /// <summary>
    /// quỹ款项Chờ遇享受cờ
    /// </summary>
    public enum Enumdyxsbz
    {
        [Description("không")]
        s = 0,
        [Description("là")]
        f = 1
    }
    /// <summary>
    /// Chẩn đoánLoại
    /// </summary>
    public enum Enumzdlx
    {
        [Description("西医Chẩn đoán")]
        xyzd = 1,
        [Description("trong医主病Chẩn đoán")]
        zyzzd = 2,
        [Description("trong医主证Chẩn đoán")]
        zyzzzd = 3
    }
    /// <summary>
    /// Loại bảo hiểmLoại
    /// </summary>
    public enum Enumxzlx
    {
        [Description("người lao động基nàyBảo hiểm y tế")]
        zgjbylbx = 310,
        [Description("公务员医疗补助")]
        gwyylbz = 320,
        [Description("大额医疗phí补助")]
        deylfybz = 330,
        [Description("Nghỉ hưunghỉ hưunhân viên医疗保障")]
        lxryylbz = 340,
        [Description("một至sáu级残废军人医疗补助")]
        cfjrylbz = 350,
        [Description("老红军医疗保障")]
        lhjylbz = 360,
        [Description("企业补充Bảo hiểm y tế")]
        qybcylbx = 370,
        [Description("mới型农村合作医疗")]
        xxnchzyl = 380,
        [Description("城乡dân cư基nàyBảo hiểm y tế")]
        cxjmjbylbx = 390,
        [Description("城镇dân cư基nàyBảo hiểm y tế")]
        czjmjbylbx = 391,
        [Description("城乡dân cư大病Bảo hiểm y tế")]
        cxjmdbbx = 392,
        [Description("Khác特殊nhân viên医疗保障")]
        qttsryylbz = 399,
        [Description("Dài hạn照护bảo hiểm")]
        cqzhbx = 410,
        [Description("Bảo hiểm sinh đẻ")]
        sybx = 510
    }
    /// <summary>
    /// 医疗Loại
    /// </summary>
    public enum Enumyllb
    {
        [Description("thông thườngNgoại trú")]
        ptmz = 11,
        [Description("Ngoại trúđăng ký khám")]
        mzgh = 12,
        [Description("cấp cứu")]
        jz = 13,
        [Description("Ngoại trú慢特病")]
        mzmtb = 14,
        [Description("意ngoài伤害Ngoại trú")]
        ywshmz = 19,
        [Description("thông thườngnhập viện")]
        ptzy = 21,
        [Description("Chuyểnngoài诊治nhập viện")]
        zwzzzy = 23,
        [Description("cấp cứuChuyểnnhập viện")]
        jzzzy = 24,
        [Description("生育Ngoại trú")]
        symz = 51,
        [Description("生育nhập viện")]
        syzy = 52,
        [Description("计划生育Phẫu thuật费")]
        jhsyssf = 53,
        [Description("lớnBệnhNgoại trú")]
        zdjbmz = 9901,
        [Description("lớnBệnhnhập viện")]
        zdjbzy = 9902,
        [Description("儿童两病Ngoại trú")]
        etlbmz = 9903,
        [Description("儿童两病nhập viện")]
        etlbzy = 9904,
        [Description("耐多药结核Ngoại trú")]
        ndyjhmz = 9906,
        [Description("耐多药结核nhập viện")]
        ndyjhzy = 9907,
        [Description("Chuyển入nhập viện")]
        zrzy = 9925,
        [Description("Trẻ sơ sinh随母nhập viện")]
        xsesmzy = 9921
    }
    /// <summary>
    /// 申报来源
    /// </summary>
    public enum Enumsbly
    {
        [Description("chỉ định医药机构")]
        etlbmz = 01,
        [Description("trong心đã办hệ thống")]
        etlbzy = 02,
        [Description("网trênđã办hệ thống")]
        ndyjhmz = 03,
        [Description("APP")]
        ndyjhzy = 04,
        [Description("một体机")]
        zrzy = 05,
        [Description("Khác")]
        xsesmzy = 99
    }
    /// <summary>
    /// 生育Loại
    /// </summary>
    public enum EnumSylb
    {
        /// <summary>
        /// Bình thường产
        /// </summary>
        [Description("Bình thường产")]
        Zcc = 1,
        /// <summary>
        /// 助娩产
        /// </summary>
        [Description("助娩产")]
        Zmc = 2,
        /// <summary>
        /// 剖宫产
        /// </summary>
        [Description("剖宫产")]
        Pgc = 3,
        /// <summary>
        /// 难产
        /// </summary>
        [Description("难产")]
        nc = 4,
        /// <summary>
        /// sảy thai
        /// </summary>
        [Description("sảy thai")]
        lc = 5,
        /// <summary>
        /// 引产
        /// </summary>
        [Description("引产")]
        yc = 6,
        /// <summary>
        /// 产trướcKiểm tra
        /// </summary>
        [Description("产trướcKiểm tra")]
        cqjc = 7,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9,
        /// <summary>
        /// 宫ngoài孕
        /// </summary>
        [Description("宫ngoài孕")]
        gwc = 10,
        /// <summary>
        /// 遗传基因检测
        /// </summary>
        [Description("遗传基因检测")]
        ycjyjc = 11,
        /// <summary>
        /// bốn个Tháng以trên流引产
        /// </summary>
        [Description("bốn个Tháng以trênsảy thai")]
        sgyyslc = 12,
        /// <summary>
        /// bốn个Tháng以dướisảy thai
        /// </summary>
        [Description("bốn个Tháng以dướisảy thai")]
        sgyyxlc = 13
    }
    /// <summary>
    /// 计划生育Phẫu thuậtLoại
    /// </summary>
    public enum EnumSysslb
    {
        /// <summary>
        /// 放置宫trong节育器
        /// </summary>
        [Description("放置宫trong节育器")]
        fzgnjyq = 1,
        /// <summary>
        /// 取出宫trong节育器
        /// </summary>
        [Description("取出宫trong节育器")]
        qcgnjyq = 2,
        /// <summary>
        /// sảy thai术
        /// </summary>
        [Description("sảy thai术")]
        lcs = 3,
        /// <summary>
        /// 引产术
        /// </summary>
        [Description("引产术")]
        ycs = 4,
        /// <summary>
        /// 绝育Phẫu thuật
        /// </summary>
        [Description("绝育Phẫu thuật")]
        jyss = 5,
        /// <summary>
        /// 绝育复通Phẫu thuật
        /// </summary>
        [Description("绝育复通Phẫu thuật")]
        jyftss = 6,
        /// <summary>
        /// 绝育Phẫu thuật（输精管）
        /// </summary>
        [Description("绝育Phẫu thuật（输精管）")]
        jysssjg = 7,
        /// <summary>
        /// 绝育复通Phẫu thuật（输精管）
        /// </summary>
        [Description("绝育复通Phẫu thuật（输精管）")]
        jyftsssjg = 8,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9,
        /// <summary>
        /// 皮dưới埋植术
        /// </summary>
        [Description("皮dưới埋植术")]
        pxmzs = 10,
        /// <summary>
        /// Hủy皮dưới埋植术
        /// </summary>
        [Description("Hủy皮dưới埋植术")]
        qxpxmzs = 11,
        /// <summary>
        /// 输卵管结扎术
        /// </summary>
        [Description("输卵管结扎术")]
        srgjzs = 12
    }
    /// <summary>
    /// 晚育cờ/sớm产cờ
    /// </summary>
    public enum EnumWybz
    {
        /// <summary>
        /// không
        /// </summary>
        [Description("không")]
        f = 0,
        /// <summary>
        /// là
        /// </summary>
        [Description("là")]
        s = 1,
    }
    /// <summary>
    /// Bác sĩLoại
    /// </summary>
    public enum EnumYslx
    {
        [Description("nhập việnBác sĩ")] //Resident
        ZyDoc = 1,
        [Description("Bác sĩ điều trị chính")] //Attending
        ZzDoc = 2,
        [Description("主任Bác sĩ")] //chief
        ZrDoc = 3
    }
    /// <summary>
    /// 异地标识
    /// </summary>
    public enum EnumYd
    {
        /// <summary>
        /// không
        /// </summary>
        [Description("không")]
        f = 0,
        /// <summary>
        /// là
        /// </summary>
        [Description("là")]
        s = 1,
    }
    /// <summary>
    /// Tính chất bệnh nhân
    /// </summary>
    public enum EnumBrxz
    {
        /// <summary>
        /// Tự phí
        /// </summary>
        [Description("Tự phí")]
        zf = 0,
        /// <summary>
        /// người lao độngBHYT
        /// </summary>
        [Description("người lao độngBHYT")]
        zg = 11,
        /// <summary>
        /// dân cưBHYT
        /// </summary>
        [Description("dân cưBHYT")]
        jm = 12,
        /// <summary>
        /// Nghỉ hưunghỉ hưu
        /// </summary>
        [Description("Nghỉ hưunghỉ hưu")]
        lx = 13,
        /// <summary>
        /// thông thườngBHYT
        /// </summary>
        [Description("thông thườngBHYT")]
        pt = 1,
    }
    /// <summary>
    /// 订单Trạng thái0:Chờthanh toán 1:đãthanh toán  2::Đã hoàn款 3：Đang thanh toán
    /// </summary>
    public enum EnumOrderStatus
    {
        /// <summary>
        /// Chờthanh toán
        /// </summary>
        [Description("Chờthanh toán")]
        dzf = 0,
        /// <summary>
        /// đãthanh toán
        /// </summary>
        [Description("đãthanh toán")]
        yzf = 1,
        /// <summary>
        /// Đã hoàn款
        /// </summary>
        [Description("Đã hoàn款")]
        ytk = 2,
        /// <summary>
        /// Đang thanh toán
        /// </summary>
        [Description("Đang thanh toán")]
        zfz = 3,
    }
    /// <summary>
    /// 交易ThẻLoại
    /// </summary>
    public enum EnumJykxzlx
    {
        /// <summary>
        /// Tự phí
        /// </summary>
        [Description("Tự phí")]
        zf = 0,
        /// <summary>
        /// BHYT
        /// </summary>
        [Description("BHYT")]
        yb = 1
    }
}
