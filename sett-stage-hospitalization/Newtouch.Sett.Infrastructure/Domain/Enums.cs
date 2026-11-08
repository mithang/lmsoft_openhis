using System;
using System.ComponentModel;

namespace Newtouch.Sett.Domain
{
    /// <summary>
    /// Mở rộng hạng mục enum loại bản ghi nhật ký
    /// Value: Vui lòng bắt đầu từ 101
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
    /// Đang ở viện cờ
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
        /// Xuất viện khoa bệnh (ra khoa bệnh) (chờ quyết toán)
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
    /// Loại giao dịch BHYT: 0 Không giao dịch, 1 Giao dịch thông thường, 2 Giao dịch bệnh nặng, 3 Giao dịch giường bệnh tại nhà, 4 Giao dịch tai nạn lao động, 5 Giao dịch nhập viện, 6 Giao dịch bảo hiểm nông nghiệp
    /// xt_brxz.ybjylx
    /// </summary>
    public enum EnumYBJYLX
    {
        /// <summary>
        /// không giao dịch
        /// </summary>
        [Description("không giao dịch")]
        ybjylx0 = 0,
        /// <summary>
        /// thông thường giao dịch
        /// </summary>
        [Description("thông thường giao dịch")]
        ybjylx1 = 1,
        /// <summary>
        /// bệnh nặng giao dịch
        /// </summary>
        [Description("bệnh nặng giao dịch")]
        ybjylx2 = 2,
        /// <summary>
        /// giường bệnh tại nhà giao dịch
        /// </summary>
        [Description("giường bệnh tại nhà giao dịch")]
        ybjylx3 = 3,
        /// <summary>
        /// tai nạn lao động giao dịch
        /// </summary>
        [Description("tai nạn lao động giao dịch")]
        ybjylx4 = 4,
        /// <summary>
        /// nhập viện giao dịch
        /// </summary>
        [Description("nhập viện giao dịch")]
        ybjylx5 = 5,
        /// <summary>
        /// bảo hiểm nông nghiệp giao dịch
        /// </summary>
        [Description("bảo hiểm nông nghiệp giao dịch")]
        ybjylx6 = 6,
    }

    /// <summary>
    /// Tính chất tự trả (thuộc tính của từ điển thuốc, mục)
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
    /// Thuật toán tính chất tự trả
    /// Xác nhận tính chất của phần tự trả tính ra theo tỷ lệ tự trả
    /// </summary>
    public enum EnumSuanFaZFXZ
    {
        /// <summary>
        /// Tự trả (cùng với tự trả tiền mặt sau giao dịch BHYT)
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
    /// Tính chất thu chi tài khoản
    /// xt_brzhszjl.szxz
    /// </summary>
    public enum AccSzxz
    {
        [Description("Tài khoản gửi/rút ngoại trú")]
        MZZH = 0,
        [Description("giường bệnh tại nhà ghi sổ")]
        JCZH = 1,
        [Description("Quyết toán ngoại trú")]
        MZJS = 2,
        [Description("Khoa bệnh ghi sổ")]
        BQJZ = 3,
        [Description("quyết toán nhập viện")]
        ZYJS = 4,
        [Description("Tài khoản gửi/rút nhập viện")]
        ZYZH = 5,
        [Description("khoản nộp bổ sung nhập viện")]
        ZYBJK = 6,
        [Description("ghi sổ ngoại trú đăng ký")]
        MZJZDJ = 7
    }

