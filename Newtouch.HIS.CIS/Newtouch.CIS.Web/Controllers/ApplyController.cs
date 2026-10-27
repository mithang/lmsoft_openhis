using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Compat;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.HIS.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    public class ApplyController : OrgControllerBase
    {

        private readonly IInspectionTemplateRepo _inspectionTemplateRepo;
        private readonly ITemplateGroupPackageRepo _templateGroupPackageRepo;
        private readonly IInspectionCategoryRepo _inspectionCategoryRepo;

        public ApplyController(IInspectionTemplateRepo inspectionTemplateRepo, ITemplateGroupPackageRepo templateGroupPackageRepo, IInspectionCategoryRepo inspectionCategoryRepo)
        {
            this._inspectionTemplateRepo = inspectionTemplateRepo;
            this._templateGroupPackageRepo = templateGroupPackageRepo;
            this._inspectionCategoryRepo = inspectionCategoryRepo;
        }

        /// <summary>
        /// Xét nghiệm
        /// </summary>
        /// <returns></returns>
        public IActionResult Inspection()
        {
            return View();
        }

        /// <summary>
        /// Kiểm tra
        /// </summary>
        /// <returns></returns>
        public IActionResult Examination()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}

