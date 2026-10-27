using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// trongyquánđẩyThông tin bệnh nhân
    /// </summary>
    public interface ICmmHis01Repo : IRepositoryBase<CmmHis01Entity>
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CmmHis01Entity SelectData(string id, string organizeId);

        /// <summary>
        /// select data
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CmmHis01Entity SelectDataByMzh(string mzh, string organizeId);
    }
}
