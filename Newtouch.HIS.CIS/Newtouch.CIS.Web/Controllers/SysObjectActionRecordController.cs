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
    /// Chung đối tượng Thao tác ghi chép
    /// </summary>
    public class SysObjectActionRecordController : OrgControllerBase
    {
        private readonly ISysObjectActionRecordRepo _sysObjectActionRecordRepo;

        public SysObjectActionRecordController(ISysObjectActionRecordRepo sysObjectActionRecordRepo)
        {
            this._sysObjectActionRecordRepo = sysObjectActionRecordRepo;
        }

        /// <summary>
        /// Thêm Thao tác ghi chép
        /// </summary>
        /// <returns></returns>
        public IActionResult Add(SysObjectActionRecordEntity entity)
        {
            // G5: entity null thi entity.OrganizeId NRE; entity rong thi Add insert dong
            // thieu objectType/objectKey/actionType (NOT NULL) roi tra ve loi SQL tho.
            // Chan ngay tu dau bang loi trung thuc tieng Viet.
            if (entity == null || string.IsNullOrWhiteSpace(entity.objectType)
                || string.IsNullOrWhiteSpace(entity.objectKey)
                || string.IsNullOrWhiteSpace(entity.actionType))
            {
                return Error("Vui lòng nhập đầy đủ loại đối tượng, mã đối tượng và loại thao tác.");
            }
            entity.OrganizeId = this.OrganizeId;
            _sysObjectActionRecordRepo.Add(entity);
            return Success();
        }

    }
}