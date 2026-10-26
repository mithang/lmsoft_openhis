using NewtouchHIS.Domain.Entity.EMR;

namespace NewtouchHIS.Domain.InterfaceObjets
{
    /// <summary>
    /// trang đầu bệnh án bệnh nhân
    /// </summary>
    public class MedicalHomeVO: MedicalHomeBaseVO
    {
        /// <summary>
        /// danh sách chẩn đoán
        /// </summary>
        public List<MedicalHomeDiagEntity> DiagList { get; set; }
        /// <summary>
        /// danh sách phẫu thuật
        /// </summary>
        public List<MedicalHomeOperationEntity> OperationList { get; set; }
    }

    public class MedicalHomeBaseVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string YLFKFS { get; set; }
        /// <summary>
        /// số nội trú
        /// </summary>
        public string ZYH { get; set; }

        public string BAH { get; set; }
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
        public string XB { get; set; }
        public string XB_Rel { get; set; }
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
        /// điện thoại
        /// </summary>
        /// <returns></returns>
        public string DH { get; set; }
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
        /// tổng chi phí
        /// </summary>
        /// <returns></returns>
        public decimal? ZFY { get; set; }

        /// <summary>
        /// số tiền tự trả
        /// </summary>
        /// <returns></returns>
        public decimal? ZFJE { get; set; }
    }
}
