namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// hệ thống thuốc đơn chứng
    /// </summary>
    public interface ISysMedicineReceiptDmnService
    {
        /// <summary>
        /// chứng từ nhập/xuất kho duyệt
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <param name="shzt"></param>
        /// <returns></returns>
        bool ApprovalStorageIoReceipt(string crkId, int djlx, string shzt);
        bool SubmitDrupreparation(string byId, string shzt);
        /// <summary>
        /// lấy hệ số chuyển đổi
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        int GetZhyz(string organizeId, string yfbmCode, string ypCode);
    }
}
