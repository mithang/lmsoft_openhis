using Newtouch.EMR.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo: chl
    /// Ngày: 2021-02-03 15:08
    /// Mô tả: Thông tin ghi chép vào viện
    /// </summary>
    public interface IYBInpPatRegInfoRepo : IRepositoryBase<YBInpPatRegInfoEntity>
    {
        void SubmitRegInfo(YBInpPatRegInfoEntity ety);
    }
}