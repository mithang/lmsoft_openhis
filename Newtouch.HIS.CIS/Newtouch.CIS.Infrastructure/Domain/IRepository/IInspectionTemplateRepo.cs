using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInspectionTemplateRepo : IRepositoryBase<InspectionTemplateEntity>
    {
        /// <summary>
        /// mẫudanh sách
        /// </summary>
        /// <returns></returns>
        List<InspectionTemplateEntity> GetList(int type, string orgId);
    }
}
