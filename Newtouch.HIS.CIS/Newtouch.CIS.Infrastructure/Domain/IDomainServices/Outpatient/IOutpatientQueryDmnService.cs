using NewtouchHIS.Lib.Base.Model;
using Newtouch.Domain.ValueObjects.Outpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IDomainServices.Outpatient
{
    public interface IOutpatientQueryDmnService
    {
        IList<OutpatientConsultRecordVO> GetConsultRecordGridJson(Pagination pagination, string orgId, string kssj, string jssj, string ysgh);
        IList<OutpatientConsultDetailVO> GetConsultDetailGridJson(Pagination pagination, string orgId, string kssj, string jssj, string ysgh, string keyword);
        /// <summary>
        /// Tra cứuNgoại trúchi tiếttàiliệu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<OutpatientDetailVO> GetOutpatientDetailGridJson(Pagination pagination, string orgId, string kssj, string jssj,string yscode);
        /// <summary>
        /// Tra cứuNgoại trúchi tiếttàiliệuĐơn thuốcchi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<OutpatientDetailMXVO> GetOutpatientDetailMXGridJson(string mzh,string orgId);
        /// <summary>
        /// Ngoại trúđặt hẹnthông tinTra cứu
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="yscode"></param>
        /// <returns></returns>
        IList<OutpatientReservationVO> GetReservationGridJson(Pagination pagination, string orgId, string kssj, string jssj, string yscode,string xm);

		IList<OutpatientConsultRecordVO> GetlsblInfoJson(Pagination pagination, string orgId, string blh, string ysgh);
		IList<OutpatientCfmxVO> GetlsjzcfblInfoJson(Pagination pagination, string orgId, string jzid, string ysgh);
	}
}
