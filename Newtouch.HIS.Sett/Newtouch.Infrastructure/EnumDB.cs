using System;
using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// nhật ký bản ghi loại enum mở rộng hạng mục
    /// Valuevui lòng từ 101 bắt đ ầu
    /// </summary>
    public enum DbLogType_Ex
    {
        /// <summary>
        /// Quyết toán xuất viện
        /// </summary>
        [Description("出院结算")]
        DischargeSettlement = 101,

    }

    /// <summary>
    /// Dấu hiệu đang nằm viện
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// Đ ăng ký nhập viện
        /// </summary>
        [Description("新入院")]
        Xry = 0,
        /// <summary>
        /// Khu bệnh trong 
        /// </summary>
        [Description("病区中")]
        Bqz = 1,
        /// <summary>
        /// Khu bệnh xuất viện(rakhu bệnh)(chờ thanh toán)
        /// </summary>
        [Description("待结账")]
        Djz = 2,
        /// <summary>
        /// Đ ãxuất viện(quyết toán xuất viện)
        /// </summary>
        [Description("已出院")]
        Ycy = 3,
        /// <summary>
        /// chuyển Quận trong 
        /// </summary>
        [Description("转区中")]
        Zq = 7,

        /// <summary>
        /// Hủy bỏ bản ghi /Hủy nhập viện
        /// </summary>
        [Description("作废记录")]
        Wry = 9,
    }

    /// <summary>
    /// Hệ thốngTài khoảnLoại
    /// </summary>
    public enum EnumXTZHXZ
    {
        /// <summary>
        /// Đ ơn vị tài khoản 
        /// </summary>
        [Description("单位帐户")]
        DWZH = 0,
        /// <summary>
        /// cá nhân tài khoản 
        /// </summary>
        [Description("个人帐户")]
        GRZH = 1,
        /// <summary>
        /// giư ờng gia đ ình tài khoản
        /// </summary>
        [Description("家床账户")]
        JCZH = 2,
        /// <summary>
        /// Nhập việntạm ứng khoản tài khoản
        /// </summary>
        [Description("住院预缴款账户")]
        ZYYJKZH = 3,
        /// <summary>
        /// giúp khó khăntài khoản
        /// </summary>
        [Description("帮困账户")]
        BKZH = 4,
        /// <summary>
        /// Đ ăng ký ngoại trú tài khoản
        /// </summary>
        [Description("门诊登记账户")]
        MZDJZH = 5,
        /// <summary>
        /// Ngoại trútạm ứng khoản tài khoản
        /// </summary>
        [Description("门诊预缴款账户")]
        MZYJKZH = 6
    }

    /// <summary>
    /// BHYT giao dịch loại 0 không giao dịch 1 thông thư ờng giao dịch 2 bệnh nặng giao dịch 3 giao dịch giư ờng gia đ ình 4 tai nạn lao động giao dịch 5 nhập viện giao dịch 6 bảo hiểm nông nghiệp giao dịch 
    /// xt_brxz.ybjylx
    /// </summary>
    public enum EnumYBJYLX
    {
        /// <summary>
        /// không giao dịch 
        /// </summary>
        [Description("不交易")]
        ybjylx0 = 0,
        /// <summary>
        /// Thông thư ờng giao dịch 
        /// </summary>
        [Description("普通交易")]
        ybjylx1 = 1,
        /// <summary>
        /// bệnh nặng giao dịch 
        /// </summary>
        [Description("大病交易")]
        ybjylx2 = 2,
        /// <summary>
        /// giao dịch giư ờng gia đ ình 
        /// </summary>
        [Description("家床交易")]
        ybjylx3 = 3,
        /// <summary>
        /// tai nạn lao động giao dịch 
        /// </summary>
        [Description("工伤交易")]
        ybjylx4 = 4,
        /// <summary>
        /// Nhập viện giao dịch 
        /// </summary>
        [Description("住院交易")]
        ybjylx5 = 5,
        /// <summary>
        /// bảo hiểm nông nghiệp giao dịch 
        /// </summary>
        [Description("农保交易")]
        ybjylx6 = 6,
    }

    /// <summary>
    /// Tính chất tự trả(thuốc, hạng mục từ điển của thuộc tính)
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Có thể thanh toán
        /// </summary>
        [Description("可报")]
        KB = 0,
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("自费")]
        ZF = 1,
        /// <summary>
        /// Tự trả theo phân loại
        /// </summary>
        [Description("分类自负")]
        FLZF = 2,
        /// <summary>
        /// tuyệt đối đ ối với tự chi trả
        /// </summary>
        [Description("绝对自理")]
        JueDuiZiLi = 3,
        /// <summary>
        /// Giáp loại 
        /// </summary>
        [Description("甲类")]
        Jia,
        /// <summary>
        /// Ất loại 
        /// </summary>
        [Description("乙类")]
        Yi,
        /// <summary>
        /// Bính loại 
        /// </summary>
        [Description("丙类")]
        Bing,
    }

    /// <summary>
    /// thuật toán tính chất tự trả
    /// Xác nhận theo tỷ lệ tự trả tínhracủa tự trả một phần củatính chất
    /// </summary>
    public enum EnumSuanFaZFXZ
    {
        /// <summary>
        /// Tự trả(cùngBHYT giao dịch sau tự trả tiền mặt)
        /// </summary>
        [Description("自负")]
        ZF = 0,
        /// <summary>
        /// Tự chi trả
        /// </summary>
        [Description("自理")]
        ZL = 1,
    }

    /// <summary>
    /// Thu chi tài khoảntính chất 
    /// xt_brzhszjl.szxz
    /// </summary>
    public enum AccSzxz
    {
        [Description("门诊账户存取")]
        MZZH = 0,
        [Description("家床记帐")]
        JCZH = 1,
        [Description("门诊结算")]
        MZJS = 2,
        [Description("病区记帐")]
        BQJZ = 3,
        [Description("住院结算")]
        ZYJS = 4,
        [Description("住院账户存取")]
        ZYZH = 5,
        [Description("住院补缴款")]
        ZYBJK = 6,
        [Description("门诊记账登记")]
        MZJZDJ = 7
    }

    /// <summary>
    /// Loại thẻ/ thẻ thân phận dấu hiệu 
    /// </summary>
    [Description("卡类型")]
    public enum EnumCardType
    {
        /// <summary>
        /// BHYTtừdòng thẻ 
        /// </summary>
        [Description("医保磁条卡")]
        CTK = 0,
        /// <summary>
        /// ảo thẻ / trong viện thẻ (tự đ ộng tạo giờảo thẻ / thủ công nhập giờ trong viện thẻ)
        /// </summary>
        [Description("虚拟卡")]
        XNK = 1,
        /// <summary>
        /// bảo hiểm xã hội thẻ 
        /// </summary>
        [Description("社保卡")]
        YBJYK = 2,
        /// <summary>
        /// BHYTchứng từ điện tử
        /// </summary>
        [Description("医保电子凭证")]
        ybdzpz = 3,
        /// <summary>
        /// CMNDBHYTKhám bệnh
        /// </summary>
        [Description("身份证(医保)")]
        sfz = 4,
        /// <summary>
        /// sức khỏe mã 
        /// </summary>
        [Description("健康码")]
        XNHJYK = 5,
        /// <summary>
        /// quét mặt
        /// </summary>
        [Description("刷脸")]
        YBfACE = 6,
    }

    /// <summary>
    /// Loại ngoại trú mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// Phòng khám thông thư ờng
        /// </summary>
        [Description("普通门诊")]
        generalOutpat = 1,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("急诊")]
        emerDiagnosis = 2,
        /// <summary>
        /// Phòng khám chuyên gia
        /// </summary>
        [Description("专家门诊")]
        expertOutpat = 3,
        /// <summary>
        /// bệnh đặc biệtngoại trú
        /// </summary>
        [Description("特病门诊")]
        SpecialOutpat = 4,
        /// <summary>
        /// bệnh nặng ngoại trú
        /// </summary>
        [Description("重大疾病门诊")]
        MajorDiseases = 5,
        /// <summary>
        /// chậm bệnh tình dụcngoại trú
        /// </summary>
        [Description("慢性病门诊")]
        ChronicDisease = 6,
        /// <summary>
        /// cư dân hai bệnh
        /// </summary>
        [Description("居民两病")]
        jmlb = 7,
        /// <summary>
        /// ngoài ý muốn tổn thươngngoại trú
        /// </summary>
        [Description("意外伤害门诊")]
        ywshmz = 8,
        /// <summary>
        /// sinh sảnngoại trú
        /// </summary>
        [Description("生育门诊")]
        symz = 9,
        /// <summary>
        /// kháng nhiều laongoại trú
        /// </summary>
        [Description("耐多药结核门诊")]
        ndyjhmz = 10,
        /// <summary>
        /// trẻ em hai bệnhngoại trú
        /// </summary>
        [Description("儿童两病门诊")]
        etlbmz = 11,
        /// <summary>
        /// đơn giảnngoại trú
        /// </summary>
        [Description("简易门诊")]
        jymz = 12
    }

    /// <summary>
    /// Ngoại trú nhập viện dấu hiệu 0 chung 1 chỉ ngoại trú 2 chỉ nhập viện 3 hệ thống(ngoại trú nhập viện đ ều không có thể dùng) ví dụ: giư ờng gia đ ình 
    /// </summary>
    public enum Enummzzybz
    {
        /// <summary>
        /// Chung
        /// </summary>
        [Description("通用")]
        ty = 0,
        /// <summary>
        /// chỉ ngoại trú
        /// </summary>
        [Description("仅门诊")]
        mz = 1,
        /// <summary>
        /// chỉ nhập viện
        /// </summary>
        [Description("仅住院")]
        zy = 2,
        /// <summary>
        /// Hệ thống
        /// </summary>
        [Description("系统")]
        sys = 3
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumSex
    {
        /// <summary>
        /// nam
        /// </summary>
        [Description("男")]
        male = 1,
        /// <summary>
        /// nữ
        /// </summary>
        [Description("女")]
        female = 2,
        /// <summary>
        /// không chi tiết
        /// </summary>
        [Description("不详")]
        other = 3
    }

    /// <summary>
    /// Trạng thái quyết toán
    /// </summary>
    public enum EnumJieSuanZT
    {
        /// <summary>
        /// Đ ã quyết toán(chưa hoàn)
        /// </summary>
        [Description("已结")]
        YJ = 1,
        /// <summary>
        /// Đ ã hoàn
        /// </summary>
        [Description("已退")]
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
        /// Hạng mục
        /// </summary>
        XM,
    }

    /// <summary>
    /// tài chính hóa đ ơn dừng dùng dấu hiệu 
    /// </summary>
    public enum EnumCWFPTY
    {
        /// <summary>
        /// Chưadừng dùng 
        /// </summary>
        WTY = 0,

        /// <summary>
        /// Đ ãdừng dùng 
        /// </summary>
        YTY = 1,
    }
    /// <summary>
    /// Đ ăng ký khámtính chất 
    /// 0-thông thư ờngĐ ăng ký khám
    /// 1- trống Đ ăng ký khám
    /// 2- giư ờng gia đ ình Đ ăng ký khám
    /// 3-tự trả chuyển BHYTĐ ăng ký khám
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
        /// Đ ăng ký khám
        /// </summary>
        [Description("挂号收费")]
        GH = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("家床记账")]
        JCJZ = 1,
        /// <summary>
        /// Ghi sổ ngoại trú
        /// </summary>
        [Description("门诊记账")]
        MZJZ = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("住院")]
        ZY = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("ZHSZ")]
        ZHSZ = 4
    }

    /// <summary>
    /// y lệnh tính chất 1 tạm thời y lệnh 2 y lệnh dài hạn 3 xuất viện y lệnh 
    /// </summary>
    public enum EnumYZXZ
    {
        /// <summary>
        /// tạm thời y lệnh 
        /// </summary>
        LSYZ = 1,
        /// <summary>
        /// y lệnh dài hạn 
        /// </summary>
        CQYZ = 2,
        /// <summary>
        /// Xuất viện y lệnh 
        /// </summary>
        CYYZ = 3
    }


    /// <summary>
    /// y lệnh trạng thái 1 chưa hủy bỏ 2 Đ ã hủy bỏ 
    /// </summary>
    public enum EnumYZZT
    {
        /// <summary>
        /// Chưa hủy bỏ 
        /// </summary>
        WCX = 1,
        /// <summary>
        /// Đ ã hủy bỏ 
        /// </summary>
        YCX = 2
    }

    /// <summary>
    /// Ghi sổ nhập viện hạng mục thu phí loại 
    /// </summary>
    public enum EnumxmType
    {
        XM = 2,//Thuốc
        YP = 1 //Hạng mục
    }

    /// <summary>
    /// ngôn ngữ
    /// </summary>
    public enum Enumlan
    {
        Default = 0,
        Fanti = 1,
        English = 2,
        Jpanese = 3,
    }
    /// <summary>
    /// In phư ơng thức 1:In bù 2:In lại 3:In hóa đ ơn
    /// </summary>
    public enum Enumdyfs
    {
        /// <summary>
        /// In bù
        /// </summary>
        BD = 1,
        /// <summary>
        /// In lại
        /// </summary>
        CD = 2,
        /// <summary>
        /// In hóa đ ơn
        /// </summary>
        Print = 3
    }

    /// <summary>
    /// Loại đ ăng ký khám đ ặc biệt đ ặc biệt V
    /// </summary>
    [Obsolete("不同医疗机构的编码不一样")]
    public enum Enumghlx
    {
        TeXu = 7884,
        TeXuV = 7889,
    }

    /// <summary>
    /// có hiệu lực dấu hiệu 
    /// </summary>
    public enum EnumSXBZ
    {
        /// <summary>
        /// tức là giờcó hiệu lực
        /// </summary>
        [Description("即时生效")]
        Jssx = 0,
        /// <summary>
        /// có hiệu lực tại điểm chỉ định
        /// </summary>
        [Description("定点生效")]
        Ddsx = 1

    }

    /// <summary>
    /// đ ịa lĩnh vực dấu hiệu 
    /// </summary>
    public enum EnumDY
    {
        /// <summary>
        /// Đ ịa phư ơng
        /// </summary>
        [Description("本地")]
        bd = 0,
        /// <summary>
        /// Nơi khác
        /// </summary>
        [Description("外地")]
        wd = 1

    }

    /// <summary>
    /// Đ ã kết hôn chưa
    /// </summary>
    public enum EnumHF
    {
        /// <summary>
        /// Chưa kết hôn
        /// </summary>
        [Description("未婚")]
        wh = 0,
        /// <summary>
        /// Đ ã kết hôn
        /// </summary>
        [Description("已婚")]
        yh,
        /// <summary>
        /// không chi tiết
        /// </summary>
        [Description("不详")]
        UnKnown = 9,
    }

    /// <summary>
    /// Nhập việncon đường
    /// </summary>
    public enum EnumRYTJ
    {
        /// <summary>
        /// Ngoại trú
        /// </summary>
        [Description("普通住院")]
        mz = 21,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("急诊转住院")]
        jz = 24,
        /// <summary>
        /// Khác y tế cơ quan chuyển vào
        /// </summary>
        [Description("转入住院")]
        qtyljg = 9925,
        /// <summary>
        /// chuyển ngoài chẩn trịnhập viện
        /// </summary>
        [Description("转外诊治住院")]
        zwzzzy = 23,
        /// <summary>
        /// bệnh nặng nhập viện
        /// </summary>
        [Description("重大疾病住院")]
        dbry = 9902,
        /// <summary>
        /// sinh sảnnhập viện
        /// </summary>
        [Description("生育住院")]
        syzy = 52,
        /// <summary>
        /// trẻ em hai bệnhnhập viện
        /// </summary>
        [Description("儿童两病住院")]
        etlb = 9904,
        /// <summary>
        /// kháng nhiều laonhập viện
        /// </summary>
        [Description("耐多药结核住院")]
        ndyzy = 9907,
        /// <summary>
        /// trẻ sơ sinh theo mẹnhập viện
        /// </summary>
        [Description("新生儿随母住院")]
        xsesmzy = 9921,
        /// <summary>
        /// 120
        /// </summary>
        [Description("120")]
        jj120,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        Else = 9,
    }

    /// <summary>
    /// Loại giấy tờ
    /// </summary>
    public enum EnumZJLX
    {
        /// <summary>
        /// CMND
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
        qt = 9

    }

    /// <summary>
    /// BHYTĐ ặc biệt chờ đãi ngộ
    /// </summary>
    public enum EnumYBTSDY
    {
        /// <summary>
        /// Thông thư ờng
        /// </summary>
        [Description("普通")]
        pt = 1,
        /// <summary>
        /// nghỉ hưu 
        /// </summary>
        [Description("离休")]
        lx,
        /// <summary>
        /// thương tật
        /// </summary>
        [Description("伤残")]
        sc

    }

    /// <summary>
    /// áp dụng dùng BHYT biện pháp 0:bảo hiểm thành thị 1:cá nhânBHYT 2:thị trấn nhỏ 3:Giờcông nhân
    // A:dân chính y tế giúp khó khăn
    // B:giànămdi chúc C: năm người già được bảo hiểm D:khuyết tật nặng nhân sự 
    // E: trong học sinh tiểu họcvàtrẻ sơ sinh và trẻ nhỏ F: Khác cư dân 
    //G: y tế tương trợ giúp khó khănĐ ối tư ợng
    /// </summary>
    public enum EnumSYYBBF
    {
        /// <summary>
        /// bảo hiểm thành thị
        /// </summary>
        [Description("城保")]
        cb = 1,
        /// <summary>
        /// cá nhânBHYT
        /// </summary>
        [Description("个体医保")]
        gtyb,
        /// <summary>
        /// thị trấn nhỏ
        /// </summary>
        [Description("小城镇")]
        xcz,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        qt = 9

    }

    /// <summary>
    /// Đ ăng ký khám lịch trực thời gian
    /// </summary>
    public enum EnumRegSchedule
    {
        /// <summary>
        /// Không có
        /// </summary>
        [Description("无")]
        none = 0,
        /// <summary>
        /// cảngày
        /// </summary>
        [Description("全天")]
        allday = 1,
        /// <summary>
        /// Buổi sáng
        /// </summary>
        [Description("上午")]
        AM = 2,
        /// <summary>
        /// Buổi chiều
        /// </summary>
        [Description("下午")]
        PM = 3,

    }

    /// <summary>
    /// Kế hoạch ghi sổ thực hiện trạng thái
    /// </summary>
    public enum EnumJzjhZXZT
    {
        /// <summary>
        /// Chưa thực hiện 
        /// </summary>
        [Description("未执行")]
        None = 0,
        /// <summary>
        /// thực hiện trong 
        /// </summary>
        [Description("执行中")]
        Part = 1,
        /// <summary>
        /// Tất cả
        /// </summary>
        [Description("已完成")]
        Finished = 2,
        /// <summary>
        /// Đ ã dừng 
        /// </summary>
        [Description("已停止")]
        Stopped = 3,
    }

    /// <summary>
    /// nhắc nhở loại 
    /// </summary>
    public enum EnmuReminderType
    {
        /// <summary>
        /// Số tiền giới hạn trên nhắc nhở 
        /// </summary>
        [Description("金额上限提醒")]
        金额上限提醒 = 1,    // ví dụ:Chuyên viên trị liệu một tháng trong thu phí không được vượt qua mư ời vạn 
    }

    /// <summary>
    /// Đ ồng bộ điều trị bản ghi xử lý trạng thái
    /// </summary>
    public enum EnumTbzljlClztType
    {
        /// <summary>
        /// ChưaXác nhận
        /// </summary>
        [Description("未确认")]
        WQR = 1,
        /// <summary>
        /// Đ ãxác nhận
        /// </summary>
        [Description("已确认")]
        YQR,
        /// <summary>
        /// Hủy bỏ
        /// </summary>
        [Description("作废")]
        ZF,
    }

    /// <summary>
    /// đ ăng nhập dấu hiệu 
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
    /// Hạng mục thu phíchiến lược định giá
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// theo thời lư ợng
        /// </summary>
        [Description("按时长")]
        Time = 1,
        /// <summary>
        /// theo số lư ợng
        /// </summary>
        [Description("按数量")]
        Amount,
        /// <summary>
        /// theo diện tích
        /// </summary>
        [Description("按面积")]
        Acreage,
    }

    /// <summary>
    /// Ngoại trú bệnh nhân khám bệnh trạng thái(khám bệnh dấu hiệu)
    /// </summary>
    public enum EnumOutpatientJzbz
    {
        /// <summary>
        /// chờ khám bệnh
        /// </summary>
        [Description("待就诊")]
        Djz = 1,
        /// <summary>
        /// Khám bệnh trong 
        /// </summary>
        [Description("就诊中")]
        Jzz,
        /// <summary>
        /// Kết thúcKhám bệnh
        /// </summary>
        [Description("结束就诊")]
        Jsjz,
    }

    /// <summary>
    /// enum loại 
    /// </summary>
    public enum EnumPrescriptionType
    {
        /// <summary>
        /// Thuốc
        /// </summary>
        [Description("药品")]
        Medicine = 1,
        /// <summary>
        /// Hạng mục điều trị
        /// </summary>
        [Description("治疗项目")]
        Treament,
    }

    /// <summary>
    /// tính chất thu chi
    /// </summary>
    public enum EnumSZXZ
    {
        [Description("充值")]
        cz = 1,
        [Description("取款")]
        qk = 2,
        [Description("门诊结算")]
        mzjs = 3,
        [Description("门诊结算退回")]
        mzjsth = 4,
        [Description("住院结算")]
        zyjs = 5,
        [Description("住院结算退回")]
        zyjsth = 6,
        [Description("退余额")]
        tye = 9
    }

    /// <summary>
    /// nguy kịch cấp bậc 
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// Thông thư ờng
        /// </summary>
        [Description("一般")]
        Normal = 1,
        /// <summary>
        /// bệnh nặng
        /// </summary>
        [Description("病重")]
        Serious = 2,
        /// <summary>
        /// bệnh nguy kịch
        /// </summary>
        [Description("病危")]
        Bedying = 3
    }

    public enum EnumOrgshzt
    {
        /// <summary>
        /// kiểm tra thông qua 
        /// </summary>
        [Description("通过")]
        TG = 1,
        /// <summary>
        /// kiểm tra chưa thông qua 
        /// </summary>
        [Description("未通过")]
        WTG = 2,
        /// <summary>
        /// chờ thẩm định
        /// </summary>
        [Description("待审")]
        DS = 3,
        /// <summary>
        /// ChưaGửi
        /// </summary>
        [Description("未提交")]
        WTJ = 4,
        /// <summary>
        /// gửi trước
        /// </summary>
        [Description("预提交")]
        YTJ = 5
    }

    public enum EnumPayStatus
    {
        [Description("支付成功")]
        Success = 1,
        [Description("支付失败")]
        Failed = 2
    }

    public enum EnumRefundStatus
    {
        [Description("退款成功")]
        Success = 1,
        [Description("退款失败")]
        Failed = 2,
        [Description("未知")]
        UnKnown = 3
    }

    public enum EnumTradeType
    {

        /// <summary>
        /// thanh toán Alipay khoản mã 
        /// </summary>
        [Description("支付宝")]
        Alipay_Bar_Code = 1,
        /// <summary>
        /// WeChat Pay khoản mã 
        /// </summary>
        [Description("微信")]
        Wechat_MICROPAY = 2,
    }

    /// <summary>
    /// enum -Tu ổiĐ ơn vị
    /// </summary>
    public enum EnumAgeUnit
    {
        Year = 1,
        Month = 2,
        Day = 3,
        Hour = 4,
    }
    /// <summary>
    /// Ngoại trú đ ặt lịch trạng thái
    /// </summary>
    public enum EnumMzyyzt
    {
        [Description("已约")]
        book = 1,
        [Description("已挂号")]
        reg = 2,
        [Description("预约已取消")]
        cancel = 3,
        [Description("当日挂号")]
        bookreg = 4,
        [Description("取消挂号")]
        regcancel = 5
    }
    /// <summary>
    /// Lịch trực ngoại trú loại 
    /// </summary>
    public enum EnumMzpblx
    {
        [Description("全部")]
        all = 1,
        [Description("开放预约")]
        yy = 2,
        [Description("非预约")]
        fyy = 3
    }
    /// <summary>
    /// mở đ ặt lịch 
    /// </summary>
    public enum EnumMzpbBook
    {
        [Description("是")]
        Y = 1,
        [Description("否")]
        F = 0,
    }
    /// <summary>
    /// Đ ơn thuốc trạng thái
    /// </summary>
    public enum EnumZfzt
    {

        [Description("正常处方")]
        zc = 0,
        [Description("退药或者其他作废处方")]
        ty = 1
    }

    /// <summary>
    /// Cấp cứuTrạng thái
    /// </summary>
    public enum EnumJzzt
    {
        [Description("否")]
        zc = 0,
        [Description("是")]
        ty = 1
    }
    /// <summary>
    /// Đ ăng ký khám ngoại trúnguồn
    /// </summary>
    public enum EnumMzghly
    {
        [Description("His")]
        His = 0,
        [Description("预约")]
        Yy = 1,
        [Description("银医通")]
        Yyt = 3,
        [Description("微信")]
        WeChat = 4,
        [Description("支付宝")]
        Alipay = 5,
        [Description("体检")]
        Tj = 6,
    }
    /// <summary>
    /// BHYT thuộc tính 
    /// </summary>
    public enum EnumCblb
    {
        [Description("全部")]
        all = 0,
        [Description("职工")]
        zg = 1,
        [Description("居民")]
        jm = 2,
        [Description("离休")]
        lx = 3,
        [Description("异地职工")]
        ydzg = 4,
        [Description("异地居民")]
        ydjm = 5,

    }

    public enum EnumRportType
    {
        [Description("职工费用申报表(民政救助)")]
        zgmzjz = 1,
        [Description("居民费用申请表(民政救助)")]
        jmmzjz = 2,
        [Description("普通报账报表(职工)")]
        zgbz = 3,
        [Description("普通报账报表(居民)")]
        jmbz = 4,
        [Description("普通报账报表(离休)")]
        lxbz = 5,
        [Description("普通报账报表(异地职工)")]
        ydzgbz = 6,
        [Description("普通报账报表(异地居民)")]
        ydjmbz = 7,

    }
    /// <summary>
    /// nhân sự loại
    /// </summary>
    public enum EnumRylb
    {
        [Description("职工在职")]
        zgzz = 1101,
        [Description("公务员在职")]
        gwyzz = 1102,
        [Description("灵活就业人员在职")]
        lhjyryzz = 1103,
        [Description("地方其他扩展人员")]
        qtdfkzry = 1160,
        [Description("职工退休")]
        zgtx = 1201,
        [Description("公务员退休")]
        gwytx = 1202,
        [Description("灵活就业人员退休")]
        lhjyrytx = 1203,
        [Description("地方其他扩展人员")]
        qtdfkzrytx = 1260,
        [Description("离休人员")]
        lxry = 1300,
        [Description("地方其他扩展人员")]
        lxdfqtkzry = 1360,
        [Description("新生儿")]
        xse = 1401,
        [Description("学龄前儿童")]
        xlqet = 1402,
        [Description("中小学生")]
        zxxs = 1403,
        [Description("大学生")]
        dxs = 1404,
        [Description("未成年（未入学）")]
        wcn = 1405,
        [Description("地方其他扩展身份")]
        dfqtgkzf = 1460,
        [Description("普通居民（成年）")]
        ptjm = 1501,
        [Description("地方其他扩展身份")]
        jmdfqtgzsf = 1560,
        [Description("居民（老年）")]
        jmln = 16,
        [Description("居民（成年）")]
        jmcn = 15,
        [Description("居民（未成年）")]
        jmwcn = 14,
        [Description("离休")]
        lx = 13,
        [Description("退休人员")]
        txry = 12,
        [Description("在职")]
        zz = 11

    }
    public enum EnumYbQuery
    {
        [Description("【2001】人员待遇享受")]
        rydyxs = 1,
        [Description("【4207】自费病人就医费用明细查询")]
        zfbrjyfymx = 4207,
        [Description("【4208】自费病人就医就诊信息查询")]
        zfbrjyjzxx = 4208,
        [Description("【4209】自费病人就医诊断信息查询")]
        zfbrjyzdxx = 4209,
        [Description("【5201】就诊信息查询")]
        jzxxcx = 2,
        [Description("【5202】诊断信息查询")]
        zdxxcx = 3,
        [Description("【5203】结算信息查询")]
        jsxxcx = 4,
        [Description("【5204】费用明细查询")]
        fymxcx = 5,
        [Description("【5205】人员慢特病用药记录查询")]
        rymtbyyjl = 6,
        [Description("【5206】人员累计信息查询")]
        ryljxxcx = 7,
        [Description("【5301】人员特殊病备案查询")]
        rytsbbacx = 8,
        [Description("【5302】人员定点信息查询")]
        ryddxxcx = 9,
        [Description("【5303】在院信息查询")]
        zyxxcx = 10,
        [Description("【5304】转院信息查询")]
        zyxx = 11,
        [Description("【5401】项目互认信息查询")]
        xmhrxx = 5401,
        [Description("【5402】报告明细信息查询")]
        bgmxxx = 5402,

    }

    public enum EnumYbJGQuery
    {
        [Description("【1201】医药机构信息获取")]
        jgxx = 1201,
        [Description("【3508】定点医药机构商品库存信息查询")]
        yljgspkcxx = 3508,
        [Description("【3509】定点医药机构商品库存变更记录查询")]
        yljgspkcbgjl = 3509,
        [Description("【3510】定点医药机构商品采购信息查询")]
        yljgspcgxx = 3510,
        [Description("【3511】定点医药机构商品销售信息查询")]
        yljgspxsxx = 3511,
        [Description("【3512】定点医药机构入库商品追溯信息查询")]
        yljgrkspzsxx = 3512,
        [Description("【3513】定点医药机构商品销售追溯信息查询")]
        yljgspxszsxx = 3513,
        [Description("【3607】结算清单质控结果查询")]
        jsqdzkjg = 3607,
        [Description("【5101】科室信息查询")]
        ksxxcx = 5101,
    }
    /// <summary>
    /// chờ đãi ngộ kiểm tra loại 
    /// </summary>
    public enum Enumdyjclx
    {
        [Description("基金项检查")]
        jjxjc = 1,
        [Description("其他检查")]
        qtjc = 99
    }
    /// <summary>
    /// quỹ thanh toán loại 
    /// </summary>
    public enum Enumjjzflx
    {
        [Description("城镇职工基本医疗保险统筹基金")]
        tcjj = 310100,
        [Description("城镇职工基本医疗保险个人账户基金")]
        grzhjj = 310200,
        [Description("公务员医疗补助基金")]
        gwybzjj = 320100,
        [Description("大额医疗费用补助基金")]
        debzjj = 330100,
        [Description("离休人员医疗保障基金")]
        lxbzjj = 340100,
        [Description("一至六级残疾军人医疗补助基金")]
        cjjrbzjj = 350100,
        [Description("企业补充医疗保险基金")]
        qybxjj = 370100,
        [Description("医院垫付基金")]
        yydfjj = 999996,
        [Description("区级公务员基金")]
        qgwyjj = 999901,
        [Description("城乡居民基本医疗保险基金")]
        cxjmbxjj = 390100,
        [Description("城乡居民大病医疗保险基金")]
        cxjmdbbxjj = 390200,
        [Description("生育基金")]
        syjj = 510100,
        [Description("职工生育基金")]
        zgsyjj = 310103,
        [Description("医疗救助基金")]
        yljzjj = 610100,
        [Description("医疗再救助基金")]
        ylzjzjj = 610102,
        [Description("健康扶贫基金")]
        jkpfjj = 610103,
        [Description("精准脱贫基金")]
        jztpjj = 610104,
        [Description("其他基金")]
        qtjj = 999997
    }
    /// <summary>
    /// quỹ khoản hạng mục chờ hưởng đãi ngộ dấu hiệu 
    /// </summary>
    public enum Enumdyxsbz
    {
        [Description("否")]
        s = 0,
        [Description("是")]
        f = 1
    }
    /// <summary>
    /// Chẩn đoánLoại
    /// </summary>
    public enum Enumzdlx
    {
        [Description("西医诊断")]
        xyzd = 1,
        [Description("中医主病诊断")]
        zyzzd = 2,
        [Description("中医主证诊断")]
        zyzzzd = 3
    }
    /// <summary>
    /// bảo hiểm chủng loại kiểu 
    /// </summary>
    public enum Enumxzlx
    {
        [Description("职工基本医疗保险")]
        zgjbylbx = 310,
        [Description("公务员医疗补助")]
        gwyylbz = 320,
        [Description("大额医疗费用补助")]
        deylfybz = 330,
        [Description("离休人员医疗保障")]
        lxryylbz = 340,
        [Description("一至六级残废军人医疗补助")]
        cfjrylbz = 350,
        [Description("老红军医疗保障")]
        lhjylbz = 360,
        [Description("企业补充医疗保险")]
        qybcylbx = 370,
        [Description("新型农村合作医疗")]
        xxnchzyl = 380,
        [Description("城乡居民基本医疗保险")]
        cxjmjbylbx = 390,
        [Description("城镇居民基本医疗保险")]
        czjmjbylbx = 391,
        [Description("城乡居民大病医疗保险")]
        cxjmdbbx = 392,
        [Description("其他特殊人员医疗保障")]
        qttsryylbz = 399,
        [Description("长期照护保险")]
        cqzhbx = 410,
        [Description("生育保险")]
        sybx = 510
    }
    /// <summary>
    /// y tế loại
    /// </summary>
    public enum Enumyllb
    {
        [Description("普通门诊")]
        ptmz = 11,
        [Description("门诊挂号")]
        mzgh = 12,
        [Description("急诊")]
        jz = 13,
        [Description("门诊慢特病")]
        mzmtb = 14,
        [Description("意外伤害门诊")]
        ywshmz = 19,
        [Description("普通住院")]
        ptzy = 21,
        [Description("转外诊治住院")]
        zwzzzy = 23,
        [Description("急诊转住院")]
        jzzzy = 24,
        [Description("生育门诊")]
        symz = 51,
        [Description("生育住院")]
        syzy = 52,
        [Description("计划生育手术费")]
        jhsyssf = 53,
        [Description("重大疾病门诊")]
        zdjbmz = 9901,
        [Description("重大疾病住院")]
        zdjbzy = 9902,
        [Description("儿童两病门诊")]
        etlbmz = 9903,
        [Description("儿童两病住院")]
        etlbzy = 9904,
        [Description("耐多药结核门诊")]
        ndyjhmz = 9906,
        [Description("耐多药结核住院")]
        ndyjhzy = 9907,
        [Description("转入住院")]
        zrzy = 9925,
        [Description("新生儿随母住院")]
        xsesmzy = 9921
    }
    /// <summary>
    /// xin nguồn báo cáo
    /// </summary>
    public enum Enumsbly
    {
        [Description("定点医药机构")]
        etlbmz = 01,
        [Description("中心经办系统")]
        etlbzy = 02,
        [Description("网上经办系统")]
        ndyjhmz = 03,
        [Description("APP")]
        ndyjhzy = 04,
        [Description("一体机")]
        zrzy = 05,
        [Description("其他")]
        xsesmzy = 99
    }
    /// <summary>
    /// sinh sảnloại
    /// </summary>
    public enum EnumSylb
    {
        /// <summary>
        /// bình thư ờng sinh
        /// </summary>
        [Description("正常产")]
        Zcc = 1,
        /// <summary>
        /// sinh có hỗ trợ
        /// </summary>
        [Description("助娩产")]
        Zmc = 2,
        /// <summary>
        /// sinh mổ
        /// </summary>
        [Description("剖宫产")]
        Pgc = 3,
        /// <summary>
        /// sinh khó
        /// </summary>
        [Description("难产")]
        nc = 4,
        /// <summary>
        /// sẩy thai
        /// </summary>
        [Description("流产")]
        lc = 5,
        /// <summary>
        /// gây chuyển dạ
        /// </summary>
        [Description("引产")]
        yc = 6,
        /// <summary>
        /// sinh trư ớc kiểm tra 
        /// </summary>
        [Description("产前检查")]
        cqjc = 7,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        qt = 9,
        /// <summary>
        /// tử cung ngoài thai
        /// </summary>
        [Description("宫外孕")]
        gwc = 10,
        /// <summary>
        /// di truyền xét nghiệm gen
        /// </summary>
        [Description("遗传基因检测")]
        ycjyjc = 11,
        /// <summary>
        /// bốn tháng trở lên sẩy thai và gây chuyển dạ
        /// </summary>
        [Description("四个月以上流产")]
        sgyyslc = 12,
        /// <summary>
        /// bốn tháng trở xuống sẩy thai
        /// </summary>
        [Description("四个月以下流产")]
        sgyyxlc = 13
    }
    /// <summary>
    /// kế hoạch sinh sản phẫu thuật loại
    /// </summary>
    public enum EnumSysslb
    {
        /// <summary>
        /// đặt dụng cụ tử cung trong mục nuôi dưỡng thiết bị 
        /// </summary>
        [Description("放置宫内节育器")]
        fzgnjyq = 1,
        /// <summary>
        /// lấy dụng cụ tử cung trong mục nuôi dưỡng thiết bị 
        /// </summary>
        [Description("取出宫内节育器")]
        qcgnjyq = 2,
        /// <summary>
        /// phẫu thuật sẩy thai
        /// </summary>
        [Description("流产术")]
        lcs = 3,
        /// <summary>
        /// phẫu thuật gây chuyển dạ
        /// </summary>
        [Description("引产术")]
        ycs = 4,
        /// <summary>
        /// triệt sản phẫu thuật 
        /// </summary>
        [Description("绝育手术")]
        jyss = 5,
        /// <summary>
        /// phục hồi triệt sản phẫu thuật 
        /// </summary>
        [Description("绝育复通手术")]
        jyftss = 6,
        /// <summary>
        /// triệt sản phẫu thuật (ống dẫn tinh)
        /// </summary>
        [Description("绝育手术（输精管）")]
        jysssjg = 7,
        /// <summary>
        /// phục hồi triệt sản phẫu thuật (ống dẫn tinh)
        /// </summary>
        [Description("绝育复通手术（输精管）")]
        jyftsssjg = 8,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        qt = 9,
        /// <summary>
        /// da dư ới phẫu thuật cấy ghép
        /// </summary>
        [Description("皮下埋植术")]
        pxmzs = 10,
        /// <summary>
        /// Hủyda dư ới phẫu thuật cấy ghép
        /// </summary>
        [Description("取消皮下埋植术")]
        qxpxmzs = 11,
        /// <summary>
        /// phẫu thuật thắt ống dẫn trứng
        /// </summary>
        [Description("输卵管结扎术")]
        srgjzs = 12
    }
    /// <summary>
    /// muộn sinh dấu hiệu / sớm sinh dấu hiệu 
    /// </summary>
    public enum EnumWybz
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
    /// Bác sĩ loại 
    /// </summary>
    public enum EnumYslx
    {
        [Description("住院医生")] //Resident
        ZyDoc = 1,
        [Description("主治医生")] //Attending
        ZzDoc = 2,
        [Description("主任医生")] //chief
        ZrDoc = 3
    }
    /// <summary>
    /// nơi khác dấu hiệu 
    /// </summary>
    public enum EnumYd
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
    /// Tính chất bệnh nhân
    /// </summary>
    public enum EnumBrxz
    {
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("自费")]
        zf = 0,
        /// <summary>
        /// nhân viênBHYT
        /// </summary>
        [Description("职工医保")]
        zg = 11,
        /// <summary>
        /// cư dân BHYT
        /// </summary>
        [Description("居民医保")]
        jm = 12,
        /// <summary>
        /// nghỉ hưu 
        /// </summary>
        [Description("离休")]
        lx = 13,
        /// <summary>
        /// BHYT thông thư ờng
        /// </summary>
        [Description("普通医保")]
        pt = 1,
    }
    /// <summary>
    /// đ ơn hàng trạng thái0: chờ thanh toán 1:Đ ã thanh toán 2::Đ ã hoàn khoản 3: thanh toán trong 
    /// </summary>
    public enum EnumOrderStatus
    {
        /// <summary>
        /// chờ thanh toán 
        /// </summary>
        [Description("待支付")]
        dzf = 0,
        /// <summary>
        /// Đ ã thanh toán 
        /// </summary>
        [Description("已支付")]
        yzf = 1,
        /// <summary>
        /// Đ ã hoàn khoản 
        /// </summary>
        [Description("已退款")]
        ytk = 2,
        /// <summary>
        /// thanh toán trong 
        /// </summary>
        [Description("支付中")]
        zfz = 3,
    }
    /// <summary>
    /// giao dịch loại thẻ
    /// </summary>
    public enum EnumJykxzlx
    {
        /// <summary>
        /// Tự trả
        /// </summary>
        [Description("自费")]
        zf = 0,
        /// <summary>
        /// BHYT
        /// </summary>
        [Description("医保")]
        yb = 1
    }
}
