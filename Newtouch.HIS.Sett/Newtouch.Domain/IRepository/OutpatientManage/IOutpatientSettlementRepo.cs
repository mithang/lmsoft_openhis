using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    public interface IOutpatientSettlementRepo : IRepositoryBase<OutpatientSettlementEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OutpatientSettlementEntity SelectMZJS(int jsnm, string orgId);

        ///// <summary>
        ///// TheoghnmTra cứulist
        ///// </summary>
        ///// <param name="orgId"></param>
        ///// <param name="ghnm"></param>
        ///// <returns></returns>
        //List<OutpatientSettlementEntity> SelectEntityByGhnm(string orgId, int ghnm);

        /// <summary>
        /// Cưỡng chế cập nhật số hóa đơn
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <param name="orgId"></param>
        void UpdateSettedFph(int jsnm, string fph, string orgId);
    }
}
