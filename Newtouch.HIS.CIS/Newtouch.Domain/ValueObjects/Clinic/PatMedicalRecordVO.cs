using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class PatMedicalRecordVO : OutpMedicalRecordVO
    {
        public string patid { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string xm { get; set; }
    }

    // </summary> 
    public class OutpMedicalRecordVO
    {
        public string blId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        public string mzh { get; set; }

        /// <summary>
        /// Thông tin đăng ký khám dư thừa  Enum 1 Khám thường ngoại trú 2 Cấp cứu 3Chuyên gia
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        #region 一般检查
        /// <summary>
        /// Cân nặng
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// Thân nhiệt
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// Mạch
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// Cách đo đường huyết Sau bữa ăn Lúc đói Ngẫu nhiên 
        /// EnumXtclfs
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// Đường huyết
        /// </summary>
        public decimal? xuetang { get; set; }
        /// <summary>
        /// Chiều cao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// Huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// Huyết áp tâm trương
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// Hô hấp
        /// </summary>
        public decimal? huxi { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân EnumMarriageStu
        /// </summary>
        public string hy { get; set; }
        #endregion

        /// <summary>
        /// Triệu chứng chính
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// Thời gian phát bệnh
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// Bệnh sử hiện tại
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string jws { get; set; }

        /// <summary>
        /// Khám thực thể
        /// </summary>
        public string ct { get; set; }
        /// <summary>
        /// Xử lý
        /// </summary>
        public string clfa { get; set; }
        /// <summary>
        /// Kiểm tra hỗ trợ
        /// </summary>
        public string fzjc { get; set; }
        /// <summary>
        /// Tiền sử kinh nguyệt
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string gms { get; set; }
        public List<WMDiagnosisVO> xyzd { get; set; }
        public List<TCMDiagnosisVO> zyzd { get; set; }
    }

    public class WMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx { get; set; }
        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public bool? ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán tây y
        /// </summary>
        public string zdbz { get; set; }
    }
    /// <summary>
    /// Chẩn đoán đông y
    /// </summary>
    public class TCMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx { get; set; }
        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public bool? ysbz { get; set; }

        /// <summary>
        /// Mã chứng hậu
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// Tên chứng hậu
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán đông y
        /// </summary>
        public string zdbz { get; set; }
    }
}
