using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Bệnh nhân nội trú(Dùng cho giao diện)
    /// </summary>
    public class InPatientInfoDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? nl { get; set; }

        public string nlshow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdicd10 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ryrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqmc { get; set; }
        public string cwmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zybzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wb { get; set; }

        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlx { get; set; }
        /// <summary>
        /// Loại giấy tờ
        /// </summary>
        public string zjlxValue { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Số CMND/CCCD
        /// </summary>
        public string idCardNo { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string sexValue { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }
        /// <summary>
        /// Khoa
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// Bác sĩ
        /// </summary>
        public string ysxm { get; set; }

        //Mức nguy kịch?
        //Cách nhập viện?

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// Loại thẻ
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// Người liên hệ khẩn cấp
        /// </summary>
        public string contPerName { get; set; }
        /// <summary>
        /// Điện thoại người liên hệ khẩn cấp
        /// </summary>
        public string contPerPhoneNum { get; set; }
        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string contPerRel { get; set; }
        /// <summary>
        /// Quan hệ người liên hệ khẩn cấp
        /// </summary>
        public string contPerRelValue { get; set; }

        public string ryfs { get; set; }
        /// <summary>
        /// Đã tải lên hay chưa
        /// </summary>
        public string issc { get; set; }
        public string doctor { get; set; }
        public string inHosDays { get; set; }
        public string zzdmc2 { get; set; }
        public string zzdmc3 { get; set; }
        public string cyzd { get; set; }
        public string zy { get; set; }
        public string xzz { get; set; }
        public string cyfs { get; set; }
        public string jzDoctor { get; set; }
        public string pkbz { get; set; }

    }

    /// <summary>
    /// Bệnh nhân nội trú(Dùng để tra cứu bệnh nhân)
    /// </summary>
    public class InPatientInfoVO : InPatientInfoDTO
    {
        /// <summary>
        /// Số tiền đã quyết toán
        /// </summary>
        public decimal? yjfyje { get; set; }
        /// <summary>
        /// Số tiền chưa quyết toán
        /// </summary>
        public decimal? wjfyje { get; set; }
        /// <summary>
        /// Mã nơi tham gia bảo hiểm
        /// </summary>
        public string cbdbm { get; set; }
        public string jzh { get; set; }

        public string mzmc { get; set; }
        public string rybq { get; set; }
        public string lxrdh { get; set; }
        public decimal? zyfy { get; set; }
        public decimal? yjj { get; set; }
        /// <summary>
        /// Tính chất thẻ:0 Tự chi trả 1:BHYT
        /// </summary>
        public string brxzlb { get; set; }
        /// <summary>
        /// Số giao dịch quyết toán BHYT
        /// </summary>
        public string setl_id { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string psn_no { get; set; }
        public string mdtrt_id { get; set; }
    }

    public class InSettlementInfoVO : InPatientInfoVO
    {
        public string sczt { get; set; }
        public string tjzt { get; set; }
        public string setl_id { get; set; }
        public string psn_no { get; set; }
    }

    public class patFeeVo
    {
        public decimal? je { get; set; }
        public string zyh { get; set; }
    }
}
