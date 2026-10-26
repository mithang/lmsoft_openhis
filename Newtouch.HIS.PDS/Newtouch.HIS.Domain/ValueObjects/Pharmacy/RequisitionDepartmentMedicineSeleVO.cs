namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// lĩnh nội bộ bộ phận thuốc chọn nguồn dữ liệu
    /// </summary>
    public class RequisitionDepartmentMedicineSeleVO
    {
        /// <summary>
        /// thuốc Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// thuốc thu phí tên nhóm lớn
        /// </summary>
        public string ypdlmc { get; set; }

        /// <summary>
        /// số lượng có thể lĩnh and đơn vị 5 hộp 6支, 仅显示用
        /// </summary>
        public string Klslanddw { get; set; }
        
        /// <summary>
        /// số lượng tồn kho (XT_YP_KCXXK.Kcsl)
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// số lượng tồn kho đơn vị (tương ứng XT_YP_KCXXK.Kcsl)
        /// </summary>
        public string Kcsldw { get; set; }

        /// <summary>
        /// quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// tên nhà máy dược
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// kho thuốc hệ số chuyển đổi (tính giá dùng của)
        /// </summary>
        public decimal ykzhyz { get; set; }

    }
}
