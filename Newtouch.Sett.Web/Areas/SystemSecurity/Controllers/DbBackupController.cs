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

namespace Newtouch.Sett.Web.Areas.SystemSecurity.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemSecurity")]

    public class DbBackupController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IDbBackupApp _dbBackupApp;
        public DbBackupController(IDbBackupApp dbBackupApp)
        {
            _dbBackupApp = dbBackupApp;
        }


        [HttpGet]

        public IActionResult GetGridJson(string queryJson)
        {
            var data = _dbBackupApp.GetList(queryJson);
            return Content(data.ToJson());
        }
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.F_FilePath = SettPathHelper.MapPath("~/Resource/DbBackup/" + dbBackupEntity.F_FileName + ".bak");
            dbBackupEntity.F_FileName = dbBackupEntity.F_FileName + ".bak";
            _dbBackupApp.SubmitForm(dbBackupEntity);
            return Success("Thao tác thành công。");
        }
        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _dbBackupApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }
        [HttpPost]
        //[HandlerAuthorize]
        // Framework FileWebHelper.DownLoadold (Response-streaming download) ->
        // Core File result; missing file returns NoContent (original did nothing).
        public IActionResult DownloadBackup(string keyValue)
        {
            var data = _dbBackupApp.GetForm(keyValue);
            if (data == null)
            {
                return NoContent();
            }
            string filename = System.Net.WebUtility.UrlDecode(data.F_FileName);
            string filepath = SettPathHelper.MapPath(data.F_FilePath);
            if (System.IO.File.Exists(filepath))
            {
                return File(System.IO.File.ReadAllBytes(filepath), "application/octet-stream", filename);
            }
            return NoContent();
        }
    }
}
