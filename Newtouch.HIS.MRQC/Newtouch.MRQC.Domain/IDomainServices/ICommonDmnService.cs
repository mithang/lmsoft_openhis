using FrameworkBase.MultiOrg.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.IDomainServices
{
    public interface ICommonDmnService
    {
        /// <summary>
        /// nhân sự（vị trí công tác）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="dutyCode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysDutyStaffVO> GetStaffByDutyCode(string orgId, string dutyCode, string keyword = null);
    }
}
