using Newtouch.HIS.Domain;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// Phương thức xuất nhập kho
    /// </summary>
    public interface IXtypcrkfsRepo : IRepositoryBase<XtypcrkfsEntity>
    {
        /// <summary>
        /// get crkfs list 
        /// </summary>
        /// <param name="crkbz"></param>
        /// <returns></returns>
        List<XtypcrkfsEntity> GetCrkfsList(string crkbz);
    }
}