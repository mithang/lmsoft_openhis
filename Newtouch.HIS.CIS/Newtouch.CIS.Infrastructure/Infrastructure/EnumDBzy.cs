using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// ởviệnCờ
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// Nhập việnĐăng ký
        /// </summary>
        Xry = 0,
        /// <summary>
        /// Khu bệnhtrong
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Khu bệnhXuất viện(raKhu bệnh)(chờkếtsổ)
        /// </summary>
        Djz = 2,
        /// <summary>
        /// đãXuất viện(Xuất việnquyết toán)
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hủy bỏghi chép/HủyNhập viện
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
        /// Xuất việnmang thuốc
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
        /// trongthảo dược
        /// </summary>
        [Description("Thuốc Đông y")]
        zcy = 10,
	    /// <summary>
	    /// Phục hồi chức năng
	    /// </summary>
	    [Description("Phục hồi chức năng")]
	    rehab = 11,
        /// <summary>
	    /// vật tưVật tư tiêu hao
	    /// </summary>
	    [Description("Vật tư")]
        yyhc = 12

    }
    /// <summary>
    /// Y lệnhTrạng thái
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// chờduyệt 
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
        /// rútDC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// DừnghoặcHủy bỏ
        /// </summary>
        [Description("Dừng")]
        TZ = 4 
    }
    /// <summary>
    /// Đơn vị thuốcLoại
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
        /// vàohàngĐơn vị
        /// </summary>
        Jhdw = 5,
        /// <summary>
        /// Đơn vị nhỏ nhất
        /// </summary>
        Zxdw = 6
    }
    /// <summary>
    /// góiphạm vi
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
    /// điều dưỡngbệnh nhânTrạng thái(điều dưỡngdấu hiệu sinh tồnnhậpdùng)
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
    /// Chu kỳ thực hiệnĐơn vị
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
        /// khôngquythìchu kỳ
        /// </summary>
        Unruly = 4
    }
    /// <summary>
    /// phát thuốctrảthuốcCờ
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
    /// Y lệnhTính chất
    /// </summary>
    public enum EnumYzxz
    {
        [Description("Tạm thời")]
        Ls = 1,
        [Description("Dài hạn")]
        Cq = 2
    }
    /// <summary>
    /// giường ký tựghi chépTrạng thái
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
    /// Bác sĩLoại
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
        /// Icấp
        /// </summary>
        [Description("Cấp I")]
        I = 1,
        /// <summary>
        /// IIcấp
        /// </summary>
        [Description("Cấp II")]
        II = 2,
        /// <summary>
        /// IIIcấp
        /// </summary>
        [Description("Cấp III")]
        III = 3,
        /// <summary>
        /// đặccấp
        /// </summary>
        [Description("Cấp đặc biệt")]
        T = 4
    }
    /// <summary>
    /// quyết toánTrạng thái
    /// </summary>
    public enum EnumJszt
    {
        [Description("Đã quyết toán")]
        Yjs = 1,
        [Description("Chưa quyết toán")]
        Wjs = 2
    }
    /// <summary>
    /// cố địnhđịnhThực hiệntiêu chuẩn
    /// </summary>
    public enum EnumGdzxbz
    {
        /// <summary>
        /// cố địnhđịnh
        /// </summary>
        Fixed = 1,
        /// <summary>
        /// khôngcố địnhđịnh
        /// </summary>
        Nonfixed = 0
    }
    /// <summary>
    /// nguytrọngcấploại
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// Bình thường
        /// </summary>
        [Description("Thông thường")]
        Normal =1,
        /// <summary>
        /// bệnhtrọng
        /// </summary>
        [Description("Bệnh nặng")]
        Serious =2,
        /// <summary>
        /// bệnhnguy
        /// </summary>
        [Description("Nguy kịch")]
        Bedying =3
    }
    //EnumDBđãgóichứa
    //public enum EnumZdlx
    //{
    //    MainDiag=1,
    //    SedDiag=2
    //}

    /// <summary>
    /// Nhiệt độđolượngMô tả
    /// </summary>
    public enum EnumTwclfs
    {
        /// <summary>
        /// miệngnhiệt
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
    /// ăn uốngLoại
    /// </summary>
    public enum EnumSSLB
    {
        /// <summary>
        /// Loại
        /// </summary>
        [Description("Loại")]
        lx = 1,
        /// <summary>
        /// đặcđặccầncầu
        /// </summary>
        [Description("Yêu cầu đặc biệt")]
        tsyq = 2,
        /// <summary>
        /// ăn uốngloạisố
        /// </summary>
        [Description("Mẫu bữa ăn")]
        ssxh = 3,
    }

    /// <summary>
    /// mẫuSử dụngQuyền
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
    /// Hồ sơLoại
    /// </summary>
    public enum EnumBllx
    {
        /// <summary>
        /// Nhập việnHồ sơ
        /// </summary>
        [Description("Bệnh án nội trú")]
        zybl = 1,

        /// <summary>
        /// bệnhtrìnhghi chép
        /// </summary>
        [Description("Ghi chép quá trình bệnh")]
        bcjl = 2,

        /// <summary>
        /// y tếvănsách
        /// </summary>
        [Description("Văn bản y tế")]
        ylws = 3,

        /// <summary>
        /// điều dưỡngghi chép
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
        /// tốtchuyển
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
        /// chuyểnviện
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

    //dathửđothử
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
    /// dathửkếtnếu
    /// </summary>
    public enum EnumPsResult {
        [Description("Âm tính")]
        negative = 0,
        [Description("Dương tính")]
        positive = 1,
    }

    /// <summary>
    /// Khoachuẩn bị thuốcTrạng thái
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
    /// Khoachuẩn bị thuốcTrả lạiTrạng thái
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
    /// Thuốcđếnnguồn
    /// </summary>
    public enum EnumYply
    {
        [Description("Thuốc dự trữ khoa")]
        ksby = 1,
        [Description("Nhà thuốc")]
        yf = 2
    }

    /// <summary>
    /// dathử
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
