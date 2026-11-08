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
        /// Tên bệnh nhân
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
        /// Dư thừa đăng ký khám thông tin  Liệt kê 1 thông thường Ngoại trú 2 gấp khám 3chuyên gia
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }
        #region Bình thườngKiểm tra
        /// <summary>
        /// thể trọng
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// Nhiệt độ
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// Mạch
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// đường huyết phương thức đo Sau ăn Lúc đói Ngẫu nhiên 
        /// EnumXtclfs
        /// </summary>
        public string xuetangclfs { get; set; }

        /// <summary>
        /// đường huyết
        /// </summary>
        public decimal? xuetang { get; set; }
        /// <summary>
        /// thân cao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// thu Huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// thư tờ áp
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
        /// chính bảo
        /// </summary>
        public string zs { get; set; }

        /// <summary>
        /// phát bệnh Thời gian
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// hiện bệnh sử
        /// </summary>
        public string xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string jws { get; set; }

        /// <summary>
        /// tra thể
        /// </summary>
        public string ct { get; set; }
        /// <summary>
        /// chỗ lý
        /// </summary>
        public string clfa { get; set; }
        /// <summary>
        /// hỗ trợ Kiểm tra
        /// </summary>
        public string fzjc { get; set; }
        /// <summary>
        /// Tháng tiền sử bệnh
        /// </summary>
        public string yjs { get; set; }
        /// <summary>
        /// qua Tiền sử dị ứng
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
        /// Nghi ngờ Cờ
        /// </summary>
        public bool? ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Chẩn đoán Tây yGhi chú
        /// </summary>
        public string zdbz { get; set; }
    }
    /// <summary>
    /// Chẩn đoán YHCT
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
        /// Nghi ngờ Cờ
        /// </summary>
        public bool? ysbz { get; set; }

        /// <summary>
        /// hội chứng Mã
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// hội chứng Tên
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Chẩn đoán YHCT Ghi chú
        /// </summary>
        public string zdbz { get; set; }
    }
}
