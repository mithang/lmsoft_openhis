namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    /// <summary>
    /// Ngoại trúBệnh nhânBệnh án
    /// </summary> 
    public class OutpMedicalRecordVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? OrganizeId { get; set; }
        public string? mzh { get; set; }

        /// <summary>
        /// Thông tin đăng ký khám dư thừa  Enum 1 Khám ngoại trú thông thường 2 Cấp cứu 3Chuyên gia
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string? blh { get; set; }        

        /// <summary>
        /// Lý do khám chính
        /// </summary>
        public string? zs { get; set; }

        /// <summary>
        /// Thời gian phát bệnh
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// Bệnh sử hiện tại
        /// </summary>
        public string? xbs { get; set; }

        /// <summary>
        /// Tiền sử
        /// </summary>
        public string? jws { get; set; }

        /// <summary>
        /// Khám cơ thể
        /// </summary>
        public string? ct { get; set; }
        /// <summary>
        /// Xử lý
        /// </summary>
        public string? clfa { get; set; }
        /// <summary>
        /// Kiểm tra hỗ trợ
        /// </summary>
        public string? fzjc { get; set; }
        /// <summary>
        /// Tiền sử kinh nguyệt
        /// </summary>
        public string? yjs { get; set; }
        /// <summary>
        /// Tiền sử dị ứng
        /// </summary>
        public string? gms { get; set; }
        /// <summary>
        /// Kiểm tra thông thường
        /// </summary>
        public OutpGeneralInspectionVO? ybjc { get; set; }
        public List<OutpWMDiagnosisVO>? xyzd { get; set; }
        public List<OutpTCMDiagnosisVO>? zyzd { get; set; }
    }
    /// <summary>
    /// Bệnh án ngoại trú-Kiểm tra thông thường
    /// </summary>
    public class OutpGeneralInspectionVO
    {
        #region Kiểm tra thông thường
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
        /// Cách đo đường huyết Sau ăn Lúc đói Ngẫu nhiên 
        /// EnumXtclfs
        /// </summary>
        public string? xuetangclfs { get; set; }

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
        public string? hy { get; set; }
        #endregion
    }

    /// <summary>
    /// Chẩn đoán tây y ngoại trú
    /// </summary>
    public class OutpWMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? zdlx { get; set; }
        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? icd10 { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán tây y
        /// </summary>
        public string? zdbz { get; set; }
    }
    /// <summary>
    /// Chẩn đoán y học cổ truyền ngoại trú
    /// </summary>
    public class OutpTCMDiagnosisVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? zdCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? zdmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zdlx { get; set; }
        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }

        /// <summary>
        /// Chứng hậuMã
        /// </summary>
        public string? zhCode { get; set; }

        /// <summary>
        /// Tên chứng hậu
        /// </summary>
        public string? zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string? icd10 { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán y học cổ truyền
        /// </summary>
        public string? zdbz { get; set; }
    }
}
