using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 在院标志
    /// </summary>
    public enum EnumZYBZ
    {
        /// <summary>
        /// 入院登记
        /// </summary>
        Xry = 0,
        /// <summary>
        /// 病区中
        /// </summary>
        Bqz = 1,
        /// <summary>
        /// 病区出院（出病区）（待结账）
        /// </summary>
        Djz = 2,
        /// <summary>
        /// 已出院（出院结算）
        /// </summary>
        Ycy = 3,
        /// <summary>
        /// 转区
        /// </summary>
        Zq = 7,

        /// <summary>
        /// 作废记录/取消入院
        /// </summary>
        Wry = 9,
    }

    /// <summary>
    /// 医嘱类型
    /// </summary>
    public enum EnumYzlx
    {
        ///// <summary>
        ///// 通用
        ///// </summary>
        //[Description("通用")]
        //Ty = 1,
        /// <summary>
        /// 药品
        /// </summary>
        [Description("Thuốc")]
        Yp = 2,
        /// <summary>
        /// 文字
        /// </summary>
        [Description("Văn bản")]
        Wz = 3,
        /// <summary>
        /// 出院带药
        /// </summary>
        [Description("Thuốc mang về khi ra viện")]
        Cydy = 4,
        /// <summary>
        /// 项目
        /// </summary>
        [Description("Hạng mục")]
        sfxm = 5,
        /// <summary>
        /// 项目
        /// </summary>
        [Description("Xét nghiệm")]
        jy = 6,
        /// <summary>
        /// 项目
        /// </summary>
        [Description("Kiểm tra")]
        jc = 7,
        /// <summary>
        /// 项目
        /// </summary>
        [Description("Y lệnh ăn uống")]
        ssyz = 8,
        /// <summary>
        /// 手术
        /// </summary>
        [Description("Phẫu thuật")]
        oper = 9,
        /// <summary>
        /// 中草药
        /// </summary>
        [Description("Thuốc thảo dược Trung Quốc")]
        zcy = 10,
        /// <summary>
        /// 康复
        /// </summary>
        [Description("Phục hồi chức năng")]
        rehab = 11
    }
    /// <summary>
    /// 医嘱状态
    /// </summary>
    public enum EnumYzzt
    {
        /// <summary>
        /// 待审 
        /// </summary>
        [Description("Chờ duyệt")]
        Ds = 0,
        /// <summary>
        /// 审核
        /// </summary>
        [Description("Duyệt")]
        Sh = 1,
        /// <summary>
        /// 执行
        /// </summary>
        [Description("Thực hiện")]
        Zx = 2,
        /// <summary>
        /// 撤DC
        /// </summary>
        [Description("DC")]
        DC = 3,
        /// <summary>
        /// 停止或作废
        /// </summary>
        [Description("Dừng")]
        TZ = 4
    }
    /// <summary>
    /// 药品单位类别
    /// </summary>
    public enum EnumYPdwlb
    {
        /// <summary>
        /// 剂量单位
        /// </summary>
        Jldw = 1,
        /// <summary>
        /// 药库单位
        /// </summary>
        Ykdw = 2,
        /// <summary>
        /// 门诊单位
        /// </summary>
        Mzdw = 3,
        /// <summary>
        /// 住院单位
        /// </summary>
        Zydw = 4,
        /// <summary>
        /// 进货单位
        /// </summary>
        Jhdw = 5,
        /// <summary>
        /// 最小单位
        /// </summary>
        Zxdw = 6
    }
    /// <summary>
    /// 套餐范围
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
    /// 护理病人状态（护理生命体征录入用）
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
    /// 执行周期单位
    /// </summary>
    public enum EnumZxzqdw
    {
        /// <summary>
        /// 天
        /// </summary>
        Day = 1,
        /// <summary>
        /// 小时
        /// </summary>
        Hour = 2,
        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 3,
        /// <summary>
        /// 不规则周期
        /// </summary>
        Unruly = 4
    }
    /// <summary>
    /// 发药退药标志
    /// </summary>
    public enum EnumMedSTflag
    {
        [Description("Phát thuốc")]
        Receive = 1,
        [Description("Trả thuốc")]
        Return = 2
    }
    /// <summary>
    /// 医嘱性质
    /// </summary>
    public enum EnumYzxz
    {
        [Description("Tạm thời")]
        Ls = 1,
        [Description("Dài hạn")]
        Cq = 2
    }
    /// <summary>
    /// 床位记录状态
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
    /// 医生类型
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
    /// 护理级别
    /// </summary> 
    public enum EnumHljb
    {
        /// <summary>
        /// I级
        /// </summary>
        [Description("Cấp I")]
        I = 1,
        /// <summary>
        /// II级
        /// </summary>
        [Description("Cấp II")]
        II = 2,
        /// <summary>
        /// III级
        /// </summary>
        [Description("Cấp III")]
        III = 3,
        /// <summary>
        /// 特级
        /// </summary>
        [Description("Cấp đặc biệt")]
        T = 4
    }
    /// <summary>
    /// 结算状态
    /// </summary>
    public enum EnumJszt
    {
        [Description("Đã thanh toán")]
        Yjs = 1,
        [Description("Chưa thanh toán")]
        Wjs = 2
    }
    /// <summary>
    /// 固定执行标准
    /// </summary>
    public enum EnumGdzxbz
    {
        /// <summary>
        /// 固定
        /// </summary>
        Fixed = 1,
        /// <summary>
        /// 不固定
        /// </summary>
        Nonfixed = 0
    }
    /// <summary>
    /// 危重级别
    /// </summary>
    public enum EnumWzjb
    {
        /// <summary>
        /// 一般
        /// </summary>
        [Description("Bình thường")]
        Normal = 1,
        /// <summary>
        /// 病重
        /// </summary>
        [Description("Bệnh nặng")]
        Serious = 2,
        /// <summary>
        /// 病危
        /// </summary>
        [Description("Nguy kịch")]
        Bedying = 3
    }
    //EnumDB已包含
    //public enum EnumZdlx
    //{
    //    MainDiag=1,
    //    SedDiag=2
    //}

    /// <summary>
    /// 体温测量说明
    /// </summary>
    public enum EnumTwclfs
    {
        /// <summary>
        /// 口温
        /// </summary>
        [Description("Nhiệt độ miệng")]
        kw = 1,
        /// <summary>
        /// 腋温
        /// </summary>
        [Description("Nhiệt độ nách")]
        yw = 2,
        /// <summary>
        /// 肛温
        /// </summary>
        [Description("Nhiệt độ hậu môn")]
        gw = 1,
    }



    /// <summary>
    /// 病历类型
    /// </summary>
    public enum EnumBllx
    {
        /// <summary>
        /// 住院病历
        /// </summary>
        [Description("Bệnh án nội trú")]
        zybl = 1,

        /// <summary>
        /// 病程记录
        /// </summary>
        [Description("Ghi chép quá trình bệnh")]
        bcjl = 2,

        /// <summary>
        /// 医疗文书
        /// </summary>
        [Description("Văn bản y tế")]
        ylws = 3,

        /// <summary>
        /// 护理记录
        /// </summary>
        [Description("Ghi chép điều dưỡng")]
        hljl = 4,

        /// <summary>
        /// 病案首页
        /// </summary>
        [Description("Trang đầu bệnh án")]
        basy = 5,

        /// <summary>
        /// 康复评估
        /// </summary>
        [Description("Đánh giá phục hồi chức năng")]
        kfpg = 6
    }
    /// <summary>
    /// 诊断类型
    /// </summary>
    public enum EnumZdlx {
        [Description("Chẩn đoán Tây y")]
        WM = 1,
        [Description("Chẩn đoán Đông y")]
        TCM =2

    }

}
