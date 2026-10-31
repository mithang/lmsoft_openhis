using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Dấu hiệu đang nằm viện
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
        /// Ra khỏi khu bệnh(Ra khỏi khu bệnh)(Chờ quyết toán)
        /// </summary>
        Djz = 2,
        /// <summary>
        /// Đã ra viện(Quyết toán ra viện)
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// Chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hủy bản ghi/Hủy nhập viện
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
        /// Văn bản
        /// </summary>
        [Description("Văn bản")]
        Wz = 3,
        /// <summary>
        /// Thuốc mang về khi ra viện
        /// </summary>
        [Description("Thuốc mang về khi ra viện")]
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
        [Description("Y lệnh ăn uống")]
        ssyz = 8,
        /// <summary>
        /// Phẫu thuật
        /// </summary>
        [Description("Phẫu thuật")]
        oper = 9,
        /// <summary>
        /// Thuốc đông y
        /// </summary>
        [Description("Thuốc thảo dược Trung Quốc")]
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
        /// Thu hồi DC
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
    /// Trạng thái bệnh nhân điều dưỡng(dùng để nhập dấu hiệu sinh tồn điều dưỡng)
    /// </summary>
    public enum EnumHlbrzt
    {
        [Description("Nhập viện")]
        Ry = 1,
        [Description("Ra viện")]
        Cy = 2,
        [Description("Xin nghỉ")]
        Qj = 3,
        [Description("Chuyển vào")]
        Zr = 4,
        [Description("Chuyển ra")]
        Zc = 5,
        [Description("Từ chối đo")]
        Jc = 6,
        [Description("Quay lại viện")]
        Fy = 7,
        [Description("Đo lại")]
        Fc = 8
    }
    /// <summary>
    /// Đơn vị chu kỳ thực hiện
    /// </summary>
    public enum EnumZxzqdw
    {
        /// <summary>
        /// Ngày
        /// </summary>
        Day = 1,
        /// <summary>
        /// Giờ
        /// </summary>
        Hour = 2,
        /// <summary>
        /// Phút
        /// </summary>
        Minute = 3,
        /// <summary>
        /// Chu kỳ không đều
        /// </summary>
        Unruly = 4
    }
    /// <summary>
    /// Dấu hiệu phát thuốc/trả thuốc
    /// </summary>
    public enum EnumMedSTflag
    {
        [Description("Phát thuốc")]
        Receive = 1,
        [Description("Trả thuốc")]
        Return = 2
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
    /// Trạng thái bản ghi giường
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
        [Description("Bác sĩ trưởng khoa")] //chief
        ZrDoc = 3
    }
    /// <summary>
    /// Cấp độ điều dưỡng
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
        /// Đặc cấp
        /// </summary>
        [Description("Cấp đặc biệt")]
        T = 4
    }
    /// <summary>
    /// Trạng thái thanh toán
    /// </summary>
    public enum EnumJszt
    {
        [Description("Đã thanh toán")]
        Yjs = 1,
        [Description("Chưa thanh toán")]
        Wjs = 2
    }
    /// <summary>
    /// Tiêu chuẩn thực hiện cố định
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
    /// Cấp độ nguy kịch
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// Thông thường
        /// </summary>
        [Description("Bình thường")]
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
    //EnumDB Đã bao gồm
    //public enum EnumZdlx
    //{
    //    MainDiag=1,
    //    SedDiag=2
    //}

    /// <summary>
    /// Hướng dẫn đo nhiệt độ
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
        [Description("Ghi chép quá trình bệnh")]
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
        [Description("Trang đầu bệnh án")]
        basy = 5,

        /// <summary>
        /// Đánh giá phục hồi chức năng
        /// </summary>
        [Description("Đánh giá phục hồi chức năng")]
        kfpg = 6
    }
    /// <summary>
    /// Loại chẩn đoán
    /// </summary>
    public enum EnumZdlx {
        [Description("Chẩn đoán Tây y")]
        WM = 1,
        [Description("Chẩn đoán Đông y")]
        TCM =2

    }

}
