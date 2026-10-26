using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.IDomainServices
{
    public interface IPresTemplateDmnService
    {
        /// <summary>
        /// Tra cứu chi tiết mẫu
        /// </summary>
        /// <param name="mbId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        PresTemplateBO SelectPresDetailByMbId(string mbId, string orgId,string cflx=null);

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="mbObj"></param>
        /// <param name="mxList"></param>
        string SaveData(PresTemplateEntity mbObj, List<PresTemplateDetailmxVo> mxList);

         void DeleteTemplate(string mbId, string orgId);
        /// <summary>
        /// Tra cứu mẫu đơn thuốc
        /// </summary>
        /// <param name="cflx"></param>
        /// <param name="mblx"></param>
        /// <param name="orgId"></param>
        /// <param name="deptCode"></param>
        /// <param name="userCode"></param>
        /// <param name="mbKeyword"></param>
        /// <returns></returns>
        List<PresTemplateTree> SelectCfTemplateList(int cflx, int mblx, string orgId, string deptCode, string userCode, string mbKeyword = null);
    }
}
