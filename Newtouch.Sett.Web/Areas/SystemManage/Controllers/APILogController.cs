using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class APILogController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISyncTreatmentServiceRecordRepo _syncTreatmentServiceRecordRepo;
        public APILogController(ISyncTreatmentServiceRecordRepo syncTreatmentServiceRecordRepo)
        {
            _syncTreatmentServiceRecordRepo = syncTreatmentServiceRecordRepo;
        }


        public IActionResult GetListJson(Pagination pagination, string blh, string xm, DateTime? kssj, DateTime? jssj, string brlx, bool? wtjlbz, bool? isDeleted, string rygh)
        {
            //var gh = UserIdentity.rygh;
            bool? isAdmini = UserIdentity.IsHospAdministrator;
            var data = _syncTreatmentServiceRecordRepo.GetPagintionList(pagination, OrganizeId, kssj, jssj, rygh, isDeleted, isAdmini, brlx, 1, wtjlbz, "", "", blh, xm);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }
    }
}
