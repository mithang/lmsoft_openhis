using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Bảng liên kết số thứ tự bồi thường ngoại trú Bảo hiểm nông thôn mới
    /// </summary>
    public interface IOutpatientXnhOutpIdRelRepo : IRepositoryBase<OutpatientXnhOutpIdRelEntity>
    {
        /// <summary>
        /// Trạng thái sửa đổi không hiệu lực
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateZtDisable(string mzh, string organizeId, string userCode);

        /// <summary>
        /// Trạng thái sửa đổi không hiệu lực
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateZtDisable(string mzh, string outpId, string organizeId, string userCode);

        /// <summary>
        /// Sửa trạng thái xử lý
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">Trạng thái xử lý 0-Chưa xử lý 1-Chi tiết đã tải lên 2-Đã quyết toán 3-Đã đảo hóa đơn đỏ  4-Quay lại ngoại trú </param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateProcessingStatus(string mzh, int processingStatus, string organizeId, string userCode);

        /// <summary>
        /// Sửa trạng thái xử lý
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="processingStatus">Trạng thái xử lý 0-Chưa xử lý 1-Chi tiết đã tải lên 2-Đã quyết toán 3-Đã đảo hóa đơn đỏ  4-Quay lại ngoại trú </param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int UpdateProcessingStatus(string mzh, int processingStatus, string outpId, string organizeId, string userCode);
    }
}