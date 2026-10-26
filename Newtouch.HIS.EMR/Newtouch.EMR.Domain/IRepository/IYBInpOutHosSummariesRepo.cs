using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2021-03-01 15:24
    /// Mô tả: Tóm tắt ra viện
    /// </summary>
    public interface IYBInpOutHosSummariesRepo : IRepositoryBase<YBInpOutHosSummariesEntity>
    {
        void Submit(YBInpOutHosSummariesEntity ety);
    }
}