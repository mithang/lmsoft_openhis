namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Bộ phận nhà thuốc Thuốc Số lượng tồn kho,Đơn vị
    /// </summary>
    public class DepartmentMedicineStockUnitVO
    {
        /// <summary>
        /// Đơn vị bộ phận phát thuốc
        /// </summary>
        public string fybmdw { get; set; }

        /// <summary>
        /// Hệ số chuyển đổi bộ phận phát thuốc
        /// </summary>
        public decimal fybmzhyz { get; set; }

        /// <summary>
        /// Số lượng tồn kho(Đơn vị nhỏ nhất tương ứng)
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// Số lượng tồn kho hiện có+Đơn vị(Hiển thị theo cách kho thuốc)
        /// </summary>
        public string kcslandDw { get; set; }

        /// <summary>
        /// Đơn vị(Bộ phận nhà thuốc hiện tại Thuốc Đơn vị)
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// Thuốc bộ phận này cần sửa Hệ số chuyển đổi
        /// </summary>
        public decimal bbmzhyz { get; set; }
    }
}
