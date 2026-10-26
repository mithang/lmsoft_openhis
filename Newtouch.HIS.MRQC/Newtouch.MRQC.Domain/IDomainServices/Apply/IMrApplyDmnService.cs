using Newtouch.Core.Common;
using Newtouch.MRQC.Domain.ValueObjects.Apply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.IDomainServices.Apply
{
    public interface IMrApplyDmnService
    {
        /// <summary>
        /// danh sách đề nghị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="status"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        IList<MedicalApplyVo> GetBlApplyList(Pagination pagination, string OrganizeId, string status, DateTime ksrq, DateTime jsrq);
        /// <summary>
        /// cập nhật trạng thái phê duyệt
        /// </summary>
        /// <param name="shbhlist"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="Deptcode"></param>
        /// <param name="Gh"></param>
        void UpdateApplyStatus(string shbhlist, string OrganizeId,string Deptcode,string Gh);
    }
}
