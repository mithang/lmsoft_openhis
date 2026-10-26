using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Domain.Entity.EMR
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.MrmsDb)]
    [SugarTable("mr_basy", "")]
    public partial class MedicalHomeRecordEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// số thẻ sức khỏe
        /// </summary>
        /// <returns></returns>
        public string JKKH { get; set; }
        /// <summary>
        /// số lần nội trú
        /// </summary>
        /// <returns></returns>
        public string ZYCS { get; set; }
        /// <summary>
        /// số nội trú
        /// </summary>
        public string ZYH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "BAH不可为空")]
        [StringLength(100, ErrorMessage = "BAH长度限制为100")]
        public string BAH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "YLFKFS长度限制为100")]
        public string YLFKFS { get; set; }
        /// <summary>
        /// PATID
        /// </summary>
        /// <returns></returns>
        public string PATID { get; set; }

        /// <summary>
        /// họ tên
        /// </summary>
        /// <returns></returns>
        public string XM { get; set; }
        /// <summary>
        /// số chứng minh nhân dân
        /// </summary>
        /// <returns></returns>
        public string SFZH { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "XB长度限制为100")]
        public string XB { get; set; }

        /// <summary>
        /// định dạng là：YYYYMMDD，Ví dụ：20131125
        /// </summary>
        /// <returns></returns>
        public DateTime CSRQ { get; set; }
        /// <summary>
        /// tuổi
        /// </summary>
        /// <returns></returns>
        public decimal NL { get; set; }        
        /// <summary>
        /// tuổi trẻ sơ sinh
        /// </summary>
        /// <returns></returns>
        public decimal? BZYZSNL { get; set; }

        /// <summary>
        /// cân nặng khi sinh của trẻ sơ sinh(gam)
        /// </summary>
        /// <returns></returns>
        public decimal? XSECSTZ { get; set; }

        /// <summary>
        /// cân nặng khi nhập viện của trẻ sơ sinh(gam）
        /// </summary>
        /// <returns></returns>
        public decimal? XSERYTZ { get; set; }
        /// <summary>
        /// địa chỉ hiện tại
        /// </summary>
        /// <returns></returns>
        public string XZZ { get; set; }

        /// <summary>
        /// điện thoại
        /// </summary>
        /// <returns></returns>
        public string DH { get; set; }

        /// <summary>
        /// mã bưu điện
        /// </summary>
        /// <returns></returns>
        public string XZZYB { get; set; }
        /// <summary>
        /// địa chỉ hộ khẩu
        /// </summary>
        /// <returns></returns>
        public string HKDZ { get; set; }

        /// <summary>
        /// mã bưu điện
        /// </summary>
        /// <returns></returns>
        public string HKDYB { get; set; }

        /// <summary>
        /// đơn vị công tác và địa chỉ
        /// </summary>
        /// <returns></returns>
        public string GZDWJDZ { get; set; }

        /// <summary>
        /// điện thoại đơn vị
        /// </summary>
        /// <returns></returns>
        public string DWDH { get; set; }

        /// <summary>
        /// mã bưu điện
        /// </summary>
        /// <returns></returns>
        public string DWYB { get; set; }

        /// <summary>
        /// họ tên người liên hệ
        /// </summary>
        /// <returns></returns>
        public string LXRXM { get; set; }

        /// <summary>
        /// quan hệ
        /// </summary>
        /// <returns></returns>
        public string GX { get; set; }

        /// <summary>
        /// địa chỉ
        /// </summary>
        /// <returns></returns>
        public string LXRDZ { get; set; }

        /// <summary>
        /// điện thoại
        /// </summary>
        /// <returns></returns>
        public string LXRDH { get; set; }

        /// <summary>
        /// đường nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYTJ { get; set; }

        /// <summary>
        /// thời gian nhập viện
        /// </summary>
        /// <returns></returns>
        public DateTime RYSJ { get; set; }

        /// <summary>
        /// khi
        /// </summary>
        /// <returns></returns>
        public decimal? RYSJS { get; set; }

        /// <summary>
        /// khoa nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYKB { get; set; }

        /// <summary>
        /// phòng bệnh nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYBF { get; set; }

        /// <summary>
        /// khoa chuyển
        /// </summary>
        /// <returns></returns>
        public string ZKKB { get; set; }

        /// <summary>
        /// định dạng là：YYYYMMDD，Ví dụ：20131125
        /// </summary>
        /// <returns></returns>
        public DateTime? CYSJ { get; set; }

        /// <summary>
        /// khi
        /// </summary>
        /// <returns></returns>
        public decimal? CYSJS { get; set; }

        /// <summary>
        /// mã：mã khoa
        /// </summary>
        /// <returns></returns>
        public string CYKB { get; set; }

        /// <summary>
        /// phòng bệnh xuất viện
        /// </summary>
        /// <returns></returns>
        public string CYBF { get; set; }

        /// <summary>
        /// nội trú thực tế(ngày)
        /// </summary>
        /// <returns></returns>
        public string SJZYTS { get; set; }

        /// <summary>
        /// ngoại trú(cấp cứu)chẩn đoán khám bệnh
        /// </summary>
        /// <returns></returns>
        public string MZZD { get; set; }

        /// <summary>
        /// mã bệnh
        /// </summary>
        /// <returns></returns>
        public string MZZDDM { get; set; }

        /// <summary>
        /// có nguy kịch và bệnh nặng hay không
        /// </summary>
        /// <returns></returns>
        public string BWHBZ { get; set; }

        /// <summary>
        /// chẩn đoán nhập viện
        /// </summary>
        /// <returns></returns>
        public string RYZD { get; set; }

        /// <summary>
        /// mã bệnh
        /// </summary>
        /// <returns></returns>
        public string RYZDDM { get; set; }
        /// <summary>
        /// thực hiện lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string SSLCLJ { get; set; }

        /// <summary>
        /// hoàn thành lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string WCLCLJ { get; set; }

        /// <summary>
        /// nguyên nhân rút khỏi lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string TCYY { get; set; }

        /// <summary>
        /// lộ trình lâm sàng có biến đổi hay không
        /// </summary>
        /// <returns></returns>
        public string SFBY { get; set; }

        /// <summary>
        /// nguyên nhân biến đổi lộ trình lâm sàng
        /// </summary>
        /// <returns></returns>
        public string BYYY { get; set; }

        /// <summary>
        /// số lần cấp cứu
        /// </summary>
        /// <returns></returns>
        public decimal? QJCS { get; set; }

        /// <summary>
        /// số lần cấp cứu thành công
        /// </summary>
        /// <returns></returns>
        public decimal? QJCGCS { get; set; }

        /// <summary>
        /// ngày chẩn đoán xác định
        /// </summary>
        /// <returns></returns>
        public DateTime? QZRQ { get; set; }

        /// <summary>
        /// có phải phẫu thuật theo lịch hay không
        /// </summary>
        /// <returns></returns>
        public string ZQSS { get; set; }

        /// <summary>
        /// mã：mã nguồn bệnh nhân
        /// </summary>
        /// <returns></returns>
        public string BRLY { get; set; }

        /// <summary>
        /// nguyên nhân bên ngoài gây ngộ độc
        /// </summary>
        /// <returns></returns>
        public string WBYY { get; set; }

        /// <summary>
        /// mã bệnh
        /// </summary>
        /// <returns></returns>
        public string H23 { get; set; }

        /// <summary>
        /// chẩn đoán bệnh lý ra
        /// </summary>
        /// <returns></returns>
        public string BLZD { get; set; }

        /// <summary>
        /// mã bệnh
        /// </summary>
        /// <returns></returns>
        public string BLZDDM { get; set; }

        /// <summary>
        /// số bệnh lý
        /// </summary>
        /// <returns></returns>
        public string BLH { get; set; }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "GJ长度限制为100")]
        public string GJ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MZ长度限制为100")]
        public string MZ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ZY长度限制为100")]
        public string ZY { get; set; }        /// <summary>
        /// nơi sinh
        /// </summary>
        /// <returns></returns>
        public string CSD { get; set; }

        /// <summary>
        /// quê quán
        /// </summary>
        /// <returns></returns>
        public string GG { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "HY长度限制为100")]
        public string HY { get; set; }
        /// <summary>
        /// dị ứng thuốc
        /// </summary>
        /// <returns></returns>
        public string YWGM { get; set; }

        /// <summary>
        /// bệnh dị ứng thuốc
        /// </summary>
        /// <returns></returns>
        public string GMYW { get; set; }

        /// <summary>
        /// khám nghiệm tử thi bệnh nhân đã tử vong
        /// </summary>
        /// <returns></returns>
        public string SWHZSJ { get; set; }

        /// <summary>
        /// nhóm máu
        /// </summary>
        /// <returns></returns>
        public string XX { get; set; }

        /// <summary>
        /// mã：Rh
        /// </summary>
        /// <returns></returns>
        public string RH { get; set; }

        /*loại máu truyền*/
        /// <summary>
        /// 1.hồng cầu
        /// </summary>
        /// <returns></returns>
        public decimal? HXB { get; set; }

        /// <summary>
        /// 2.tiểu cầu
        /// </summary>
        /// <returns></returns>
        public decimal? XXB { get; set; }

        /// <summary>
        /// 3.huyết tương
        /// </summary>
        /// <returns></returns>
        public decimal? XJ { get; set; }

        /// <summary>
        /// 4.máu toàn phần
        /// </summary>
        /// <returns></returns>
        public decimal? QX { get; set; }

        /// <summary>
        /// 5.truyền lại máu tự thân
        /// </summary>
        /// <returns></returns>
        public decimal? ZTXHS { get; set; }

        /// <summary>
        /// 6.albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BDB { get; set; }

        /// <summary>
        /// 7.kết tủa lạnh
        /// </summary>
        /// <returns></returns>
        public decimal? LCD { get; set; }

        /// <summary>
        /// 8.khác
        /// </summary>
        /// <returns></returns>
        public string QT { get; set; }

        /// <summary>
        /// phản ứng truyền máu
        /// </summary>
        /// <returns></returns>
        public string SXFY { get; set; }

        /// <summary>
        /// thời hạn tái khám
        /// </summary>
        /// <returns></returns>
        public string SZ { get; set; }

        /// <summary>
        /// thời hạn tái khám_tuần
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXZ { get; set; }

        /// <summary>
        /// thời hạn tái khám_tháng
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXY { get; set; }

        /// <summary>
        /// thời hạn tái khám_năm
        /// </summary>
        /// <returns></returns>
        public decimal? SZQXN { get; set; }

        /// <summary>
        /// chủ nhiệm khoa
        /// </summary>
        /// <returns></returns>
        public string KZR { get; set; }

        /// <summary>
        /// chủ nhiệm（phó chủ nhiệm）bác sĩ
        /// </summary>
        /// <returns></returns>
        public string ZRYS { get; set; }

        /// <summary>
        /// bác sĩ khám chính
        /// </summary>
        /// <returns></returns>
        public string ZZYS { get; set; }

        /// <summary>
        /// bác sĩ điều trị chính
        /// </summary>
        /// <returns></returns>
        public string ZZYS1 { get; set; }


        /// <summary>
        /// bác sĩ nội trú
        /// </summary>
        /// <returns></returns>
        public string ZYYS { get; set; }

        /// <summary>
        /// y tá chịu trách nhiệm
        /// </summary>
        /// <returns></returns>
        public string ZRHS { get; set; }

        /// <summary>
        /// bác sĩ tu nghiệp
        /// </summary>
        /// <returns></returns>
        public string JXYS { get; set; }

        /// <summary>
        /// bác sĩ thực tập
        /// </summary>
        /// <returns></returns>
        public string SXYS { get; set; }

        /// <summary>
        /// nhân viên mã hóa
        /// </summary>
        /// <returns></returns>
        public string BMY { get; set; }

        /// <summary>
        /// mã：chất lượng bệnh án
        /// </summary>
        /// <returns></returns>
        public string BAZL { get; set; }

        /// <summary>
        /// bác sĩ kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string ZKYS { get; set; }

        /// <summary>
        /// y tá kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public string ZKHS { get; set; }

        /// <summary>
        /// ngày kiểm soát chất lượng
        /// </summary>
        /// <returns></returns>
        public DateTime? ZKRQ { get; set; }

        /// <summary>
        /// phương thức rời viện
        /// </summary>
        /// <returns></returns>
        public string LYFS { get; set; }

        /// <summary>
        /// y lệnh chuyển viện，tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string YZZY_YLJG { get; set; }

        /// <summary>
        /// y lệnh chuyển đến cơ sở dịch vụ y tế cộng đồng/trạm y tế xã，tên cơ sở y tế dự kiến tiếp nhận
        /// </summary>
        /// <returns></returns>
        public string WSY_YLJG { get; set; }

        /// <summary>
        /// có xuất viện hay không31tình hình phẫu thuật theo kế hoạch tái nhập viện trong vòng vài ngày
        /// </summary>
        /// <returns></returns>
        public string SFZZYJH { get; set; }

        /// <summary>
        /// mục đích
        /// </summary>
        /// <returns></returns>
        public string MD { get; set; }

        /// <summary>
        /// thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não：ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_T { get; set; }

        /// <summary>
        /// thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não：giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_XS { get; set; }

        /// <summary>
        /// thời gian hôn mê trước khi nhập viện của bệnh nhân chấn thương sọ não：phút
        /// </summary>
        /// <returns></returns>
        public decimal? RYQ_F { get; set; }

        /// <summary>
        /// thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não：ngày
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_T { get; set; }

        /// <summary>
        /// thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não：giờ
        /// </summary>
        /// <returns></returns>
        public decimal? RYH_XS { get; set; }

        /// <summary>
        /// thời gian hôn mê sau khi nhập viện của bệnh nhân chấn thương sọ não：phút
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
        /// quản lý lộ trình lâm sàng 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string LCLJGL { get; set; }

        /// <summary>
        /// quản lý loại bệnh 0 Không 1 Có
        /// </summary>
        /// <returns></returns>
        public string LCBZGL { get; set; }

        /// <summary>
        /// phân loại quản lý loại bệnh
        /// </summary>
        /// <returns></returns>
        public string BZGLFL { get; set; }

        /// <summary>
        /// phân loại tình trạng bệnh 1 nguy kịch 2 bệnh nặng 3 khó 4 cấp cứu 5 chung
        /// </summary>
        /// <returns></returns>
        public string BQFX { get; set; }

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
        /// trạng thái bệnh án 1 chờ nhập 2 đang nhập 3 đã lưu trữ
        /// </summary>
        /// <returns></returns>
        public string bazt { get; set; }

        /// <summary>
        /// tổng chi phí
        /// </summary>
        /// <returns></returns>
        public decimal? ZFY { get; set; }

        /// <summary>
        /// số tiền tự trả
        /// </summary>
        /// <returns></returns>
        public decimal? ZFJE { get; set; }

        /// <summary>
        /// loại dịch vụ y tế tổng hợp(1)phí dịch vụ y tế thông thường
        /// </summary>
        /// <returns></returns>
        public decimal? YLFUF { get; set; }

        /// <summary>
        /// phí biện chứng luận trị y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? BZLZF { get; set; }

        /// <summary>
        /// phí hội chẩn biện chứng luận trị y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYBLZHZF { get; set; }

        /// <summary>
        /// phí thao tác điều trị thông thường
        /// </summary>
        /// <returns></returns>
        public decimal? ZLCZF { get; set; }

        /// <summary>
        /// phí điều dưỡng
        /// </summary>
        /// <returns></returns>
        public decimal? HLF { get; set; }

        /// <summary>
        /// chi phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? QTFY { get; set; }

        /// <summary>
        /// loại chẩn đoán(5)phí chẩn đoán bệnh lý
        /// </summary>
        /// <returns></returns>
        public decimal? BLZDF { get; set; }

        /// <summary>
        /// phí chẩn đoán phòng thí nghiệm
        /// </summary>
        /// <returns></returns>
        public decimal? SYSZDF { get; set; }

        /// <summary>
        /// phí chẩn đoán hình ảnh
        /// </summary>
        /// <returns></returns>
        public decimal? YXXZDF { get; set; }

        /// <summary>
        /// phí hạng mục chẩn đoán lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? LCZDXMF { get; set; }

        /// <summary>
        /// loại điều trị(9)phí hạng mục điều trị không phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? FSSZLXMF { get; set; }

        /// <summary>
        /// phí vật lý trị liệu lâm sàng
        /// </summary>
        /// <returns></returns>
        public decimal? WLZLF { get; set; }

        /// <summary>
        /// phí điều trị phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? SSZLF { get; set; }

        /// <summary>
        /// phí gây mê
        /// </summary>
        /// <returns></returns>
        public decimal? MAF { get; set; }

        /// <summary>
        /// phí phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? SSF { get; set; }

        /// <summary>
        /// loại phục hồi chức năng(11)phí phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public decimal? KFF { get; set; }

        /// <summary>
        /// loại y học cổ truyền(dịch vụ y tế y học cổ truyền và y học dân tộc)（12）chẩn đoán y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZLF { get; set; }

        /// <summary>
        /// điều trị y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZL { get; set; }

        /// <summary>
        /// điều trị bên ngoài bằng y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYWZ { get; set; }

        /// <summary>
        /// chấn thương xương y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYGS { get; set; }

        /// <summary>
        /// châm cứu và cứu ngải
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYJF { get; set; }

        /// <summary>
        /// điều trị xoa bóp y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTNZL { get; set; }

        /// <summary>
        /// điều trị hậu môn – trực tràng bằng y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYGCZL { get; set; }

        /// <summary>
        /// điều trị đặc biệt y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTSZL { get; set; }

        /// <summary>
        /// y học cổ truyền khác
        /// </summary>
        /// <returns></returns>
        public decimal? ZYQT { get; set; }

        /// <summary>
        /// gia công pha chế đặc biệt y học cổ truyền
        /// </summary>
        /// <returns></returns>
        public decimal? ZYTSTPJG { get; set; }

        /// <summary>
        /// ăn uống theo biện chứng
        /// </summary>
        /// <returns></returns>
        public decimal? BZSS { get; set; }

        /// <summary>
        /// loại thuốc Tây y(15)phí thuốc Tây y
        /// </summary>
        /// <returns></returns>
        public decimal? XYF { get; set; }

        /// <summary>
        /// phí thuốc kháng khuẩn
        /// </summary>
        /// <returns></returns>
        public decimal? KJYWF { get; set; }

        /// <summary>
        /// loại thuốc Đông y(16)phí thuốc thành phẩm Đông y
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF { get; set; }

        /// <summary>
        /// phí chế phẩm thuốc Đông y của cơ sở y tế
        /// </summary>
        /// <returns></returns>
        public decimal? ZYZJF { get; set; }

        /// <summary>
        /// phí thảo dược
        /// </summary>
        /// <returns></returns>
        public decimal? ZCYF1 { get; set; }

        /// <summary>
        /// loại máu và chế phẩm máu(18)phí máu
        /// </summary>
        /// <returns></returns>
        public decimal? XF { get; set; }

        /// <summary>
        /// phí chế phẩm albumin
        /// </summary>
        /// <returns></returns>
        public decimal? BDBLZPF { get; set; }

        /// <summary>
        /// phí chế phẩm globulin
        /// </summary>
        /// <returns></returns>
        public decimal? QDBLZPF { get; set; }

        /// <summary>
        /// phí chế phẩm yếu tố đông máu
        /// </summary>
        /// <returns></returns>
        public decimal? NXYZLZPF { get; set; }

        /// <summary>
        /// phí chế phẩm cytokine
        /// </summary>
        /// <returns></returns>
        public decimal? XBYZLZPF { get; set; }

        /// <summary>
        /// loại vật tư tiêu hao(23)phí vật tư y tế dùng một lần cho kiểm tra
        /// </summary>
        /// <returns></returns>
        public decimal? HCYYCLF { get; set; }

        /// <summary>
        /// phí vật tư y tế dùng một lần cho điều trị
        /// </summary>
        /// <returns></returns>
        public decimal? YYCLF { get; set; }

        /// <summary>
        /// phí vật tư y tế dùng một lần cho phẫu thuật
        /// </summary>
        /// <returns></returns>
        public decimal? YCXYYCLF { get; set; }

        /// <summary>
        /// loại khác(26)phí khác
        /// </summary>
        /// <returns></returns>
        public decimal? QTF { get; set; }

        /// <summary>
        /// kiểm traCT
        /// </summary>
        public string CT { get; set; }

        /// <summary>
        /// kiểm traPETCT
        /// </summary>
        public string PETCT { get; set; }

        /// <summary>
        /// kiểm tra hai nguồnCT
        /// </summary>
        public string SYCT { get; set; }

        /// <summary>
        /// kiểm traBsiêu âm
        /// </summary>
        public string BC { get; set; }

        /// <summary>
        /// kiểm traXphim
        /// </summary>
        public string XP { get; set; }

        /// <summary>
        /// kiểm tra siêu âm tim
        /// </summary>
        public string CSXDT { get; set; }

        /// <summary>
        /// kiểm traMRI
        /// </summary>
        public string MRI { get; set; }

        /// <summary>
        /// kiểm tra đồng vị
        /// </summary>
        public string TWSJC { get; set; }

        /// <summary>
        /// Ⅰthời gian sử dụng thuốc kháng khuẩn dự phòng cho vết mổ phẫu thuật loại
        /// </summary>
        public decimal? SYCXSJ { get; set; }

        /// <summary>
        /// sử dụng thuốc phối hợp
        /// </summary>
        public string LHYY { get; set; }

        /*trang phụ*/
        /// <summary>
        /// tình hình nhiễm khuẩn bệnh viện
        /// </summary>
        public string YYGRQK { get; set; }
        /// <summary>
        /// nhiễm khuẩn bệnh viện có liên quan đến phẫu thuật hay không
        /// </summary>
        public string YYGRSSXG { get; set; }
        /// <summary>
        /// nhiễm khuẩn bệnh viện có liên quan đến thao tác xâm lấn hay không
        /// </summary>
        public string YYGRSFQRXG { get; set; }
        /// <summary>
        /// tình hình sử dụng thuốc kháng khuẩn
        /// </summary>
        public string KJYWSYQK { get; set; }
        /// <summary>
        /// tên thuốc kháng khuẩn1
        /// </summary>
        public string KJYWMC1 { get; set; }
        /// <summary>
        /// tên thuốc kháng khuẩn2
        /// </summary>
        public string KJYWMC2 { get; set; }
        /// <summary>
        /// tên thuốc kháng khuẩn3
        /// </summary>
        public string KJYWMC3 { get; set; }
        /// <summary>
        /// tên thuốc kháng khuẩn4
        /// </summary>
        public string KJYWMC4 { get; set; }
        /// <summary>
        /// tên thuốc kháng khuẩn5
        /// </summary>
        public string KJYWMC5 { get; set; }
        /// <summary>
        /// tên thuốc kháng khuẩn6
        /// </summary>
        public string KJYWMC6 { get; set; }

        /// <summary>
        /// có xảy ra loét do tỳ đè hay không
        /// </summary>
        public string SFFSYC { get; set; }
        /// <summary>
        /// có xảy ra trong thời gian nội trú hay không
        /// </summary>
        public string SFZYQJFS { get; set; }
        /// <summary>
        /// phân giai đoạn loét do tỳ đè
        /// </summary>
        public string YCFQ { get; set; }
        /// <summary>
        /// phản ứng truyền dịch
        /// </summary>
        public string SYFY { get; set; }
        /// <summary>
        /// thuốc gây phản ứng
        /// </summary>
        public string YFFYDYW { get; set; }
        /// <summary>
        /// biểu hiện lâm sàng khi truyền dịch
        /// </summary>
        public string SYLCBX { get; set; }
        /// <summary>
        /// trong thời gian nội trú có xảy ra ngã hoặc rơi khỏi giường hay không
        /// </summary>
        public string ZYSFDDHZC { get; set; }
        /// <summary>
        /// mức độ tổn thương do ngã hoặc rơi khỏi giường trong thời gian nội trú
        /// </summary>
        public string ZYDDHZCDCD { get; set; }
        /// <summary>
        /// nguyên nhân ngã hoặc rơi khỏi giường
        /// </summary>
        public string DDHZCDYY { get; set; }
        /// <summary>
        /// hạn chế cơ thể trong thời gian nội trú
        /// </summary>
        public string ZYQJSTYY { get; set; }
        /// <summary>
        /// thẩm tách khi rời viện（thẩm tách máu、thẩm tách phúc mạc）giá trị nitơ urê
        /// </summary>
        public decimal? LYTXNSDZ { get; set; }
        /// <summary>
        /// người phụ trách đơn vị
        /// </summary>
        public string DWFZR { get; set; }
        /// <summary>
        /// người phụ trách thống kê
        /// </summary>
        public string TJFZR { get; set; }
        /// <summary>
        /// người khai báo
        /// </summary>
        public string TBR { get; set; }
        /// <summary>
        /// điện thoại liên hệ
        /// </summary>
        public string LXDH { get; set; }
        /// <summary>
        /// điện thoại di động
        /// </summary>
        public string SJ { get; set; }
        /// <summary>
        /// ngày báo cáo
        /// </summary>
        public string BCRQ { get; set; }

        public int? IsLock { get; set; }

        /// <summary>
        /// (tuổi chưa đủ1người theo tháng)tuổi
        /// </summary>
        public int? BZYYSNL { get; set; }

        /// <summary>
        /// chuyển đến từ cơ sở y tế khác
        /// </summary>
        public string QTYLJGZR { get; set; }

        
    }
}
