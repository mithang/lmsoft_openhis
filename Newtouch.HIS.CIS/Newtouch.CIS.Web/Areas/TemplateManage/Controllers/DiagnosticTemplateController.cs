using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    [Area("TemplateManage")]
    public class DiagnosticTemplateController : OrgControllerBase
    {
        private readonly IComDiagnosisRepo _ComDiagnosisRepo;

        public DiagnosticTemplateController(IComDiagnosisRepo comDiagnosisRepo)
        {
            this._ComDiagnosisRepo = comDiagnosisRepo;
        }
        public override IActionResult Index()
        {
            return View();
        }
        public IActionResult DelDiagnosticTemplate(string Id)
        {
            _ComDiagnosisRepo.DelDiagnosticTemplate(Id);
            return Success("Xóa thành công");
        }
    }
}