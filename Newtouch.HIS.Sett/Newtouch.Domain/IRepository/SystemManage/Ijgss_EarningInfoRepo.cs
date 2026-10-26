using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface Ijgss_EarningInfoRepo : IRepositoryBase<jgss_EarningInfoEntity>
    {
        /// <summary>
        /// Cập nhật trạng thái kiểm tra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shzt"></param>
         void updateshzt(string id, string shzt,string shr = null);
    }
}