    /// <summary>
    /// Loại thẻ / Phần nhận dạng thẻ CMND/CCCD
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
        /// Thẻ ảo/Thẻ trong viện (tự động tạo khi dùng thẻ ảo / nhập tay khi dùng thẻ trong viện)
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
        /// CMND/CCCD khám BHYT
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
        /// thông thường Ngoại trú
        /// </summary>
        [Description("thông thường Ngoại trú")]
        generalOutpat = 1,
        /// <summary>
        /// cấp cứu
        /// </summary>
        [Description("cấp cứu")]
        emerDiagnosis = 2,
        /// <summary>
        /// Ngoại trú chuyên gia
        /// </summary>
        [Description("Ngoại trú chuyên gia")]
        expertOutpat = 3,
        /// <summary>
        /// Ngoại trú bệnh đặc biệt
        /// </summary>
        [Description("Ngoại trú bệnh đặc biệt")]
        SpecialOutpat = 4,
        /// <summary>
        /// Ngoại trú bệnh nặng
        /// </summary>
        [Description("Ngoại trú bệnh nặng")]
        MajorDiseases = 5,
        /// <summary>
        /// Ngoại trú bệnh mãn tính
        /// </summary>
        [Description("Ngoại trú bệnh mãn tính")]
        ChronicDisease = 6,
        /// <summary>
        /// dân cư hai bệnh
        /// </summary>
        [Description("dân cư hai bệnh")]
        jmlb = 7,
        /// <summary>
        /// Ngoại trú thương tích ngoài ý muốn
        /// </summary>
        [Description("Ngoại trú thương tích ngoài ý muốn")]
        ywshmz = 8,
        /// <summary>
        /// Ngoại trú sinh đẻ
        /// </summary>
        [Description("Ngoại trú sinh đẻ")]
        symz = 9,
        /// <summary>
        /// lao đa kháng thuốc Ngoại trú
        /// </summary>
        [Description("lao đa kháng thuốc Ngoại trú")]
        ndyjhmz = 10,
        /// <summary>
        /// hai bệnh trẻ em Ngoại trú
        /// </summary>
        [Description("hai bệnh trẻ em Ngoại trú")]
        etlbmz = 11,
        /// <summary>
        /// Ngoại trú đơn giản
        /// </summary>
        [Description("Ngoại trú đơn giản")]
        jymz = 12
    }

    /// <summary>
    /// Cờ ngoại trú/nhập viện: 0 Chung, 1 Chỉ ngoại trú, 2 Chỉ nhập viện, 3 Hệ thống (cả ngoại trú và nhập viện đều không dùng được), ví dụ: giường bệnh tại nhà
    /// </summary>
    public enum Enummzzybz
    {
        /// <summary>
        /// chung
        /// </summary>
        [Description("chung")]
        ty = 0,
        /// <summary>
        /// chỉ Ngoại trú
        /// </summary>
        [Description("chỉ Ngoại trú")]
        mz = 1,
        /// <summary>
        /// chỉ nhập viện
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
    /// Trạng thái quyết toán
    /// </summary>
    public enum EnumJieSuanZT
    {
        /// <summary>
        /// Đã quyết toán（chưa hoàn）
        /// </summary>
        [Description("Đã quyết toán")]
        YJ = 1,
        /// <summary>
        /// Đã hoàn
        /// </summary>
        [Description("Đã hoàn")]
        YT,
    }

    /// <summary>
    /// y lệnh tính chất
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
    /// Cờ ngừng dùng hóa đơn tài chính
    /// </summary>
    public enum EnumCWFPTY
    {
        /// <summary>
        /// Chưa ngừng dùng
        /// </summary>
        WTY = 0,

        /// <summary>
        /// Đã ngừng dùng
        /// </summary>
        YTY = 1,
    }
    /// <summary>
    /// đăng ký khám tính chất 
    /// 0-thông thường đăng ký khám
    /// 1-trống đăng ký khám
    /// 2 - Đăng ký khám giường bệnh tại nhà
    /// 3-Tự phíChuyểnBHYTđăng ký khám
    /// 4-ghi sổ ngoại trú đăng ký khám
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
    /// Loại quyết toán
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
    ///y lệnh tính chất 1 Tạm thời y lệnh   2 Dài hạn y lệnh   3 Xuất viện y lệnh
    /// </summary>
    public enum EnumYZXZ
    {
        /// <summary>
        /// Tạm thời y lệnh
        /// </summary>
        LSYZ = 1,
        /// <summary>
        /// Dài hạn y lệnh
        /// </summary>
        CQYZ = 2,
        /// <summary>
        /// Xuất viện y lệnh
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
    /// ghi sổ nhập việnMục thu phíLoại
    /// </summary>
    public enum EnumxmType
    {
        XM = 2,//Thuốc
        YP = 1 //Mục
    }

