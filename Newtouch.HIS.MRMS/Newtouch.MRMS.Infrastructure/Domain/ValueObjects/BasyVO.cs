using Newtouch.MR.ManageSystem.Domain.DTO;
using Newtouch.MR.ManageSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace Newtouch.MR.ManageSystem.Domain.ValueObjects
{
    /// <summary>
    /// Khung nhìn tổng hợp trang đầu bệnh án
    /// </summary>
    public class BasyVO: BasyRelVO
    {
        
        public string Id { get; set; }

        /// <summary>
        /// Tên cơ quan
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
        /// Định dạng là: YYYYMMDD, ví dụ: 20131125
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
        /// Cân nặng lúc sinh của trẻ sơ sinh(gam)
        /// </summary>
        /// <returns></returns>
        public decimal? XSECSTZ { get; set; }

        /// <summary>
        /// Cân nặng lúc nhập viện của trẻ sơ sinh(gam)
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
        public string RYSJ { get; set; }

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
        /// Khoa chuyển
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }

        /// <summary>
        /// Định dạng là: YYYYMMDD, ví dụ: 20131125
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
        /// Thực hiện phác đồ lâm sàng
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
        public string QZRQ { get; set; }

        /// <summary>
        /// Có phải phẫu thuật theo lịch hẹn không
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
        /// Tình trạng lúc nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYBQ { get; set; }

        /// <summary>
        /// Tình trạng xuất viện1Khỏi bệnh2Đỡ3Chưa khỏi4Tử vong5Khác
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
        /// Chẩn đoán giải phẫu bệnh
        /// </summary>
        /// <returns></returns>
        public string BLZD { get; set; }

        /// <summary>
        /// Mã bệnh
        /// </summary>
        /// <returns></returns>
        public string BLZDDM { get; set; }

        /// <summary>
        /// Số giải phẫu bệnh
        /// </summary>
        /// <returns></returns>
        public string BLH { get; set; }

        /// <summary>
        /// Dị ứng thuốc
        /// </summary>
        /// <returns></returns>
        public string YWGM { get; set; }

        /// <summary>
        /// Bệnh thuốc dị ứng
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

        /// <summary>
        /// Trưởng khoa
        /// </summary>
        /// <returns></returns>
        public string KZR { get; set; }

        /// <summary>
        /// Trưởng(Phó trưởng)Bác sĩ
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
        /// Điều dưỡng chịu trách nhiệm
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
        public string ZKRQ { get; set; }

        /// <summary>
        /// Cách rời viện
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }

        /// <summary>
        /// Y lệnh chuyển viện, Tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string YZZY_YLJG { get; set; }

        /// <summary>
        /// Y lệnh chuyển đến cơ sở y tế cộng đồng/Trạm y tế xã, Tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string WSY_YLJG { get; set; }

        /// <summary>
        /// Có kế hoạch phẫu thuật tái nhập viện trong 31 ngày sau xuất viện không
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }

        /// <summary>
        /// Mục đích
        /// </summary>
        /// <returns></returns>
        public string MD { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não: ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não: giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não: phút
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_F { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não: ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_T { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não: giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_XS { get; set; }

        /// <summary>
        /// Thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não: phút
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
        /// Quản lý phác đồ lâm sàng
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
        /// Phân loại tình trạng bệnh 1 Nguy kịch 2 Bệnh nặng 3 Khó 4 Cấp cứu 5 Thông thường
        /// </summary>
        public string BQFX { get; set; }

        public List<MrbasyzdEntity> zdlist { get; set; }
        public List<MrbasyssEntity> oplist { get; set; }
        /// <summary>
        /// Số giường nhập viện
        /// </summary>
        public string RYCH { get; set; }
        /// <summary>
        /// Số giường xuất viện
        /// </summary>
        public string CYCH { get; set; }
        /// <summary>
        /// Nơi sinh-tỉnh
        /// </summary>
        public string CSD_SN { get; set; }
        /// <summary>
        /// Nơi sinh-thành phố
        /// </summary>
        public string CSD_SI { get; set; }
        /// <summary>
        /// Nơi sinh-Quận/huyện
        /// </summary>
        public string CSD_QX { get; set; }
        /// <summary>
        /// Nơi sinh-Đường phố
        /// </summary>
        public string CSD_JD { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại-tỉnh
        /// </summary>
        public string XZZ_SN { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại-thành phố
        /// </summary>
        public string XZZ_SI { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại-Quận/huyện
        /// </summary>
        public string XZZ_QX { get; set; }
        /// <summary>
        /// Địa chỉ hiện tại-Đường phố
        /// </summary>
        public string XZZ_JD { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu-thành phố
        /// </summary>
        public string HKDZ_SN { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu-
        /// </summary>
        public string HKDZ_SI { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu-
        /// </summary>
        public string HKDZ_QX { get; set; }
        /// <summary>
        /// Nơi đăng ký hộ khẩu-
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
        /// Trạng thái bệnh án Enumbazt
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
        /// Chi phí khác-Dịch vụ y tế tổng hợp
        /// </summary>
        public decimal? QTFY { get; set; }
        /// <summary>
        /// Phí chẩn đoán giải phẫu bệnh
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
        /// Phí điều trị YHCT
        /// </summary>
        public decimal? ZYZLF { get; set; }
        /// <summary>
        /// Phí thuốc Tây
        /// </summary>
        public decimal? XYF { get; set; }
        /// <summary>
        /// Phí thuốc kháng khuẩn
        /// </summary>
        public decimal? KJYWF { get; set; }
        /// <summary>
        /// Phí thuốc thành phẩm YHCT
        /// </summary>
        public decimal? ZCYF { get; set; }
        /// <summary>
        /// Phí thảo dược
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
        /// Phí biện chứng luận trị YHCT
        /// </summary>
        public decimal? BZLZF { get; set; }
        /// <summary>
        /// Phí hội chẩn biện chứng luận trị YHCT
        /// </summary>
        public decimal? ZYBLZHZF { get; set; }
        /// <summary>
        /// Điều trị YHCT
        /// </summary>
        public decimal? ZYZL { get; set; }
        /// <summary>
        /// Điều trị ngoài YHCT
        /// </summary>
        public decimal? ZYWZ { get; set; }
        /// <summary>
        /// Cốt thương YHCT
        /// </summary>
        public decimal? ZYGS { get; set; }
        /// <summary>
        /// Châm cứu và cứu ngải
        /// </summary>
        public decimal? ZCYJF { get; set; }
        /// <summary>
        /// Điều trị xoa bóp bấm huyệt YHCT
        /// </summary>
        public decimal? ZYTNZL { get; set; }
        /// <summary>
        /// Điều trị hậu môn trực tràng YHCT
        /// </summary>
        public decimal? ZYGCZL { get; set; }
        /// <summary>
        /// Điều trị đặc biệt YHCT
        /// </summary>
        public decimal? ZYTSZL { get; set; }
        /// <summary>
        /// YHCT khác
        /// </summary>
        public decimal? ZYQT { get; set; }
        /// <summary>
        /// Pha chế đặc biệt YHCT
        /// </summary>
        public decimal? ZYTSTPJG { get; set; }
        /// <summary>
        /// Ăn uống theo biện chứng
        /// </summary>
        public decimal? BZSS { get; set; }
        /// <summary>
        /// Phí chế phẩm thuốc YHCT của cơ sở y tế
        /// </summary>
        public decimal? ZYZJF { get; set; }


        //Trường không thuộc trang đầu bệnh án
        public int? cyts { get; set; }

        /// <summary>
        /// Ngày lưu trữ
        /// </summary>
        public string  GDRQ { get; set; }

        /// <summary>
        /// Trạng thái ký nhận
        /// </summary>
        public string RecordStu { get; set; }

        public string CommitTime { get; set; }

    }
}
