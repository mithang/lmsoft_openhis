using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SqlSugar;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Infrastructure;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class MrDeptDmnService : DmnServiceBase, IMrDeptDmnService
    {
        public MrDeptDmnService()
               
        {

        }

        /// <summary>
        /// Lấy danh sách khoa bệnh án HIS phân trang
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MrDeptVO> GetPaginationList(Pagination pagination, string orgId)
        {
            string sql = @"select a.Id, a.organizeId,baksId,c.ksmc as baksName,a.code as hisdept,a.name as hisdeptname from [NewtouchHIS_Base].[dbo].[V_S_Sys_Department] a
left join [Newtouch_MRMS].[dbo].[mr_rel_dept] b 
on a.Code = b.hisdept 
left join [Newtouch_MRMS].[dbo].[mr_dic_dept] c 
on b.baksId=c.ksbm 
where a.zt=1";
            var para = new List<SugarParameter>();

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and a.organizeId=@orgId";
            }
            para.Add(new SugarParameter("@orgId", orgId));

            return this.QueryWithPage<MrDeptVO>(sql.ToString(), pagination, para == null ? null : para.ToArray());

        }
    }
}