    /// <summary>
    /// Ngôn ngữ
    /// </summary>
    public enum Enumlan
    {
        Default = 0,
        Fanti = 1,
        English = 2,
        Jpanese = 3,
    }
    /// <summary>
    /// Phương thức in: 1: In bổ sung, 2: In lại, 3: In hóa đơn
    /// </summary>
    public enum Enumdyfs
    {
        /// <summary>
        /// In bổ sung
        /// </summary>
        BD = 1,
        /// <summary>
        /// In lại
        /// </summary>
        CD = 2,
        /// <summary>
        /// In hóa đơn
        /// </summary>
        Print = 3
    }

    /// <summary>
    /// Loại đăng ký khám: đặc biệt, đặc biệt V
    /// </summary>
    [Obsolete("Mã của các cơ sở y tế khác nhau không giống nhau")]
    public enum Enumghlx
    {
        TeXu = 7884,
        TeXuV = 7889,
    }

    /// <summary>
    /// Cờ có hiệu lực
    /// </summary>
    public enum EnumSXBZ
    {
        /// <summary>
        /// tức là lúc có hiệu lực
        /// </summary>
        [Description("tức là lúc có hiệu lực")]
        Jssx = 0,
        /// <summary>
        /// Chỉ định có hiệu lực
        /// </summary>
        [Description("chỉ định có hiệu lực")]
        Ddsx = 1

    }

    /// <summary>
    /// Cờ khu vực
    /// </summary>
    public enum EnumDY
    {
        /// <summary>
        /// địa phương
        /// </summary>
        [Description("địa phương")]
        bd = 0,
        /// <summary>
        /// Nơi khác
        /// </summary>
        [Description("nơi khác")]
        wd = 1

    }

    /// <summary>
    /// hôn nhân không
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// chưa hôn nhân
        /// </summary>
        [Description("chưa hôn nhân")]
        wh = 0,
        /// <summary>
        /// đã hôn nhân
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
    /// Đường nhập viện
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
        [Description("Cấp cứu chuyển nhập viện")]
        jz = 24,
        /// <summary>
        /// Chuyển vào từ cơ sở y tế khác
        /// </summary>
        [Description("Chuyển vào nhập viện")]
        qtyljg = 9925,
        /// <summary>
        /// Chuyển ngoài chẩn trị nhập viện
        /// </summary>
        [Description("Chuyển ngoài chẩn trị nhập viện")]
        zwzzzy = 23,
        /// <summary>
        /// Bệnh nặng nhập viện
        /// </summary>
        [Description("Bệnh nặng nhập viện")]
        dbry = 9902,
        /// <summary>
        /// sinh đẻ nhập viện
        /// </summary>
        [Description("sinh đẻ nhập viện")]
        syzy = 52,
        /// <summary>
        /// hai bệnh trẻ em nhập viện
        /// </summary>
        [Description("hai bệnh trẻ em nhập viện")]
        etlb = 9904,
        /// <summary>
        /// Lao đa kháng thuốc nhập viện
        /// </summary>
        [Description("Lao đa kháng thuốc nhập viện")]
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
    /// Loại giấy tờ
    /// </summary>
    public enum EnumZJLX
    {
        /// <summary>
        /// CMND/CCCD
        /// </summary>
        [Description("CMND/CCCD")]
        sfz = 1,
        /// <summary>
        /// hộ chiếu
        /// </summary>
        [Description("hộ chiếu")]
        hz,
        /// <summary>
        /// chứng nhận sĩ quan
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
    /// Chế độ đặc biệt BHYT
    /// </summary>
    public enum EnumYBTSDY
    {
        /// <summary>
        /// thông thường
        /// </summary>
        [Description("thông thường")]
        pt = 1,
        /// <summary>
        /// Nghỉ hưu
        /// </summary>
        [Description("Nghỉ hưu")]
        lx,
        /// <summary>
        /// Thương tật
        /// </summary>
        [Description("Thương tật")]
        sc

    }

