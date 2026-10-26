using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Cờ đang ở viện
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// Đăng ký vào viện
        /// </summary>
        Xry = 0,
        /// <summary>
        /// Đang ở khu bệnh
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Ra khu bệnh (rời khu bệnh) (chờ thanh toán)
        /// </summary>
        Djz = 2,
        /// <summary>
        /// Đã ra viện (thanh toán ra viện)
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// Chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hủy bản ghi / Hủy nhập viện
        /// </summary>
        Wry = 9,
    }

    /// <summary>
    /// Loại y lệnh
    /// </summary>
    public enum EnumYzlx
    {
        ///// <summary>
        ///// Dùng chung
        ///// </summary>
        //[Description("Dùng chung")]
        //Ty = 1,
        /// <summary>
        /// Thuốc
        /// </summary>
        [Description("药品")]
        Yp = 2,
        /// <summary>
        /// Văn bản
        /// </summary>
        [Description("文字")]
        Wz = 3,
        /// <summary>
        /// Thuốc mang về khi ra viện
        /// </summary>
        [Description("出院带药")]
        Cydy = 4,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("项目")]
        sfxm = 5,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("检验")]
        jy = 6,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("检查")]
        jc = 7,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("膳食医嘱")]
        ssyz = 8,
        /// <summary>
        /// Phẫu thuật
        /// </summary>
        [Description("手术")]
        oper = 9,
        /// <summary>
        /// Thảo dược Đông y
        /// </summary>
        [Description("中草药")]
        zcy = 10,
        /// <summary>
        /// Phục hồi chức năng
        /// </summary>
        [Description("康复")]
        rehab = 11
    }
    /// <summary>
    /// Trạng thái y lệnh
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// Chờ xét duyệt 
        /// </summary>
        [Description("待审")]
        Ds = 0,
        /// <summary>
        /// Xét duyệt
        /// </summary>
        [Description("审核")]
        Sh = 1,
        /// <summary>
        /// Thực hiện
        /// </summary>
        [Description("执行")]
        Zx = 2,
        /// <summary>
        /// Hủy DC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// Dừng hoặc hủy
        /// </summary>
        [Description("停止")]
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
    /// Trạng thái bệnh nhân điều dưỡng (dùng để nhập dấu hiệu sinh tồn điều dưỡng)
    /// </summary>
    public enum EnumHlbrzt
    {
        [Description("入院")]
        Ry = 1,
        [Description("出院")]
        Cy = 2,
        [Description("请假")]
        Qj = 3,
        [Description("转入")]
        Zr = 4,
        [Description("转出")]
        Zc = 5,
        [Description("拒测")]
        Jc = 6,
        [Description("返院")]
        Fy = 7,
        [Description("复测")]
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
    /// Cờ phát thuốc/trả thuốc
    /// </summary>
    public enum EnumMedSTflag
    {
        [Description("发药")]
        Receive = 1,
        [Description("退药")]
        Return = 2
    }
    /// <summary>
    /// Tính chất y lệnh
    /// </summary>
    public enum EnumYzxz
    {
        [Description("临时")]
        Ls = 1,
        [Description("长期")]
        Cq = 2
    }
    /// <summary>
    /// Trạng thái bản ghi giường bệnh
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
    /// Loại bác sĩ
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
    /// Cấp độ điều dưỡng
    /// </summary> 
    public enum EnumHljb
    {
        /// <summary>
        /// Cấp I
        /// </summary>
        [Description("I级")]
        I = 1,
        /// <summary>
        /// Cấp II
        /// </summary>
        [Description("II级")]
        II = 2,
        /// <summary>
        /// Cấp III
        /// </summary>
        [Description("III级")]
        III = 3,
        /// <summary>
        /// Cấp đặc biệt
        /// </summary>
        [Description("特级")]
        T = 4
    }
    /// <summary>
    /// Trạng thái thanh toán
    /// </summary>
    public enum EnumJszt
    {
        [Description("已结算")]
        Yjs = 1,
        [Description("未结算")]
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
        [Description("一般")]
        Normal = 1,
        /// <summary>
        /// Bệnh nặng
        /// </summary>
        [Description("病重")]
        Serious = 2,
        /// <summary>
        /// Bệnh nguy kịch
        /// </summary>
        [Description("病危")]
        Bedying = 3
    }
    //EnumDB đã bao gồm
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
        [Description("口温")]
        kw = 1,
        /// <summary>
        /// Nhiệt độ nách
        /// </summary>
        [Description("腋温")]
        yw = 2,
        /// <summary>
        /// Nhiệt độ hậu môn
        /// </summary>
        [Description("肛温")]
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
        [Description("住院病历")]
        zybl = 1,

        /// <summary>
        /// Ghi chép diễn biến bệnh
        /// </summary>
        [Description("病程记录")]
        bcjl = 2,

        /// <summary>
        /// Văn bản y tế
        /// </summary>
        [Description("医疗文书")]
        ylws = 3,

        /// <summary>
        /// Ghi chép điều dưỡng
        /// </summary>
        [Description("护理记录")]
        hljl = 4,

        /// <summary>
        /// Trang đầu hồ sơ bệnh án
        /// </summary>
        [Description("病案首页")]
        basy = 5,

        /// <summary>
        /// Đánh giá phục hồi chức năng
        /// </summary>
        [Description("康复评估")]
        kfpg = 6
    }
    /// <summary>
    /// Loại chẩn đoán
    /// </summary>
    public enum EnumZdlx {
        [Description("西医诊断")]
        WM = 1,
        [Description("中医诊断")]
        TCM =2

    }

}
