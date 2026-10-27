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

namespace Newtouch.Sett.Web.Areas.IssueTreatRecord.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("IssueTreatRecord")]

    public class ComfirmIssueController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISyncTreatmentServiceRecordRepo _syncTreatmentServiceRecordRepo;
        public ComfirmIssueController(ISyncTreatmentServiceRecordRepo syncTreatmentServiceRecordRepo)
        {
            _syncTreatmentServiceRecordRepo = syncTreatmentServiceRecordRepo;
        }


        public IActionResult GetListJson(Pagination pagination, string blh, string xm, DateTime? kssj, DateTime? jssj, string brlx)
        {
            var gh = UserIdentity.rygh;
            var data = _syncTreatmentServiceRecordRepo.GetPagintionList(pagination, OrganizeId, kssj, jssj, gh, false, false, brlx, 1, false, "", "", blh, xm);
            var list = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult Involid(List<string> list)
        {
            _syncTreatmentServiceRecordRepo.SaveCancel(OrganizeId, list);
            return Success("Thao tác thành công");
        }
    }
}
