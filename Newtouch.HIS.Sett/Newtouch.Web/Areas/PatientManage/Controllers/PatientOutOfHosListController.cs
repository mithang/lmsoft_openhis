using System.Web.Mvc;
using Newtouch.HIS.Domain.DTO.PrintDto;
using Newtouch.HIS.Domain.IDomainServices.PatientManage;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Areas.PatientManage.Controllers
{
    public class PatientOutOfHosListController : ControllerBase
    {
        private IPatientListPrintDmnService _printService;

        public PatientOutOfHosListController(IPatientListPrintDmnService printService)
        {
            this._printService = printService;
        }

        /// <summary>
        /// Tìm kiếm theo điều kiện
        /// </summary>
        /// <param name="patientListVO"></param>
        /// <returns></returns>
        public ActionResult LoadData(PatientListInputVO patientListVO)
        {
            var data = _printService.PatientList(patientListVO);
            if (data != null)
            {
                return Content(data.ToJson());
            }
            return View();
        }
    }
}