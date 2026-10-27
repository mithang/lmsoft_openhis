using Newtouch.MR.ManageSystem.Domain.Entity;
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
    public interface IMrFeeRelDmnService
    {
        IList<bafeeRelVO> GetPagintionList(Pagination pagination, string keyword, string organizeId,string code);
        IList<feeSelVO> GetFeeSel(string orgId);
        bafeeRelVO GetFormJson(string keyValue, string organizeId);
        int SubmitForm(bafeeRelVO entity);
		int SaveForm(bafeeRelVO entity);
		IList<bafeeRelVO> GetPagintionListById(Pagination pagination, string organizeId, string id);
		IList<bafeeRelVO> GetPagintionListByCodes(Pagination pagination, string organizeId, string ids);
		IList<itemEntity> GetPagintionItem(Pagination pagination, string organizeId, string keywordm,string hissfdl,string code);
        void SaveRelbyHissfdl(string code, string hissfdl, string user,string orgId);
        void SaveRelbyhissfxm(string code, string ids, string user, string orgId);

    }
}
