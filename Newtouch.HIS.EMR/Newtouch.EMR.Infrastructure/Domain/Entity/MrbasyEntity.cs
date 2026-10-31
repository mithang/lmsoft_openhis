using System;
using SqlSugar;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2020-03-13 11:29
    /// Mô tả:Bản ghi trang đầu bệnh án
    /// </summary>
    [SugarTable("mr_basy")]
    public class MrbasyEntity 
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Phương thức thanh toán y tế
        /// </summary>
        /// <returns></returns>
        public string YLFKFS { get; set; }

        /// <summary>
        /// Số thẻ sức khỏe
        /// </summary>
        /// <returns></returns>
        public string JKKH { get; set; }

        /// <summary>
        /// Số lần nhập viện
        /// </summary>
        /// <returns></returns>
        public string ZYCS { get; set; }

        /// <summary>
        /// Số hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// PATID
        /// </summary>
        /// <returns></returns>
        public string PATID { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        /// <returns></returns>
        public string XM { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// <returns></returns>
        public string XB { get; set; }

        /// <summary>
        /// Định dạng là:YYYYMMDD,ví dụ:20131125
        /// </summary>
        /// <returns></returns>
        public DateTime CSRQ { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        /// <returns></returns>
        public decimal NL { get; set; }

        /// <summary>
        /// Quốc tịch
        /// </summary>
        /// <returns></returns>
        public string GJ { get; set; }

        /// <summary>
        /// 11
        /// </summary>
        /// <returns></returns>
        public decimal? BZYZSNL { get; set; }

        /// <summary>
        /// Cân nặng lúc sinh của trẻ sơ sinh(gam)
        /// </summary>
        /// <returns></returns>
        public decimal? XSECSTZ { get; set; }

        /// <summary>
        /// Cân nặng nhập viện của trẻ sơ sinh(gam)
        /// </summary>
        /// <returns></returns>
        public decimal? XSERYTZ { get; set; }

        /// <summary>
        /// Nơi sinh
        /// </summary>
        /// <returns></returns>
        public string CSD { get; set; }

        /// <summary>
        /// Quê quán
        /// </summary>
        /// <returns></returns>
        public string GG { get; set; }

        /// <summary>
        /// Dân tộc
        /// </summary>
        /// <returns></returns>
        public string MZ { get; set; }

        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        /// <returns></returns>
        public string SFZH { get; set; }

        /// <summary>
        /// Nghề nghiệp
        /// </summary>
        /// <returns></returns>
        public string ZY { get; set; }

        /// <summary>
        /// Hôn nhân
        /// </summary>
        /// <returns></returns>
        public string HY { get; set; }

        /// <summary>
        /// Địa chỉ hiện tại
        /// </summary>
        /// <returns></returns>
        public string XZZ { get; set; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        /// <returns></returns>
        public string DH { get; set; }

        /// <summary>
        /// Mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string XZZYB { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu
        /// </summary>
        /// <returns></returns>
        public string HKDZ { get; set; }

        /// <summary>
        /// Mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string HKDYB { get; set; }

        /// <summary>
        /// Địa chỉ nơi làm việc
        /// </summary>
        /// <returns></returns>
        public string GZDWJDZ { get; set; }

        /// <summary>
        /// Điện thoại cơ quan
        /// </summary>
        /// <returns></returns>
        public string DWDH { get; set; }

        /// <summary>
        /// Mã bưu chính
        /// </summary>
        /// <returns></returns>
        public string DWYB { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        /// <returns></returns>
        public string LXRXM { get; set; }

        /// <summary>
        /// Quan hệ
        /// </summary>
        /// <returns></returns>
        public string GX { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// <returns></returns>
        public string LXRDZ { get; set; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        /// <returns></returns>
        public string LXRDH { get; set; }

        /// <summary>
        /// Đường nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYTJ { get; set; }

        /// <summary>
        /// Thời gian nhập viện
        /// </summary>
        /// <returns></returns>
        public DateTime RYSJ { get; set; }

        /// <summary>
        /// Giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYSJS { get; set; }

        /// <summary>
        /// Chuyên khoa nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYKB { get; set; }

        /// <summary>
        /// Phòng bệnh nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYBF { get; set; }

        /// <summary>
        /// Khoa chuyển
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }

        /// <summary>
        /// Định dạng là:YYYYMMDD,ví dụ:20131125
        /// </summary>
        /// <returns></returns>
        public DateTime? CYSJ { get; set; }

        /// <summary>
        /// Giờ
        /// </summary>
        /// <returns></returns>
        public decimal? CYSJS { get; set; }

        /// <summary>
        /// Mã:Mã khoa
        /// </summary>
        /// <returns></returns>
        public string CYKB { get; set; }

        /// <summary>
        /// Phòng bệnh ra viện
        /// </summary>
        /// <returns></returns>
        public string CYBF { get; set; }

        /// <summary>
        /// Nằm viện thực tế(Ngày)
        /// </summary>
        /// <returns></returns>
        public string SJZYTS { get; set; }

        /// <summary>
        /// Ngoại trú(cấp cứu) Chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string MZZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string MZZDDM { get; set; }

        /// <summary>
        /// Có nguy kịch và bệnh nặng hay không
        /// </summary>
        /// <returns></returns>
        public string BWHBZ { get; set; }

        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string RYZDDM { get; set; }

        /// <summary>
        /// Áp dụng đường đi lâm sàng
        /// </summary>
        /// <returns></returns>
        public string SSLCLJ { get; set; }

        /// <summary>
        /// Hoàn thành đường đi lâm sàng
        /// </summary>
        /// <returns></returns>
        public string WCLCLJ { get; set; }

        /// <summary>
        /// Nguyên nhân rời lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string TCYY { get; set; }

        /// <summary>
        /// Lộ trình lâm sàng có biến đổi hay không
        /// </summary>
        /// <returns></returns>
        public string SFBY { get; set; }

        /// <summary>
        /// Nguyên nhân biến đổi lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string BYYY { get; set; }

        /// <summary>
        /// Số lần cấp cứu
        /// </summary>
        /// <returns></returns>
        public decimal? QJCS { get; set; }

        /// <summary>
        /// Số lần cấp cứu thành công
        /// </summary>
        /// <returns></returns>
        public decimal? QJCGCS { get; set; }

        /// <summary>
        /// Ngày chẩn đoán xác định
        /// </summary>
        /// <returns></returns>
        public DateTime? QZRQ { get; set; }

        /// <summary>
        /// Có phải phẫu thuật theo lịch hay không
        /// </summary>
        /// <returns></returns>
        public string ZQSS { get; set; }

        /// <summary>
        /// Mã:Mã nguồn bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BRLY { get; set; }

        /// <summary>
        /// Nguyên nhân bên ngoài gây ngộ độc
        /// </summary>
        /// <returns></returns>
        public string WBYY { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string H23 { get; set; }

        /// <summary>
        /// Chẩn đoán bệnh lý cho ra
        /// </summary>
        /// <returns></returns>
        public string BLZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string BLZDDM { get; set; }

        /// <summary>
        /// Số bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BLH { get; set; }

        /// <summary>
        /// Dị ứng thuốc
        /// </summary>
        /// <returns></returns>
        public string YWGM { get; set; }

        /// <summary>
        /// Bệnh do thuốc dị ứng
        /// </summary>
        /// <returns></returns>
        public string GMYW { get; set; }

        /// <summary>
        /// Khám nghiệm tử thi bệnh nhân tử vong
        /// </summary>
        /// <returns></returns>
        public string SWHZSJ { get; set; }

        /// <summary>
        /// Nhóm máu
        /// </summary>
        /// <returns></returns>
        public string XX { get; set; }

        /// <summary>
        /// Mã:Rh
        /// </summary>
        /// <returns></returns>
        public string RH { get; set; }

        /*Loại máu truyền*/
        /// <summary>
        /// 1.Hồng cầu
        /// </summary>
        /// <returns></returns>
        public decimal? HXB { get; set; }

        /// <summary>
        /// 2.Tiểu cầu
        /// </summary>
        /// <returns></returns>
        public decimal? XXB { get; set; }

        /// <summary>
        /// 3.Huyết tương
        /// </summary>
        /// <returns></returns>
        public decimal? XJ { get; set; }

        /// <summary>
        /// 4.Máu toàn phần
        /// </summary>
        /// <returns></returns>
        public decimal? QX { get; set; }

        /// <summary>
        /// 5.Truyền máu tự thân
        /// </summary>
        /// <returns></returns>
        public decimal? ZTXHS { get; set; }

        /// <summary>
        /// 6.Albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BDB { get; set; }

        /// <summary>
        /// 7.Tủa lạnh
        /// </summary>
        /// <returns></returns>
        public decimal? LCD { get; set; }

        /// <summary>
        /// 8.Khác
        /// </summary>
        /// <returns></returns>
        public string QT { get; set; }

        /// <summary>
        /// Phản ứng truyền máu
        /// </summary>
        /// <returns></returns>
        public string SXFY { get; set; }

        /// <summary>
        /// Thời hạn tái khám
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// Thời hạn tái khám_Tuần
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXZ { get; set; }

        /// <summary>
        /// Thời hạn tái khám_Tháng
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXY { get; set; }

        /// <summary>
        /// Thời hạn tái khám_Năm
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXN { get; set; }

        /// <summary>
        /// Chủ nhiệm khoa
        /// </summary>
        /// <returns></returns>
        public string KZR { get; set; }

        /// <summary>
        /// Chủ nhiệm(Phó chủ nhiệm)Bác sĩ
        /// </summary>
        /// <returns></returns>
        public string ZRYS { get; set; }

        /// <summary>
        /// Bác sĩ chẩn trị chính
        /// </summary>
        /// <returns></returns>
        public string ZZYS { get; set; }

		/// <summary>
		/// Bác sĩ điều trị chính
		/// </summary>
		/// <returns></returns>
		public string ZZYS1 { get; set; }


		/// <summary>
		/// Bác sĩ nội trú
		/// </summary>
		/// <returns></returns>
		public string ZYYS { get; set; }

        /// <summary>
        /// Điều dưỡng chịu trách nhiệm
        /// </summary>
        /// <returns></returns>
        public string ZRHS { get; set; }

        /// <summary>
        /// Bác sĩ đào tạo nâng cao
        /// </summary>
        /// <returns></returns>
        public string JXYS { get; set; }

        /// <summary>
        /// Bác sĩ thực tập
        /// </summary>
        /// <returns></returns>
        public string SXYS { get; set; }

        /// <summary>
        /// Nhân viên mã hóa
        /// </summary>
        /// <returns></returns>
        public string BMY { get; set; }

        /// <summary>
        /// Mã:Chất lượng hồ sơ bệnh án
        /// </summary>
        /// <returns></returns>
        public string BAZL { get; set; }

        /// <summary>
        /// Bác sĩ kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string ZKYS { get; set; }

        /// <summary>
        /// Điều dưỡng kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string ZKHS { get; set; }

        /// <summary>
        /// Ngày kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public DateTime? ZKRQ { get; set; }

        /// <summary>
        /// Cách rời viện
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }

        /// <summary>
        /// Chuyển viện theo y lệnh,Tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string YZZY_YLJG { get; set; }

        /// <summary>
        /// Chuyển đến cơ sở dịch vụ y tế cộng đồng theo y lệnh/Trạm y tế xã,Tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string WSY_YLJG { get; set; }

        /// <summary>
        /// Có ra viện không31ngày tái nhập viện có phẫu thuật theo kế hoạch
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }

        /// <summary>
        /// Mục đích
        /// </summary>
        /// <returns></returns>
        public string MD { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não:Ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não:Giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não:phút
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_F { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não:Ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não:Giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não:phút
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_F { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Quản lý lộ trình lâm sàng 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string LCLJGL { get; set; }

        /// <summary>
        /// Quản lý loại bệnh 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string LCBZGL { get; set; }

        /// <summary>
        /// Phân loại quản lý loại bệnh
        /// </summary>
        /// <returns></returns>
        public string BZGLFL { get; set; }

        /// <summary>
        /// Phân loại tình trạng bệnh 1 Nguy kịch 2 Bệnh nặng 3 Khó 4 Cấp cứu 5 Thông thường
        /// </summary>
        /// <returns></returns>
        public string BQFX { get; set; }

        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// CSD_SN
        /// </summary>
        /// <returns></returns>
        public string CSD_SN { get; set; }

        /// <summary>
        /// CSD_SI
        /// </summary>
        /// <returns></returns>
        public string CSD_SI { get; set; }

        /// <summary>
        /// CSD_QX
        /// </summary>
        /// <returns></returns>
        public string CSD_QX { get; set; }

        /// <summary>
        /// CSD_JD
        /// </summary>
        /// <returns></returns>
        public string CSD_JD { get; set; }

        /// <summary>
        /// XZZ_SN
        /// </summary>
        /// <returns></returns>
        public string XZZ_SN { get; set; }

        /// <summary>
        /// XZZ_SI
        /// </summary>
        /// <returns></returns>
        public string XZZ_SI { get; set; }

        /// <summary>
        /// XZZ_QX
        /// </summary>
        /// <returns></returns>
        public string XZZ_QX { get; set; }

        /// <summary>
        /// XZZ_JD
        /// </summary>
        /// <returns></returns>
        public string XZZ_JD { get; set; }

        /// <summary>
        /// HKDZ_SN
        /// </summary>
        /// <returns></returns>
        public string HKDZ_SN { get; set; }

        /// <summary>
        /// HKDZ_SI
        /// </summary>
        /// <returns></returns>
        public string HKDZ_SI { get; set; }

        /// <summary>
        /// HKDZ_QX
        /// </summary>
        /// <returns></returns>
        public string HKDZ_QX { get; set; }

        /// <summary>
        /// HKDZ_JD
        /// </summary>
        /// <returns></returns>
        public string HKDZ_JD { get; set; }

        /// <summary>
        /// LXRDZ_SN
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_SN { get; set; }

        /// <summary>
        /// LXRDZ_SI
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_SI { get; set; }

        /// <summary>
        /// LXRDZ_QX
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_QX { get; set; }

        /// <summary>
        /// LXRDZ_JD
        /// </summary>
        /// <returns></returns>
        public string LXRDZ_JD { get; set; }

        /// <summary>
        /// RYCH
        /// </summary>
        /// <returns></returns>
        public string RYCH { get; set; }

        /// <summary>
        /// CYCH
        /// </summary>
        /// <returns></returns>
        public string CYCH { get; set; }

        /// <summary>
        /// Trạng thái bệnh án 1 Chờ nhập 2 Đang nhập 3 Đã lưu trữ
        /// </summary>
        /// <returns></returns>
        public string bazt { get; set; }

        /// <summary>
        /// Tổng chi phí
        /// </summary>
        /// <returns></returns>
        public decimal? ZFY { get; set; }

        /// <summary>
        /// Số tiền tự trả
        /// </summary>
        /// <returns></returns>
        public decimal? ZFJE { get; set; }

        /// <summary>
        /// Loại dịch vụ y tế tổng hợp(1) Phí dịch vụ y tế chung
        /// </summary>
        /// <returns></returns>
        public decimal? YLFUF { get; set; }

        /// <summary>
        /// Phí biện chứng luận trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? BZLZF { get; set; }

        /// <summary>
        /// Phí hội chẩn biện chứng luận trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYBLZHZF { get; set; }

        /// <summary>
        /// Phí thao tác điều trị chung
        /// </summary>
        /// <returns></returns>
        public decimal? ZLCZF { get; set; }

        /// <summary>
        /// Phí điều dưỡng
        /// </summary>
        /// <returns></returns>
        public decimal? HLF { get; set; }

        /// <summary>
        /// Chi phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? QTFY { get; set; }

        /// <summary>
        /// Loại chẩn đoán(5) Phí chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public decimal? BLZDF { get; set; }

        /// <summary>
        /// Phí chẩn đoán xét nghiệm
        /// </summary>
        /// <returns></returns>
        public decimal? SYSZDF { get; set; }

        /// <summary>
        /// Phí chẩn đoán hình ảnh
        /// </summary>
        /// <returns></returns>
        public decimal? YXXZDF { get; set; }

        /// <summary>
        /// Phí hạng mục chẩn đoán lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? LCZDXMF { get; set; }

        /// <summary>
        /// Loại điều trị(9) Phí hạng mục điều trị không phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? FSSZLXMF { get; set; }

        /// <summary>
        /// Phí vật lý trị liệu lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? WLZLF { get; set; }

        /// <summary>
        /// Phí điều trị phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? SSZLF { get; set; }

        /// <summary>
        /// Phí gây mê
        /// </summary>
        /// <returns></returns>
        public decimal? MAF { get; set; }

        /// <summary>
        /// Phí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? SSF { get; set; }

        /// <summary>
        /// Loại phục hồi chức năng(11) Phí phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public decimal? KFF { get; set; }

        /// <summary>
        /// Loại đông y(Dịch vụ y tế Đông y và y học dân tộc)(12)Chẩn đoán Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZLF { get; set; }

        /// <summary>
        /// Điều trị Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZL { get; set; }

        /// <summary>
        /// Điều trị ngoài Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYWZ { get; set; }

        /// <summary>
        /// Chấn thương xương Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYGS { get; set; }

        /// <summary>
        /// Châm cứu và cứu ngải
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYJF { get; set; }

        /// <summary>
        /// Điều trị xoa bóp Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTNZL { get; set; }

        /// <summary>
        /// Điều trị hậu môn trực tràng Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYGCZL { get; set; }

        /// <summary>
        /// Điều trị đặc biệt Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTSZL { get; set; }

        /// <summary>
        /// Đông y khác
        /// </summary>
        /// <returns></returns>
        public decimal? ZYQT { get; set; }

        /// <summary>
        /// Pha chế gia công đặc biệt Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTSTPJG { get; set; }

        /// <summary>
        /// Ăn uống theo biện chứng
        /// </summary>
        /// <returns></returns>
        public decimal? BZSS { get; set; }

        /// <summary>
        /// Loại thuốc tây(15) Chi phí thuốc tây
        /// </summary>
        /// <returns></returns>
        public decimal? XYF { get; set; }

        /// <summary>
        /// Phí thuốc kháng khuẩn
        /// </summary>
        /// <returns></returns>
        public decimal? KJYWF { get; set; }

        /// <summary>
        /// Loại thuốc đông y(16) Chi phí thuốc thành phẩm đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF { get; set; }

        /// <summary>
        /// Phí chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZJF { get; set; }

        /// <summary>
        /// Chi phí thuốc đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF1 { get; set; }

        /// <summary>
        /// Loại máu và chế phẩm máu(18) Chi phí máu
        /// </summary>
        /// <returns></returns>
        public decimal? XF { get; set; }

        /// <summary>
        /// Chi phí chế phẩm albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BDBLZPF { get; set; }

        /// <summary>
        /// Chi phí chế phẩm globulin
        /// </summary>
        /// <returns></returns>
        public decimal? QDBLZPF { get; set; }

        /// <summary>
        /// Chi phí chế phẩm yếu tố đông máu
        /// </summary>
        /// <returns></returns>
        public decimal? NXYZLZPF { get; set; }

        /// <summary>
        /// Chi phí chế phẩm cytokine
        /// </summary>
        /// <returns></returns>
        public decimal? XBYZLZPF { get; set; }

        /// <summary>
        /// Loại vật tư tiêu hao(23) Chi phí vật tư y tế dùng một lần cho kiểm tra
        /// </summary>
        /// <returns></returns>
        public decimal? HCYYCLF { get; set; }

        /// <summary>
        /// Chi phí vật tư y tế dùng một lần cho điều trị
        /// </summary>
        /// <returns></returns>
        public decimal? YYCLF { get; set; }

        /// <summary>
        /// Chi phí vật tư y tế dùng một lần cho phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? YCXYYCLF { get; set; }

        /// <summary>
        /// Loại khác(26) Phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? QTF { get; set; }

        /// <summary>
        /// kiểm tra CT
        /// </summary>
        public string CT { get; set; }

        /// <summary>
        /// kiểm traPETCT
        /// </summary>
        public string PETCT { get; set; }

        /// <summary>
        /// Kiểm tra CT nguồn kép CT
        /// </summary>
        public string SYCT { get; set; }

        /// <summary>
        /// kiểm traBSiêu âm
        /// </summary>
        public string BC { get; set; }

        /// <summary>
        /// kiểm traXPhim
        /// </summary>
        public string XP { get; set; }

        /// <summary>
        /// Kiểm tra siêu âm tim
        /// </summary>
        public string CSXDT { get; set; }

        /// <summary>
        /// kiểm traMRI
        /// </summary>
        public string MRI { get; set; }

        /// <summary>
        /// Kiểm tra đồng vị
        /// </summary>
        public string TWSJC { get; set; }

        /// <summary>
        /// ⅠThời gian sử dụng kháng sinh dự phòng vết mổ theo loại
        /// </summary>
        public decimal? SYCXSJ { get; set; }

        /// <summary>
        /// Dùng thuốc kết hợp
        /// </summary>
        public string LHYY { get; set; }

        /*Trang phụ*/
        /// <summary>
        /// Tình trạng nhiễm khuẩn bệnh viện
        /// </summary>
        public string YYGRQK { get; set; }
        /// <summary>
        /// Nhiễm khuẩn bệnh viện có liên quan đến phẫu thuật không
        /// </summary>
        public string YYGRSSXG { get; set; }
        /// <summary>
        /// Nhiễm khuẩn bệnh viện có liên quan đến thao tác xâm lấn không
        /// </summary>
        public string YYGRSFQRXG { get; set; }
        /// <summary>
        /// Tình hình sử dụng kháng sinh
        /// </summary>
        public string KJYWSYQK { get; set; }
        /// <summary>
        /// Tên kháng sinh1
        /// </summary>
        public string KJYWMC1 { get; set; }
        /// <summary>
        /// Tên kháng sinh2
        /// </summary>
        public string KJYWMC2 { get; set; }
        /// <summary>
        /// Tên kháng sinh3
        /// </summary>
        public string KJYWMC3 { get; set; }
        /// <summary>
        /// Tên kháng sinh4
        /// </summary>
        public string KJYWMC4 { get; set; }
        /// <summary>
        /// Tên kháng sinh5
        /// </summary>
        public string KJYWMC5 { get; set; }
        /// <summary>
        /// Tên kháng sinh6
        /// </summary>
        public string KJYWMC6 { get; set; }

        /// <summary>
        /// Có xảy ra loét do tì đè không
        /// </summary>
        public string SFFSYC { get; set; }
        /// <summary>
        /// Có xảy ra trong thời gian nằm viện không
        /// </summary>
        public string SFZYQJFS { get; set; }
        /// <summary>
        /// Giai đoạn loét do tì đè
        /// </summary>
        public string YCFQ { get; set; }
        /// <summary>
        /// Phản ứng truyền dịch
        /// </summary>
        public string SYFY { get; set; }
        /// <summary>
        /// Thuốc gây phản ứng
        /// </summary>
        public string YFFYDYW { get; set; }
        /// <summary>
        /// Biểu hiện lâm sàng khi truyền dịch
        /// </summary>
        public string SYLCBX { get; set; }
        /// <summary>
        /// Trong thời gian nằm viện có xảy ra ngã hoặc rơi khỏi giường không
        /// </summary>
        public string ZYSFDDHZC { get; set; }
        /// <summary>
        /// Mức độ tổn thương do ngã hoặc rơi khỏi giường trong thời gian nằm viện
        /// </summary>
        public string ZYDDHZCDCD { get; set; }
        /// <summary>
        /// Nguyên nhân ngã hoặc rơi khỏi giường
        /// </summary>
        public string DDHZCDYY { get; set; }
        /// <summary>
        /// Cố định cơ thể trong thời gian nằm viện
        /// </summary>
        public string ZYQJSTYY { get; set; }
        /// <summary>
        /// Thẩm tách khi rời viện(Thẩm tách máu,Thẩm tách phúc mạc)Giá trị nitơ urê
        /// </summary>
        public decimal? LYTXNSDZ { get; set; }
        /// <summary>
        /// Người phụ trách đơn vị
        /// </summary>
        public string DWFZR { get; set; }
        /// <summary>
        /// Người phụ trách thống kê
        /// </summary>
        public string TJFZR { get; set; }
        /// <summary>
        /// Người khai báo
        /// </summary>
        public string TBR { get; set; }
        /// <summary>
        /// Điện thoại liên hệ
        /// </summary>
        public string LXDH { get; set; }
        /// <summary>
        /// Di động
        /// </summary>
        public string SJ { get; set; }
        /// <summary>
        /// Ngày báo cáo
        /// </summary>
        public string BCRQ { get; set; }

        public int? IsLock { get; set; }

		/// <summary>
		/// (Tuổi chưa đủ1tháng tuổi) Tuổi
		/// </summary>
		public int? BZYYSNL { get; set; }

		/// <summary>
		/// Chuyển từ cơ sở y tế khác
		/// </summary>
		public string QTYLJGZR { get; set; }
        /// <summary>
        /// Áp dụng đường đi lâm sàng
        /// </summary>
        public string ZYSSLCLJ { get; set; }
        /// <summary>
        /// Sử dụng chế phẩm thuốc đông y của cơ sở y tế
        /// </summary>
        public string SYYLJGZYZJ { get; set; }
        /// <summary>
        /// Sử dụng thiết bị chẩn trị đông y
        /// </summary>
        public string SYZYZLSB { get; set; }
        /// <summary>
        /// Sử dụng kỹ thuật chẩn trị đông y
        /// </summary>
        public string SYZYZLJS { get; set; }
        /// <summary>
        /// Biện chứng thi hộ
        /// </summary>
        public string BZSH { get; set; }
    }
}
