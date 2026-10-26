namespace Newtouch.Domain.ValueObjects
{
    public class WMDiagnosisHtmlVO
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
        public int zdlx{ get; set; }
        /// <summary>
        /// Cờ nghi ngờ
        /// </summary>
        public bool ysbz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icd10 { get; set; }
        /// <summary>
        /// Ghi chú chẩn đoán tây y
        /// </summary>
        public string zdbz { get; set; }
        /// <summary>
        /// Loại sơ đồ vị trí răng
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// Vị trí sơ đồ răng
        /// </summary>
        public string ywstr { get; set; }
        /// <summary>
        /// Hiển thị sơ đồ răng
        /// </summary>
        public string ywxs { get; set; }
    }
}
