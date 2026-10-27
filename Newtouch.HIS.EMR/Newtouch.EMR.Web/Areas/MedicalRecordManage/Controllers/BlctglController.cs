using Newtouch.EMR.Domain.Entity;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Framework.Web.Controllers;
using System.Collections.Generic;

using Newtouch.EMR.Domain.IRepository;
using System.Linq;
using System;

using Newtouch.EMR.Domain.IDomainServices;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-09-07 15:46
    /// 描 述：词条管理
    /// </summary>
    [Area("MedicalRecordManage")]
    public class BlctglController : OrgControllerBase
    {
        private readonly IBlctglRepo _blctglRepo;
        private readonly ICommonDmnService _CommonDmnService;

        private char[] trimstr = { ' ', '-', '\'', '\"', '\\', '\n', '\r' };

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="blctglRepo"></param>
        public BlctglController(IBlctglRepo blctglRepo, ICommonDmnService commonDmnService)
        {
            this._blctglRepo = blctglRepo;
            this._CommonDmnService = commonDmnService;
        }

        public IActionResult CTGLList()
        {
            return View();
        }

		public IActionResult YXFHlist()
		{
			return View();
		}

        public IActionResult GetTreeList(int qx)
        {

            var treeList = new List<TreeViewModel>();
            var dataList = _blctglRepo.GetTreeList(OrganizeId, qx, this.UserIdentity.UserCode, "0");

            foreach (var data in dataList)
            {

                treeList.Add(new TreeViewModel()
                {
                    id = data.ID,
                    value = "parent",
                    text = data.mc,
                    parentId = null,
                    hasChildren = true,
                    isexpand = true,
                });
                var dataChildren = _blctglRepo.GetTreeList(OrganizeId, qx, this.UserIdentity.UserCode, data.ID);

                foreach (var child in dataChildren)
                {
                    if (!string.IsNullOrWhiteSpace(child.mc) && !string.IsNullOrWhiteSpace(child.ctnr))
                    {
                        treeList.Add(new TreeViewModel()
                        {
                            id = child.ID,
                            value = child.ctnr.Replace("\n","").Trim(trimstr),
                            text = child.mc,
                            title=child.ctnr.Replace("\n", "").Trim(trimstr),
                            parentId = child.parentId,
                            hasChildren = false,
                            isexpand = false,
                        });
                    }

                }

            }
            var show = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));

        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _blctglRepo.FindEntity(keyValue);
            string mlmc = "";
            if (entity != null)
            {
                mlmc = _blctglRepo.FindEntity(entity.parentId).mc;
            }
            var data = new {
                ct = entity,
                parentmc = mlmc
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IActionResult SubmitForm(BlctglEntity entity, string keyValue)
        {
            
            if (!string.IsNullOrEmpty(entity.ID))
            {
                entity.LastModifierCode = this.UserIdentity.UserCode;
            }
            else
            {
                if (Request.ReqVal("type").ToString() == "mulu")
                {
                    entity.parentId = "0";
                    entity.qx = Convert.ToInt32(Request.ReqVal("mlqx"));
                    entity.mc = Request.ReqVal("mlmc").ToString().Trim(trimstr);
                }
                else
                {
                    string PartCTID = Request.ReqVal("parentId").ToString();
                    entity.parentId = PartCTID;
                }
                entity.CreatorCode = this.UserIdentity.UserCode;
                entity.ksbm = this.UserIdentity.DepartmentCode;
                entity.OrganizeId = OrganizeId;
            }

            if (!string.IsNullOrWhiteSpace(entity.mc))
            {
                entity.mc = entity.mc.ToString().Trim(trimstr);
            }
            if (!string.IsNullOrWhiteSpace(entity.ctnr))
            {
                entity.ctnr = entity.ctnr.ToString().Trim(trimstr);
            }

            _blctglRepo.SubmitForm(entity, entity.ID);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>

        public IActionResult DeleteForm(string delCTID)
        {
            _blctglRepo.DeleteForm(delCTID);
            return Success("Thao tác thành công。");
        }

        public IActionResult ShowTab()
        {
            return View();
        }
        public IActionResult DiagList()
        {
            return View();
        }
        /// <summary>
        /// 诊断列表
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowDiagList()
        {
            return View();
        }

        ///// <summary>
        ///// 诊断 检索
        ///// </summary>
        ///// <param name="keyword"></param>
        ///// <returns></returns>
        //public IActionResult GetDiagnosisList(string keyword, string zdlx, string ybnhlx)
        //{
        //    var list = _CommonDmnService.GetList(this.OrganizeId);
        //    var data = new List<object>();
        //    foreach (var item in list)
        //    {
        //        var obj = new
        //        {
        //            id = item.icd10,
        //            text = item.zdName
        //        };
        //        data.Add(obj);
        //    }
        //    return Content(data.ToJson());
        //    //return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public IActionResult GetDiagnosisGrid(Pagination pagination,string keyword,int? zdlx)
        {
            var data = new
            {
                rows = _CommonDmnService.GetList(pagination,this.OrganizeId, keyword,zdlx),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

		public IActionResult Getyxfhlist(int ls)
		{
			var data = _blctglRepo.Getyxfhlist(ls);
			return Content(data.ToJson());
		}
    }
}