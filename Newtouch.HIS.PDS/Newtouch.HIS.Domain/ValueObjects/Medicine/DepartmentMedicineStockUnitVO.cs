namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// bộ phận nhà thuốc thuốc số lượng tồn kho, đơn vị
    /// </summary>
    public class DepartmentMedicineStockUnitVO
    {
        /// <summary>
        /// bộ phận phát thuốc đơn vị
        /// </summary>
        public string fybmdw { get; set; }

        /// <summary>
        /// bộ phận phát thuốc hệ số chuyển đổi
        /// </summary>
        public decimal fybmzhyz { get; set; }

        /// <summary>
        /// số lượng tồn kho (tương ứng đơn vị nhỏ nhất)
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// tồn kho hiện có số lượng+đơn vị (bằng kho thuốc của phương thức để hiển thị)
        /// </summary>
        public string kcslandDw { get; set; }

        /// <summary>
        /// đơn vị (hiện tại bộ phận nhà thuốc thuốc đơn vị)
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// này bộ nên sửa thuốc hệ số chuyển đổi
        /// </summary>
        public decimal bbmzhyz { get; set; }
    }
}
