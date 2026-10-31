using Newtouch.EMR.Domain.Entity;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2021-03-01 15:24
    /// Mô tả:Tóm tắt ra viện-Chẩn đoán
    /// </summary>
    public interface IYBInpOutHosSummariesDiagRepo : IRepositoryBase<YBInpOutHosSummariesDiagEntity>
    {
        void Submit(YBInpOutHosSummariesDiagEntity ety);
    }
}
