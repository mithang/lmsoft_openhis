using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// ở viện Cờ
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// Nhập viện Đăng ký
        /// </summary>
        Xry = 0,
        /// <summary>
        /// Khu bệnh trong
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Khu bệnh Xuất viện(raKhu bệnh)(chờ kết sổ)
        /// </summary>
        Djz = 2,
        /// <summary>
        /// đã Xuất viện(Xuất viện quyết toán)
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hủy bỏ ghi chép/Hủy Nhập viện
        /// </summary>
        Wry = 9,
    }

    /// <summary>
    /// Kiểu y lệnh
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
        /// văn bản
        /// </summary>
        [Description("Văn bản")]
        Wz = 3,
        /// <summary>
        /// Xuất viện mang thuốc
        /// </summary>
        [Description("Thuốc mang về khi ra viện")]
        Cydy = 4,
        /// <summary>
        /// Mục
        /// </summary>
        [Description("Hạng mục")]
        sfxm = 5,
        /// <summary>
        /// Xét nghiệm
        /// </summary>
        [Description("Xét nghiệm")]
        jy = 6,
        /// <summary>
        /// Kiểm tra
        /// </summary>
        [Description("Kiểm tra")]
        jc = 7,
        /// <summary>
        /// ăn uốngY lệnh
        /// </summary>
        [Description("Y lệnh dinh dưỡng")]
        ssyz = 8,
        /// <summary>
        /// Phẫu thuật
        /// </summary>
        [Description("Phẫu thuật")]
        oper = 9,
        /// <summary>
        /// trong thảo dược
        /// </summary>
        [Description("Thuốc Đông y")]
        zcy = 10,
	    /// <summary>
	    /// Phục hồi chức năng
	    /// </summary>
	    [Description("Phục hồi chức năng")]
	    rehab = 11,
        /// <summary>
	    /// vật tư Vật tư tiêu hao
	    /// </summary>
	    [Description("Vật tư")]
        yyhc = 12

    }
    /// <summary>
    /// Y lệnh Trạng thái
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// chờ duyệt 
        /// </summary>
        [Description("Chờ duyệt")]
        Ds = 0,
        /// <summary>
        /// Phê duyệt
        /// </summary>
        [Description("Duyệt")]
        Sh = 1,
        /// <summary>
        /// Thực hiện
        /// </summary>
        [Description("Thực hiện")]
        Zx = 2,
        /// <summary>
        /// rút DC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// Dừng hoặc Hủy bỏ
        /// </summary>
        [Description("Dừng")]
        TZ = 4 
    }
    /// <summary>
    /// Đơn vị thuốc Loại
    /// </summary>
    public enum EnumYPdwlb
    {
        /// <summary>
        /// Đơn vị liều
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
        /// Đơn vị nhập viện
        /// </summary>
        Zydw = 4,
        /// <summary>
        /// vào hàng Đơn vị
        /// </summary>
        Jhdw = 5,
        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        Zxdw = 6
    }
    /// <summary>
    /// gói phạm vi
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
    /// điều dưỡng bệnh nhân Trạng thái(điều dưỡng dấu hiệu sinh tồn nhập dùng)
    /// </summary>
    public enum EnumHlbrzt
    {
        [Description("Nhập viện")]
        Ry = 1,
        [Description("Đang nằm viện")]
        Zy = 2,
        [Description("Ra viện")]
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
        [Description("Về phòng")]
        hs = 13
    }
    /// <summary>
    /// Chu kỳ thực hiện Đơn vị
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
        /// Phút
        /// </summary>
        Minute = 3,
        /// <summary>
        /// không quy thì chu kỳ
        /// </summary>
        Unruly = 4
    }
    /// <summary>
    /// phát thuốc trả thuốc Cờ
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
    /// Y lệnh Tính chất
    /// </summary>
    public enum EnumYzxz
    {
        [Description("Tạm thời")]
        Ls = 1,
        [Description("Dài hạn")]
        Cq = 2
    }
    /// <summary>
    /// giường ký tự ghi chép Trạng thái
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
    /// Bác sĩ Loại
    /// </summary>
    public enum EnumYslx
    {
        [Description("Bác sĩ nội trú")] //Resident
        ZyDoc = 1,
        [Description("Bác sĩ điều trị chính")] //Attending
        ZzDoc = 2,
        [Description("Bác sĩ trưởng khoa")] //chief
        ZrDoc = 3
    }
    /// <summary>
    /// Cấp độ chăm sóc
    /// </summary> 
    public enum EnumHljb
    {
        /// <summary>
        /// I cấp
        /// </summary>
        [Description("Cấp I")]
        I = 1,
        /// <summary>
        /// II cấp
        /// </summary>
        [Description("Cấp II")]
        II = 2,
        /// <summary>
        /// III cấp
        /// </summary>
        [Description("Cấp III")]
        III = 3,
        /// <summary>
        /// đặc cấp
        /// </summary>
        [Description("Cấp đặc biệt")]
        T = 4
    }
    /// <summary>
    /// quyết toán Trạng thái
    /// </summary>
    public enum EnumJszt
    {
        [Description("Đã quyết toán")]
        Yjs = 1,
        [Description("Chưa quyết toán")]
        Wjs = 2
    }
    /// <summary>
    /// cố định định Thực hiện tiêu chuẩn
    /// </summary>
    public enum EnumGdzxbz
    {
        /// <summary>
        /// cố định định
        /// </summary>
        Fixed = 1,
        /// <summary>
        /// không cố định định
        /// </summary>
        Nonfixed = 0
    }
    /// <summary>
    /// nguy trọng cấp loại
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// Bình thường
        /// </summary>
        [Description("Thông thường")]
        Normal =1,
        /// <summary>
        /// bệnh trọng
        /// </summary>
        [Description("Bệnh nặng")]
        Serious =2,
        /// <summary>
        /// bệnh nguy
        /// </summary>
        [Description("Nguy kịch")]
        Bedying =3
    }
    //EnumDB đã gói chứa
    //public enum EnumZdlx
    //{
    //    MainDiag=1,
    //    SedDiag=2
    //}

    /// <summary>
    /// Nhiệt độ đo lượng Mô tả
    /// </summary>
    public enum EnumTwclfs
    {
        /// <summary>
        /// miệng nhiệt
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
    /// ăn uống Loại
    /// </summary>
    public enum EnumSSLB
    {
        /// <summary>
        /// Loại
        /// </summary>
        [Description("Loại")]
        lx = 1,
        /// <summary>
        /// đặc đặc cần cầu
        /// </summary>
        [Description("Yêu cầu đặc biệt")]
        tsyq = 2,
        /// <summary>
        /// ăn uống loại số
        /// </summary>
        [Description("Mẫu bữa ăn")]
        ssxh = 3,
    }

    /// <summary>
    /// mẫu Sử dụng Quyền
    /// </summary>
    public enum Enummbqx
    {
        /// <summary>
        /// Chung
        /// </summary>
        [Description("Dùng chung")]
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
    /// Hồ sơ Loại
    /// </summary>
    public enum EnumBllx
    {
        /// <summary>
        /// Nhập viện Hồ sơ
        /// </summary>
        [Description("Bệnh án nội trú")]
        zybl = 1,

        /// <summary>
        /// bệnh trình ghi chép
        /// </summary>
        [Description("Ghi chép quá trình bệnh")]
        bcjl = 2,

        /// <summary>
        /// y tế văn sách
        /// </summary>
        [Description("Văn bản y tế")]
        ylws = 3,

        /// <summary>
        /// điều dưỡng ghi chép
        /// </summary>
        [Description("Ghi chép điều dưỡng")]
        hljl = 4,

        /// <summary>
        /// Trang đầu hồ sơ bệnh án
        /// </summary>
        /// 
        [Description("Trang đầu bệnh án")]
        basy = 5
    }

    public enum EnumCYQK
    {
        /// <summary>
        /// khỏi bệnh
        /// </summary>
        [Description("Khỏi bệnh")]
        zy = 1,
        /// <summary>
        /// tốt chuyển
        /// </summary>
        [Description("Đỡ hơn")]
        hz = 2,
        /// <summary>
        /// chưa khỏi
        /// </summary>
        [Description("Chưa khỏi")]
        wy = 3,
        /// <summary>
        /// Tử vong
        /// </summary>
        [Description("Tử vong")]
        sw = 4,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        qt = 5,
        /// <summary>
        /// chuyển viện
        /// </summary>
        [Description("Chuyển viện")]
        zhuanyuan = 6

    }
    public enum EnumSF
    {
        /// <summary>
        /// Không
        /// </summary>
        [Description("Không")]
        f = 0,
        /// <summary>
        /// là
        /// </summary>
        [Description("Có")]
        s = 1,
    }

    //da thử đo thử
    public enum EnumPSCS {
        /// <summary>
        /// Không
        /// </summary>
        [Description("Không")]
        f = 0,
        /// <summary>
        /// là
        /// </summary>
        [Description("Có")]
        s = 1,
    }
    /// <summary>
    /// da thử kết nếu
    /// </summary>
    public enum EnumPsResult {
        [Description("Âm tính")]
        negative = 0,
        [Description("Dương tính")]
        positive = 1,
    }

    /// <summary>
    /// Khoa chuẩn bị thuốc Trạng thái
    /// </summary>
    public enum EnumKSBYZT
    {
        [Description("Đã đăng ký")]
        sq=0,
        [Description("Đã nộp")]
        sh= 1,
        [Description("Đã duyệt")]
        tj= 2,
        [Description("Đã phát thuốc")]
        fy= 3,
        [Description("Đã trả lại")]
        th= 4,
        [Description("Đã hủy bỏ")]
        zf= 5,
    }

    /// <summary>
    /// Khoa chuẩn bị thuốc Trả lại Trạng thái
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
        [Description("Thu hồi")]
        ch = 5,
        [Description("Trả lại")]
        th = 6,
        [Description("Hủy bỏ")]
        zf = 7,
    }
    /// <summary>
    /// Thuốc đến nguồn
    /// </summary>
    public enum EnumYply
    {
        [Description("Thuốc dự trữ khoa")]
        ksby = 1,
        [Description("Nhà thuốc")]
        yf = 2
    }

    /// <summary>
    /// da thử
    /// </summary>
    public enum EnumPs
    {
        /// <summary>
        /// âm
        /// </summary>
        [Description("Âm")]
        yin = 0,
        /// <summary>
        /// dương
        /// </summary>
        [Description("Dương")]
        yang = 1,
    }
}
