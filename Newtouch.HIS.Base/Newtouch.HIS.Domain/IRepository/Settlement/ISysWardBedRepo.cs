using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysWardBedRepo : IRepositoryBase<SysWardBedEntity>
    {
        /// <summary>
        /// Lưu
        /// </summary>
        void SubmitForm(SysWardBedEntity entity, int? cwId);

        /// <summary>
        /// Xóa
        /// </summary>
        void DeleteForm(int cwId,string orgId);

        /// <summary>
        /// Theo tình trạng sử dụng giường bệnh
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="sfzy"></param>
        void UpdateOccupyByCode(string code, string orgId, bool sfzy);
    }
}
