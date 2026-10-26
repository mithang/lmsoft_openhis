using FrameworkBase.MultiOrg.Web;
using Newtouch.Application.Interface;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Bảo trì liên động cách dùng
    /// </summary>
    public class UsageLinkageController : OrgControllerBase
    {
        private readonly IUsageLinkageApp _usageLinkageApp;
        private readonly ISysUsageLinkageRepo _sysUsageLinkageRepo;
        private readonly ISysUsageLinkageDmnService _sysUsageLinkageDmnService;

        /// <summary>
        /// Lấy danh sách chính
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfCode"></param>
        /// <param name="xmCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeGridJson(Pagination pagination, string keyword, string yfCode, string xmCode)
        {
            var data = new
            {
                rows = _sysUsageLinkageDmnService.SelectSysUsageLinkage(pagination, keyword, yfCode, xmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Khi sửa thông tin, mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var result = _sysUsageLinkageDmnService.SelectSysUsageLinkage(keyValue);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitForm(string keyValue)
        {
            var sysUsageLinkageEntity = new SysUsageLinkageEntity
            {
                Id = keyValue,
                dlCode = Request.Params["sfdlCode"],
                OrganizeId = OrganizeId,
                sfxmCode = Request.Params["sfxmCode"],
                yfCode = Request.Params["yfCode"],
                zt = Request.Params["zt"]
            };
            var result = _usageLinkageApp.SubmitForm(sysUsageLinkageEntity);
            return string.IsNullOrWhiteSpace(result) ? Success("操作成功。") : Error(result);
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteForm(string keyValue)
        {
            var entity = _sysUsageLinkageRepo.FindEntity(keyValue);
            if (entity == null) return Success("删除成功。");
            return _sysUsageLinkageRepo.Delete(entity) > 0 ? Success("删除成功。") : Error("删除失败。");
        }
    }
}