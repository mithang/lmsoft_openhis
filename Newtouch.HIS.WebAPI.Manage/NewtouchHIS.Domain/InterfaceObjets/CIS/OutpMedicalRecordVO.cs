namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    /// <summary>
    /// bệnh án bệnh nhân ngoại trú
    /// </summary> 
    public class OutpMedicalRecordVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string? OrganizeId { get; set; }
        public string? mzh { get; set; }

        /// <summary>
        /// thông tin đăng ký khám dự phòng  enum 1 khám ngoại trú thông thường 2 cấp cứu 3chuyên gia
        /// </summary>
        public int? mjzbz { get; set; }

        /// <summary>
        /// số bệnh án
        /// </summary>
        public string? blh { get; set; }        

        /// <summary>
        /// than phiền chính
        /// </summary>
        public string? zs { get; set; }

        /// <summary>
        /// thời gian phát bệnh
        /// </summary>
        public DateTime? fbsj { get; set; }

        /// <summary>
        /// bệnh sử hiện tại
        /// </summary>
        public string? xbs { get; set; }

        /// <summary>
        /// tiền sử
        /// </summary>
        public string? jws { get; set; }

        /// <summary>
        /// khám thực thể
        /// </summary>
        public string? ct { get; set; }
        /// <summary>
        /// xử lý
        /// </summary>
        public string? clfa { get; set; }
        /// <summary>
        /// kiểm tra hỗ trợ
        /// </summary>
        public string? fzjc { get; set; }
        /// <summary>
        /// tiền sử kinh nguyệt
        /// </summary>
        public string? yjs { get; set; }
        /// <summary>
        /// tiền sử dị ứng
        /// </summary>
        public string? gms { get; set; }
        /// <summary>
        /// kiểm tra thông thường
        /// </summary>
        public OutpGeneralInspectionVO? ybjc { get; set; }
        public List<OutpWMDiagnosisVO>? xyzd { get; set; }
        public List<OutpTCMDiagnosisVO>? zyzd { get; set; }
    }
    /// <summary>
    /// bệnh án ngoại trú-kiểm tra thông thường
    /// </summary>
    public class OutpGeneralInspectionVO
    {
        #region 一般检查
        /// <summary>
        /// cân nặng
        /// </summary>
        public decimal? tizhong { get; set; }

        /// <summary>
        /// nhiệt độ cơ thể
        /// </summary>
        public decimal? tiwen { get; set; }

        /// <summary>
        /// mạch
        /// </summary>
        public decimal? maibo { get; set; }

        /// <summary>
        /// phương thức đo đường huyết sau bữa ăn lúc đói ngẫu nhiên 
        /// EnumXtclfs
        /// </summary>
        public string? xuetangclfs { get; set; }

        /// <summary>
        /// đường huyết
        /// </summary>
        public decimal? xuetang { get; set; }
        /// <summary>
        /// chiều cao
        /// </summary>
        public decimal? shengao { get; set; }
        /// <summary>
        /// huyết áp tâm thu
        /// </summary>
        public decimal? shousuoya { get; set; }
        /// <summary>
        /// huyết áp tâm trương
        /// </summary>
        public decimal? shuzhangya { get; set; }
        /// <summary>
        /// hô hấp
        /// </summary>
        public decimal? huxi { get; set; }
        /// <summary>
        /// tình trạng hôn nhân EnumMarriageStu
        /// </summary>
        public string? hy { get; set; }
        #endregion
    }

    /// <summary>
    /// chẩn đoán Tây y ngoại trú
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
        /// cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? icd10 { get; set; }
        /// <summary>
        /// ghi chú chẩn đoán Tây y
        /// </summary>
        public string? zdbz { get; set; }
    }
    /// <summary>
    /// chẩn đoán y học cổ truyền ngoại trú
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
        /// cờ nghi ngờ
        /// </summary>
        public string? ysbz { get; set; }

        /// <summary>
        /// mã chứng hậu
        /// </summary>
        public string? zhCode { get; set; }

        /// <summary>
        /// tên chứng hậu
        /// </summary>
        public string? zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string? icd10 { get; set; }
        /// <summary>
        /// ghi chú chẩn đoán y học cổ truyền
        /// </summary>
        public string? zdbz { get; set; }
    }
}
