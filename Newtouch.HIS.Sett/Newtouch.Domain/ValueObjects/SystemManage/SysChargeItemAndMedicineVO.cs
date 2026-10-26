namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// Hạng mục thu phí,Thuốc VO
    /// </summary>
    public class SysChargeItemAndMedicineVO
    {
        /// <summary>
        /// 
        /// Khi là thuốcypCode
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// Khi là thuốcypmc
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 1 Thuốc   2 Hạng mục
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// Số lượng đơn vị đo
        /// </summary>
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
        /// <summary>
        /// Liều lượng
        /// </summary>
        public decimal? jl { get; set; }
        /// <summary>
        /// Đơn vị liều dùng
        /// </summary>
        public string jzdw { get; set; }

    }
}
