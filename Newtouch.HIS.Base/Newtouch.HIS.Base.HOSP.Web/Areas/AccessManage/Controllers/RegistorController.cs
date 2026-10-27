using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Areas.AccessManage.Controllers
{
    [Area("AccessManage")]
    public class RegistorController : ControllerBase
    {
        // GET: AccessManage/Registor
        private readonly IAccessAuthRepo _accessAuthRepo;
        public RegistorController(IAccessAuthRepo accessAuthRepo)
        {
            this._accessAuthRepo = accessAuthRepo;
        }

        public IActionResult Form()
        {
            return View();
        }
        public IActionResult GetRegistorList(Pagination pagination, string keyword,string org)
        {
            var data = new
            {
                rows = _accessAuthRepo.FindList(pagination, keyword,org),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult GetRegistorForm(string keyValue)
        {
            var data = _accessAuthRepo.FindEntity(keyValue);
            return Content(data.ToJson());
        }


        public IActionResult submitForm(AccessAuthEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            if (string.IsNullOrWhiteSpace(entity.RegCode))
            {
                throw new FailedException("Vui lòng nhập mã");
            }
            if (string.IsNullOrWhiteSpace(entity.RegName))
            {
                throw new FailedException("Vui lòng nhập tên");
            }
            _accessAuthRepo.RegistProcess(entity, keyValue);
            return Success("Thao tác thành công");
        }
        /// <summary>
        /// Lấy khóa ủy quyền
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetAccessKey(string keyValue)
        {             
            var ety = _accessAuthRepo.FindEntity(keyValue,"1");
            if (ety != null)
            {
                AccessIdentity au = new AccessIdentity();
                au.accesskey = "API_" + ety.RegCode + "_" + ety.Id;
                au.AuthorizedLev = ety.AuthorizedLev.ToString();
                au.AuthorizedPeriod = ety.AuthorizedPeriod.ToString();
                au.RegCode = ety.RegCode;
                au.RegName = ety.RegName;
                au.Timestamp =string.IsNullOrWhiteSpace(ety.accesskey)==true? DateTime.Now:Convert.ToDateTime(ety.accesskey);
                au.AppId= ety.RegCode;
                au.UserId = au.accesskey;
                au.UserName= ety.RegName;                
                au.Account = au.RegCode;
                au.OrganizeId = ety.OrganizeId;
                var auth = "";
                string token = auth.GetType().GetProperty("token").GetValue(auth, null).ToString() ;
                string tokenType = auth.GetType().GetProperty("tokentype").GetValue(auth, null).ToString();
                au.TokenType = tokenType;
                var data = new { token = token, tokenType = tokenType, AppId = ety.RegCode, regtime=au.Timestamp };                
                return Success("Tạo khóa thành công", data.ToJson());
            }
            return Error("Thông tin đăng ký không hợp lệ");
        }
    }
}