using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ICLogicJoinGridDmnService
    {
        /// <summary>
        /// Thuật toán tính phí bệnh nhângridHiển thị
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Tìm kiếm thuật toán tính phí bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<PatiChargeLogicVO> GetPatiChargeLogicBySearch(Pagination pagination, string keyword, string orgId);
        /// <summary>
        /// Khi biên tập trang,Tải đối tượng của dòng đã chọn
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        List<PatiChargeLogicVO> GetPatiChargeLogicFirst(string keyvalue);
    }
}
