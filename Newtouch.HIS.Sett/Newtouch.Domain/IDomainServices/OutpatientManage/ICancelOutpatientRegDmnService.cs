using Newtouch.Core.Common;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.OutpatientManage
{
    public interface ICancelOutpatientRegDmnService
    {
        /// <summary>
        /// Tra cứu danh sách có thể hủy đăng ký khám
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<CancelRegisterSelectVO> SelectRegisterlist(Pagination pagination, string orgId, string blh, string xm, DateTime? kssj, DateTime? jssj);

        /// <summary>
        /// Lưu hủy đăng ký khám
        /// </summary>
        /// <param name="list"></param>
        void SaveCancelRegister(string orgId, List<SaveCancelRegisterGhnmVO> list);
    }
}
