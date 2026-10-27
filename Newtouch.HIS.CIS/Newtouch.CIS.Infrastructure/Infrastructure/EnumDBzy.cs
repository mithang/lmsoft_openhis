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
        /// Khu bệnhXuất viện（raKhu bệnh）（chờkếtsổ）
        /// </summary>
        Djz = 2,
        /// <summary>
        /// đãXuất viện（Xuất việnquyết toán）
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
        [Description("药品")]
        Yp = 2,
        /// <summary>
        /// văn bản
        /// </summary>
        [Description("文字")]
        Wz = 3,
        /// <summary>
        /// Xuất việnmang thuốc
        /// </summary>
        [Description("出院带药")]
        Cydy = 4,
        /// <summary>
        /// Mục
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
        /// ăn uốngY lệnh
        /// </summary>
        [Description("膳食医嘱")]
        ssyz = 8,
        /// <summary>
        /// Phẫu thuật
        /// </summary>
        [Description("手术")]
        oper = 9,
        /// <summary>
        /// trongthảo dược
        /// </summary>
        [Description("中草药")]
        zcy = 10,
	    /// <summary>
	    /// Phục hồi chức năng
	    /// </summary>
	    [Description("康复")]
	    rehab = 11,
        /// <summary>
	    /// vật tưVật tư tiêu hao
	    /// </summary>
	    [Description("耗材")]
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
        [Description("待审")]
        Ds = 0,
        /// <summary>
        /// Phê duyệt
        /// </summary>
        [Description("审核")]
        Sh = 1,
        /// <summary>
        /// Thực hiện
        /// </summary>
        [Description("执行")]
        Zx = 2,
        /// <summary>
        /// rútDC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// DừnghoặcHủy bỏ
        /// </summary>
        [Description("停止")]
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
    /// điều dưỡngbệnh nhânTrạng thái（điều dưỡngdấu hiệu sinh tồnnhậpdùng）
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
        [Description("发药")]
        Receive = 1,
        [Description("全退")]
        AllReturn = 2,
        [Description("部分退")]
        PartReturn = 3
    }
    /// <summary>
    /// Y lệnhTính chất
    /// </summary>
    public enum EnumYzxz
    {
        [Description("临时")]
        Ls = 1,
        [Description("长期")]
        Cq = 2
    }
    /// <summary>
    /// giường ký tựghi chépTrạng thái
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
    /// Bác sĩLoại
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
        /// đặccấp
        /// </summary>
        [Description("特级")]
        T = 4
    }
    /// <summary>
    /// quyết toánTrạng thái
    /// </summary>
    public enum EnumJszt
    {
        [Description("已结算")]
        Yjs = 1,
        [Description("未结算")]
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
        [Description("一般")]
        Normal =1,
        /// <summary>
        /// bệnhtrọng
        /// </summary>
        [Description("病重")]
        Serious =2,
        /// <summary>
        /// bệnhnguy
        /// </summary>
        [Description("病危")]
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
    /// ăn uốngLoại
    /// </summary>
    public enum EnumSSLB
    {
        /// <summary>
        /// Loại
        /// </summary>
        [Description("类型")]
        lx = 1,
        /// <summary>
        /// đặcđặccầncầu
        /// </summary>
        [Description("特殊要求")]
        tsyq = 2,
        /// <summary>
        /// ăn uốngloạisố
        /// </summary>
        [Description("膳食型号")]
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
    /// Hồ sơLoại
    /// </summary>
    public enum EnumBllx
    {
        /// <summary>
        /// Nhập việnHồ sơ
        /// </summary>
        [Description("住院病历")]
        zybl = 1,

        /// <summary>
        /// bệnhtrìnhghi chép
        /// </summary>
        [Description("病程记录")]
        bcjl = 2,

        /// <summary>
        /// y tếvănsách
        /// </summary>
        [Description("医疗文书")]
        ylws = 3,

        /// <summary>
        /// điều dưỡngghi chép
        /// </summary>
        [Description("护理记录")]
        hljl = 4,

        /// <summary>
        /// Trang đầu hồ sơ bệnh án
        /// </summary>
        /// 
        [Description("病案首页")]
        basy = 5
    }

    public enum EnumCYQK
    {
        /// <summary>
        /// khỏi bệnh
        /// </summary>
        [Description("治愈")]
        zy = 1,
        /// <summary>
        /// tốtchuyển
        /// </summary>
        [Description("好转")]
        hz = 2,
        /// <summary>
        /// chưa愈
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
        /// chuyểnviện
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
        /// là
        /// </summary>
        [Description("是")]
        s = 1,
    }

    //dathửđothử
    public enum EnumPSCS {
        /// <summary>
        /// Không
        /// </summary>
        [Description("否")]
        f = 0,
        /// <summary>
        /// là
        /// </summary>
        [Description("是")]
        s = 1,
    }
    /// <summary>
    /// dathửkếtnếu
    /// </summary>
    public enum EnumPsResult {
        [Description("阴性")]
        negative = 0,
        [Description("阳性")]
        positive = 1,
    }

    /// <summary>
    /// Khoachuẩn bị thuốcTrạng thái
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
    /// Khoachuẩn bị thuốcTrả lạiTrạng thái
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
    /// Thuốcđếnnguồn
    /// </summary>
    public enum EnumYply
    {
        [Description("科室备药")]
        ksby = 1,
        [Description("药房")]
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
        [Description("阴")]
        yin = 0,
        /// <summary>
        /// dương
        /// </summary>
        [Description("阳")]
        yang = 1,
    }
}
