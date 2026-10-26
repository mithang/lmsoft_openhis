using System.ComponentModel;

namespace Newtouch.Infrastructure
{
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
        /// Trong khu bệnh
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Xuất viện khỏi khu bệnh (xuất khỏi khu bệnh) (chờ thanh toán)
        /// </summary>
        Djz = 2,
        /// <summary>
        /// Đã xuất viện (thanh toán xuất viện)
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// Chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Bản ghi hủy/Hủy nhập viện
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
        //[Description("通用")]
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
        /// Thuốc mang về khi xuất viện
        /// </summary>
        [Description("出院带药")]
        Cydy = 4,
        /// <summary>
        /// Hạng mục
        /// </summary>
        [Description("项目")]
        sfxm = 5,
        /// <summary>
        /// Xét nghiệm
        /// </summary>
        [Description("检验")]
        jy = 6,
        /// <summary>
        /// Kiểm tra
        /// </summary>
        [Description("检查")]
        jc = 7,
        /// <summary>
        /// Y lệnh ăn uống
        /// </summary>
        [Description("膳食医嘱")]
        ssyz = 8,
        /// <summary>
        /// Phẫu thuật
        /// </summary>
        [Description("手术")]
        oper = 9,
        /// <summary>
        /// Thảo dược
        /// </summary>
        [Description("中草药")]
        zcy = 10,
	    /// <summary>
	    /// Phục hồi chức năng
	    /// </summary>
	    [Description("康复")]
	    rehab = 11,
        /// <summary>
	    /// Vật tư tiêu hao
	    /// </summary>
	    [Description("耗材")]
        yyhc = 12

    }
    /// <summary>
    /// Trạng thái y lệnh
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// Chờ duyệt 
        /// </summary>
        [Description("待审")]
        Ds = 0,
        /// <summary>
        /// Duyệt
        /// </summary>
        [Description("审核")]
        Sh = 1,
        /// <summary>
        /// Thực thi
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
    /// Trạng thái bệnh nhân chăm sóc (dùng để nhập dấu hiệu sinh tồn chăm sóc)
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
        [Description("全退")]
        AllReturn = 2,
        [Description("部分退")]
        PartReturn = 3
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
    /// Cấp độ chăm sóc
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
        Normal =1,
        /// <summary>
        /// Bệnh nặng
        /// </summary>
        [Description("病重")]
        Serious =2,
        /// <summary>
        /// Bệnh nguy kịch
        /// </summary>
        [Description("病危")]
        Bedying =3
    }
    //EnumDB đã bao gồm
    //public enum EnumZdlx
    //{
    //    MainDiag=1,
    //    SedDiag=2
    //}

    /// <summary>
    /// Giải thích đo nhiệt độ
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
    /// Loại ăn uống
    /// </summary>
    public enum EnumSSLB
    {
        /// <summary>
        /// Loại
        /// </summary>
        [Description("类型")]
        lx = 1,
        /// <summary>
        /// Yêu cầu đặc biệt
        /// </summary>
        [Description("特殊要求")]
        tsyq = 2,
        /// <summary>
        /// Kiểu ăn uống
        /// </summary>
        [Description("膳食型号")]
        ssxh = 3,
    }

    /// <summary>
    /// Quyền sử dụng mẫu
    /// </summary>
    public enum Enummbqx
    {
        /// <summary>
        /// Dùng chung
        /// </summary>
        [Description("通用")]
        pub = 0,
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("个人")]
        prv = 1,
        /// <summary>
        /// Khoa
        /// </summary>
        [Description("科室")]
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
        [Description("住院病历")]
        zybl = 1,

        /// <summary>
        /// Bản ghi quá trình bệnh
        /// </summary>
        [Description("病程记录")]
        bcjl = 2,

        /// <summary>
        /// Văn bản y tế
        /// </summary>
        [Description("医疗文书")]
        ylws = 3,

        /// <summary>
        /// Bản ghi chăm sóc
        /// </summary>
        [Description("护理记录")]
        hljl = 4,

        /// <summary>
        /// Trang đầu bệnh án
        /// </summary>
        /// 
        [Description("病案首页")]
        basy = 5
    }

    public enum EnumCYQK
    {
        /// <summary>
        /// Chữa khỏi
        /// </summary>
        [Description("治愈")]
        zy = 1,
        /// <summary>
        /// Thuyên giảm
        /// </summary>
        [Description("好转")]
        hz = 2,
        /// <summary>
        /// Chưa khỏi
        /// </summary>
        [Description("未愈")]
        wy = 3,
        /// <summary>
        /// Tử vong
        /// </summary>
        [Description("死亡")]
        sw = 4,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("其他")]
        qt = 5,
        /// <summary>
        /// Chuyển viện
        /// </summary>
        [Description("转院")]
        zhuanyuan = 6

    }
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

    //Kiểm tra thử da
    public enum EnumPSCS {
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
    /// Kết quả thử da
    /// </summary>
    public enum EnumPsResult {
        [Description("阴性")]
        negative = 0,
        [Description("阳性")]
        positive = 1,
    }

    /// <summary>
    /// Trạng thái chuẩn bị thuốc cho khoa
    /// </summary>
    public enum EnumKSBYZT
    {
        [Description("已申请")]
        sq=0,
        [Description("已提交")]
        sh= 1,
        [Description("已审核")]
        tj= 2,
        [Description("已发药")]
        fy= 3,
        [Description("已退回")]
        th= 4,
        [Description("已作废")]
        zf= 5,
    }

    /// <summary>
    /// Trạng thái trả lại chuẩn bị thuốc cho khoa
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
    /// Nguồn thuốc
    /// </summary>
    public enum EnumYply
    {
        [Description("科室备药")]
        ksby = 1,
        [Description("药房")]
        yf = 2
    }

    /// <summary>
    /// Thử da
    /// </summary>
    public enum EnumPs
    {
        /// <summary>
        /// Âm
        /// </summary>
        [Description("阴")]
        yin = 0,
        /// <summary>
        /// Dương
        /// </summary>
        [Description("阳")]
        yang = 1,
    }
}
