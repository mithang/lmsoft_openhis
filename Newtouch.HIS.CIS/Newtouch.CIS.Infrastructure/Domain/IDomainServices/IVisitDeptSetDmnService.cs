using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Bác sĩrakhám
    /// </summary>
    public interface IVisitDeptSetDmnService
    {

        /// <summary>
        /// LấyrakhámBác sĩthông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptSetVO> SelectVisitDeptSet(Pagination pagination, string keyword, string organizeId);

        /// <summary>
        /// LấyrakhámBác sĩchi tiết
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VisitDeptDetail> SelectVisitDeptSetDetail(string ysgh, string organizeId);

        /// <summary>
        /// LấyrakhámBác sĩchi tiết
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VisitDeptDetail SelectVisitDeptSetInfo(string id);

        /// <summary>
        /// LấyrakhámBác sĩthông tin
        /// </summary>
        /// <param name="ysgh"></param>
        /// <param name="organizeId"></param>
        /// <param name="id">visit_deptSet.id</param>
        /// <returns></returns>
        VisitDeptDetail SelectDoctorInfo(string ysgh, string organizeId, string id = "");
    }
}
