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
        /// Nghi ngờCờ
        /// </summary>
        public bool ysbz { get; set; }

        /// <summary>
        /// hội chứngMã
        /// </summary>
        public string zhCode { get; set; }

        /// <summary>
        /// hội chứngTên
        /// </summary>
        public string zhmc { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Chẩn đoán YHCTGhi chú
        /// </summary>
        public string zdbz { get; set; }
    }
}
