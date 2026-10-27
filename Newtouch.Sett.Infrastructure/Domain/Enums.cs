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
        [Description("quyết to án xuất viện")]
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
        [Description("Chờ quyết to án")]
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
        [Description("không giao dịch")]
        ybjylx0 = 0,
        /// <summary>
        /// thông thường交易
        /// </summary>
        [Description("thông thường giao dịch")]
        ybjylx1 = 1,
        /// <summary>
        /// 大病交易
        /// </summary>
        [Description("bệnh nặng giao dịch")]
        ybjylx2 = 2,
        /// <summary>
        /// 家床交易
        /// </summary>
        [Description("giường bệnh tại nhà giao dịch")]
        ybjylx3 = 3,
        /// <summary>
        /// 工伤交易
        /// </summary>
        [Description("tai nạn lao động giao dịch")]
        ybjylx4 = 4,
        /// <summary>
        /// nhập viện交易
        /// </summary>
        [Description("nhập viện giao dịch")]
        ybjylx5 = 5,
        /// <summary>
        /// bảo hiểm nông nghiệp交易
        /// </summary>
        [Description("bảo hiểm nông nghiệp giao dịch")]
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
        [Description("Ngoại trúTài khoản gửi/rút")]
        MZZH = 0,
        [Description("giường bệnh tại nhà ghi sổ")]
        JCZH = 1,
        [Description("Quyết to án ngoại trú")]
        MZJS = 2,
        [Description("Khoa bệnh ghi sổ")]
        BQJZ = 3,
        [Description("quyết to án nhập viện")]
        ZYJS = 4,
        [Description("nhập việnTài khoản gửi/rút")]
        ZYZH = 5,
        [Description("khoản nộp bổ sung nhập viện")]
        ZYBJK = 6,
        [Description("ghi sổ ngoại trú đăng ký")]
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
        [Description("chuyên giaNgoại trú")]
        expertOutpat = 3,
        /// <summary>
        /// 特病Ngoại trú
        /// </summary>
        [Description("đặc bệnhNgoại trú")]
        SpecialOutpat = 4,
        /// <summary>
        /// lớnBệnhNgoại trú
        /// </summary>
        [Description("lớnBệnhNgoại trú")]
        MajorDiseases = 5,
        /// <summary>
        /// 慢性病Ngoại trú
        /// </summary>
        [Description("bệnh mãn tínhNgoại trú")]
        ChronicDisease = 6,
        /// <summary>
        /// dân cư两病
        /// </summary>
        [Description("dân cư hai bệnh")]
        jmlb = 7,
        /// <summary>
        /// 意ngoài伤害Ngoại trú
        /// </summary>
        [Description("Ngoại trú thương tích ngoài ý muốn")]
        ywshmz = 8,
        /// <summary>
        /// 生育Ngoại trú
        /// </summary>
        [Description("sinh đẻNgoại trú")]
        symz = 9,
        /// <summary>
        /// 耐多药结核Ngoại trú
        /// </summary>
        [Description("lao đa kh áng thuốcNgoại trú")]
        ndyjhmz = 10,
        /// <summary>
        /// 儿童两病Ngoại trú
        /// </summary>
        [Description("hai bệnh trẻ emNgoại trú")]
        etlbmz = 11,
        /// <summary>
        /// 简易Ngoại trú
        /// </summary>
        [Description("đơn giảnNgoại trú")]
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
        [Description("chung")]
        ty = 0,
        /// <summary>
        /// chỉNgoại trú
        /// </summary>
        [Description("chỉNgoại trú")]
        mz = 1,
        /// <summary>
        /// chỉnhập viện
        /// </summary>
        [Description("chỉ nhập viện")]
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
        [Description("Đã quyết to án")]
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
        [Description("đăng ký khám thu phí")]
        GH = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("giường bệnh tại nhà ghi sổ")]
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
    [Obsolete("Mã của các cơ sở y tế khác nhau không giống nhau")]
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
        [Description("tức làlúc có hiệu lực")]
        Jssx = 0,
        /// <summary>
        /// chỉ định生效
        /// </summary>
        [Description("chỉ định có hiệu lực")]
        Ddsx = 1

    }

    /// <summary>
    /// 地域cờ
    /// </summary>
    public enum EnumDY
    {
        /// <summary>
        /// địa phương
        /// </summary>
        [Description("địa phương")]
        bd = 0,
        /// <summary>
        /// nơi khác
        /// </summary>
        [Description("nơi khác")]
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
        [Description("chưa hôn nhân")]
        wh = 0,
        /// <summary>
        /// đãhôn nhân
        /// </summary>
        [Description("đã hôn nhân")]
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
        [Description("thông thường nhập viện")]
        mz = 21,
        /// <summary>
        /// cấp cứu
        /// </summary>
        [Description("cấp cứuChuyển nhập viện")]
        jz = 24,
        /// <summary>
        /// Khác医疗机构Chuyển入
        /// </summary>
        [Description("Chuyển vào nhập viện")]
        qtyljg = 9925,
        /// <summary>
        /// Chuyểnngoài诊治nhập viện
        /// </summary>
        [Description("Chuyển ngoài chẩn trị nhập viện")]
        zwzzzy = 23,
        /// <summary>
        /// lớnBệnhnhập viện
        /// </summary>
        [Description("lớnBệnh nhập viện")]
        dbry = 9902,
        /// <summary>
        /// 生育nhập viện
        /// </summary>
        [Description("sinh đẻ nhập viện")]
        syzy = 52,
        /// <summary>
        /// 儿童两病nhập viện
        /// </summary>
        [Description("hai bệnh trẻ em nhập viện")]
        etlb = 9904,
        /// <summary>
        /// 耐多药结核nhập viện
        /// </summary>
        [Description("lao đa kh áng thuốc nhập viện")]
        ndyzy = 9907,
        /// <summary>
        /// Trẻ sơ sinh nhập viện theo mẹ
        /// </summary>
        [Description("Trẻ sơ sinh nhập viện theo mẹ")]
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
        [Description("hộ chiếu")]
        hz,
        /// <summary>
        /// 军官证
        /// </summary>
        [Description("chứng nhận sĩ quan")]
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
        [Description("Nghỉ hưu nghỉ hưu")]
        lx,
        /// <summary>
        /// Thương tật
        /// </summary>
        [Description("Thương tật")]
        sc

    }

    /// <summary>
    /// 适用BHYTphương pháp 0：城保 1：个BHYT cá thể 2：小城镇 3：小lúc工
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
        [Description("bảo hiểm thành phố")]
        cb = 1,
        /// <summary>
        /// 个BHYT cá thể
        /// </summary>
        [Description("BHYT cá thể")]
        gtyb,
        /// <summary>
        /// 小城镇
        /// </summary>
        [Description("thị trấn nhỏ")]
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
        /// buổi sáng
        /// </summary>
        [Description("buổi sáng")]
        AM = 2,
        /// <summary>
        /// buổi chiều
        /// </summary>
        [Description("buổi chiều")]
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
        [Description("đăng nhập một lần")]
        SSO = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("hệ thống đăng nhập")]
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
        /// theo diện tích
        /// </summary>
        [Description("theo diện tích")]
        Acreage,
    }

    /// <summary>
    /// Ngoại trúBệnh nhân诊Trạng thái（诊cờ）
    /// </summary>
    public enum EnumOutpatientJzbz
    {
        [Description("Chờ khám")] Djz = 1,
        [Description("Đang khám")] Jzz = 2,
        [Description("Kết thúc khám")] Jsjz = 3
    }

    /// <summary>
    /// 枚举Loại
    /// </summary>
    public enum EnumPrescriptionType { [Description("Thuốc")] Medicine = 1, [Description("Khoản mục trị liệu")] Treament = 2 }

    /// <summary>
    /// 收支tính chất
    /// </summary>
    public enum EnumSZXZ
    {
        [Description("nạp tiền")]
        cz = 1,
        [Description("Rút tiền")]
        qk = 2,
        [Description("Quyết to án ngoại trú")]
        mzjs = 3,
        [Description("Hoàn trả quyết to án ngoại trú")]
        mzjsth = 4,
        [Description("quyết to án nhập viện")]
        zyjs = 5,
        [Description("Hoàn trả quyết to án nhập viện")]
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
        /// thông thường
        /// </summary>
        [Description("thông thường")]
        Normal = 1,
        /// <summary>
        /// 病重
        /// </summary>
        [Description("bệnh nặng")]
        Serious = 2,
        /// <summary>
        /// 病危
        /// </summary>
        [Description("nguy kịch")]
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
        [Description("Chờ xét duyệt")]
        DS = 3,
        /// <summary>
        /// chưaGửi
        /// </summary>
        [Description("Chưa gửi")]
        WTJ = 4,
        /// <summary>
        /// Gửi trước
        /// </summary>
        [Description("Gửi trước")]
        YTJ = 5
    }

    public enum EnumPayStatus
    {
        [Description("Thanh to án thành công")]
        Success = 1,
        [Description("Thanh to án thất bại")]
        Failed = 2
    }

    public enum EnumRefundStatus { [Description("Hoàn tiền thành công")] Success = 1, [Description("Hoàn tiền thất bại")] Failed = 2, [Description("Không rõ")] UnKnown = 3 }

    public enum EnumTradeType
    {

        /// <summary>
        /// thanh toán宝付款码
        /// </summary>
        [Description("Alipay")]
        Alipay_Bar_Code = 1,
        /// <summary>
        /// 微信付款码
        /// </summary>
        [Description("WeChat")]
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
        [Description("đã hẹn")]
        book = 1,
        [Description("đã đăng ký khám")]
        reg = 2,
        [Description("đặt hẹn đãHủy")]
        cancel = 3,
        [Description("đăng ký khám trong ngày")]
        bookreg = 4,
        [Description("Hủy đăng ký khám")]
        regcancel = 5
    }
    /// <summary>
    /// Ngoại trú排班Loại
    /// </summary>
    public enum EnumMzpblx
    {
        [Description("Tất cả")]
        all = 1,
        [Description("mở đặt hẹn")]
        yy = 2,
        [Description("không đặt hẹn")]
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

        [Description("Bình thường đơn thuốc")]
        zc = 0,
        [Description("hoàn thuốcHoặcKhá chủy bỏ đơn thuốc")]
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
        [Description("Ngân hàng-Y tế thông")]
        Yyt = 3,
        [Description("WeChat")]
        WeChat = 4,
        [Description("Alipay")]
        Alipay = 5,
        [Description("khám sức khỏe")]
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
        [Description("Nghỉ hưu nghỉ hưu")]
        lx = 3,
        [Description("khác địa phương người lao động")]
        ydzg = 4,
        [Description("khác địa phương dân cư")]
        ydjm = 5,

    }

    public enum EnumRportType
    {
        [Description("người lao động phí xin báo bảng(cứu trợ dân chính)")]
        zgmzjz = 1,
        [Description("dân cư phí xinVui lòng bảng(cứu trợ dân chính)")]
        jmmzjz = 2,
        [Description("báo cáo thanh to án thông thường(người lao động)")]
        zgbz = 3,
        [Description("báo cáo thanh to án thông thường(dân cư)")]
        jmbz = 4,
        [Description("báo cáo thanh to án thông thường(Nghỉ hưu nghỉ hưu)")]
        lxbz = 5,
        [Description("báo cáo thanh to án thông thường(khác địa phương người lao động)")]
        ydzgbz = 6,
        [Description("báo cáo thanh to án thông thường(khác địa phương dân cư)")]
        ydjmbz = 7,

    }
    /// <summary>
    /// nhân viênLoại
    /// </summary>
    public enum EnumRylb
    {
        [Description("người lao độngtại chức")]
        zgzz = 1101,
        [Description("công chứctại chức")]
        gwyzz = 1102,
        [Description("nghề tự do nhân viêntại chức")]
        lhjyryzz = 1103,
        [Description("địa phươngKhác mở rộng nhân viên")]
        qtdfkzry = 1160,
        [Description("người lao động nghỉ hưu")]
        zgtx = 1201,
        [Description("công chức nghỉ hưu")]
        gwytx = 1202,
        [Description("nhân viên nghề tự do nghỉ hưu")]
        lhjyrytx = 1203,
        [Description("địa phươngKhác mở rộng nhân viên")]
        qtdfkzrytx = 1260,
        [Description("Nghỉ hưu nghỉ hưu nhân viên")]
        lxry = 1300,
        [Description("địa phươngKhác mở rộng nhân viên")]
        lxdfqtkzry = 1360,
        [Description("Trẻ sơ sinh")]
        xse = 1401,
        [Description("học tuổi trước trẻ em")]
        xlqet = 1402,
        [Description("trong nhỏ học sinh")]
        zxxs = 1403,
        [Description("sinh viên")]
        dxs = 1404,
        [Description("chưa thành niên (chưa nhập học)")]
        wcn = 1405,
        [Description("địa phươngKhác mở rộngCMND/CCCDphần")]
        dfqtgkzf = 1460,
        [Description("dân cư thông thường (thành niên)")]
        ptjm = 1501,
        [Description("địa phươngKhác mở rộngCMND/CCCDphần")]
        jmdfqtgzsf = 1560,
        [Description("dân cư (người cao tuổi)")]
        jmln = 16,
        [Description("dân cư (thành niên)")]
        jmcn = 15,
        [Description("dân cư (chưa thành niên)")]
        jmwcn = 14,
        [Description("Nghỉ hưu nghỉ hưu")]
        lx = 13,
        [Description("nhân viên nghỉ hưu")]
        txry = 12,
        [Description("tại chức")]
        zz = 11

    }
    public enum EnumYbQuery
    {
        [Description("【2001】Nhân viên chờ hưởng quyền lợi")]
        rydyxs = 1,
        [Description("【4207】Bệnh nhân tự phíyChi tiết phíTruy vấn")]
        zfbrjyfymx = 4207,
        [Description("【4208】Bệnh nhân tự phíy khám thông tinTruy vấn")]
        zfbrjyjzxx = 4208,
        [Description("【4209】Bệnh nhân tự phíyChẩn đo án thông tinTruy vấn")]
        zfbrjyzdxx = 4209,
        [Description("【5201】khám thông tinTruy vấn")]
        jzxxcx = 2,
        [Description("【5202】Chẩn đo án thông tinTruy vấn")]
        zdxxcx = 3,
        [Description("【5203】Thông tin quyết to ánTruy vấn")]
        jsxxcx = 4,
        [Description("【5204】Chi tiết phíTruy vấn")]
        fymxcx = 5,
        [Description("【5205】nhân viên bệnh mãn tính đặc biệt dùng thuốcBản ghiTruy vấn")]
        rymtbyyjl = 6,
        [Description("【5206】nhân viên lũy kế thông tinTruy vấn")]
        ryljxxcx = 7,
        [Description("【5301】nhân viên bệnh đặc biệt đăng ký lưuTruy vấn")]
        rytsbbacx = 8,
        [Description("【5302】nhân viên chỉ định thông tinTruy vấn")]
        ryddxxcx = 9,
        [Description("【5303】Đang ở viện thông tinTruy vấn")]
        zyxxcx = 10,
        [Description("【5304】Chuyển viện thông tinTruy vấn")]
        zyxx = 11,
        [Description("【5401】Mục công nhận lẫn nhau thông tinTruy vấn")]
        xmhrxx = 5401,
        [Description("【5402】Báo cáoChi tiết thông tinTruy vấn")]
        bgmxxx = 5402,

    }

    public enum EnumYbJGQuery
    {
        [Description("【1201】cơ sở y tế thông tin lấy")]
        jgxx = 1201,
        [Description("【3508】chỉ định cơ sở y tế hàng hóaTồn kho thông tinTruy vấn")]
        yljgspkcxx = 3508,
        [Description("【3509】chỉ định cơ sở y tế hàng hóaTồn kho thay đổiBản ghiTruy vấn")]
        yljgspkcbgjl = 3509,
        [Description("【3510】chỉ định cơ sở y tế hàng hóa mua sắm thông tinTruy vấn")]
        yljgspcgxx = 3510,
        [Description("【3511】chỉ định cơ sở y tế hàng hóa bán thông tinTruy vấn")]
        yljgspxsxx = 3511,
        [Description("【3512】chỉ định cơ sở y tế nhập kho hàng hóa truy xuất thông tinTruy vấn")]
        yljgrkspzsxx = 3512,
        [Description("【3513】chỉ định cơ sở y tế hàng hóa bán truy xuất thông tinTruy vấn")]
        yljgspxszsxx = 3513,
        [Description("【3607】quyết to án bảngKết quả kiểm soát chất lượngTruy vấn")]
        jsqdzkjg = 3607,
        [Description("【5101】Khoa thông tinTruy vấn")]
        ksxxcx = 5101,
    }
    /// <summary>
    /// Chờ遇Kiểm traLoại
    /// </summary>
    public enum Enumdyjclx
    {
        [Description("Kiểm tra hạng mục quỹ")]
        jjxjc = 1,
        [Description("KhácKiểm tra")]
        qtjc = 99
    }
    /// <summary>
    /// quỹthanh toánLoại
    /// </summary>
    public enum Enumjjzflx
    {
        [Description("quỹ chung BHYT cơ bản người lao động thành thị")]
        tcjj = 310100,
        [Description("quỹ tài khoản cá nhân BHYT cơ bản người lao động thành thị")]
        grzhjj = 310200,
        [Description("công chức trợ cấp y tế quỹ")]
        gwybzjj = 320100,
        [Description("số tiền lớn y tế phí trợ cấp quỹ")]
        debzjj = 330100,
        [Description("Nghỉ hưu nghỉ hưu nhân viên bảo đảm y tế quỹ")]
        lxbzjj = 340100,
        [Description("Quỹ trợ cấp y tế quân nhân khuyết tật cấp một đến sáu")]
        cjjrbzjj = 350100,
        [Description("bổ sung doanh nghiệpBảo hiểm y tế quỹ")]
        qybxjj = 370100,
        [Description("Bệnh viện tạm ứng quỹ")]
        yydfjj = 999996,
        [Description("Quỹ công chức cấp quận")]
        qgwyjj = 999901,
        [Description("quỹ BHYT cơ bản dân cư thành thị-nông thôn")]
        cxjmbxjj = 390100,
        [Description("thành thị-nông thôn dân cư bệnh nặngBảo hiểm y tế quỹ")]
        cxjmdbbxjj = 390200,
        [Description("sinh đẻ quỹ")]
        syjj = 510100,
        [Description("người lao động sinh đẻ quỹ")]
        zgsyjj = 310103,
        [Description("trợ giúp y tế quỹ")]
        yljzjj = 610100,
        [Description("trợ giúp y tế bổ sung quỹ")]
        ylzjzjj = 610102,
        [Description("giảm nghèo sức khỏe quỹ")]
        jkpfjj = 610103,
        [Description("giảm nghèo chính xác quỹ")]
        jztpjj = 610104,
        [Description("Khác quỹ")]
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
        [Description("tây yChẩn đo án")]
        xyzd = 1,
        [Description("trongy chủ bệnhChẩn đo án")]
        zyzzd = 2,
        [Description("trongy chủ chứngChẩn đo án")]
        zyzzzd = 3
    }
    /// <summary>
    /// Loại bảo hiểmLoại
    /// </summary>
    public enum Enumxzlx
    {
        [Description("BHYT cơ bản người lao động")]
        zgjbylbx = 310,
        [Description("công chức trợ cấp y tế")]
        gwyylbz = 320,
        [Description("số tiền lớn y tế phí trợ cấp")]
        deylfybz = 330,
        [Description("Nghỉ hưu nghỉ hưu nhân viên bảo đảm y tế")]
        lxryylbz = 340,
        [Description("Trợ cấp y tế quân nhân tàn tật cấp một đến sáu")]
        cfjrylbz = 350,
        [Description("cựu Hồng quân bảo đảm y tế")]
        lhjylbz = 360,
        [Description("bổ sung doanh nghiệpBảo hiểm y tế")]
        qybcylbx = 370,
        [Description("BHYT hợp tác nông thôn kiểu mới")]
        xxnchzyl = 380,
        [Description("BHYT cơ bản dân cư thành thị-nông thôn")]
        cxjmjbylbx = 390,
        [Description("BHYT cơ bản dân cư thành thị")]
        czjmjbylbx = 391,
        [Description("thành thị-nông thôn dân cư bệnh nặngBảo hiểm y tế")]
        cxjmdbbx = 392,
        [Description("Khác đặc biệt nhân viên bảo đảm y tế")]
        qttsryylbz = 399,
        [Description("Dài hạn chiếu hộ bảo hiểm")]
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
        [Description("Ngoại trú đăng ký khám")]
        mzgh = 12,
        [Description("cấp cứu")]
        jz = 13,
        [Description("Ngoại trú bệnh mãn tính đặc biệt")]
        mzmtb = 14,
        [Description("Ngoại trú thương tích ngoài ý muốn")]
        ywshmz = 19,
        [Description("thông thường nhập viện")]
        ptzy = 21,
        [Description("Chuyển ngoài chẩn trị nhập viện")]
        zwzzzy = 23,
        [Description("cấp cứuChuyển nhập viện")]
        jzzzy = 24,
        [Description("sinh đẻNgoại trú")]
        symz = 51,
        [Description("sinh đẻ nhập viện")]
        syzy = 52,
        [Description("kế hoạch hóa gia đìnhPhẫu thuật phí")]
        jhsyssf = 53,
        [Description("lớnBệnhNgoại trú")]
        zdjbmz = 9901,
        [Description("lớnBệnh nhập viện")]
        zdjbzy = 9902,
        [Description("hai bệnh trẻ emNgoại trú")]
        etlbmz = 9903,
        [Description("hai bệnh trẻ em nhập viện")]
        etlbzy = 9904,
        [Description("lao đa kh áng thuốcNgoại trú")]
        ndyjhmz = 9906,
        [Description("lao đa kh áng thuốc nhập viện")]
        ndyjhzy = 9907,
        [Description("Chuyển vào nhập viện")]
        zrzy = 9925,
        [Description("Trẻ sơ sinh nhập viện theo mẹ")]
        xsesmzy = 9921
    }
    /// <summary>
    /// 申报来源
    /// </summary>
    public enum Enumsbly
    {
        [Description("chỉ định cơ sở y tế")]
        etlbmz = 01,
        [Description("Hệ thống đã xử lý tại trung tâm")]
        etlbzy = 02,
        [Description("Hệ thống đã xử lý trực tuyến")]
        ndyjhmz = 03,
        [Description("APP")]
        ndyjhzy = 04,
        [Description("máy tích hợp")]
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
        [Description("Bình thường sinh")]
        Zcc = 1,
        /// <summary>
        /// 助娩产
        /// </summary>
        [Description("hỗ trợ sinh")]
        Zmc = 2,
        /// <summary>
        /// 剖宫产
        /// </summary>
        [Description("mổ lấy thai")]
        Pgc = 3,
        /// <summary>
        /// 难产
        /// </summary>
        [Description("sinh khó")]
        nc = 4,
        /// <summary>
        /// sảy thai
        /// </summary>
        [Description("sảy thai")]
        lc = 5,
        /// <summary>
        /// 引产
        /// </summary>
        [Description("gây chuyển dạ")]
        yc = 6,
        /// <summary>
        /// 产trướcKiểm tra
        /// </summary>
        [Description("sinh trướcKiểm tra")]
        cqjc = 7,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9,
        /// <summary>
        /// thai ngoài tử cung
        /// </summary>
        [Description("thai ngoài tử cung")]
        gwc = 10,
        /// <summary>
        /// 遗传基因检测
        /// </summary>
        [Description("xét nghiệm gen di truyền")]
        ycjyjc = 11,
        /// <summary>
        /// bốn个Tháng以trên流引产
        /// </summary>
        [Description("Sảy thai trên bốn tháng")]
        sgyyslc = 12,
        /// <summary>
        /// bốn个Tháng以dướisảy thai
        /// </summary>
        [Description("Sảy thai dưới bốn tháng")]
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
        [Description("đặt vòng tránh thai")]
        fzgnjyq = 1,
        /// <summary>
        /// 取出宫trong节育器
        /// </summary>
        [Description("tháo vòng tránh thai")]
        qcgnjyq = 2,
        /// <summary>
        /// sảy thai术
        /// </summary>
        [Description("thủ thuật sảy thai")]
        lcs = 3,
        /// <summary>
        /// 引产术
        /// </summary>
        [Description("thủ thuật gây chuyển dạ")]
        ycs = 4,
        /// <summary>
        /// 绝育Phẫu thuật
        /// </summary>
        [Description("triệt sảnPhẫu thuật")]
        jyss = 5,
        /// <summary>
        /// 绝育复通Phẫu thuật
        /// </summary>
        [Description("phục hồi triệt sảnPhẫu thuật")]
        jyftss = 6,
        /// <summary>
        /// 绝育Phẫu thuật（输精管）
        /// </summary>
        [Description("triệt sảnPhẫu thuật（ống dẫn tinh）")]
        jysssjg = 7,
        /// <summary>
        /// 绝育复通Phẫu thuật（输精管）
        /// </summary>
        [Description("phục hồi triệt sảnPhẫu thuật（ống dẫn tinh）")]
        jyftsssjg = 8,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9,
        /// <summary>
        /// 皮dưới埋植术
        /// </summary>
        [Description("cấy que dưới da")]
        pxmzs = 10,
        /// <summary>
        /// Hủy皮dưới埋植术
        /// </summary>
        [Description("Hủy cấy que dưới da")]
        qxpxmzs = 11,
        /// <summary>
        /// 输卵管结扎术
        /// </summary>
        [Description("thắt ống dẫn trứng")]
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
        [Description("chủ nhiệmBác sĩ")] //chief
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
        [Description("người lao động BHYT")]
        zg = 11,
        /// <summary>
        /// dân cưBHYT
        /// </summary>
        [Description("dân cư BHYT")]
        jm = 12,
        /// <summary>
        /// Nghỉ hưunghỉ hưu
        /// </summary>
        [Description("Nghỉ hưu nghỉ hưu")]
        lx = 13,
        /// <summary>
        /// thông thườngBHYT
        /// </summary>
        [Description("thông thường BHYT")]
        pt = 1,
    }
    /// <summary>
    /// 订单Trạng thái0:Chờthanh toán 1:đãthanh toán  2::Đã hoàn khoản 3：Đang thanh toán
    /// </summary>
    public enum EnumOrderStatus
    {
        /// <summary>
        /// Chờthanh toán
        /// </summary>
        [Description("Chờthanh to án")]
        dzf = 0,
        /// <summary>
        /// đãthanh toán
        /// </summary>
        [Description("đãthanh to án")]
        yzf = 1,
        /// <summary>
        /// Đã hoàn khoản
        /// </summary>
        [Description("Đã hoàn khoản")]
        ytk = 2,
        /// <summary>
        /// Đang thanh toán
        /// </summary>
        [Description("Đang thanh to án")]
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
