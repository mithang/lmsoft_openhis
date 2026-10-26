using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Bác sĩ khám bệnh
    /// </summary>
    public interface IVisitDeptSetDmnService
    {

        /// <summary>
        /// Lấy thông tin bác sĩ khám bệnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptSetVO> SelectVisitDeptSet(Pagination pagination, string keyword, string organizeId);

        /// <summary>
        /// Lấy chi tiết bác sĩ khám bệnh
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptDetail> SelectVisitDeptSetDetail(string ysgh, string organizeId);

        /// <summary>
        /// Lấy chi tiết bác sĩ khám bệnh
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VisitDeptDetail SelectVisitDeptSetInfo(string id);

        /// <summary>
        /// Lấy thông tin bác sĩ khám bệnh
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <param name="id">visit_deptSet.id</param>
        /// <returns></returns>
        VisitDeptDetail SelectDoctorInfo(string ysgh, string organizeId, string id = "");
    }
}