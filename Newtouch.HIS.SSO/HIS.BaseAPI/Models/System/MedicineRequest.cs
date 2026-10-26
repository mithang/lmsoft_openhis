namespace HIS.BaseAPI.Models.System
{
    public class MedicineRequest
    {

    }
    /// <summary>
    /// cách dùng thuốc
    /// </summary>
    public class DrugUsageRequest
    {
        public string? code { get; set; }
        public int? yplx { get; set; }
    }
}
