namespace Newtouch.Domain.ValueObjects
{
    public class TCMDiagnosisHtmlVO
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
        public bool ysbz { get; set; }

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
