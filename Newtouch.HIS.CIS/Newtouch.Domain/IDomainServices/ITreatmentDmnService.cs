using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.ValueObjects;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Domain.ViewModels;

namespace Newtouch.Domain.IDomainServices
{
    /// <summary>
    /// Thông tin khám bệnh
    /// </summary>
    public interface ITreatmentDmnService
    {
        /// <summary>
        /// Tra cứu thông tin khám bệnh
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TreatmentVo> SelectTreatmentEntities(string mzh, string orgId);

        TreatEntityObj SelectTreatmentEntitie(string mzh, string orgId);
        /// <summary>
        /// Tra cứu thông tin khám bệnh của bệnh nhân
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kh"></param>
        /// <param name="xm"></param>
        /// <param name="jzlx"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        IList<PatientVisitInfoVO> GetPatientVisitList(Pagination pagination, string kh, string xm, string jzlx, string jszt, DateTime? kssj, DateTime? jssj, string orgId);
        /// <summary>
        /// Lấy thông tin nội trú
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        InPatientInfoVO GetInPatientInfo(string zyh, string orgId);
    }
}