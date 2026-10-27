namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin hiển thị thông tin thuốc bộ phận này
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
        /// Tồn kho kho thuốc
        /// </summary>
        public decimal? YkKcsl { get; set; }

        /// <summary>
        /// Vị trí kho thuốc
        /// </summary>
        public string YkDw { get; set; }

        /// <summary>
        /// Đơn vị bộ phận
        /// </summaYkDwry>
        public string deptdw { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// Giá bán buôn
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
        /// Bộ phận thuốc nàyID XT_YP_BMYPXX Khóa chính
        /// </summary>
        public string bmypId { get; set; }
        /// <summary>
        /// Hạn dùng
        /// </summary>
        public System.DateTime? yxq { get; set; }
    }
}
