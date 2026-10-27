using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysMedicineDosageRepo: IRepositoryBase<SysMedicineDosageEntity>
    {
        /// <summary>
        /// Gửi quan hệ tương ứng dạng bào chế thuốc
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="yfbmCode"></param>
         void submitUsage(string jxCode, string yfCode);
    }
}
