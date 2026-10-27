using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class PharmacyDmnService : DmnServiceBase, IPharmacyDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        public PharmacyDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Truy vấn thông tin cửa sổ nhà thuốc
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="yfckCode"></param>
        /// <param name="yfckmc"></param>
        /// <param name="topOrganizeId"></param>
        /// <returns></returns>
        public IList<PharmacyWindowVO> GetPagintionList(Pagination pagination, string organizeId, string keyword = null)
        {
            var strSql = @"select yfck.yfckId,yfck.yfckCode,yfck.yfckmc,yfck.zt,yfck.px,yfck.CreateTime,yfck.CreatorCode
        from xt_yfck yfck
    where yfck.OrganizeId=@OrganizeId
and (isnull(@keyword, '') = '' or yfck.yfckCode like @searchkeyword or yfck.yfckmc like @searchkeyword )";
            SugarParameter[] param = new SugarParameter[] {
                            new SugarParameter("@keyword",keyword ?? ""),
                            new SugarParameter("@OrganizeId",organizeId),
                            new SugarParameter("@searchkeyword", "%" + (keyword ?? "") + "%")
                        };
            return this.QueryWithPage<PharmacyWindowVO>(strSql, pagination, param);
        }

        /// <summary>
        /// Xem thông tin thuốc bộ phận nhà thuốc(Loại lớn)
        /// </summary>
        /// <param name="dlCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<PharmacyDepartmentOpenMedicineRepoVO> SelectDepartmentMedicine(string dlCode, string organizeId)
        {
            const string sql = @"
SELECT * 
FROM [NewtouchHIS_Base].dbo.xt_yfbm_yp(NOLOCK) yfbmyp
WHERE yfbmyp.OrganizeId=@organizeId 
AND yfbmyp.dlCode=@dlCode
AND yfbmyp.zt='1'
";
            SugarParameter[] parame = {
                new SugarParameter("@dlCode",dlCode ?? ""),
                new SugarParameter("@organizeId",organizeId)
            };
            return FindList<PharmacyDepartmentOpenMedicineRepoVO>(sql, parame);
        }
    }
}
