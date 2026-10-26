namespace Newtouch.Domain.ValueObjects
{
    public class SqtxXmYpInfoVO
    {
        /// <summary>
        /// Mã BHYT
        /// </summary>
        public string ybdm { get; set; }
        /// <summary>
        /// Tỷ lệ tự trả Nhắc nhở trước là một ký tự,Do đó định nghĩa làint
        /// </summary>
        public int zfbl { get; set; }
        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal jl { get; set; }
        /// <summary>
        /// Đơn vị liều lượng
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// Đơn vị bán lẻ ngoại trú
        /// </summary>
        public decimal mzcls { get; set; }
        /// <summary>
        /// Nhóm lớn thu phícode
        /// </summary>
        public string sfdlCode { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal dj { get; set; } 
    }
}
