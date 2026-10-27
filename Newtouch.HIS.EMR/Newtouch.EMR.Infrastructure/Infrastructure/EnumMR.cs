using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure.EnumMR
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
	public enum EnumMRSex
	{
		[Description("Nam")]
		M = 1,
		[Description("Nữ")]
		F = 2
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
		//[Description("已归档")]
		//gd = 3
	}

	///// <summary>
	///// 病历状态 与EMR通用
	///// </summary>
	//public enum EnumRecordStu
	//{
	//    [Description("未提交")]
	//    wtj = 0,
	//    [Description("已提交")]
	//    ytj = 1,
	//    [Description("退回")]
	//    th = 2,
	//    [Description("已签收")]
	//    yqs = 3,
	//    //[Description("病案归档")]
	//    //bagd = 4
	//}
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
	/// 死亡患者尸检
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
		[Description("Chưa kiểm tra")]
		WC = 4
	}

	//public enum EnumRytj
	//{
	//    [Description("急诊")]
	//    JZ = 1,
	//    [Description("门诊")]
	//    MZ = 2,
	//    [Description("其他医疗机构转入")]
	//    QTOrg = 3,
	//    [Description("其他")]
	//    QT = 9
	//}
	/// <summary>
	/// 关系
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
		[Description("Cháu nội/cháu ngoại")]
		sun,
		[Description("Cha mẹ")]
		fm,
		[Description("Ông bà nội/ngoại")]
		zfm,
		[Description("Anh em ruột")]
		xdjm,
		[Description("Khác")]
		qt
	}
	/// <summary>
	/// 是或否
	/// </summary>
	public enum EnumYorN
	{
		[Description("Có")]
		Y = 1,
		[Description("Không")]
		N = 2
	}


	/// <summary>
	/// 是或否
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
		[Description("Chuyển từ cơ sở y tế khác")]
		qkbm = 3,
		[Description("Khác")]
		w = 4
	}
	/// <summary>
	/// 否或是
	/// </summary>
	public enum EnumNorY
	{
		[Description("Không")]
		N = 1,
		[Description("Có")]
		Y = 2
	}
	/// <summary>
	/// 有或无
	/// </summary>
	public enum EnumHorN
	{
		[Description("Không có")]
		Non = 1,
		[Description("Có")]
		Has = 2
	}
	/// <summary>
	/// 输血反应
	/// 1.是  2.否  3.未输
	/// </summary>
	public enum EumSYFY
	{
		[Description("Có")]
		y = 1,
		[Description("Không")]
		n = 2,
		[Description("Chưa truyền")]
		Non = 3
	}
	/// <summary>
	///入院病情 1	有 2	临床未确定 3	情况不明 4	无
	/// </summary>
	public enum EnumRybq
	{
		[Description("Có")]
		y = 1,
		[Description("Lâm sàng chưa xác định")]
		lcwqd = 2,
		[Description("Không rõ tình hình")]
		qkbm = 3,
		[Description("Không có")]
		w = 4
	}

	/// <summary>
	/// 出院情况
	/// 1	治愈 2	好转 3	未愈4	死亡5	其他
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
	/// 诊断类型-主从
	/// </summary>
	public enum EnumZdlxbs
	{
        [Description("Chẩn đoán chính")]
		zy = 1,
		[Description("Chẩn đoán phụ")]
		cy = 2
        
    }
    /// <summary>
	/// 诊断类型-主从
	/// </summary>
	public enum EnumZyZdlxbs
    {
        [Description("Bệnh chính")]
        zy = 1,
        [Description("Chứng chính")]
        cy = 2

    }

    /// <summary>
    /// 入院途径
    /// </summary>
    public enum EnumRYTJ
	{
		/// <summary>
		/// 门诊
		/// </summary>
		[Description("Ngoại trú")]
		mz = 1,
		/// <summary>
		/// 急诊
		/// </summary>
		[Description("Cấp cứu")]
		jz = 2,
		/// <summary>
		/// 其他医疗机构转入
		/// </summary>
		[Description("Chuyển từ cơ sở y tế khác")]
		qtyljg = 3,
		/// <summary>
		/// 120
		/// </summary>
		//[Description("120")]
		//jj120,
		/// <summary>
		/// 其他
		/// </summary>
		[Description("Khác")]
		Else = 9,
	}
	///// <summary>
	///// 病历类型
	///// </summary>
	//public enum EnumBllx
	//{
	//    /// <summary>
	//    /// 住院病历
	//    /// </summary>
	//    [Description("住院病历")]
	//    zybl = 1,

	//    /// <summary>
	//    /// 病程记录
	//    /// </summary>
	//    [Description("病程记录")]
	//    bcjl = 2,

	//    /// <summary>
	//    /// 医疗文书
	//    /// </summary>
	//    [Description("医疗文书")]
	//    ylws = 3,

	//    /// <summary>
	//    /// 护理记录
	//    /// </summary>
	//    [Description("护理记录")]
	//    hljl = 4,

	//    /// <summary>
	//    /// 病案首页
	//    /// </summary>
	//    /// 
	//    [Description("病案首页")]
	//    basy = 5
	//}
	/// <summary>
	/// 诊断类型
	/// </summary>
	//public enum EnumZdlx2
	//{
	//    [Description("西医诊断")]
	//    WM = 1,
	//    [Description("中医诊断")]
	//    TCM = 2

	//}
	/// <summary>
	/// 病案质量
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
	/// 离院方式
	/// </summary>
	public enum EnumLyfs
	{
		[Description("Ra viện theo y lệnh")]
		yzly = 1,
		/// <summary>
		/// 急诊
		/// </summary>
		[Description("Chuyển viện theo y lệnh")]
		yzzy,
		[Description("Chuyển cơ sở y tế cộng đồng/trạm y tế xã theo y lệnh")]
		yzzsq,
		[Description("Ra viện không theo y lệnh")]
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
		[Description("Bình thường")]
		yb
	}
	/// <summary>
	/// 临床路径变异原因
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
	/// 检查情况
	/// </summary>
	public enum EnumJBQK
	{
		[Description("CT")]
		CT = 1,
		[Description("PETCT")]
		PETCT = 2,
		[Description("Hai nguồn")]
		SY = 3,
		[Description("Siêu âm B")]
		Bc = 4,
		[Description("Phim X-quang")]
		Xp = 5,
		[Description("Siêu âm tim")]
		CSXDT = 6,
		[Description("MRI")]
		MRI = 7
	}
	/// <summary>
	/// 检查情况
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
	///抗菌药物使用情况 
	/// </summary>
	public enum EnumKJYWSYQK
	{
		[Description("Loại Ⅰ")]
		first = 1,
		[Description("Liên Ⅱ")]
		second = 2,
		[Description("Liên Ⅲ")]
		third = 3,
		[Description("Liên Ⅳ")]
		forth = 4,
		[Description("Trên liên Ⅳ")]
		more = 5
	}

	/// <summary>
	///输液反映 0未输 2无 3有
	/// </summary>
	public enum EnumSYFY
	{
		[Description("Chưa truyền")]
		first = 0,
		[Description("Không có")]
		second = 1,
		[Description("Có")]
		third = 2
	}

	/// <summary>
	///输血反映 0未输 2无 3有
	/// </summary>
	public enum EnumSXFY
	{
		[Description("Có")]
		Y = 1,
		[Description("Không")]
		N = 2,
		[Description("Chưa truyền")]
		none = 3
	}

	/// <summary>
	///住院跌倒伤害程度 0未输 2无 3有
	/// </summary>
	public enum EnumZYDD_SHCD
	{
		[Description("Cấp 1")]
		first = 1,
		[Description("Cấp 2")]
		second = 2,
		[Description("Cấp 3")]
		third = 3,
		[Description("Chưa gây tổn hại")]
		none = 4
	}

	/// <summary>
	///跌倒或坠床的原因
	/// </summary>
	public enum EnumZYDD_YY
	{
		[Description("Nguyên nhân sức khỏe")]
		jkyy = 1,
		[Description("Nguyên nhân điều trị, thuốc, gây mê")]
		zlywmzyy = 2,
		[Description("Yếu tố môi trường")]
		hjys = 3,
		[Description("Nguyên nhân khác")]
		qtyy = 4
	}

	/// <summary>
	///压疮分期
	/// </summary>
	public enum EnumYCFQ
	{
		[Description("Giai đoạn 1")]
		first = 1,
		[Description("Giai đoạn 2")]
		second = 2,
		[Description("Giai đoạn 3")]
		third = 3,
		[Description("Giai đoạn 4")]
		forth = 4
	}
    /// <summary>
	///实施临床路径 1中医 2西医 3否
	/// </summary>
	public enum EnumZYSSLCLJ
    {
        [Description("Y học cổ truyền")]
        TCM = 1,
        [Description("Y học hiện đại")]
        WM = 2,
        [Description("Không")]
        none = 3
    }

}
