using System.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.MR.ManageSystem.Infrastructure
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
    public enum EnumSex
    {
        [Description("Giới tính chưa rõ")]
        Unknown = 0,
        [Description("Nam")]
        M = 1,
        [Description("Nữ")]
        F = 2,
        [Description("Giới tính chưa rõ")]
        Empty = 9
    }
    /// <summary>
    /// 病案状态
    /// </summary>
    public enum Enumbazt
    {
        [Description("Chờ nhập")]
        dlr = 1,
        [Description("Đã nhập")]
        lrz = 2,
        [Description("Đã lưu trữ")]
        gd = 3
    }

    /// <summary>
    /// 病历状态 与EMR通用
    /// </summary>
    public enum EnumRecordStu
    {

        [Description("Đã gửi")]
        ytj = 1,
        [Description("Trả lại")]
        th = 2,
        [Description("Đã ký nhận")]
        yqs = 3,
        [Description("Chưa gửi")]
        wtj = 4,
        //[Description("病案归档")]
        //bagd = 9
    }
    /// <summary>
    /// 血型
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
        [Description("Chưa kiểm tra")]
        WC = 6
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
        [Description("Chưa kiểm tra")]
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
    /// 关系
    /// </summary>
    public enum EnumGx {
        [Description("Bản thân hoặc chủ hộ")]
        br = 1,
        [Description("Vợ/chồng")]
        fq = 2,
        [Description("Con trai")]
        z = 3,
        [Description("Con gái")]
        n ,       
        [Description("Cháu nội, cháu ngoại")]
        sun,
        [Description("Cha mẹ")]
        fm,
        [Description("Ông bà nội/ngoại")]
        zfm,
        [Description("Anh/chị/em")]
        xdjm,
        [Description("Khác")]
        qt
    }
    /// <summary>
    /// 是否有无
    /// </summary>
    public enum EnumYorN
    {
        [Description("Không")]
        N=1,
        [Description("Có")]
        Y
    }
    /// <summary>
    /// 是否有无
    /// </summary>
    public enum EnumHorN
    {
        [Description("Không có")]
        Non = 1,
        [Description("Có")]
        Has
    }
    /// <summary>
    ///入院病情 1	有 2	Lâm sàng chưa xác định 3	Tình trạng chưa rõ 4	无
    /// </summary>
    public enum EnumRybq {
        [Description("Có")]
        y=1,
        [Description("Lâm sàng chưa xác định")]
        wqd,
        [Description("Tình trạng chưa rõ")]
        bm,
        [Description("Không có")]
        w
    }

    /// <summary>
    /// 出院情况
    /// 1	Khỏi bệnh 2	Đỡ hơn 3	Chưa khỏi4	Tử vong5	Khác
    /// </summary>
    public enum EnumCyqk
    {
        [Description("Khỏi bệnh")]
        zy=1,
        [Description("Đỡ hơn")]
        hz,
        [Description("Chưa khỏi")]
        wy,
        [Description("Tử vong")]
        sw,
        [Description("Khác")]
        qt
    }
    /// <summary>
    /// 诊断类型
    /// </summary>
    public enum EnumZdlx
    {
        [Description("Chẩn đoán chính")]
        zy=1,
        [Description("Chẩn đoán phụ")]
        cy
    }


    /// <summary>
    /// 入院途径
    /// </summary>
    public enum EnumRYTJ
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
        jz,
        /// <summary>
        /// Chuyển đến từ cơ sở y tế khác
        /// </summary>
        [Description("Chuyển đến từ cơ sở y tế khác")]
        qtyljg,
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
    /// 病历类型
    /// </summary>
    public enum EnumBllx
    {
        /// <summary>
        /// Bệnh án nội trú
        /// </summary>
        [Description("Bệnh án nội trú")]
        zybl = 1,

        /// <summary>
        /// Hồ sơ diễn biến bệnh
        /// </summary>
        [Description("Hồ sơ diễn biến bệnh")]
        bcjl = 2,

        /// <summary>
        /// Văn bản y tế
        /// </summary>
        [Description("Văn bản y tế")]
        ylws = 3,

        /// <summary>
        /// Hồ sơ điều dưỡng
        /// </summary>
        [Description("Hồ sơ điều dưỡng")]
        hljl = 4,

        /// <summary>
        /// Trang đầu bệnh án
        /// </summary>
        /// 
        [Description("Trang đầu bệnh án")]
        basy = 5,

        /// <summary>
        /// Đánh giá phục hồi
        /// </summary>
        [Description("Đánh giá phục hồi")]
        kfpg = 6
    }
    /// <summary>
    /// 诊断类型
    /// </summary>
    public enum EnumZdlx2
    {
        [Description("Chẩn đoán tây y")]
        WM = 1,
        [Description("Chẩn đoán y học cổ truyền")]
        TCM = 2

    }
    /// <summary>
    /// 病案质量
    /// </summary>
    public enum EnumBazl
    {
        [Description("A")]
        I = 1,
        [Description("B")]
        II,
        [Description("C")]
        III
    }
    /// <summary>
    /// 离院方式
    /// </summary>
    public enum EnumLyfs
    {
        [Description("Rời viện theo y lệnh")]
        yzly = 1,
        /// <summary>
        /// Cấp cứu
        /// </summary>
        [Description("Chuyển viện theo y lệnh")]
        yzzy,
        [Description("Chuyển đến cơ sở y tế cộng đồng/trạm y tế xã theo y lệnh")]
        yzzsq,
        [Description("Rời viện không theo y lệnh")]
        fyz,
        [Description("Tử vong")]
        sw,
        [Description("Khác")]
        other = 9,
    }
    /// <summary>
    /// 病情分型
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
}
