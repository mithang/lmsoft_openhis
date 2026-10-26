using Newtouch.EMR.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MRHomePage
{
    public class BasyVO:BasyRelVO
    {

        public string Id { get; set; }

        /// <summary>
        /// Tên tổ chức
        /// </summary>
        /// <returns></returns>
        public string USERNAME { get; set; }

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
        /// Định dạng: YYYYMMDD, ví dụ: 20131125
        /// </summary>
        /// <returns></returns>
        public string CSRQ { get; set; }

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
        /// Cân nặng lúc sinh của trẻ sơ sinh (gam)
        /// </summary>
        /// <returns></returns>
        public decimal? XSECSTZ { get; set; }

        /// <summary>
        /// Cân nặng lúc vào viện của trẻ sơ sinh (gam)
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
        /// Mã bưu chính
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
        /// Đường vào viện
        /// </summary>
        /// <returns></returns>
        public string RYTJ { get; set; }

        /// <summary>
        /// Thời gian vào viện
        /// </summary>
        /// <returns></returns>
        public string RYSJ { get; set; }

        /// <summary>
        /// giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYSJS { get; set; }

        /// <summary>
        /// Khoa vào viện
        /// </summary>
        /// <returns></returns>
        public string RYKB { get; set; }

        /// <summary>
        /// Phòng bệnh lúc vào viện
        /// </summary>
        /// <returns></returns>
        public string RYBF { get; set; }

        /// <summary>
        /// Khoa chuyển đến
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }

        /// <summary>
        /// Định dạng: YYYYMMDD, ví dụ: 20131125
        /// </summary>
        /// <returns></returns>
        public string CYSJ { get; set; }

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
        /// Phòng bệnh lúc ra viện
        /// </summary>
        /// <returns></returns>
        public string CYBF { get; set; }

        /// <summary>
        /// Nằm viện thực tế (ngày)
        /// </summary>
        /// <returns></returns>
        public string SJZYTS { get; set; }

        /// <summary>
        /// Chẩn đoán ngoại trú (cấp cứu)
        /// </summary>
        /// <returns></returns>
        public string MZZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string MZZDDM { get; set; }

        /// <summary>
        /// Có nguy kịch và nặng hay không
        /// </summary>
        /// <returns></returns>
        public string BWHBZ { get; set; }

        /// <summary>
        /// Chẩn đoán lúc vào viện
        /// </summary>
        /// <returns></returns>
        public string RYZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string RYZDDM { get; set; }

        /// <summary>
        /// Thực hiện lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string SSLCLJ { get; set; }

        /// <summary>
        /// Hoàn thành lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string WCLCLJ { get; set; }

        /// <summary>
        /// Lý do thoát khỏi lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string TCYY { get; set; }

        /// <summary>
        /// Lộ trình lâm sàng có biến thể hay không
        /// </summary>
        /// <returns></returns>
        public string SFBY { get; set; }

        /// <summary>
        /// Nguyên nhân biến thể của lộ trình lâm sàng
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
        /// Ngày xác định chẩn đoán
        /// </summary>
        /// <returns></returns>
        public string QZRQ { get; set; }

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
        /// Chẩn đoán chính
        /// </summary>
        /// <returns></returns>
        public string ZYZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string JBDM { get; set; }

        /// <summary>
        /// Tình hình ra viện: 1 Khỏi, 2 Đỡ, 3 Chưa khỏi, 4 Tử vong, 5 Khác
        /// </summary>
        /// <returns></returns>
        public string CYQK { get; set; }

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
        /// Chẩn đoán bệnh lý ra
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
        /// Bệnh do thuốc gây dị ứng
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
        /// Mã: Rh
        /// </summary>
        /// <returns></returns>
        public string RH { get; set; }

        /*Loại máu truyền*/
        /// <summary>
        /// 1. Hồng cầu
        /// </summary>
        /// <returns></returns>
        public decimal? HXB { get; set; }

        /// <summary>
        /// 2. Tiểu cầu
        /// </summary>
        /// <returns></returns>
        public decimal? XXB { get; set; }

        /// <summary>
        /// 3. Huyết tương
        /// </summary>
        /// <returns></returns>
        public decimal? XJ { get; set; }

        /// <summary>
        /// 4. Máu toàn phần
        /// </summary>
        /// <returns></returns>
        public decimal? QX { get; set; }

        /// <summary>
        /// 5. Truyền lại máu tự thân
        /// </summary>
        /// <returns></returns>
        public decimal? ZTXHS { get; set; }

        /// <summary>
        /// 6. Albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BDB { get; set; }

        /// <summary>
        /// 7. Tủa lạnh
        /// </summary>
        /// <returns></returns>
        public decimal? LCD { get; set; }

        /// <summary>
        /// 8. Khác
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
        /// Thời hạn tái khám _ tuần
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXZ { get; set; }

        /// <summary>
        /// Thời hạn tái khám _ tháng
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXY { get; set; }

        /// <summary>
        /// Thời hạn tái khám _ năm
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXN { get; set; }

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
        /// Bác sĩ điều trị chính
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
		/// Bác sĩ điều trị chính
		/// </summary>
		/// <returns></returns>
		public string ZZYS1 { get; set; }

		/// <summary>
		/// Nhân viên mã hóa
		/// </summary>
		/// <returns></returns>
		public string BMY { get; set; }

        /// <summary>
        /// Mã: Chất lượng hồ sơ bệnh án
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
        public string ZKRQ { get; set; }

        /// <summary>
        /// Hình thức rời viện
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }

        /// <summary>
        /// Y lệnh chuyển viện, tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string YZZY_YLJG { get; set; }

        /// <summary>
        /// Y lệnh chuyển đến trạm y tế cộng đồng/trạm y tế xã, tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string WSY_YLJG { get; set; }

        /// <summary>
        /// Có kế hoạch tái nhập viện và phẫu thuật trong 31 ngày sau ra viện hay không
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }

        /// <summary>
        /// Mục đích
        /// </summary>
        /// <returns></returns>
        public string MD { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi vào viện của bệnh nhân chấn thương sọ não: ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi vào viện của bệnh nhân chấn thương sọ não: giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi vào viện của bệnh nhân chấn thương sọ não: phút
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_F { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi vào viện của bệnh nhân chấn thương sọ não: ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi vào viện của bệnh nhân chấn thương sọ não: giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi vào viện của bệnh nhân chấn thương sọ não: phút
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
        /// Quản lý lộ trình lâm sàng
        /// </summary>
        public string LCLJGL { get; set; }
        /// <summary>
        /// Quản lý loại bệnh
        /// </summary>
        public string LCBZGL { get; set; }
        /// <summary>
        /// Phân loại quản lý loại bệnh
        /// </summary>
        public string BZGLFL { get; set; }
        /// <summary>
        /// Phân loại tình trạng bệnh: 1 Nguy kịch, 2 Nặng, 3 Khó, 4 Cấp cứu, 5 Thông thường
        /// </summary>
        public string BQFX { get; set; }

        public List<MrbasyzdEntity> zdlist { get; set; }
        public List<MrbasyssEntity> oplist { get; set; }
        /// <summary>
        /// Số giường lúc vào viện
        /// </summary>
        public string RYCH { get; set; }
        /// <summary>
        /// Số giường lúc ra viện
        /// </summary>
        public string CYCH { get; set; }
        /// <summary>
        /// Nơi sinh - tỉnh
        /// </summary>
        public string CSD_SN { get; set; }
        /// <summary>
        /// Nơi sinh - thành phố
        /// </summary>
        public string CSD_SI { get; set; }
        /// <summary>
        /// Nơi sinh - quận/huyện
        /// </summary>
        public string CSD_QX { get; set; }
        /// <summary>
        /// Nơi sinh - đường phố
        /// </summary>
        public string CSD_JD { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại - tỉnh
        /// </summary>
        public string XZZ_SN { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại - thành phố
        /// </summary>
        public string XZZ_SI { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại - quận/huyện
        /// </summary>
        public string XZZ_QX { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại - đường phố
        /// </summary>
        public string XZZ_JD { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu - thành phố
        /// </summary>
        public string HKDZ_SN { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu -
        /// </summary>
        public string HKDZ_SI { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu -
        /// </summary>
        public string HKDZ_QX { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu -
        /// </summary>
        public string HKDZ_JD { get; set; }
        /// <summary>
        /// Người liên hệ
        /// </summary>
        public string LXRDZ_SN { get; set; }
        /// <summary>
        /// Người liên hệ
        /// </summary>
        public string LXRDZ_SI { get; set; }
        /// <summary>
        /// Người liên hệ
        /// </summary>
        public string LXRDZ_QX { get; set; }
        /// <summary>
        /// Người liên hệ
        /// </summary>
        public string LXRDZ_JD { get; set; }
        /// <summary>
        /// Trạng thái hồ sơ bệnh án Enumbazt
        /// </summary>
        public string bazt { get; set; }


        public decimal? ZFY { get; set; }
        /// <summary>
        /// Số tiền tự trả
        /// </summary>
        public decimal? ZFJE { get; set; }
        /// <summary>
        /// Phí dịch vụ y tế thông thường
        /// </summary>
        public decimal? YLFUF { get; set; }
        /// <summary>
        /// Phí thủ thuật điều trị thông thường
        /// </summary>
        public decimal? ZLCZF { get; set; }
        /// <summary>
        /// Phí điều dưỡng
        /// </summary>
        public decimal? HLF { get; set; }
        /// <summary>
        /// Chi phí khác - loại dịch vụ y tế tổng hợp
        /// </summary>
        public decimal? QTFY { get; set; }
        /// <summary>
        /// Phí chẩn đoán bệnh lý
        /// </summary>
        public decimal? BLZDF { get; set; }
        /// <summary>
        /// Phí chẩn đoán xét nghiệm
        /// </summary>
        public decimal? SYSZDF { get; set; }
        /// <summary>
        /// Phí chẩn đoán hình ảnh
        /// </summary>
        public decimal? YXXZDF { get; set; }
        /// <summary>
        /// Phí hạng mục chẩn đoán lâm sàng
        /// </summary>
        public decimal? LCZDXMF { get; set; }
        /// <summary>
        /// Phí hạng mục điều trị không phẫu thuật
        /// </summary>
        public decimal? FSSZLXMF { get; set; }
        /// <summary>
        /// Phí vật lý trị liệu lâm sàng
        /// </summary>
        public decimal? WLZLF { get; set; }
        /// <summary>
        /// Phí điều trị phẫu thuật
        /// </summary>
        public decimal? SSZLF { get; set; }
        /// <summary>
        /// Phí gây mê
        /// </summary>
        public decimal? MAF { get; set; }
        /// <summary>
        /// Phí phẫu thuật
        /// </summary>
        public decimal? SSF { get; set; }
        /// <summary>
        /// Phí phục hồi chức năng
        /// </summary>
        public decimal? KFF { get; set; }
        /// <summary>
        /// Phí điều trị Đông y
        /// </summary>
        public decimal? ZYZLF { get; set; }
        /// <summary>
        /// Phí thuốc Tây
        /// </summary>
        public decimal? XYF { get; set; }
        /// <summary>
        /// Chi phí thuốc kháng khuẩn
        /// </summary>
        public decimal? KJYWF { get; set; }
        /// <summary>
        /// Phí thuốc thành phẩm Đông y
        /// </summary>
        public decimal? ZCYF { get; set; }
        /// <summary>
        /// Phí thảo dược Đông y
        /// </summary>
        public decimal? ZCYF1 { get; set; }
        /// <summary>
        /// Phí máu
        /// </summary>
        public decimal? XF { get; set; }
        /// <summary>
        /// Phí chế phẩm albumin
        /// </summary>
        public decimal? BDBLZPF { get; set; }
        /// <summary>
        /// Phí chế phẩm globulin
        /// </summary>
        public decimal? QDBLZPF { get; set; }
        /// <summary>
        /// Phí chế phẩm yếu tố đông máu
        /// </summary>
        public decimal? NXYZLZPF { get; set; }
        /// <summary>
        /// Phí chế phẩm cytokine
        /// </summary>
        public decimal? XBYZLZPF { get; set; }
        /// <summary>
        /// Phí vật tư y tế dùng một lần cho kiểm tra
        /// </summary>
        public decimal? HCYYCLF { get; set; }
        /// <summary>
        /// Phí vật tư y tế dùng một lần cho điều trị
        /// </summary>
        public decimal? YYCLF { get; set; }
        /// <summary>
        /// Phí vật tư y tế dùng một lần cho phẫu thuật
        /// </summary>
        public decimal? YCXYYCLF { get; set; }
        /// <summary>
        /// Phí khác
        /// </summary>
        public decimal? QTF { get; set; }


        /// <summary>
        /// Phí biện chứng luận trị Đông y
        /// </summary>
        public decimal? BZLZF { get; set; }
        /// <summary>
        /// Phí hội chẩn biện chứng luận trị Đông y
        /// </summary>
        public decimal? ZYBLZHZF { get; set; }
        /// <summary>
        /// Điều trị Đông y
        /// </summary>
        public decimal? ZYZL { get; set; }
        /// <summary>
        /// Điều trị ngoài Đông y
        /// </summary>
        public decimal? ZYWZ { get; set; }
        /// <summary>
        /// Chỉnh hình Đông y
        /// </summary>
        public decimal? ZYGS { get; set; }
        /// <summary>
        /// Châm cứu và cứu ngải
        /// </summary>
        public decimal? ZCYJF { get; set; }
        /// <summary>
        /// Điều trị xoa bóp Đông y
        /// </summary>
        public decimal? ZYTNZL { get; set; }
        /// <summary>
        /// Điều trị hậu môn - trực tràng Đông y
        /// </summary>
        public decimal? ZYGCZL { get; set; }
        /// <summary>
        /// Điều trị đặc biệt Đông y
        /// </summary>
        public decimal? ZYTSZL { get; set; }
        /// <summary>
        /// Đông y khác
        /// </summary>
        public decimal? ZYQT { get; set; }
        /// <summary>
        /// Pha chế gia công đặc biệt Đông y
        /// </summary>
        public decimal? ZYTSTPJG { get; set; }
        /// <summary>
        /// Ăn uống theo biện chứng
        /// </summary>
        public decimal? BZSS { get; set; }
        /// <summary>
        /// Phí chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        public decimal? ZYZJF { get; set; }

        /// <summary>
        /// Kiểm tra CT
        /// </summary>
        public string CT { get; set; }

        /// <summary>
        /// Kiểm tra PET-CT
        /// </summary>
        public string PETCT { get; set; }

        /// <summary>
        /// Kiểm tra CT hai nguồn
        /// </summary>
        public string SYCT { get; set; }

        /// <summary>
        /// Kiểm tra siêu âm B
        /// </summary>
        public string BC { get; set; }

        /// <summary>
        /// Kiểm tra phim X-quang
        /// </summary>
        public string XP { get; set; }

        /// <summary>
        /// Kiểm tra siêu âm tim
        /// </summary>
        public string CSXDT { get; set; }

        /// <summary>
        /// Kiểm tra MRI
        /// </summary>
        public string MRI { get; set; }

        /// <summary>
        /// Kiểm tra đồng vị
        /// </summary>
        public string TWSJC { get; set; }

        /// <summary>
        /// Thời gian dùng kháng sinh dự phòng cho vết mổ phẫu thuật loại I
        /// </summary>
        public decimal? SYCXSJ { get; set; }

        /// <summary>
        /// Dùng thuốc phối hợp
        /// </summary>
        public string LHYY { get; set; }

        /*Trang phụ*/
        /// <summary>
        /// Tình hình nhiễm trùng bệnh viện
        /// </summary>
        public string YYGRQK { get; set; }
        /// <summary>
        /// Nhiễm trùng bệnh viện có liên quan đến phẫu thuật hay không
        /// </summary>
        public string YYGRSSXG { get; set; }
        /// <summary>
        /// Nhiễm trùng bệnh viện có liên quan đến thủ thuật xâm lấn hay không
        /// </summary>
        public string YYGRSFQRXG { get; set; }
        /// <summary>
        /// Tình hình sử dụng thuốc kháng khuẩn
        /// </summary>
        public string KJYWSYQK { get; set; }
        /// <summary>
        /// Tên thuốc kháng khuẩn 1
        /// </summary>
        public string KJYWMC1 { get; set; }
        /// <summary>
        /// Tên thuốc kháng khuẩn 2
        /// </summary>
        public string KJYWMC2 { get; set; }
        /// <summary>
        /// Tên thuốc kháng khuẩn 3
        /// </summary>
        public string KJYWMC3 { get; set; }
        /// <summary>
        /// Tên thuốc kháng khuẩn 4
        /// </summary>
        public string KJYWMC4 { get; set; }
        /// <summary>
        /// Tên thuốc kháng khuẩn 5
        /// </summary>
        public string KJYWMC5 { get; set; }
        /// <summary>
        /// Tên thuốc kháng khuẩn 6
        /// </summary>
        public string KJYWMC6 { get; set; }

        /// <summary>
        /// Có xảy ra loét do tì đè hay không
        /// </summary>
        public string SFFSYC { get; set; }
        /// <summary>
        /// Có xảy ra trong thời gian nằm viện hay không
        /// </summary>
        public string SFZYQJFS { get; set; }
        /// <summary>
        /// Phân giai đoạn loét do tì đè
        /// </summary>
        public string YCFQ { get; set; }
        /// <summary>
        /// Phản ứng truyền dịch
        /// </summary>
        public string SYFY { get; set; }
        /// <summary>
        /// Thuốc gây ra phản ứng
        /// </summary>
        public string YFFYDYW { get; set; }
        /// <summary>
        /// Biểu hiện lâm sàng khi truyền dịch
        /// </summary>
        public string SYLCBX { get; set; }
        /// <summary>
        /// Trong thời gian nằm viện có xảy ra ngã hoặc rơi khỏi giường hay không
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
        /// Hạn chế cơ thể trong thời gian nằm viện
        /// </summary>
        public string ZYQJSTYY { get; set; }
        /// <summary>
        /// Giá trị nitơ urê khi rời viện sau lọc máu (thẩm tách máu, thẩm tách phúc mạc)
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
        /// Người kê khai
        /// </summary>
        public string TBR { get; set; }
        /// <summary>
        /// Số điện thoại liên hệ
        /// </summary>
        public string LXDH { get; set; }
        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string SJ { get; set; }
        /// <summary>
        /// Ngày báo cáo
        /// </summary>
        public string BCRQ { get; set; }

        //Trường không thuộc trang đầu hồ sơ bệnh án
        public int? cyts { get; set; }

		/// <summary>
		/// Tuổi (đối với người dưới 1 tháng tuổi)
		/// </summary>
		public int? BZYYSNL { get; set; }

		/// <summary>
		/// Chuyển đến từ cơ sở y tế khác
		/// </summary>
		public string QTYLJGZR { get; set; }
        /// <summary>
        /// Thực hiện lộ trình lâm sàng
        /// </summary>
        public string ZYSSLCLJ { get; set; }
        /// <summary>
        /// Sử dụng chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        public string SYYLJGZYZJ { get; set; }
        /// <summary>
        /// Sử dụng thiết bị chẩn trị Đông y
        /// </summary>
        public string SYZYZLSB { get; set; }
        /// <summary>
        /// Sử dụng kỹ thuật chẩn trị Đông y
        /// </summary>
        public string SYZYZLJS { get; set; }
        /// <summary>
        /// Điều dưỡng theo biện chứng
        /// </summary>
        public string BZSH { get; set; }

    }
}
