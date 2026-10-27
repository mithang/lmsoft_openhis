namespace Newtouch.Domain.ValueObjects
{
    public class SqtxXmYpInfoVO
    {
        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string ybdm { get; set; }
        /// <summary>
        /// Tỷ lệ tự trả nhắc trướclàmột ký tự，故địnhnghĩalàint
        /// </summary>
        public int zfbl { get; set; }
        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal jl { get; set; }
        /// <summary>
        /// Đơn vị liều
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// Đơn vị lẻ ngoại trú
        /// </summary>
        public decimal mzcls { get; set; }
        /// <summary>
        /// Nhóm thu phí lớncode
        /// </summary>
        public string sfdlCode { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; } 
    }
}
