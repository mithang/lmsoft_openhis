using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Thao tác chứng từ nhập/xuất kho
    /// </summary>
    public interface IKfCrkdjRepo : IRepositoryBase<KfCrkdjEntity>
    {
        /// <summary>
        /// Trả lại đơn đăng ký Từ chờ xử lý chuyển thành lưu tạm,Chỉ áp dụng cho nhập kho ngoài
        /// </summary>
        /// <param name="crkId"></param>
        /// <returns></returns>
        string BackToTemporary(long crkId);
    }
}