using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Compat;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class TakemedicineController : OrgControllerBase
    {
    }
}