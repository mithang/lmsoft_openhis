using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatientSettlementDetailRepo : RepositoryBase<OutpatientSettlementDetailEntity>, IOutpatientSettlementDetailRepo
    {
        public OutpatientSettlementDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// theo jsnm tra cứu chi tiết quyết toán ngoại trú
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public List<OutpatientSettlementDetailEntity> SelectMzjsmxByJsnm(int jsnm, string orgId)
        {
            return this.IQueryable().Where(a => a.jsnm == jsnm && a.OrganizeId == orgId).ToList();
        }
    }
}


