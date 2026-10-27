using System.ComponentModel;

namespace NewtouchHIS.Base.Domain.EnumExtend
{
    public class ClientEnum
    {

    }
    public class SettClientEnum : ClientEnum
    {
        /// <summary>
        /// Loại ghi nhật ký Mở rộng mục enum
        /// ValueVui lòng bắt đầu từ101Bắt đầu
        /// </summary>
        public enum DbLogType_Ex
        {
            /// <summary>
            /// Quyết toán xuất viện
            /// </summary>
            [Description("Quyết toán xuất viện")]
            DischargeSettlement = 101,

        }

        /// <summary>
        /// Cờ đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// Đăng ký nhập viện
            /// </summary>
            [Description("Nhập viện mới")]
            Xry = 0,
            /// <summary>
            /// Đang ở khu bệnh
            /// </summary>
            [Description("Đang ở khu bệnh")]
            Bqz = 1,
            /// <summary>
            /// Xuất khỏi khu bệnh(Ra khỏi khu bệnh)(Chờ quyết toán)
            /// </summary>
            [Description("Chờ quyết toán")]
            Djz = 2,
            /// <summary>
            /// Đã xuất viện(Quyết toán xuất viện)
            /// </summary>
            [Description("Đã xuất viện")]
            Ycy = 3,
            /// <summary>
            /// Đang chuyển khu
            /// </summary>
            [Description("Đang chuyển khu")]
            Zq = 7,

            /// <summary>
            /// Hồ sơ hủy/Hủy nhập viện
            /// </summary>
            [Description("Hồ sơ hủy")]
            Wry = 9,
        }