    /// <summary>
    /// Phương pháp áp dụng BHYT: 0: Bảo hiểm thành phố, 1: BHYT cá thể, 2: Thị trấn nhỏ, 3: Lao động theo giờ
    // A: Trợ giúp y tế dân chính
    // B：老Năm遗嘱 C:năm保老人 D：Nhân viên tàn tật nặng
    // E: Học sinh tiểu học đang đi học và trẻ sơ sinh F: Dân cư khác
    //G: Đối tượng trợ giúp tương trợ y tế
    /// </summary>
    public enum EnumSYYBBF
    {
        /// <summary>
        /// bảo hiểm thành phố
        /// </summary>
        [Description("bảo hiểm thành phố")]
        cb = 1,
        /// <summary>
        /// BHYT cá thể
        /// </summary>
        [Description("BHYT cá thể")]
        gtyb,
        /// <summary>
        /// thị trấn nhỏ
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
    /// Thời gian lịch trực đăng ký khám
    /// </summary>
    public enum EnumRegSchedule
    {
        /// <summary>
        /// không có
        /// </summary>
        [Description("không có")]
        none = 0,
        /// <summary>
        /// toàn bộ Ngày
        /// </summary>
        [Description("toàn bộ Ngày")]
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
    /// Thực hiện kế hoạch ghi sổTrạng thái
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
        /// Nhắc nhở vượt hạn mức số tiền
        /// </summary>
        [Description("Nhắc nhở vượt hạn mức số tiền")]
        TienTrenHanNhacNho = 1,    //Ví dụ: Trị liệu sư một tháng thu phí không được vượt quá mười vạn
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
    /// Cờ đăng nhập
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
    /// Chính sách tính giá mục thu phí
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// theo Thời lượng
        /// </summary>
        [Description("theo Thời lượng")]
        Time = 1,
        /// <summary>
        /// theo Số lượng
        /// </summary>
        [Description("theo Số lượng")]
        Amount,
        /// <summary>
        /// theo diện tích
        /// </summary>
        [Description("theo diện tích")]
        Acreage,
    }

    /// <summary>
    /// Trạng thái khám của bệnh nhân ngoại trú (cờ khám)
    /// </summary>
    public enum EnumOutpatientJzbz
    {
        /// <summary>
        /// Chờ khám
        /// </summary>
        [Description("Chờ khám")]
        Djz = 1,
        /// <summary>
        /// Đang khám
        /// </summary>
        [Description("Đang khám")]
        Jzz,
        /// <summary>
        /// Kết thúc khám
        /// </summary>
        [Description("Kết thúc khám")]
        Jsjz,
    }

    /// <summary>
    /// Loại enum
    /// </summary>
    public enum EnumPrescriptionType
    {
        /// <summary>
        /// Thuốc
        /// </summary>
        [Description("Thuốc")]
        Medicine = 1,
        /// <summary>
        /// Khoản mục trị liệu
        /// </summary>
        [Description("Khoản mục trị liệu")]
        Treament,
    }

    /// <summary>
    /// Tính chất thu chi
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
    /// Mức độ nguy kịch
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// thông thường
        /// </summary>
        [Description("thông thường")]
        Normal = 1,
        /// <summary>
        /// bệnh nặng
        /// </summary>
        [Description("bệnh nặng")]
        Serious = 2,
        /// <summary>
        /// Bệnh nguy kịch
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
        /// Chờ xét duyệt
        /// </summary>
        [Description("Chờ xét duyệt")]
        DS = 3,
        /// <summary>
        /// Chưa gửi
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
        [Description("Không rõ")]
        UnKnown = 3
    }

    public enum EnumTradeType
    {

