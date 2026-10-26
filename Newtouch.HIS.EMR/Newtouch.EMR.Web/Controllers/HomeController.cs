using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.Common;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.EMR.Domain.IDomainServices;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Trang chủ
    /// </summary>
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        private readonly IMRHomePageDmnService _sysUserDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Index()
        {
            var userName = OperatorProvider.GetCurrent();
            //var userName = OperatorProvider.GetCurrent().UserCode;
            //if (userName != "root")
            //{
            //    var rygh = OperatorProvider.GetCurrent().rygh;
            //    ViewBag.gjybdm = _sysUserDmnService.GetYbdmByGh(rygh);
            //}
            var loginFromFlag = WebHelper.GetCookie(Constants.AppId + "_LoginFromFlag");
            ViewBag.loginFromFlag = loginFromFlag;
            return base.Index();
        }

        /// <summary>
        /// Lấy giá trị phát sinh ngẫu nhiên của trường nghiệp vụ (tự tăng + Format)
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public ActionResult GetNewFieldUniqueValue(string fieldName, string initFormat = "", int initFieldLength = 0)
        {
            var orgId = this.OrganizeId;
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (string.IsNullOrWhiteSpace(orgId) || initFormat == null)
            {
                value = null;
            }
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }
        /*
Đồng bộ cấu hình tham số hệ thống
        */ 
        public ActionResult SyncSysConfigParams(string orgId)
        {
            //Dữ liệu cơ sở
            var sysConfigBaseEntities = _sysConfigRepo.GetList("", "*").ToList();
            //Dữ liệu sẵn có của tổ chức
            var sysConfigEntities = _sysConfigRepo.GetList("", orgId).ToList();
            //Loại bỏ trùng lặp theo code
            var sysConfigCodes = new HashSet<string>(sysConfigEntities.Select(entity => entity.Code));
            var filteredEntities = sysConfigBaseEntities
                .Where(baseEntity => !sysConfigCodes.Contains(baseEntity.Code))
                .ToList();
            foreach (var item in filteredEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }
            var insert = _sysConfigRepo.Insert(filteredEntities);
            return Success("",insert);
        }
    }
}
