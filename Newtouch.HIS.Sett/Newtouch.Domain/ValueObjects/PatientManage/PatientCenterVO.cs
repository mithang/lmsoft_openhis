using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class PatientCenterVO
    {
        public List<PatientBasicAndCardInfoVO> basic { get; set; }
        public List<HosPatientVo> zyinfolist { get; set; }
        public List<OutpatientVO> mzinfolist { get; set; }
    }
    public class HosPatientVo : HospPatientInfoVO
    {
        public string nlshow { get; set; }
        public string cwmc { get; set; }
        /// <summary>
        /// Thứ tự nội trú
        /// </summary>
        public int zycx { get; set; }
        /// <summary>
        /// Khoa nhập viện
        /// </summary>
        public string ryks { get; set; }
        public string ryksmc { get; set; }
        /// <summary>
        /// Khu bệnh nhập viện
        /// </summary>
        public string ryward { get; set; }
        public string rywardname { get; set; }
        /// <summary>
        /// Khu bệnh hiện tại
        /// </summary>
        public string ward { get; set; }
        public string wardname { get; set; }
        /// <summary>
        /// Bác sĩ điều trị chính
        /// </summary>
        public string zzys { get; set; }
        public string zzysmc { get; set; }
        /// <summary>
        /// Bác sĩ nội trú
        /// </summary>
        public string zyys { get; set; }
        public string zyysmc { get; set; }
        /// <summary>
        /// Bác sĩ trưởng khoa
        /// </summary>
        public string zrys { get; set; }
        public string zrysmc { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string gms { get; set; }
        public string rysj { get; set; }
        public string cqsj { get; set; }
        public string cysj { get; set; }
        public string cyjdrq2 { get; set; }
        /// <summary>
        /// Số dư tài khoản
        /// </summary>
        public decimal? zhye { get; set; }
        public decimal? zfy { get; set; }
        /// <summary>
        /// Chi phí chờ nộp
        /// </summary>
        public decimal? djfy { get; set; }
        /// <summary>
        /// Chi phí đã quyết toán
        /// </summary>
        public decimal? yjfy { get; set; }
        public string cyzddm { get; set; }
        public string cyzdmc { get; set; }
    }


    /// <summary>
    /// Khung nhìn bệnh nhân ngoại trú
    /// </summary>
    public class OutpatientVO
    {
        /// <summary>
		/// Số khám ngoại trú
		/// </summary>
		public string mzh { get; set; }
        
        public int patid { get; set; }
        public string brxz { get; set; }
        public int? ghnm { get; set; }
        /// <summary>
        /// Nguồn đăng ký khám
        /// </summary>
        public string ghly { get; set; }
        public string mjzbz { get; set; }
        public string ks { get; set; }
        public string ys { get; set; }
        public string zdicd10 { get; set; }
        public string zdmc { get; set; }
        public string nlshow { get; set; }

        public DateTime? ghrq { get; set; }

        /// <summary>
        /// Mã nội bộ quyết toán
        /// </summary>
        public int? jsnm { get; set; }
        /// <summary>
        /// Ngày thu phí
        /// </summary>
        public DateTime? sfrq { get; set; }
        /// <summary>
        /// Nhân viên quyết toán
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian quyết toán
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Tên nhân viên quyết toán
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// Đăng ký khám ngoại trú hoặc Ghi sổ thu phí ngoại trú
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Quyết toán BHYT Số quyết toán BHYT
        /// </summary>
        public string ybjsh { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal jszje { get; set; }

        /// <summary>
        /// Thanh toán quyết toán
        /// </summary>
        public decimal jsxjzf { get; set; }
        /// <summary>
        /// Tên phương thức thanh toán tiền mặt
        /// </summary>
        public string xjzffsmc { get; set; }

        public string jzid { get; set; }
        public string pch { get; set; }
        public string yllb { get; set; }
        public string bzbm { get; set; }
    }
}
