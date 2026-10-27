using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.DTO.OutputDto;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.Entity.Outpatient;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    [Area("TemplateManage")]
    public class MRecordTemplateController : OrgControllerBase
    {
        private readonly IMRecordTemplateDmnService _mRecordTemplateDmnService;
        private readonly IMRTemplateRepo _mRTemplateRepo;

        public MRecordTemplateController(IMRecordTemplateDmnService mRecordTemplateDmnService, IMRTemplateRepo mRTemplateRepo)
        {
            this._mRecordTemplateDmnService = mRecordTemplateDmnService;
            this._mRTemplateRepo = mRTemplateRepo;
        }

        // GET: TemplateManage/MRecordTemplate

        /// <summary>
        /// Hồ sơmẫucây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeList(int mblx)
        {
            var treeList = new List<TreeViewModel>();
            List<MRTemplateListBO> data = _mRecordTemplateDmnService.SelectTemplateList(mblx,this.OrganizeId,this.UserIdentity.DepartmentCode,this.UserIdentity.UserCode);

            foreach (var item in data)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = item.mbId,   //mẫuId
                    value = item.mbId,
                    text = item.mbmc,
                    parentId = null,
                    hasChildren = false,
                    isexpand = false,
                    complete = true
                });
            }

            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// Tra cứumẫuchi tiết
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public IActionResult SelectTemplateDetailByMbId(string mbId)
        {
            var data = _mRecordTemplateDmnService.SelectTemplateDetailByMbId(mbId, this.OrganizeId);
            data.xyzdList = data.xyzdList.OrderBy(a => a.zdlx).ToList();   //chínhChẩn đoánxếpởtrướcmặt
            data.zyzdList = data.zyzdList.OrderBy(a => a.zdlx).ToList();   //chínhChẩn đoánxếpởtrướcmặt
            

            return Content(data.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <returns></returns>
        public IActionResult SaveData(MRTemplateEntity blmbObject, List<WMDiagnosisHtmlVO> xyzdList, List<TCMDiagnosisHtmlVO> zyzdList,List<CfTemplateGroupPackageEntity> mbztList)
        {
            blmbObject.OrganizeId = this.OrganizeId;
            if (blmbObject.mblx == (int)EnumCfMbLx.department)
            {
                blmbObject.ks = this.UserIdentity.DepartmentCode;   //Khoa
            }
            _mRecordTemplateDmnService.SaveData(blmbObject, xyzdList, zyzdList,mbztList);
            return Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public IActionResult DeleteTemplate(string mbId)
        {
            _mRecordTemplateDmnService.DeleteTemplate(mbId);
            return Success();
        }

    }

}