        /// <summary>
        /// Mã thanh toán Alipay
        /// </summary>
        [Description("Alipay")]
        Alipay_Bar_Code = 1,
        /// <summary>
        /// Mã thanh toán WeChat
        /// </summary>
        [Description("WeChat")]
        Wechat_MICROPAY = 2,
    }

    /// <summary>
    /// Enum - Đơn vị tuổi
    /// </summary>
    public enum EnumAgeUnit
    {
        Year = 1,
        Month = 2,
        Day = 3,
        Hour = 4,
    }
    /// <summary>
    /// Ngoại trú đặt hẹnTrạng thái
    /// </summary>
    public enum EnumMzyyzt
    {
        [Description("đã hẹn")]
        book = 1,
        [Description("đã đăng ký khám")]
        reg = 2,
        [Description("đặt hẹn đã Hủy")]
        cancel = 3,
        [Description("đăng ký khám trong ngày")]
        bookreg = 4,
        [Description("Hủy đăng ký khám")]
        regcancel = 5
    }
    /// <summary>
    /// Loại lịch trực ngoại trú
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
    /// Mở đặt hẹn
    /// </summary>
    public enum EnumMzpbBook
    {
        [Description("là")]
        Y = 1,
        [Description("không")]
        F = 0,
    }
    /// <summary>
    /// Trạng thái đơn thuốc
    /// </summary>
    public enum EnumZfzt
    {

        [Description("Bình thường đơn thuốc")]
        zc = 0,
        [Description("Hoàn thuốc hoặc hủy bỏ đơn thuốc")]
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
    /// Nguồn đăng ký khám ngoại trú
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
    /// BHYT thuộc tính
    /// </summary>
    public enum EnumCblb
    {
        [Description("Tất cả")]
        all = 0,
        [Description("người lao động")]
        zg = 1,
        [Description("dân cư")]
        jm = 2,
        [Description("Nghỉ hưu")]
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
        [Description("Bảng xin phí dân cư (cứu trợ dân chính)")]
        jmmzjz = 2,
        [Description("báo cáo thanh toán thông thường(người lao động)")]
        zgbz = 3,
        [Description("báo cáo thanh toán thông thường(dân cư)")]
        jmbz = 4,
        [Description("báo cáo thanh toán thông thường (Nghỉ hưu)")]
        lxbz = 5,
        [Description("báo cáo thanh toán thông thường(khác địa phương người lao động)")]
        ydzgbz = 6,
        [Description("báo cáo thanh toán thông thường(khác địa phương dân cư)")]
        ydjmbz = 7,

    }
    /// <summary>
    /// nhân viênLoại
    /// </summary>
    public enum EnumRylb
    {
        [Description("người lao động tại chức")]
        zgzz = 1101,
        [Description("công chức tại chức")]
        gwyzz = 1102,
        [Description("nghề tự do nhân viên tại chức")]
        lhjyryzz = 1103,
        [Description("Nhân viên mở rộng khác địa phương")]
        qtdfkzry = 1160,
        [Description("người lao động nghỉ hưu")]
        zgtx = 1201,
        [Description("công chức nghỉ hưu")]
        gwytx = 1202,
        [Description("nhân viên nghề tự do nghỉ hưu")]
        lhjyrytx = 1203,
        [Description("Nhân viên mở rộng khác địa phương")]
        qtdfkzrytx = 1260,
        [Description("Nghỉ hưu nhân viên")]
        lxry = 1300,
        [Description("Nhân viên mở rộng khác địa phương")]
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
        [Description("CMND/CCCD mở rộng khác địa phương")]
        dfqtgkzf = 1460,
        [Description("dân cư thông thường (thành niên)")]
        ptjm = 1501,
        [Description("CMND/CCCD mở rộng khác địa phương")]
        jmdfqtgzsf = 1560,
        [Description("dân cư (người cao tuổi)")]
        jmln = 16,
        [Description("dân cư (thành niên)")]
        jmcn = 15,
        [Description("dân cư (chưa thành niên)")]
        jmwcn = 14,
        [Description("Nghỉ hưu")]
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
        [Description("【4207】Tra cứu chi tiết phí bệnh nhân tự phí")]
        zfbrjyfymx = 4207,
        [Description("【4208】Tra cứu thông tin khám bệnh nhân tự phí")]
        zfbrjyjzxx = 4208,
        [Description("【4209】Tra cứu thông tin chẩn đoán bệnh nhân tự phí")]
        zfbrjyzdxx = 4209,
        [Description("【5201】Tra cứu thông tin khám")]
        jzxxcx = 2,
        [Description("【5202】Tra cứu thông tin chẩn đoán")]
        zdxxcx = 3,
        [Description("【5203】Tra cứu thông tin quyết toán")]
        jsxxcx = 4,
        [Description("【5204】Tra cứu chi tiết phí")]
        fymxcx = 5,
        [Description("【5205】Tra cứu bản ghi dùng thuốc bệnh mãn tính đặc biệt của nhân viên")]
        rymtbyyjl = 6,
        [Description("【5206】Tra cứu thông tin lũy kế của nhân viên")]
        ryljxxcx = 7,
        [Description("【5301】Tra cứu đăng ký lưu bệnh đặc biệt của nhân viên")]
        rytsbbacx = 8,
        [Description("【5302】Tra cứu thông tin chỉ định của nhân viên")]
        ryddxxcx = 9,
        [Description("【5303】Tra cứu thông tin đang ở viện")]
        zyxxcx = 10,
        [Description("【5304】Tra cứu thông tin chuyển viện")]
        zyxx = 11,
        [Description("【5401】Tra cứu thông tin mục công nhận lẫn nhau")]
        xmhrxx = 5401,
        [Description("【5402】Tra cứu thông tin chi tiết báo cáo")]
        bgmxxx = 5402,

    }

