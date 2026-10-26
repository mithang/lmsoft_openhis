using System.ComponentModel;

namespace Newtouch.MR.ManageSystem.Infrastructure
{
    /// <summary>
    /// Đang nằm việnCờ
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// Đăng ký nhập viện
        /// </summary>
        Xry = 0,
        /// <summary>
        /// Khu bệnhtrong
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// Khu bệnhXuất viện (rờiKhu bệnh）（chờ thanh toán）
        /// </summary>
        Djz = 2,
        /// <summary>
        /// Đã xuất viện (thanh toán xuất viện）
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// Chuyển khu
        /// </summary>
        Zq = 7,

        /// <summary>
        /// Hủy bỏGhi chép/Hủy nhập viện
        /// </summary>
        Wry = 9,
    }
    public enum EnumSex
    {
        [Description("未知的性别")]
        Unknown = 0,
        [Description("男")]
        M = 1,
        [Description("女")]
        F = 2,
        [Description("未说明的性别")]
        Empty = 9
    }
    /// <summary>
    /// Trạng thái bệnh án
    /// </summary>
    public enum Enumbazt
    {
        [Description("待录入")]
        dlr = 1,
        [Description("已录入")]
        lrz = 2,
        [Description("已归档")]
        gd = 3
    }

    /// <summary>
    /// Bệnh ánTrạng thái dùng chung với EMR
    /// </summary>
    public enum EnumRecordStu
    {

        [Description("已提交")]
        ytj = 1,
        [Description("退回")]
        th = 2,
        [Description("已签收")]
        yqs = 3,
        [Description("未提交")]
        wtj = 4,
        //[Description("Bệnh ánLưu trữ")]
        //bagd = 9
    }
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
        [Description("不详")]
        BX = 5,
        [Description("未查")]
        WC = 6
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

    //public enum EnumRytj
    //{
    //    [Description("Cấp cứu")]
    //    JZ = 1,
    //    [Description("Ngoại trú")]
    //    MZ = 2,
    //    [Description("Chuyển đến từ cơ sở y tế khác")]
    //    QTOrg = 3,
    //    [Description("Khác")]
    //    QT = 9
    //}
    /// <summary>
    /// Quan hệ
    /// </summary>
    public enum EnumGx {
        [Description("本人或户主")]
        br = 1,
        [Description("配偶")]
        fq = 2,
        [Description("子")]
        z = 3,
        [Description("女")]
        n ,       
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
    /// Có hay không
    /// </summary>
    public enum EnumYorN
    {
        [Description("否")]
        N=1,
        [Description("是")]
        Y
    }
    /// <summary>
    /// Có hay không
    /// </summary>
    public enum EnumHorN
    {
        [Description("无")]
        Non = 1,
        [Description("有")]
        Has
    }
    /// <summary>
    ///Tình trạng bệnh khi nhập viện 1	Có 2	Chưa xác định lâm sàng 3	Tình hình không rõ 4	Không
    /// </summary>
    public enum EnumRybq {
        [Description("有")]
        y=1,
        [Description("临床未确定")]
        wqd,
        [Description("情况不明")]
        bm,
        [Description("无")]
        w
    }

    /// <summary>
    /// Tình hình xuất viện
    /// 1	Khỏi bệnh 2	Đỡ hơn 3	Chưa khỏi4	Tử vong5	Khác
    /// </summary>
    public enum EnumCyqk
    {
        [Description("治愈")]
        zy=1,
        [Description("好转")]
        hz,
        [Description("未愈")]
        wy,
        [Description("死亡")]
        sw,
        [Description("其他")]
        qt
    }
    /// <summary>
    /// Loại chẩn đoán
    /// </summary>
    public enum EnumZdlx
    {
        [Description("主要诊断")]
        zy=1,
        [Description("次要诊断")]
        cy
    }


    /// <summary>
    /// Đường nhập viện
    /// </summary>
    public enum EnumRYTJ
    {
        /// <summary>
        /// Ngoại trú
        /// </summary>
        [Description("门诊")]
        mz = 1,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("急诊")]
        jz,
        /// <summary>
        /// Chuyển đến từ cơ sở y tế khác
        /// </summary>
        [Description("其他医疗机构转入")]
        qtyljg,
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
    /// Loại bệnh án
    /// </summary>
    public enum EnumBllx
    {
        /// <summary>
        /// Bệnh án nằm viện
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
        /// Trang đầu bệnh án
        /// </summary>
        /// 
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
    public enum EnumZdlx2
    {
        [Description("西医诊断")]
        WM = 1,
        [Description("中医诊断")]
        TCM = 2

    }
    /// <summary>
    /// Chất lượng bệnh án
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
    /// Hình thức rời viện
    /// </summary>
    public enum EnumLyfs
    {
        [Description("医嘱离院")]
        yzly = 1,
        /// <summary>
        /// Cấp cứu
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
    /// Phân loại tình trạng bệnh
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
}