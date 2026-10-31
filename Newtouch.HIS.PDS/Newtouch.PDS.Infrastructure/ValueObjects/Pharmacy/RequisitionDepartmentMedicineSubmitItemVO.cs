namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Đơn lĩnh Gửi đơn xin
    /// </summary>
    public class RequisitionDepartmentMedicineSubmitItemVO
    {
        /// <summary>
        /// ThuốcCode
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số lượng lĩnh
        /// </summary>
        public int slsl { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi bộ phận này(Bộ phận lĩnh)
        /// </summary>
        public int bbmzhyz { get; set; }
    }
}
