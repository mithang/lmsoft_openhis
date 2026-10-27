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

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IEMRDmnService
    {
        //IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh, string ysgh, string OrgId, int type);
        IList<PatMedRecordTreeVO> GetPatMedRecordTree(string OrgId, string zyh, string rygh);
        IList<PatListVO> GetPatList(Pagination pagination, string keyword, string zyh,
            string cyts, string blzt, string ysgh, string OrgId, int type);
		IList<PatListVO> GetPagintionList(Pagination pagination, string keyword, string zyh,
			string cyts, string blzt, string ysgh, string OrgId, int type, string kssj, string jssj);
        ZybrjbxxVO GetZyPatInfobyzyh(string orgId, string zyh);

    }
}
