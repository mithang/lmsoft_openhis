namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// Truy vấn nhà cung cấp thuốc
    /// </summary>
    public class MedicineSupplierQueryRequest : OrgRequestBase
    {
        /// <summary>
        /// Từ khóa
        /// </summary>
        public string keyword { get; set; }

    }
}
