using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class ClientEnum
    {

    }
    public class SettClientEnum : ClientEnum
    {
        /// <summary>
        /// loại ghi nhật ký mở rộng hạng mục enum
        /// Valuevui lòng từ101bắt đầu
        /// </summary>
        public enum DbLogType_Ex
        {
            /// <summary>
            /// thanh toán xuất viện
            /// </summary>
            [Description("出院结算")]
            DischargeSettlement = 101,

        }

        /// <summary>
        /// cờ đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// đăng ký nhập viện
            /// </summary>
            [Description("新入院")]
            Xry = 0,
            /// <summary>
            /// trong khu bệnh
            /// </summary>
            [Description("病区中")]
            Bqz = 1,
            /// <summary>
            /// xuất viện khỏi khu bệnh（rời khu bệnh）（chờ thanh toán）
            /// </summary>
            [Description("待结账")]
            Djz = 2,
            /// <summary>
            /// đã xuất viện（thanh toán xuất viện）
            /// </summary>
            [Description("已出院")]
            Ycy = 3,
            /// <summary>
            /// đang chuyển khu
            /// </summary>
            [Description("转区中")]
            Zq = 7,

            /// <summary>
            /// bản ghi hủy/hủy nhập viện
            /// </summary>
            [Description("作废记录")]
            Wry = 9,
        }

        /// <summary>
        /// loại tài khoản hệ thống
        /// </summary>
        public enum EnumXTZHXZ
        {
            /// <summary>
            /// tài khoản đơn vị
            /// </summary>
            [Description("单位帐户")]
            DWZH = 0,
            /// <summary>
            /// tài khoản cá nhân
            /// </summary>
            [Description("个人帐户")]
            GRZH = 1,
            /// <summary>
            /// tài khoản giường tại nhà
            /// </summary>
            [Description("家床账户")]
            JCZH = 2,
            /// <summary>
            /// tài khoản tạm ứng nội trú
            /// </summary>
            [Description("住院预缴款账户")]
            ZYYJKZH = 3,
            /// <summary>
            /// tài khoản hỗ trợ khó khăn
            /// </summary>
            [Description("帮困账户")]
            BKZH = 4,
            /// <summary>
            /// tài khoản đăng ký ngoại trú
            /// </summary>
            [Description("门诊登记账户")]
            MZDJZH = 5,
            /// <summary>
            /// tài khoản tạm ứng ngoại trú
            /// </summary>
            [Description("门诊预缴款账户")]
            MZYJKZH = 6
        }

        /// <summary>
        /// loại giao dịch bảo hiểm y tế  0 không giao dịch 1 giao dịch thông thường 2 giao dịch bệnh nặng 3 giao dịch giường tại nhà 4 giao dịch tai nạn lao động 5 giao dịch nội trú 6 giao dịch bảo hiểm nông nghiệp
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
            /// giao dịch thông thường
            /// </summary>
            [Description("普通交易")]
            ybjylx1 = 1,
            /// <summary>
            /// giao dịch bệnh nặng
            /// </summary>
            [Description("大病交易")]
            ybjylx2 = 2,
            /// <summary>
            /// giao dịch giường tại nhà
            /// </summary>
            [Description("家床交易")]
            ybjylx3 = 3,
            /// <summary>
            /// giao dịch tai nạn lao động
            /// </summary>
            [Description("工伤交易")]
            ybjylx4 = 4,
            /// <summary>
            /// giao dịch nội trú
            /// </summary>
            [Description("住院交易")]
            ybjylx5 = 5,
            /// <summary>
            /// giao dịch bảo hiểm nông nghiệp
            /// </summary>
            [Description("农保交易")]
            ybjylx6 = 6,
        }

        /// <summary>
        /// tính chất tự trả（thuốc、thuộc tính từ điển dự án）
        /// </summary>
        public enum EnumZiFuXingZhi
        {
            /// <summary>
            /// có thể thanh toán bảo hiểm
            /// </summary>
            [Description("可报")]
            KB = 0,
            /// <summary>
            /// tự trả
            /// </summary>
            [Description("自费")]
            ZF = 1,
            /// <summary>
            /// tự trả theo phân loại
            /// </summary>
            [Description("分类自负")]
            FLZF = 2,
            /// <summary>
            /// hoàn toàn tự lo
            /// </summary>
            [Description("绝对自理")]
            JueDuiZiLi = 3,
            /// <summary>
            /// loại A
            /// </summary>
            [Description("甲类")]
            Jia,
            /// <summary>
            /// loại B
            /// </summary>
            [Description("乙类")]
            Yi,
            /// <summary>
            /// loại C
            /// </summary>
            [Description("丙类")]
            Bing,
        }

        /// <summary>
        /// thuật toán tính chất tự trả
        /// xác định tính chất của phần tự trả tính theo tỷ lệ tự trả
        /// </summary>
        public enum EnumSuanFaZFXZ
        {
            /// <summary>
            /// tự trả（tiền mặt tự trả sau giao dịch bảo hiểm y tế tương tự）
            /// </summary>
            [Description("自负")]
            ZF = 0,
            /// <summary>
            /// tự lo
            /// </summary>
            [Description("自理")]
            ZL = 1,
        }

        /// <summary>
        /// tính chất thu chi tài khoản 
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
        /// loại thẻ/định danh danh tính thẻ
        /// </summary>
        [Description("卡类型")]
        public enum EnumCardType
        {
            /// <summary>
            /// thẻ từ bảo hiểm y tế
            /// </summary>
            [Description("医保磁条卡")]
            CTK = 0,
            /// <summary>
            /// thẻ ảo/thẻ trong viện（thẻ ảo khi tự động tạo/thẻ trong viện khi nhập thủ công）
            /// </summary>
            [Description("虚拟卡")]
            XNK = 1,
            /// <summary>
            /// thẻ bảo hiểm xã hội
            /// </summary>
            [Description("社保卡")]
            YBJYK = 2,
            /// <summary>
            /// chứng từ điện tử bảo hiểm y tế
            /// </summary>
            [Description("医保电子凭证")]
            ybdzpz = 3,
            /// <summary>
            /// khám bảo hiểm y tế bằng chứng minh nhân dân
            /// </summary>
            [Description("身份证(医保)")]
            sfz = 4,
            /// <summary>
            /// mã sức khỏe
            /// </summary>
            [Description("健康码")]
            XNHJYK = 5,
        }

        /// <summary>
        /// loại ngoại trú mjzbz
        /// </summary>
        public enum EnumOutPatientType
        {
            /// <summary>
            /// khám ngoại trú thông thường
            /// </summary>
            [Description("普通门诊")]
            generalOutpat = 1,
            /// <summary>
            /// cấp cứu
            /// </summary>
            [Description("急诊")]
            emerDiagnosis = 2,
            /// <summary>
            /// khám chuyên gia
            /// </summary>
            [Description("专家门诊")]
            expertOutpat = 3,
            /// <summary>
            /// khám ngoại trú bệnh đặc biệt
            /// </summary>
            [Description("特病门诊")]
            SpecialOutpat = 4,
            /// <summary>
            /// ngoại trú bệnh nặng
            /// </summary>
            [Description("重大疾病门诊")]
            MajorDiseases = 5,
            /// <summary>
            /// khám ngoại trú bệnh mạn tính
            /// </summary>
            [Description("慢性病门诊")]
            ChronicDisease = 6,
            /// <summary>
            /// hai bệnh ở cư dân
            /// </summary>
            [Description("居民两病")]
            jmlb = 7,
            /// <summary>
            /// khám ngoại trú chấn thương ngoài ý muốn
            /// </summary>
            [Description("意外伤害门诊")]
            ywshmz = 8,
            /// <summary>
            /// ngoại trú sinh đẻ
            /// </summary>
            [Description("生育门诊")]
            symz = 9,
            /// <summary>
            /// ngoại trú lao kháng nhiều thuốc
            /// </summary>
            [Description("耐多药结核门诊")]
            ndyjhmz = 10,
            /// <summary>
            /// ngoại trú hai bệnh ở trẻ em
            /// </summary>
            [Description("儿童两病门诊")]
            etlbmz = 11
        }

        /// <summary>
        /// cờ ngoại trú – nội trú  0 chung 1 chỉ ngoại trú 2 chỉ nội trú 3 hệ thống（cả ngoại trú và nội trú đều không dùng được）Ví dụ：giường tại nhà
        /// có định nghĩa trùng lặp
        /// </summary>
        public enum Enummzzybz
        {
            /// <summary>
            /// chung
            /// </summary>
            [Description("通用")]
            ty = 0,
            /// <summary>
            /// chỉ ngoại trú
            /// </summary>
            [Description("仅门诊")]
            mz = 1,
            /// <summary>
            /// chỉ nội trú
            /// </summary>
            [Description("仅住院")]
            zy = 2,
            /// <summary>
            /// hệ thống
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
            /// không rõ
            /// </summary>
            [Description("不详")]
            other = 3
        }

        /// <summary>
        /// trạng thái thanh toán
        /// </summary>
        public enum EnumJieSuanZT
        {
            /// <summary>
            /// đã kết toán（chưa trả lại）
            /// </summary>
            [Description("已结")]
            YJ = 1,
            /// <summary>
            /// đã trả lại
            /// </summary>
            [Description("已退")]
            YT,
        }

        /// <summary>
        /// tính chất y lệnh
        /// </summary>
        public enum EnumYiZhuXZ
        {
            /// <summary>
            /// thuốc
            /// </summary>
            YP = 1,
            /// <summary>
            /// hạng mục
            /// </summary>
            XM,
        }

        /// <summary>
        /// cờ ngừng sử dụng hóa đơn tài chính
        /// </summary>
        public enum EnumCWFPTY
        {
            /// <summary>
            /// chưa ngừng sử dụng
            /// </summary>
            WTY = 0,

            /// <summary>
            /// đã ngừng sử dụng
            /// </summary>
            YTY = 1,
        }
        /// <summary>
        /// tính chất đăng ký khám 
        /// 0-đăng ký khám thông thường
        /// 1-đăng ký khám trống
        /// 2-đăng ký khám giường tại nhà
        /// 3-đăng ký khám chuyển từ tự trả sang bảo hiểm y tế
        /// 4-đăng ký khám ghi sổ ngoại trú
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
        /// loại thanh toán
        /// </summary>
        public enum EnumJslx
        {
            /// <summary>
            /// đăng ký khám
            /// </summary>
            [Description("挂号收费")]
            GH = 0,
            /// <summary>
            /// 
            /// </summary>
            [Description("家床记账")]
            JCJZ = 1,
            /// <summary>
            /// ghi sổ ngoại trú
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

        ///// <summary>
        /////tính chất y lệnh 1 y lệnh tạm thời   2 y lệnh dài hạn   3 y lệnh xuất viện
        ///// </summary>
        //public enum EnumYZXZ
        //{
        //    /// <summary>
        //    /// y lệnh tạm thời
        //    /// </summary>
        //    LSYZ = 1,
        //    /// <summary>
        //    /// y lệnh dài hạn
        //    /// </summary>
        //    CQYZ = 2,
        //    /// <summary>
        //    /// y lệnh xuất viện
        //    /// </summary>
        //    CYYZ = 3
        //}


        /// <summary>
        ///trạng thái y lệnh 1 chưa thu hồi   2 đã thu hồi
        /// </summary>
        public enum EnumYZZT
        {
            /// <summary>
            /// chưa thu hồi
            /// </summary>
            WCX = 1,
            /// <summary>
            /// đã thu hồi
            /// </summary>
            YCX = 2
        }

        /// <summary>
        /// loại hạng mục thu phí ghi sổ nội trú
        /// </summary>
        public enum EnumxmType
        {
            XM = 2,//thuốc
            YP = 1 //hạng mục
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
        /// phương thức in 1:in bổ sung  2:in lại 3:in hóa đơn
        /// </summary>
        public enum Enumdyfs
        {
            /// <summary>
            /// in bổ sung
            /// </summary>
            BD = 1,
            /// <summary>
            /// in lại
            /// </summary>
            CD = 2,
            /// <summary>
            /// in hóa đơn
            /// </summary>
            Print = 3
        }

        /// <summary>
        /// loại đăng ký khám đặc biệt đặc biệtV
        /// </summary>
        [Obsolete("不同医疗机构的编码不一样")]
        public enum Enumghlx
        {
            TeXu = 7884,
            TeXuV = 7889,
        }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        public enum EnumSXBZ
        {
            /// <summary>
            /// có hiệu lực ngay
            /// </summary>
            [Description("即时生效")]
            Jssx = 0,
            /// <summary>
            /// có hiệu lực tại điểm cố định
            /// </summary>
            [Description("定点生效")]
            Ddsx = 1

        }

        /// <summary>
        /// cờ khu vực
        /// </summary>
        public enum EnumDY
        {
            /// <summary>
            /// địa phương
            /// </summary>
            [Description("本地")]
            bd = 0,
            /// <summary>
            /// nơi khác
            /// </summary>
            [Description("外地")]
            wd = 1

        }

        /// <summary>
        /// đã kết hôn hay chưa
        /// </summary>
        public enum EnumHF
        {
            /// <summary>
            /// chưa kết hôn
            /// </summary>
            [Description("未婚")]
            wh = 0,
            /// <summary>
            /// đã kết hôn
            /// </summary>
            [Description("已婚")]
            yh,
            /// <summary>
            /// không rõ
            /// </summary>
            [Description("不详")]
            UnKnown = 9,
        }

        /// <summary>
        /// đường nhập viện
        /// </summary>
        public enum EnumRYTJ
        {
            /// <summary>
            /// khám ngoại trú
            /// </summary>
            [Description("普通住院")]
            mz = 21,
            /// <summary>
            /// cấp cứu
            /// </summary>
            [Description("急诊转住院")]
            jz = 24,
            /// <summary>
            /// chuyển đến từ cơ sở y tế khác
            /// </summary>
            [Description("转入住院")]
            qtyljg = 9925,
            /// <summary>
            /// nội trú điều trị chuyển ra ngoài
            /// </summary>
            [Description("转外诊治住院")]
            zwzzzy = 23,
            /// <summary>
            /// nội trú bệnh nặng
            /// </summary>
            [Description("重大疾病住院")]
            dbry = 9902,
            /// <summary>
            /// nội trú sinh đẻ
            /// </summary>
            [Description("生育住院")]
            syzy = 52,
            /// <summary>
            /// nội trú hai bệnh ở trẻ em
            /// </summary>
            [Description("儿童两病住院")]
            etlb = 9904,
            /// <summary>
            /// nội trú lao kháng nhiều thuốc
            /// </summary>
            [Description("耐多药结核住院")]
            ndyzy = 9907,
            /// <summary>
            /// trẻ sơ sinh nằm viện cùng mẹ
            /// </summary>
            [Description("新生儿随母住院")]
            xsesmzy = 9921,
            /// <summary>
            /// 120
            /// </summary>
            [Description("120")]
            jj120,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            Else = 9,
        }

        /// <summary>
        /// loại giấy tờ
        /// </summary>
        public enum EnumZJLX
        {
            /// <summary>
            /// chứng minh nhân dân
            /// </summary>
            [Description("身份证")]
            sfz = 1,
            /// <summary>
            /// hộ chiếu
            /// </summary>
            [Description("护照")]
            hz,
            /// <summary>
            /// giấy chứng nhận sĩ quan
            /// </summary>
            [Description("军官证")]
            jgz,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            qt = 9

        }

        /// <summary>
        /// đãi ngộ đặc biệt bảo hiểm y tế
        /// </summary>
        public enum EnumYBTSDY
        {
            /// <summary>
            /// thông thường
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
        /// phương pháp bảo hiểm y tế áp dụng 0：bảo hiểm thành phố 1：bảo hiểm y tế cá nhân 2：thị trấn nhỏ 3：công nhân theo giờ
        // A:hỗ trợ khó khăn y tế dân chính
        // B：di chúc người già C:người già được bảo trợ năm mặt D：người tàn tật nặng
        // E:học sinh tiểu học, trung học và trẻ sơ sinh F： cư dân khác
        //G：đối tượng hỗ trợ khó khăn tương trợ y tế
        /// </summary>
        public enum EnumSYYBBF
        {
            /// <summary>
            /// bảo hiểm thành phố
            /// </summary>
            [Description("城保")]
            cb = 1,
            /// <summary>
            /// bảo hiểm y tế cá nhân
            /// </summary>
            [Description("个体医保")]
            gtyb,
            /// <summary>
            /// thị trấn nhỏ
            /// </summary>
            [Description("小城镇")]
            xcz,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            qt = 9

        }

        /// <summary>
        /// thời gian lịch đăng ký khám
        /// </summary>
        public enum EnumRegSchedule
        {
            /// <summary>
            /// không có
            /// </summary>
            [Description("无")]
            none = 0,
            /// <summary>
            /// cả ngày
            /// </summary>
            [Description("全天")]
            allday = 1,
            /// <summary>
            /// buổi sáng
            /// </summary>
            [Description("上午")]
            AM = 2,
            /// <summary>
            /// buổi chiều
            /// </summary>
            [Description("下午")]
            PM = 3,

        }

        /// <summary>
        /// trạng thái thực hiện kế hoạch ghi sổ
        /// </summary>
        public enum EnumJzjhZXZT
        {
            /// <summary>
            /// chưa thực hiện
            /// </summary>
            [Description("未执行")]
            None = 0,
            /// <summary>
            /// đang thực hiện
            /// </summary>
            [Description("执行中")]
            Part = 1,
            /// <summary>
            /// tất cả
            /// </summary>
            [Description("已完成")]
            Finished = 2,
            /// <summary>
            /// đã dừng
            /// </summary>
            [Description("已停止")]
            Stopped = 3,
        }

        /// <summary>
        /// loại nhắc nhở
        /// </summary>
        public enum EnmuReminderType
        {
            /// <summary>
            /// nhắc nhở giới hạn trên số tiền
            /// </summary>
            [Description("金额上限提醒")]
            金额上限提醒 = 1,    //Ví dụ：nhà trị liệu trong một tháng thu phí không được vượt quá một trăm nghìn
        }

        /// <summary>
        /// đồng bộ hồ sơ điều trị trạng thái xử lý
        /// </summary>
        public enum EnumTbzljlClztType
        {
            /// <summary>
            /// chưa xác nhận
            /// </summary>
            [Description("未确认")]
            WQR = 1,
            /// <summary>
            /// đã xác nhận
            /// </summary>
            [Description("已确认")]
            YQR,
            /// <summary>
            /// hủy
            /// </summary>
            [Description("作废")]
            ZF,
        }

        /// <summary>
        /// cờ đăng nhập
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
        /// chiến lược định giá hạng mục thu phí
        /// </summary>
        public enum EnumSfxmJjcl
        {
            /// <summary>
            /// theo thời lượng
            /// </summary>
            [Description("按时长")]
            Time = 1,
            /// <summary>
            /// theo số lượng
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
        /// trạng thái khám bệnh của bệnh nhân ngoại trú（cờ khám bệnh）
        /// </summary>
        public enum EnumOutpatientJzbz
        {
            /// <summary>
            /// chờ khám
            /// </summary>
            [Description("待就诊")]
            Djz = 1,
            /// <summary>
            /// đang khám
            /// </summary>
            [Description("就诊中")]
            Jzz,
            /// <summary>
            /// kết thúc khám bệnh
            /// </summary>
            [Description("结束就诊")]
            Jsjz,
        }

        /// <summary>
        /// kiểu enum
        /// </summary>
        public enum EnumPrescriptionType
        {
            /// <summary>
            /// thuốc
            /// </summary>
            [Description("药品")]
            Medicine = 1,
            /// <summary>
            /// hạng mục điều trị
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
        /// mức nguy kịch
        /// </summary>
        public enum EnumWzjb
        {
            /// <summary>
            /// chung
            /// </summary>
            [Description("一般")]
            Normal = 1,
            /// <summary>
            /// bệnh nặng
            /// </summary>
            [Description("病重")]
            Serious = 2,
            /// <summary>
            /// nguy kịch
            /// </summary>
            [Description("病危")]
            Bedying = 3
        }

        public enum EnumOrgshzt
        {
            /// <summary>
            /// duyệt đạt
            /// </summary>
            [Description("通过")]
            TG = 1,
            /// <summary>
            /// duyệt chưa đạt
            /// </summary>
            [Description("未通过")]
            WTG = 2,
            /// <summary>
            /// chờ duyệt
            /// </summary>
            [Description("待审")]
            DS = 3,
            /// <summary>
            /// chưa nộp
            /// </summary>
            [Description("未提交")]
            WTJ = 4,
            /// <summary>
            /// nộp trước
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
            /// mã thanh toán Alipay
            /// </summary>
            [Description("支付宝")]
            Alipay_Bar_Code = 1,
            /// <summary>
            /// mã thanh toán WeChat
            /// </summary>
            [Description("微信")]
            Wechat_MICROPAY = 2,
        }

        /// <summary>
        /// enum-đơn vị tuổi
        /// </summary>
        public enum EnumAgeUnit
        {
            Year = 1,
            Month = 2,
            Day = 3,
            Hour = 4,
        }
        /// <summary>
        /// trạng thái hẹn ngoại trú
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
        /// loại lịch ngoại trú
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
        /// mở hẹn
        /// </summary>
        public enum EnumMzpbBook
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            F = 0,
        }
        /// <summary>
        /// trạng thái đơn thuốc
        /// </summary>
        public enum EnumZfzt
        {

            [Description("正常处方")]
            zc = 0,
            [Description("退药或者其他作废处方")]
            ty = 1
        }

        /// <summary>
        /// trạng thái cấp cứu
        /// gốcEnumJzzt
        /// </summary>
        public enum EnumEJzzt
        {
            [Description("否")]
            zc = 0,
            [Description("是")]
            ty = 1
        }
        /// <summary>
        /// nguồn đăng ký khám ngoại trú
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
            [Description("自助机")]
            SelfTerminal = 7,
        }
        /// <summary>
        /// thuộc tính bảo hiểm y tế
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
        /// loại nhân viên
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
            [Description("人员待遇享受")]
            rydyxs = 1,
            [Description("就诊信息查询")]
            jzxxcx = 2,
            [Description("诊断信息查询")]
            zdxxcx = 3,
            [Description("结算信息查询")]
            jsxxcx = 4,
            [Description("费用明细查询")]
            fymxcx = 5,
            [Description("人员慢特病用药记录查询")]
            rymtbyyjl = 6,
            [Description("人员累计信息查询")]
            ryljxxcx = 7,
            [Description("人员特殊病备案查询")]
            rytsbbacx = 8,
            [Description("人员定点信息查询")]
            ryddxxcx = 9,
            [Description("在院信息查询")]
            zyxxcx = 10,
            [Description("转院信息查询")]
            zyxx = 11,

        }
        /// <summary>
        /// loại kiểm tra đãi ngộ
        /// </summary>
        public enum Enumdyjclx
        {
            [Description("基金项检查")]
            jjxjc = 1,
            [Description("其他检查")]
            qtjc = 99
        }
        /// <summary>
        /// loại thanh toán quỹ
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
        /// cờ hưởng đãi ngộ khoản quỹ
        /// </summary>
        public enum Enumdyxsbz
        {
            [Description("否")]
            s = 0,
            [Description("是")]
            f = 1
        }
        /// <summary>
        /// loại chẩn đoán
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
        /// loại bảo hiểm
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
        /// loại y tế
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
        /// nguồn khai báo
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
        /// loại sinh đẻ
        /// </summary>
        public enum EnumSylb
        {
            /// <summary>
            /// sinh thường
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
            /// khó sinh
            /// </summary>
            [Description("难产")]
            nc = 4,
            /// <summary>
            /// sảy thai
            /// </summary>
            [Description("流产")]
            lc = 5,
            /// <summary>
            /// gây chuyển dạ
            /// </summary>
            [Description("引产")]
            yc = 6,
            /// <summary>
            /// kiểm tra trước sinh
            /// </summary>
            [Description("产前检查")]
            cqjc = 7,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            qt = 9,
            /// <summary>
            /// mang thai ngoài tử cung
            /// </summary>
            [Description("宫外孕")]
            gwc = 10,
            /// <summary>
            /// xét nghiệm gen di truyền
            /// </summary>
            [Description("遗传基因检测")]
            ycjyjc = 11,
            /// <summary>
            /// nạo phá thai sau bốn tháng
            /// </summary>
            [Description("四个月以上流产")]
            sgyyslc = 12,
            /// <summary>
            /// sảy thai dưới bốn tháng
            /// </summary>
            [Description("四个月以下流产")]
            sgyyxlc = 13
        }
        /// <summary>
        /// loại phẫu thuật kế hoạch hóa gia đình
        /// </summary>
        public enum EnumSysslb
        {
            /// <summary>
            /// đặt dụng cụ tránh thai trong tử cung
            /// </summary>
            [Description("放置宫内节育器")]
            fzgnjyq = 1,
            /// <summary>
            /// lấy dụng cụ tránh thai trong tử cung ra
            /// </summary>
            [Description("取出宫内节育器")]
            qcgnjyq = 2,
            /// <summary>
            /// thủ thuật sảy thai
            /// </summary>
            [Description("流产术")]
            lcs = 3,
            /// <summary>
            /// thủ thuật gây chuyển dạ
            /// </summary>
            [Description("引产术")]
            ycs = 4,
            /// <summary>
            /// phẫu thuật triệt sản
            /// </summary>
            [Description("绝育手术")]
            jyss = 5,
            /// <summary>
            /// phẫu thuật tái thông triệt sản
            /// </summary>
            [Description("绝育复通手术")]
            jyftss = 6,
            /// <summary>
            /// phẫu thuật triệt sản（ống dẫn tinh）
            /// </summary>
            [Description("绝育手术（输精管）")]
            jysssjg = 7,
            /// <summary>
            /// phẫu thuật tái thông triệt sản（ống dẫn tinh）
            /// </summary>
            [Description("绝育复通手术（输精管）")]
            jyftsssjg = 8,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            qt = 9,
            /// <summary>
            /// thủ thuật cấy dưới da
            /// </summary>
            [Description("皮下埋植术")]
            pxmzs = 10,
            /// <summary>
            /// hủy thủ thuật cấy dưới da
            /// </summary>
            [Description("取消皮下埋植术")]
            qxpxmzs = 11,
            /// <summary>
            /// thủ thuật thắt ống dẫn trứng
            /// </summary>
            [Description("输卵管结扎术")]
            srgjzs = 12
        }
        /// <summary>
        /// cờ sinh muộn/cờ sinh non
        /// </summary>
        public enum EnumWybz
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("否")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("是")]
            s = 1,
        }
        /// <summary>
        /// loại bác sĩ
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
        /// định danh khác địa phương
        /// </summary>
        public enum EnumYd
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("否")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("是")]
            s = 1,
        }
        /// <summary>
        /// tính chất bệnh nhân
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// tự trả
            /// </summary>
            [Description("自费")]
            zf = 0,
            /// <summary>
            /// bảo hiểm y tế nhân viên
            /// </summary>
            [Description("职工医保")]
            zg = 1,
            /// <summary>
            /// bảo hiểm y tế cư dân
            /// </summary>
            [Description("居民医保")]
            jm = 2,
            /// <summary>
            /// nghỉ hưu
            /// </summary>
            [Description("离休")]
            lx = 3,
            /// <summary>
            /// bảo hiểm y tế thông thường
            /// </summary>
            [Description("普通医保")]
            pt = 11,
        }
        /// <summary>
        /// trạng thái đơn hàng0:chờ thanh toán 1:đang thanh toán  2:đã thanh toán 3：đã hoàn tiền 4:đã hủy
        /// </summary>
        public enum EnumOrderStatus
        {
            /// <summary>
            /// chờ thanh toán
            /// </summary>
            [Description("待支付")]
            dzf = 0,
            /// <summary>
            /// đang thanh toán
            /// </summary>
            [Description("支付中")]
            zfz = 1,
            /// <summary>
            /// đã thanh toán
            /// </summary>
            [Description("已支付")]
            yzf = 2,
            /// <summary>
            /// đã hoàn tiền
            /// </summary>
            [Description("已退款")]
            ytk = 3,
            /// <summary>
            /// đã hủy
            /// </summary>
            [Description("已作废")]
            zf = 4,
        }
        /// <summary>
        /// loại thẻ giao dịch
        /// </summary>
        public enum EnumJykxzlx
        {
            /// <summary>
            /// tự trả
            /// </summary>
            [Description("自费")]
            zf = 0,
            /// <summary>
            /// bảo hiểm y tế
            /// </summary>
            [Description("医保")]
            yb = 1
        }
        /// <summary>
        /// loại nghiệp vụ đơn hàng
        /// </summary>
        public enum EnumOrderType
        {
            /// <summary>
            /// khám ngoại trú
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// nội trú
            /// </summary>
            [Description("住院")]
            zy = 2
        }
    }

    public class CisClientEnum : ClientEnum
    {
        /// <summary>
        /// loại giấy tờ
        /// </summary>
        public enum EnumZjlx
        {
            /// <summary>
            /// chứng minh nhân dân
            /// </summary>
            [Description("身份证")]
            sfz = 1,
            /// <summary>
            /// hộ chiếu
            /// </summary>
            [Description("护照")]
            hz,
            /// <summary>
            /// giấy chứng nhận sĩ quan
            /// </summary>
            [Description("军官证")]
            jgz,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            other = 9
        }

        /// <summary>
        /// tính chất tự trả（thuốc、thuộc tính từ điển dự án）
        /// </summary>
        public enum EnumZiFuXingZhi
        {
            /// <summary>
            /// có thể thanh toán bảo hiểm
            /// </summary>
            [Description("可报")]
            KB = 0,
            /// <summary>
            /// tự trả
            /// </summary>
            [Description("自费")]
            ZF = 1,
            /// <summary>
            /// tự trả theo phân loại
            /// </summary>
            [Description("分类自负")]
            FLZF = 2,
            /// <summary>
            /// hoàn toàn tự lo
            /// </summary>
            [Description("绝对自理")]
            JueDuiZiLi = 3,
            /// <summary>
            /// loại A
            /// </summary>
            [Description("甲类")]
            Jia,
            /// <summary>
            /// loại B
            /// </summary>
            [Description("乙类")]
            Yi,
            /// <summary>
            /// loại C
            /// </summary>
            [Description("丙类")]
            Bing,
        }

        /// <summary>
        /// loại thuốc
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
        /// cấp kháng sinh
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
        /// trạng thái khám bệnh
        /// </summary>
        public enum EnumJzzt
        {
            /// <summary>
            /// chưa khám
            /// </summary>
            [Description("未就诊")]
            NotYetTreate = 1,
            /// <summary>
            /// đang khám
            /// </summary>
            [Description("就诊中")]
            Treating = 2,
            /// <summary>
            /// kết thúc khám bệnh
            /// </summary>
            [Description("就诊结束")]
            Treated = 3,
        }



        /// <summary>
        /// loại đơn thuốc
        /// </summary>
        public enum EnumCflx
        {
            /// <summary>
            /// đơn thuốc Tây y
            /// </summary>
            [Description("西药处方")]
            WMPres = 1,
            /// <summary>
            /// đơn thuốc Đông y
            /// </summary>
            [Description("中药处方")]
            TCMPres = 2,
            /// <summary>
            /// đơn phục hồi chức năng
            /// </summary>
            [Description("康复处方")]
            RehabPres = 3,
            /// <summary>
            /// đơn xét nghiệm
            /// </summary>
            [Description("检验处方")]
            InspectionPres = 4,
            /// <summary>
            /// đơn kiểm tra
            /// </summary>
            [Description("检查处方")]
            ExaminationPres = 5,
            /// <summary>
            /// đơn hạng mục thông thường
            /// </summary>
            [Description("常规项目处方")]
            RegularItemPres = 6,

            [Description("电子处方")]
            Dzcf = 7,

        }

        public enum EnumYpyf
        {
            [Description("西医")]
            WM = 1,
            [Description("中医")]
            TCM = 2
        }

        /// <summary>
        /// loại chẩn đoán
        /// </summary>
        public enum EnumZdlx
        {
            /// <summary>
            /// chẩn đoán chính
            /// </summary>
            [Description("主诊断")]
            Main = 1,
            /// <summary>
            /// chẩn đoán phụ
            /// </summary>
            [Description("副诊断")]
            Deputy = 2
        }

        /// <summary>
        /// loại mẫu đơn thuốc
        /// （loại mẫu bệnh án cũng dùng enum này）
        /// </summary>
        public enum EnumCfMbLx
        {
            /// <summary>
            /// cá nhân
            /// </summary>
            [Description("个人")]
            personal = 1,
            /// <summary>
            /// khoa
            /// </summary>
            [Description("科室")]
            department = 2,
            /// <summary>
            /// toàn viện
            /// </summary>
            [Description("全院")]
            hospital = 3,
        }

        /// <summary>
        /// loại nhóm kiểm tra xét nghiệm  （kiểm tra và xét nghiệm dùng chung một trang，chỉ là tham số（tức làtype）khác nhau）
        /// </summary>
        public enum EnumjyjczutaoLx
        {
            /// <summary>
            /// xét nghiệm
            /// </summary>
            [Description("检验")]
            jy = 1,
            /// <summary>
            /// kiểm tra
            /// </summary>
            [Description("检查")]
            jc = 2
        }

        /// <summary>
        /// loại mẫu kiểm tra xét nghiệm  （kiểm tra và xét nghiệm dùng chung một trang，chỉ là tham số（tức làtype）khác nhau）
        /// </summary>
        public enum EnumjyjcmbLx
        {
            /// <summary>
            /// xét nghiệm
            /// </summary>
            [Description("检验")]
            jy = 1,
            /// <summary>
            /// kiểm tra
            /// </summary>
            [Description("检查")]
            jc = 2
        }

        /// <summary>
        /// chiến lược định giá hạng mục thu phí
        /// </summary>
        public enum EnumSfxmJjcl
        {
            /// <summary>
            /// theo thời lượng
            /// </summary>
            [Description("按时长")]
            Time = 1,
            /// <summary>
            /// theo số lượng
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
        /// nguồn bệnh án lịch sử
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
        /// cờ đăng nhập
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
        /// loại ăn uống
        /// </summary>
        public enum Enumyslb
        {
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
        /// trạng thái gọi số xếp hàng
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
        /// cờ ngoại trú – nội trú  0 chung 1 chỉ ngoại trú 2 chỉ nội trú 3 hệ thống（cả ngoại trú và nội trú đều không dùng được）Ví dụ：giường tại nhà
        /// </summary>
        public enum Enummzzybz
        {

            /// <summary>
            /// chỉ ngoại trú
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// chỉ nội trú
            /// </summary>
            [Description("住院")]
            zy = 2,
            ///// <summary>
            ///// chung
            ///// </summary>
            //[Description("chung")]
            //ty = 9,
        }
        public enum Enumzxzt
        {
            /// <summary>
            /// chưa thực hiện
            /// </summary>
            [Description("未执行")]
            wzx = 0,
            /// <summary>
            /// đã thực hiện
            /// </summary>
            [Description("已执行")]
            yzx = 1,
            /// <summary>
            /// hủy thực hiện
            /// </summary>
            [Description("取消执行")]
            yqx = 2

        }
        /// <summary>
        /// trạng thái gọi số phân loại
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
        /// trạng thái khám chữa bệnh từ xa
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

        #region 住院
        /// <summary>
        /// cờ đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// đăng ký nhập viện
            /// </summary>
            Xry = 0,
            /// <summary>
            /// trong khu bệnh
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// xuất viện khỏi khu bệnh（rời khu bệnh）（chờ thanh toán）
            /// </summary>
            Djz = 2,
            /// <summary>
            /// đã xuất viện（thanh toán xuất viện）
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// chuyển khu
            /// </summary>
            Zq = 7,

            /// <summary>
            /// bản ghi hủy/hủy nhập viện
            /// </summary>
            Wry = 9,
        }

        /// <summary>
        /// loại y lệnh
        /// </summary>
        public enum EnumYzlx
        {
            ///// <summary>
            ///// chung
            ///// </summary>
            //[Description("chung")]
            //Ty = 1,
            /// <summary>
            /// thuốc
            /// </summary>
            [Description("药品")]
            Yp = 2,
            /// <summary>
            /// chữ
            /// </summary>
            [Description("文字")]
            Wz = 3,
            /// <summary>
            /// mang thuốc khi xuất viện
            /// </summary>
            [Description("出院带药")]
            Cydy = 4,
            /// <summary>
            /// hạng mục
            /// </summary>
            [Description("项目")]
            sfxm = 5,
            /// <summary>
            /// hạng mục
            /// </summary>
            [Description("检验")]
            jy = 6,
            /// <summary>
            /// hạng mục
            /// </summary>
            [Description("检查")]
            jc = 7,
            /// <summary>
            /// hạng mục
            /// </summary>
            [Description("膳食医嘱")]
            ssyz = 8,
            /// <summary>
            /// phẫu thuật
            /// </summary>
            [Description("手术")]
            oper = 9,
            /// <summary>
            /// thảo dược
            /// </summary>
            [Description("中草药")]
            zcy = 10,
            /// <summary>
            /// phục hồi chức năng
            /// </summary>
            [Description("康复")]
            rehab = 11

        }
        /// <summary>
        /// trạng thái y lệnh
        /// </summary>
        public enum EnumYzzt
        {
            /// <summary>
            /// chờ duyệt 
            /// </summary>
            [Description("待审")]
            Ds = 0,
            /// <summary>
            /// duyệt
            /// </summary>
            [Description("审核")]
            Sh = 1,
            /// <summary>
            /// thực hiện
            /// </summary>
            [Description("执行")]
            Zx = 2,
            /// <summary>
            /// thu hồiDC
            /// </summary>
            [Description("DC")]
            DC = 3,
            /// <summary>
            /// dừng hoặc hủy
            /// </summary>
            [Description("停止")]
            TZ = 4
        }
        /// <summary>
        /// loại đơn vị thuốc
        /// </summary>
        public enum EnumYPdwlb
        {
            /// <summary>
            /// đơn vị liều lượng
            /// </summary>
            Jldw = 1,
            /// <summary>
            /// đơn vị kho thuốc
            /// </summary>
            Ykdw = 2,
            /// <summary>
            /// đơn vị ngoại trú
            /// </summary>
            Mzdw = 3,
            /// <summary>
            /// đơn vị nội trú
            /// </summary>
            Zydw = 4,
            /// <summary>
            /// đơn vị nhập hàng
            /// </summary>
            Jhdw = 5,
            /// <summary>
            /// đơn vị nhỏ nhất
            /// </summary>
            Zxdw = 6
        }
        /// <summary>
        /// phạm vi gói
        /// </summary>
        public enum EnumTcfw
        {
            [Description("个人")]
            Person = 1,
            [Description("科室")]
            Dept = 2,
            [Description("病区")]
            Ward = 3,
            [Description("全院")]
            Hosp = 4
        }
        /// <summary>
        /// trạng thái bệnh nhân điều dưỡng（dùng để nhập dấu hiệu sinh tồn điều dưỡng）
        /// </summary>
        public enum EnumHlbrzt
        {
            [Description("入院")]
            Ry = 1,
            [Description("在院")]
            Zy = 2,
            [Description("出院")]
            Cy = 3,
            [Description("请假")]
            Qj = 4,
            [Description("转入")]
            Zr = 5,
            [Description("转出")]
            Zc = 6,
            [Description("拒测")]
            Jc = 7,
            [Description("返院")]
            Fy = 8,
            [Description("复测")]
            Fc = 9,
            [Description("卧床")]
            Wc = 10,
            [Description("死亡")]
            Sw = 11,
            [Description("手术")]
            ss = 12,
            [Description("回室")]
            hs = 13
        }
        /// <summary>
        /// đơn vị chu kỳ thực hiện
        /// </summary>
        public enum EnumZxzqdw
        {
            /// <summary>
            /// ngày
            /// </summary>
            Day = 1,
            /// <summary>
            /// giờ
            /// </summary>
            Hour = 2,
            /// <summary>
            /// phút
            /// </summary>
            Minute = 3,
            /// <summary>
            /// chu kỳ không đều
            /// </summary>
            Unruly = 4
        }
        /// <summary>
        /// cờ phát thuốc – trả thuốc
        /// </summary>
        public enum EnumMedSTflag
        {
            [Description("发药")]
            Receive = 1,
            [Description("全退")]
            AllReturn = 2,
            [Description("部分退")]
            PartReturn = 3
        }
        /// <summary>
        /// tính chất y lệnh
        /// </summary>
        public enum EnumYzxz
        {
            [Description("临时")]
            Ls = 1,
            [Description("长期")]
            Cq = 2
        }
        /// <summary>
        /// trạng thái hồ sơ giường bệnh
        /// </summary>
        public enum EnumCwjlzt
        {
            [Description("当前")]
            Dq = 1,
            [Description("转床")]
            Zc = 2,
            [Description("转区")]
            Zq = 3,
            [Description("出区")]
            Cq = 4,
            [Description("取消入区")]
            QxRq = 5
        }
        /// <summary>
        /// loại bác sĩ
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
        /// cấp điều dưỡng
        /// </summary> 
        public enum EnumHljb
        {
            /// <summary>
            /// Icấp
            /// </summary>
            [Description("I级")]
            I = 1,
            /// <summary>
            /// IIcấp
            /// </summary>
            [Description("II级")]
            II = 2,
            /// <summary>
            /// IIIcấp
            /// </summary>
            [Description("III级")]
            III = 3,
            /// <summary>
            /// đặc cấp
            /// </summary>
            [Description("特级")]
            T = 4
        }
        /// <summary>
        /// trạng thái thanh toán
        /// </summary>
        public enum EnumJszt
        {
            [Description("已结算")]
            Yjs = 1,
            [Description("未结算")]
            Wjs = 2
        }
        /// <summary>
        /// tiêu chuẩn thực hiện cố định
        /// </summary>
        public enum EnumGdzxbz
        {
            /// <summary>
            /// cố định
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// không cố định
            /// </summary>
            Nonfixed = 0
        }
        /// <summary>
        /// mức nguy kịch
        /// </summary>
        public enum EnumWzjb
        {
            /// <summary>
            /// chung
            /// </summary>
            [Description("一般")]
            Normal = 1,
            /// <summary>
            /// bệnh nặng
            /// </summary>
            [Description("病重")]
            Serious = 2,
            /// <summary>
            /// nguy kịch
            /// </summary>
            [Description("病危")]
            Bedying = 3
        }
        //EnumDBđã bao gồm
        //public enum EnumZdlx
        //{
        //    MainDiag=1,
        //    SedDiag=2
        //}

        /// <summary>
        /// mô tả đo nhiệt độ cơ thể
        /// </summary>
        public enum EnumTwclfs
        {
            /// <summary>
            /// nhiệt độ miệng
            /// </summary>
            [Description("口温")]
            kw = 1,
            /// <summary>
            /// nhiệt độ nách
            /// </summary>
            [Description("腋温")]
            yw = 2,
            /// <summary>
            /// nhiệt độ hậu môn
            /// </summary>
            [Description("肛温")]
            gw = 1,
        }
        /// <summary>
        /// loại bữa ăn
        /// </summary>
        public enum EnumSSLB
        {
            /// <summary>
            /// loại
            /// </summary>
            [Description("类型")]
            lx = 1,
            /// <summary>
            /// yêu cầu đặc biệt
            /// </summary>
            [Description("特殊要求")]
            tsyq = 2,
            /// <summary>
            /// mẫu bữa ăn
            /// </summary>
            [Description("膳食型号")]
            ssxh = 3,
        }

        /// <summary>
        /// quyền sử dụng mẫu
        /// </summary>
        public enum Enummbqx
        {
            /// <summary>
            /// chung
            /// </summary>
            [Description("通用")]
            pub = 0,
            /// <summary>
            /// cá nhân
            /// </summary>
            [Description("个人")]
            prv = 1,
            /// <summary>
            /// khoa
            /// </summary>
            [Description("科室")]
            dept = 2
        }

        /// <summary>
        /// loại bệnh án
        /// </summary>
        public enum EnumBllx
        {
            /// <summary>
            /// bệnh án nội trú
            /// </summary>
            [Description("住院病历")]
            zybl = 1,

            /// <summary>
            /// hồ sơ diễn biến bệnh
            /// </summary>
            [Description("病程记录")]
            bcjl = 2,

            /// <summary>
            /// văn bản y tế
            /// </summary>
            [Description("医疗文书")]
            ylws = 3,

            /// <summary>
            /// hồ sơ điều dưỡng
            /// </summary>
            [Description("护理记录")]
            hljl = 4,

            /// <summary>
            /// trang đầu bệnh án
            /// </summary>
            /// 
            [Description("病案首页")]
            basy = 5,

            /// <summary>
            /// đánh giá phục hồi chức năng
            /// </summary>
            [Description("康复评估")]
            kfpg = 6
        }

        //public enum EnumCYQK
        //{
        //    /// <summary>
        //    /// khỏi bệnh
        //    /// </summary>
        //    [Description("khỏi bệnh")]
        //    zy = 1,
        //    /// <summary>
        //    /// thuyên giảm
        //    /// </summary>
        //    [Description("thuyên giảm")]
        //    hz = 2,
        //    /// <summary>
        //    /// chưa khỏi
        //    /// </summary>
        //    [Description("chưa khỏi")]
        //    wy = 3,
        //    /// <summary>
        //    /// tử vong
        //    /// </summary>
        //    [Description("tử vong")]
        //    sw = 4,
        //    /// <summary>
        //    /// khác
        //    /// </summary>
        //    [Description("khác")]
        //    qt = 5,
        //    /// <summary>
        //    /// chuyển viện
        //    /// </summary>
        //    [Description("chuyển viện")]
        //    zhuanyuan = 6

        //}
        public enum EnumSF
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("否")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("是")]
            s = 1,
        }

        //kiểm tra thử phản ứng da
        public enum EnumPSCS
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("否")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("是")]
            s = 1,
        }
        /// <summary>
        /// kết quả thử phản ứng da
        /// </summary>
        public enum EnumPsResult
        {
            [Description("阴性")]
            negative = 0,
            [Description("阳性")]
            positive = 1,
        }

        /// <summary>
        /// trạng thái chuẩn bị thuốc của khoa
        /// </summary>
        public enum EnumKSBYZT
        {
            [Description("已申请")]
            sq = 0,
            [Description("已提交")]
            sh = 1,
            [Description("已审核")]
            tj = 2,
            [Description("已发药")]
            fy = 3,
            [Description("已退回")]
            th = 4,
            [Description("已作废")]
            zf = 5,
        }

        /// <summary>
        /// trạng thái trả lại thuốc chuẩn bị của khoa
        /// </summary>
        public enum EnumKSBYTHZT
        {
            [Description("保存")]
            sq = 1,
            [Description("审核")]
            sh = 2,
            [Description("提交")]
            tj = 3,
            [Description("发药")]
            fy = 4,
            [Description("撤回")]
            ch = 5,
            [Description("退回")]
            th = 6,
            [Description("作废")]
            zf = 7,
        }
        /// <summary>
        /// nguồn thuốc
        /// </summary>
        public enum EnumYply
        {
            [Description("科室备药")]
            ksby = 1,
            [Description("药房")]
            yf = 2
        }

        /// <summary>
        /// thử phản ứng da
        /// </summary>
        public enum EnumPs
        {
            [Description("阴")]
            yin = 0,
            [Description("阳")]
            yang = 1,
        }
        #endregion
    }
    #region Emr
    public class EmrClientEnum : ClientEnum
    {
        /// <summary>
        /// quyền sử dụng mẫu
        /// </summary>
        public enum Enummbqx
        {
            /// <summary>
            /// chung
            /// </summary>
            [Description("通用")]
            pub = 0,
            /// <summary>
            /// cá nhân
            /// </summary>
            [Description("个人")]
            prv = 1,
            /// <summary>
            /// khoa
            /// </summary>
            [Description("科室")]
            dept = 2
        }
        /// <summary>
        /// phân bổ quyền mẫu
        /// </summary>
        public enum EnummbqxFp
        {
            /// <summary>
            /// không có bất kỳ quyền nào
            /// </summary>
            [Description("无权限")]
            non = 0,
            /// <summary>
            /// chỉ đọc
            /// </summary>
            [Description("只读")]
            read = 1,
            /// <summary>
            /// đọc – ghi
            /// </summary>
            [Description("读写")]
            edit = 2
        }

        /// <summary>
        /// trạng thái bệnh án
        /// </summary>
        public enum EnumRecordStu
        {
            [Description("未提交")]
            wtj = 0,
            [Description("已提交")]
            ytj = 1,
            [Description("退回")]
            th = 2,
            [Description("已签收")]
            yqs = 3,
            //[Description("lưu trữ bệnh án")]
            //bagd = 4
        }

        public enum EnumPlanStu
        {
            [Description("进行中")]
            zc = 0,
            [Description("已停止")]
            tz = 1,
            [Description("停止取消")]
            tzqx = 2,
        }

        /// <summary>
        /// loại lớn bệnh án và mẫu cờ nghiệp vụ
        /// </summary>
        public enum EnumMzbz
        {
            /// <summary>
            /// nội trú
            /// </summary>
            [Description("住院")]
            zy = 0,
            /// <summary>
            /// khám ngoại trú
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// không giới hạn
            /// </summary>
            [Description("不限")]
            all = 2,
        }

        /// <summary>
        /// phương thức tải mẫu
        /// </summary>
        public enum EnummbqxTempLoadWay
        {
            /// <summary>
            /// chung
            /// </summary>
            [Description("都昌编辑器")]
            DcWriter = 0,
            /// <summary>
            /// cá nhân
            /// </summary>
            [Description("独立视图")]
            View = 1
        }

        /// <summary>
        /// cờ sử dụng
        /// </summary>
        public enum EnumSybz
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 0
        }
        public enum EnumBlys
        {
            [Description("目录")]
            ml = -1,
            [Description("文本")]
            wb = 0,
            [Description("下拉")]
            xl = 1,
            [Description("下拉多选")]
            xldx = 2,
            [Description("日期(yyyy-MM-dd)")]
            rq = 3,
            [Description("时间(HH:mm)")]
            sj = 4,
            [Description("日期时间(yyyy-MM-dd HH:mm)")]
            rqsj = 5,
            [Description("数值")]
            sz = 6
        }
        public enum ApplyStatusEnum
        {
            /// <summary>
            /// chưa phê duyệt
            /// </summary>
            [Description("未批准")]
            wpz = 0,
            /// <summary>
            /// đã phê duyệt
            /// </summary>
            [Description("已审批")]
            ysp = 1,
            /// <summary>
            /// trả lại
            /// </summary>
            [Description("退回")]
            th = 2
        }

        public enum EnumMRSex
        {
            [Description("男")]
            M = 1,
            [Description("女")]
            F = 2
        }
        /// <summary>
        /// trạng thái bệnh án
        /// </summary>
        public enum Enumbazt
        {
            [Description("待录入")]
            dlr = 1,
            [Description("已录入")]
            lrz = 2,
            //[Description("đã lưu trữ")]
            //gd = 3
        }

        ///// <summary>
        ///// trạng thái bệnh án vàEMRchung
        ///// </summary>
        //public enum EnumRecordStu
        //{
        //    [Description("chưa nộp")]
        //    wtj = 0,
        //    [Description("đã nộp")]
        //    ytj = 1,
        //    [Description("trả lại")]
        //    th = 2,
        //    [Description("đã ký nhận")]
        //    yqs = 3,
        //    //[Description("lưu trữ bệnh án")]
        //    //bagd = 4
        //}
        /// <summary>
        /// nhóm máu
        /// </summary>
        public enum EnumBloodType
        {
            [Description("A")]
            A = 1,
            [Description("B")]
            B = 2,
            [Description("O")]
            O = 3,
            [Description("AB")]
            AB = 4,
            [Description("不详")]
            BX = 5,
            [Description("未查")]
            WC = 6
        }

        /// <summary>
        /// khám nghiệm tử thi bệnh nhân đã tử vong
        /// </summary>
        public enum EnumSwhzsj
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 2
        }

        /// <summary>
        /// RH
        /// </summary>
        public enum EnumBloodTypeRH
        {
            [Description("阴")]
            yin = 1,
            [Description("阳")]
            yang = 2,
            [Description("不详")]
            BX = 3,
            [Description("未查")]
            WC = 4
        }

        /// <summary>
        /// quan hệ
        /// </summary>
        public enum EnumGx
        {
            [Description("本人或户主")]
            br = 1,
            [Description("配偶")]
            fq = 2,
            [Description("子")]
            z = 3,
            [Description("女")]
            n,
            [Description("孙子、孙女或外孙子、外孙女")]
            sun,
            [Description("父母")]
            fm,
            [Description("祖父母或外祖父母")]
            zfm,
            [Description("兄、弟、姐、妹")]
            xdjm,
            [Description("其他")]
            qt
        }
        /// <summary>
        /// Có hoặc Không
        /// </summary>
        public enum EnumYorN
        {
            [Description("否")]
            N = 1,
            [Description("是")]
            Y = 2
        }


        /// <summary>
        /// Có hoặc Không
        /// </summary>
        public enum EnumNewYorN
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 2
        }

        public enum EnumYork
        {
            [Description("急诊")]
            y = 1,
            [Description("门诊")]
            lcwqd = 2,
            [Description("其他医疗机构转入")]
            qkbm = 3,
            [Description("其他")]
            w = 4
        }
        /// <summary>
        /// Không hoặc là
        /// </summary>
        public enum EnumNorY
        {
            [Description("否")]
            N = 1,
            [Description("是")]
            Y = 2
        }
        /// <summary>
        /// có hoặc không
        /// </summary>
        public enum EnumHorN
        {
            [Description("无")]
            Non = 1,
            [Description("有")]
            Has = 2
        }
        /// <summary>
        /// phản ứng truyền máu
        /// 1.Có  2.Không  3.chưa nhập
        /// </summary>
        public enum EumSYFY
        {
            [Description("是")]
            y = 1,
            [Description("否")]
            n = 2,
            [Description("未输")]
            Non = 3
        }
        /// <summary>
        ///tình trạng bệnh khi nhập viện 1	có 2	lâm sàng chưa xác định 3	tình hình không rõ 4	không có
        /// </summary>
        public enum EnumRybq
        {
            [Description("有")]
            y = 1,
            [Description("临床未确定")]
            lcwqd = 2,
            [Description("情况不明")]
            qkbm = 3,
            [Description("无")]
            w = 4
        }

        /// <summary>
        /// tình hình xuất viện
        /// 1	khỏi bệnh 2	thuyên giảm 3	chưa khỏi4	tử vong5	khác
        /// </summary>
        public enum EnumCyqk
        {
            [Description("治愈")]
            zy = 1,
            [Description("好转")]
            hz = 2,
            [Description("未愈")]
            wy = 3,
            [Description("死亡")]
            sw = 4,
            [Description("其他")]
            qt = 5
        }
        /// <summary>
        /// loại chẩn đoán-chính – phụ
        /// </summary>
        public enum EnumZdlxbs
        {
            [Description("主要诊断")]
            zy = 1,
            [Description("次要诊断")]
            cy = 2
        }
        /// <summary>
        /// loại chẩn đoán
        /// </summary>
        public enum EnumZdlx2
        {
            [Description("西医诊断")]
            WM = 1,
            [Description("中医诊断")]
            TCM = 2

        }

        /// <summary>
        /// đường nhập viện
        /// </summary>
        public enum EnumMrRYTJ
        {
            /// <summary>
            /// khám ngoại trú
            /// </summary>
            [Description("门诊")]
            mz = 1,
            /// <summary>
            /// cấp cứu
            /// </summary>
            [Description("急诊")]
            jz = 2,
            /// <summary>
            /// chuyển đến từ cơ sở y tế khác
            /// </summary>
            [Description("其他医疗机构转入")]
            qtyljg = 3,
            /// <summary>
            /// 120
            /// </summary>
            //[Description("120")]
            //jj120,
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            Else = 9,
        }
        /// <summary>
        /// chất lượng bệnh án
        /// </summary>
        public enum EnumBazl
        {
            [Description("甲")]
            I = 1,
            [Description("乙")]
            II,
            [Description("丙")]
            III
        }
        /// <summary>
        /// phương thức rời viện
        /// </summary>
        public enum EnumLyfs
        {
            [Description("医嘱离院")]
            yzly = 1,
            /// <summary>
            /// cấp cứu
            /// </summary>
            [Description("医嘱转院")]
            yzzy,
            [Description("医嘱转社区卫生服务机构/乡镇卫生院")]
            yzzsq,
            [Description("非医嘱离院")]
            fyz,
            [Description("死亡")]
            sw,
            [Description("其他")]
            other = 9,
        }
        /// <summary>
        /// phân loại tình trạng bệnh
        /// </summary>
        public enum EnumBqfx
        {
            [Description("病危")]
            bw = 1,
            [Description("病重")]
            bz,
            [Description("疑难")]
            yn,
            [Description("抢救")]
            qj,
            [Description("一般")]
            yb
        }
        /// <summary>
        /// nguyên nhân biến đổi lộ trình lâm sàng
        /// </summary>
        public enum EnumBYYY
        {
            [Description("正变异")]
            zby = 1,
            [Description("负变异")]
            fby,
            [Description("系统原因")]
            xtyy
        }
        /// <summary>
        /// tình hình kiểm tra
        /// </summary>
        public enum EnumJBQK
        {
            [Description("CT")]
            CT = 1,
            [Description("PETCT")]
            PETCT = 2,
            [Description("双源")]
            SY = 3,
            [Description("B超")]
            Bc = 4,
            [Description("X片")]
            Xp = 5,
            [Description("超声心动图")]
            CSXDT = 6,
            [Description("MRI")]
            MRI = 7
        }
        /// <summary>
        /// tình hình kiểm tra
        /// </summary>
        public enum EnumJCQK
        {
            [Description("阳性")]
            Yang = 1,
            [Description("阴性")]
            Non = 2,
            [Description("未做")]
            Yin = 3,


        }

        /// <summary>
        ///tình hình sử dụng thuốc kháng khuẩn 
        /// </summary>
        public enum EnumKJYWSYQK
        {
            [Description("Ⅰ种")]
            first = 1,
            [Description("Ⅱ联")]
            second = 2,
            [Description("Ⅲ联")]
            third = 3,
            [Description("Ⅳ联")]
            forth = 4,
            [Description(">Ⅳ联")]
            more = 5
        }

        /// <summary>
        ///phản ứng truyền dịch 0chưa nhập 2không có 3có
        /// </summary>
        public enum EnumSYFY
        {
            [Description("未输")]
            first = 0,
            [Description("无")]
            second = 1,
            [Description("有")]
            third = 2
        }

        /// <summary>
        ///phản ứng truyền máu 0chưa nhập 2không có 3có
        /// </summary>
        public enum EnumSXFY
        {
            [Description("是")]
            Y = 1,
            [Description("否")]
            N = 2,
            [Description("未输")]
            none = 3
        }

        /// <summary>
        ///mức độ tổn thương do ngã trong thời gian nội trú 0chưa nhập 2không có 3có
        /// </summary>
        public enum EnumZYDD_SHCD
        {
            [Description("一级")]
            first = 1,
            [Description("二级")]
            second = 2,
            [Description("三级")]
            third = 3,
            [Description("未造成伤害")]
            none = 4
        }

        /// <summary>
        ///nguyên nhân ngã hoặc rơi khỏi giường
        /// </summary>
        public enum EnumZYDD_YY
        {
            [Description("健康原因")]
            jkyy = 1,
            [Description("治疗，药物，麻醉原因")]
            zlywmzyy = 2,
            [Description("环境因素")]
            hjys = 3,
            [Description("其他原因")]
            qtyy = 4
        }

        /// <summary>
        ///phân giai đoạn loét do tỳ đè
        /// </summary>
        public enum EnumYCFQ
        {
            [Description("1期")]
            first = 1,
            [Description("2期")]
            second = 2,
            [Description("3期")]
            third = 3,
            [Description("4期")]
            forth = 4
        }



    }
    #endregion


    #region OR
    public class OrClientEnum : ClientEnum
    {
        /// <summary>
        /// trạng thái đăng ký
        /// </summary>
        public enum EnumSqzt
        {
            [Description("待审核")]
            /// <summary>
            /// đã đăng ký, chờ duyệt(lịch trực)
            /// </summary>
            dsh = 1,
            [Description("已审核")]
            /// <summary>
            /// đã duyệt(lịch trực)chờ phẫu thuật
            /// </summary>
            ypb = 2,
            [Description("已取消申请")]
            /// <summary>
            /// đã hủy đăng ký
            /// </summary>
            yqx = 3,

        }

        /// <summary>
        /// trạng thái phẫu thuật
        /// </summary>
        public enum EnumSSzt
        {
            [Description("待排班")]
            /// <summary>
            /// đã duyệt(lịch trực)
            /// </summary>
            dpb = 1,
            [Description("已排班")]
            /// <summary>
            /// đã duyệt(lịch trực)chờ phẫu thuật
            /// </summary>
            ypb = 2,
            [Description("已作废")]
            /// <summary>
            /// đã hủy đăng ký
            /// </summary>
            yzf = 3,
            [Description("已登记")]
            /// <summary>
            /// đã hủy đăng ký
            /// </summary>
            yzx = 4

        }

        /// <summary>
        /// trạng thái đăng ký phẫu thuật
        /// </summary>
        public enum EnumSSdjzt
        {
            [Description("待登记")]
            ddj = 1,
            [Description("已登记")]
            ydj = 2,
            [Description("登记作废")]
            yzf = 3
        }
        /// <summary>
        /// cấp phẫu thuật
        /// </summary>
        public enum EnumSSjb
        {
            [Description("I级")]
            yj = 1,
            [Description("II级")]
            ej = 2,
            [Description("III级")]
            sj = 3,
            [Description("IV级")]
            fj = 4
        }

        /// <summary>
        /// có cách ly hay không
        /// </summary>
        public enum EnumIsgl
        {
            [Description("否")]
            no = 0,
            [Description("是")]
            yes = 1
        }

        /// <summary>
        /// có vi khuẩn hay không
        /// </summary>
        public enum EnumIsjun
        {
            [Description("否")]
            no = 0,
            [Description("是")]
            yes = 1
        }
    }
    #endregion

    #region PDS
    public class PdsClientEnum : ClientEnum
    {/// <summary>
     /// loại chứng từ
     /// </summary>
        public enum EnumDanJuLX
        {
            /// <summary>
            /// nhập kho thuốc
            /// </summary>
            [Description("药品入库")]   //số chứng từYPRKD111111111111
            yaopinruku = 1,

            /// <summary>
            /// xuất kho ra ngoài
            /// </summary>
            [Description("外部出库")]   //số chứng từWBCKD111111111111
            waibucuku = 2,

            /// <summary>
            /// phát thuốc trực tiếp
            /// </summary>
            [Description("直接出库")]   //số chứng từNBFYD111111111111
            zhijiefayao = 3,

            /// <summary>
            /// phát thuốc theo đăng ký nhận
            /// </summary>
            [Description("申领出库")]   //số chứng từSLCKD111111111111
            shenlingfayao = 4,

            /// <summary>
            /// trả lại thuốc phát nội bộ
            /// </summary>
            [Description("内部发药退回")] //số chứng từNBFYTHD111111111111
            neibufayaotuihui = 5,

            /// <summary>
            /// nhà thuốc phát thuốc cho khoa
            /// </summary>
            [Description("科室发药")] //số chứng từKSFYD111111111111
            keshifayao = 6,

            /// <summary>
            /// xuất kho thuốc cơ bản
            /// </summary>
            [Description("基药出库")] // thống kê thông tin chứng từ theo phương thức xuất kho
            jiyaochuku = 13,
            /// <summary>
            /// xuất kho hàng loạt
            /// </summary>
            [Description("批量出库")] // thống kê thông tin chứng từ theo phương thức xuất kho
            piliangchuku = 14,
        }

        /// <summary>
        /// trạng thái tồn kho
        /// </summary>
        public enum EnumKCZT
        {
            /// <summary>
            /// bật
            /// </summary>
            [Description("启用")]
            Enabled = 1,
            /// <summary>
            /// ngừng sử dụng
            /// </summary>
            [Description("停用")]
            Disabled = 0
        }

        /// <summary>
        /// trạng thái khoa này
        /// </summary>
        public enum BenBuMenZT
        {
            /// <summary>
            /// bình thường
            /// </summary>
            [Description("正常")]
            Normal = 1,
            /// <summary>
            /// điều khiển
            /// </summary>
            [Description("控制")]
            Control = 0
        }

        /// <summary>
        /// trạng thái thuốc
        /// </summary>
        public enum EnumYPZT
        {
            /// <summary>
            /// bật
            /// </summary>
            [Description("启用")]
            Enabled = 1,
            /// <summary>
            /// ngừng sử dụng
            /// </summary>
            [Description("停用")]
            Disabled = 0
        }

        /// <summary>
        /// hiển thị tồn kho
        /// </summary>
        public enum EnumKCXS
        {
            /// <summary>
            /// None
            /// </summary>
            [Description("全部")]
            None = -1,
            /// <summary>
            /// hiển thị tồn kho bằng 0
            /// </summary>
            [Description("显示零库存")]
            Xslkc = 0,
            /// <summary>
            /// không hiển thị số lượng lý thuyết bằng 0
            /// </summary>
            [Description("不显示理论数量为零")]
            Bxsllslwl = 1,
            /// <summary>
            /// không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("不显示实际数量为零")]
            Bxssjslwl = 2,
            /// <summary>
            /// không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("不显示两者都为零")]
            Bxslzdwl = 4
        }

        /// <summary>
        /// trạng thái phát phiếu đăng ký nhận
        /// </summary>
        public enum EnumSLDDeliveryStatus
        {
            /// <summary>
            /// chưa phát
            /// </summary>
            [Description("未发")]
            None = 0,

            /// <summary>
            /// đã phát một phần
            /// </summary>
            [Description("已发部分")]
            Part,

            /// <summary>
            /// đã phát hết
            /// </summary>
            [Description("已全发")]
            All,

            /// <summary>
            /// đã chấm dứt（một số（thậm chí tất cả）chi tiết sau này sẽ không phát nữa，nhưng điều này sẽ được coi là phiếu đăng ký nhận đã kết thúc）
            /// </summary>
            [Description("已终止")]
            Abandon
        }

        /// <summary>
        /// kết quả duyệt
        /// </summary>
        public enum EnumDjShzt
        {
            /// <summary>
            /// chưa duyệt
            /// </summary>
            [Description("未审核")]
            WaitingApprove = 0,
            /// <summary>
            /// đã đạt
            /// </summary>
            [Description("已通过")]
            Approved = 1,
            /// <summary>
            /// chưa đạt（trạng thái cuối cùng）
            /// </summary>
            [Description("未通过")]
            Rejected = 2,
            /// <summary>
            /// đã hủy（trạng thái cuối cùng）（chỉ khi duyệt đạt -> mới có thể đến trạng thái này）
            /// </summary>
            [Description("已作废")]
            Cancelled = 3,
        }

        /// <summary>
        /// cờ lãi lỗ
        /// </summary>
        public enum EnumProfitLossMark
        {
            /// <summary>
            /// báo hỏng
            /// </summary>
            [Description("报损")]
            Loss = 0,

            /// <summary>
            /// báo thừa
            /// </summary>
            [Description("报溢")]
            Profit = 1
        }

        /// <summary>
        /// loại thao tác điều chỉnh giá thuốc（thu hồi phê duyệt từ chối thực hiện）
        /// </summary>
        public enum EnumPriceAdjustOperationType
        {
            /// <summary>
            /// chờ duyệt
            /// </summary>
            [Description("待审核")]
            None = 0,
            /// <summary>
            /// phê duyệt
            /// </summary>
            [Description("批准")]
            Approval = 1,
            /// <summary>
            /// từ chối
            /// </summary>
            [Description("拒绝")]
            Refuse = 2,
            /// <summary>
            /// thu hồi
            /// </summary>
            [Description("撤销")]
            Withdraw = 3,
            /// <summary>
            /// thực hiện
            /// </summary>
            [Description("执行")]
            Execute = 4
        }

        /// <summary>
        /// trạng thái thực hiện điều chỉnh giá thuốc（0：chưa thực hiện 1：đã thực hiện）
        /// </summary>
        public enum EnumPriceAdjustZXStatus
        {
            /// <summary>
            /// chưa thực hiện
            /// </summary>
            [Description("未执行")]
            Not = 0,
            /// <summary>
            /// đã thực hiện
            /// </summary>
            [Description("已执行")]
            Executed = 1
        }

        /// <summary>
        /// nhóm thu phí lớn loại nhóm lớn
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
        /// ghi chú phát thuốc 0：chưa phát；1：đã xếp；2：đã phát；3：đã trả lại
        /// </summary>
        public enum EnumFybz
        {
            /// <summary>
            /// chưa xếp
            /// </summary>
            [Description("未排")]
            Wp = 0,

            /// <summary>
            /// đã xếp
            /// </summary>
            [Description("已排")]
            Yp = 1,

            /// <summary>
            /// đã phát
            /// </summary>
            [Description("已发")]
            Yf = 2,

            /// <summary>
            /// đã trả lại
            /// </summary>
            [Description("已退")]
            Yt = 3
        }

        /// <summary>
        /// tính chất y lệnh tạm thời、dài hạn
        /// </summary>
        public enum Yzxz
        {
            /// <summary>
            /// None
            /// </summary>
            [Description("全部")]
            None = 0,

            /// <summary>
            /// tạm thời
            /// </summary>
            [Description("临时")]
            Ls = 1,

            /// <summary>
            /// dài hạn
            /// </summary>
            [Description("长期")]
            Cq = 2,
        }
        public enum Yzlx
        {
            [Description("药品")]
            Yp = 2,
            /// <summary>
            /// thảo dược
            /// </summary>
            [Description("中草药")]
            zcy = 10,
        }

        /// <summary>
        /// hồ sơ thao tác thuốc zy_ypyzczjl.operateType và mz_cfypczjl.operateType
        /// </summary>
        public enum EnumOperateType
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,

            /// <summary>
            /// phát thuốc
            /// </summary>
            [Description("发药")]
            Fy = 1,

            /// <summary>
            /// trả thuốc
            /// </summary>
            [Description("退药")]
            Ty = 2
        }

        /// <summary>
        /// phương thức phát thuốc
        /// </summary>
        public enum EnumFyfs
        {
            /// <summary>
            /// xuất kho bình thường
            /// </summary>
            [Description("正常出库")]
            Zcck = 11,

            /// <summary>
            /// xuất kho trả hàng
            /// </summary>
            [Description("退货出库")]
            Thck = 5,

            /// <summary>
            /// xuất kho tặng
            /// </summary>
            [Description("赠送出库")]
            Zsck = 12,

            /// <summary>
            /// xuất kho cứu trợ thiên tai
            /// </summary>
            [Description("救灾出库")]
            Jzck = 13,
        }


        /// <summary>
        /// chế độ mua hàng
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
        /// loại hàng hóa
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
        /// đơn vị đo lường mua hàng
        /// </summary>
        public enum EnumCGJLDW
        {
            [Description("计价单位")]
            jjdw = 1,
            [Description("最小使用单位")]
            zxsydw = 2,
        }


    }
    #endregion

    #region Hrp
    public class HrpClientEnum : ClientEnum
    {
        /// <summary>
        /// cấp kho
        /// </summary>
        public enum EnumWarehouseLevel
        {
            /// <summary>
            /// cấp một
            /// </summary>
            [Description("一级")]
            OneLevel = 1,

            /// <summary>
            /// cấp hai
            /// </summary>
            [Description("二级")]
            TwoLevel = 2,

            /// <summary>
            /// cấp ba
            /// </summary>
            [Description("三级")]
            ThreeLevel = 3,

            /// <summary>
            /// cấp bốn
            /// </summary>
            [Description("四级")]
            FourLevel = 4,

            /// <summary>
            /// cấp năm
            /// </summary>
            [Description("五级")]
            FiveLevel = 5,

            /// <summary>
            /// cấp sáu
            /// </summary>
            [Description("六级")]
            SixLevel = 6,

        }

        /// <summary>
        /// hiệu lực/không hiệu lực
        /// </summary>
        public enum Enumzt
        {
            /// <summary>
            /// không hiệu lực
            /// </summary>
            [Description("无效")]
            Disable = 0,

            /// <summary>
            /// hiệu lực
            /// </summary>
            [Description("有效")]
            Enable = 1

        }

        /// <summary>
        /// trạng thái duyệt
        /// </summary>
        public enum EnumAuditState
        {

            /// <summary>
            /// chờ xử lý
            /// </summary>
            [Description("待处理")]
            Waiting = 0,

            /// <summary>
            /// duyệt đạt
            /// </summary>
            [Description("审核通过")]
            Adopt = 1,

            /// <summary>
            /// duyệt không đạt
            /// </summary>
            [Description("审核不通过")]
            Refuse = 2,

            /// <summary>
            /// đã hủy
            /// </summary>
            [Description("已作废")]
            Cancelled = 3,

            /// <summary>
            /// tạm lưu
            /// </summary>
            [Description("暂存")]
            Temporary = 4,
        }

        /// <summary>
        /// loại chứng từ
        /// </summary>
        public enum EnumOutOrInStorageBillType
        {
            /// <summary>
            /// nhập kho từ bên ngoài
            /// </summary>
            [Description("外部入库")]
            Wbrk = 1,

            /// <summary>
            /// xuất kho ra ngoài
            /// </summary>
            [Description("外部出库")]
            Wbck = 2,

            /// <summary>
            /// xuất kho trực tiếp
            /// </summary>
            [Description("直接出库")]
            Zjck = 3,

            /// <summary>
            /// xuất kho theo đăng ký nhận
            /// </summary>
            [Description("申领出库")]
            Slck = 4,

            /// <summary>
            /// trả hàng nội bộ
            /// </summary>
            [Description("内部退货")]
            Nbth = 5,

            /// <summary>
            /// báo hỏng, báo thừa
            /// </summary>
            [Description("报损报溢")]
            Bsby = 6,

            /// <summary>
            /// phân phối đến khoa
            /// </summary>
            [Description("出库至科室")] //số chứng từckZKS111111111111
            chukuzhikeshi = 7,

            /// <summary>
            /// đăng ký nhận của khoa
            /// </summary>
            [Description("科室申领")]
            kssl = 8,

            /// <summary>
            /// xuất kho theo đăng ký nhận của khoa
            /// </summary>
            [Description("科室申领出库")]
            ksSlck = 9,

            /// <summary>
            /// kế hoạch mua hàng
            /// </summary>
            [Description("采购计划")]
            purchasingPlan = 10,

            /// <summary>
            /// đơn hàng mua hàng
            /// </summary>
            [Description("采购订单")]
            purchasingOrder = 11,
        }

        /// <summary>
        /// hiển thị tồn kho
        /// </summary>
        public enum EnumKCXS
        {
            /// <summary>
            /// None
            /// </summary>
            [Description("全部")]
            None = -1,
            /// <summary>
            /// hiển thị tồn kho bằng 0
            /// </summary>
            [Description("显示零库存")]
            Xslkc = 0,
            /// <summary>
            /// không hiển thị số lượng lý thuyết bằng 0
            /// </summary>
            [Description("不显示理论数量为零")]
            Bxsllslwl = 1,
            /// <summary>
            /// không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("不显示实际数量为零")]
            Bxssjslwl = 2,
            /// <summary>
            /// không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("不显示两者都为零")]
            Bxslzdwl = 4
        }

        /// <summary>
        /// khóa tồn kho
        /// </summary>
        public enum EnumKcLock
        {
            /// <summary>
            /// không khóa
            /// </summary>
            [Description("无锁")]
            none = 0,

            /// <summary>
            /// bắt đầu kiểm kê
            /// </summary>
            [Description("开始盘点")]
            InventoryStart = 1,

            /// <summary>
            /// kết thúc kiểm kê
            /// </summary>
            [Description("结束盘点")]
            InventoryEnd = 2,

            /// <summary>
            /// lãi lỗ
            /// </summary>
            [Description("损益")]
            ProfitAndLoss = 3,

            /// <summary>
            /// duyệt chứng từ xuất nhập kho
            /// </summary>
            [Description("出入库单据审核")]
            OutgoingOrWarehousing = 4
        }

        /// <summary>
        /// cờ lãi lỗ
        /// </summary>
        public enum EnumSybz
        {
            /// <summary>
            /// báo hỏng
            /// </summary>
            [Description("报损")]
            Loss = 0,

            /// <summary>
            /// báo thừa
            /// </summary>
            [Description("报溢")]
            Profit = 1,
        }

        #region 调价

        /// <summary>
        /// cờ duyệt điều chỉnh giá 0:chưa duyệt 1:đã duyệt 2:đã từ chối 3.đã thu hồi
        /// </summary>
        public enum EnumTjShzt
        {
            /// <summary>
            /// chưa duyệt
            /// </summary>
            [Description("未审核")]
            Waiting = 0,

            /// <summary>
            /// đã duyệt
            /// </summary>
            [Description("已审核")]
            Audited = 1,

            /// <summary>
            /// đã từ chối
            /// </summary>
            [Description("已拒绝")]
            Refuse = 2,

            /// <summary>
            /// đã thu hồi
            /// </summary>
            [Description("已撤销")]
            Revoke = 3
        }

        /// <summary>
        /// cờ thực hiện điều chỉnh giá
        /// </summary>
        public enum EnumTjZxbz
        {
            /// <summary>
            /// chưa thực hiện
            /// </summary>
            [Description("未执行")]
            Not = 0,

            /// <summary>
            /// đã thực hiện
            /// </summary>
            [Description("已执行")]
            Already = 1
        }
        #endregion

        /// <summary>
        /// nhà sản xuất/nhà phân phối/khác  loại công ty
        /// </summary>
        public enum EnumSupplierType
        {
            /// <summary>
            /// khác
            /// </summary>
            [Description("其他")]
            Other = 0,

            /// <summary>
            /// nhà sản xuất
            /// </summary>
            [Description("生产商")]
            Producer = 1,

            /// <summary>
            /// nhà phân phối
            /// </summary>
            [Description("经销商")]
            Distributor = 2,
        }

        /// <summary>
        /// quá trình xử lý   1:chờ xử lý；2:duyệt đạt；3:duyệt không đạt；4:phân phối；5:hoàn thành một phần；6:hoàn thành；7:từ chối nhận
        /// </summary>
        public enum EnumApplyProcess
        {
            /// <summary>
            /// chờ xử lý
            /// </summary>
            [Description("等待处理")]
            Waiting = 1,

            /// <summary>
            /// duyệt đạt
            /// </summary>
            [Description("审核通过")]
            AuditApproved = 2,

            /// <summary>
            /// duyệt không đạt
            /// </summary>
            [Description("审核不通过")]
            AuditFailed = 3,

            /// <summary>
            /// đang phân phối
            /// </summary>
            [Description("配送中")]
            Distributing = 4,

            /// <summary>
            /// hoàn thành một phần
            /// </summary>
            [Description("部分完成")]
            PartialCompletion = 5,

            /// <summary>
            /// hoàn thành tất cả
            /// </summary>
            [Description("全部完成")]
            Completion = 6,

            /// <summary>
            /// từ chối phân phối
            /// </summary>
            [Description("配送拒收")]
            Rejection = 7,
        }

        /// <summary>
        /// loại chứng từ 1-đăng ký nhận của khoa；2-đăng ký nhận của kho
        /// </summary>
        public enum EnumApplyType
        {
            /// <summary>
            /// chờ xử lý
            /// </summary>
            [Description("科室申领")]
            DepartmentApply = 1,

            /// <summary>
            /// duyệt đạt
            /// </summary>
            [Description("库房申领")]
            WarehouseApply = 2,
        }

        /// <summary>
        /// quy trình xử lý đơn hàng mua hàng -1：từ chối xử lý； 0：chờ xử lý； 1：chuẩn bị hàng； 2：phân phối； 3：ký nhận； 4：hoàn thành； 5：từ chối ký 
        /// </summary>
        public enum EnumOrderProcess
        {
            /// <summary>
            /// từ chối xử lý
            /// </summary>
            [Description("拒处理")]
            Refusal = -1,

            /// <summary>
            /// chờ xử lý
            /// </summary>
            [Description("待处理")]
            Waiting = 0,

            /// <summary>
            /// chuẩn bị hàng
            /// </summary>
            [Description("备货")]
            PreparingGoods = 1,

            /// <summary>
            /// phân phối
            /// </summary>
            [Description("配送")]
            Delivering = 2,

            /// <summary>
            /// ký nhận
            /// </summary>
            [Description("签收")]
            SignFor = 3,

            /// <summary>
            /// hoàn thành
            /// </summary>
            [Description("完成")]
            Complete = 4,

            /// <summary>
            /// từ chối ký
            /// </summary>
            [Description("拒签")]
            RefusalSign = 5
        }

        /// <summary>
        /// loại đơn hàng 0：phiếu tạm lưu；1：phiếu chính thức
        /// </summary>
        public enum EnumOrderTypeHrp
        {

            /// <summary>
            /// phiếu tạm lưu
            /// </summary>
            [Description("暂存单")]
            TempOrder = 0,

            /// <summary>
            /// phiếu chính thức
            /// </summary>
            [Description("正式单")]
            OfficialOrder = 1,

            /// <summary>
            /// phiếu hủy
            /// </summary>
            [Description("作废单")]
            BadOrder = 2,
        }

        /// <summary>
        /// tính chất tự trả v2 
        /// sử dụng lại Trùng Khánh
        /// </summary>
        public enum EnumZFXZv2
        {
            /// <summary>
            /// tự trả
            /// </summary>
            [Description("自费")]
            ZF = 1,
            /// <summary>
            /// loại A
            /// </summary>
            [Description("甲类")]
            J = 4,
            /// <summary>
            /// loại B
            /// </summary>
            [Description("乙类")]
            Y = 5,
            /// <summary>
            /// loại C
            /// </summary>
            [Description("丙类")]
            B = 6,
        }


        #region 采购

        //4.1.1 loại thao tác
        public enum EnumCZLX
        {
            [Description("新增")] XZ = 1,
            [Description("作废")] ZF = 3,
        }

        //4.1.2 loại phiếu mua hàng
        public enum EnumCGDLX
        {
            [Description("医院自行采购单")] YYZXCGD = 1,
            [Description("托管药库采购单")] TGYKCGD = 2,
        }

        //4.1.3 loại mua hàng
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

        //4.1.4 yêu cầu phân phối
        public enum EnumPSYQ
        {
            [Description("按单配送")] ADPS = 1,
            [Description("按需配送")] AXPS = 2,
        }

        //4.1.5 loại trả hàng

        public enum EnumTHLX
        {
            [Description("正常退货")] ZCTH = 1,
            [Description("补差价退货")] BCJTH = 2,

        }

        //4.1.6 loại nghiệm thu phân phối
        public enum EnumPSYSLX
        {
            [Description("预验收")] YYS = 1,
            [Description("实验收")] SYS = 2,

        }

        //4.1.7 bên điền chứng từ
        public enum EnumDJTXF
        {
            [Description("采购方填写")]
            CGFTX = 1,
            [Description("销售方代填")]
            XSFDT = 2,
        }

        //4.1.8 trạng thái phiếu phân phối
        public enum EnumPSDZT
        {
            [Description("待确认")] DQR = 0,
            [Description("作废")] ZF = 1,
            [Description("已确认，未验收")] YQR = 10,

        }
        //4.1.9 trạng thái chi tiết mua hàng
        public enum EnumCGMXZT
        {
            [Description("待提交")] DTJ = 0,
            [Description("已作废")] YZF = 1,
            [Description("待审核")] DSH = 10,
            [Description("审核不通过")] SHBTG = 11,
            [Description("待企业接受确认")] DQYJSQR = 20,
            [Description("企业已接收确认配送")] QYYJSQRPS = 21,
            [Description("企业已接收确认不予配送")] QYYJSQRBYPS = 22,
            [Description("企业已配送完成")] QYYPSWC = 23,
        }

        //4.1.10 tồn kho doanh nghiệp
        public enum EnumQYKC
        {
            [Description("充足")] CZ = 1,
            [Description("有货， 限量")] YHXL = 2,
            [Description("有货， 少量")] YHSL = 3,
            [Description("暂无货")] ZWH = 4,
            [Description("不予披露")] BYPL = 5,
        }

        //4.1.11 trạng thái hóa đơn
        public enum EnumFPZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("未验收")] WYS = 10,
            [Description("已验收")] YYS = 20,
            [Description("已拒收")] YJS = 21,
            [Description("已支付")] FPYZF = 30,
        }

        //4.1.12 trạng thái chi tiết phân phối
        public enum EnumPSMXZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("已提交未验收")] YTJWYS = 10,
            [Description("验收中")] YSZ = 20,
            [Description("验收入库完成")] YSRKWC = 30,
        }

        //4.1.13 trạng thái chi tiết trả hàng
        public enum EnumTHMXZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("已确认待企业接受")] YQRDQYJS = 10,
            [Description("企业已确认退货处理")] QYYQRTHCL = 20,
            [Description("企业已确认不予处理")] QYYQRBYCL = 21,
        }

        //4.1.14 loại giấy tờ
        public enum EnumZJLX
        {
            [Description("组织机构代码证")] ZZJGDMZ = 1,
            [Description("营业执照")] YYZZ = 2,
            [Description("经营许可证")] JYXKZ = 3,
            [Description("生产许可证")] SCXKZ = 4,
        }

        //4.1.15 loại mã vạch chi tiết phân phối
        public enum EnumPSMXTMLX
        {
            [Description("GS1 条码")] GSI = 1,
            [Description("HIBC 条码")] HIBC = 2,
            [Description("其他")] QT = 99,
        }

        //4.1.16 kết quả nghiệm thu hóa đơn
        public enum EnumFPYSJG
        {
            [Description("验收通过")] YSTG = 1,
            [Description("验收不通过")] YSBTG = 0,
        }

        //4.1.17 trạng thái phiếu mua hàng
        public enum EnumCGDZT
        {
            [Description("待提交")] DTJ = 0,
            [Description("已作废")] YZF = 1,
            [Description("已提交")] YTJ = 20,
        }

        //4.1.18 trạng thái phiếu trả hàng
        public enum EnumTHDZT
        {
            [Description("待确认")] DQR = 0,
            [Description("已作废")] YZF = 1,
            [Description("已确认")] YQR = 10,

        }

        //4.1.19 phương thức mua hàng

        public enum EnumCGFS
        {
            [Description("非系统采购")]
            FXTCG = 1,
            [Description("按系统采购")]
            AXTCG = 2,
        }

        //4.1.20 có bao gồm dịch vụ kèm theo hay không
        public enum EnumSFHBSFW
        {
            [Description("否")] F = 0,
            [Description("是")] S = 1,
        }


        #endregion
    }
    #endregion
}
