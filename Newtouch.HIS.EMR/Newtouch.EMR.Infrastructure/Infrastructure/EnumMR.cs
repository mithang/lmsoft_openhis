using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Infrastructure.EnumMR
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
		/// Ra khỏi khu bệnh（Ra khỏi khu bệnh）（Chờ quyết toán）
		/// </summary>
		Djz = 2,
		/// <summary>
		/// Đã ra viện（Quyết toán ra viện）
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
	public enum EnumMRSex
	{
		[Description("Nam")]
		M = 1,
		[Description("Nữ")]
		F = 2
	}
	/// <summary>
	/// Trạng thái bệnh án
	/// </summary>
	public enum Enumbazt
	{
		[Description("Chờ nhập")]
		dlr = 1,
		[Description("Đã nhập")]
		lrz = 2,
		//[Description("Đã lưu trữ")]
		//gd = 3
	}

	///// <summary>
	///// Trạng thái bệnh án vàEMRChung
	///// </summary>
	//public enum EnumRecordStu
	//{
	//    [Description("Chưa nộp")]
	//    wtj = 0,
	//    [Description("Đã nộp")]
	//    ytj = 1,
	//    [Description("Trả lại")]
	//    th = 2,
	//    [Description("Đã ký nhận")]
	//    yqs = 3,
	//    //[Description("Lưu trữ bệnh án")]
	//    //bagd = 4
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
		[Description("Không rõ")]
		BX = 5,
		[Description("Chưa kiểm tra")]
		WC = 6
	}

	/// <summary>
	/// Khám nghiệm tử thi bệnh nhân tử vong
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
	//    [Description("Cấp cứu")]
	//    JZ = 1,
	//    [Description("Ngoại trú")]
	//    MZ = 2,
	//    [Description("Chuyển từ cơ sở y tế khác")]
	//    QTOrg = 3,
	//    [Description("Khác")]
	//    QT = 9
	//}
	/// <summary>
	/// Quan hệ
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
	/// Có hoặc không
	/// </summary>
	public enum EnumYorN
	{
		[Description("Có")]
		Y = 1,
		[Description("Không")]
		N = 2
	}


	/// <summary>
	/// Có hoặc không
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
	/// Không hoặc là
	/// </summary>
	public enum EnumNorY
	{
		[Description("Không")]
		N = 1,
		[Description("Có")]
		Y = 2
	}
	/// <summary>
	/// Có hoặc không
	/// </summary>
	public enum EnumHorN
	{
		[Description("Không có")]
		Non = 1,
		[Description("Có")]
		Has = 2
	}
	/// <summary>
	/// Phản ứng truyền máu
	/// 1.Có  2.Không  3.Chưa truyền
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
	///Tình trạng bệnh lúc nhập viện 1	Có 2	Lâm sàng chưa xác định 3	Chưa rõ tình hình 4	Không có
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
	/// Tình hình ra viện
	/// 1	Khỏi bệnh 2	Đỡ hơn 3	Chưa khỏi4	Tử vong5	Khác
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
	/// Loại chẩn đoán-Chính/phụ
	/// </summary>
	public enum EnumZdlxbs
	{
        [Description("Chẩn đoán chính")]
		zy = 1,
		[Description("Chẩn đoán phụ")]
		cy = 2
        
    }
    /// <summary>
	/// Loại chẩn đoán-Chính/phụ
	/// </summary>
	public enum EnumZyZdlxbs
    {
        [Description("Bệnh chính")]
        zy = 1,
        [Description("Chứng chính")]
        cy = 2

    }

    /// <summary>
    /// Đường nhập viện
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
		jz = 2,
		/// <summary>
		/// Chuyển từ cơ sở y tế khác
		/// </summary>
		[Description("Chuyển từ cơ sở y tế khác")]
		qtyljg = 3,
		/// <summary>
		/// 120
		/// </summary>
		//[Description("120")]
		//jj120,
		/// <summary>
		/// Khác
		/// </summary>
		[Description("Khác")]
		Else = 9,
	}
	///// <summary>
	///// Loại bệnh án
	///// </summary>
	//public enum EnumBllx
	//{
	//    /// <summary>
	//    /// Bệnh án nội trú
	//    /// </summary>
	//    [Description("Bệnh án nội trú")]
	//    zybl = 1,

	//    /// <summary>
	//    /// Ghi chép diễn biến bệnh
	//    /// </summary>
	//    [Description("Ghi chép diễn biến bệnh")]
	//    bcjl = 2,

	//    /// <summary>
	//    /// Văn bản y tế
	//    /// </summary>
	//    [Description("Văn bản y tế")]
	//    ylws = 3,

	//    /// <summary>
	//    /// Ghi chép điều dưỡng
	//    /// </summary>
	//    [Description("Ghi chép điều dưỡng")]
	//    hljl = 4,

	//    /// <summary>
	//    /// Trang đầu bệnh án
	//    /// </summary>
	//    /// 
	//    [Description("Trang đầu bệnh án")]
	//    basy = 5
	//}
	/// <summary>
	/// Loại chẩn đoán
	/// </summary>
	//public enum EnumZdlx2
	//{
	//    [Description("Chẩn đoán Tây y")]
	//    WM = 1,
	//    [Description("Chẩn đoán Đông y")]
	//    TCM = 2

	//}
	/// <summary>
	/// Chất lượng hồ sơ bệnh án
	/// </summary>
	public enum EnumBazl
	{
		[Description("Hạng A")]
		I = 1,
		[Description("Hạng B")]
		II,
		[Description("Hạng C")]
		III
	}
	/// <summary>
	/// Cách rời viện
	/// </summary>
	public enum EnumLyfs
	{
		[Description("Ra viện theo y lệnh")]
		yzly = 1,
		/// <summary>
		/// Cấp cứu
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
	/// Phân loại tình trạng bệnh
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
	/// Nguyên nhân biến đổi lộ trình lâm sàng
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
	/// Tình hình kiểm tra
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
	/// Tình hình kiểm tra
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
	///Tình hình sử dụng kháng sinh 
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
	///Phản ứng truyền dịch 0 Chưa truyền 2 Không có 3 Có
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
	///Phản ứng truyền máu 0 Chưa truyền 2 Không có 3 Có
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
	///Mức độ tổn thương do ngã khi nội trú 0 Chưa truyền 2 Không có 3 Có
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
	///Nguyên nhân ngã hoặc rơi khỏi giường
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
	///Giai đoạn loét do tì đè
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
	///Áp dụng đường đi lâm sàng 1 Đông y 2 Tây y 3 Không
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
