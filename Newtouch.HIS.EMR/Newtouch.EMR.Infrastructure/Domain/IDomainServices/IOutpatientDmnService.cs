using NewtouchHIS.Lib.Base.Model;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.EMR.Domain.IRepository;

namespace Newtouch.EMR.Domain.IDomainServices
{
    public interface IOutpatientDmnService
    {
        /// <summary>
        /// Danh sách ngoại trú đã khám
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="jzzt"></param>
        /// <param name="mjzbz"></param>
        /// <param name="rygh"></param>
        /// <param name="kzrq"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<TreatEntityVO> GetTreatingOrTreatedList(Pagination pagination, string orgId, int mjzbz, string rygh, DateTime? kzrq, string keyword);
        /// <summary>
        /// Lấy thông tin khám của bệnh nhân theo số ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="mzh"></param>
        /// <returns></returns>
        TreatEntityVO GetPatMzbymzh(string orgId, string mzh,string rygh);
        /// <summary>
        /// Cây bệnh án ngoại trú
        /// </summary>
        /// <param name="OrgId"></param>
        /// <param name="mzh"></param>
        /// <param name="rygh"></param>
        /// <returns></returns>
        IList<PatMedRecordTreeVO> GetOutPatMedRecordTree(string OrgId, string mzh, string rygh);
    }
}
