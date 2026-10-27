using Newtouch.EMR.Domain.Entity;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo：chl
    /// Ngày：2021-03-01 15:24
    /// Mô tả：Đăng ký nhập viện-Chẩn đoán
    /// </summary>
    public interface IYBInpPatRegInfoDiagRepo : IRepositoryBase<YBInpPatRegInfoDiagEntity>
    {
        void Submit(YBInpPatRegInfoDiagEntity ety);
    }
}
