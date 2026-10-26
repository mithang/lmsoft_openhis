using System.Web.Mvc;

namespace Newtouch.HIS.Sett.Rehab.Controllers.KnowledgeBaseManagementControll
{
    public class KnowledgeBaseManagementController : Controller
    {
        // GET: KnowledgeBaseManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MedicalInsurance()
        {
            return View();
        }

        public ActionResult AddMedicalInsurance()
        {
            return View();
        }



        #region 商业保险
        /// <summary>
        /// bảo hiểm thư ơng mại 
        /// </summary>
        /// <returns></returns>
        public ActionResult CommercialInsurance()
        {
            return View();
        }
        /// <summary>
        /// Thêm mới bảo hiểm thư ơng mại 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCommercialInsurance()
        {
            return View();
        }
        #endregion
    }
}