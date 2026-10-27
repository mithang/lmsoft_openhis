using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices
{
    public interface IOutpatientNurseDmnServise
    {
        IList<OutpatientNurseTreeVO> OutpatientNurseTreeVO(string orgId, string keyword, DateTime? kssj, DateTime? jssj, string type);

        /// <summary>
        /// dathửThông tin bệnh nhântriểnhiển thị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="patList"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutpatientNursequeryVO> OutpatientNursequery(Pagination pagination, string patList, string orgId);

        /// <summary>
        /// dathửkếtnếunhập
        /// </summary>
        /// <param name="user"></param>
        /// <param name="yzids"></param>
        /// <param name="lrjg"></param>
        /// <returns></returns>
        string Enteragain(OperatorModel user, string cfmxid, string lrjg);

        /// <summary>
        /// dathửTra cứukếtnếuthông tintriểnhiển thị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<OutpatientNursequeryVO> skintesfrom(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, string orgId);

        /// <summary>
        /// dathửHủyThực hiện
        /// </summary>
        /// <param name="gmxxid"></param>
        /// <returns></returns>
        string skintescancel(string gmxxid, OperatorModel user);

        string getcfh(string cfid, string orgid);

        IList<OutpatientNurseTreeVO> GetPatTree(string orgid,string keyword);

        IList<OutpatientNursequeryVO> prescriptionfrom(Pagination pagination, string jzid, DateTime? klsj,string orgid,string cflb);

        IList<ObservationFromVO> observationquery(Pagination pagination, string keyword, DateTime? kssj, DateTime? jssj, string orgId);
        IList<ObservationFromVO> Getlgjl(string syxxids,string orgid);

        string SavaLgdj(IList<ObservationFromVO> lgdjlist, string orgid, OperatorModel user);
    }
}
