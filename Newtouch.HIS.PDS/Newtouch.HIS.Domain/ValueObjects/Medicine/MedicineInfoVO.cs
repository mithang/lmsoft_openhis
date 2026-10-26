namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// này bộ phận thông tin thuốc hiển thị thông tin
    /// </summary>
    public class MedicineInfoVO
    {
        public int ypId { get; set; }
        public string ypCode { get; set; }

        public string ypmc { get; set; }

        public string ycmc { get; set; }

        public string ypxzmc { get; set; }

        public string jxmc { get; set; }
        public string ypgg { get; set; }

        public int kcsl { get; set; }
        public string Zcxh { get; set; }
        public string syzt { get; set; }

        /// <summary>
        /// kho thuốc tồn kho
        /// </summary>
        public decimal? YkKcsl { get; set; }

        /// <summary>
        /// kho thuốc vị trí kho
        /// </summary>
        public string YkDw { get; set; }

        /// <summary>
        /// đơn vị bộ phận
        /// </summaYkDwry>
        public string deptdw { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcxx { get; set; }
        public int Jhd { get; set; }
        public int Jhl { get; set; }
        public string yplb { get; set; }
        public string ypzt { get; set; }
        public int? Sysx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// này bộ phận thuốc ID XT_YP_BMYPXX khóa chính
        /// </summary>
        public string bmypId { get; set; }
        /// <summary>
        /// hạn sử dụng
        /// </summary>
        public System.DateTime? yxq { get; set; }
    }
}
