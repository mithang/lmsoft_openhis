using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class PatientBasicAndCardInfoVO
    {
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        #region 病人基本信息
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime csny { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// Tuổi
        /// </summary>
        public Int16 nl { get; set; }
        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// Điện thoại
        /// </summary>
        public string yddh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// Khu vực
        /// </summary>
        public string dy { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public byte? hf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlx { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string wechat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zjlxfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cs_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hu_dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_sheng { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_shi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_xian { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xian_dz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string xl { get; set; }

        /// <summary>
        /// xt_gj.gj
        /// </summary>
        public string gj { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string dh { get; set; }
        #endregion
    }
}
