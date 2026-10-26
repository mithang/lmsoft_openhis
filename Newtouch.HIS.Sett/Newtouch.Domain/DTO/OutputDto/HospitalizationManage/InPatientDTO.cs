using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage
{
    public class InPatientDTO
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
        //public string zzdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string zzdicd10 { get; set; }

        /// <summary>
        /// Chẩn đoán xuất viện
        /// </summary>
        //public string zzdmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string cyqk { get; set; }
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
        /// Chẩn đoán nhập viện
        /// </summary>
        public IList<ryzd> ryzd { get; set; }
        /// <summary>
        /// Chẩn đoán xuất viện
        /// </summary>
        public IList<cyzd> cyzd { get; set; }
    }

    public class ryzd {
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
        public string zdpx { get; set; }
    }

    public class cyzd
    {
        /// <summary>
        /// 
        /// </summary>
        public string zzdCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zzdmc { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string cyqk { get; set; }
    }
}
