using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// don thuoc ngoai tru
    /// </summary>
    public interface IMzCfmxRepo : IRepositoryBase<MzCfmxEntity>
    {
        /// <summary>
        /// lay chi tiet don thuoc theo so don thuoc
        /// </summary>
        /// <param name="cfh"></param>
        /// <returns></returns>
        List<MzCfmxEntity> GetCfmxByCfh(string cfh);

        /// <summary>
        /// danh gia da ton tai hay chua
        /// </summary>
        /// <param name="cfh"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="gg">quy cach</param>
        /// <returns></returns>
        bool IsExist(string cfh, string ypCode, string organizeId, string gg = "");

        /// <summary>
        /// xoa chi tiet don thuoc cu
        /// </summary>
        /// <param name="cfh">so don thuoc</param>
        /// <returns></returns>
        int DeleteCfmx(string cfh);

        /// <summary>
        /// xoa chi tiet don thuoc cu
        /// </summary>
        /// <param name="cfhs">danh sach so don thuoc</param>
        /// <returns></returns>
        int DeleteCfmx(List<string> cfhs);
    }
}