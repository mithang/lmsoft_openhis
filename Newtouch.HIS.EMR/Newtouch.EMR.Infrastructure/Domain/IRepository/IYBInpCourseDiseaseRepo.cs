using Newtouch.EMR.Domain.Entity;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2021-03-01 15:24
    /// Mô tả：Ghi chép diễn biến bệnh
    /// </summary>
    public interface IYBInpCourseDiseaseRepo : IRepositoryBase<YBInpCourseDiseaseEntity>
    {
        void Submit(YBInpCourseDiseaseEntity ety);
    }
}
