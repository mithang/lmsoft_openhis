using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysConfigHospRepo : IRepositoryBase<SysConfigHospEntity>
    {
        /// <summary>
        /// Lấy cấu hình nội trú theo mã
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        SysConfigHospEntity GetByDM(string dm);

        /// <summary>
        /// TheodmLấy cấu hình
        /// </summary>
        /// <param name="dm"></param>
        /// <returns></returns>
        string GetPZByDM(string dm);

    }
}
