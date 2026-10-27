using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.CIS.Compat;
using Newtouch.Application.Interface;
using Newtouch.Domain.IDomainServices;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Chungđối tượngThao tácghi chép
    /// </summary>
    public class SysObjectActionRecordController : OrgControllerBase
    {
        private readonly ISysObjectActionRecordRepo _sysObjectActionRecordRepo;

        public SysObjectActionRecordController(ISysObjectActionRecordRepo sysObjectActionRecordRepo)
        {
            this._sysObjectActionRecordRepo = sysObjectActionRecordRepo;
        }

        /// <summary>
        /// ThêmThao tácghi chép
        /// </summary>
        /// <returns></returns>
        public IActionResult Add(SysObjectActionRecordEntity entity)
        {
            entity.OrganizeId = this.OrganizeId;
            _sysObjectActionRecordRepo.Add(entity);
            return Success();
        }

    }
}