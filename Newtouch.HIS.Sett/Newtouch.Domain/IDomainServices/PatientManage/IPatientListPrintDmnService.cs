using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.PrintDto;
using Newtouch.HIS.Domain.ReportTemplateVO;

namespace Newtouch.HIS.Domain.IDomainServices.PatientManage
{
    public interface IPatientListPrintDmnService
    {
        /// <summary>
        /// Lấy danh sách bệnh nhân
        /// </summary>
        /// <returns></returns>
        List<PatientListOutPutVO> PatientList(PatientListInputVO PatientListInputVO);
    }
}
