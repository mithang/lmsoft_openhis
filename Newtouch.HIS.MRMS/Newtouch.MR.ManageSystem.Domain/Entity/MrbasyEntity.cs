using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2019-11-20 11:05
    /// Mô tả: Trang đầu bệnh án
    /// </summary>
    [Table("mr_basy")]
    public class MrbasyEntity : IEntity<MrbasyEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Hình thức thanh toán y tế
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
        /// Số bệnh án
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
        /// Định dạng là: YYYYMMDD, ví dụ：20131125
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
        /// Cân nặng khi sinh của trẻ sơ sinh(gam)
        /// </summary>
        /// <returns></returns>
        public decimal? XSECSTZ { get; set; }

        /// <summary>
        /// Cân nặng khi nhập viện của trẻ sơ sinh(gam）
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
        /// Số chứng minh nhân dân
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
        /// Mã bưu điện
        /// </summary>
        /// <returns></returns>
        public string XZZYB { get; set; }

        /// <summary>
        /// Địa chỉ hộ khẩu
        /// </summary>
        /// <returns></returns>
        public string HKDZ { get; set; }

        /// <summary>
        /// Mã bưu điện
        /// </summary>
        /// <returns></returns>
        public string HKDYB { get; set; }

        /// <summary>
        /// Đơn vị công tác và địa chỉ
        /// </summary>
        /// <returns></returns>
        public string GZDWJDZ { get; set; }

        /// <summary>
        /// Điện thoại đơn vị
        /// </summary>
        /// <returns></returns>
        public string DWDH { get; set; }

        /// <summary>
        /// Mã bưu điện
        /// </summary>
        /// <returns></returns>
        public string DWYB { get; set; }

        /// <summary>
        /// Họ tên người liên hệ
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
        /// giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYSJS { get; set; }

        /// <summary>
        /// Khoa nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYKB { get; set; }

        /// <summary>
        /// Phòng bệnh nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYBF { get; set; }

        /// <summary>
        /// Khoa chuyển đến
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }

        /// <summary>
        /// Định dạng là: YYYYMMDD, ví dụ：20131125
        /// </summary>
        /// <returns></returns>
        public DateTime CYSJ { get; set; }

        /// <summary>
        /// giờ
        /// </summary>
        /// <returns></returns>
        public decimal? CYSJS { get; set; }

        /// <summary>
        /// Mã: Mã khoa
        /// </summary>
        /// <returns></returns>
        public string CYKB { get; set; }

        /// <summary>
        /// Phòng bệnh xuất viện
        /// </summary>
        /// <returns></returns>
        public string CYBF { get; set; }

        /// <summary>
        /// Nằm viện thực tế(ngày)
        /// </summary>
        /// <returns></returns>
        public string SJZYTS { get; set; }

        /// <summary>
        /// Ngoại trú(Cấp cứu)chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string MZZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string MZZDDM { get; set; }

        /// <summary>
        /// Thực hiện lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string SSLCLJ { get; set; }

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
        /// Ngày xác định chẩn đoán
        /// </summary>
        /// <returns></returns>
        public DateTime? QZRQ { get; set; }

        /// <summary>
        /// Có phải phẫu thuật theo lịch hay không
        /// </summary>
        /// <returns></returns>
        public string ZQSS { get; set; }

        /// <summary>
        /// Mã: Mã nguồn bệnh nhân
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
        /// Chẩn đoán bệnh lý
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
        /// Bệnh do dị ứng thuốc
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
        /// Mã：Rh
        /// </summary>
        /// <returns></returns>
        public string RH { get; set; }

        /// <summary>
        /// Trưởng khoa
        /// </summary>
        /// <returns></returns>
        public string KZR { get; set; }

        /// <summary>
        /// Bác sĩ trưởng khoa (phó trưởng khoa)
        /// </summary>
        /// <returns></returns>
        public string ZRYS { get; set; }

        /// <summary>
        /// Bác sĩ điều trị
        /// </summary>
        /// <returns></returns>
        public string ZZYS { get; set; }

        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        public string ZYYS { get; set; }

        /// <summary>
        /// Điều dưỡng phụ trách
        /// </summary>
        /// <returns></returns>
        public string ZRHS { get; set; }

        /// <summary>
        /// Bác sĩ tu nghiệp
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
        /// Mã: Chất lượng bệnh án
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
        /// Hình thức rời viện
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }

        /// <summary>
        /// Chuyển viện theo y lệnh, tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string YZZY_YLJG { get; set; }

        /// <summary>
        /// Chuyển theo y lệnh đến cơ sở dịch vụ y tế cộng đồng/Trạm y tế xã/thị trấn, tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string WSY_YLJG { get; set; }

        /// <summary>
        /// Có kế hoạch tái nhập viện và phẫu thuật trong 31 ngày sau xuất viện hay không
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }

        /// <summary>
        /// Mục đích
        /// </summary>
        /// <returns></returns>
        public string MD { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não: ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não: giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước nhập viện của bệnh nhân chấn thương sọ não: phút
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_F { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não: ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não: giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau nhập viện của bệnh nhân chấn thương sọ não: phút
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
        /// Quản lý lộ trình lâm sàng 0 không, 1 là có
        /// </summary>
        /// <returns></returns>
        public string LCLJGL { get; set; }

        /// <summary>
        /// Quản lý loại bệnh: 0 là không, 1 là có
        /// </summary>
        /// <returns></returns>
        public string LCBZGL { get; set; }

        /// <summary>
        /// Phân loại quản lý loại bệnh
        /// </summary>
        /// <returns></returns>
        public string BZGLFL { get; set; }

        /// <summary>
        /// Phân loại tình trạng bệnh: 1 Nguy kịch, 2 Nặng, 3 Khó, 4 Cấp cứu, 5 Thông thường
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
        /// Trạng thái bệnh án 1 chờ nhập, 2 đang nhập 3 Đã lưu trữ
        /// </summary>
        /// <returns></returns>
        public string bazt { get; set; }

        /// <summary>
        /// Tổng phí
        /// </summary>
        /// <returns></returns>
        public decimal? ZFY { get; set; }

        /// <summary>
        /// Số tiền tự trả
        /// </summary>
        /// <returns></returns>
        public decimal? ZFJE { get; set; }

        /// <summary>
        /// Nhóm dịch vụ y tế tổng hợp(1)Phí dịch vụ y tế thông thường
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
        /// Phí thao tác điều trị thông thường
        /// </summary>
        /// <returns></returns>
        public decimal? ZLCZF { get; set; }

        /// <summary>
        /// Phí điều dưỡng
        /// </summary>
        /// <returns></returns>
        public decimal? HLF { get; set; }

        /// <summary>
        /// Phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? QTFY { get; set; }

        /// <summary>
        /// Nhóm chẩn đoán(5)Phí chẩn đoán bệnh lý
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
        /// Nhóm điều trị(9)Phí hạng mục điều trị không phẫu thuật
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
        /// Nhóm phục hồi chức năng(11)Phí phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public decimal? KFF { get; set; }

        /// <summary>
        /// Nhóm Đông y(Đông yvàDịch vụ y tế dân tộc)（12）Đông yChẩn đoán
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
        /// Chỉnh hình Đông y
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
        /// Nhóm thuốc Tây(15)Phí thuốc Tây
        /// </summary>
        /// <returns></returns>
        public decimal? XYF { get; set; }

        /// <summary>
        /// Phí thuốc kháng khuẩn
        /// </summary>
        /// <returns></returns>
        public decimal? KJYWF { get; set; }

        /// <summary>
        /// Nhóm thuốc Đông y(16)Phí thuốc thành phẩm Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF { get; set; }

        /// <summary>
        /// Phí chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZJF { get; set; }

        /// <summary>
        /// Phí thuốc thảo dược
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF1 { get; set; }

        /// <summary>
        /// Nhóm máu và chế phẩm máu(18)Phí máu
        /// </summary>
        /// <returns></returns>
        public decimal? XF { get; set; }

        /// <summary>
        /// Phí chế phẩm albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BDBLZPF { get; set; }

        /// <summary>
        /// Phí chế phẩm globulin
        /// </summary>
        /// <returns></returns>
        public decimal? QDBLZPF { get; set; }

        /// <summary>
        /// Phí chế phẩm yếu tố đông máu
        /// </summary>
        /// <returns></returns>
        public decimal? NXYZLZPF { get; set; }

        /// <summary>
        /// Phí chế phẩm cytokine
        /// </summary>
        /// <returns></returns>
        public decimal? XBYZLZPF { get; set; }

        /// <summary>
        /// Nhóm vật tư tiêu hao(23)Phí vật tư y tế dùng một lần cho kiểm tra
        /// </summary>
        /// <returns></returns>
        public decimal? HCYYCLF { get; set; }

        /// <summary>
        /// Phí vật tư y tế dùng một lần cho điều trị
        /// </summary>
        /// <returns></returns>
        public decimal? YYCLF { get; set; }

        /// <summary>
        /// Phí vật tư y tế dùng một lần cho phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? YCXYYCLF { get; set; }

        /// <summary>
        /// Nhóm khác(26)Phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? QTF { get; set; }

    }
}