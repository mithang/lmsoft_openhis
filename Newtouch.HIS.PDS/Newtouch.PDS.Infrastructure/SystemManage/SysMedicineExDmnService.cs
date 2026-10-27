using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// Thông tin thuốc
    /// </summary>
    public class SysMedicineExDmnService : DmnServiceBase, ISysMedicineExDmnService
    {
        public SysMedicineExDmnService(IDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// Truy vấn chi tiết thuốc
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode)
        {
            const string sql = @"select * from [NewtouchHIS_Base]..V_C_xt_yp where zt = '1' and ypCode = @ypCode and OrganizeId = @orgId";
            SugarParameter[] pars = {
                new SugarParameter("@orgId", orgId), new SugarParameter("@ypCode", ypCode)
            };
            return FirstOrDefaultNoLog<SysMedicineComplexVEntity>(sql, pars);
        }
    }
}