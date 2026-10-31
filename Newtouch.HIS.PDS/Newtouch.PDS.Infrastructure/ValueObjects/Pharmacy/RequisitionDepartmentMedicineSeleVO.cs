namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Lĩnh nội bộ Chọn thuốc bộ phận Nguồn dữ liệu
    /// </summary>
    public class RequisitionDepartmentMedicineSeleVO
    {
        /// <summary>
        /// ThuốcCode
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// Thuốc Tên loại thu phí lớn
        /// </summary>
        public string ypdlmc { get; set; }

        /// <summary>
        /// Số lượng có thể lĩnhandĐơn vị 5hộp6chi,Chỉ hiển thị để dùng
        /// </summary>
        public string Klslanddw { get; set; }
        
        /// <summary>
        /// Số lượng tồn kho(XT_YP_KCXXK.Kcsl)
        /// </summary>
        public int Kcsl { get; set; }

        /// <summary>
        /// Đơn vị Số lượng tồn kho(Tương ứng XT_YP_KCXXK.Kcsl)
        /// </summary>
        public string Kcsldw { get; set; }

        /// <summary>
        /// Quy cách thuốc
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// Tên hãng thuốc
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá bán buôn
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi kho thuốc(Dùng để tính giá)
        /// </summary>
        public decimal ykzhyz { get; set; }

    }
}
