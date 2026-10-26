using System;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class InpatientSettPatInfoVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string  zyh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zybz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int patId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zjh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? brxzbh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybjylx { get; set; }

        public string brxzlb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  brxzmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ryrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? zyyjjzh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Số ngày nội trú
        /// </summary>
        public decimal? zyts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// Năm tháng sinh
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// Tuổi
        /// </summary>
        public int? nl
        {
            get
            {
                if (csny.HasValue)
                {
                    return (DateTime.Now.Year - csny.Value.Year) + 1;
                }
                return null;
            }
        }

        /***** Hoàn phí nội trúadd *****/
        /// <summary>
        /// Chẩn đoán nhập viện
        /// </summary>
        public string ryzdmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string ryzdicd10 { get; set; }
        /// <summary>
        /// Chẩn đoán xuất viện
        /// </summary>
        public string cyzd { get; set; }
        /// <summary>
        /// Chẩn đoán xuất việnICD10
        /// </summary>
        public string cyzdicd10 { get; set; }
        /// <summary>
        /// Giường bệnh
        /// </summary>
        public string cw { get; set; }

        public string kh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CardType { get; set; }

        public string CardTypeName { get; set;}

		/// <summary>
		/// Số dư tài khoản
		/// </summary>
		public decimal? zhye { get; set; }
        /// <summary>
        /// Loại tham gia bảo hiểm Để phân biệt BHYT Thượng Hải và BHYT quốc gia
        /// </summary>
        public string cblb { get; set; }
    }
}