        /// <summary>
        /// Loại tài khoản hệ thống
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
            /// Tài khoản giường tại nhà
            /// </summary>
            [Description("Tài khoản giường tại nhà")]
            JCZH = 2,
            /// <summary>
            /// Tài khoản tạm ứng nội trú
            /// </summary>
            [Description("Tài khoản tạm ứng nội trú")]
            ZYYJKZH = 3,
            /// <summary>
            /// Tài khoản hỗ trợ
            /// </summary>
            [Description("Tài khoản hỗ trợ")]
            BKZH = 4,
            /// <summary>
            /// Tài khoản đăng ký ngoại trú
            /// </summary>
            [Description("Tài khoản đăng ký ngoại trú")]
            MZDJZH = 5,
            /// <summary>
            /// Tài khoản tạm ứng ngoại trú
            /// </summary>
            [Description("Tài khoản tạm ứng ngoại trú")]
            MZYJKZH = 6
        }

        /// <summary>
        /// Loại giao dịch BHYT  0 Không giao dịch 1 Giao dịch thông thường 2 Giao dịch bệnh hiểm nghèo 3 Giao dịch giường tại nhà 4 Giao dịch tai nạn lao động 5 Giao dịch nội trú 6 Giao dịch bảo hiểm nông nghiệp
        /// xt_brxz.ybjylx
        /// </summary>
        public enum EnumYBJYLX
        {
            /// <summary>
            /// Không giao dịch
            /// </summary>
            [Description("Không giao dịch")]
            ybjylx0 = 0,
            /// <summary>
            /// Giao dịch thông thường
            /// </summary>
            [Description("Giao dịch thông thường")]
            ybjylx1 = 1,
            /// <summary>
            /// Giao dịch bệnh hiểm nghèo
            /// </summary>
            [Description("Giao dịch bệnh hiểm nghèo")]
            ybjylx2 = 2,
            /// <summary>
            /// Giao dịch giường tại nhà
            /// </summary>
            [Description("Giao dịch giường tại nhà")]
            ybjylx3 = 3,
            /// <summary>
            /// Giao dịch tai nạn lao động
            /// </summary>
            [Description("Giao dịch tai nạn lao động")]
            ybjylx4 = 4,
            /// <summary>
            /// Giao dịch nội trú
            /// </summary>
            [Description("Giao dịch nội trú")]
            ybjylx5 = 5,
            /// <summary>
            /// Giao dịch bảo hiểm nông nghiệp
            /// </summary>
            [Description("Giao dịch bảo hiểm nông nghiệp")]
            ybjylx6 = 6,
        }

        /// <summary>
        /// Tính chất tự trả(Thuốc,Thuộc tính của từ điển hạng mục)
        /// </summary>
        public enum EnumZiFuXingZhi
        {
            /// <summary>
            /// Được thanh toán
            /// </summary>
            [Description("Được thanh toán")]
            KB = 0,
            /// <summary>
            /// Tự chi trả
            /// </summary>
            [Description("Tự chi trả")]
            ZF = 1,
            /// <summary>
            /// Tự trả theo phân loại
            /// </summary>
            [Description("Tự trả theo phân loại")]
            FLZF = 2,
            /// <summary>
            /// Hoàn toàn tự lo
            /// </summary>
            [Description("Hoàn toàn tự lo")]
            JueDuiZiLi = 3,
            /// <summary>
            /// Hạng A
            /// </summary>
            [Description("Hạng A")]
            Jia,
            /// <summary>
            /// Hạng B
            /// </summary>
            [Description("Hạng B")]
            Yi,
            /// <summary>
            /// Hạng C
            /// </summary>
            [Description("Hạng C")]
            Bing,
        }

        /// <summary>
        /// Thuật toán Tính chất tự trả
        /// Xác định tính chất của phần tự trả tính theo tỷ lệ tự trả
        /// </summary>
        public enum EnumSuanFaZFXZ
        {
            /// <summary>
            /// Tự trả(Tiền mặt tự trả sau giao dịch BHYT)
            /// </summary>
            [Description("Tự trả")]
            ZF = 0,
            /// <summary>
            /// Tự lo
            /// </summary>
            [Description("Tự lo")]
            ZL = 1,
        }

        /// <summary>
        /// Tính chất thu chi tài khoản 
        /// xt_brzhszjl.szxz
        /// </summary>
        public enum AccSzxz
        {
            [Description("Gửi/rút tài khoản ngoại trú")]
            MZZH = 0,
            [Description("Ghi sổ giường tại nhà")]
            JCZH = 1,
            [Description("Quyết toán ngoại trú")]
            MZJS = 2,
            [Description("Ghi sổ khu bệnh")]
            BQJZ = 3,
            [Description("Quyết toán nội trú")]
            ZYJS = 4,
            [Description("Gửi/rút tài khoản nội trú")]
            ZYZH = 5,
            [Description("Bù khoản nội trú")]
            ZYBJK = 6,
            [Description("Đăng ký ghi sổ ngoại trú")]
            MZJZDJ = 7
        }

        /// <summary>
        /// Loại thẻ/Mã định danh thân phận thẻ
        /// </summary>
        [Description("Loại thẻ")]
        public enum EnumCardType
        {
            /// <summary>
            /// Thẻ từ BHYT
            /// </summary>
            [Description("Thẻ từ BHYT")]
            CTK = 0,
            /// <summary>
            /// Thẻ ảo/Thẻ trong viện(Thẻ ảo khi tự động tạo/Thẻ trong viện khi nhập tay)
            /// </summary>
            [Description("Thẻ ảo")]
            XNK = 1,
            /// <summary>
            /// Thẻ bảo hiểm xã hội
            /// </summary>
            [Description("Thẻ bảo hiểm xã hội")]
            YBJYK = 2,
            /// <summary>
            /// Chứng từ điện tử BHYT
            /// </summary>
            [Description("Chứng từ điện tử BHYT")]
            ybdzpz = 3,
            /// <summary>
            /// Khám BHYT bằng CMND
            /// </summary>
            [Description("CMND(BHYT)")]
            sfz = 4,
            /// <summary>
            /// Mã sức khỏe
            /// </summary>
            [Description("Mã sức khỏe")]
            XNHJYK = 5,
        }

        /// <summary>
        /// Loại ngoại trú mjzbz
        /// </summary>
        public enum EnumOutPatientType
        {
            /// <summary>
            /// Khám ngoại trú thông thường
            /// </summary>
            [Description("Khám ngoại trú thông thường")]
            generalOutpat = 1,
            /// <summary>
            /// Cấp cứu
            /// </summary>
            [Description("Cấp cứu")]
            emerDiagnosis = 2,
            /// <summary>
            /// Khám chuyên gia
            /// </summary>
            [Description("Khám chuyên gia")]
            expertOutpat = 3,
            /// <summary>
            /// Khám bệnh đặc biệt
            /// </summary>
            [Description("Khám bệnh đặc biệt")]
            SpecialOutpat = 4,
            /// <summary>
            /// Khám ngoại trú bệnh hiểm nghèo
            /// </summary>
            [Description("Khám ngoại trú bệnh hiểm nghèo")]
            MajorDiseases = 5,
            /// <summary>
            /// Khám bệnh mạn tính
            /// </summary>
            [Description("Khám bệnh mạn tính")]
            ChronicDisease = 6,
            /// <summary>
            /// Hai bệnh cư dân
            /// </summary>
            [Description("Hai bệnh cư dân")]
            jmlb = 7,
            /// <summary>
            /// Khám ngoại trú tai nạn bất ngờ
            /// </summary>
            [Description("Khám ngoại trú tai nạn bất ngờ")]
            ywshmz = 8,
            /// <summary>
            /// Khám ngoại trú sinh sản
            /// </summary>
            [Description("Khám ngoại trú sinh sản")]
            symz = 9,
            /// <summary>
            /// Khám ngoại trú lao kháng đa thuốc
            /// </summary>
            [Description("Khám ngoại trú lao kháng đa thuốc")]
            ndyjhmz = 10,
            /// <summary>
            /// Khám ngoại trú hai bệnh trẻ em
            /// </summary>
            [Description("Khám ngoại trú hai bệnh trẻ em")]
            etlbmz = 11
        }

        /// <summary>
        /// Cờ ngoại trú/nội trú  0 Chung 1 Chỉ ngoại trú 2 Chỉ nội trú 3 Hệ thống(Cả ngoại trú và nội trú đều không dùng được)Ví dụ:Giường tại nhà
        /// Có định nghĩa trùng lặp
        /// </summary>
        public enum Enummzzybz
        {
            /// <summary>
            /// Chung
            /// </summary>
            [Description("Chung")]
            ty = 0,
            /// <summary>
            /// Chỉ ngoại trú
            /// </summary>
            [Description("Chỉ ngoại trú")]
            mz = 1,
            /// <summary>
            /// Chỉ nội trú
            /// </summary>
            [Description("Chỉ nội trú")]
            zy = 2,
            /// <summary>
            /// Hệ thống
            /// </summary>
            [Description("Hệ thống")]
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
            /// Đã quyết toán(Chưa trả)
            /// </summary>
            [Description("Đã quyết toán")]
            YJ = 1,
            /// <summary>
            /// Đã trả
            /// </summary>
            [Description("Đã trả")]
            YT,
        }

        /// <summary>
        /// Tính chất y lệnh
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
        /// Tính chất đăng ký khám 
        /// 0-Đăng ký khám thường
        /// 1-Đăng ký khám rỗng
        /// 2-Đăng ký khám giường tại nhà
        /// 3-Đăng ký khám chuyển tự trả sang BHYT
        /// 4-Đăng ký khám ghi sổ ngoại trú
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
            /// Đăng ký khám
            /// </summary>
            [Description("Thu phí đăng ký khám")]
            GH = 0,
            /// <summary>
            /// 
            /// </summary>
            [Description("Ghi sổ giường tại nhà")]
            JCJZ = 1,
            /// <summary>
            /// Ghi sổ ngoại trú
            /// </summary>
            [Description("Ghi sổ ngoại trú")]
            MZJZ = 2,
            /// <summary>
            /// 
            /// </summary>
            [Description("Nội trú")]
            ZY = 3,
            /// <summary>
            /// 
            /// </summary>
            [Description("ZHSZ")]
            ZHSZ = 4
        }

        ///// <summary>
        /////Tính chất y lệnh 1 Y lệnh tạm thời   2 Y lệnh dài hạn   3 Y lệnh xuất viện
        ///// </summary>
        //public enum EnumYZXZ
        //{
        //    /// <summary>
        //    /// Y lệnh tạm thời
        //    /// </summary>
        //    LSYZ = 1,
        //    /// <summary>
        //    /// Y lệnh dài hạn
        //    /// </summary>
        //    CQYZ = 2,
        //    /// <summary>
        //    /// Y lệnh xuất viện
        //    /// </summary>
        //    CYYZ = 3
        //}


        /// <summary>
        ///Trạng thái y lệnh 1 Chưa thu hồi   2 Đã thu hồi
        /// </summary>
        public enum EnumYZZT
        {
            /// <summary>
            /// Chưa thu hồi
            /// </summary>
            WCX = 1,
            /// <summary>
            /// Đã thu hồi
            /// </summary>
            YCX = 2
        }

        /// <summary>
        /// Loại hạng mục thu phí ghi sổ nội trú
        /// </summary>
        public enum EnumxmType
        {
            XM = 2,//Thuốc
            YP = 1 //Hạng mục
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
        /// Cách in 1:In bù  2:In lại 3:In hóa đơn
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
            /// In hóa đơn
            /// </summary>
            Print = 3
        }

        /// <summary>
        /// Loại đăng ký khám Theo yêu cầu đặc biệt Theo yêu cầu đặc biệtV
        /// </summary>
        [Obsolete("Mã của các cơ sở y tế khác nhau thì khác nhau")]
        public enum Enumghlx
        {
            TeXu = 7884,
            TeXuV = 7889,
        }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        public enum EnumSXBZ
        {
            /// <summary>
            /// Có hiệu lực ngay
            /// </summary>
            [Description("Có hiệu lực ngay")]
            Jssx = 0,
            /// <summary>
            /// Có hiệu lực tại điểm định sẵn
            /// </summary>
            [Description("Có hiệu lực tại điểm định sẵn")]
            Ddsx = 1

        }

        /// <summary>
        /// Cờ khu vực
        /// </summary>
        public enum EnumDY
        {
            /// <summary>
            /// Tại địa phương
            /// </summary>
            [Description("Tại địa phương")]
            bd = 0,
            /// <summary>
            /// Ngoài địa phương
            /// </summary>
            [Description("Ngoài địa phương")]
            wd = 1

        }

        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public enum EnumHF
        {
            /// <summary>
            /// Chưa kết hôn
            /// </summary>
            [Description("Chưa kết hôn")]
            wh = 0,
            /// <summary>
            /// Đã kết hôn
            /// </summary>
            [Description("Đã kết hôn")]
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
            [Description("Nội trú thông thường")]
            mz = 21,
            /// <summary>
            /// Cấp cứu
            /// </summary>
            [Description("Cấp cứu chuyển nội trú")]
            jz = 24,
            /// <summary>
            /// Chuyển đến từ cơ sở y tế khác
            /// </summary>
            [Description("Chuyển vào nội trú")]
            qtyljg = 9925,
            /// <summary>
            /// Chuyển ra ngoài điều trị nội trú
            /// </summary>
            [Description("Chuyển ra ngoài điều trị nội trú")]
            zwzzzy = 23,
            /// <summary>
            /// Nội trú bệnh hiểm nghèo
            /// </summary>
            [Description("Nội trú bệnh hiểm nghèo")]
            dbry = 9902,
            /// <summary>
            /// Nội trú sinh sản
            /// </summary>
            [Description("Nội trú sinh sản")]
            syzy = 52,
            /// <summary>
            /// Nội trú hai bệnh trẻ em
            /// </summary>
            [Description("Nội trú hai bệnh trẻ em")]
            etlb = 9904,
            /// <summary>
            /// Nội trú lao kháng đa thuốc
            /// </summary>
            [Description("Nội trú lao kháng đa thuốc")]
            ndyzy = 9907,
            /// <summary>
            /// Trẻ sơ sinh nội trú theo mẹ
            /// </summary>
            [Description("Trẻ sơ sinh nội trú theo mẹ")]
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
            /// CMND
            /// </summary>
            [Description("CMND")]
            sfz = 1,
            /// <summary>
            /// Hộ chiếu
            /// </summary>
            [Description("Hộ chiếu")]
            hz,
            /// <summary>
            /// Chứng nhận sĩ quan
            /// </summary>
            [Description("Chứng nhận sĩ quan")]
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
            /// Thông thường
            /// </summary>
            [Description("Thông thường")]
            pt = 1,
            /// <summary>
            /// Nghỉ hưu theo chế độ
            /// </summary>
            [Description("Nghỉ hưu theo chế độ")]
            lx,
            /// <summary>
            /// Thương tật
            /// </summary>
            [Description("Thương tật")]
            sc

        }

        /// <summary>
        /// Phương pháp BHYT áp dụng 0:Bảo hiểm thành phố 1:BHYT cá thể 2:Thị trấn nhỏ 3:Lao động theo giờ
        // A:Hỗ trợ y tế dân chính
        // B:Di chúc người cao tuổi C:Người cao tuổi diện ngũ bảo D:Người khuyết tật nặng
        // E:Học sinh tiểu học, trung học và trẻ sơ sinh F: Cư dân khác
        //G:Đối tượng hỗ trợ tương trợ y tế
        /// </summary>
        public enum EnumSYYBBF
        {
            /// <summary>
            /// Bảo hiểm thành phố
            /// </summary>
            [Description("Bảo hiểm thành phố")]
            cb = 1,
            /// <summary>
            /// BHYT cá thể
            /// </summary>
            [Description("BHYT cá thể")]
            gtyb,
            /// <summary>
            /// Thị trấn nhỏ
            /// </summary>
            [Description("Thị trấn nhỏ")]
            xcz,
            /// <summary>
            /// Khác
            /// </summary>
            [Description("Khác")]
            qt = 9

        }

        /// <summary>
        /// Thời gian lịch đăng ký khám
        /// </summary>
        public enum EnumRegSchedule
        {
            /// <summary>
            /// Không có
            /// </summary>
            [Description("Không có")]
            none = 0,
            /// <summary>
            /// Cả ngày
            /// </summary>
            [Description("Cả ngày")]
            allday = 1,
            /// <summary>
            /// Buổi sáng
            /// </summary>
            [Description("Buổi sáng")]
            AM = 2,
            /// <summary>
            /// Buổi chiều
            /// </summary>
            [Description("Buổi chiều")]
            PM = 3,

        }

        /// <summary>
        /// Trạng thái thực thi kế hoạch ghi sổ
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
        /// Loại nhắc nhở
        /// </summary>
        public enum EnmuReminderType
        {
            /// <summary>
            /// Nhắc nhở giới hạn số tiền
            /// </summary>
            [Description("Nhắc nhở giới hạn số tiền")]
            金额上限提醒 = 1,    //Ví dụ: Nhà trị liệu trong một tháng thu phí không được vượt quá một trăm nghìn
        }

        /// <summary>
        /// Đồng bộ hồ sơ điều trị Trạng thái xử lý
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
            /// Hủy
            /// </summary>
            [Description("Hủy")]
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
            [Description("Đăng nhập một lần (SSO)")]
            SSO = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("Đăng nhập hệ thống")]
            App = 2
        }

        /// <summary>
        /// Chiến lược định giá hạng mục thu phí
        /// </summary>
        public enum EnumSfxmJjcl
        {
            /// <summary>
            /// Theo thời lượng
            /// </summary>
            [Description("Theo thời lượng")]
            Time = 1,
            /// <summary>
            /// Theo số lượng
            /// </summary>
            [Description("Theo số lượng")]
            Amount,
            /// <summary>
            /// Theo diện tích
            /// </summary>
            [Description("Theo diện tích")]
            Acreage,
        }

        /// <summary>
        /// Trạng thái khám của bệnh nhân ngoại trú(Cờ khám bệnh)
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
        /// Kiểu enum
        /// </summary>
        public enum EnumPrescriptionType
        {
            /// <summary>
            /// Thuốc
            /// </summary>
            [Description("Thuốc")]
            Medicine = 1,
            /// <summary>
            /// Hạng mục điều trị
            /// </summary>
            [Description("Hạng mục điều trị")]
            Treament,
        }

        /// <summary>
        /// Tính chất thu chi
        /// </summary>
        public enum EnumSZXZ
        {
            [Description("Nạp tiền")]
            cz = 1,
            [Description("Rút tiền")]
            qk = 2,
            [Description("Quyết toán ngoại trú")]
            mzjs = 3,
            [Description("Trả lại quyết toán ngoại trú")]
            mzjsth = 4,
            [Description("Quyết toán nội trú")]
            zyjs = 5,
            [Description("Trả lại quyết toán nội trú")]
            zyjsth = 6,
            [Description("Trả số dư")]
            tye = 9
        }

        /// <summary>
        /// Mức độ nguy kịch
        /// </summary>
        public enum EnumWzjb
        {
            /// <summary>
            /// Thông thường
            /// </summary>
            [Description("Thông thường")]
            Normal = 1,
            /// <summary>
            /// Bệnh nặng
            /// </summary>
            [Description("Bệnh nặng")]
            Serious = 2,
            /// <summary>
            /// Nguy kịch
            /// </summary>
            [Description("Nguy kịch")]
            Bedying = 3
        }

        public enum EnumOrgshzt
        {
            /// <summary>
            /// Duyệt đạt
            /// </summary>
            [Description("Đạt")]
            TG = 1,
            /// <summary>
            /// Duyệt chưa đạt
            /// </summary>
            [Description("Chưa đạt")]
            WTG = 2,
            /// <summary>
            /// Chờ duyệt
            /// </summary>
            [Description("Chờ duyệt")]
            DS = 3,
            /// <summary>
            /// Chưa nộp
            /// </summary>
            [Description("Chưa nộp")]
            WTJ = 4,
            /// <summary>
            /// Nộp trước
            /// </summary>
            [Description("Nộp trước")]
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
        /// Enum-Đơn vị tuổi
        /// </summary>
        public enum EnumAgeUnit
        {
            Year = 1,
            Month = 2,
            Day = 3,
            Hour = 4,
        }
        /// <summary>
        /// Trạng thái đặt hẹn ngoại trú
        /// </summary>
        public enum EnumMzyyzt
        {
            [Description("Đã hẹn")]
            book = 1,
            [Description("Đã đăng ký khám")]
            reg = 2,
            [Description("Đặt hẹn đã hủy")]
            cancel = 3,
            [Description("Đăng ký khám trong ngày")]
            bookreg = 4,
            [Description("Hủy đăng ký khám")]
            regcancel = 5
        }
        /// <summary>
        /// Loại lịch ngoại trú
        /// </summary>
        public enum EnumMzpblx
        {
            [Description("Tất cả")]
            all = 1,
            [Description("Mở đặt hẹn")]
            yy = 2,
            [Description("Không đặt hẹn")]
            fyy = 3
        }
        /// <summary>
        /// Mở đặt hẹn
        /// </summary>
        public enum EnumMzpbBook
        {
            [Description("Có")]
            Y = 1,
            [Description("Không")]
            F = 0,
        }
        /// <summary>
        /// Trạng thái đơn thuốc
        /// </summary>
        public enum EnumZfzt
        {

            [Description("Đơn thuốc bình thường")]
            zc = 0,
            [Description("Đơn thuốc trả thuốc hoặc hủy khác")]
            ty = 1
        }

        /// <summary>
        /// Trạng thái cấp cứu
        /// GốcEnumJzzt
        /// </summary>
        public enum EnumEJzzt
        {
            [Description("Không")]
            zc = 0,
            [Description("Có")]
            ty = 1
        }
        /// <summary>
        /// Nguồn đăng ký khám ngoại trú
        /// </summary>
        public enum EnumMzghly
        {
            [Description("His")]
            His = 0,
            [Description("Đặt hẹn")]
            Yy = 1,
            [Description("Ngân y thông")]
            Yyt = 3,
            [Description("WeChat")]
            WeChat = 4,
            [Description("Alipay")]
            Alipay = 5,
            [Description("Khám sức khỏe")]
            Tj = 6,
            [Description("Máy tự phục vụ")]
            SelfTerminal = 7,
        }
        /// <summary>
        /// Thuộc tính BHYT
        /// </summary>
        public enum EnumCblb
        {
            [Description("Tất cả")]
            all = 0,
            [Description("Công nhân viên chức")]
            zg = 1,
            [Description("Cư dân")]
            jm = 2,
            [Description("Nghỉ hưu theo chế độ")]
            lx = 3,
            [Description("Công nhân viên chức ngoài địa phương")]
            ydzg = 4,
            [Description("Cư dân ngoài địa phương")]
            ydjm = 5,

        }

        public enum EnumRportType
        {
            [Description("Bảng kê khai chi phí công nhân viên chức(Cứu trợ dân chính)")]
            zgmzjz = 1,
            [Description("Bảng đăng ký chi phí cư dân(Cứu trợ dân chính)")]
            jmmzjz = 2,
            [Description("Báo cáo quyết toán thông thường(Công nhân viên chức)")]
            zgbz = 3,
            [Description("Báo cáo quyết toán thông thường(Cư dân)")]
            jmbz = 4,
            [Description("Báo cáo quyết toán thông thường(Nghỉ hưu theo chế độ)")]
            lxbz = 5,
            [Description("Báo cáo quyết toán thông thường(Công nhân viên chức ngoài địa phương)")]
            ydzgbz = 6,
            [Description("Báo cáo quyết toán thông thường(Cư dân ngoài địa phương)")]
            ydjmbz = 7,

        }
        /// <summary>
        /// Loại nhân viên
        /// </summary>
        public enum EnumRylb
        {
            [Description("Công nhân viên chức đang làm việc")]
            zgzz = 1101,
            [Description("Công chức đang làm việc")]
            gwyzz = 1102,
            [Description("Người làm việc linh hoạt đang làm việc")]
            lhjyryzz = 1103,
            [Description("Nhân viên mở rộng khác của địa phương")]
            qtdfkzry = 1160,
            [Description("Công nhân viên chức nghỉ hưu")]
            zgtx = 1201,
            [Description("Công chức nghỉ hưu")]
            gwytx = 1202,
            [Description("Người làm việc linh hoạt nghỉ hưu")]
            lhjyrytx = 1203,
            [Description("Nhân viên mở rộng khác của địa phương")]
            qtdfkzrytx = 1260,
            [Description("Người nghỉ hưu theo chế độ")]
            lxry = 1300,
            [Description("Nhân viên mở rộng khác của địa phương")]
            lxdfqtkzry = 1360,
            [Description("Trẻ sơ sinh")]
            xse = 1401,
            [Description("Trẻ trước tuổi đi học")]
            xlqet = 1402,
            [Description("Học sinh tiểu học và trung học")]
            zxxs = 1403,
            [Description("Sinh viên đại học")]
            dxs = 1404,
            [Description("Chưa thành niên(Chưa nhập học)")]
            wcn = 1405,
            [Description("Thân phận mở rộng khác của địa phương")]
            dfqtgkzf = 1460,
            [Description("Cư dân thông thường(Người trưởng thành)")]
            ptjm = 1501,
            [Description("Thân phận mở rộng khác của địa phương")]
            jmdfqtgzsf = 1560,
            [Description("Cư dân(Người cao tuổi)")]
            jmln = 16,
            [Description("Cư dân(Người trưởng thành)")]
            jmcn = 15,
            [Description("Cư dân(Chưa thành niên)")]
            jmwcn = 14,
            [Description("Nghỉ hưu theo chế độ")]
            lx = 13,
            [Description("Người nghỉ hưu")]
            txry = 12,
            [Description("Đang làm việc")]
            zz = 11

        }
        public enum EnumYbQuery
        {
            [Description("Hưởng chế độ nhân viên")]
            rydyxs = 1,
            [Description("Truy vấn thông tin khám")]
            jzxxcx = 2,
            [Description("Truy vấn thông tin chẩn đoán")]
            zdxxcx = 3,
            [Description("Truy vấn thông tin quyết toán")]
            jsxxcx = 4,
            [Description("Truy vấn chi tiết chi phí")]
            fymxcx = 5,
            [Description("Truy vấn hồ sơ dùng thuốc bệnh mạn tính, đặc biệt của nhân viên")]
            rymtbyyjl = 6,
            [Description("Truy vấn thông tin tích lũy của nhân viên")]
            ryljxxcx = 7,
            [Description("Truy vấn hồ sơ bệnh đặc biệt của nhân viên")]
            rytsbbacx = 8,
            [Description("Truy vấn thông tin điểm định sẵn của nhân viên")]
            ryddxxcx = 9,
            [Description("Truy vấn thông tin đang nằm viện")]
            zyxxcx = 10,
            [Description("Truy vấn thông tin chuyển viện")]
            zyxx = 11,

        }
        /// <summary>
        /// Loại kiểm tra chế độ
        /// </summary>
        public enum Enumdyjclx
        {
            [Description("Kiểm tra khoản quỹ")]
            jjxjc = 1,
            [Description("Kiểm tra khác")]
            qtjc = 99
        }
        /// <summary>
        /// Loại chi trả quỹ
        /// </summary>
        public enum Enumjjzflx
        {
            [Description("Quỹ điều phối BHYT cơ bản cho công nhân viên chức thành thị")]
            tcjj = 310100,
            [Description("Quỹ tài khoản cá nhân BHYT cơ bản cho công nhân viên chức thành thị")]
            grzhjj = 310200,
            [Description("Quỹ trợ cấp y tế công chức")]
            gwybzjj = 320100,
            [Description("Quỹ trợ cấp chi phí y tế lớn")]
            debzjj = 330100,
            [Description("Quỹ bảo đảm y tế cho người nghỉ hưu theo chế độ")]
            lxbzjj = 340100,
            [Description("Quỹ trợ cấp y tế cho quân nhân khuyết tật cấp một đến sáu")]
            cjjrbzjj = 350100,
            [Description("Quỹ BHYT bổ sung doanh nghiệp")]
            qybxjj = 370100,
            [Description("Quỹ bệnh viện ứng trước")]
            yydfjj = 999996,
            [Description("Quỹ công chức cấp quận")]
            qgwyjj = 999901,
            [Description("Quỹ BHYT cơ bản cho cư dân thành thị và nông thôn")]
            cxjmbxjj = 390100,
            [Description("Quỹ BHYT bệnh hiểm nghèo cho cư dân thành thị và nông thôn")]
            cxjmdbbxjj = 390200,
            [Description("Quỹ sinh sản")]
            syjj = 510100,
            [Description("Quỹ sinh sản công nhân viên chức")]
            zgsyjj = 310103,
            [Description("Quỹ cứu trợ y tế")]
            yljzjj = 610100,
            [Description("Quỹ tái cứu trợ y tế")]
            ylzjzjj = 610102,
            [Description("Quỹ xóa đói giảm nghèo vì sức khỏe")]
            jkpfjj = 610103,
            [Description("Quỹ thoát nghèo chính xác")]
            jztpjj = 610104,
            [Description("Quỹ khác")]
            qtjj = 999997
        }
        /// <summary>
        /// Cờ hưởng chế độ khoản quỹ
        /// </summary>
        public enum Enumdyxsbz
        {
            [Description("Không")]
            s = 0,
            [Description("Có")]
            f = 1
        }
        /// <summary>
        /// Loại chẩn đoán
        /// </summary>
        public enum Enumzdlx
        {
            [Description("Chẩn đoán tây y")]
            xyzd = 1,
            [Description("Chẩn đoán bệnh chính y học cổ truyền")]
            zyzzd = 2,
            [Description("Chẩn đoán chứng chính y học cổ truyền")]
            zyzzzd = 3
        }
        /// <summary>
        /// Loại hình bảo hiểm
        /// </summary>
        public enum Enumxzlx
        {
            [Description("BHYT cơ bản cho công nhân viên chức")]
            zgjbylbx = 310,
            [Description("Trợ cấp y tế công chức")]
            gwyylbz = 320,
            [Description("Trợ cấp chi phí y tế lớn")]
            deylfybz = 330,
            [Description("Bảo đảm y tế cho người nghỉ hưu theo chế độ")]
            lxryylbz = 340,
            [Description("Trợ cấp y tế cho quân nhân tàn tật cấp một đến sáu")]
            cfjrylbz = 350,
            [Description("Bảo đảm y tế cho lão Hồng quân")]
            lhjylbz = 360,
            [Description("BHYT bổ sung doanh nghiệp")]
            qybcylbx = 370,
            [Description("Hợp tác y tế nông thôn kiểu mới")]
            xxnchzyl = 380,
            [Description("BHYT cơ bản cho cư dân thành thị và nông thôn")]
            cxjmjbylbx = 390,
            [Description("BHYT cơ bản cho cư dân thành thị")]
            czjmjbylbx = 391,
            [Description("BHYT bệnh hiểm nghèo cho cư dân thành thị và nông thôn")]
            cxjmdbbx = 392,
            [Description("Bảo đảm y tế cho nhân viên đặc biệt khác")]
            qttsryylbz = 399,
            [Description("Bảo hiểm chăm sóc dài hạn")]
            cqzhbx = 410,
            [Description("Bảo hiểm sinh sản")]
            sybx = 510
        }
        /// <summary>
        /// Loại y tế
        /// </summary>
        public enum Enumyllb
        {
            [Description("Khám ngoại trú thông thường")]
            ptmz = 11,
            [Description("Đăng ký khám ngoại trú")]
            mzgh = 12,
            [Description("Cấp cứu")]
            jz = 13,
            [Description("Bệnh mạn tính, đặc biệt ngoại trú")]
            mzmtb = 14,
            [Description("Khám ngoại trú tai nạn bất ngờ")]
            ywshmz = 19,
            [Description("Nội trú thông thường")]
            ptzy = 21,
            [Description("Chuyển ra ngoài điều trị nội trú")]
            zwzzzy = 23,
            [Description("Cấp cứu chuyển nội trú")]
            jzzzy = 24,
            [Description("Khám ngoại trú sinh sản")]
            symz = 51,
            [Description("Nội trú sinh sản")]
            syzy = 52,
            [Description("Phí phẫu thuật kế hoạch hóa gia đình")]
            jhsyssf = 53,
            [Description("Khám ngoại trú bệnh hiểm nghèo")]
            zdjbmz = 9901,
            [Description("Nội trú bệnh hiểm nghèo")]
            zdjbzy = 9902,
            [Description("Khám ngoại trú hai bệnh trẻ em")]
            etlbmz = 9903,
            [Description("Nội trú hai bệnh trẻ em")]
            etlbzy = 9904,
            [Description("Khám ngoại trú lao kháng đa thuốc")]
            ndyjhmz = 9906,
            [Description("Nội trú lao kháng đa thuốc")]
            ndyjhzy = 9907,
            [Description("Chuyển vào nội trú")]
            zrzy = 9925,
            [Description("Trẻ sơ sinh nội trú theo mẹ")]
            xsesmzy = 9921
        }
        /// <summary>
        /// Nguồn kê khai
        /// </summary>
        public enum Enumsbly
        {
            [Description("Cơ sở y dược điểm định sẵn")]
            etlbmz = 01,
            [Description("Hệ thống xử lý của trung tâm")]
            etlbzy = 02,
            [Description("Hệ thống xử lý trực tuyến")]
            ndyjhmz = 03,
            [Description("APP")]
            ndyjhzy = 04,
            [Description("Máy tích hợp")]
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
            /// Sinh thường
            /// </summary>
            [Description("Sinh thường")]
            Zcc = 1,
            /// <summary>
            /// Sinh có hỗ trợ
            /// </summary>
            [Description("Sinh có hỗ trợ")]
            Zmc = 2,
            /// <summary>
            /// Sinh mổ
            /// </summary>
            [Description("Sinh mổ")]
            Pgc = 3,
            /// <summary>
            /// Sinh khó
            /// </summary>
            [Description("Sinh khó")]
            nc = 4,
            /// <summary>
            /// Sảy thai
            /// </summary>
            [Description("Sảy thai")]
            lc = 5,
            /// <summary>
            /// Gây chuyển dạ
            /// </summary>
            [Description("Gây chuyển dạ")]
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
            /// Thai ngoài tử cung
            /// </summary>
            [Description("Thai ngoài tử cung")]
            gwc = 10,
            /// <summary>
            /// Xét nghiệm gen di truyền
            /// </summary>
            [Description("Xét nghiệm gen di truyền")]
            ycjyjc = 11,
            /// <summary>
            /// Sảy thai/gây chuyển dạ trên bốn tháng
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
            /// Đặt dụng cụ tử cung
            /// </summary>
            [Description("Đặt dụng cụ tử cung")]
            fzgnjyq = 1,
            /// <summary>
            /// Lấy dụng cụ tử cung
            /// </summary>
            [Description("Lấy dụng cụ tử cung")]
            qcgnjyq = 2,
            /// <summary>
            /// Thủ thuật nạo phá thai
            /// </summary>
            [Description("Thủ thuật nạo phá thai")]
            lcs = 3,
            /// <summary>
            /// Thủ thuật gây chuyển dạ
            /// </summary>
            [Description("Thủ thuật gây chuyển dạ")]
            ycs = 4,
            /// <summary>
            /// Phẫu thuật triệt sản
            /// </summary>
            [Description("Phẫu thuật triệt sản")]
            jyss = 5,
            /// <summary>
            /// Phẫu thuật phục hồi sau triệt sản
            /// </summary>
            [Description("Phẫu thuật phục hồi sau triệt sản")]
            jyftss = 6,
            /// <summary>
            /// Phẫu thuật triệt sản(Ống dẫn tinh)
            /// </summary>
            [Description("Phẫu thuật triệt sản(Ống dẫn tinh)")]
            jysssjg = 7,
            /// <summary>
            /// Phẫu thuật phục hồi sau triệt sản(Ống dẫn tinh)
            /// </summary>
            [Description("Phẫu thuật phục hồi sau triệt sản(Ống dẫn tinh)")]
            jyftsssjg = 8,
            /// <summary>
            /// Khác
            /// </summary>
            [Description("Khác")]
            qt = 9,
            /// <summary>
            /// Thủ thuật cấy dưới da
            /// </summary>
            [Description("Thủ thuật cấy dưới da")]
            pxmzs = 10,
            /// <summary>
            /// Hủy thủ thuật cấy dưới da
            /// </summary>
            [Description("Hủy thủ thuật cấy dưới da")]
            qxpxmzs = 11,
            /// <summary>
            /// Thủ thuật thắt ống dẫn trứng
            /// </summary>
            [Description("Thủ thuật thắt ống dẫn trứng")]
            srgjzs = 12
        }
        /// <summary>
        /// Cờ sinh muộn/Cờ sinh non
        /// </summary>
        public enum EnumWybz
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("Không")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("Có")]
            s = 1,
        }
        /// <summary>
        /// Loại bác sĩ
        /// </summary>
        public enum EnumYslx
        {
            [Description("Bác sĩ nội trú")] //Resident
            ZyDoc = 1,
            [Description("Bác sĩ điều trị chính")] //Attending
            ZzDoc = 2,
            [Description("Bác sĩ chủ nhiệm")] //chief
            ZrDoc = 3
        }
        /// <summary>
        /// Dấu hiệu ngoài địa phương
        /// </summary>
        public enum EnumYd
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("Không")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("Có")]
            s = 1,
        }
        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public enum EnumBrxz
        {
            /// <summary>
            /// Tự chi trả
            /// </summary>
            [Description("Tự chi trả")]
            zf = 0,
            /// <summary>
            /// BHYT công nhân viên chức
            /// </summary>
            [Description("BHYT công nhân viên chức")]
            zg = 1,
            /// <summary>
            /// BHYT cư dân
            /// </summary>
            [Description("BHYT cư dân")]
            jm = 2,
            /// <summary>
            /// Nghỉ hưu theo chế độ
            /// </summary>
            [Description("Nghỉ hưu theo chế độ")]
            lx = 3,
            /// <summary>
            /// BHYT thông thường
            /// </summary>
            [Description("BHYT thông thường")]
            pt = 11,
        }
        /// <summary>
        /// Trạng thái đơn hàng0:Chờ thanh toán 1:Đang thanh toán  2:Đã thanh toán 3:Đã hoàn tiền 4:Đã hủy
        /// </summary>
        public enum EnumOrderStatus
        {
            /// <summary>
            /// Chờ thanh toán
            /// </summary>
            [Description("Chờ thanh toán")]
            dzf = 0,
            /// <summary>
            /// Đang thanh toán
            /// </summary>
            [Description("Đang thanh toán")]
            zfz = 1,
            /// <summary>
            /// Đã thanh toán
            /// </summary>
            [Description("Đã thanh toán")]
            yzf = 2,
            /// <summary>
            /// Đã hoàn tiền
            /// </summary>
            [Description("Đã hoàn tiền")]
            ytk = 3,
            /// <summary>
            /// Đã hủy
            /// </summary>
            [Description("Đã hủy")]
            zf = 4,
        }
        /// <summary>
        /// Loại thẻ giao dịch
        /// </summary>
        public enum EnumJykxzlx
        {
            /// <summary>
            /// Tự chi trả
            /// </summary>
            [Description("Tự chi trả")]
            zf = 0,
            /// <summary>
            /// BHYT
            /// </summary>
            [Description("BHYT")]
            yb = 1
        }
        /// <summary>
        /// Loại nghiệp vụ đơn hàng
        /// </summary>
        public enum EnumOrderType
        {
            /// <summary>
            /// Ngoại trú
            /// </summary>
            [Description("Ngoại trú")]
            mz = 1,
            /// <summary>
            /// Nội trú
            /// </summary>
            [Description("Nội trú")]
            zy = 2
        }
    }

    public class CisClientEnum : ClientEnum
    {
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public enum EnumZjlx
        {
            /// <summary>
            /// CMND
            /// </summary>
            [Description("CMND")]
            sfz = 1,
            /// <summary>
            /// Hộ chiếu
            /// </summary>
            [Description("Hộ chiếu")]
            hz,
            /// <summary>
            /// Chứng nhận sĩ quan
            /// </summary>
            [Description("Chứng nhận sĩ quan")]
            jgz,
            /// <summary>
            /// Khác
            /// </summary>
            [Description("Khác")]
            other = 9
        }

        /// <summary>
        /// Tính chất tự trả(Thuốc,Thuộc tính của từ điển hạng mục)
        /// </summary>
        public enum EnumZiFuXingZhi
        {
            /// <summary>
            /// Được thanh toán
            /// </summary>
            [Description("Được thanh toán")]
            KB = 0,
            /// <summary>
            /// Tự chi trả
            /// </summary>
            [Description("Tự chi trả")]
            ZF = 1,
            /// <summary>
            /// Tự trả theo phân loại
            /// </summary>
            [Description("Tự trả theo phân loại")]
            FLZF = 2,
            /// <summary>
            /// Hoàn toàn tự lo
            /// </summary>
            [Description("Hoàn toàn tự lo")]
            JueDuiZiLi = 3,
            /// <summary>
            /// Hạng A
            /// </summary>
            [Description("Hạng A")]
            Jia,
            /// <summary>
            /// Hạng B
            /// </summary>
            [Description("Hạng B")]
            Yi,
            /// <summary>
            /// Hạng C
            /// </summary>
            [Description("Hạng C")]
            Bing,
        }

        /// <summary>
        /// Loại thuốc
        /// </summary>
        public enum EnumYpsx
        {
            [Description("Thuốc gây mê, hướng thần(Gây mê,Hướng thần I,Hướng thần II)")]
            Mzyp = 9,
            [Description("Thuốc độc")]
            Dxyp = 4,
            [Description("Chế phẩm máu")]
            Xyzp = 5,
            [Description("Thuốc hormone")]
            Jsyp = 6,
            [Description("Thuốc quý")]
            Gzyp = 7,
            [Description("Thuốc chống ung thư")]
            Kzlywyp = 8,
        }
        /// <summary>
        /// Cấp kháng sinh
        /// </summary>
        public enum EnumKss
        {

            [Description("Thuốc dùng không giới hạn")]
            Fxzsyyw = 0,
            [Description("Thuốc dùng giới hạn")]
            Xzsyyw = 1,
            [Description("Thuốc dùng đặc biệt")]
            Tssyyw = 2,
        }

        /// <summary>
        /// Trạng thái khám
        /// </summary>
        public enum EnumJzzt
        {
            /// <summary>
            /// Chưa khám
            /// </summary>
            [Description("Chưa khám")]
            NotYetTreate = 1,
            /// <summary>
            /// Đang khám
            /// </summary>
            [Description("Đang khám")]
            Treating = 2,
            /// <summary>
            /// Kết thúc khám
            /// </summary>
            [Description("Kết thúc khám")]
            Treated = 3,
        }



        /// <summary>
        /// Loại đơn thuốc
        /// </summary>
        public enum EnumCflx
        {
            /// <summary>
            /// Đơn thuốc tây
            /// </summary>
            [Description("Đơn thuốc tây")]
            WMPres = 1,
            /// <summary>
            /// Đơn thuốc đông y
            /// </summary>
            [Description("Đơn thuốc đông y")]
            TCMPres = 2,
            /// <summary>
            /// Đơn thuốc phục hồi chức năng
            /// </summary>
            [Description("Đơn thuốc phục hồi chức năng")]
            RehabPres = 3,
            /// <summary>
            /// Đơn xét nghiệm
            /// </summary>
            [Description("Đơn xét nghiệm")]
            InspectionPres = 4,
            /// <summary>
            /// Đơn kiểm tra
            /// </summary>
            [Description("Đơn kiểm tra")]
            ExaminationPres = 5,
            /// <summary>
            /// Đơn hạng mục thông thường
            /// </summary>
            [Description("Đơn hạng mục thông thường")]
            RegularItemPres = 6,

            [Description("Đơn thuốc điện tử")]
            Dzcf = 7,

        }

        public enum EnumYpyf
        {
            [Description("Tây y")]
            WM = 1,
            [Description("Y học cổ truyền")]
            TCM = 2
        }

        /// <summary>
        /// Loại chẩn đoán
        /// </summary>
        public enum EnumZdlx
        {
            /// <summary>
            /// Chẩn đoán chính
            /// </summary>
            [Description("Chẩn đoán chính")]
            Main = 1,
            /// <summary>
            /// Chẩn đoán phụ
            /// </summary>
            [Description("Chẩn đoán phụ")]
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
            [Description("Cá nhân")]
            personal = 1,
            /// <summary>
            /// Khoa
            /// </summary>
            [Description("Khoa")]
            department = 2,
            /// <summary>
            /// Toàn viện
            /// </summary>
            [Description("Toàn viện")]
            hospital = 3,
        }

        /// <summary>
        /// Loại gói xét nghiệm/kiểm tra  (Xét nghiệm và kiểm tra dùng chung một trang,chỉ khác tham số(tức làtype)Khác nhau)
        /// </summary>
        public enum EnumjyjczutaoLx
        {
            /// <summary>
            /// Xét nghiệm
            /// </summary>
            [Description("Xét nghiệm")]
            jy = 1,
            /// <summary>
            /// Kiểm tra
            /// </summary>
            [Description("Kiểm tra")]
            jc = 2
        }

        /// <summary>
        /// Loại mẫu xét nghiệm/kiểm tra  (Xét nghiệm và kiểm tra dùng chung một trang,chỉ khác tham số(tức làtype)Khác nhau)
        /// </summary>
        public enum EnumjyjcmbLx
        {
            /// <summary>
            /// Xét nghiệm
            /// </summary>
            [Description("Xét nghiệm")]
            jy = 1,
            /// <summary>
            /// Kiểm tra
            /// </summary>
            [Description("Kiểm tra")]
            jc = 2
        }

        /// <summary>
        /// Chiến lược định giá hạng mục thu phí
        /// </summary>
        public enum EnumSfxmJjcl
        {
            /// <summary>
            /// Theo thời lượng
            /// </summary>
            [Description("Theo thời lượng")]
            Time = 1,
            /// <summary>
            /// Theo số lượng
            /// </summary>
            [Description("Theo số lượng")]
            Amount,
            /// <summary>
            /// Theo diện tích
            /// </summary>
            [Description("Theo diện tích")]
            Acreage,
        }

        /// <summary>
        /// Nguồn bệnh án lịch sử
        /// </summary>
        public enum Enumlsblly
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("Hệ thống này")]
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
            [Description("Đăng nhập một lần (SSO)")]
            SSO = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("Đăng nhập hệ thống")]
            App = 2
        }

        /// <summary>
        /// Loại ăn uống
        /// </summary>
        public enum Enumyslb
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("Suất ăn thường cho người Hồi")]
            hmps = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("Suất ăn nửa lỏng cho người Hồi")]
            hmbl = 2,
            /// <summary>
            /// 
            /// </summary>
            [Description("Suất ăn lỏng cho người Hồi")]
            hmlz = 3,
            /// <summary>
            /// 
            /// </summary>
            [Description("Suất ăn thường thông thường")]
            ybps = 4,
            /// <summary>
            /// 
            /// </summary>
            [Description("Suất ăn nửa lỏng thông thường")]
            ybbl = 5,
            /// <summary>
            /// 
            /// </summary>
            [Description("Suất ăn lỏng thông thường")]
            yblz = 6,
            /// <summary>
            /// 
            /// </summary>
            [Description("Tự chuẩn bị suất ăn")]
            sszb = 7,
            /// <summary>
            /// 
            /// </summary>
            [Description("Nhịn ăn")]
            js = 8,
            /// <summary>
            /// 
            /// </summary>
            [Description("Dinh dưỡng đường ruột")]
            cnyy = 9,
            /// <summary>
            /// 
            /// </summary>
            [Description("Chất bổ sung dinh dưỡng")]
            yybcj = 10,
        }


        /// <summary>
        /// Trạng thái xếp hàng gọi số
        /// </summary>
        public enum EmunQueueCalledStu
        {
            [Description("Đã điểm danh")]
            sign = 1,
            [Description("Đã gọi")]
            call = 2,
            [Description("Quá số")]
            pass = 3,
            [Description("Trả lời")]
            reply = 4,
        }

        /// <summary>
        /// Cờ ngoại trú/nội trú  0 Chung 1 Chỉ ngoại trú 2 Chỉ nội trú 3 Hệ thống(Cả ngoại trú và nội trú đều không dùng được)Ví dụ:Giường tại nhà
        /// </summary>
        public enum Enummzzybz
        {

            /// <summary>
            /// Chỉ ngoại trú
            /// </summary>
            [Description("Ngoại trú")]
            mz = 1,
            /// <summary>
            /// Chỉ nội trú
            /// </summary>
            [Description("Nội trú")]
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
            /// Chưa thực hiện
            /// </summary>
            [Description("Chưa thực hiện")]
            wzx = 0,
            /// <summary>
            /// Đã thực hiện
            /// </summary>
            [Description("Đã thực hiện")]
            yzx = 1,
            /// <summary>
            /// Hủy thực hiện
            /// </summary>
            [Description("Hủy thực hiện")]
            yqx = 2

        }
        /// <summary>
        /// Trạng thái gọi số phân loại
        /// </summary>
        public enum EmunConsultCalledStu
        {
            [Description("Chờ gọi số")]
            wait = 1,
            [Description("Đã gọi")]
            call = 2,
            [Description("Quá số")]
            pass = 3,
            [Description("Trả lời")]
            reply = 4,
            [Description("Gọi số lại")]
            recall = 5,
        }
        /// <summary>
        /// Trạng thái khám chữa bệnh từ xa
        /// </summary>
        public enum Emunzlzt
        {
            [Description("Chờ xác nhận")]
            dqr = 1,
            [Description("Đang khám")]
            jzz = 2,
            [Description("Đã kết thúc")]
            yjs = 3,
            [Description("Đã trả lại")]
            yth = 4,
            [Description("Đã thu hồi")]
            ycx = 5,
            [Description("Đã phát thuốc")]
            yfy = 6,
        }

        #region Nội trú
        /// <summary>
        /// Cờ đang nằm viện
        /// </summary>
        public enum EnumZYBZ
        {
            /// <summary>
            /// Đăng ký nhập viện
            /// </summary>
            Xry = 0,
            /// <summary>
            /// Đang ở khu bệnh
            /// </summary>
            Bqz = 1,
            /// <summary>
            /// Xuất khỏi khu bệnh(Ra khỏi khu bệnh)(Chờ quyết toán)
            /// </summary>
            Djz = 2,
            /// <summary>
            /// Đã xuất viện(Quyết toán xuất viện)
            /// </summary>
            Ycy = 3,
            /// <summary>
            /// Chuyển khu
            /// </summary>
            Zq = 7,

            /// <summary>
            /// Hồ sơ hủy/Hủy nhập viện
            /// </summary>
            Wry = 9,
        }

        /// <summary>
        /// Loại y lệnh
        /// </summary>
        public enum EnumYzlx
        {
            ///// <summary>
            ///// Chung
            ///// </summary>
            //[Description("Chung")]
            //Ty = 1,
            /// <summary>
            /// Thuốc
            /// </summary>
            [Description("Thuốc")]
            Yp = 2,
            /// <summary>
            /// Văn bản
            /// </summary>
            [Description("Văn bản")]
            Wz = 3,
            /// <summary>
            /// Mang thuốc khi xuất viện
            /// </summary>
            [Description("Mang thuốc khi xuất viện")]
            Cydy = 4,
            /// <summary>
            /// Hạng mục
            /// </summary>
            [Description("Hạng mục")]
            sfxm = 5,
            /// <summary>
            /// Hạng mục
            /// </summary>
            [Description("Xét nghiệm")]
            jy = 6,
            /// <summary>
            /// Hạng mục
            /// </summary>
            [Description("Kiểm tra")]
            jc = 7,
            /// <summary>
            /// Hạng mục
            /// </summary>
            [Description("Y lệnh suất ăn")]
            ssyz = 8,
            /// <summary>
            /// Phẫu thuật
            /// </summary>
            [Description("Phẫu thuật")]
            oper = 9,
            /// <summary>
            /// Thảo dược
            /// </summary>
            [Description("Thảo dược")]
            zcy = 10,
            /// <summary>
            /// Phục hồi chức năng
            /// </summary>
            [Description("Phục hồi chức năng")]
            rehab = 11

        }
        /// <summary>
        /// Trạng thái y lệnh
        /// </summary>
        public enum EnumYzzt
        {
            /// <summary>
            /// Chờ duyệt 
            /// </summary>
            [Description("Chờ duyệt")]
            Ds = 0,
            /// <summary>
            /// Duyệt
            /// </summary>
            [Description("Duyệt")]
            Sh = 1,
            /// <summary>
            /// Thực hiện
            /// </summary>
            [Description("Thực hiện")]
            Zx = 2,
            /// <summary>
            /// RútDC
            /// </summary>
            [Description("DC")]
            DC = 3,
            /// <summary>
            /// Dừng hoặc hủy
            /// </summary>
            [Description("Dừng")]
            TZ = 4
        }
        /// <summary>
        /// Loại đơn vị thuốc
        /// </summary>
        public enum EnumYPdwlb
        {
            /// <summary>
            /// Đơn vị liều lượng
            /// </summary>
            Jldw = 1,
            /// <summary>
            /// Đơn vị kho thuốc
            /// </summary>
            Ykdw = 2,
            /// <summary>
            /// Đơn vị ngoại trú
            /// </summary>
            Mzdw = 3,
            /// <summary>
            /// Đơn vị nội trú
            /// </summary>
            Zydw = 4,
            /// <summary>
            /// Đơn vị nhập hàng
            /// </summary>
            Jhdw = 5,
            /// <summary>
            /// Đơn vị nhỏ nhất
            /// </summary>
            Zxdw = 6
        }
        /// <summary>
        /// Phạm vi gói
        /// </summary>
        public enum EnumTcfw
        {
            [Description("Cá nhân")]
            Person = 1,
            [Description("Khoa")]
            Dept = 2,
            [Description("Khu bệnh")]
            Ward = 3,
            [Description("Toàn viện")]
            Hosp = 4
        }
        /// <summary>
        /// Trạng thái bệnh nhân điều dưỡng(Dùng để nhập dấu hiệu sinh tồn điều dưỡng)
        /// </summary>
        public enum EnumHlbrzt
        {
            [Description("Nhập viện")]
            Ry = 1,
            [Description("Đang nằm viện")]
            Zy = 2,
            [Description("Xuất viện")]
            Cy = 3,
            [Description("Xin nghỉ")]
            Qj = 4,
            [Description("Chuyển vào")]
            Zr = 5,
            [Description("Chuyển ra")]
            Zc = 6,
            [Description("Từ chối đo")]
            Jc = 7,
            [Description("Trở lại viện")]
            Fy = 8,
            [Description("Đo lại")]
            Fc = 9,
            [Description("Nằm giường")]
            Wc = 10,
            [Description("Tử vong")]
            Sw = 11,
            [Description("Phẫu thuật")]
            ss = 12,
            [Description("Trở về phòng")]
            hs = 13
        }
        /// <summary>
        /// Đơn vị chu kỳ thực hiện
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
            /// Chu kỳ không đều
            /// </summary>
            Unruly = 4
        }
        /// <summary>
        /// Cờ phát/trả thuốc
        /// </summary>
        public enum EnumMedSTflag
        {
            [Description("Phát thuốc")]
            Receive = 1,
            [Description("Trả toàn bộ")]
            AllReturn = 2,
            [Description("Trả một phần")]
            PartReturn = 3
        }
        /// <summary>
        /// Tính chất y lệnh
        /// </summary>
        public enum EnumYzxz
        {
            [Description("Tạm thời")]
            Ls = 1,
            [Description("Dài hạn")]
            Cq = 2
        }
        /// <summary>
        /// Trạng thái hồ sơ giường bệnh
        /// </summary>
        public enum EnumCwjlzt
        {
            [Description("Hiện tại")]
            Dq = 1,
            [Description("Chuyển giường")]
            Zc = 2,
            [Description("Chuyển khu")]
            Zq = 3,
            [Description("Ra khỏi khu")]
            Cq = 4,
            [Description("Hủy vào khu")]
            QxRq = 5
        }
        /// <summary>
        /// Loại bác sĩ
        /// </summary>
        public enum EnumYslx
        {
            [Description("Bác sĩ nội trú")] //Resident
            ZyDoc = 1,
            [Description("Bác sĩ điều trị chính")] //Attending
            ZzDoc = 2,
            [Description("Bác sĩ chủ nhiệm")] //chief
            ZrDoc = 3
        }
        /// <summary>
        /// Cấp điều dưỡng
        /// </summary> 
        public enum EnumHljb
        {
            /// <summary>
            /// ICấp
            /// </summary>
            [Description("ICấp")]
            I = 1,
            /// <summary>
            /// IICấp
            /// </summary>
            [Description("IICấp")]
            II = 2,
            /// <summary>
            /// IIICấp
            /// </summary>
            [Description("IIICấp")]
            III = 3,
            /// <summary>
            /// Đặc cấp
            /// </summary>
            [Description("Đặc cấp")]
            T = 4
        }
        /// <summary>
        /// Trạng thái quyết toán
        /// </summary>
        public enum EnumJszt
        {
            [Description("Đã quyết toán")]
            Yjs = 1,
            [Description("Chưa quyết toán")]
            Wjs = 2
        }
        /// <summary>
        /// Tiêu chuẩn thực thi cố định
        /// </summary>
        public enum EnumGdzxbz
        {
            /// <summary>
            /// Cố định
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// Không cố định
            /// </summary>
            Nonfixed = 0
        }
        /// <summary>
        /// Mức độ nguy kịch
        /// </summary>
        public enum EnumWzjb
        {
            /// <summary>
            /// Thông thường
            /// </summary>
            [Description("Thông thường")]
            Normal = 1,
            /// <summary>
            /// Bệnh nặng
            /// </summary>
            [Description("Bệnh nặng")]
            Serious = 2,
            /// <summary>
            /// Nguy kịch
            /// </summary>
            [Description("Nguy kịch")]
            Bedying = 3
        }
        //EnumDBĐã bao gồm
        //public enum EnumZdlx
        //{
        //    MainDiag=1,
        //    SedDiag=2
        //}

        /// <summary>
        /// Giải thích đo thân nhiệt
        /// </summary>
        public enum EnumTwclfs
        {
            /// <summary>
            /// Nhiệt độ miệng
            /// </summary>
            [Description("Nhiệt độ miệng")]
            kw = 1,
            /// <summary>
            /// Nhiệt độ nách
            /// </summary>
            [Description("Nhiệt độ nách")]
            yw = 2,
            /// <summary>
            /// Nhiệt độ hậu môn
            /// </summary>
            [Description("Nhiệt độ hậu môn")]
            gw = 1,
        }
        /// <summary>
        /// Loại suất ăn
        /// </summary>
        public enum EnumSSLB
        {
            /// <summary>
            /// Loại
            /// </summary>
            [Description("Loại")]
            lx = 1,
            /// <summary>
            /// Yêu cầu đặc biệt
            /// </summary>
            [Description("Yêu cầu đặc biệt")]
            tsyq = 2,
            /// <summary>
            /// Loại suất ăn
            /// </summary>
            [Description("Loại suất ăn")]
            ssxh = 3,
        }

        /// <summary>
        /// Quyền dùng mẫu
        /// </summary>
        public enum Enummbqx
        {
            /// <summary>
            /// Chung
            /// </summary>
            [Description("Chung")]
            pub = 0,
            /// <summary>
            /// Cá nhân
            /// </summary>
            [Description("Cá nhân")]
            prv = 1,
            /// <summary>
            /// Khoa
            /// </summary>
            [Description("Khoa")]
            dept = 2
        }

        /// <summary>
        /// Loại bệnh án
        /// </summary>
        public enum EnumBllx
        {
            /// <summary>
            /// Bệnh án nội trú
            /// </summary>
            [Description("Bệnh án nội trú")]
            zybl = 1,

            /// <summary>
            /// Ghi chép diễn biến bệnh
            /// </summary>
            [Description("Ghi chép diễn biến bệnh")]
            bcjl = 2,

            /// <summary>
            /// Văn bản y tế
            /// </summary>
            [Description("Văn bản y tế")]
            ylws = 3,

            /// <summary>
            /// Ghi chép điều dưỡng
            /// </summary>
            [Description("Ghi chép điều dưỡng")]
            hljl = 4,

            /// <summary>
            /// Trang đầu bệnh án
            /// </summary>
            /// 
            [Description("Trang đầu bệnh án")]
            basy = 5,

            /// <summary>
            /// Đánh giá phục hồi chức năng
            /// </summary>
            [Description("Đánh giá phục hồi chức năng")]
            kfpg = 6
        }

        //public enum EnumCYQK
        //{
        //    /// <summary>
        //    /// Khỏi bệnh
        //    /// </summary>
        //    [Description("Khỏi bệnh")]
        //    zy = 1,
        //    /// <summary>
        //    /// Đỡ hơn
        //    /// </summary>
        //    [Description("Đỡ hơn")]
        //    hz = 2,
        //    /// <summary>
        //    /// Chưa khỏi
        //    /// </summary>
        //    [Description("Chưa khỏi")]
        //    wy = 3,
        //    /// <summary>
        //    /// Tử vong
        //    /// </summary>
        //    [Description("Tử vong")]
        //    sw = 4,
        //    /// <summary>
        //    /// Khác
        //    /// </summary>
        //    [Description("Khác")]
        //    qt = 5,
        //    /// <summary>
        //    /// Chuyển viện
        //    /// </summary>
        //    [Description("Chuyển viện")]
        //    zhuanyuan = 6

        //}
        public enum EnumSF
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("Không")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("Có")]
            s = 1,
        }

        //Kiểm tra thử phản ứng da
        public enum EnumPSCS
        {
            /// <summary>
            /// Không
            /// </summary>
            [Description("Không")]
            f = 0,
            /// <summary>
            /// Có
            /// </summary>
            [Description("Có")]
            s = 1,
        }
        /// <summary>
        /// Kết quả thử phản ứng da
        /// </summary>
        public enum EnumPsResult
        {
            [Description("Âm tính")]
            negative = 0,
            [Description("Dương tính")]
            positive = 1,
        }

        /// <summary>
        /// Trạng thái thuốc dự trữ của khoa
        /// </summary>
        public enum EnumKSBYZT
        {
            [Description("Đã đăng ký")]
            sq = 0,
            [Description("Đã nộp")]
            sh = 1,
            [Description("Đã duyệt")]
            tj = 2,
            [Description("Đã phát thuốc")]
            fy = 3,
            [Description("Đã trả lại")]
            th = 4,
            [Description("Đã hủy")]
            zf = 5,
        }

        /// <summary>
        /// Trạng thái trả lại thuốc dự trữ của khoa
        /// </summary>
        public enum EnumKSBYTHZT
        {
            [Description("Lưu")]
            sq = 1,
            [Description("Duyệt")]
            sh = 2,
            [Description("Nộp")]
            tj = 3,
            [Description("Phát thuốc")]
            fy = 4,
            [Description("Rút lại")]
            ch = 5,
            [Description("Trả lại")]
            th = 6,
            [Description("Hủy")]
            zf = 7,
        }
        /// <summary>
        /// Nguồn thuốc
        /// </summary>
        public enum EnumYply
        {
            [Description("Thuốc dự trữ của khoa")]
            ksby = 1,
            [Description("Nhà thuốc")]
            yf = 2
        }

        /// <summary>
        /// Thử phản ứng da
        /// </summary>
        public enum EnumPs
        {
            [Description("Âm tính")]
            yin = 0,
            [Description("Dương tính")]
            yang = 1,
        }
        #endregion
    }
    #region Emr
    public class EmrClientEnum : ClientEnum
    {
        /// <summary>
        /// Quyền dùng mẫu
        /// </summary>
        public enum Enummbqx
        {
            /// <summary>
            /// Chung
            /// </summary>
            [Description("Chung")]
            pub = 0,
            /// <summary>
            /// Cá nhân
            /// </summary>
            [Description("Cá nhân")]
            prv = 1,
            /// <summary>
            /// Khoa
            /// </summary>
            [Description("Khoa")]
            dept = 2
        }
        /// <summary>
        /// Phân quyền mẫu
        /// </summary>
        public enum EnummbqxFp
        {
            /// <summary>
            /// Không có quyền nào
            /// </summary>
            [Description("Không có quyền")]
            non = 0,
            /// <summary>
            /// Chỉ đọc
            /// </summary>
            [Description("Chỉ đọc")]
            read = 1,
            /// <summary>
            /// Đọc/ghi
            /// </summary>
            [Description("Đọc/ghi")]
            edit = 2
        }

        /// <summary>
        /// Trạng thái bệnh án
        /// </summary>
        public enum EnumRecordStu
        {
            [Description("Chưa nộp")]
            wtj = 0,
            [Description("Đã nộp")]
            ytj = 1,
            [Description("Trả lại")]
            th = 2,
            [Description("Đã ký nhận")]
            yqs = 3,
            //[Description("Lưu trữ bệnh án")]
            //bagd = 4
        }

        public enum EnumPlanStu
        {
            [Description("Đang tiến hành")]
            zc = 0,
            [Description("Đã dừng")]
            tz = 1,
            [Description("Dừng/hủy")]
            tzqx = 2,
        }

        /// <summary>
        /// Nhóm lớn bệnh án và mẫu Cờ nghiệp vụ
        /// </summary>
        public enum EnumMzbz
        {
            /// <summary>
            /// Nội trú
            /// </summary>
            [Description("Nội trú")]
            zy = 0,
            /// <summary>
            /// Ngoại trú
            /// </summary>
            [Description("Ngoại trú")]
            mz = 1,
            /// <summary>
            /// Không giới hạn
            /// </summary>
            [Description("Không giới hạn")]
            all = 2,
        }

        /// <summary>
        /// Cách tải mẫu
        /// </summary>
        public enum EnummbqxTempLoadWay
        {
            /// <summary>
            /// Chung
            /// </summary>
            [Description("Trình soạn thảo DCSoft")]
            DcWriter = 0,
            /// <summary>
            /// Cá nhân
            /// </summary>
            [Description("View độc lập")]
            View = 1
        }

        /// <summary>
        /// Cờ sử dụng
        /// </summary>
        public enum EnumSybz
        {
            [Description("Có")]
            Y = 1,
            [Description("Không")]
            N = 0
        }
        public enum EnumBlys
        {
            [Description("Mục lục")]
            ml = -1,
            [Description("Văn bản")]
            wb = 0,
            [Description("Thả xuống")]
            xl = 1,
            [Description("Thả xuống chọn nhiều")]
            xldx = 2,
            [Description("Ngày(yyyy-MM-dd)")]
            rq = 3,
            [Description("Thời gian(HH:mm)")]
            sj = 4,
            [Description("Ngày giờ(yyyy-MM-dd HH:mm)")]
            rqsj = 5,
            [Description("Giá trị số")]
            sz = 6
        }
        public enum ApplyStatusEnum
        {
            /// <summary>
            /// Chưa phê duyệt
            /// </summary>
            [Description("Chưa phê duyệt")]
            wpz = 0,
            /// <summary>
            /// Đã phê duyệt
            /// </summary>
            [Description("Đã phê duyệt")]
            ysp = 1,
            /// <summary>
            /// Trả lại
            /// </summary>
            [Description("Trả lại")]
            th = 2
        }

        public enum EnumMRSex
        {
            [Description("Nam")]
            M = 1,
            [Description("Nữ")]
            F = 2
        }
        /// <summary>
        /// Trạng thái bệnh án
        /// </summary>
        public enum Enumbazt
        {
            [Description("Chờ nhập")]
            dlr = 1,
            [Description("Đã nhập")]
            lrz = 2,
            //[Description("Đã lưu trữ")]
            //gd = 3
        }

        ///// <summary>
        ///// Trạng thái bệnh án  và EMRChung
        ///// </summary>
        //public enum EnumRecordStu
        //{
        //    [Description("Chưa nộp")]
        //    wtj = 0,
        //    [Description("Đã nộp")]
        //    ytj = 1,
        //    [Description("Trả lại")]
        //    th = 2,
        //    [Description("Đã ký nhận")]
        //    yqs = 3,
        //    //[Description("Lưu trữ bệnh án")]
        //    //bagd = 4
        //}
        /// <summary>
        /// Nhóm máu
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
            [Description("Không rõ")]
            BX = 5,
            [Description("Chưa tra")]
            WC = 6
        }

        /// <summary>
        /// Tử vongBệnh nhânKhám nghiệm tử thi
        /// </summary>
        public enum EnumSwhzsj
        {
            [Description("Có")]
            Y = 1,
            [Description("Không")]
            N = 2
        }

        /// <summary>
        /// RH
        /// </summary>
        public enum EnumBloodTypeRH
        {
            [Description("Âm tính")]
            yin = 1,
            [Description("Dương tính")]
            yang = 2,
            [Description("Không rõ")]
            BX = 3,
            [Description("Chưa tra")]
            WC = 4
        }

        /// <summary>
        /// Quan hệ
        /// </summary>
        public enum EnumGx
        {
            [Description("Bản thân hoặc chủ hộ")]
            br = 1,
            [Description("Vợ/chồng")]
            fq = 2,
            [Description("Con")]
            z = 3,
            [Description("Nữ")]
            n,
            [Description("Cháu trai,Cháu gái hoặc cháu ngoại trai,Cháu ngoại gái")]
            sun,
            [Description("Cha mẹ")]
            fm,
            [Description("Ông bà nội hoặc ông bà ngoại")]
            zfm,
            [Description("Anh,Em trai,Chị,Em gái")]
            xdjm,
            [Description("Khác")]
            qt
        }
        /// <summary>
        /// Có hoặc không
        /// </summary>
        public enum EnumYorN
        {
            [Description("Không")]
            N = 1,
            [Description("Có")]
            Y = 2
        }


        /// <summary>
        /// Có hoặc không
        /// </summary>
        public enum EnumNewYorN
        {
            [Description("Có")]
            Y = 1,
            [Description("Không")]
            N = 2
        }

        public enum EnumYork
        {
            [Description("Cấp cứu")]
            y = 1,
            [Description("Ngoại trú")]
            lcwqd = 2,
            [Description("Chuyển đến từ cơ sở y tế khác")]
            qkbm = 3,
            [Description("Khác")]
            w = 4
        }
        /// <summary>
        /// Không hoặc có
        /// </summary>
        public enum EnumNorY
        {
            [Description("Không")]
            N = 1,
            [Description("Có")]
            Y = 2
        }
        /// <summary>
        /// Có hoặc không
        /// </summary>
        public enum EnumHorN
        {
            [Description("Không có")]
            Non = 1,
            [Description("Có")]
            Has = 2
        }
        /// <summary>
        /// Phản ứng truyền máu
        /// 1.Có  2.Không  3.Chưa nhập
        /// </summary>
        public enum EumSYFY
        {
            [Description("Có")]
            y = 1,
            [Description("Không")]
            n = 2,
            [Description("Chưa nhập")]
            Non = 3
        }
        /// <summary>
        ///Tình trạng bệnh khi nhập viện 1	Có 2	Lâm sàng chưa xác định 3	Tình hình không rõ 4	Không có
        /// </summary>
        public enum EnumRybq
        {
            [Description("Có")]
            y = 1,
            [Description("Lâm sàng chưa xác định")]
            lcwqd = 2,
            [Description("Tình hình không rõ")]
            qkbm = 3,
            [Description("Không có")]
            w = 4
        }

        /// <summary>
        /// Tình hình xuất viện
        /// 1	Khỏi bệnh 2	Đỡ hơn 3	Chưa khỏi4	Tử vong5	Khác
        /// </summary>
        public enum EnumCyqk
        {
            [Description("Khỏi bệnh")]
            zy = 1,
            [Description("Đỡ hơn")]
            hz = 2,
            [Description("Chưa khỏi")]
            wy = 3,
            [Description("Tử vong")]
            sw = 4,
            [Description("Khác")]
            qt = 5
        }
        /// <summary>
        /// Loại chẩn đoán-Chính - phụ
        /// </summary>
        public enum EnumZdlxbs
        {
            [Description("Chẩn đoán chủ yếu")]
            zy = 1,
            [Description("Chẩn đoán thứ yếu")]
            cy = 2
        }
        /// <summary>
        /// Loại chẩn đoán
        /// </summary>
        public enum EnumZdlx2
        {
            [Description("Chẩn đoán tây y")]
            WM = 1,
            [Description("Chẩn đoán y học cổ truyền")]
            TCM = 2

        }

        /// <summary>
        /// Đường nhập viện
        /// </summary>
        public enum EnumMrRYTJ
        {
            /// <summary>
            /// Ngoại trú
            /// </summary>
            [Description("Ngoại trú")]
            mz = 1,
            /// <summary>
            /// Cấp cứu
            /// </summary>
            [Description("Cấp cứu")]
            jz = 2,
            /// <summary>
            /// Chuyển đến từ cơ sở y tế khác
            /// </summary>
            [Description("Chuyển đến từ cơ sở y tế khác")]
            qtyljg = 3,
            /// <summary>
            /// 120
            /// </summary>
            //[Description("120")]
            //jj120,
            /// <summary>
            /// Khác
            /// </summary>
            [Description("Khác")]
            Else = 9,
        }
        /// <summary>
        /// Chất lượng bệnh án
        /// </summary>
        public enum EnumBazl
        {
            [Description("Hạng A")]
            I = 1,
            [Description("Hạng B")]
            II,
            [Description("Hạng C")]
            III
        }
        /// <summary>
        /// Cách rời viện
        /// </summary>
        public enum EnumLyfs
        {
            [Description("Rời viện theo y lệnh")]
            yzly = 1,
            /// <summary>
            /// Cấp cứu
            /// </summary>
            [Description("Y lệnh chuyển viện")]
            yzzy,
            [Description("Y lệnh chuyển tới cơ sở dịch vụ y tế cộng đồng/Trạm y tế xã")]
            yzzsq,
            [Description("Rời viện không theo y lệnh")]
            fyz,
            [Description("Tử vong")]
            sw,
            [Description("Khác")]
            other = 9,
        }
        /// <summary>
        /// Phân loại tình trạng bệnh
        /// </summary>
        public enum EnumBqfx
        {
            [Description("Nguy kịch")]
            bw = 1,
            [Description("Bệnh nặng")]
            bz,
            [Description("Khó")]
            yn,
            [Description("Cấp cứu")]
            qj,
            [Description("Thông thường")]
            yb
        }
        /// <summary>
        /// Nguyên nhân biến dị đường lâm sàng
        /// </summary>
        public enum EnumBYYY
        {
            [Description("Biến dị dương")]
            zby = 1,
            [Description("Biến dị âm")]
            fby,
            [Description("Nguyên nhân hệ thống")]
            xtyy
        }
        /// <summary>
        /// Tình hình kiểm tra
        /// </summary>
        public enum EnumJBQK
        {
            [Description("CT")]
            CT = 1,
            [Description("PETCT")]
            PETCT = 2,
            [Description("Hai nguồn")]
            SY = 3,
            [Description("BSiêu âm")]
            Bc = 4,
            [Description("Xphim")]
            Xp = 5,
            [Description("Siêu âm tim")]
            CSXDT = 6,
            [Description("MRI")]
            MRI = 7
        }
        /// <summary>
        /// Tình hình kiểm tra
        /// </summary>
        public enum EnumJCQK
        {
            [Description("Dương tính")]
            Yang = 1,
            [Description("Âm tính")]
            Non = 2,
            [Description("Chưa làm")]
            Yin = 3,


        }

        /// <summary>
        ///Tình hình sử dụng thuốc kháng khuẩn 
        /// </summary>
        public enum EnumKJYWSYQK
        {
            [Description("Loại Ⅰ")]
            first = 1,
            [Description("Liên II")]
            second = 2,
            [Description("Liên III")]
            third = 3,
            [Description("Liên IV")]
            forth = 4,
            [Description("Liên > IV")]
            more = 5
        }

        /// <summary>
        ///Phản ứng truyền dịch 0Chưa nhập 2Không có 3Có
        /// </summary>
        public enum EnumSYFY
        {
            [Description("Chưa nhập")]
            first = 0,
            [Description("Không có")]
            second = 1,
            [Description("Có")]
            third = 2
        }

        /// <summary>
        ///Phản ứng truyền máu 0Chưa nhập 2Không có 3Có
        /// </summary>
        public enum EnumSXFY
        {
            [Description("Có")]
            Y = 1,
            [Description("Không")]
            N = 2,
            [Description("Chưa nhập")]
            none = 3
        }

        /// <summary>
        ///Mức độ tổn thương do ngã khi nội trú 0Chưa nhập 2Không có 3Có
        /// </summary>
        public enum EnumZYDD_SHCD
        {
            [Description("Cấp một")]
            first = 1,
            [Description("Cấp hai")]
            second = 2,
            [Description("Cấp ba")]
            third = 3,
            [Description("Chưa gây tổn thương")]
            none = 4
        }

        /// <summary>
        ///Nguyên nhân ngã hoặc rơi khỏi giường
        /// </summary>
        public enum EnumZYDD_YY
        {
            [Description("Nguyên nhân sức khỏe")]
            jkyy = 1,
            [Description("Điều trị,Thuốc,Nguyên nhân gây mê")]
            zlywmzyy = 2,
            [Description("Yếu tố môi trường")]
            hjys = 3,
            [Description("Nguyên nhân khác")]
            qtyy = 4
        }

        /// <summary>
        ///Phân giai đoạn loét do tì đè
        /// </summary>
        public enum EnumYCFQ
        {
            [Description("Đợt 1")]
            first = 1,
            [Description("Đợt 2")]
            second = 2,
            [Description("Đợt 3")]
            third = 3,
            [Description("Đợt 4")]
            forth = 4
        }



    }
    #endregion


    #region OR
    public class OrClientEnum : ClientEnum
    {
        /// <summary>
        /// Đăng kýTrạng thái
        /// </summary>
        public enum EnumSqzt
        {
            [Description("Chờ duyệt")]
            /// <summary>
            /// Đã đăng ký, chờ duyệt(Lịch trực)
            /// </summary>
            dsh = 1,
            [Description("Đã duyệt")]
            /// <summary>
            /// Đã duyệt(Lịch trực)Chờ phẫu thuật
            /// </summary>
            ypb = 2,
            [Description("Đã hủy đăng ký")]
            /// <summary>
            /// Đã hủy đăng ký
            /// </summary>
            yqx = 3,

        }

        /// <summary>
        /// Trạng thái phẫu thuật
        /// </summary>
        public enum EnumSSzt
        {
            [Description("Chờ xếp lịch")]
            /// <summary>
            /// Đã duyệt(Lịch trực)
            /// </summary>
            dpb = 1,
            [Description("Đã xếp lịch")]
            /// <summary>
            /// Đã duyệt(Lịch trực)Chờ phẫu thuật
            /// </summary>
            ypb = 2,
            [Description("Đã hủy")]
            /// <summary>
            /// Đã hủy đăng ký
            /// </summary>
            yzf = 3,
            [Description("Đã đăng ký")]
            /// <summary>
            /// Đã hủy đăng ký
            /// </summary>
            yzx = 4

        }

        /// <summary>
        /// Trạng thái đăng ký phẫu thuật
        /// </summary>
        public enum EnumSSdjzt
        {
            [Description("Chờ đăng ký")]
            ddj = 1,
            [Description("Đã đăng ký")]
            ydj = 2,
            [Description("Đăng ký bị hủy")]
            yzf = 3
        }
        /// <summary>
        /// Cấp phẫu thuật
        /// </summary>
        public enum EnumSSjb
        {
            [Description("ICấp")]
            yj = 1,
            [Description("IICấp")]
            ej = 2,
            [Description("IIICấp")]
            sj = 3,
            [Description("IVCấp")]
            fj = 4
        }

        /// <summary>
        /// Có cách ly hay không
        /// </summary>
        public enum EnumIsgl
        {
            [Description("Không")]
            no = 0,
            [Description("Có")]
            yes = 1
        }

        /// <summary>
        /// Có vi khuẩn hay không
        /// </summary>
        public enum EnumIsjun
        {
            [Description("Không")]
            no = 0,
            [Description("Có")]
            yes = 1
        }
    }
    #endregion

    #region PDS
    public class PdsClientEnum : ClientEnum
    {/// <summary>
     /// Loại chứng từ
     /// </summary>
        public enum EnumDanJuLX
        {
            /// <summary>
            /// Nhập kho thuốc
            /// </summary>
            [Description("Nhập kho thuốc")]   //Số chứng từYPRKD111111111111
            yaopinruku = 1,

            /// <summary>
            /// Xuất kho ra ngoài
            /// </summary>
            [Description("Xuất kho ra ngoài")]   //Số chứng từWBCKD111111111111
            waibucuku = 2,

            /// <summary>
            /// Phát thuốc trực tiếp
            /// </summary>
            [Description("Xuất kho trực tiếp")]   //Số chứng từNBFYD111111111111
            zhijiefayao = 3,

            /// <summary>
            /// Phát thuốc theo lĩnh
            /// </summary>
            [Description("Xuất kho theo lĩnh")]   //Số chứng từSLCKD111111111111
            shenlingfayao = 4,

            /// <summary>
            /// Trả lại thuốc phát nội bộ
            /// </summary>
            [Description("Trả lại thuốc phát nội bộ")] //Số chứng từNBFYTHD111111111111
            neibufayaotuihui = 5,

            /// <summary>
            /// Nhà thuốc phát thuốc cho khoa
            /// </summary>
            [Description("Khoa phát thuốc")] //Số chứng từKSFYD111111111111
            keshifayao = 6,

            /// <summary>
            /// Xuất kho thuốc thiết yếu
            /// </summary>
            [Description("Xuất kho thuốc thiết yếu")] // Thống kê thông tin chứng từ theo phương thức xuất kho
            jiyaochuku = 13,
            /// <summary>
            /// Xuất kho hàng loạt
            /// </summary>
            [Description("Xuất kho hàng loạt")] // Thống kê thông tin chứng từ theo phương thức xuất kho
            piliangchuku = 14,
        }

        /// <summary>
        /// Trạng thái tồn kho
        /// </summary>
        public enum EnumKCZT
        {
            /// <summary>
            /// Bật
            /// </summary>
            [Description("Bật")]
            Enabled = 1,
            /// <summary>
            /// Ngừng dùng
            /// </summary>
            [Description("Ngừng dùng")]
            Disabled = 0
        }

        /// <summary>
        /// Trạng thái bộ phận này
        /// </summary>
        public enum BenBuMenZT
        {
            /// <summary>
            /// Bình thường
            /// </summary>
            [Description("Bình thường")]
            Normal = 1,
            /// <summary>
            /// Kiểm soát
            /// </summary>
            [Description("Kiểm soát")]
            Control = 0
        }

        /// <summary>
        /// Trạng thái thuốc
        /// </summary>
        public enum EnumYPZT
        {
            /// <summary>
            /// Bật
            /// </summary>
            [Description("Bật")]
            Enabled = 1,
            /// <summary>
            /// Ngừng dùng
            /// </summary>
            [Description("Ngừng dùng")]
            Disabled = 0
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
            [Description("Hiển thị tồn kho bằng 0")]
            Xslkc = 0,
            /// <summary>
            /// Không hiển thị số lượng lý thuyết bằng 0
            /// </summary>
            [Description("Không hiển thị số lượng lý thuyết bằng 0")]
            Bxsllslwl = 1,
            /// <summary>
            /// Không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("Không hiển thị số lượng thực tế bằng 0")]
            Bxssjslwl = 2,
            /// <summary>
            /// Không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("Không hiển thị khi cả hai đều bằng 0")]
            Bxslzdwl = 4
        }

        /// <summary>
        /// Trạng thái phát phiếu lĩnh
        /// </summary>
        public enum EnumSLDDeliveryStatus
        {
            /// <summary>
            /// Chưa phát
            /// </summary>
            [Description("Chưa phát")]
            None = 0,

            /// <summary>
            /// Đã phát một phần
            /// </summary>
            [Description("Đã phát một phần")]
            Part,

            /// <summary>
            /// Đã phát toàn bộ
            /// </summary>
            [Description("Đã phát toàn bộ")]
            All,

            /// <summary>
            /// Đã chấm dứt(Một số(thậm chí toàn bộ)chi tiết về sau đều sẽ không phát nữa,nhưng việc này sẽ được coi là một phiếu lĩnh đã kết thúc)
            /// </summary>
            [Description("Đã chấm dứt")]
            Abandon
        }

        /// <summary>
        /// Kết quả duyệt
        /// </summary>
        public enum EnumDjShzt
        {
            /// <summary>
            /// Chưa duyệt
            /// </summary>
            [Description("Chưa duyệt")]
            WaitingApprove = 0,
            /// <summary>
            /// Đã thông qua
            /// </summary>
            [Description("Đã thông qua")]
            Approved = 1,
            /// <summary>
            /// Chưa đạt(Trạng thái cuối cùng)
            /// </summary>
            [Description("Chưa đạt")]
            Rejected = 2,
            /// <summary>
            /// Đã hủy(Trạng thái cuối cùng)(Chỉ khi duyệt đạt -> mới tới được trạng thái này)
            /// </summary>
            [Description("Đã hủy")]
            Cancelled = 3,
        }

        /// <summary>
        /// Cờ lãi lỗ
        /// </summary>
        public enum EnumProfitLossMark
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
            Profit = 1
        }

        /// <summary>
        /// Loại thao tác điều chỉnh giá thuốc(Thu hồi Phê duyệt Từ chối Thực hiện)
        /// </summary>
        public enum EnumPriceAdjustOperationType
        {
            /// <summary>
            /// Chờ duyệt
            /// </summary>
            [Description("Chờ duyệt")]
            None = 0,
            /// <summary>
            /// Phê duyệt
            /// </summary>
            [Description("Phê duyệt")]
            Approval = 1,
            /// <summary>
            /// Từ chối
            /// </summary>
            [Description("Từ chối")]
            Refuse = 2,
            /// <summary>
            /// Thu hồi
            /// </summary>
            [Description("Thu hồi")]
            Withdraw = 3,
            /// <summary>
            /// Thực hiện
            /// </summary>
            [Description("Thực hiện")]
            Execute = 4
        }

        /// <summary>
        /// Trạng thái thực thi điều chỉnh giá thuốc(0:Chưa thực hiện 1:Đã thực hiện)
        /// </summary>
        public enum EnumPriceAdjustZXStatus
        {
            /// <summary>
            /// Chưa thực hiện
            /// </summary>
            [Description("Chưa thực hiện")]
            Not = 0,
            /// <summary>
            /// Đã thực hiện
            /// </summary>
            [Description("Đã thực hiện")]
            Executed = 1
        }

        /// <summary>
        /// Nhóm thu phí lớn Loại nhóm lớn
        /// </summary>
        public enum EnumSfdlDllb
        {
            /// <summary>
            /// 
            /// </summary>
            [Description("Thuốc")]
            Yp = 1,
            /// <summary>
            /// 
            /// </summary>
            [Description("Hạng mục điều trị")]
            Zlxm,
            /// <summary>
            /// 
            /// </summary>
            [Description("Hạng mục không điều trị")]
            Flzxm
        }

        /// <summary>
        /// Ghi chú phát thuốc 0:Chưa phát;1:Đã xếp;2:Đã phát;3:Đã trả
        /// </summary>
        public enum EnumFybz
        {
            /// <summary>
            /// Chưa xếp
            /// </summary>
            [Description("Chưa xếp")]
            Wp = 0,

            /// <summary>
            /// Đã xếp
            /// </summary>
            [Description("Đã xếp")]
            Yp = 1,

            /// <summary>
            /// Đã phát
            /// </summary>
            [Description("Đã phát")]
            Yf = 2,

            /// <summary>
            /// Đã trả
            /// </summary>
            [Description("Đã trả")]
            Yt = 3
        }

        /// <summary>
        /// Tính chất y lệnh Tạm thời,Dài hạn
        /// </summary>
        public enum Yzxz
        {
            /// <summary>
            /// None
            /// </summary>
            [Description("Tất cả")]
            None = 0,

            /// <summary>
            /// Tạm thời
            /// </summary>
            [Description("Tạm thời")]
            Ls = 1,

            /// <summary>
            /// Dài hạn
            /// </summary>
            [Description("Dài hạn")]
            Cq = 2,
        }
        public enum Yzlx
        {
            [Description("Thuốc")]
            Yp = 2,
            /// <summary>
            /// Thảo dược
            /// </summary>
            [Description("Thảo dược")]
            zcy = 10,
        }

        /// <summary>
        /// Hồ sơ thao tác thuốc zy_ypyzczjl.operateType và mz_cfypczjl.operateType
        /// </summary>
        public enum EnumOperateType
        {
            /// <summary>
            /// None
            /// </summary>
            None = 0,

            /// <summary>
            /// Phát thuốc
            /// </summary>
            [Description("Phát thuốc")]
            Fy = 1,

            /// <summary>
            /// Trả thuốc
            /// </summary>
            [Description("Trả thuốc")]
            Ty = 2
        }

        /// <summary>
        /// Cách phát thuốc
        /// </summary>
        public enum EnumFyfs
        {
            /// <summary>
            /// Xuất kho bình thường
            /// </summary>
            [Description("Xuất kho bình thường")]
            Zcck = 11,

            /// <summary>
            /// Xuất kho trả hàng
            /// </summary>
            [Description("Xuất kho trả hàng")]
            Thck = 5,

            /// <summary>
            /// Xuất kho tặng
            /// </summary>
            [Description("Xuất kho tặng")]
            Zsck = 12,

            /// <summary>
            /// Xuất kho cứu trợ thiên tai
            /// </summary>
            [Description("Xuất kho cứu trợ thiên tai")]
            Jzck = 13,
        }


        /// <summary>
        /// Mô hình mua sắm
        /// </summary>
        public enum EnumCGLX
        {
            [Description("Đấu thầu trong phạm vi BHYT")]
            ybfwzb = 1,
            [Description("Chưa đấu thầu trong phạm vi BHYT")]
            ybfwwzb = 2,
            [Description("Đấu thầu trong phạm vi BHYT nhưng không trúng thầu")]
            ybfwzbwzb = 3,
            [Description("Niêm yết mạng trong phạm vi BHYT")]
            ybfwgw = 4,
            [Description(" Niêm yết mạng trong phạm vi tự chi trả")]
            zffwgw = 5,
            [Description("Mua sắm tập trung gắn lượng-giá cấp thành phố")]
            sjljggjzcg = 6,
            [Description("Không cho phép mua sắm")]
            byxcg = 7,
            [Description("Khác")]
            qt = 9,
        }
        /// <summary>
        /// Loại hàng hóa
        /// </summary>
        public enum EnumSPLX
        {
            [Description("Loại thuốc")]
            ypl = 1,
            [Description("Loại vật tư, thiết bị y tế")]
            yyhcqxl = 2,
            [Description("Khác")]
            qt = 9,
        }
        /// <summary>
        /// Đơn vị đo lường mua sắm
        /// </summary>
        public enum EnumCGJLDW
        {
            [Description("Đơn vị tính giá")]
            jjdw = 1,
            [Description("Đơn vị dùng nhỏ nhất")]
            zxsydw = 2,
        }


    }
    #endregion

    #region Hrp
    public class HrpClientEnum : ClientEnum
    {
        /// <summary>
        /// Cấp kho
        /// </summary>
        public enum EnumWarehouseLevel
        {
            /// <summary>
            /// Cấp một
            /// </summary>
            [Description("Cấp một")]
            OneLevel = 1,

            /// <summary>
            /// Cấp hai
            /// </summary>
            [Description("Cấp hai")]
            TwoLevel = 2,

            /// <summary>
            /// Cấp ba
            /// </summary>
            [Description("Cấp ba")]
            ThreeLevel = 3,

            /// <summary>
            /// Cấp bốn
            /// </summary>
            [Description("Cấp bốn")]
            FourLevel = 4,

            /// <summary>
            /// Cấp năm
            /// </summary>
            [Description("Cấp năm")]
            FiveLevel = 5,

            /// <summary>
            /// Cấp sáu
            /// </summary>
            [Description("Cấp sáu")]
            SixLevel = 6,

        }

        /// <summary>
        /// Hiệu lực/không hợp lệ
        /// </summary>
        public enum Enumzt
        {
            /// <summary>
            /// không hợp lệ
            /// </summary>
            [Description("không hợp lệ")]
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
            /// Duyệt đạt
            /// </summary>
            [Description("Duyệt đạt")]
            Adopt = 1,

            /// <summary>
            /// Duyệt không đạt
            /// </summary>
            [Description("Duyệt không đạt")]
            Refuse = 2,

            /// <summary>
            /// Đã hủy
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
            /// Nhập kho từ ngoài
            /// </summary>
            [Description("Nhập kho từ ngoài")]
            Wbrk = 1,

            /// <summary>
            /// Xuất kho ra ngoài
            /// </summary>
            [Description("Xuất kho ra ngoài")]
            Wbck = 2,

            /// <summary>
            /// Xuất kho trực tiếp
            /// </summary>
            [Description("Xuất kho trực tiếp")]
            Zjck = 3,

            /// <summary>
            /// Xuất kho theo lĩnh
            /// </summary>
            [Description("Xuất kho theo lĩnh")]
            Slck = 4,

            /// <summary>
            /// Trả hàng nội bộ
            /// </summary>
            [Description("Trả hàng nội bộ")]
            Nbth = 5,

            /// <summary>
            /// Báo hỏng/báo thừa
            /// </summary>
            [Description("Báo hỏng/báo thừa")]
            Bsby = 6,

            /// <summary>
            /// Giao tới khoa
            /// </summary>
            [Description("Xuất kho tới khoa")] //Số chứng từckZKS111111111111
            chukuzhikeshi = 7,

            /// <summary>
            /// Khoa lĩnh
            /// </summary>
            [Description("Khoa lĩnh")]
            kssl = 8,

            /// <summary>
            /// Xuất kho theo lĩnh của khoa
            /// </summary>
            [Description("Xuất kho theo lĩnh của khoa")]
            ksSlck = 9,

            /// <summary>
            /// Kế hoạch mua sắm
            /// </summary>
            [Description("Kế hoạch mua sắm")]
            purchasingPlan = 10,

            /// <summary>
            /// Đơn đặt hàng mua sắm
            /// </summary>
            [Description("Đơn đặt hàng mua sắm")]
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
            [Description("Hiển thị tồn kho bằng 0")]
            Xslkc = 0,
            /// <summary>
            /// Không hiển thị số lượng lý thuyết bằng 0
            /// </summary>
            [Description("Không hiển thị số lượng lý thuyết bằng 0")]
            Bxsllslwl = 1,
            /// <summary>
            /// Không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("Không hiển thị số lượng thực tế bằng 0")]
            Bxssjslwl = 2,
            /// <summary>
            /// Không hiển thị số lượng thực tế bằng 0
            /// </summary>
            [Description("Không hiển thị khi cả hai đều bằng 0")]
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
            /// Lãi lỗ
            /// </summary>
            [Description("Lãi lỗ")]
            ProfitAndLoss = 3,

            /// <summary>
            /// Duyệt chứng từ nhập/xuất kho
            /// </summary>
            [Description("Duyệt chứng từ nhập/xuất kho")]
            OutgoingOrWarehousing = 4
        }

        /// <summary>
        /// Cờ lãi lỗ
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
        /// Cờ duyệt điều chỉnh giá 0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã thu hồi
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
            /// Đã thu hồi
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
            /// Chưa thực hiện
            /// </summary>
            [Description("Chưa thực hiện")]
            Not = 0,

            /// <summary>
            /// Đã thực hiện
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
        /// Quá trình xử lý   1:Chờ xử lý;2:Duyệt đạt;3:Duyệt không đạt;4:Giao hàng;5:Hoàn thành một phần;6:Hoàn thành;7:Từ chối nhận
        /// </summary>
        public enum EnumApplyProcess
        {
            /// <summary>
            /// Chờ xử lý
            /// </summary>
            [Description("Chờ xử lý")]
            Waiting = 1,

            /// <summary>
            /// Duyệt đạt
            /// </summary>
            [Description("Duyệt đạt")]
            AuditApproved = 2,

            /// <summary>
            /// Duyệt không đạt
            /// </summary>
            [Description("Duyệt không đạt")]
            AuditFailed = 3,

            /// <summary>
            /// Đang giao hàng
            /// </summary>
            [Description("Đang giao hàng")]
            Distributing = 4,

            /// <summary>
            /// Hoàn thành một phần
            /// </summary>
            [Description("Hoàn thành một phần")]
            PartialCompletion = 5,

            /// <summary>
            /// Hoàn thành toàn bộ
            /// </summary>
            [Description("Hoàn thành toàn bộ")]
            Completion = 6,

            /// <summary>
            /// Giao hàng bị từ chối nhận
            /// </summary>
            [Description("Giao hàng bị từ chối nhận")]
            Rejection = 7,
        }

        /// <summary>
        /// Loại chứng từ 1-Khoa lĩnh;2-Kho lĩnh
        /// </summary>
        public enum EnumApplyType
        {
            /// <summary>
            /// Chờ xử lý
            /// </summary>
            [Description("Khoa lĩnh")]
            DepartmentApply = 1,

            /// <summary>
            /// Duyệt đạt
            /// </summary>
            [Description("Kho lĩnh")]
            WarehouseApply = 2,
        }

        /// <summary>
        /// Quy trình xử lý đơn đặt hàng mua sắm -1:Từ chối xử lý; 0:Chờ xử lý; 1:Chuẩn bị hàng; 2:Giao hàng; 3:Ký nhận; 4:Hoàn thành; 5:Từ chối ký nhận 
        /// </summary>
        public enum EnumOrderProcess
        {
            /// <summary>
            /// Từ chối xử lý
            /// </summary>
            [Description("Từ chối xử lý")]
            Refusal = -1,

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
            /// Giao hàng
            /// </summary>
            [Description("Giao hàng")]
            Delivering = 2,

            /// <summary>
            /// Ký nhận
            /// </summary>
            [Description("Ký nhận")]
            SignFor = 3,

            /// <summary>
            /// Hoàn thành
            /// </summary>
            [Description("Hoàn thành")]
            Complete = 4,

            /// <summary>
            /// Từ chối ký nhận
            /// </summary>
            [Description("Từ chối ký nhận")]
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
            [Description("Phiếu lưu tạm")]
            TempOrder = 0,

            /// <summary>
            /// Phiếu chính thức
            /// </summary>
            [Description("Phiếu chính thức")]
            OfficialOrder = 1,

            /// <summary>
            /// Phiếu hủy
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
            /// Tự chi trả
            /// </summary>
            [Description("Tự chi trả")]
            ZF = 1,
            /// <summary>
            /// Hạng A
            /// </summary>
            [Description("Hạng A")]
            J = 4,
            /// <summary>
            /// Hạng B
            /// </summary>
            [Description("Hạng B")]
            Y = 5,
            /// <summary>
            /// Hạng C
            /// </summary>
            [Description("Hạng C")]
            B = 6,
        }


        #region Mua sắm

        //4.1.1 Loại thao tác
        public enum EnumCZLX
        {
            [Description("Thêm mới")] XZ = 1,
            [Description("Hủy")] ZF = 3,
        }

        //4.1.2 Loại phiếu mua sắm
        public enum EnumCGDLX
        {
            [Description("Phiếu mua sắm tự thực hiện của bệnh viện")] YYZXCGD = 1,
            [Description("Phiếu mua sắm kho thuốc ủy thác")] TGYKCGD = 2,
        }

        //4.1.3 Loại mua sắm
        public enum EnumCGLX
        {
            [Description("Mua sắm đấu thầu")]
            ZBCG = 1,
            [Description("Mua sắm theo lượng")]
            DLCG = 2,
            [Description("Mua sắm niêm yết mạng")]
            GWCG = 3,
            [Description("Khác")]
            QT = 9,
        }

        //4.1.4 Yêu cầu giao hàng
        public enum EnumPSYQ
        {
            [Description("Giao hàng theo phiếu")] ADPS = 1,
            [Description("Giao hàng theo nhu cầu")] AXPS = 2,
        }

        //4.1.5 Loại trả hàng

        public enum EnumTHLX
        {
            [Description("Trả hàng bình thường")] ZCTH = 1,
            [Description("Trả hàng bù chênh lệch giá")] BCJTH = 2,

        }

        //4.1.6 Loại nghiệm thu giao hàng
        public enum EnumPSYSLX
        {
            [Description("Nghiệm thu trước")] YYS = 1,
            [Description("Nghiệm thu thực tế")] SYS = 2,

        }

        //4.1.7 Bên điền phiếu
        public enum EnumDJTXF
        {
            [Description("Bên mua điền")]
            CGFTX = 1,
            [Description("Bên bán điền thay")]
            XSFDT = 2,
        }

        //4.1.8 Trạng thái phiếu giao hàng
        public enum EnumPSDZT
        {
            [Description("Chờ xác nhận")] DQR = 0,
            [Description("Hủy")] ZF = 1,
            [Description("Đã xác nhận,Chưa nghiệm thu")] YQR = 10,

        }
        //4.1.9 Trạng thái chi tiết mua sắm
        public enum EnumCGMXZT
        {
            [Description("Chờ nộp")] DTJ = 0,
            [Description("Đã hủy")] YZF = 1,
            [Description("Chờ duyệt")] DSH = 10,
            [Description("Duyệt không đạt")] SHBTG = 11,
            [Description("Chờ doanh nghiệp tiếp nhận xác nhận")] DQYJSQR = 20,
            [Description("Doanh nghiệp đã tiếp nhận xác nhận giao hàng")] QYYJSQRPS = 21,
            [Description("Doanh nghiệp đã tiếp nhận xác nhận không giao hàng")] QYYJSQRBYPS = 22,
            [Description("Doanh nghiệp đã giao hàng xong")] QYYPSWC = 23,
        }

        //4.1.10 Tồn kho doanh nghiệp
        public enum EnumQYKC
        {
            [Description("Đầy đủ")] CZ = 1,
            [Description("Có hàng, Có hàng, giới hạn số lượng")] YHXL = 2,
            [Description("Có hàng, Có hàng, số lượng ít")] YHSL = 3,
            [Description("Tạm thời không có hàng")] ZWH = 4,
            [Description("Không công bố")] BYPL = 5,
        }

        //4.1.11 Trạng thái hóa đơn
        public enum EnumFPZT
        {
            [Description("Chờ xác nhận")] DQR = 0,
            [Description("Đã hủy")] YZF = 1,
            [Description("Chưa nghiệm thu")] WYS = 10,
            [Description("Đã nghiệm thu")] YYS = 20,
            [Description("Đã từ chối nhận")] YJS = 21,
            [Description("Đã thanh toán")] FPYZF = 30,
        }

        //4.1.12 Trạng thái chi tiết giao hàng
        public enum EnumPSMXZT
        {
            [Description("Chờ xác nhận")] DQR = 0,
            [Description("Đã hủy")] YZF = 1,
            [Description("Đã nộp, chưa nghiệm thu")] YTJWYS = 10,
            [Description("Đang nghiệm thu")] YSZ = 20,
            [Description("Nghiệm thu nhập kho xong")] YSRKWC = 30,
        }

        //4.1.13 Trạng thái chi tiết trả hàng
        public enum EnumTHMXZT
        {
            [Description("Chờ xác nhận")] DQR = 0,
            [Description("Đã hủy")] YZF = 1,
            [Description("Đã xác nhận, chờ doanh nghiệp tiếp nhận")] YQRDQYJS = 10,
            [Description("Doanh nghiệp đã xác nhận xử lý trả hàng")] QYYQRTHCL = 20,
            [Description("Doanh nghiệp đã xác nhận không xử lý")] QYYQRBYCL = 21,
        }

        //4.1.14 Loại giấy tờ
        public enum EnumZJLX
        {
            [Description("Giấy chứng nhận mã tổ chức")] ZZJGDMZ = 1,
            [Description("Giấy phép kinh doanh")] YYZZ = 2,
            [Description("Giấy phép hoạt động")] JYXKZ = 3,
            [Description("Giấy phép sản xuất")] SCXKZ = 4,
        }

        //4.1.15 Loại mã vạch chi tiết giao hàng
        public enum EnumPSMXTMLX
        {
            [Description("GS1 Mã vạch")] GSI = 1,
            [Description("HIBC Mã vạch")] HIBC = 2,
            [Description("Khác")] QT = 99,
        }

        //4.1.16 Kết quả nghiệm thu hóa đơn
        public enum EnumFPYSJG
        {
            [Description("Nghiệm thu đạt")] YSTG = 1,
            [Description("Nghiệm thu không đạt")] YSBTG = 0,
        }

        //4.1.17 Trạng thái phiếu mua sắm
        public enum EnumCGDZT
        {
            [Description("Chờ nộp")] DTJ = 0,
            [Description("Đã hủy")] YZF = 1,
            [Description("Đã nộp")] YTJ = 20,
        }

        //4.1.18 Trạng thái phiếu trả hàng
        public enum EnumTHDZT
        {
            [Description("Chờ xác nhận")] DQR = 0,
            [Description("Đã hủy")] YZF = 1,
            [Description("Đã xác nhận")] YQR = 10,

        }

        //4.1.19 Phương thức mua sắm

        public enum EnumCGFS
        {
            [Description("Mua sắm ngoài hệ thống")]
            FXTCG = 1,
            [Description("Mua sắm theo hệ thống")]
            AXTCG = 2,
        }

        //4.1.20 Có kèm dịch vụ đi kèm hay không
        public enum EnumSFHBSFW
        {
            [Description("Không")] F = 0,
            [Description("Có")] S = 1,
        }


        #endregion
    }
    #endregion
}
