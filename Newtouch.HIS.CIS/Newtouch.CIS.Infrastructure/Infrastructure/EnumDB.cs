using System.ComponentModel;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// ngày chí ghi chép Loại
    /// </summary>
    public enum DbLogType_Ex
    {

    }

    /// <summary>
    /// chứng mục Loại
    /// </summary>
    public enum EnumZjlx
    {
        /// <summary>
        /// CMND/CCCD
        /// </summary>
        [Description("Chứng minh nhân dân")]
        sfz = 1,
        /// <summary>
        /// Hộ chiếu
        /// </summary>
        [Description("Hộ chiếu")]
        hz,
        /// <summary>
        /// Giấy chứng nhận sĩ quan
        /// </summary>
        [Description("Chứng minh sĩ quan")]
        jgz,
        /// <summary>
        /// Khác
        /// </summary>
        [Description("Khác")]
        other = 9
    }

    /// <summary>
    /// Tính chất tự trả(Thuốc,Mục từ điển của Thuộc tính)
    /// </summary>
    public enum EnumZiFuXingZhi
    {
        /// <summary>
        /// Được thanh toán
        /// </summary>
        [Description("Được bảo hiểm")]
        KB = 0,
        /// <summary>
        /// tự chi trả
        /// </summary>
        [Description("Tự chi trả")]
        ZF = 1,
        /// <summary>
        /// Tự trả theo loại
        /// </summary>
        [Description("Tự trả theo loại")]
        FLZF = 2,
        /// <summary>
        /// Tự túc hoàn toàn
        /// </summary>
        [Description("Hoàn toàn tự trả")]
        JueDuiZiLi = 3,
        /// <summary>
        /// Loại A
        /// </summary>
        [Description("Hạng A")]
        Jia,
        /// <summary>
        /// Loại B
        /// </summary>
        [Description("Hạng B")]
        Yi,
        /// <summary>
        /// Loại C
        /// </summary>
        [Description("Hạng C")]
        Bing,
    }

	/// <summary>
	/// Thuốc Loại
	/// </summary>
	public enum EnumYpsx
	{
		[Description("Thuốc gây nghiện, hướng thần (gây mê, hướng thần I, hướng thần II)")]
		Mzyp = 9,
		[Description("Thuốc độc")]
		Dxyp = 4,
		[Description("Chế phẩm máu")]
		Xyzp = 5,
		[Description("Thuốc hormone")]
		Jsyp = 6,
		[Description("Thuốc quý hiếm")]
		Gzyp = 7,
		[Description("Thuốc chống ung thư")]
		Kzlywyp = 8,
	}
	/// <summary>
	/// Kháng sinh cấp loại
	/// </summary>
	public enum EnumKss
	{

		[Description("Thuốc dùng không hạn chế")]
		Fxzsyyw = 0,
		[Description("Thuốc dùng hạn chế")]
		Xzsyyw = 1,
		[Description("Thuốc dùng đặc biệt")]
		Tssyyw = 2,
	}

	/// <summary>
	/// Khám bệnh Trạng thái
	/// </summary>
	public enum EnumJzzt
    {
        /// <summary>
        /// chưa Khám bệnh
        /// </summary>
        [Description("Chưa khám")]
        NotYetTreate = 1,
        /// <summary>
        /// Khám bệnh trong
        /// </summary>
        [Description("Đang khám")]
        Treating = 2,
        /// <summary>
        /// Khám bệnh Kết thúc
        /// </summary>
        [Description("Đã khám xong")]
        Treated = 3,
    }

    /// <summary>
    /// Ngoại trú Loại mjzbz
    /// </summary>
    public enum EnumOutPatientType
    {
        /// <summary>
        /// thông thường Ngoại trú
        /// </summary>
        [Description("Khám ngoại trú thông thường")]
        generalOutpat = 1,
        /// <summary>
        /// gấp khám
        /// </summary>
        [Description("Cấp cứu")]
        emerDiagnosis = 2,
        /// <summary>
        /// chuyên gia Ngoại trú
        /// </summary>
        [Description("Khám chuyên gia")]
        expertOutpat = 3,
        /// <summary>
        /// chuyên gia Ngoại trú
        /// </summary>
        [Description("Khám bệnh đặc biệt")]
        SpecialOutpat = 4,
        /// <summary>
        /// trọng lớn bệnh bệnh Ngoại trú
        /// </summary>
        [Description("Khám bệnh hiểm nghèo")]
        MajorDiseases = 5,
        /// <summary>
        /// Bệnh mạn tính Ngoại trú
        /// </summary>
        [Description("Khám bệnh mạn tính")]
        ChronicDisease = 6,
        /// <summary>
        /// cư dân hai bệnh
        /// </summary>
        [Description("Hai bệnh của dân cư")]
        jmlb = 7,
        /// <summary>
        /// Khám ngoại trú chấn thương ngoài ý muốn
        /// </summary>
        [Description("Khám tai nạn thương tích")]
        ywshmz = 8,
        /// <summary>
        /// sinh dục Ngoại trú
        /// </summary>
        [Description("Khám sinh sản")]
        symz = 9,
        /// <summary>
        /// Khám ngoại trú lao đa kháng thuốc
        /// </summary>
        [Description("Khám lao đa kháng thuốc")]
        ndyjhmz = 10,
        /// <summary>
        /// trẻ em hai bệnh Ngoại trú
        /// </summary>
        [Description("Hai bệnh trẻ em")]
        etlbmz = 11,
        /// <summary>
        /// Khám ngoại trú đơn giản
        /// </summary>
        [Description("Khám đơn giản")]
        jymz = 12
    }

    /// <summary>
    /// Đơn thuốc Loại
    /// </summary>
    public enum EnumCflx
    {
        /// <summary>
        /// Thuốc Tây Đơn thuốc
        /// </summary>
        [Description("Đơn thuốc Tây")]
        WMPres = 1,
        /// <summary>
        /// Thuốc Đông y Đơn thuốc
        /// </summary>
        [Description("Đơn thuốc Đông y")]
        TCMPres = 2,
        /// <summary>
        /// Phục hồi chức năng Đơn thuốc
        /// </summary>
        [Description("Đơn phục hồi chức năng")]
        RehabPres = 3,
        /// <summary>
        /// Xét nghiệm Đơn thuốc
        /// </summary>
        [Description("Đơn xét nghiệm")]
        InspectionPres = 4,
        /// <summary>
        /// Kiểm tra Đơn thuốc
        /// </summary>
        [Description("Đơn kiểm tra")]
        ExaminationPres = 5,
        /// <summary>
        /// Hạng mục thông thường Đơn thuốc
        /// </summary>
        [Description("Đơn hạng mục thường quy")]
        RegularItemPres = 6,
        /// <summary>
        /// điện tử Đơn thuốc
        /// </summary>
		[Description("Đơn thuốc điện tử")]
		Dzcf =7,
        /// <summary>
        /// y dùng Vật tư tiêu hao
        /// </summary>
        [Description("Vật tư y tế")]
        Yyhc = 8
    }

    public enum EnumYpyf
    {
        [Description("Tây y")]
        WM=1,
        [Description("Đông y")]
        TCM=2
    }

    /// <summary>
    /// Loại chẩn đoán
    /// </summary>
    public enum EnumZdlx
    {
        /// <summary>
        /// chính Chẩn đoán
        /// </summary>
        [Description("Chẩn đoán chính")]
        Main = 1,
        /// <summary>
        /// phụ Chẩn đoán
        /// </summary>
        [Description("Chẩn đoán phụ")]
        Deputy = 2
    }

    /// <summary>
    /// Đơn thuốc mẫu Loại
    /// (Hồ sơ mẫu Loại cũng là dùng này Liệt kê)
    /// </summary>
    public enum EnumCfMbLx
    {
        /// <summary>
        /// Cá nhân
        /// </summary>
        [Description("Cá nhân")]
        personal = 1,
        /// <summary>
        /// Khoa
        /// </summary>
        [Description("Khoa")]
        department = 2,
        /// <summary>
        /// Toàn viện
        /// </summary>
        [Description("Toàn viện")]
        hospital = 3,
    }

    /// <summary>
    /// Xét nghiệm Kiểm tra Bộ mẫu Loại  (Xét nghiệm Kiểm tra chung dùng một trang mặt,chỉ là tham số(tức type)không cùng)
    /// </summary>
    public enum EnumjyjczutaoLx
    {
        /// <summary>
        /// Xét nghiệm
        /// </summary>
        [Description("Xét nghiệm")]
        jy = 1,
        /// <summary>
        /// Kiểm tra
        /// </summary>
        [Description("Kiểm tra")]
        jc = 2
    }

    /// <summary>
    /// Xét nghiệm Kiểm tra mẫu Loại  (Xét nghiệm Kiểm tra chung dùng một trang mặt,chỉ là tham số(tức type)không cùng)
    /// </summary>
    public enum EnumjyjcmbLx
    {
        /// <summary>
        /// Xét nghiệm
        /// </summary>
        [Description("Xét nghiệm")]
        jy = 1,
        /// <summary>
        /// Kiểm tra
        /// </summary>
        [Description("Kiểm tra")]
        jc = 2
    }

    /// <summary>
    /// Mục thu phí Chiến lược tính giá
    /// </summary>
    public enum EnumSfxmJjcl
    {
        /// <summary>
        /// Theo thời lượng
        /// </summary>
        [Description("Theo thời lượng")]
        Time = 1,
        /// <summary>
        /// Theo số lượng
        /// </summary>
        [Description("Theo số lượng")]
        Amount,
        /// <summary>
        /// Theo diện tích
        /// </summary>
        [Description("Theo diện tích")]
        Acreage,
    }

    /// <summary>
    /// lịch sử Hồ sơ đến nguồn
    /// </summary>
    public enum Enumlsblly
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Hệ thống này")]
        self = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("api")]
        api,
    }

    /// <summary>
    /// Đăng nhập Cờ
    /// </summary>
    public enum EnumLoginFlag
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Đăng nhập một lần (SSO)")]
        SSO = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Đăng nhập hệ thống")]
        App = 2
    }

    /// <summary>
    /// uống ăn Loại
    /// </summary>
    public enum Enumyslb {
        /// <summary>
        /// 
        /// </summary>
        [Description("Cơm thường người Hồi")]
        hmps = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Ăn bán lỏng người Hồi")]
        hmbl = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("Ăn lỏng người Hồi")]
        hmlz = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("Cơm thường")]
        ybps = 4,
        /// <summary>
        /// 
        /// </summary>
        [Description("Ăn bán lỏng")]
        ybbl = 5,
        /// <summary>
        /// 
        /// </summary>
        [Description("Ăn lỏng")]
        yblz = 6,
        /// <summary>
        /// 
        /// </summary>
        [Description("Tự chuẩn bị bữa ăn")]
        sszb = 7,
        /// <summary>
        /// 
        /// </summary>
        [Description("Nhịn ăn")]
        js = 8,
        /// <summary>
        /// 
        /// </summary>
        [Description("Dinh dưỡng qua đường ruột")]
        cnyy = 9,
        /// <summary>
        /// 
        /// </summary>
        [Description("Chất bổ sung dinh dưỡng")]
        yybcj = 10,
    }

    /// <summary>
    /// Ngoại trú đăng ký khám đến nguồn
    /// </summary>
    public enum EnumMzghly
    {
        [Description("His")]
        his = 0,
        [Description("Đặt hẹn")]
        yy = 1,
        [Description("Ngân hàng - Y tế (Yin Yi Tong)")]
        yyt = 3,
        [Description("WeChat")]
        wechat = 4,
        [Description("Alipay")]
        alipay = 5,
        [Description("Khám sức khỏe")]
        tj = 6,
    }

    /// <summary>
    /// xếp đội gọi số Trạng thái
    /// </summary>
    public enum EmunQueueCalledStu
    {
        [Description("Đã điểm danh")]
        sign = 1,
        [Description("Đã gọi")]
        call = 2,
        [Description("Quá số")]
        pass = 3,
        [Description("Đã trả lời")]
        reply = 4,
    }

    /// <summary>
    /// Dấu hiệu ngoại trú/nội trú  0 Chung 1 chỉ Ngoại trú 2 chỉ Nhập viện 3 Hệ thống(Ngoại trú Nhập viện đều không có thể dùng)VD nếu:nhà giường
    /// </summary>
    public enum Enummzzybz
    {

        /// <summary>
        /// chỉ Ngoại trú
        /// </summary>
        [Description("Ngoại trú")]
        mz = 1,
        /// <summary>
        /// chỉ Nhập viện
        /// </summary>
        [Description("Nội trú")]
        zy = 2,
        ///// <summary>
        ///// Chung
        ///// </summary>
        //[Description("Chung")]
        //ty = 9,
    }
    public enum Enumzxzt
    {
        /// <summary>
        /// chưa Thực hiện
        /// </summary>
        [Description("Chưa thực hiện")]
        wzx = 0,
        /// <summary>
        /// đã Thực hiện
        /// </summary>
        [Description("Đã thực hiện")]
        yzx = 1
       
    } 
    /// <summary>
    /// phút khám gọi số Trạng thái
    /// </summary>
    public enum EmunConsultCalledStu
    {
        [Description("Chờ gọi số")]
        wait = 1,
        [Description("Đã gọi")]
        call = 2,
        [Description("Quá số")]
        pass = 3,
        [Description("Đã trả lời")]
        reply = 4,
        [Description("Gọi lại số")]
        recall = 5,
    }
    /// <summary>
    /// xa trình khám trị Trạng thái
    /// </summary>
    public enum Emunzlzt
    {
        [Description("Chờ xác nhận")]
        dqr = 1,
        [Description("Đang khám")]
        jzz = 2,
        [Description("Đã kết thúc")]
        yjs = 3,
        [Description("Đã trả lại")]
        yth = 4,
        [Description("Đã hủy")]
        ycx = 5,
        [Description("Đã phát thuốc")]
        yfy = 6,
    }
    /// <summary>
    /// lãi lỗ Cờ
    /// </summary>
    public enum EnumSybz
    {
        /// <summary>
        /// Báo hỏng
        /// </summary>
        [Description("Báo hỏng")]
        Loss = 0,

        /// <summary>
        /// báo thừa
        /// </summary>
        [Description("Báo thừa")]
        Profit = 1,
    }
    public enum EnumOutOrInStorageBillType
    {
        /// <summary>
        /// ngoài bộ Nhập kho
        /// </summary>
        [Description("Nhập kho ngoài")]
        Wbrk = 1,

        /// <summary>
        /// ngoài bộ Xuất kho
        /// </summary>
        [Description("Xuất kho ngoài")]
        Wbck = 2,

        /// <summary>
        /// trực tiếp Xuất kho
        /// </summary>
        [Description("Xuất kho trực tiếp")]
        Zjck = 3,

        /// <summary>
        /// đăng ký lĩnh Xuất kho
        /// </summary>
        [Description("Xuất kho theo đơn lĩnh")]
        Slck = 4,

        /// <summary>
        /// trong bộ trả hàng
        /// </summary>
        [Description("Trả lại nội bộ")]
        Nbth = 5,

        /// <summary>
        /// Báo hỏng báo thừa
        /// </summary>
        [Description("Báo hỏng/báo thừa")]
        Bsby = 6,

        /// <summary>
        /// phối gửi đến Khoa
        /// </summary>
        [Description("Xuất kho đến khoa")] //phiếu số phiếu ckZKS111111111111
        chukuzhikeshi = 7,

        /// <summary>
        /// Khoa đăng ký lĩnh
        /// </summary>
        [Description("Khoa lĩnh")]
        kssl = 8,

        /// <summary>
        /// Khoa đăng ký lĩnh Xuất kho
        /// </summary>
        [Description("Khoa lĩnh xuất kho")]
        ksSlck = 9,

        /// <summary>
        /// Mua hàng tính vạch
        /// </summary>
        [Description("Kế hoạch mua sắm")]
        purchasingPlan = 10,

        /// <summary>
        /// Mua hàng đặt phiếu
        /// </summary>
        [Description("Đơn hàng mua sắm")]
        purchasingOrder = 11,
    }
    /// <summary>
    /// Hiệu lực/Vô hiệu
    /// </summary>
    public enum Enumzt
    {
        /// <summary>
        /// Vô hiệu
        /// </summary>
        [Description("Không hiệu lực")]
        Disable = 0,

        /// <summary>
        /// Hiệu lực
        /// </summary>
        [Description("Hiệu lực")]
        Enable = 1

    }
    /// <summary>
    /// Phê duyệt Trạng thái
    /// </summary>
    public enum EnumAuditState
    {

        /// <summary>
        /// chờ chỗ lý
        /// </summary>
        [Description("Chờ xử lý")]
        Waiting = 0,

        /// <summary>
        /// Phê duyệt thông qua
        /// </summary>
        [Description("Duyệt đạt")]
        Adopt = 1,

        /// <summary>
        /// Phê duyệt không thông qua
        /// </summary>
        [Description("Duyệt không đạt")]
        Refuse = 2,

        /// <summary>
        /// Đã hủy bỏ
        /// </summary>
        [Description("Đã hủy bỏ")]
        Cancelled = 3,

        /// <summary>
        /// tạm tồn tại
        /// </summary>
        [Description("Lưu tạm")]
        Temporary = 4,
    }
    /// <summary>
    /// Tồn kho Hiển thị
    /// </summary>
    public enum EnumKCXS
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("Tất cả")]
        None = -1,
        /// <summary>
        /// Hiển thị không Tồn kho
        /// </summary>
        [Description("Hiển thị tồn kho bằng 0")]
        Xslkc = 0,
        /// <summary>
        /// không Hiển thị lý luận Số lượng là không
        /// </summary>
        [Description("Không hiển thị số lượng lý thuyết bằng 0")]
        Bxsllslwl = 1,
        /// <summary>
        /// không Hiển thị thực tế Số lượng là không
        /// </summary>
        [Description("Không hiển thị số lượng thực tế bằng 0")]
        Bxssjslwl = 2,
        /// <summary>
        /// không Hiển thị thực tế Số lượng là không
        /// </summary>
        [Description("Không hiển thị khi cả hai bằng 0")]
        Bxslzdwl = 4
    }
}
