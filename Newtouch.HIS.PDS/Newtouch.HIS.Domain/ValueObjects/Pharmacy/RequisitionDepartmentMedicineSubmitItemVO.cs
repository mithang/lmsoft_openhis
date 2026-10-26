namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// phiếu lĩnh gửi đề nghị
    /// </summary>
    public class RequisitionDepartmentMedicineSubmitItemVO
    {
        /// <summary>
        /// thuốc Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số lượng lĩnh
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// này bộ phận hệ số chuyển đổi (bộ phận lĩnh)
        /// </summary>
        public int bbmzhyz { get; set; }
    }
}
