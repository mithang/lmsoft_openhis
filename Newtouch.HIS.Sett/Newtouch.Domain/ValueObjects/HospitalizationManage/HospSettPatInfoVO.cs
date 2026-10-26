using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Quyết toán xuất viện Lấy thông tin cơ bản bệnh nhân(Thông tin cơ bản,Tài khoản,Khoa v.v.)
    /// </summary>
    public class HospSettPatInfoVO
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// Cờ trạng thái
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Tính chất bệnh nhân
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// Tên tính chất bệnh nhân
        /// </summary>
        public string brxzmc { get; set; }

        /// <summary>
        /// Loại giao dịch BHYT của bệnh nhân
        /// </summary>
        public string ybjylx { get; set; }

        /// <summary>
        /// Ngày nhập viện
        /// </summary>
        public DateTime? ryrq { get; set; }

        /// <summary>
        /// Ngày xuất viện
        /// </summary>
        public DateTime? cyrq { get; set; }

        /// <summary>
        /// Số chứng từ
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// patid,dùngxt_brjbxxGán giá trị cho bảng
        /// </summary>
        public int? patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Tiền tạm ứng nội trú Tài khoản
        /// </summary>
        public int? zyyjjzh { get; set; }

        /// <summary>
        /// Tiền tạm ứng nội trú Mã tài khoản
        /// </summary>
        public int? zyyjjzhbh { get; set; }

        /// <summary>
        /// Tiền tạm ứng nội trú Tài khoản Tính chất tài khoản
        /// </summary>
        public string zyyjjzhzhxz { get; set; }

        /// <summary>
        /// Chẩn đoán nhập viện Mã chẩn đoán
        /// </summary>
        public string ryzdCode { get; set; }

        /// <summary>
        /// Chẩn đoán nhập viện icd10
        /// </summary>
        public string ryzdicd10 { get; set; }

        /// <summary>
        /// Số ngày nội trú
        /// </summary>
        public decimal? zyts { get; set; }

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

    }
}
