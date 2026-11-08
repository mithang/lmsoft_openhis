using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface ISysMedicineProfitLossReasonRepo : IRepositoryBase<SysMedicineProfitLossReasonEntity>
    {
        /// <summary>
        /// Theo lãi lỗ Loại Tra cứu nguyên nhân lãi lỗ list
        /// </summary>
        /// <param name="sylx"></param>
        /// <returns></returns>
        List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx);
        IList<SysMedicineProfitLossReasonEntity> GetPagintionList(Pagination pagination, string keyword);
        void SubmitForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue);
        void DeleteForm(string keyValue);
    }
}
