using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class OupatientInvoicePrintRepo : RepositoryBase<OupatientInvoicePrintEntity>, IOupatientInvoicePrintRepo
    {
        public OupatientInvoicePrintRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// theo jsnm tra cứu in bản ghi 
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public OupatientInvoicePrintEntity SelectOutPatientInvoicePrintByJsnm(int jsnm, string orgId)
        {
            return this.IQueryable().Where(a => a.jsnm == jsnm && a.zt == "1" && a.OrganizeId == orgId).OrderByDescending(a => a.CreateTime).FirstOrDefault();
        }

    }
}


