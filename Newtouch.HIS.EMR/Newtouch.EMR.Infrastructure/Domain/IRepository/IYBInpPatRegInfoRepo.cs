using Newtouch.EMR.Domain.Entity;

namespace Newtouch.EMR.Domain.IRepository
{
    /// <summary>
    /// Người tạo:chl
    /// Ngày:2021-02-03 15:08
    /// Mô tả:Thông tin bản ghi nhập viện
    /// </summary>
    public interface IYBInpPatRegInfoRepo : IRepositoryBase<YBInpPatRegInfoEntity>
    {
        void SubmitRegInfo(YBInpPatRegInfoEntity ety);
    }
}
