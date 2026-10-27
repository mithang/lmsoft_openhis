using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    [Area("TemplateManage")]
    public class GroupPackageController : OrgControllerBase
    {
        private readonly IGroupPackageRepo _groupPackageRepo;
        private readonly IGroupPackageDmnService _groupPackageDmnService;

        public GroupPackageController(IGroupPackageRepo groupPackageRepo, IGroupPackageDmnService groupPackageDmnService)
        {
            this._groupPackageRepo = groupPackageRepo;
            this._groupPackageDmnService = groupPackageDmnService;
        }

        // GET: TemplateManage/GroupPackage

        /// <summary>
        /// 组套 浮层
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult SelectGroupPackageList(int type, string keyword)
        {
            var data = _groupPackageRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.zt == "1" && a.Type == type).ToList();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(a => a.ztmc.Contains(keyword)).ToList();
            }
            return Content(data.ToJson());
        }

        /// <summary>
        /// 组套列表（查询）
        /// </summary>
        /// <param name="zutaoType"></param>
        /// <returns></returns>
        public IActionResult SelectGridList(int type,string keyword)
        {
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var data = _groupPackageRepo.GetList(type, this.OrganizeId, keyword);
                return Content(data.ToJson());
            }
            else
            {
                var data = _groupPackageRepo.GetList(type, this.OrganizeId);
                return Content(data.ToJson());
            }

        }

        /// <summary>
        /// 组套详情（查询）
        /// </summary>
        /// <param name="ztId"></param>
        /// <returns></returns>
        public IActionResult GetGroupPackageDetail(string ztId)
        {
            var data = _groupPackageDmnService.GetGroupPackageDetail(ztId, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        /// <returns></returns>
        public IActionResult SaveData(GroupPackageEntity ztobj, List<GroupPackageItemEntity> ztxmlist)
        {
            //模板表
            ztobj.OrganizeId = this.OrganizeId;

            _groupPackageDmnService.SaveData(ztobj, ztxmlist);
            return Success();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ztId"></param>
        /// <returns></returns>
        public IActionResult DeleteData(string ztId)
        {
            _groupPackageDmnService.DeleteData(ztId);
            return Success();
        }

    }
}