    public enum EnumYbJGQuery
    {
        [Description("【1201】cơ sở y tế thông tin lấy")]
        jgxx = 1201,
        [Description("【3508】Tra cứu thông tin tồn kho hàng hóa cơ sở y tế chỉ định")]
        yljgspkcxx = 3508,
        [Description("【3509】Tra cứu bản ghi thay đổi tồn kho hàng hóa cơ sở y tế chỉ định")]
        yljgspkcbgjl = 3509,
        [Description("【3510】Tra cứu thông tin mua sắm hàng hóa cơ sở y tế chỉ định")]
        yljgspcgxx = 3510,
        [Description("【3511】Tra cứu thông tin bán hàng hóa cơ sở y tế chỉ định")]
        yljgspxsxx = 3511,
        [Description("【3512】Tra cứu truy xuất hàng hóa nhập kho cơ sở y tế chỉ định")]
        yljgrkspzsxx = 3512,
        [Description("【3513】Tra cứu truy xuất bán hàng hóa cơ sở y tế chỉ định")]
        yljgspxszsxx = 3513,
        [Description("【3607】Tra cứu kết quả kiểm soát chất lượng bảng quyết toán")]
        jsqdzkjg = 3607,
        [Description("【5101】Tra cứu thông tin khoa")]
        ksxxcx = 5101,
    }
    /// <summary>
    /// Loại kiểm tra chế độ
    /// </summary>
    public enum Enumdyjclx
    {
        [Description("Kiểm tra hạng mục quỹ")]
        jjxjc = 1,
        [Description("Khác Kiểm tra")]
        qtjc = 99
    }
    /// <summary>
    /// quỹ thanh toánLoại
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
        [Description("Quỹ bảo hiểm y tế bổ sung doanh nghiệp")]
        qybxjj = 370100,
        [Description("Bệnh viện tạm ứng quỹ")]
        yydfjj = 999996,
        [Description("Quỹ công chức cấp quận")]
        qgwyjj = 999901,
        [Description("quỹ BHYT cơ bản dân cư thành thị-nông thôn")]
        cxjmbxjj = 390100,
        [Description("Quỹ bảo hiểm y tế bệnh nặng dân cư thành thị-nông thôn")]
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
    /// Cờ hưởng chế độ khoản tiền quỹ
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
        [Description("Tây y Chẩn đoán")]
        xyzd = 1,
        [Description("Đông y chủ bệnh Chẩn đoán")]
        zyzzd = 2,
        [Description("Đông y chủ chứng Chẩn đoán")]
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
        [Description("Bảo hiểm y tế bổ sung doanh nghiệp")]
        qybcylbx = 370,
        [Description("BHYT hợp tác nông thôn kiểu mới")]
        xxnchzyl = 380,
        [Description("BHYT cơ bản dân cư thành thị-nông thôn")]
        cxjmjbylbx = 390,
        [Description("BHYT cơ bản dân cư thành thị")]
        czjmjbylbx = 391,
        [Description("Bảo hiểm y tế bệnh nặng dân cư thành thị-nông thôn")]
        cxjmdbbx = 392,
        [Description("Khác đặc biệt nhân viên bảo đảm y tế")]
        qttsryylbz = 399,
        [Description("Dài hạn chiếu hộ bảo hiểm")]
        cqzhbx = 410,
        [Description("Bảo hiểm sinh đẻ")]
        sybx = 510
    }
    /// <summary>
    /// Loại y tế
    /// </summary>
    public enum Enumyllb
    {
        [Description("thông thường Ngoại trú")]
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
        [Description("Cấp cứu chuyển nhập viện")]
        jzzzy = 24,
        [Description("Ngoại trú sinh đẻ")]
        symz = 51,
        [Description("sinh đẻ nhập viện")]
        syzy = 52,
        [Description("Phí phẫu thuật kế hoạch hóa gia đình")]
        jhsyssf = 53,
        [Description("Ngoại trú bệnh nặng")]
        zdjbmz = 9901,
        [Description("Bệnh nặng nhập viện")]
        zdjbzy = 9902,
        [Description("hai bệnh trẻ em Ngoại trú")]
        etlbmz = 9903,
        [Description("hai bệnh trẻ em nhập viện")]
        etlbzy = 9904,
        [Description("lao đa kháng thuốc Ngoại trú")]
        ndyjhmz = 9906,
        [Description("lao đa kháng thuốc nhập viện")]
        ndyjhzy = 9907,
        [Description("Chuyển vào nhập viện")]
        zrzy = 9925,
        [Description("Trẻ sơ sinh nhập viện theo mẹ")]
        xsesmzy = 9921
    }
    /// <summary>
    /// Nguồn khai báo
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
    /// Loại sinh sản
    /// </summary>
    public enum EnumSylb
    {
        /// <summary>
        /// Bình thường sinh
        /// </summary>
        [Description("Bình thường sinh")]
        Zcc = 1,
        /// <summary>
        /// hỗ trợ sinh
        /// </summary>
        [Description("hỗ trợ sinh")]
        Zmc = 2,
        /// <summary>
        /// mổ lấy thai
        /// </summary>
        [Description("mổ lấy thai")]
        Pgc = 3,
        /// <summary>
        /// sinh khó
        /// </summary>
        [Description("sinh khó")]
        nc = 4,
        /// <summary>
        /// sảy thai
        /// </summary>
        [Description("sảy thai")]
        lc = 5,
        /// <summary>
        /// gây chuyển dạ
        /// </summary>
        [Description("gây chuyển dạ")]
        yc = 6,
        /// <summary>
        /// Kiểm tra trước sinh
        /// </summary>
        [Description("Kiểm tra trước sinh")]
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
        /// xét nghiệm gen di truyền
        /// </summary>
        [Description("xét nghiệm gen di truyền")]
        ycjyjc = 11,
        /// <summary>
        /// Gây chuyển dạ trên bốn tháng
        /// </summary>
        [Description("Sảy thai trên bốn tháng")]
        sgyyslc = 12,
        /// <summary>
        /// Sảy thai dưới bốn tháng
        /// </summary>
        [Description("Sảy thai dưới bốn tháng")]
        sgyyxlc = 13
    }
    /// <summary>
    /// Loại phẫu thuật kế hoạch hóa gia đình
    /// </summary>
    public enum EnumSysslb
    {
        /// <summary>
        /// đặt vòng tránh thai
        /// </summary>
        [Description("đặt vòng tránh thai")]
        fzgnjyq = 1,
        /// <summary>
        /// tháo vòng tránh thai
        /// </summary>
        [Description("tháo vòng tránh thai")]
        qcgnjyq = 2,
        /// <summary>
        /// thủ thuật sảy thai
        /// </summary>
        [Description("thủ thuật sảy thai")]
        lcs = 3,
        /// <summary>
        /// thủ thuật gây chuyển dạ
        /// </summary>
        [Description("thủ thuật gây chuyển dạ")]
        ycs = 4,
        /// <summary>
        /// Phẫu thuật triệt sản
        /// </summary>
        [Description("Phẫu thuật triệt sản")]
        jyss = 5,
        /// <summary>
        /// Phẫu thuật phục hồi triệt sản
        /// </summary>
        [Description("Phẫu thuật phục hồi triệt sản")]
        jyftss = 6,
        /// <summary>
        /// Phẫu thuật triệt sản (ống dẫn tinh)
        /// </summary>
        [Description("Phẫu thuật triệt sản (ống dẫn tinh)")]
        jysssjg = 7,
        /// <summary>
        /// Phẫu thuật phục hồi triệt sản (ống dẫn tinh)
        /// </summary>
        [Description("Phẫu thuật phục hồi triệt sản (ống dẫn tinh)")]
        jyftsssjg = 8,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 9,
        /// <summary>
        /// cấy que dưới da
        /// </summary>
        [Description("cấy que dưới da")]
        pxmzs = 10,
        /// <summary>
        /// Hủy cấy que dưới da
        /// </summary>
        [Description("Hủy cấy que dưới da")]
        qxpxmzs = 11,
        /// <summary>
        /// Thắt ống dẫn trứng
        /// </summary>
        [Description("thắt ống dẫn trứng")]
        srgjzs = 12
    }
    /// <summary>
    /// Cờ sinh muộn / cờ sinh sớm
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
        [Description("Bác sĩ nhập viện")] //Resident
        ZyDoc = 1,
        [Description("Bác sĩ điều trị chính")] //Attending
        ZzDoc = 2,
        [Description("chủ nhiệm Bác sĩ")] //chief
        ZrDoc = 3
    }
    /// <summary>
    /// Dấu hiệu khác địa phương
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
        /// người lao động BHYT
        /// </summary>
        [Description("người lao động BHYT")]
        zg = 11,
        /// <summary>
        /// dân cư BHYT
        /// </summary>
        [Description("dân cư BHYT")]
        jm = 12,
        /// <summary>
        /// Nghỉ hưu
        /// </summary>
        [Description("Nghỉ hưu")]
        lx = 13,
        /// <summary>
        /// thông thường BHYT
        /// </summary>
        [Description("thông thường BHYT")]
        pt = 1,
    }
    /// <summary>
    /// Trạng thái đơn hàng: 0: Chờ thanh toán, 1: Đã thanh toán, 2: Đã hoàn khoản, 3: Đang thanh toán
    /// </summary>
    public enum EnumOrderStatus
    {
        /// <summary>
        /// Chờ thanh toán
        /// </summary>
        [Description("Chờ thanh toán")]
        dzf = 0,
        /// <summary>
        /// đã thanh toán
        /// </summary>
        [Description("đã thanh toán")]
        yzf = 1,
        /// <summary>
        /// Đã hoàn khoản
        /// </summary>
        [Description("Đã hoàn khoản")]
        ytk = 2,
        /// <summary>
        /// Đang thanh toán
        /// </summary>
        [Description("Đang thanh toán")]
        zfz = 3,
    }
    /// <summary>
    /// Loại thẻ giao dịch
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
