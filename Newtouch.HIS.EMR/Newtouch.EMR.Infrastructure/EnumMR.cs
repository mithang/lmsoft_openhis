using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure.EnumMR
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
	public enum EnumMRSex
	{
		[Description("男")]
		M = 1,
		[Description("女")]
		F = 2
	}
	/// <summary>
	/// Trạng thái hồ sơ bệnh án
	/// </summary>
	public enum Enumbazt
	{
		[Description("待录入")]
		dlr = 1,
		[Description("已录入")]
		lrz = 2,
		//[Description("Đã lưu trữ")]
		//gd = 3
	}

	///// <summary>
	///// Trạng thái bệnh án - dùng chung với EMR
	///// </summary>
	//public enum EnumRecordStu
	//{
	// [Description("Chưa gửi")]
	//    wtj = 0,
	// [Description("Đã gửi")]
	//    ytj = 1,
	// [Description("Trả lại")]
	//    th = 2,
	// [Description("Đã ký nhận")]
	//    yqs = 3,
	//    //[Description("Lưu trữ hồ sơ bệnh án")]
	// bagd = 4
	//}
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
	/// Khám nghiệm tử thi bệnh nhân tử vong
	/// </summary>
	public enum EnumSwhzsj
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2
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
	// [Description("Cấp cứu")]
	//    JZ = 1,
	// [Description("Ngoại trú")]
	//    MZ = 2,
	// [Description("Chuyển đến từ cơ sở y tế khác")]
	//    QTOrg = 3,
	// [Description("Khác")]
	//    QT = 9
	//}
	/// <summary>
	/// Quan hệ
	/// </summary>
	public enum EnumGx
	{
		[Description("本人或户主")]
		br = 1,
		[Description("配偶")]
		fq = 2,
		[Description("子")]
		z = 3,
		[Description("女")]
		n,
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
	/// Có hoặc không
	/// </summary>
	public enum EnumYorN
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2
	}


	/// <summary>
	/// Có hoặc không
	/// </summary>
	public enum EnumNewYorN
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2
	}

	public enum EnumYork
	{
		[Description("急诊")]
		y = 1,
		[Description("门诊")]
		lcwqd = 2,
		[Description("其他医疗机构转入")]
		qkbm = 3,
		[Description("其他")]
		w = 4
	}
	/// <summary>
	/// Không hoặc có
	/// </summary>
	public enum EnumNorY
	{
		[Description("否")]
		N = 1,
		[Description("是")]
		Y = 2
	}
	/// <summary>
	/// Có hoặc không
	/// </summary>
	public enum EnumHorN
	{
		[Description("无")]
		Non = 1,
		[Description("有")]
		Has = 2
	}
	/// <summary>
	/// Phản ứng truyền máu
	/// 1. Có, 2. Không, 3. Chưa truyền
	/// </summary>
	public enum EumSYFY
	{
		[Description("是")]
		y = 1,
		[Description("否")]
		n = 2,
		[Description("未输")]
		Non = 3
	}
	/// <summary>
	///Tình trạng bệnh lúc vào viện: 1 Có, 2 Lâm sàng chưa xác định, 3 Không rõ tình hình, 4 Không
	/// </summary>
	public enum EnumRybq
	{
		[Description("有")]
		y = 1,
		[Description("临床未确定")]
		lcwqd = 2,
		[Description("情况不明")]
		qkbm = 3,
		[Description("无")]
		w = 4
	}

	/// <summary>
	/// Tình hình ra viện
	/// 1 Khỏi, 2 Đỡ, 3 Chưa khỏi, 4 Tử vong, 5 Khác
	/// </summary>
	public enum EnumCyqk
	{
		[Description("治愈")]
		zy = 1,
		[Description("好转")]
		hz = 2,
		[Description("未愈")]
		wy = 3,
		[Description("死亡")]
		sw = 4,
		[Description("其他")]
		qt = 5
	}
	/// <summary>
	/// Loại chẩn đoán - chính/phụ
	/// </summary>
	public enum EnumZdlxbs
	{
        [Description("主要诊断")]
		zy = 1,
		[Description("次要诊断")]
		cy = 2
        
    }
    /// <summary>
	/// Loại chẩn đoán - chính/phụ
	/// </summary>
	public enum EnumZyZdlxbs
    {
        [Description("主病")]
        zy = 1,
        [Description("主证")]
        cy = 2

    }

    /// <summary>
    /// Đường vào viện
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
		jz = 2,
		/// <summary>
		/// Chuyển đến từ cơ sở y tế khác
		/// </summary>
		[Description("其他医疗机构转入")]
		qtyljg = 3,
		/// <summary>
		/// 120
		/// </summary>
		//[Description("120")]
		//jj120,
		/// <summary>
		/// Khác
		/// </summary>
		[Description("其他")]
		Else = 9,
	}
	///// <summary>
	///// Loại bệnh án
	///// </summary>
	//public enum EnumBllx
	//{
	// <summary>
	//    /// Bệnh án nội trú
	// </summary>
	// [Description("Bệnh án nội trú")]
	//    zybl = 1,

	// <summary>
	//    /// Ghi chép diễn biến bệnh
	// </summary>
	// [Description("Ghi chép diễn biến bệnh")]
	//    bcjl = 2,

	// <summary>
	//    /// Văn bản y tế
	// </summary>
	// [Description("Văn bản y tế")]
	//    ylws = 3,

	// <summary>
	//    /// Ghi chép điều dưỡng
	// </summary>
	// [Description("Ghi chép điều dưỡng")]
	//    hljl = 4,

	// <summary>
	//    /// Trang đầu hồ sơ bệnh án
	// </summary>
	// 
	// [Description("Trang đầu hồ sơ bệnh án")]
	//    basy = 5
	//}
	/// <summary>
	/// Loại chẩn đoán
	/// </summary>
	//public enum EnumZdlx2
	//{
	// [Description("Chẩn đoán Tây y")]
	//    WM = 1,
	// [Description("Chẩn đoán Đông y")]
	//    TCM = 2

	//}
	/// <summary>
	/// Chất lượng hồ sơ bệnh án
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
	/// <summary>
	/// Nguyên nhân biến thể của lộ trình lâm sàng
	/// </summary>
	public enum EnumBYYY
	{
		[Description("正变异")]
		zby = 1,
		[Description("负变异")]
		fby,
		[Description("系统原因")]
		xtyy
	}
	/// <summary>
	/// Tình hình kiểm tra
	/// </summary>
	public enum EnumJBQK
	{
		[Description("CT")]
		CT = 1,
		[Description("PETCT")]
		PETCT = 2,
		[Description("双源")]
		SY = 3,
		[Description("B超")]
		Bc = 4,
		[Description("X片")]
		Xp = 5,
		[Description("超声心动图")]
		CSXDT = 6,
		[Description("MRI")]
		MRI = 7
	}
	/// <summary>
	/// Tình hình kiểm tra
	/// </summary>
	public enum EnumJCQK
	{
		[Description("阳性")]
		Yang = 1,
		[Description("阴性")]
		Non = 2,
		[Description("未做")]
		Yin = 3,


	}

	/// <summary>
	///Tình hình sử dụng thuốc kháng khuẩn 
	/// </summary>
	public enum EnumKJYWSYQK
	{
		[Description("Ⅰ种")]
		first = 1,
		[Description("Ⅱ联")]
		second = 2,
		[Description("Ⅲ联")]
		third = 3,
		[Description("Ⅳ联")]
		forth = 4,
		[Description(">Ⅳ联")]
		more = 5
	}

	/// <summary>
	///Phản ứng truyền dịch: 0 Chưa truyền, 2 Không, 3 Có
	/// </summary>
	public enum EnumSYFY
	{
		[Description("未输")]
		first = 0,
		[Description("无")]
		second = 1,
		[Description("有")]
		third = 2
	}

	/// <summary>
	///Phản ứng truyền máu: 0 Chưa truyền, 2 Không, 3 Có
	/// </summary>
	public enum EnumSXFY
	{
		[Description("是")]
		Y = 1,
		[Description("否")]
		N = 2,
		[Description("未输")]
		none = 3
	}

	/// <summary>
	///Mức độ tổn thương do ngã khi nằm viện: 0 Chưa nhập, 2 Không, 3 Có
	/// </summary>
	public enum EnumZYDD_SHCD
	{
		[Description("一级")]
		first = 1,
		[Description("二级")]
		second = 2,
		[Description("三级")]
		third = 3,
		[Description("未造成伤害")]
		none = 4
	}

	/// <summary>
	///Nguyên nhân ngã hoặc rơi khỏi giường
	/// </summary>
	public enum EnumZYDD_YY
	{
		[Description("健康原因")]
		jkyy = 1,
		[Description("治疗，药物，麻醉原因")]
		zlywmzyy = 2,
		[Description("环境因素")]
		hjys = 3,
		[Description("其他原因")]
		qtyy = 4
	}

	/// <summary>
	///Phân giai đoạn loét do tì đè
	/// </summary>
	public enum EnumYCFQ
	{
		[Description("1期")]
		first = 1,
		[Description("2期")]
		second = 2,
		[Description("3期")]
		third = 3,
		[Description("4期")]
		forth = 4
	}
    /// <summary>
	///Thực hiện lộ trình lâm sàng: 1 Đông y, 2 Tây y, 3 Không
	/// </summary>
	public enum EnumZYSSLCLJ
    {
        [Description("中医")]
        TCM = 1,
        [Description("西医")]
        WM = 2,
        [Description("否")]
        none = 3
    }

}
