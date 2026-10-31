using Newtouch.Domain.IRepository;
using Newtouch.CIS.Web.Models;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.CIS.Repository;
using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    [Area("TemplateManage")]
    public class InspectionTemplateController : OrgControllerBase
    {
        private readonly IInspectionTemplateRepo _inspectionTemplateRepo;
        private readonly IInspectionCategoryRepo _inspectionCategoryRepo;
        private readonly IInspectionTemplateDmnService _inspectionTemplateDmnService;

        public InspectionTemplateController(IInspectionTemplateRepo inspectionTemplateRepo, IInspectionCategoryRepo inspectionCategoryRepo, IInspectionTemplateDmnService inspectionTemplateDmnService)
        {
            this._inspectionTemplateRepo = inspectionTemplateRepo;
            this._inspectionCategoryRepo = inspectionCategoryRepo;
            this._inspectionTemplateDmnService = inspectionTemplateDmnService;
        }
        // GET: TemplateManage/Inspection
        
        /// <summary>
        /// Xét nghiệmKiểm traNhóm lớn dướikéo
        /// </summary>
        /// <returns></returns>
        public IActionResult GetCategoryList()
        {
            var data = _inspectionCategoryRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.zt == "1").ToList();
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// mẫudanh sách(Tra cứu)
        /// </summary>
        /// <param name="zutaoType"></param>
        /// <returns></returns>
        public IActionResult SelectGridList(int type)
        {
            var data = _inspectionTemplateRepo.GetList(type, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// mẫuChi tiết
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public IActionResult GetTemplateDetail(string mbId)
        {
            var data = _inspectionTemplateDmnService.GetTemplateDetail(mbId, this.OrganizeId);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        /// <returns></returns>
        public IActionResult SaveData(InspectionTemplateEntity mbobj, List<TemplateGroupPackageEntity> mbztlist)
        {
            mbobj.zt = mbobj.zt == "on" ? "1" : "0";
            //bảng mẫu
            mbobj.OrganizeId = this.OrganizeId;

            _inspectionTemplateDmnService.SaveData(mbobj, mbztlist);
            return Success();
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="mbId"></param>
        /// <returns></returns>
        public IActionResult DeleteData(string mbId)
        {
            _inspectionTemplateDmnService.DeleteData(mbId);
            return Success();
        }

        /// <summary>
        /// Đơn thuốctrangcủaXét nghiệmKiểm tramẫucây
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTreeList(int jyjcmbLx,string TemplateId, string ztKeyword)
        {
            var treeList = new List<TreeViewModel>();
            var alldldata = _inspectionTemplateDmnService.GetTemplateListByType(this.OrganizeId, jyjcmbLx);
            var dldata = alldldata.Select(a => new { a.dlmc }).Distinct().ToList();
            foreach (var item in dldata)
            {
                //Xét nghiệmKiểm traNhóm lớn
                treeList.Add(new TreeViewModel()
                {
                    id = item.dlmc,
                    value = item.dlmc,
                    text = item.dlmc,
                    parentId = null,
                    hasChildren = true,
                    isexpand = true,
                });

				var mbdata= string.IsNullOrWhiteSpace(TemplateId) == true? alldldata.Where(a => a.dlmc == item.dlmc).Select(a => new { a.mbId, a.mbmc }).ToList() : alldldata.Where(a => a.dlmc == item.dlmc && a.mbId == TemplateId).Select(a => new { a.mbId, a.mbmc }).ToList(); ;
				
				foreach (var mb in mbdata)
                {
                    //mẫu
                    var ztdata = _inspectionTemplateDmnService.GetTemplateDetailByMbId(this.OrganizeId, mb.mbId, ztKeyword).Select(a => new { a.ztId, a.ztmc }).Distinct();
                    //bool hasChildren = ztdata.Count() > 0 ? true : false;
                    bool hasChildren = true;
                    if (ztdata.Count() > 0)
                    {
                        treeList.Add(new TreeViewModel()
                        {
                            id = mb.mbId,
                            value = mb.mbId,
                            text = mb.mbmc,
                            parentId = item.dlmc,
                            hasChildren = hasChildren,
                            isexpand = hasChildren,
                            complete = true
                        });
                    }
                    //Bộ mẫu
                    foreach (var zt in ztdata)
                    {
                        treeList.Add(new TreeViewModel()
                        {
                            id = zt.ztId,
                            value = zt.ztId,
                            text = zt.ztmc,
                            parentId = mb.mbId,
                            hasChildren = false,
                            isexpand = false,
                            complete = true
                        });
                    }
                }
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// TheoztIdTra cứuBộ mẫudướicủaMục thu phí
        /// </summary>
        /// <returns></returns>
        public IActionResult GetGPackageDetailByZtId(string ztId,string mbId=null)
        {
            var sfxmdata = _inspectionTemplateDmnService.GetGPackageDetailByZtId(this.OrganizeId, ztId,mbId);
            return Content(sfxmdata.ToJson());
        }
        public IActionResult GetGPackageDetailByZtIdArray(string[] ztId,string mbId=null)
        {
            string ztIds = "";
            if (ztId != null && ztId.Length > 0)
            {
                ztIds=String.Join(",", ztId);
            }
            var sfxmdata = _inspectionTemplateDmnService.GetGPackageDetailByZtId(this.OrganizeId, ztIds,mbId);
            return Content(sfxmdata.ToJson());
        }
        public IActionResult GetGPackageInfoByZtId(string ztId,string mbId)
        {
            var sfxmdata = _inspectionTemplateDmnService.GetGPackageInfoByZtId(this.OrganizeId, ztId,mbId);
            return Content(sfxmdata.ToJson());
        }

        public IActionResult GetMbList(int jyjcmbLx)
		{
			var alldldata = _inspectionTemplateDmnService.GetTemplateListByType(this.OrganizeId, jyjcmbLx);
			var mbdata = alldldata.Select(a => new { a.mbId, a.mbmc }).ToList();
			return Content(mbdata.ToJson());
		}

	}
}